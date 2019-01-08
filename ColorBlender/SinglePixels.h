#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _SINGLEPIXELS_H
#define _SINGLEPIXELS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) UINT32 GetPixelAtLocation(void *Source, __int32 SourceWidth, __int32 SourceHeight,
	__int32 X, __int32 Y);
extern "C" __declspec(dllexport) UINT32 GetPixelAtLocation2(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	__int32 X, __int32 Y);

#endif
