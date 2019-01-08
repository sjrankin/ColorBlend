#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _FORMATCONVERSION_H
#define _FORMATCONVERSION_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ConvertGray8ToBGRA32(void *Source, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *Destination);

#endif