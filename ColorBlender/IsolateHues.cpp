#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int HueIsolationOp_Clear = 0;
const int HueIsolationOp_Gray = 1;
const int HueIsolationOp_Black = 2;
const int HueIsolationOp_White = 3;
const int HueIsolationOp_Grayscale = 4;
const int HueIsolationOp_Copy = 5;
const int HueIsolationOp_Desaturate = 6;
const int HueIsolationOp_Deluminate = 7;

int IsolateHues(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, double HueRangeStart, double HueRangeEnd, int IsolateForegroundOp,
	int IsolateBackgroundOp)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	if (HueRangeStart < 0.0)
		HueRangeStart = 0.0;
	if (HueRangeEnd > 360.0)
		HueRangeEnd = 360.0;
	if (HueRangeStart > HueRangeEnd)
	{
		double scratch = HueRangeStart;
		HueRangeStart = HueRangeEnd;
		HueRangeEnd = scratch;
	}

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			if ((H >= HueRangeStart) && (H <= HueRangeEnd))
			{
				switch (IsolateForegroundOp)
				{

				}
				Dest[Index + 3] = A;
				Dest[Index + 2] = R;
				Dest[Index + 1] = G;
				Dest[Index + 0] = B;
			}
			else
			{
				switch (IsolateBackgroundOp)
				{
				case HueIsolationOp_Clear:
					Dest[Index + 3] = 0x0;
					Dest[Index + 2] = 0x0;
					Dest[Index + 1] = 0x0;
					Dest[Index + 0] = 0x0;
					break;

				case HueIsolationOp_Gray:
					Dest[Index + 3] = 0xff;
					Dest[Index + 2] = 0xc0;
					Dest[Index + 1] = 0xc0;
					Dest[Index + 0] = 0xc0;
					break;

				case HueIsolationOp_White:
					Dest[Index + 3] = 0xff;
					Dest[Index + 2] = 0xff;
					Dest[Index + 1] = 0xff;
					Dest[Index + 0] = 0xff;
					break;

				case HueIsolationOp_Black:
					Dest[Index + 3] = 0xff;
					Dest[Index + 2] = 0x0;
					Dest[Index + 1] = 0x0;
					Dest[Index + 0] = 0x0;
					break;

				case HueIsolationOp_Grayscale:
				{
					BYTE Gr = (BYTE)(((double)R + (double)G + (double)B) / 3.0);
					Dest[Index + 3] = 0xff;
					Dest[Index + 2] = Gr;
					Dest[Index + 1] = Gr;
					Dest[Index + 0] = Gr;
				}
				break;

				case HueIsolationOp_Copy:
					Dest[Index + 3] = R;
					Dest[Index + 2] = R;
					Dest[Index + 1] = G;
					Dest[Index + 0] = B;
					break;

				case HueIsolationOp_Desaturate:
				{
					BYTE DR, DG, DB;
					HSLtoRGB8(H, 0.0, L, &DR, &DG, &DB);
					Dest[Index + 3] = A;
					Dest[Index + 2] = DR;
					Dest[Index + 1] = DG;
					Dest[Index + 0] = DB;
				}
				break;

				case HueIsolationOp_Deluminate:
				{
					BYTE DR, DG, DB;
					double DL = L * 0.35;
					HSLtoRGB8(H, S, DL, &DR, &DG, &DB);
					Dest[Index + 3] = A;
					Dest[Index + 2] = DR;
					Dest[Index + 1] = DG;
					Dest[Index + 0] = DB;
				}
				break;

				default:
					Dest[Index + 3] = 0xff;
					Dest[Index + 2] = R;
					Dest[Index + 1] = 0x0;
					Dest[Index + 0] = 0x0;
					break;
				}
			}
		}
	}

	return Success;
}