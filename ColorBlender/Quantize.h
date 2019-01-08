#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _QUANTIZE_H
#define _QUANTIZE_H

#pragma warning (disable : 4244 4800 4901)

//Image and color reduction functions.
extern "C" __declspec(dllexport) int Octree(void *Source, __int32 Width, __int32 Height, __int32 Stride, int Count, void *OTree, int *OTreeCount);
extern "C" __declspec(dllexport) int ReduceColors(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    void *OTree, int OTreeCount);
extern "C" __declspec(dllexport) UINT32 ReduceColor(void *OTree, UINT32 PackedColor);
extern "C" __declspec(dllexport) UINT32 ReduceColor2(void *OTree, BYTE Red, BYTE Green, BYTE Blue);
extern "C" __declspec(dllexport) int MedianCut(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int MedianCutToIndexed(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *IndexedDestination, void *PaletteData,
    int PaletteSize);

extern "C" __declspec(dllexport) double OverallBrightness(void *Source, __int32 Width, __int32 Height, __int32 Stride);

#endif