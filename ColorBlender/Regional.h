#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _REGIONAL_H
#define _REGIONAL_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int RegionalOperation(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    UINT32 RegionWidth, UINT32 RegionHeight, bool DoAlpha, bool DoRed, bool DoGreen, bool DoBlue,
    UINT32 Operator);

#endif