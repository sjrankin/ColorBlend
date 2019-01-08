#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _CROPPING_H
#define _CROPPING_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ImageCrop(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    void *Destination);

#endif#pragma once
