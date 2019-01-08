#pragma once
#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"


#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int IsolateHues(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, double HueRangeStart, double HueRangeEnd, int IsolateForegroundOp,
	int IsolateBackgroundOp);

