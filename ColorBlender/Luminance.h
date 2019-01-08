#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _LUMINANCE_H
#define _LUMINANCE_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int InvertLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, BOOL UseThreshold, BYTE LuminanceThreshold);

#endif