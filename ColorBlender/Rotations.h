#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _ROTATIONS_H
#define _ROTATIONS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int ImageRotateRightBy(void *Source, __int32 SourceWidth, __int32 SourceHeight,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight,
	int RotateHow);
extern "C" __declspec(dllexport) int ImageRotateRight90(void *Source, __int32 SourceWidth, __int32 SourceHeight, void *Destination,
	__int32 DestinationWidth, __int32 DestinationHeight);
extern "C" __declspec(dllexport) int ImageRotateRight180(void *Source, __int32 SourceWidth, __int32 SourceHeight, void *Destination,
	__int32 DestinationWidth, __int32 DestinationHeight);
extern "C" __declspec(dllexport) int ImageRotateRight270(void *Source, __int32 SourceWidth, __int32 SourceHeight, void *Destination,
	__int32 DestinationWidth, __int32 DestinationHeight);
extern "C" __declspec(dllexport) int RotateBufferRight(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride);
extern "C" __declspec(dllexport) int CardinalImageRotate(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	BOOL RotateLeft);
extern "C" __declspec(dllexport) int ImageRotate(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	double Rotation, UINT32 FillColor);
extern "C" __declspec(dllexport) int ImageRotate90(void *Source, __int32 SourceWidth, __int32 SourceHeight,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight);

#endif
