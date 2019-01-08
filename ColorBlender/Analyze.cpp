#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int RegionMeanColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedMeanColor, BOOL GetHighlight,
    double *BrightestLuminance, double *DarkestLuminance,
    __int32 *BrightX, __int32 *BrightY, __int32 *DarkX, __int32 *DarkY)
{
    if (Source == NULL)
        return ErrorStackPushReturn(NullPointer, "RegionMeanColor");
    if (RegionX1 < 0 || RegionX2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMeanColor", "Horizontal too small.");
    if (RegionX1 >= Width || RegionX2 >= Width)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMeanColor", "Horizontal too big.");
    if (RegionY1 < 0 || RegionY2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMeanColor", "Vertical too small.");
    if (RegionY1 >= Height || RegionY2 >= Height)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMeanColor", "Vertical too big");
    if (RegionX1 >= RegionX2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMeanColor", "Left greater than right.");
    if (RegionY1 >= RegionY2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMeanColor", "Top greater than bottom.");

    *BrightX = -1;
    *BrightY = -1;
    *DarkX = -1;
    *DarkY = -1;
    *BrightestLuminance = -1.0;
    *DarkestLuminance = 1000.0;

    __int32 Alpha = 0;
    __int32 Red = 0;
    __int32 Green = 0;
    __int32 Blue = 0;
    __int32 ColorCount = 0;
    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;

    for (int Row = RegionY1; Row <= RegionY2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = RegionX1; Column <= RegionX2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Alpha += Src[Index + 3];
            Red += Src[Index + 2];
            Green += Src[Index + 1];
            Blue += Src[Index + 0];
            ColorCount++;
            if (GetHighlight)
            {
                double PixLum = GetPixelLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
                if (PixLum > *BrightestLuminance)
                {
                    *BrightestLuminance = PixLum;
                    *BrightX = Column;
                    *BrightY = Row;
                }
                if (PixLum < *DarkestLuminance)
                {
                    *DarkestLuminance = PixLum;
                    *DarkX = Column;
                    *DarkY = Row;
                }
            }
        }
    }

    if (ColorCount < 1)
        return NoPixelsSelected;

    BYTE A = (BYTE)(Alpha / ColorCount);
    BYTE R = (BYTE)(Red / ColorCount);
    BYTE G = (BYTE)(Green / ColorCount);
    BYTE B = (BYTE)(Blue / ColorCount);

    *PackedMeanColor = (A << 24) | (R << 16) | (G << 8) | B;

    return ErrorStackPushReturn(Success, "RegionMeanColor");
}

int RegionBrightestColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedBrightestColor)
{
    if (Source == NULL)
        return ErrorStackPushReturn(NullPointer, "RegionBrightestColor");
    if (RegionX1 < 0 || RegionX2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionBrightestColor", "Horizontal too small.");
    if (RegionX1 >= Width || RegionX2 >= Width)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionBrightestColor", "Horizontal too big.");
    if (RegionY1 < 0 || RegionY2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionBrightestColor", "Vertical too small.");
    if (RegionY1 >= Height || RegionY2 >= Height)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionBrightestColor", "Vertical too big");
    if (RegionX1 >= RegionX2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionBrightestColor", "Left greater than right.");
    if (RegionY1 >= RegionY2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionBrightestColor", "Top greater than bottom.");

    __int32 Alpha = 0;
    __int32 Red = 0;
    __int32 Green = 0;
    __int32 Blue = 0;
    __int32 ColorCount = 0;
    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    double Luminance = 0.0;

    for (int Row = RegionY1; Row <= RegionY2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = RegionX1; Column <= RegionX2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            double ScratchLuminance = GetPixelLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
            if (ScratchLuminance > Luminance)
            {
                Red = Src[Index + 2];
                Green = Src[Index + 1];
                Blue = Src[Index + 0];
                Luminance = ScratchLuminance;
            }
            ColorCount++;
        }
    }

    if (ColorCount < 1)
        return ErrorStackPushReturn(NoPixelsSelected, "RegionBrightestColor");

    *PackedBrightestColor = (0xff << 24) | ((BYTE)Red << 16) | ((BYTE)Green << 8) | (BYTE)Blue;

    return ErrorStackPushReturn(Success, "RegionBrightestColor");
}

int RegionDarkestColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedDarkestColor)
{
    if (Source == NULL)
        return ErrorStackPushReturn(NullPointer, "RegionDarkestColor");
    if (RegionX1 < 0 || RegionX2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionDarkestColor", "Horizontal too small.");
    if (RegionX1 >= Width || RegionX2 >= Width)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionDarkestColor", "Horizontal too big.");
    if (RegionY1 < 0 || RegionY2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionDarkestColor", "Vertical too small.");
    if (RegionY1 >= Height || RegionY2 >= Height)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionDarkestColor", "Vertical too big");
    if (RegionX1 >= RegionX2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionDarkestColor", "Left greater than right.");
    if (RegionY1 >= RegionY2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionDarkestColor", "Top greater than bottom.");

    __int32 Alpha = 0;
    __int32 Red = 0;
    __int32 Green = 0;
    __int32 Blue = 0;
    __int32 ColorCount = 0;
    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    double Luminance = 10.0;

    for (int Row = RegionY1; Row <= RegionY2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = RegionX1; Column <= RegionX2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            double ScratchLuminance = GetPixelLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
            if (ScratchLuminance < Luminance)
            {
                Red = Src[Index + 2];
                Green = Src[Index + 1];
                Blue = Src[Index + 0];
                Luminance = ScratchLuminance;
            }
            ColorCount++;
        }
    }

    if (ColorCount < 1)
        return ErrorStackPushReturn(NoPixelsSelected, "RegionDarkestColor");

    *PackedDarkestColor = (0xff << 24) | ((BYTE)Red << 16) | ((BYTE)Green << 8) | (BYTE)Blue;

    return ErrorStackPushReturn(Success, "RegionDarkestColor");
}

int RegionLuminanceValue(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *LuminanceAsColor)
{
    if (Source == NULL)
        return ErrorStackPushReturn(NullPointer, "RegionLuminanceValue");
    if (RegionX1 < 0 || RegionX2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionLuminanceValue", "Horizontal too small.");
    if (RegionX1 >= Width || RegionX2 >= Width)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionLuminanceValue", "Horizontal too big.");
    if (RegionY1 < 0 || RegionY2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionLuminanceValue", "Vertical too small.");
    if (RegionY1 >= Height || RegionY2 >= Height)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionLuminanceValue", "Vertical too big");
    if (RegionX1 >= RegionX2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionLuminanceValue", "Left greater than right.");
    if (RegionY1 >= RegionY2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionLuminanceValue", "Top greater than bottom.");

    __int32 ColorCount = 0;
    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    double Luminance = 0.0;

    for (int Row = RegionY1; Row <= RegionY2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = RegionX1; Column <= RegionX2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Luminance += GetPixelLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
            ColorCount++;
        }
    }

    if (ColorCount < 1)
        return ErrorStackPushReturn(NoPixelsSelected, "RegionLuminanceValue");

    double MeanLuminance = Luminance / ColorCount;
    BYTE L = (BYTE)(MeanLuminance * 255.0);

    *LuminanceAsColor = (0xff << 24) | (L << 16) | (L << 8) | L;

    return ErrorStackPushReturn(Success, "RegionLuminanceValue");
}

int __cdecl Int32Comparer(const void *arg1, const void *arg2)
{
    return (*(int*)arg1 - *(int*)arg2);
}

int RegionMedianColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedMedianColor)
{
    if (Source == NULL)
        return ErrorStackPushReturn(NullPointer, "RegionMedianColor");
    if (RegionX1 < 0 || RegionX2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMedianColor", "Horizontal too small.");
    if (RegionX1 >= Width || RegionX2 >= Width)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMedianColor", "Horizontal too big.");
    if (RegionY1 < 0 || RegionY2 < 0)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMedianColor", "Vertical too small.");
    if (RegionY1 >= Height || RegionY2 >= Height)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMedianColor", "Vertical too big");
    if (RegionX1 >= RegionX2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMedianColor", "Left greater than right.");
    if (RegionY1 >= RegionY2)
        return ErrorStackPushReturn2(IndexOutOfRange, "RegionMedianColor", "Top greater than bottom.");

    __int32 ColorCount = 0;
    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    int ArraySize = (RegionX2 - RegionX1 + 1) * (RegionY2 - RegionY1 + 1);
    __int32 *RedAccumulator = new __int32[ArraySize];
    __int32 *GreenAccumulator = new __int32[ArraySize];
    __int32 *BlueAccumulator = new __int32[ArraySize];
    __int32 *AlphaAccumulator = new __int32[ArraySize];

    for (int Row = RegionY1; Row <= RegionY2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = RegionX1; Column <= RegionX2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            AlphaAccumulator[ColorCount] = Src[Index + 3];
            RedAccumulator[ColorCount] = Src[Index + 2];
            GreenAccumulator[ColorCount] = Src[Index + 1];
            BlueAccumulator[ColorCount] = Src[Index + 0];
            ColorCount++;
        }
    }

    if (ColorCount < 2)
    {
        delete[] RedAccumulator;
        delete[] GreenAccumulator;
        delete[] BlueAccumulator;
        delete[] AlphaAccumulator;
        return NoPixelsSelected;
    }

    qsort((void *)RedAccumulator, ColorCount, sizeof(__int32), Int32Comparer);
    qsort((void *)GreenAccumulator, ColorCount, sizeof(__int32), Int32Comparer);
    qsort((void *)BlueAccumulator, ColorCount, sizeof(__int32), Int32Comparer);
    qsort((void *)AlphaAccumulator, ColorCount, sizeof(__int32), Int32Comparer);

    BYTE A = 0;
    BYTE R = 0;
    BYTE G = 0;
    BYTE B = 0;

    if (ColorCount % 2 == 1)
    {
        int Index = (ColorCount / 2) + 1;
        A = AlphaAccumulator[Index];
        R = RedAccumulator[Index];
        G = GreenAccumulator[Index];
        B = BlueAccumulator[Index];
    }
    else
    {
        int LowIndex = (ColorCount / 2) - 1;
        int HighIndex = (ColorCount / 2) + 1;
        A = (BYTE)((AlphaAccumulator[LowIndex] + AlphaAccumulator[HighIndex]) / 2);
        R = (BYTE)((RedAccumulator[LowIndex] + RedAccumulator[HighIndex]) / 2);
        G = (BYTE)((GreenAccumulator[LowIndex] + GreenAccumulator[HighIndex]) / 2);
        B = (BYTE)((BlueAccumulator[LowIndex] + BlueAccumulator[HighIndex]) / 2);
    }

    *PackedMedianColor = (A << 24) | (R << 16) | (G << 8) | B;

    return ErrorStackPushReturn(Success, "RegionMedianColor");
}
