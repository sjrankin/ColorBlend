#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _DISPLAYLIST_H
#define _DISPLAYLIST_H

#pragma warning (disable : 4244 4800 4901)

//Display list functions.
extern "C" __declspec(dllexport) __int32 ExecuteDisplayList(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void* RawDisplayList, __int32 DisplayListCount);
extern "C" __declspec(dllexport) int RenderDisplayList(void* Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    DisplayInstructionList2* DisplayList, __int32 DisplayListCount);
extern "C" __declspec(dllexport) int Compositor(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *CommonObjectSet, int ObjectCount);

#endif
