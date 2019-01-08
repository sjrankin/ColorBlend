#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _BUFFERS_H
#define _BUFFERS_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int InPlaceReverseScanLine(UINT32 *ScanLine, int PixelCount);
extern "C" __declspec(dllexport) int SwapPixel(UINT32 *Pixel1, UINT32 *Pixel2);
extern "C" __declspec(dllexport) int CopyBuffer3(void *Source, UINT32 BufferSize, void *Destination);
extern "C" __declspec(dllexport) int CopyBuffer2(void *Source, __int32 Width, __int32 Height, void *Destination);
extern "C" __declspec(dllexport) int CropBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void* Region);
extern "C" __declspec(dllexport) int CropBuffer2(void *SourceBuffer, void *DestinationBuffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2);
extern "C" __declspec(dllexport) int CopyBufferRegion(void *SourceBuffer, void *DestinationBuffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2);
extern "C" __declspec(dllexport) BOOL CreateBitMask(void *Target, __int32 TargetWidth, __int32 TargetHeight,
    __int32 Left, __int32 Top, __int32 Width, __int32 Height, BYTE BitOnValue, BYTE BitOffValue);
extern "C" __declspec(dllexport) BOOL CreateMask(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *ImageSource, UINT32 Threshold, BOOL AlphaToo,
    BYTE MaskA, BYTE MaskR, BYTE MaskG, BYTE MaskB);
extern "C" __declspec(dllexport) BOOL CreateMaskFromLuminance(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *ImageSource, double Threshold, BOOL AlphaToo,
    BYTE MaskA, BYTE MaskR, BYTE MaskG, BYTE MaskB);
extern "C" __declspec(dllexport) int CreateAlphaMaskFromLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double Luminance, BOOL UseMaskedPixel, UINT32 MaskedPixelColor);
extern "C" __declspec(dllexport) BOOL ClearBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    BYTE ClearA, BYTE ClearR, BYTE ClearG, BYTE ClearB,
    BOOL DrawGrid, BYTE GridA, BYTE GridR, BYTE GridG, BYTE GridB, __int32 GridCellWidth, __int32 GridCellHeight,
    BOOL DrawOutline, BYTE OutA, BYTE OutR, BYTE OutG, BYTE OutB);
extern "C" __declspec(dllexport) int FillBufferWithBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    UINT32 EmptyColor);
extern "C" __declspec(dllexport) int CopySubRegion(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
    __int32 X1, __int32 Y1);
extern "C" __declspec(dllexport) int CopyHorizontalLine(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    void *LineBuffer, int LineCount, int LineStart);
extern "C" __declspec(dllexport) int CopyVerticalLine(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    void *ColumnBuffer, int ColumnCount, int ColumnStart);
extern "C" __declspec(dllexport) int CopyCircularBuffer(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride, int X, int Y, int Radius, UINT32 PackedBG);
extern "C" __declspec(dllexport) int ClearBufferRegion(void *Destination, __int32 Width, __int32 Height, __int32 Stride, UINT32 FillColor,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom);
extern "C" __declspec(dllexport) int ClearBuffer2(void *Destination, __int32 Width, __int32 Height, __int32 Stride, UINT32 FillColor);
extern "C" __declspec(dllexport) int CopyRegion(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Destination, __int32 DestinationWidth, __int32 DestinationStride, void *UpperLeft, void *LowerRight);
extern "C" __declspec(dllexport) int PasteRegion(void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *UpperLeft, void *LowerRight);
extern "C" __declspec(dllexport) int PasteRegion2(void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *UpperLeft, void *LowerRight);
extern "C" __declspec(dllexport) int PasteRegion3(void *Destination, __int32 DestWidth, __int32 DestHeight,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, void *UpperLeft, void *LowerRight);
extern "C" __declspec(dllexport) int PasteRegion4(void *Destination, __int32 DestWidth, __int32 DestHeight,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, int X1, int Y1, int X2, int Y2);
extern "C" __declspec(dllexport) int CopyBufferToBuffer(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination);
extern "C" __declspec(dllexport) int ClearBufferRegion2(void *Buffer, __int32 Width, __int32 Height, __int32 Stride, UINT32 FillColor,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom);
extern "C" __declspec(dllexport) int SwapImageBuffers(void *Buffer1, void *Buffer2, __int32 Width, __int32 Height, __int32 Stride);
extern "C" __declspec(dllexport) int ClearBufferDWord(void *Destination, __int32 Width, __int32 Height, UINT32 ClearWith);
extern "C" __declspec(dllexport) int ExtractScanLine(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int X2, int Y);

extern "C" __declspec(dllexport) int CopyBufferRegionPixel(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int Y1, int X2, int Y2);

#endif