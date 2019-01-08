#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _STEGANOGRAPHY_H
#define _STEGANOGRAPHY_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int RandomizeImageBitsRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    BYTE RandomizeMask, UINT32 RandomSeed, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue);
extern "C" __declspec(dllexport) int RandomizeImageBits1(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE RandomizeMask, UINT32 RandomSeed, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue);
extern "C" __declspec(dllexport) int RandomizeImageBits2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE RandomizeMask, UINT32 RandomSeed);

extern "C" __declspec(dllexport) int AddDataToRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    void *DataSource, int DataSourceLength);
extern "C" __declspec(dllexport) int AddData1(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *DataSource, int DataSourceLength);
extern "C" __declspec(dllexport) int AddData2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *DataSource, int DataSourceLength);
extern "C" __declspec(dllexport) int AddData3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, char *DataSource, int DataSourceLength);

extern "C" __declspec(dllexport) int AddStringToRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength);
extern "C" __declspec(dllexport) int AddString1(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength);
extern "C" __declspec(dllexport) int AddString2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength);
extern "C" __declspec(dllexport) int AddString3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, char *Text, int TextLength);

extern "C" __declspec(dllexport) int BytesRequiredToFit(BYTE ChannelMask, int TextLength);
extern "C" __declspec(dllexport) BOOL CanFit(__int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    BYTE ChannelMask, int TextLength);

extern "C" __declspec(dllexport) int DataMerge(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    void *DataBuffer, UINT32 DataCount, BOOL ByTwo, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue);

#endif