#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _KALEIDOSCOPIC_H
#define _KALEIDOSCOPIC_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int SimpleVerticalKaleidoscope(void *Source, __int32 Width, __int32 Height, void *Destination, __int32 Pivot);
extern "C" __declspec(dllexport) int SimpleHorizontalKaleidoscope(void *Source, __int32 Width, __int32 Height, void *Destination, __int32 Pivot);

#endif