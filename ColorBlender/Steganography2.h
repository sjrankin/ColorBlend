#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _STEGANOGRAPHY2_H
#define _STEGANOGRAPHY2_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int SteganographyAddStream(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    BYTE *StreamSource, __int32 StreamLength, __int32 DataOffset,
    BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset, __int32 DataType);

extern "C" __declspec(dllexport) int SteganographyAddConstant(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *FinalImage, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    BYTE Constant,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset);

extern "C" __declspec(dllexport) int SteganographyAddString(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    char *Message, __int32 MessageLength,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset);

extern "C" __declspec(dllexport) int SteganographyFastAddString(void *SourceImage, __int32 SourceWidth, __int32 SourceHeader, __int32 SourceStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    char *Message, __int32 MessageLength);

extern "C" __declspec(dllexport) int SteganographyAddImage(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset);

extern "C" __declspec(dllexport) int SteganographyFastAddImage(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride);

extern "C" __declspec(dllexport) int SteganographyGetConstant(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset, BYTE *Constant);

extern "C" __declspec(dllexport) int SteganographyPresent(void *Source, __int32 HeaderOffset);

extern "C" __declspec(dllexport) int SearchForSteganographicHeader(void *Source, __int32 *HeaderLocation);

extern "C" __declspec(dllexport) int ExtractSteganographicHeader(void *Source, __int32 Offset, void *Header);

extern "C" __declspec(dllexport) int GetSteganographicType(void *Source, __int32 Offset);

#endif