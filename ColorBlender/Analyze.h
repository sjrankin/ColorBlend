#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _ANALYZE_H
#define _ANALYZE_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int RegionMeanColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedMeanColor, BOOL GetHighlight, double *BrightestLuminance, 
    double *DarkestLuminance, __int32 *BrightX, __int32 *BrightY, __int32 *DarkX, __int32 *DarkY);
extern "C" __declspec(dllexport) int RegionMedianColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedMedianColor);
extern "C" __declspec(dllexport) int RegionBrightestColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedBrightestColor);
extern "C" __declspec(dllexport) int RegionDarkestColor(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *PackedDarkestColor);
extern "C" __declspec(dllexport) int RegionLuminanceValue(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 RegionX1, __int32 RegionY1, __int32 RegionX2, __int32 RegionY2,
    UINT32 *LuminenceAsColor);

#endif
