#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _BUFFERMATH_H
#define _BUFFERMATH_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int AccumulateDoubleBlock(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Accumulator);
extern "C" __declspec(dllexport) int ConvertBlockToDouble(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void* Destination);
extern "C" __declspec(dllexport) int ByteBlockOperationByChannel(void* Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Operator, __int32 Operand, BOOL DoAlpha, BOOL DoRed, BOOL DoGreen, BOOL DoBlue);
extern "C" __declspec(dllexport) int ByteBlocksOperation(void* Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
    void* BufferA, void* BufferB, __int32 Operator, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int MassImageArithmetic(void *Destination, __int32 Width, __int32 Height, __int32 Stride, void *ImageSet, int ImageCount,
    int Operation, void *Extra);
extern "C" __declspec(dllexport) int ByteBlockOperation(void* Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Operator, BYTE Operand, bool IncludeAlpha);
extern "C" __declspec(dllexport) int DoubleBlockOperation(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 Operator, double Operand, bool IncludeAlpha);
extern "C" __declspec(dllexport) int PixelChannelRollingLogicalOperation(void *Source, int Width, int Height, int Stride, void *Destination,
    int LogicalOperator, BOOL RightToLeft, BYTE Mask, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int ApplyChannelMasks2(void *Source, int Width, int Height, int Stride, void *Destination,
    int LogicalOperator, BYTE AlphaMask, BOOL UseAlpha, BYTE RedMask, BOOL UseRed, BYTE GreenMask,
    BOOL UseGreen, BYTE BlueMask, BOOL UseBlue);
extern "C" __declspec(dllexport) int ApplyChannelMasks(void *Source, int Width, int Height, int Stride, void *Destination,
    int LogicalOperator, BYTE AlphaMask, BYTE RedMask, BYTE GreenMask, BYTE BlueMask, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int ChannelShiftBits(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL ShiftRight,
    int ShiftAmount, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) int ChannelRollBits(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL RollRight,
    int RollAmount, BOOL IncludeAlpha);
extern "C" __declspec(dllexport) UINT32 PixelSum(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) int PixelSumDouble(double *DoublePixels, int PixelCount, double *AlphaSum, double *RedSum, double *GreenSum, double *BlueSum);
extern "C" __declspec(dllexport) UINT32 MeanPixel(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) UINT32 MedianPixel(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) UINT32 BrightestPixel(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) UINT32 DarkestPixel(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) UINT32 ClosestPixelLuminance(UINT32 *PackedPixels, int PixelCount, double LuminanceTarget);
extern "C" __declspec(dllexport) UINT32 LeastClosestPixelLuminance(UINT32 *PackedPixels, int PixelCount, double LuminanceTarget);
extern "C" __declspec(dllexport) UINT32 SmallestAlphaPixel(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) UINT32 GreatestAlphaPixel(UINT32 *PackedPixels, int PixelCount);
extern "C" __declspec(dllexport) UINT32 ClosestAlphaPixel(UINT32 *PackedPixels, int PixelCount, BYTE ClosestTo);
extern "C" __declspec(dllexport) UINT32 FarthestAlphaPixel(UINT32 *PackedPixels, int PixelCount, BYTE FarthestFrom);

#endif