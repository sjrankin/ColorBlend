#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _SEGMENTS_H
#define _SEGMENTS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int SegmentBlocks(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 BlocksX, __int32 BlocksY, __int32 SegmentType, __int32 ShapeType, __int32 ShapeMargin, 
    BOOL OverrideTransparency, BOOL GradientTransparency, double OverriddenTransparency,
    void *Destination, UINT32 BGColor, BOOL InvertSpatially, BOOL HighlightByLuminance, BOOL InvertHighlighting);

extern "C" __declspec(dllexport) int SegmentBlocks2(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 BlocksX, __int32 BlocksY, void *Destination,
    BOOL ShowGrid, UINT32 GridColor, BOOL HighlightCell, __int32 HightlightCellX,
    __int32 HighlightCellY, UINT32 CellHighlightColor);

extern "C" __declspec(dllexport) int SegmentDrawColorShape(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, __int32 ShapeType, __int32 ShapeMargin,
    UINT32 PackedColor, UINT32 PackedBGColor, BOOL OverrideTransparency, BOOL GradientTransparency,
    double OverriddenTransparency, BOOL InvertSpatially);

extern "C" __declspec(dllexport) int SegmentDrawColorShape2(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, __int32 ShapeType, __int32 ShapeMargin,
    UINT32 PackedColor, UINT32 PackedBGColor, double Brightest, double Darkest,
    __int32 BrightX, __int32 BrightY, __int32 DarkX, __int32 DarkY);

#endif