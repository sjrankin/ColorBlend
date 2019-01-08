#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _SCALING_H
#define _SCALING_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ScaleImage(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight, int ScalingMethod);
extern "C" __declspec(dllexport) int NearestNeighborScaling(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight);
extern "C" __declspec(dllexport) int BilinearScaling(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight);

#endif
