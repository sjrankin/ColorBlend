#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _FILTERS_H
#define _FILTERS_H

#pragma warning (disable : 4244 4800 4901)

//Image manipulation functions.
extern "C" __declspec(dllexport) int SolarizeImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Threshold, BOOL Invert);
extern "C" __declspec(dllexport) int AlphaSolarizeImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, double Threshold, BOOL Invert);
extern "C" __declspec(dllexport) int AlphaSolarizeImage2(void *Source, __int32 Width, __int32 Height, __int32 Stride, double Threshold, BYTE SolarAlpha);
extern "C" __declspec(dllexport) int InvertImageRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, bool IncludeAlpha,
    __int32 Left, __int32 Right, __int32 Top, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int InvertImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, bool IncludeAlpha);
extern "C" __declspec(dllexport) int ImageMeanColorRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, UINT32* PackedMeanColor,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom);
extern "C" __declspec(dllexport) int ImageMeanColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, UINT32* PackedMeanColor);
extern "C" __declspec(dllexport) int MeanImageColorRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int MeanImageColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int BrightnessMapRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int BrightnessMap(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int AutoContrastRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Contrast,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int AutoContrast(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Contrast);
extern "C" __declspec(dllexport) int AutoSaturateRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, __int32 Saturation,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int AutoSaturate(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Saturation);
extern "C" __declspec(dllexport) int ColorThreshold0(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double Threshold, UINT32 PackedColor, BOOL InvertThreshold);
extern "C" __declspec(dllexport) int ColorThreshold(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double Threshold, UINT32 PackedLowColor, UINT32 PackedHighColor);
extern "C" __declspec(dllexport) int ColorThreshold2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double LowThreshold, UINT32 PackedLowColor, double HighThreshold, UINT32 PackedHighColor);
extern "C" __declspec(dllexport) int ColorThreshold3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    void *ThresholdList, int ListCount);
extern "C" __declspec(dllexport) int SepiaToneRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int SepiaTone(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int SetAlpha(void* Target, int TargetWidth, int TargetHeight, int TargetStride, byte NewAlpha);
extern "C" __declspec(dllexport) int SetAlphaByBrightness(void* Target, int TargetWidth, int TargetHeight, int TargetStride, BOOL Invert, BOOL UseExistingAlpha);
extern "C" __declspec(dllexport) int BufferInverter4(void* Source, int Width, int Height, int Stride, void *Destination, BOOL InvertAlpha, BOOL InvertRed,
    BOOL InvertGreen, BOOL InvertBlue,
    BOOL UseAlphaThreshold, BYTE AlphaThreshold,
    BOOL UseRedThreshold, BYTE RedThreshold,
    BOOL UseGreenThreshold, BYTE GreenThreshold,
    BOOL UseBlueThreshold, BYTE BlueThreshold);
extern "C" __declspec(dllexport) int BufferInverter3(void* Source, int Width, int Height, int Stride, void *Destination, BOOL InvertAlpha);
extern "C" __declspec(dllexport) int BufferInverter2(void* Source, int Width, int Height, int Stride, void *Destination,
    int InversionOperation, double LuminanceThreshold, bool InvertThreshold, BOOL InvertAlpha,
    BOOL InvertRed, BOOL InvertGreen, BOOL InvertBlue);
extern "C" __declspec(dllexport) int BufferInverter(void* Target, int TargetWidth, int TargetHeight, int TargetStride,
    int InversionOperation, double LuminanceThreshold, bool InvertThreshold, bool AllowInvertAlpha, BYTE InversionChannels);

extern "C" __declspec(dllexport) UINT32 MeanImageColorValue(void *Source, __int32 Width, __int32 Height, __int32 Stride);
extern "C" __declspec(dllexport) int MeanImageColor2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL IgnoreAlpha,
	UINT32 *MeanColor);

#endif