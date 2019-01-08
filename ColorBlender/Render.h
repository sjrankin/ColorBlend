#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _RENDER_H
#define _RENDER_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int DrawRectangle2_Validate(void *Destination, __int32 Width, __int32 Height,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom);
extern "C" __declspec(dllexport) int DrawRectangle2(void *Destination, __int32 Width, __int32 Height,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, UINT32 RectangleColor);
extern "C" __declspec(dllexport) int DrawRectangle_Validate(void *Source, __int32 Width, __int32 Height, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom);
extern "C" __declspec(dllexport) int DrawRectangle(void *Source, __int32 Width, __int32 Height, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, UINT32 RectangleColor);
extern "C" __declspec(dllexport) int OverlayGrid(void *Source, __int32 Width, __int32 Height, void *Destination,
    int HorizontalFrequency, int VerticalFrequency, UINT32 GridColor);
extern "C" __declspec(dllexport) BOOL DrawBlocks(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *ColorBlockList, __int32 ColorBlockCount, UINT32 DefaultColor);
extern "C" __declspec(dllexport) BOOL DrawLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    BOOL IsHorizontal, __int32 Coordinate, BYTE A, BYTE R, BYTE G, BYTE B);
extern "C" __declspec(dllexport) BOOL DrawAnyLine2(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, UINT32 PackedLineColor,
    BOOL AntiAlias, __int32 LineThicknes);
extern "C" __declspec(dllexport) __int32 RenderColorBlob(void *Target, __int32 ImageWidth, __int32 ImageHeight, __int32 ImageStride,
    UINT32 BlobColor, byte CenterAlpha, byte EdgeAlpha, UINT32 EdgeColor);
extern "C" __declspec(dllexport) BOOL DrawHorizontalLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 Y, BYTE A, BYTE R, BYTE G, BYTE B);
extern "C" __declspec(dllexport) BOOL DrawVerticalLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 X, BYTE A, BYTE R, BYTE G, BYTE B);
extern "C" __declspec(dllexport) BOOL DrawAnyLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, BYTE A, BYTE R, BYTE G, BYTE B,
    BOOL AntiAlias, __int32 LineThicknes);

#endif