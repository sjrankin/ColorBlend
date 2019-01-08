#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _DEINTERLACE_H
#define _DEINTERLACE_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int Deinterlace(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, 
    int StartingLine);

#endif