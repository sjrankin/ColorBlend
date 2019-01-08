#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _ACCUMULATION_H
#define _ACCUMULATION_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int AccumulateDoubleBlock(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Accumulator);
extern "C" __declspec(dllexport) int ConvertBlockToDouble(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void* Destination);

#endif
