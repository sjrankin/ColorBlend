#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const __int32 MaxBinCount = 256;
const __int32 MinBinCount = 8;

/// <summary>
/// Create a histogram with the supplied number of bins for a region in the buffer pointer to by <paramref name="Source"/>.
/// </summary>
/// <param name="Source">The buffer used to create the histogram.</param>
/// <param name="SourceWidth">Width of the buffer in pixels.</param>
/// <param name="SourceHeight">Height of the buffer in scan lines.</param>
/// <param name="SourceStride">Stride of the buffer.</param>
/// <param name="BinCount">Number of bins. Clamped to 8 to 256.</param>
/// <param name="RawRed">On success, will contain the raw number of red pixels per bin.</param>
/// <param name="PercentRed">On success, will contain the percentage per bin for the red channel.</param>
/// <param name="GreenCount">On success, will contain the total number of red pixels.</param>
/// <param name="RawGreen">On success, will contain the raw number of green pixels per bin.</param>
/// <param name="PercentGreen">On success, will contain the percentage per bin for the green channel.</param>
/// <param name="GreenCount">On success, will contain the total number of green pixels.</param>
/// <param name="RawBlue">On success, will contain the raw number of blue pixels per bin.</param>
/// <param name="PercentBlue">On success, will contain the percentage per bin for the blue channel.</param>
/// <param name="BlueCount">On success, will contain the total number of blue pixels.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <returns>Value indicating operational success.</returns>
int CreateHistogramRegion(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 BinCount,
    void *RawRed, void *PercentRed, UINT32& RedCount,
    void *RawGreen, void *PercentGreen, UINT32& GreenCount,
    void *RawBlue, void *PercentBlue, UINT32& BlueCount,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
    if (Source == NULL)
        return NullPointer;
    if ((RawRed == NULL) || (PercentRed == NULL))
        return NullPointer;
    if ((RawGreen == NULL) || (PercentGreen == NULL))
        return NullPointer;
    if ((RawBlue == NULL) || (PercentBlue == NULL))
        return NullPointer;
    if (Left < 0)
        return InvalidOperation;
    if (Right >= SourceWidth)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= SourceHeight)
        return InvalidOperation;

    //Clamp the bin count.
    if (BinCount < 8)
        BinCount = 8;
    if (BinCount > 256)
        BinCount = 256;

    BYTE *Buffer = (BYTE *)Source;
    int PixelSize = 4;
    UINT32 *Reds = (UINT32 *)RawRed;
    double *RedPercent = (double *)PercentRed;
    UINT32 *Greens = (UINT32 *)RawGreen;
    double *GreenPercent = (double *)PercentGreen;
    UINT32 *Blues = (UINT32 *)RawBlue;
    double *BluePercent = (double *)PercentBlue;
    UINT32 TotalRed = 0;
    UINT32 TotalGreen = 0;
    UINT32 TotalBlue = 0;

    for (int i = 0; i < BinCount; i++)
    {
        Reds[i] = Greens[i] = Blues[i] = 0;
        RedPercent[i] = GreenPercent[i] = BluePercent[i] = 0.0;
    }

    //double IndexDivisor = BinCount;//(double)MaxBinCount / (double)BinCount;
    double BinDivisor = 1.0 / ((double)BinCount / (double)MaxBinCount);

    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * SourceStride;
        for (int Column = Left; Column <= Right; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE R = Buffer[Index + 2];
            BYTE G = Buffer[Index + 1];
            BYTE B = Buffer[Index + 0];
            //Get the bin indices.
            int RIndex = (int)(R / BinDivisor);
            int GIndex = (int)(G / BinDivisor);
            int BIndex = (int)(B / BinDivisor);
            //Accumulate pixel counts.
            Reds[RIndex]++;
            Greens[GIndex]++;
            Blues[BIndex]++;
        }
    }

    //Create non-0 pixel counts.
    for (int i = 0; i < BinCount; i++)
    {
        TotalRed += Reds[i];
        TotalGreen += Greens[i];
        TotalBlue += Blues[i];
    }

    //Generate percents per pixel.
    for (int i = 0; i < BinCount; i++)
    {
        RedPercent[i] = TotalRed == 0 ? 0.0 : (double)Reds[i] / (double)TotalRed;
        GreenPercent[i] = TotalGreen == 0 ? 0.0 : (double)Greens[i] / (double)TotalGreen;
        BluePercent[i] = TotalBlue == 0 ? 0.0 : (double)Blues[i] / (double)TotalBlue;
    }

    RedCount = TotalRed;
    GreenCount = TotalGreen;
    BlueCount = TotalBlue;

    return Success;
}

/// <summary>
/// Create a histogram with the supplied number of bins for the buffer pointer to by <paramref name="Source"/>.
/// </summary>
/// <param name="Source">The buffer used to create the histogram.</param>
/// <param name="SourceWidth">Width of the buffer in pixels.</param>
/// <param name="SourceHeight">Height of the buffer in scan lines.</param>
/// <param name="SourceStride">Stride of the buffer.</param>
/// <param name="BinCount">Number of bins. Clamped to 8 to 256.</param>
/// <param name="RawRed">On success, will contain the raw number of red pixels per bin.</param>
/// <param name="PercentRed">On success, will contain the percentage per bin for the red channel.</param>
/// <param name="GreenCount">On success, will contain the total number of red pixels.</param>
/// <param name="RawGreen">On success, will contain the raw number of green pixels per bin.</param>
/// <param name="PercentGreen">On success, will contain the percentage per bin for the green channel.</param>
/// <param name="GreenCount">On success, will contain the total number of green pixels.</param>
/// <param name="RawBlue">On success, will contain the raw number of blue pixels per bin.</param>
/// <param name="PercentBlue">On success, will contain the percentage per bin for the blue channel.</param>
/// <param name="BlueCount">On success, will contain the total number of blue pixels.</param>
/// <returns>Value indicating operational success.</returns>
int CreateHistogram(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 BinCount,
    void *RawRed, void *PercentRed, UINT32& RedCount,
    void *RawGreen, void *PercentGreen, UINT32& GreenCount,
    void *RawBlue, void *PercentBlue, UINT32& BlueCount)
{
    return CreateHistogramRegion(Source,  SourceWidth, SourceHeight, SourceStride, BinCount,
        RawRed, PercentRed, RedCount,
        RawGreen, PercentGreen, GreenCount,
        RawBlue, PercentBlue, BlueCount,
        0, 0, SourceWidth - 1, SourceHeight - 1);
}
