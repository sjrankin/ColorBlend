#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _CONVOLUTION_H
#define _CONVOLUTION_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int MasterConvolveWithKernel(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, UINT32 PackedBGPixel,
    void *KernelMatrix, int KernelX, int KernelY, double Bias, double Factor,
    BOOL UseAlpha, BOOL UseRed, BOOL UseGreen, BOOL UseBlue, BOOL SkipTransparentPixels, BOOL IncludeTransparentPixels,
    BOOL UseLuminance, double LuminanceThreshold);
extern "C" __declspec(dllexport) int ConvolveWithKernel3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, UINT32 PackedBGPixel,
    void *KernelMatrix, int KernelX, int KernelY, double Bias, double Factor, BOOL SkipTransparentPixels, BOOL IncludeTransparentPixels,
    BOOL UseLuminance, double Luminance);
extern "C" __declspec(dllexport) int ConvolveWithKernel2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, UINT32 PackedBGPixel,
    void *KernelMatrix, int KernelX, int KernelY, double Bias, double Factor);
extern "C" __declspec(dllexport) int EmbossImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);

#endif