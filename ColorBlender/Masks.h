#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _MASKS_H
#define _MASKS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int AlphaSolarize(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Luminance);
extern "C" __declspec(dllexport) int AlphaMaskImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE AlphaLevel);
extern "C" __declspec(dllexport) int MaskByColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    UINT32 LowMaskColor, UINT32 HighMaskColor, BYTE AlphaValue);
extern "C" __declspec(dllexport) int AlphaFromLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL Invert);
extern "C" __declspec(dllexport) int MaskImageFromImageLuminance(void *Base, void *LuminanceLayer, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double LuminanceThreshold, UINT32 MaskedPixel);
extern "C" __declspec(dllexport) int ConditionalAlphaFromLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double LuminanceThreshold, BOOL Invert, UINT32 MaskPixel);
extern "C" __declspec(dllexport) int SetAlphaChannel(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE NewAlpha);
extern "C" __declspec(dllexport) int SetAlphaChannelInPlace(void *Buffer, __int32 Width, __int32 Height, __int32 Stride, BYTE NewAlpha);
extern "C" __declspec(dllexport) int ActionByFrequency(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, FrequencyActionBlock FrequencyAction);

#endif