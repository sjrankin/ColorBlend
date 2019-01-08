#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Swap two pixels.
/// </summary>
/// <param name="Pixel1">First pixel.</param>
/// <param name="Pixel2">Second pixel.</param>
/// <returns>Value indicating operational results.</returns>
int SwapPixel(UINT32 *Pixel1, UINT32 *Pixel2)
{
	if (Pixel1 == NULL)
		return NullPointer;
	if (Pixel2 == NULL)
		return NullPointer;
	UINT32 Scratch = *Pixel1;
	*Pixel1 = *Pixel2;
	*Pixel2 = Scratch;
	return Success;
}

/// <summary>
/// Reverse, in place, a scanline.
/// </summary>
/// <param name="ScanLine">The scanline to reverse.</param>
/// <param name="PixelCount">Number of pixels in the scanline.</param>
/// <returns>Value indicating operational results.</returns>
int InPlaceReverseScanLine(UINT32 *ScanLine, int PixelCount)
{
	if (ScanLine == NULL)
		return NullPointer;

	int WorkingCount = PixelCount / 2;
	for (int i = 0; i < WorkingCount; i++)
	{
		int SwapIndex = (PixelCount - 1) - i;
		SwapPixel(&ScanLine[i], &ScanLine[SwapIndex]);
	}

	return Success;
}

/// <summary>
/// Copies the image in <paramref name="Source"/> to <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source buffer.</param>
/// <param name="Width">Width of both <paramref name="Source"/> and <paramref name="Destination"/>.</param>
/// <param name="Height">Height of both <paramref name="Source"/> and <paramref name="Destination"/>.</param>
/// <param name="Destination">Destination buffer.</param>
int CopyBuffer2(void *Source, __int32 Width, __int32 Height, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Row * Width) + Column;
			Dest[Index] = Src[Index];
		}
	}

	return Success;
}

/// <summary>
/// Copies the image (or any memory buffer) in <paramref name="Source"/> to <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source buffer/image to copy.</param>
/// <param name="BufferSize">
/// Size in bytes of the buffer to copy. Both <paramref name="Source"/> and <paramref name="Destination"/>
/// must be the same size.
/// </param>
/// <param name="Destination">Destination of the copy operation.</param>
/// <returns>Value indicating operational results.</returns>
int CopyBuffer3(void *Source, UINT32 BufferSize, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (BufferSize < 1)
		return Success;

	memcpy_s(Destination, (rsize_t)BufferSize, Source, (rsize_t)BufferSize);
	return Success;
}

/// <summary>
/// Copy a single line to a buffer. The line must have the same stride as the buffer.
/// </summary>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="Width">Width of the destination in pixels.</param>
/// <param name="Height">Height of the destination in scan lines.</param>
/// <param name="Stride">Stride of the destination.</param>
/// <param name="LineBuffer">Pointer to the line to copy.</param>
/// <param name="LineCount">
/// Number of times to copy the line to the destination. Vertical starting point in the destination is updated after each copy.
/// </param>
/// <param name="LineStart">Where to start copying to the buffer, e.g., which line.</param>
/// <returns>Value indicating operational success.</returns>
int CopyHorizontalLine(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
	void *LineBuffer, int LineCount, int LineStart)
{
	if (Destination == NULL)
		return NullPointer;
	if (LineBuffer == NULL)
		return NullPointer;
	if (LineCount < 1)
		return InvalidOperation;
	if (LineStart < 0)
		return InvalidOperation;
	if (LineStart > Height - 1)
		return InvalidOperation;
	if (LineStart + LineCount > Height - 1)
		return InvalidOperation;
	BYTE *Buffer = (BYTE *)Destination;
	BYTE *Line = (BYTE *)LineBuffer;

	for (int Row = LineStart; Row < LineStart + LineCount; Row++)
	{
		int DestIndex = Row * Stride;
		memmove_s(Buffer + (DestIndex), Stride,
			Line, Stride);
	}

	return Success;
}

/// <summary>
/// Copy a column of data (one pixel wide) from 1 to <paramref name="ColumnCount"/> times to the destination buffer.
/// </summary>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="Width">Width of the destination in pixels.</param>
/// <param name="Height">Height of the destination in scan lines.</param>
/// <param name="Stride">Stride of the destination.</param>
/// <param name="ColumnBuffer">Pointer to the column to copy.</param>
/// <param name="ColumnCount">
/// Number of times to copy the column to the destination. Vertical starting point in the destination is updated after each copy.
/// </param>
/// <param name="ColumnStart">Where to start copying to the buffer, e.g., which destination column.</param>
/// <returns>Value indicating operational success.</returns>
int CopyVerticalLine(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
	void *ColumnBuffer, int ColumnCount, int ColumnStart)
{
	if (Destination == NULL)
		return NullPointer;
	if (ColumnBuffer == NULL)
		return NullPointer;
	if (ColumnCount < 1)
		return InvalidOperation;
	if (ColumnStart < 0)
		return InvalidOperation;
	if (ColumnStart > Height - 1)
		return InvalidOperation;
	if (ColumnStart + ColumnCount > Height - 1)
		return InvalidOperation;
	BYTE *Buffer = (BYTE *)Destination;
	BYTE *ColumnData = (BYTE *)ColumnBuffer;
	int PixelSize = 4;

	for (int Column = ColumnStart; Column < ColumnStart + ColumnCount; Column++)
	{
		for (int Row = 0; Row < Height; Row++)
		{
			int Index = (Row * Stride) + Column;
			int ColumnIndex = Row * PixelSize;
			Buffer[Index + 0] = ColumnData[ColumnIndex + 0];
			Buffer[Index + 1] = ColumnData[ColumnIndex + 1];
			Buffer[Index + 2] = ColumnData[ColumnIndex + 2];
			Buffer[Index + 3] = ColumnData[ColumnIndex + 3];
		}
	}

	return Success;
}

/// <summary>
/// Copy a subset region from the source buffer and place it into the destination buffer. The region to be copied must have
/// coordinates valid for the source buffer. The coordinates must be semantically correct (upper-left less than lower-right).
/// </summary>
/// <param name="Source">The source image.</param>
/// <param name="SourceWidth">Width of the source in pixels.</param>
/// <param name="SourceHeight">Height of the source in scan lines.</param>
/// <param name="SourceStride">Stride of the source.</param>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="DestinationWidth">Width of the destination in pixels.</param>
/// <param name="DestinationHeight">Height of the destination in scan lines.</param>
/// <param name="DestinationStride">Stride of the destination.</param>
/// <param name="UpperLeft">Pointer to the upper-left coordinate of the region to copy.</param>
/// <param name="LowerRight">Pointer to the lower-right coordinate of the region to copy.</param>
/// <returns>Value indicating operational success.</returns>
int CopyRegion(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationStride,
	void *UpperLeft, void *LowerRight)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (UpperLeft == NULL)
		return NullPointer;
	if (LowerRight == NULL)
		return NullPointer;

	BYTE *Buffer = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	AbsolutePointStruct *UL = (AbsolutePointStruct *)UpperLeft;
	AbsolutePointStruct *LR = (AbsolutePointStruct *)LowerRight;
	int PixelSize = 4;

	if (UL->X < 0)
		return InvalidOperation;
	if (UL->Y < 0)
		return InvalidOperation;
	if (UL->X >= LR->X)
		return InvalidOperation;
	if (UL->Y >= LR->Y)
		return InvalidOperation;
	if (LR->X >= SourceWidth)
		return InvalidOperation;
	if (LR->Y >= SourceHeight)
		return InvalidOperation;

	int DestRow = 0;
	int DestColumn = 0;

	for (int Row = UL->Y; Row <= LR->Y; Row++)
	{
		int RowOffset = Row * SourceStride;
		int DestRowOffset = DestRow * DestinationStride;
		DestColumn = 0;
		for (int Column = UL->X; Column <= LR->X; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			int DestIndex = (DestColumn * PixelSize) + DestRowOffset;
			Dest[DestIndex + 0] = Buffer[Index + 0];
			Dest[DestIndex + 1] = Buffer[Index + 1];
			Dest[DestIndex + 2] = Buffer[Index + 2];
			Dest[DestIndex + 3] = Buffer[Index + 3];
			DestColumn++;
		}
		DestRow++;
	}

	return Success;
}

/// <summary>
/// Paste a buffer into the destination buffer.
/// </summary>
/// <param name="Destination">The target buffer for the paste.</param>
/// <param name="DestWidth">Width of the destination in pixels.</param>
/// <param name="DestHeight">Height of the destination in scan lines.</param>
/// <param name="DestStride">Stride of the destination.</param>
/// <param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
/// <param name="SourceWidth">Width of the source in pixels.</param>
/// <param name="SourceHeight">Height of the source in scan lines.</param>
/// <param name="SourceStride">Stride of the source.</param>
/// <param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
/// <param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
/// <returns>Value indicating operational success.</returns>
int PasteRegion(void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
	void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *UpperLeft, void *LowerRight)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (UpperLeft == NULL)
		return NullPointer;
	if (LowerRight == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	AbsolutePointStruct *UL = (AbsolutePointStruct *)UpperLeft;
	AbsolutePointStruct *LR = (AbsolutePointStruct *)LowerRight;
	int PixelSize = 4;

	if (UL->X < 0)
		return InvalidOperation;
	if (UL->Y < 0)
		return InvalidOperation;
	if (UL->X >= LR->X)
		return InvalidOperation;
	if (UL->Y >= LR->Y)
		return InvalidOperation;
	if (LR->X >= DestWidth)
		return InvalidOperation;
	if (LR->Y >= DestHeight)
		return InvalidOperation;

	int SrcRow = 0;
	int SrcColumn = 0;
	for (int Row = UL->Y; Row <= LR->Y; Row++)
	{
		int RowOffset = Row * DestStride;
		for (int Column = UL->X; Column <= LR->Y; Column++)
		{
			int DestIndex = (Column * PixelSize) + RowOffset;
			int SrcIndex = (SrcColumn * PixelSize) + (SrcRow * SourceStride);
			Dest[DestIndex + 0] = Src[SrcIndex + 0];
			Dest[DestIndex + 1] = Src[SrcIndex + 1];
			Dest[DestIndex + 2] = Src[SrcIndex + 2];
			Dest[DestIndex + 3] = Src[SrcIndex + 3];
			SrcColumn++;
			if (SrcColumn >= SourceWidth)
			{
				SrcColumn = 0;
				SrcRow++;
			}
		}
	}

	return Success;
}

/// <summary>
/// Paste a buffer into the destination buffer. Source regions outside the destination will be cropped.
/// </summary>
/// <param name="Destination">The target buffer for the paste.</param>
/// <param name="DestWidth">Width of the destination in pixels.</param>
/// <param name="DestHeight">Height of the destination in scan lines.</param>
/// <param name="DestStride">Stride of the destination.</param>
/// <param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
/// <param name="SourceWidth">Width of the source in pixels.</param>
/// <param name="SourceHeight">Height of the source in scan lines.</param>
/// <param name="SourceStride">Stride of the source.</param>
/// <param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
/// <param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
/// <returns>Value indicating operational success.</returns>
int PasteRegion2(void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
	void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *UpperLeft, void *LowerRight)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (UpperLeft == NULL)
		return NullPointer;
	if (LowerRight == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	AbsolutePointStruct *UL = (AbsolutePointStruct *)UpperLeft;
	AbsolutePointStruct *LR = (AbsolutePointStruct *)LowerRight;
	int PixelSize = 4;

	if (UL->X < 0)
		UL->X = SourceWidth - UL->X;
	if (UL->Y < 0)
		UL->Y = SourceHeight - UL->Y;
	if (UL->X >= LR->X)
		return InvalidOperation;
	if (UL->Y >= LR->Y)
		return InvalidOperation;
	if (LR->X >= DestWidth)
		LR->X = DestWidth - LR->X;
	if (LR->Y >= DestHeight)
		LR->Y = DestHeight - LR->Y;

	int SrcRow = 0;
	int SrcColumn = 0;
	for (int Row = UL->Y; Row <= LR->Y; Row++)
	{
		int RowOffset = Row * DestStride;
		for (int Column = UL->X; Column <= LR->Y; Column++)
		{
			int DestIndex = (Column * PixelSize) + RowOffset;
			int SrcIndex = (SrcColumn * PixelSize) + (SrcRow * SourceStride);
			Dest[DestIndex + 0] = Src[SrcIndex + 0];
			Dest[DestIndex + 1] = Src[SrcIndex + 1];
			Dest[DestIndex + 2] = Src[SrcIndex + 2];
			Dest[DestIndex + 3] = Src[SrcIndex + 3];
			SrcColumn++;
			if (SrcColumn >= SourceWidth)
			{
				SrcColumn = 0;
				SrcRow++;
			}
		}
	}

	return Success;
}

/// <summary>
/// Paste a buffer into the destination buffer. Source regions outside the destination will be cropped.
/// </summary>
/// <param name="Destination">The target buffer for the paste.</param>
/// <param name="DestWidth">Width of the destination in pixels.</param>
/// <param name="DestHeight">Height of the destination in scan lines.</param>
/// <param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
/// <param name="SourceWidth">Width of the source in pixels.</param>
/// <param name="SourceHeight">Height of the source in scan lines.</param>
/// <param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
/// <param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
/// <returns>Value indicating operational success.</returns>
int PasteRegion3(void *Destination, __int32 DestWidth, __int32 DestHeight,
	void *Source, __int32 SourceWidth, __int32 SourceHeight, void *UpperLeft, void *LowerRight)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (UpperLeft == NULL)
		return NullPointer;
	if (LowerRight == NULL)
		return NullPointer;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;
	AbsolutePointStruct *UL = (AbsolutePointStruct *)UpperLeft;
	AbsolutePointStruct *LR = (AbsolutePointStruct *)LowerRight;

	if (UL->X < 0)
		UL->X = SourceWidth - UL->X;
	if (UL->Y < 0)
		UL->Y = SourceHeight - UL->Y;
	if (UL->X >= LR->X)
		return InvalidOperation;
	if (UL->Y >= LR->Y)
		return InvalidOperation;
	if (LR->X >= DestWidth)
		LR->X = DestWidth - LR->X;
	if (LR->Y >= DestHeight)
		LR->Y = DestHeight - LR->Y;

	int SrcRow = 0;
	int SrcColumn = 0;
	for (int Row = UL->Y; Row <= LR->Y; Row++)
	{
		int RowOffset = Row * SourceWidth;
		for (int Column = UL->X; Column <= LR->Y; Column++)
		{
			int DestIndex = Column + RowOffset;
			int SrcIndex = SrcColumn + SrcRow;
			Dest[DestIndex] = Src[SrcIndex];
			SrcColumn++;
			if (SrcColumn >= SourceWidth)
			{
				SrcColumn = 0;
				SrcRow++;
			}
		}
	}

	return Success;
}

int ClearBufferDWord(void *Destination, __int32 Width, __int32 Height, UINT32 ClearWith)
{
	if (Destination == NULL)
		return NULL;

	UINT32 *Dest = (UINT32 *)Destination;
	//memset(Dest, ClearWith, (size_t)(Width * Height));
	UINT32 BufferSize = Width * Height;
	//Dest[10] = ClearWith;
	__asm
	{
		mov ecx, BufferSize;
		mov edx, 4;
	Repeat:
		imul eax, edx, ecx;
		mov ebx, DWORD PTR Dest;
		mov edx, DWORD PTR ClearWith;
		mov DWORD PTR[ebx + eax], edx;
		loop Repeat;
	}
	/*
	__asm
	{
		movd cx, BufferSize;
		mov edi, Destination;
		mov esi, 1;
		start :
		mov dword ptr [edi+esi], ClearWidth;
		inc esi;
		loop start;
	}
*/
/*
	for (int Row = 0; Row < Height; Row++)
	{
		int DestIndex = Row * Width;
		memset(Dest, ClearWith, (size_t)Width);
	}
  */
	return Success;
}

/// <summary>
/// Paste a buffer into the destination buffer. Source regions outside the destination will be cropped.
/// </summary>
/// <param name="Destination">The target buffer for the paste.</param>
/// <param name="DestWidth">Width of the destination in pixels.</param>
/// <param name="DestHeight">Height of the destination in scan lines.</param>
/// <param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
/// <param name="SourceWidth">Width of the source in pixels.</param>
/// <param name="SourceHeight">Height of the source in scan lines.</param>
/// <param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
/// <param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
/// <returns>Value indicating operational success.</returns>
int PasteRegion4(void *Destination, __int32 DestWidth, __int32 DestHeight,
	void *Source, __int32 SourceWidth, __int32 SourceHeight, int X1, int Y1, int X2, int Y2)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;
	AbsolutePointStruct *UL = new AbsolutePointStruct();
	UL->X = X1;
	UL->Y = Y1;
	AbsolutePointStruct *LR = new AbsolutePointStruct();
	LR->X = X2;
	LR->Y = Y2;

	if (UL->X < 0)
		UL->X = SourceWidth - UL->X;
	if (UL->Y < 0)
		UL->Y = SourceHeight - UL->Y;
	if (UL->X >= LR->X)
		return InvalidOperation;
	if (UL->Y >= LR->Y)
		return InvalidOperation;
	if (LR->X >= DestWidth)
		LR->X = DestWidth - LR->X;
	if (LR->Y >= DestHeight)
		LR->Y = DestHeight - LR->Y;

	ClearBufferDWord(Destination, DestWidth, DestHeight, 0xff00ff00);

	int SrcRow = 0;
	for (int Row = UL->Y; Row <= LR->Y; Row++)
	{
		if ((Row & 0x1) == 0)
			continue;
		int DestIndex = Row * DestWidth;
		int SrcIndex = SrcRow * SourceWidth;
		memcpy(Dest + DestIndex, Src + SrcIndex, SourceWidth * 4);
		SrcRow++;
	}

	return Success;
}

int ClearBufferRegion2(void *Buffer, __int32 Width, __int32 Height, __int32 Stride, UINT32 FillColor,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	int PixelSize = 4;
	BYTE *Dest = (BYTE *)Buffer;
	BYTE A = (FillColor & 0xff000000) >> 24;
	BYTE R = (FillColor & 0x00ff0000) >> 16;
	BYTE G = (FillColor & 0x0000ff00) >> 8;
	BYTE B = (FillColor & 0x000000ff) >> 0;

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}
	return Success;
}

/// <summary>
/// Copies the contents of <paramref name="Source"/> to <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the contents of <paramref name="Source"/> will be copied.</param>
/// <returns>Value indicating operational results.</returns>
int CopyBufferToBuffer(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = Src[Index + 3];
			Dest[Index + 2] = Src[Index + 2];
			Dest[Index + 1] = Src[Index + 1];
			Dest[Index + 0] = Src[Index + 0];
		}
	}

	return Success;
}

/// <summary>
/// Clears the buffer pointed to by <paramref name="Destination"/> with the color in <paramref name="FillColor"/>.
/// </summary>
/// <param name="Destination">The buffer to be cleared.</param>
/// <param name="Width">Width of the buffer.</param>
/// <param name="Height">Height of the buffer.</param>
/// <param name="Stride">Stride of the buffer.</param>
/// <param name="FillColor">Packed color used to clear the buffer.</param>
/// <returns>Value indication operational success.</returns>
int ClearBuffer2(void *Destination, __int32 Width, __int32 Height, __int32 Stride, UINT32 FillColor)
{
	return ClearBufferRegion(Destination, Width, Height, Stride, FillColor, 0, 0, Width - 1, Height - 1);
}

/// <summary>
/// Copy a circular region from <paramref name="Source"/>. to <paramref name="Destination"/>. Non-copied region of the destination
/// is filled by the color specified in <paramref name="PackedBG"/>.
/// </summary>
/// <param name="Source">Pointer to the source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Where the circular region will be copied to.</param>
/// <param name="DestWidth">Width of the destination buffer.</param>
/// <param name="DestHeight">Height of the destination buffer.</param>
/// <param name="DestStride">Stride of the destination buffer.</param>
/// <param name="X">Horizontal coordinate of the circle to copy.</param>
/// <param name="Y">Vertical coordinate of the circle to copy.</param>
/// <param name="Radius">Radius of the circle to copy.</param>
/// <param name="PackedBG">Packed background color used to fill non-circular pixels.</param>
/// <returns>Value indicating operational success.</returns>
int CopyCircularBuffer(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
	int X, int Y, int Radius, UINT32 PackedBG)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (X + DestWidth > Width - 1)
		return InvalidOperation;
	if (X - DestWidth < 0)
		return InvalidOperation;
	if (Y + DestHeight > Height - 1)
		return InvalidOperation;
	if (Y - DestHeight < 0)
		return InvalidOperation;

	int Result = ClearBuffer2(Destination, DestWidth, DestHeight, DestStride, PackedBG);
	if (Result != Success)
		return Result;

	BYTE A = (PackedBG & 0xff000000) >> 24;
	BYTE R = (PackedBG & 0x00ff0000) >> 16;
	BYTE G = (PackedBG & 0x0000ff00) >> 8;
	BYTE B = (PackedBG & 0x000000ff) >> 0;

	return Success;
}

/// <summary>
/// Swap the contents of two buffers with the same dimensions.
/// </summary>
/// <param name="Buffer1">The first buffer.</param>
/// <param name="Buffer2">The second buffer.</param>
/// <param name="Width">Width of both buffers.</param>
/// <param name="Height">Height of both buffers.</param>
/// <param name="Stride">Stride of both buffers.</param>
int SwapImageBuffers(void *Buffer1, void *Buffer2, __int32 Width, __int32 Height, __int32 Stride)
{
	if (Buffer1 == NULL)
		return NullPointer;
	if (Buffer2 == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Buf1 = (BYTE *)Buffer1;
	BYTE *Buf2 = (BYTE *)Buffer2;
	UINT32 BufferSize = Height * Stride;
	BYTE *Swap = new BYTE[BufferSize];
	int Result = Success;

	Result = CopyBufferToBuffer(Buf1, Width, Height, Stride, Swap);
	if (Result == Success)
	{
		Result = CopyBufferToBuffer(Buf2, Width, Height, Stride, Buf1);
		if (Result == Success)
		{
			Result = CopyBufferToBuffer(Swap, Width, Height, Stride, Buf2);
		}
	}
	delete[] Swap;
	return Result;
}

int ExtractScanLine(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int X2, int Y)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (X1 < 0)
		return InvalidOperation;
	if (X2 > Width - 1)
		return InvalidOperation;
	if (X1 > X2)
		return InvalidOperation;
	if (Y < 0)
		return InvalidOperation;
	if (Y > Height - 1)
		return InvalidOperation;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	int RowOffset = Y * Width;
	for (int Column = X1; Column <= X2; Column++)
	{
		int Index = Column + RowOffset;
		Dest[Index] = Src[Index];
	}
	return Success;
}

int CopyBufferRegionPixel(void *Source, __int32 Width, __int32 Height, void *Destination,
	int X1, int Y1, int X2, int Y2)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (X1 < 0)
		return InvalidOperation;
	if (Y1 < 0)
		return InvalidOperation;
	if (X2 > Width - 1)
		return InvalidOperation;
	if (Y2 > Height - 1)
		return InvalidOperation;
	if (X1 >= X2)
		return InvalidOperation;
	if (Y1 >= Y2)
		return InvalidOperation;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	int DestIndex = 0;
	for (int Row = Y1; Row <= Y2; Row++)
	{
		int RowOffset = Row * Width;
		for (int Column = X1; Column <= X2; Column++)
		{
			int Index = Column + RowOffset;
			Dest[DestIndex] = Src[Index];
			DestIndex++;
		}
	}

	return Success;
}
