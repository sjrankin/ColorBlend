#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _COMPARITORS_H
#define _COMPARITORS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ImageDeltaComparisonRegion(void *Image1, void *Image2, __int32 Width, __int32 Height, __int32 Stride,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, UINT32 *MismatchIndex);
extern "C" __declspec(dllexport) int ImageDeltaComparison(void *Image1, void *Image2, __int32 Width, __int32 Height, __int32 Stride, UINT32 *MismatchIndex);

#endif