#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _METRICS_H
#define _METRICS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ReturnUniqueColors(void *Source, __int32 Width, __int32 Height, UINT32 *UniqueColorCount,
	void *Results, __int32 ColorsToReturn);
extern "C" __declspec(dllexport) int CountUniqueColors(void *Source, __int32 Width, __int32 Height, UINT32 *UniqueColorCount);

#endif
