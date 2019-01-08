#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _RENDERING_H
#define _RENDERING_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) bool BlendColors(void *Target, __int32 Width, __int32 Height, __int32 Stride,
    __int32 PureColorCount, void *ColorLocations, void *PureColors);
extern "C" __declspec(dllexport) __int32 MergePlanes(void *Target, void *PlaneSet, int PlaneCount, __int32 Width, __int32 Height, __int32 Stride);
extern "C" __declspec(dllexport) int MergePlanes2(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *PlaneSet, int PlaneCount);
extern "C" __declspec(dllexport) int MergePlanes3(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *PlaneSet, int PlaneCount);
extern "C" __declspec(dllexport) int MergePlanes4(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *PlaneSet, int PlaneCount,
    void *Results);

#endif