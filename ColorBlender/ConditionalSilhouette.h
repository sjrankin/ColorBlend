#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _CONDITIONALSILHOUETTE_H
#define _CONDITIONALSILHOUETTE_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int SilhouetteIf(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL UseHue,
    double HueThreshold, double HueRange, BOOL UseSaturation, double SaturationThreshold, double SaturationRange,
    BOOL UseLuminance, double LuminanceThreshold,
    BOOL LuminanceGreaterThan, UINT32 SilhouetteColor);

#endif