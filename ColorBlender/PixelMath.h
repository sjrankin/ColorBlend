#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include <time.h>
#include "Structures.h"

#ifndef _PIXEL_MATH_H
#define _PIXEL_MATH_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int PixelMathLogicalOperation(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int Operation, int Constant,
	BOOL ApplyToAlpha, BOOL ApplyToRed, BOOL ApplyToGreen, BOOL ApplyToBlue);
extern "C" __declspec(dllexport) int PixelMathOperation(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int Operation, BOOL NormalizeResults, BOOL NormalizeValues,
	BOOL ApplyToAlpha, BOOL ApplyToRed, BOOL ApplyToGreen, BOOL ApplyToBlue);

#endif