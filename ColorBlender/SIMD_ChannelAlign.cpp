#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int AlignChannels(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *RedChannel, void *GreenChannel, void *BlueChannel)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (RedChannel == NULL)
		return NullPointer;
	if (GreenChannel == NULL)
		return NullPointer;
	if (BlueChannel == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)SourceBuffer;
	BYTE *Red = (BYTE *)RedChannel;
	BYTE *Green = (BYTE *)GreenChannel;
	BYTE *Blue = (BYTE *)BlueChannel;

	int PixelSize = 4;
	UINT32 ChannelIndex = 0;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			Red[ChannelIndex] = R;
			Green[ChannelIndex] = G;
			Blue[ChannelIndex] = B;
			ChannelIndex++;
		}
	}

	return Success;
}

int AlignChannels2(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *AlphaChannel, void *RedChannel, void *GreenChannel, void *BlueChannel)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (AlphaChannel == NULL)
		return NullPointer;
	if (RedChannel == NULL)
		return NullPointer;
	if (GreenChannel == NULL)
		return NullPointer;
	if (BlueChannel == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)SourceBuffer;
	BYTE *Alpha = (BYTE *)AlphaChannel;
	BYTE *Red = (BYTE *)RedChannel;
	BYTE *Green = (BYTE *)GreenChannel;
	BYTE *Blue = (BYTE *)BlueChannel;

	int PixelSize = 4;
	UINT32 ChannelIndex = 0;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			Alpha[ChannelIndex] = A;
			Red[ChannelIndex] = R;
			Green[ChannelIndex] = G;
			Blue[ChannelIndex] = B;
			ChannelIndex++;
		}
	}

	return Success;
}

int AssembleFromChannels(void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	void *RedChannel, void *GreenChannel, void *BlueChannel)
{
	if (Destination == NULL)
		return NullPointer;
	if (RedChannel == NULL)
		return NullPointer;
	if (GreenChannel == NULL)
		return NullPointer;
	if (BlueChannel == NULL)
		return NullPointer;

	BYTE *Dest = (BYTE *)Destination;
	BYTE *Red = (BYTE *)RedChannel;
	BYTE *Green = (BYTE *)GreenChannel;
	BYTE *Blue = (BYTE *)BlueChannel;

	int PixelSize = 4;
	UINT32 ChannelIndex = 0;

	for (int Row = 0; Row < DestinationHeight; Row++)
	{
		int RowOffset = Row * DestinationStride;
		for (int Column = 0; Column < DestinationWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = 0xff;
			Dest[Index + 2] = Red[ChannelIndex];
			Dest[Index + 1] = Green[ChannelIndex];
			Dest[Index + 0] = Blue[ChannelIndex];
			ChannelIndex++;
		}
	}

	return Success;
}

int AssembleFromChannels2(void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	void *AlphaChannel, void *RedChannel, void *GreenChannel, void *BlueChannel)
{
	if (Destination == NULL)
		return NullPointer;
	if (AlphaChannel == NULL)
		return NullPointer;
	if (RedChannel == NULL)
		return NullPointer;
	if (GreenChannel == NULL)
		return NullPointer;
	if (BlueChannel == NULL)
		return NullPointer;

	BYTE *Dest = (BYTE *)Destination;
	BYTE *Alpha = (BYTE *)AlphaChannel;
	BYTE *Red = (BYTE *)RedChannel;
	BYTE *Green = (BYTE *)GreenChannel;
	BYTE *Blue = (BYTE *)BlueChannel;

	int PixelSize = 4;
	UINT32 ChannelIndex = 0;

	for (int Row = 0; Row < DestinationHeight; Row++)
	{
		int RowOffset = Row * DestinationStride;
		for (int Column = 0; Column < DestinationWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = Alpha[ChannelIndex];
			Dest[Index + 2] = Red[ChannelIndex];
			Dest[Index + 1] = Green[ChannelIndex];
			Dest[Index + 0] = Blue[ChannelIndex];
			ChannelIndex++;
		}
	}

	return Success;
}

const int RGBOrder = 0;
const int RBGOrder = 1;
const int GRBOrder = 2;
const int GBROrder = 3;
const int BRGOrder = 4;
const int BGROrder = 5;

int LinearizeChannelsIntoImage(void *RedChannel, void *GreenChannel, void *BlueChannel, __int32 ChannelSize,
	void *Destination, __int32 Width, __int32 Height, __int32 Stride, __int32 ChannelOrder)
{
	if (RedChannel == NULL)
		return NullPointer;
	if (GreenChannel == NULL)
		return NullPointer;
	if (BlueChannel == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Dest = (BYTE *)Destination;
	BYTE *Red = (BYTE *)RedChannel;
	BYTE *Green = (BYTE *)GreenChannel;
	BYTE *Blue = (BYTE *)BlueChannel;
	__int32 TotalCount = ChannelSize * 3;
	BYTE* AllChannels = (BYTE *)new BYTE[TotalCount];
	__int32 ChannelIndex = 0;
	for (int i = 0; i < ChannelSize; i++)
		AllChannels[ChannelIndex++] = Red[i];
	for (int i = 0; i < ChannelSize; i++)
		AllChannels[ChannelIndex++] = Green[i];
	for (int i = 0; i < ChannelSize; i++)
		AllChannels[ChannelIndex++] = Blue[i];
	ChannelIndex = 0;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = 0xff;
			Dest[Index + 2] = AllChannels[ChannelIndex++];
			Dest[Index + 1] = AllChannels[ChannelIndex++];
			Dest[Index + 0] = AllChannels[ChannelIndex++];
		}
	}

	delete[] AllChannels;

	return Success;
}