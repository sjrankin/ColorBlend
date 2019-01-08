#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _GENERATED_H
#define _GENERATED_H

#pragma warning (disable : 4244 4800 4901)

//Generated image functions.
extern "C" __declspec(dllexport) int RenderRandomColorRectangle(void *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BYTE LowAlpha, BYTE HighAlpha, BYTE LowRed, BYTE HighRed, BYTE LowGreen, BYTE HighGreen, BYTE LowBlue, BYTE HighBlue,
    UINT32 Seed);
extern "C" __declspec(dllexport) int RenderRampingColorRectangle(void  *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BOOL RampAlpha, BYTE AlphaStart, BYTE AlphaIncrement, BYTE NonRampAlpha,
    BOOL RampRed, BYTE RedStart, BYTE RedIncrement, BYTE NonRampRed,
    BOOL RampGreen, BYTE GreenStart, BYTE GreenIncrement, BYTE NonRampGreen,
    BOOL RampBlue, BYTE BlueStart, BYTE BlueIncrement, BYTE NonRampBlue);
extern "C" __declspec(dllexport) int RenderRandomSubBlockRectangle(void *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    __int32 BlockWidth, __int32 BlockHeight, UINT32 Seed);
extern "C" __declspec(dllexport) int RenderRampingGradientColorRectangle(void  *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    UINT32 PackedStartColor, UINT32 PackedEndColor, BOOL IgnoreAlpha, BOOL DoHorizontal);
extern "C" __declspec(dllexport) int RenderLinearGradients(void *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BOOL IgnoreAlpha, BOOL DoHorizontal, void *Stops, int StopCount);
extern "C" __declspec(dllexport) int Gradient(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *GradientColorList, __int32 GradientColorCount);

#endif