#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _DISTORT_H
#define _DISTORT_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int HorizontalMirrorPixelRegion(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int Y1, int X2, int Y2);
extern "C" __declspec(dllexport) int HorizontalMirrorPixel(void *Source, __int32 Width, __int32 Height, void *Destination);
extern "C" __declspec(dllexport) int HorizontalMirrorByte(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SetAlpha, BYTE AlphaValue);
extern "C" __declspec(dllexport) int VerticalMirrorPixelRegion(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int Y1, int X2, int Y2);
extern "C" __declspec(dllexport) int VerticalMirrorPixel(void *Source, __int32 Width, __int32 Height, void *Destination);
extern "C" __declspec(dllexport) int VerticalMirrorByte(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SetAlpha, BYTE AlphaValue);
extern "C" __declspec(dllexport) int ULtoLRPixel(void *Source, __int32 Width, __int32 Height, void *Destination);
extern "C" __declspec(dllexport) int ULtoLRByte(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SetAlpha, BYTE AlphaValue);
extern "C" __declspec(dllexport) int RotateBufferRight(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride);
extern "C" __declspec(dllexport) int ShuffleRows(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, void *RowDescriptions, int RowDescriptionCount);
extern "C" __declspec(dllexport) int SquishImage(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
    __int32 HorizontalFrequency, __int32 VerticalFrequency);

#endif