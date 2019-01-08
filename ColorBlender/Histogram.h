#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _HISTOGRAM_H
#define _HISTOGRAM_H

#pragma warning (disable : 4244 4800 4901)


//Histogram functions.
extern "C" __declspec(dllexport) int CreateHistogramRegion(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 BinCount,
    void *RawRed, void *PercentRed, UINT32& RedCount,
    void *RawGreen, void *PercentGreen, UINT32& GreenCount,
    void *RawBlue, void *PercentBlue, UINT32& BlueCount,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom);
extern "C" __declspec(dllexport) int CreateHistogram(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 BinCount,
    void *RawRed, void *PercentRed, UINT32 &TotalRed,
    void *RawGreen, void *PercentGreen, UINT32 &TotalGreen,
    void *RawBlue, void *PercentBlue, UINT32 &TotalBlue);

#endif