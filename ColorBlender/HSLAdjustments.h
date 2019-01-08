#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _HSLADJUSTMENTS_H
#define _HSLADJUSTMENTS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int GetHSLImage(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DoubleBuffer);
extern "C" __declspec(dllexport) int MakeRGBFromHSL(void *HSLBuffer, UINT32 DoubleCount, void *Destination, __int32 DestinationWidth,
	__int32 DestinationHeight, __int32 DestinationStride);
extern "C" __declspec(dllexport) int ImageHueShift(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int HueShiftValue);
extern "C" __declspec(dllexport) int AdjustImageHSLValues(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, double HMultipler, double SMultiplier, double LMultiplier);
extern "C" __declspec(dllexport) int Silly_SwapSaturationLuminance(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer);
extern "C" __declspec(dllexport) int RestrictHSL(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int HueRangeSize, int SaturationRangeSize, int LuminanceRangeSize,
	BOOL RestrictHue, BOOL RestrictSaturation, BOOL RestrictLuminance);
extern "C" __declspec(dllexport) int HSLColorReduction(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, int HueRanges, BOOL ReduceSaturation, double SaturationValue,
	BOOL ReduceLuminance, double LuminanceValue);
extern "C" __declspec(dllexport) int HSLBulkSet(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, BOOL SetHue, double NewHue, BOOL SetSaturation, double NewSaturation,
	BOOL SetLuminance, double NewLuminance);
extern "C" __declspec(dllexport) int HSLConditionalModify(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	void *Conditions, int ConditionalCount);
extern "C" __declspec(dllexport) int RGBtoHSLtoRGB(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int RGBImageToHueImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int RGBImageToSaturationImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int RGBImageToLuminanceImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int RGBImageToSLImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int RestrictHues2(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, double HueCount);
extern "C" __declspec(dllexport) int RestrictHueRange(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, double LowHue, double HighHue);

#endif