#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include <immintrin.h>

#ifndef _SIMD_CHANNELALIGN_H
#define _SIMD_CHANNELALIGN_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int AlignChannels(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *RedChannel, void *GreenChannel, void *BlueChannel);
extern "C" __declspec(dllexport) int AlignChannels2(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *AlphaChannel, void *RedChannel, void *GreenChannel, void *BlueChannel);
extern "C" __declspec(dllexport) int AssembleFromChannels(void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	void *RedChannel, void *GreenChannel, void *BlueChannel);
extern "C" __declspec(dllexport) int AssembleFromChannels2(void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	void *AlphaChannel, void *RedChannel, void *GreenChannel, void *BlueChannel);
extern "C" __declspec(dllexport) int LinearizeChannelsIntoImage(void *RedChannel, void *GreenChannel, void *BlueChannel, __int32 ChannelSize,
	void *Destination, __int32 Width, __int32 Height, __int32 Stride, __int32 ChannelOrder);

#endif