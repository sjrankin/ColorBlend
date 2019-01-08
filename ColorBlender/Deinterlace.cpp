#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Determines if the pixel in the specified location is the specified color.
/// </summary>
/// <param name="Source">The image buffer to check.</param>
/// <param name="PixelLocation">The location of the pixel in the buffer.</param>
/// <param name="R">The red component.</param>
/// <param name="G">The green component.</param>
/// <param name="B">The blue component.</param>
/// <returns>TRUE if the pixel matches the passed color, false if not.</returns>
BOOL PixelIsColor(void *Source, __int32 PixelLocation, BYTE R, BYTE G, BYTE B)
{
    if (Source == NULL)
        return FALSE;
    BYTE *Src = (BYTE *)Source;
    if (
        (Src[PixelLocation + 2] == R) &&
        (Src[PixelLocation + 1] == G) &&
        (Src[PixelLocation + 0] == B)
       )
        return TRUE;
    return FALSE;
}

/// <summary>
/// Determines if the pixel in the specified location is close to the specified color.
/// </summary>
/// <param name="Source">The image buffer to check.</param>
/// <param name="PixelLocation">The location of the pixel in the buffer.</param>
/// <param name="R">The red component.</param>
/// <param name="G">The green component.</param>
/// <param name="B">The blue component.</param>
/// <param name="Range">How close the color must be to "match".</param>
/// <returns>TRUE if the pixel falls within the range of the passed color, false if not.</returns>
BOOL PixelIsCloseToColor(void *Source, __int32 PixelLocation, BYTE R, BYTE G, BYTE B, BYTE Range)
{
    if (Source == NULL)
        return FALSE;
    BYTE *Src = (BYTE *)Source;
    int RDelta = abs((int)Src[PixelLocation + 2] - (int)R);
    int GDelta = abs((int)Src[PixelLocation + 1] - (int)G);
    int BDelta = abs((int)Src[PixelLocation + 0] - (int)B);
    if (
        (RDelta <= Range) &&
        (GDelta <= Range) &&
        (BDelta <= Range)
       )
        return TRUE;
    return FALSE;
}

/// <summary>
/// Returns the mean of two pixels in two locations in the supplied image buffer.
/// </summary>
/// <param name="Address1">Address of the first pixel.</param>
/// <param name="Address2">Address of the second pixel.</param>
/// <param name="FinalA">Will contain the mean alpha channel value.</param>
/// <param name="FinalR">Will contain the mean red channel value.</param>
/// <param name="FinalG">Will contain the mean green channel value.</param>
/// <param name="FinalB">Will contain the mean blue channel value.</param>
/// <returns>Value indicating result of operation.</returns>
int MeanOfTwoPixels(void *Source, __int32 Address1, __int32 Address2, BYTE *FinalA, BYTE *FinalR, BYTE *FinalG, BYTE *FinalB)
{
    *FinalA = 0;
    *FinalR = 0;
    *FinalG = 0;
    *FinalB = 0;
    if (Source == NULL)
        return NullPointer;
    BYTE *Src = (BYTE *)Source;
    BYTE A1 = Src[Address1 + 3];
    BYTE R1 = Src[Address1 + 2];
    BYTE G1 = Src[Address1 + 1];
    BYTE B1 = Src[Address1 + 0];
    BYTE A2 = Src[Address2 + 3];
    BYTE R2 = Src[Address2 + 2];
    BYTE G2 = Src[Address2 + 1];
    BYTE B2 = Src[Address2 + 0];
    *FinalA = (BYTE)(((int)A1 + (int)A2) / 2);
    *FinalR = (BYTE)(((int)R1 + (int)R2) / 2);
    *FinalG = (BYTE)(((int)G1 + (int)G2) / 2);
    *FinalB = (BYTE)(((int)B1 + (int)B2) / 2);
    return Success;
}

/// <summary>
/// Deinterlace (using a simple pixel mean algorithm) the source image and place the result in the destination buffer.
/// </summary>
/// <param name="Source">Pointer to the source image. Not modified by this function.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Where the modified image will be written.</param>
/// <param name="StartingLine">Where the deinterlacing will start - usually set to 0 or 1 but may be anywhere in the image.</param>
/// <returns>Value indicating result of operation.</returns>
int Deinterlace(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int StartingLine)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;
    int Start = abs(StartingLine);
    if (Start >= Height)
        return InvalidOperation;
    BOOL IsEven = Start % 2 == 0 ? TRUE : FALSE;

    for (int Row = Start; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;

        if ((Row - 1 < 0) || (Row + 1 >= Height))
        {
            //Copy the source to the destination without modification.
            for (int C = 0; C < Width; C++)
            {
                int I = (C * PixelSize) + RowOffset;
                Dest[I + 3] = Src[I + 3];
                Dest[I + 2] = Src[I + 2];
                Dest[I + 1] = Src[I + 1];
                Dest[I + 0] = Src[I + 0];
            }
            continue;
        }

        BOOL TargetRow = ((Row) & (0x1)) == 1 ? (IsEven ? FALSE : TRUE) : (IsEven ? TRUE : FALSE);
        int PreviousRowOffset = (Row - 1) * Stride;
        int NextRowOffset = (Row + 1) * Stride;

        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            int PreviousIndex = (Column * PixelSize) + PreviousRowOffset;
            int NextIndex = (Column * PixelSize) + NextRowOffset;

            BOOL BlackPixel = PixelIsCloseToColor(Source, Index, 0, 0, 0, 16);
            if (BlackPixel && TargetRow)
            {
                BYTE FinalA = 0;
                BYTE FinalR = 0;
                BYTE FinalG = 0;
                BYTE FinalB = 0;
                int Result = MeanOfTwoPixels(Source, PreviousIndex, NextIndex, &FinalA, &FinalR, &FinalG, &FinalB);
                if (Result != Success)
                    return Result;
                Dest[Index + 3] = FinalA;
                Dest[Index + 2] = FinalR;
                Dest[Index + 1] = FinalG;
                Dest[Index + 0] = FinalB;
            }
            else
            {
                Dest[Index + 3] = Src[Index + 3];
                Dest[Index + 2] = Src[Index + 2];
                Dest[Index + 1] = Src[Index + 1];
                Dest[Index + 0] = Src[Index + 0];
            }
        }

    }

    return Success;
}