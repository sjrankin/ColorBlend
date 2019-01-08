#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _BORDER_H
#define _BORDER_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int AddBorder(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 LeftBorder, __int32 TopBorder, __int32 RightBorder, __int32 BottomBorder,
    void *Destination, UINT32 BGColor);

#endif