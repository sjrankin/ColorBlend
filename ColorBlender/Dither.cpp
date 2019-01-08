#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int Dither_FloydSteinberg = 0;
const int Dither_FalseFloydSteinberg = 1;
const int Dither_Atkinson = 2;
const int Dither_JarvisJudiceNinke = 3;
const int Dither_Stucki = 4;
const int Dither_Burkes = 5;
const int Dither_Sierra1 = 6;
const int Dither_Sierra2 = 7;
const int Dither_Sierra3 = 8;

int FloydSteinbergDither(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE *Scratch,
    BOOL AsGrayscale)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    int GrayscalePixelSize = 1;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;
    const double Index0Mul = 7.0 / 16.0;
    const double Index1Mul = 1.0 / 16.0;
    const double Index2Mul = 3.0 / 16.0;
    const double Index3Mul = 5.0 / 16.0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Width;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * GrayscalePixelSize) + RowOffset;
            //
            //          X      index0
            //   index2 index3 index1
            int Index0 = -1;
            int Index1 = -1;
            int Index2 = -1;
            int Index3 = -1;
            if (Column < Width - 1)
            {
                Index0 = ((Column + 1) * GrayscalePixelSize) + RowOffset;
                if (Row < Height - 1)
                    Index1 = ((Column + 1) * GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Column > 0)
            {
                if (Row < Height - 1)
                    Index2 = ((Column - 1) * GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Row < Height - 1)
            {
                if (Row < Height - 1)
                    Index3 = ((Column + 0) * GrayscalePixelSize) + ((Row + 1) * Width);
            }

            if (Index0 > -1)
            {
                BYTE V0 = (BYTE)((double)Scratch[Index] * Index0Mul);
                Scratch[Index0] = (BYTE)((Scratch[Index0] + V0) & 0xff);
            }
            if (Index1 > -1)
            {
                BYTE V1 = (BYTE)((double)Scratch[Index] * Index1Mul);
                Scratch[Index1] = (BYTE)((Scratch[Index1] + V1) & 0xff);
            }
            if (Index2 > -1)
            {
                BYTE V2 = (BYTE)((double)Scratch[Index] * Index2Mul);
                Scratch[Index2] = (BYTE)((Scratch[Index2] + V2) & 0xff);
            }
            if (Index3 > -1)
            {
                BYTE V3 = (BYTE)((double)Scratch[Index] * Index3Mul);
                Scratch[Index3] = (BYTE)((Scratch[Index3] + V3) & 0xff);
            }
        }
    }

    return Success;
}

int FalseFloydSteinbergDither(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE *Scratch,
    BOOL AsGrayscale)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    int GrayscalePixelSize = 1;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;
    const double Index0Mul = 3.0 / 8.0;
    const double Index1Mul = 2.0 / 8.0;
    const double Index2Mul = 3.0 / 8.0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Width;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * GrayscalePixelSize) + RowOffset;
            //
            //          X      index0
            //          index2 index1
            int Index0 = -1;
            int Index1 = -1;
            int Index2 = -1;
            if (Column < Width - 1)
            {
                Index0 = ((Column + 1) * GrayscalePixelSize) + RowOffset;
                if (Row < Height - 1)
                    Index1 = ((Column + 1) * GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Column > 0)
            {
                if (Row < Height - 1)
                    Index2 = ((Column + 0) * GrayscalePixelSize) + ((Row + 1) * Width);
            }

            if (Index0 > -1)
            {
                BYTE V0 = (BYTE)((double)Scratch[Index] * Index0Mul);
                Scratch[Index0] = (BYTE)((Scratch[Index0] + V0) & 0xff);
            }
            if (Index1 > -1)
            {
                BYTE V1 = (BYTE)((double)Scratch[Index] * Index1Mul);
                Scratch[Index1] = (BYTE)((Scratch[Index1] + V1) & 0xff);
            }
            if (Index2 > -1)
            {
                BYTE V2 = (BYTE)((double)Scratch[Index] * Index2Mul);
                Scratch[Index2] = (BYTE)((Scratch[Index2] + V2) & 0xff);
            }
        }
    }

    return Success;
}

int AtkinsonDither(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE *Scratch,
    BOOL AsGrayscale)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    int GrayscalePixelSize = 1;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;
    const double Multiplier = 1.0 / 8.0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Width;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * GrayscalePixelSize) + RowOffset;
            //
            //          X      index0 index1
            //   index2 index3 index4
            //          index5
            int Index0 = -1;
            int Index1 = -1;
            int Index2 = -1;
            int Index3 = -1;
            int Index4 = -1;
            int Index5 = -1;
            if (Column < Width - 1)
            {
                Index0 = ((Column + 1) * GrayscalePixelSize) + RowOffset;

            }
            if (Column < Width - 2)
            {
                Index1 = ((Column + 2) * GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Column > 0)
            {
                if (Row < Height - 1)
                    Index2 = ((Column - 1) * GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Row < Height - 1)
            {
                Index3 = ((Column + 0) * GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Column < Width - 1)
            {
                if (Row < Height - 1)
                    Index4 = ((Column + 1)*GrayscalePixelSize) + ((Row + 1) * Width);
            }
            if (Row < Height - 2)
            {
                Index5 = ((Column + 0) * GrayscalePixelSize) + ((Row + 2) * Width);
            }

            if (Index0 > -1)
            {
                BYTE V0 = (BYTE)((double)Scratch[Index] * Multiplier);
                Scratch[Index0] = (BYTE)((Scratch[Index0] + V0) & 0xff);
            }
            if (Index1 > -1)
            {
                BYTE V1 = (BYTE)((double)Scratch[Index] * Multiplier);
                Scratch[Index1] = (BYTE)((Scratch[Index1] + V1) & 0xff);
            }
            if (Index2 > -1)
            {
                BYTE V2 = (BYTE)((double)Scratch[Index] * Multiplier);
                Scratch[Index2] = (BYTE)((Scratch[Index2] + V2) & 0xff);
            }
            if (Index3 > -1)
            {
                BYTE V3 = (BYTE)((double)Scratch[Index] * Multiplier);
                Scratch[Index3] = (BYTE)((Scratch[Index3] + V3) & 0xff);
            }
            if (Index4 > -1)
            {
                BYTE V4 = (BYTE)((double)Scratch[Index] * Multiplier);
                Scratch[Index4] = (BYTE)((Scratch[Index4] + V4) & 0xff);
            }
            if (Index5 > -1)
            {
                BYTE V5 = (BYTE)((double)Scratch[Index] * Multiplier);
                Scratch[Index5] = (BYTE)((Scratch[Index5] + V5) & 0xff);
            }
        }
    }

    return Success;
}

int DoDither(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int DitherType, BOOL AsGrayscale)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    BYTE *Scratch = new BYTE[Height * Width];
    int ScratchIndex = 0;
    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Scratch[ScratchIndex++] = (BYTE)((Src[Index + 2] + Src[Index + 1] + Src[Index + 0]) / 3);
        }
    }

    int DitherResult = NoAction;
    switch (DitherType)
    {
    case Dither_FloydSteinberg:
        DitherResult = FloydSteinbergDither(Source, Width, Height, Stride, Destination, Scratch, TRUE);
        break;

    case Dither_FalseFloydSteinberg:
        DitherResult = FalseFloydSteinbergDither(Source, Width, Height, Stride, Destination, Scratch, TRUE);
        break;

    case Dither_Atkinson:
        DitherResult = AtkinsonDither(Source, Width, Height, Stride, Destination, Scratch, TRUE);
        break;

    default:
        DitherResult = InvalidOperation;
        break;
    }

    ScratchIndex = 0;
    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = 0xff;
            BYTE FinalValue = Scratch[ScratchIndex] < 0x7f ? 0xff : 0x0;
            Dest[Index + 2] = FinalValue;
            Dest[Index + 1] = FinalValue;
            Dest[Index + 0] = FinalValue;
            ScratchIndex++;
        }
    }

    delete[] Scratch;

    return DitherResult;
}