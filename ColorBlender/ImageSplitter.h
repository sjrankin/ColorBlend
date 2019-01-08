#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _IMAGESPLITTER_H
#define _IMAGESPLITTER_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ImageCombine(void *Destination, __int32 Width, __int32 Height, void *Sources, int SubCount,
    UINT32 BGColor);
extern "C" __declspec(dllexport) int ImageSplit(void *Source, __int32 Width, __int32 Height, void *Results, int SubCount);

#endif