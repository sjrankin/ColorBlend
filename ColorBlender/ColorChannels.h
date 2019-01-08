#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _COLORCHANNELS_H
#define _COLORCHANNELS_H

#pragma warning (disable : 4244 4800 4901)

//Color channel functions.
extern "C" __declspec(dllexport) int ChannelShift(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, int ShiftBy);
extern "C" __declspec(dllexport) int ChannelSwap(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *SourceIndices, void *DestIndices, int IndexCount);
extern "C" __declspec(dllexport) int ChannelSwap3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int SwapOrder);
extern "C" __declspec(dllexport) int ChannelSwap4(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int SwapOrder,
    double LuminanceThreshold, int Conditional);
extern "C" __declspec(dllexport) int ChannelMigrate(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, int MigrateBy,
    BOOL MigrateAlpha, BOOL MigrateRed, BOOL MigrateGreen, BOOL MigrateBlue);
extern "C" __declspec(dllexport) int RandomChannelSwap(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    UINT Seed, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int PixelMigrate(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, int MigrateBy, bool IgnoreAlpha);
extern "C" __declspec(dllexport) int ChannelSwap2(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int SwapOrder, void *ExecOptions);
extern "C" __declspec(dllexport) int BufferGrayscaleRegion(void *Buffer, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int GrayscaleType, __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int BufferGrayscale(void *Buffer, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int GrayscaleType);
extern "C" __declspec(dllexport) int GrayLevelsRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int GrayLevels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount);
extern "C" __declspec(dllexport) int ColorLevelsRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut);
extern "C" __declspec(dllexport) int ColorLevels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount);
extern "C" __declspec(dllexport) int ChannelMerge(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    void *RedChannel, void *GreenChannel, void *BlueChannel);
extern "C" __declspec(dllexport) int ChannelMergeAlpha(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    void *AlphaChannel, void *RedChannel, void *GreenChannel, void *BlueChannel);
extern "C" __declspec(dllexport) int CombineChannels32(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    void *Channel1, void *Channel2, void *Channel3, void *Channel4);
extern "C" __declspec(dllexport) int HSLChannelMerge(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    void *HueChannel, void *SaturationChannel, void *LuminanceChannel);
extern "C" __declspec(dllexport) int SplitImageIntoChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *AlphaDest, void *RedDest, void *GreenDest, void *BlueDest);
extern "C" __declspec(dllexport) int SplitImageIntoHSLChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *AlphaDest, void *HueDest, void *SaturationDest, void *LuminanceDest);
extern "C" __declspec(dllexport) int RollingMeanChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int WindowSize, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int RollingMeanChannels2(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int AlphaWindowSize, int RedWindowSize, int GreenWindowSize, int BlueWindowSize);
extern "C" __declspec(dllexport) int SortChannels2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL UseLuminance,
    double LuminanceThreshold);
extern "C" __declspec(dllexport) int SortChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int SortHow, BOOL StoreSortHowAsAlpha, BOOL InvertAlpha);
extern "C" __declspec(dllexport) int SelectRGBChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SelectRed,
    BOOL SelectGreen, BOOL SelectBlue, BOOL AsGray);
extern "C" __declspec(dllexport) int SelectHSLChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SelectHue,
    BOOL SelectSaturation, BOOL SelectLuminance, bool AsGray, int ChannelOrder);
extern "C" __declspec(dllexport) int RGBCombine(void *RedSource, void *GreenSource, void *BlueSource, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE AlphaValue);
extern "C" __declspec(dllexport) int ApplyBrightnessMap(void *Source, void *IlluminationMap, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int GammaCorrection(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Gamma, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int AdjustSaturation(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double SaturationValue);
extern "C" __declspec(dllexport) int HighlightImageColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double HighlightHue, int NonHighlightAction, double HighlightLuminance, double &HueDelta);
extern "C" __declspec(dllexport) int AdjustImageHSL(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double HueAdjustment, double SaturationAdjustment, double LuminanceAdjustment);

#endif