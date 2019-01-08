#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _SEGMENT_H
#define _SEGMENT_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int SegmentizeImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 CellWidth, __int32 CellHeight, __int32 CellOriginX, __int32 CellOriginY, __int32 SegmentPattern);

#endif