#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Solarize the passed image.
/// </summary>
/// <remarks>
/// http://www.cs.umb.edu/~jreyes/csit114-fall-2007/project4/filters.html
/// </remarks>
/// <param name="Source">Pointer to the image buffer that will be solarized.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Threshold">Determines when solarization will occur. Normalized value. Clamped as normalized.</param>
/// <param name="Invert">If TRUE, solarization is inverted with respect to the threshold.</param>
/// <returns>Value indicating operational success.</returns>
int SolarizeImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Threshold, BOOL Invert)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	if (Threshold < 0.0)
		Threshold = 0.0;
	if (Threshold > 1.0)
		Threshold = 1.0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			double PixelLuminance = ColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
			//double PixelLuminance = NormalizedColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0], TRUE);
			double Test = Invert ? 1.0 - Threshold : Threshold;
			Dest[Index + 3] = Src[Index + 3];
			if (PixelLuminance >= Test)
			{
				Dest[Index + 2] = 0xff - Src[Index + 2];
				Dest[Index + 1] = 0xff - Src[Index + 1];
				Dest[Index + 0] = 0xff - Src[Index + 0];
			}
			else
			{
				Dest[Index + 2] = Src[Index + 2];
				Dest[Index + 1] = Src[Index + 1];
				Dest[Index + 0] = Src[Index + 0];
			}
		}
	}

	return Success;
}

/// <summary>
/// Solarize the alpha component of the passed image.
/// </summary>
/// <remarks>
/// http://www.cs.umb.edu/~jreyes/csit114-fall-2007/project4/filters.html
/// </remarks>
/// <param name="Source">Pointer to the image buffer that will be solarized.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Threshold">Determines when solarization will occur. Normalized value. Clamped as normalized.</param>
/// <param name="Invert">If TRUE, solarization is inverted with respect to the threshold.</param>
/// <returns>Value indicating operational success.</returns>
int AlphaSolarizeImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, double Threshold, BOOL Invert)
{
	if (Source == NULL)
		return NullPointer;
	int PixelSize = 4;
	BYTE *Buffer = (BYTE *)Source;
	if (Threshold < 0.0)
		Threshold = 0.0;
	if (Threshold > 1.0)
		Threshold = 1.0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			double PixelLuminance = ColorLuminance2(Buffer[Index + 2], Buffer[Index + 1], Buffer[Index + 0], TRUE);
			double Test = Invert ? 1.0 - Threshold : Threshold;
			if (PixelLuminance >= Test)
			{
				Buffer[Index + 3] = 0xff - Buffer[Index + 3];
			}
		}
	}

	return Success;
}

/// <summary>
/// Sets the alpha channel based on the luminance of a given pixel.
/// </summary>
/// <remarks>
/// http://www.cs.umb.edu/~jreyes/csit114-fall-2007/project4/filters.html
/// </remarks>
/// <param name="Source">Pointer to the image buffer that will be solarized.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Threshold">Determines when solarization will occur. Normalized value. Clamped as normalized.</param>
/// <param name="SolarAlpha">The alpha channel value if the luminance passes the <paramref name="Threshold"/> value.</param>
/// <returns>Value indicating operational success.</returns>
int AlphaSolarizeImage2(void *Source, __int32 Width, __int32 Height, __int32 Stride, double Threshold, BYTE SolarAlpha)
{
	if (Source == NULL)
		return NullPointer;
	int PixelSize = 4;
	BYTE *Buffer = (BYTE *)Source;
	if (Threshold < 0.0)
		Threshold = 0.0;
	if (Threshold > 1.0)
		Threshold = 1.0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			double PixelLuminance = ColorLuminance2(Buffer[Index + 2], Buffer[Index + 1], Buffer[Index + 0], TRUE);
			if (PixelLuminance >= Threshold)
			{
				Buffer[Index + 3] = SolarAlpha;
			}
		}
	}

	return Success;
}

/// <summary>
/// Invert the colors of the image pointed to by <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Pointer to the image to invert.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination of the inversion.</param>
/// <param name="IncludeAlpha">Determines if alpha is inverted. Usually not a good idea but just in case...</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <param name="CopyOutOfRegion">
/// If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
/// the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
/// </param>
/// <param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
/// <returns>Value indicating operational success.</returns>
int InvertImageRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, bool IncludeAlpha,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
	if (Source == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
	BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
	BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
	BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

	int PixelSize = 4;
	BYTE *Buffer = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
			{
				if (IncludeAlpha)
					Dest[Index + 3] = 0xff - Buffer[Index + 3];
				else
					Dest[Index + 3] = 0xff;
				Dest[Index + 2] = 0xff - Buffer[Index + 2];
				Dest[Index + 1] = 0xff - Buffer[Index + 1];
				Dest[Index + 0] = 0xff - Buffer[Index + 0];
			}
			else
			{
				if (CopyOutOfRegion)
				{
					Dest[Index + 3] = Buffer[Index + 3];
					Dest[Index + 2] = Buffer[Index + 2];
					Dest[Index + 1] = Buffer[Index + 1];
					Dest[Index + 0] = Buffer[Index + 0];
				}
				else
				{
					Dest[Index + 3] = NonOpA;
					Dest[Index + 2] = NonOpR;
					Dest[Index + 1] = NonOpG;
					Dest[Index + 0] = NonOpB;
				}
			}
		}
	}

	return Success;
}

/// <summary>
/// Invert the colors of the image pointed to by <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Pointer to the image to invert.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination of the inversion.</param>
/// <param name="IncludeAlpha">Determines if alpha is inverted. Usually not a good idea but just in case...</param>
/// <returns>Value indicating operational success.</returns>
int InvertImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, bool IncludeAlpha)
{
	return InvertImageRegion(Source, Width, Height, Stride, Destination, IncludeAlpha, 0, 0, Width - 1, Height - 1, FALSE, 0x0);
}

/// <summary>
/// Merge four channels into one image. Each channel will be placed in a byte in the final UINT32.
/// </summary>
/// <param name="Destination">Pointer to the destination image buffer.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Channel1">
/// Pointer to the image with channel 1 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
/// into index 0.
/// </param>
/// <param name="Channel2">
/// Pointer to the image with channel 2 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
/// into index 1.
/// </param>
/// <param name="Channel3">
/// Pointer to the image with channel 3 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
/// into index 2.
/// </param>
/// <param name="Channel4">
/// Pointer to the image with channel 4 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
/// into index 3.
/// </param>
/// <returns>Value indicating operational success.</returns>
int CombineChannels32(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
	void *Channel1, void *Channel2, void *Channel3, void *Channel4)
{
	if (Destination == NULL)
		return NullPointer;
	if (Channel1 == NULL)
		return NullPointer;
	if (Channel2 == NULL)
		return NullPointer;
	if (Channel3 == NULL)
		return NullPointer;
	if (Channel4 == NULL)
		return NullPointer;

	BYTE *Dest = (BYTE *)Destination;
	BYTE *Ch1 = (BYTE *)Channel1;
	BYTE *Ch2 = (BYTE *)Channel2;
	BYTE *Ch3 = (BYTE *)Channel3;
	BYTE *Ch4 = (BYTE *)Channel4;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 0] = Ch4[Index + 0];
			Dest[Index + 1] = Ch3[Index + 1];
			Dest[Index + 2] = Ch2[Index + 2];
			Dest[Index + 3] = Ch1[Index + 3];
		}
	}

	return Success;
}

/// <summary>
/// Merge three channels into one image.
/// </summary>
/// <param name="Destination">Pointer to the destination image buffer.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="RedChannel">Pointer to the image with the red channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="GreenChannel">Pointer to the image with the green channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="BlueChannel">Pointer to the image with the blue channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <returns>Value indicating operational success.</returns>
int ChannelMerge(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
	void *RedChannel, void *GreenChannel, void *BlueChannel)
{
#if 0
	BYTE *NotUsed = new BYTE[Height * Stride];
	return CombineChannels32(Destination, Width, Height, Stride, BlueChannel, GreenChannel, RedChannel, NotUsed);
	delete[] NotUsed;
#else
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

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 0] = Blue[Index + 0];
			Dest[Index + 1] = Green[Index + 1];
			Dest[Index + 2] = Red[Index + 2];
			Dest[Index + 3] = 0xff;
		}
	}

	return Success;
#endif
}

/// <summary>
/// Merge four channels (red, green, blue and alpha) into one image.
/// </summary>
/// <param name="Destination">Pointer to the destination image buffer.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="AlphaChannel">Pointer to the image with the alpha channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="RedChannel">Pointer to the image with the red channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="GreenChannel">Pointer to the image with the green channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="BlueChannel">Pointer to the image with the blue channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <returns>Value indicating operational success.</returns>
int ChannelMergeAlpha(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
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

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 0] = Blue[Index + 0];
			Dest[Index + 1] = Green[Index + 1];
			Dest[Index + 2] = Red[Index + 2];
			Dest[Index + 3] = Alpha[Index + 3];
		}
	}

	return Success;
}

/// <summary>
/// Merge three channels into one image. Alpha is set to 0xff.
/// </summary>
/// <param name="Destination">Pointer to the destination image buffer.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="HueChannel">Pointer to the image with the hue channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="SaturationChannel">Pointer to the image with the saturation channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <param name="LuminanceChannel">Pointer to the image with the luminance channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
/// <returns>Value indicating operational success.</returns>
int HSLChannelMerge(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
	void *HueChannel, void *SaturationChannel, void *LuminanceChannel)
{
	if (Destination == NULL)
		return NullPointer;
	if (HueChannel == NULL)
		return NullPointer;
	if (SaturationChannel == NULL)
		return NullPointer;
	if (LuminanceChannel == NULL)
		return NullPointer;

	BYTE *Dest = (BYTE *)Destination;
	BYTE *Hue = (BYTE *)HueChannel;
	BYTE *Saturation = (BYTE *)SaturationChannel;
	BYTE *Luminance = (BYTE *)LuminanceChannel;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 0] = Luminance[Index + 0];
			Dest[Index + 1] = Saturation[Index + 1];
			Dest[Index + 2] = Hue[Index + 2];
			Dest[Index + 3] = 0xff;
		}
	}

	return Success;
}

/// <summary>
/// Split the source image into component ARGB channels.
/// </summary>
/// <param name="Source">Pointer to the image to split.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="AlphaDest">Pointer to the image that will contain the alpha component. If NULL, the alpha component is not split out. Must have same dimensions as <paramref name="Source"/>.</param>
/// <param name="RedDest">Pointer to the image that will contain the red component. Must have same dimensions as <paramref name="Source"/>.</param>
/// <param name="GreenDest">Pointer to the image that will contain the green component. Must have same dimensions as <paramref name="Source"/>.</param>
/// <param name="BlueDest">Pointer to the image that will contain the blue component. Must have same dimensions as <paramref name="Source"/>.</param>
/// <returns>Value indicating operational success.</returns>
int SplitImageIntoChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *AlphaDest, void *RedDest, void *GreenDest, void *BlueDest)
{
	if (Source == NULL)
		return NullPointer;
	if (RedDest == NULL)
		return NullPointer;
	if (GreenDest == NULL)
		return NullPointer;
	if (BlueDest == NULL)
		return NullPointer;
	BOOL HasAlpha = AlphaDest == NULL ? FALSE : TRUE;
	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Red = (BYTE *)RedDest;
	BYTE *Green = (BYTE *)GreenDest;
	BYTE *Blue = (BYTE *)BlueDest;
	BYTE *Alpha = HasAlpha ? (BYTE *)AlphaDest : NULL;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Blue[Index + 0] = Src[Index + 0];
			Blue[Index + 1] = Src[Index + 0];
			Blue[Index + 2] = Src[Index + 0];

			Green[Index + 0] = Src[Index + 1];
			Green[Index + 1] = Src[Index + 1];
			Green[Index + 2] = Src[Index + 1];

			Red[Index + 0] = Src[Index + 2];
			Red[Index + 1] = Src[Index + 2];
			Red[Index + 2] = Src[Index + 2];

			if (HasAlpha)
			{
				Alpha[Index + 0] = Src[Index + 3];
				Alpha[Index + 1] = Src[Index + 3];
				Alpha[Index + 2] = Src[Index + 3];
			}
		}
	}

	return Success;
}

/// <summary>
/// Split the source image into component HSL channels.
/// </summary>
/// <param name="Source">Pointer to the image to split.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="AlphaDest">Pointer to the image that will contain the alpha component. If NULL, the alpha component is not split out. Must have same dimensions as <paramref name="Source"/>.</param>
/// <param name="HueDest">Pointer to the image that will contain the hue component. Must have same dimensions as <paramref name="Source"/>.</param>
/// <param name="SaturationDest">Pointer to the image that will contain the saturation component. Must have same dimensions as <paramref name="Source"/>.</param>
/// <param name="LuminanceDest">Pointer to the image that will contain the luminance component. Must have same dimensions as <paramref name="Source"/>.</param>
/// <returns>Value indicating operational success.</returns>
int SplitImageIntoHSLChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *AlphaDest, void *HueDest, void *SaturationDest, void *LuminanceDest)
{
	if (Source == NULL)
		return NullPointer;
	if (HueDest == NULL)
		return NullPointer;
	if (SaturationDest == NULL)
		return NullPointer;
	if (LuminanceDest == NULL)
		return NullPointer;
	BOOL HasAlpha = AlphaDest == NULL ? FALSE : TRUE;
	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Hue = (BYTE *)HueDest;
	BYTE *Saturation = (BYTE *)SaturationDest;
	BYTE *Luminance = (BYTE *)LuminanceDest;
	BYTE *Alpha = HasAlpha ? (BYTE *)AlphaDest : NULL;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Luminance[Index + 0] = Src[Index + 0];
			Luminance[Index + 1] = Src[Index + 0];
			Luminance[Index + 2] = Src[Index + 0];

			Saturation[Index + 0] = Src[Index + 1];
			Saturation[Index + 1] = Src[Index + 1];
			Saturation[Index + 2] = Src[Index + 1];

			Hue[Index + 0] = Src[Index + 2];
			Hue[Index + 1] = Src[Index + 2];
			Hue[Index + 2] = Src[Index + 2];

			if (HasAlpha)
			{
				Alpha[Index + 0] = Src[Index + 3];
				Alpha[Index + 1] = Src[Index + 3];
				Alpha[Index + 2] = Src[Index + 3];
			}
		}
	}

	return Success;
}

/// <summary>
/// Calculate a running mean for the specified channels.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
/// <param name="AlphaWindowSize">Size of the alpha window. Size truncated for early pixel indices.</param>
/// <param name="RedWindowSize">Size of the red window. Size truncated for early pixel indices.</param>
/// <param name="GreenWindowSize">Size of the green window. Size truncated for early pixel indices.</param>
/// <param name="BlueWindowSize">Size of the blue window. Size truncated for early pixel indices.</param>
/// <returns>Value indicating operational success.</returns>
int RollingMeanChannels2(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, int AlphaWindowSize, int RedWindowSize, int GreenWindowSize, int BlueWindowSize)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (AlphaWindowSize == RedWindowSize == GreenWindowSize == BlueWindowSize == 0)
		return NoActionTaken;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	__int32 MaxIndex = (Width * Stride) * Height;
	BOOL UniformWindowSize = (AlphaWindowSize == RedWindowSize == GreenWindowSize == BlueWindowSize) ? TRUE : FALSE;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			int ASum = 0;
			int RSum = 0;
			int GSum = 0;
			int BSum = 0;

			if (UniformWindowSize)
			{
				int LoopStart = Index - AlphaWindowSize >= 0 ? Index - AlphaWindowSize : 0;
				for (int WIndex = LoopStart; WIndex <= Index; WIndex++)
				{
					ASum += Src[WIndex + 3];
					RSum += Src[WIndex + 2];
					GSum += Src[WIndex + 1];
					BSum += Src[WIndex + 0];
				}
				A = (BYTE)((double)ASum / (double)AlphaWindowSize);
				R = (BYTE)((double)RSum / (double)AlphaWindowSize);
				G = (BYTE)((double)GSum / (double)AlphaWindowSize);
				B = (BYTE)((double)BSum / (double)AlphaWindowSize);
			}
			else
			{
				if (AlphaWindowSize > 0)
				{
					int LoopStart = Index - RedWindowSize >= 0 ? Index - RedWindowSize : 0;
					for (int WIndex = LoopStart; WIndex <= Index; WIndex++)
						RSum += Src[WIndex + 3];
					R = (BYTE)((double)RSum / (double)AlphaWindowSize);
				}
				if (RedWindowSize > 0)
				{
					int LoopStart = Index - RedWindowSize >= 0 ? Index - RedWindowSize : 0;
					for (int WIndex = LoopStart; WIndex <= Index; WIndex++)
						RSum += Src[WIndex + 2];
					R = (BYTE)((double)GSum / (double)RedWindowSize);
				}
				if (GreenWindowSize > 0)
				{
					int LoopStart = Index - GreenWindowSize >= 0 ? Index - GreenWindowSize : 0;
					for (int WIndex = LoopStart; WIndex <= Index; WIndex++)
						GSum += Src[WIndex + 1];
					G = (BYTE)((double)GSum / (double)GreenWindowSize);
				}
				if (BlueWindowSize > 0)
				{
					int LoopStart = Index - BlueWindowSize >= 0 ? Index - BlueWindowSize : 0;
					for (int WIndex = LoopStart; WIndex <= Index; WIndex++)
						BSum += Src[WIndex + 0];
					B = (BYTE)((double)ASum / (double)BlueWindowSize);
				}
			}

			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

/// <summary>
/// Calculate a running mean for all color channels (with the possible exception of the alpha channel).
/// </summary>
/// <remarks>
/// Calls RollingMeanChannels2 with parameters derived from a call to this function.
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
/// <param name="WindowSize">Size of the rolling mean window. Size truncated for early pixels.</param>
/// <param name="IncludeAlpha">
/// Determines if alpha is included in the rolling mean. In general, set this parameter to TRUE if all alpha channel values
/// are 0xff. Doing this provides better performance.
/// </param>
/// <returns>Value indicating operational success.</returns>
int RollingMeanChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, int WindowSize, BOOL IncludeAlpha)
{
	return RollingMeanChannels2(Source, Width, Height, Stride, Destination,
		IncludeAlpha ? WindowSize : 0, WindowSize, WindowSize, WindowSize);
}

/// <summary>
/// Sort the red, green, and blue channels of each pixel according to <paramref name="SortHow"/>. Result return in
/// <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
/// <returns>Value indicating operational success.</returns>
int SortChannels2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL UseLuminance,
	double LuminanceThreshold)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			BYTE Biggest = max(R, max(G, B));
			BYTE Smallest = min(R, min(G, B));
			BYTE Middle = 0;
			if (R == Biggest)
			{
				Middle = max(G, B);
			}
			if (G == Biggest)
			{
				Middle = max(R, B);
			}
			if (B == Biggest)
			{
				Middle = max(R, G);
			}
			Dest[Index + 3] = Src[Index + 3];
			Dest[Index + 2] = Biggest;
			Dest[Index + 1] = Middle;
			Dest[Index + 0] = Smallest;
		}
	}

	return Success;
}

/// <summary>
/// Sort the red, green, and blue channels of each pixel according to <paramref name="SortHow"/>. Result return in
/// <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
/// <param name="SortHow">Determines sort order.</param>
/// <param name="StoreSortHowAsAlpha">If TRUE, the sort order is stored in the destination image as the alpha channel value.</param>
/// <param name="InvertAlpha">If TRUE, the alpha value is inverted.</param>
/// <returns>Value indicating operational success.</returns>
int SortChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, int SortHow, BOOL StoreSortHowAsAlpha, BOOL InvertAlpha)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if ((SortHow < SortRGB) || (SortHow > SortBGR))
		return InvalidOperation;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			BYTE Biggest = max(R, max(G, B));
			BYTE Smallest = min(R, min(G, B));
			BYTE Middle = 0;
			if (R == Biggest)
			{
				Middle = max(G, B);
			}
			if (G == Biggest)
			{
				Middle = max(R, B);
			}
			if (B == Biggest)
			{
				Middle = max(R, G);
			}
			if (StoreSortHowAsAlpha)
				Dest[Index + 3] = SortHow;
			else
				Dest[Index + 3] = Src[Index + 3];
			if (InvertAlpha)
				Dest[Index + 3] = 0xff - Dest[Index + 3];
			switch (SortHow)
			{
			case SortRGB:
				Dest[Index + 2] = Biggest;
				Dest[Index + 1] = Middle;
				Dest[Index + 0] = Smallest;
				break;

			case SortRBG:
				Dest[Index + 2] = Biggest;
				Dest[Index + 0] = Middle;
				Dest[Index + 1] = Smallest;
				break;

			case SortGRB:
				Dest[Index + 1] = Biggest;
				Dest[Index + 2] = Middle;
				Dest[Index + 0] = Smallest;
				break;

			case SortGBR:
				Dest[Index + 1] = Biggest;
				Dest[Index + 0] = Middle;
				Dest[Index + 2] = Smallest;
				break;

			case SortBRG:
				Dest[Index + 0] = Biggest;
				Dest[Index + 2] = Middle;
				Dest[Index + 1] = Smallest;
				break;

			case SortBGR:
				Dest[Index + 0] = Biggest;
				Dest[Index + 1] = Middle;
				Dest[Index + 2] = Smallest;
				break;

			default:
				return InvalidOperation;
			}
		}
	}

	return Success;
}

/// <summary>
/// Create a sepia toned version of <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the sepia image will be drawn.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <param name="CopyOutOfRegion">
/// If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
/// the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
/// </param>
/// <param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
/// <returns>Value indicating operational result.</returns>
int SepiaToneRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
	BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
	BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
	BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
			{
				Dest[Index + 3] = Src[Index + 3];
				Dest[Index + 2] = min(((double)R * 0.393) + ((double)G * 0.769) + ((double)B * 0.189), 255);
				Dest[Index + 1] = min(((double)R * 0.349) + ((double)G * 0.686) + ((double)B * 0.168), 255);
				Dest[Index + 0] = min(((double)R * 0.272) + ((double)G * 0.534) + ((double)B * 0.131), 255);
			}
			else
			{
				if (CopyOutOfRegion)
				{
					Dest[Index + 3] = Src[Index + 3];
					Dest[Index + 2] = Src[Index + 2];
					Dest[Index + 1] = Src[Index + 1];
					Dest[Index + 0] = Src[Index + 0];
				}
				else
				{
					Dest[Index + 3] = NonOpA;
					Dest[Index + 2] = NonOpR;
					Dest[Index + 1] = NonOpG;
					Dest[Index + 0] = NonOpB;
				}
			}
		}
	}
	return Success;
}

/// <summary>
/// Create a sepia toned version of <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the sepia image will be drawn.</param>
/// <returns>Value indicating operational result.</returns>
int SepiaTone(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
#if 0
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];

			Dest[Index + 3] = Src[Index + 3];
			Dest[Index + 2] = min(((double)R * 0.393) + ((double)G * 0.769) + ((double)B * 0.189), 255);
			Dest[Index + 1] = min(((double)R * 0.349) + ((double)G * 0.686) + ((double)B * 0.168), 255);
			Dest[Index + 0] = min(((double)R * 0.272) + ((double)G * 0.534) + ((double)B * 0.131), 255);
		}
	}
	return Success;
#else
	return SepiaToneRegion(Source, Width, Height, Stride, Destination, 0, 0, Width - 1, Height - 1, TRUE, 0x0);
#endif
}

/// <summary>
/// Generate a threshold image based on <paramref name="Source"/> with low, and high regions.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the adjusted image will be drawn.</param>
/// <param name="Threshold">Luminance value that determines which color will be used.</param>
/// <param name="PackedColor">Color used when the luminance is less than <paramref name="Threshold"/>.</param>
/// <param name="InvertThreshold">If TRUE, the threshold usage flag is inverted.</param>
/// <returns>Value indicating operational result.</returns>
int ColorThreshold0(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	double Threshold, UINT32 PackedColor, BOOL InvertThreshold)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	BYTE TColorR = (PackedColor & 0x00ff0000) >> 16;
	BYTE TColorG = (PackedColor & 0x0000ff00) >> 8;
	BYTE TColorB = (PackedColor & 0x000000ff) >> 0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			double PixLuminance = NormalizedColorLuminance(R, G, B, TRUE);
			BOOL ChangeColor = PixLuminance < Threshold;
			if (InvertThreshold)
				ChangeColor = !ChangeColor;
			Dest[Index + 3] = Src[Index + 3];
			if (ChangeColor)
			{
				Dest[Index + 2] = TColorR;
				Dest[Index + 1] = TColorG;
				Dest[Index + 0] = TColorB;
			}
			else
			{
				Dest[Index + 2] = Src[Index + 2];
				Dest[Index + 1] = Src[Index + 1];
				Dest[Index + 0] = Src[Index + 0];
			}
		}
	}

	return Success;
}

/// <summary>
/// Generate a threshold image based on <paramref name="Source"/> with low, and high regions.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the adjusted image will be drawn.</param>
/// <param name="Threshold">Luminance value that determines which color will be used.</param>
/// <param name="PackedLowColor">Color used when the luminance is below (or equal to) <paramref name="Threshold"/>.</param>
/// <param name="PackedHighColor">Color used when the luminance is above (or equal to) <paramref name="Threshold"/>.</param>
/// <returns>Value indicating operational result.</returns>
int ColorThreshold(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	double Threshold, UINT32 PackedLowColor, UINT32 PackedHighColor)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	BYTE LowR = (PackedLowColor & 0x00ff0000) >> 16;
	BYTE LowG = (PackedLowColor & 0x0000ff00) >> 8;
	BYTE LowB = (PackedLowColor & 0x000000ff) >> 0;
	BYTE HighR = (PackedHighColor & 0x00ff0000) >> 16;
	BYTE HighG = (PackedHighColor & 0x0000ff00) >> 8;
	BYTE HighB = (PackedHighColor & 0x000000ff) >> 0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			double PixLuminance = NormalizedColorLuminance(R, G, B, TRUE);
			if (PixLuminance < Threshold)
			{
				Dest[Index + 2] = LowR;
				Dest[Index + 1] = LowG;
				Dest[Index + 0] = LowB;
			}
			else
			{
				Dest[Index + 2] = HighR;
				Dest[Index + 1] = HighG;
				Dest[Index + 0] = HighB;
			}
		}
	}

	return Success;
}

/// <summary>
/// Generate a threshold image based on <paramref name="Source"/> with low, middle, and high regions where the middle region consists
/// of unmodified source data.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the adjusted image will be drawn.</param>
/// <param name="LowThreshold">Luminance value below which <paramref name="PackedLowColor"/> will be used in the destination image.</param>
/// <param name="PackedLowColor">Color used when the luminance is below (or equal to) <paramref name="LowThreshold"/>.</param>
/// <param name="HighThreshold">Luminance value above which <paramref name="PackedHighColor"/> will be used in the destination image.</param>
/// <param name="PackedHighColor">Color used when the luminance is above (or equal to) <paramref name="HighThreshold"/>.</param>
/// <returns>Value indicating operational result.</returns>
int ColorThreshold2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	double LowThreshold, UINT32 PackedLowColor, double HighThreshold, UINT32 PackedHighColor)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	BYTE LowR = (PackedLowColor & 0x00ff0000) >> 16;
	BYTE LowG = (PackedLowColor & 0x0000ff00) >> 8;
	BYTE LowB = (PackedLowColor & 0x000000ff) >> 0;
	BYTE HighR = (PackedHighColor & 0x00ff0000) >> 16;
	BYTE HighG = (PackedHighColor & 0x0000ff00) >> 8;
	BYTE HighB = (PackedHighColor & 0x000000ff) >> 0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			double PixLuminance = NormalizedColorLuminance(R, G, B, TRUE);
			if (PixLuminance <= LowThreshold)
			{
				//Use the low threshold color.
				Dest[Index + 2] = LowR;
				Dest[Index + 1] = LowG;
				Dest[Index + 0] = LowB;
			}
			else
				if (PixLuminance >= HighThreshold)
				{
					//Use the high threshold color.
					Dest[Index + 2] = HighR;
					Dest[Index + 1] = HighG;
					Dest[Index + 0] = HighB;
				}
				else
				{
					//Copy unmodified source data.
					Dest[Index + 2] = R;
					Dest[Index + 1] = G;
					Dest[Index + 0] = B;
				}
		}
	}

	return Success;
}

struct ThresholdListType
{
	double LowThreshold;
	double HighThreshold;
	UINT32 PackedColor;
};

struct ThresholdNode
{
	BOOL IsValid;
	double LowThreshold;
	double HighThreshold;
	BYTE R;
	BYTE G;
	BYTE B;
};

/// <summary>
/// Change colors in the image depending on the original pixel's luminance.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the final image will be drawn.</param>
/// <param name="ThresholdList">Pointer to a list of thresholds that determine which colors are drawn for which luminances.</param>
/// <param name="ListCount">Number of items in the threshold list.</param>
/// <returns>Value indicating operational result.</returns>
int ColorThreshold3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, void *ThresholdList, int ListCount)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (ThresholdList == NULL)
		return NullPointer;
	if (ListCount < 1)
		return InvalidOperation;

	ThresholdListType *RawNodes = (ThresholdListType *)ThresholdList;
	ThresholdNode *TNodes = new ThresholdNode[ListCount];
	for (int i = 0; i < ListCount; i++)
	{
		if (RawNodes[i].LowThreshold >= RawNodes[i].HighThreshold)
		{
			TNodes[i].IsValid = FALSE;
			continue;
		}
		TNodes[i].IsValid = TRUE;
		TNodes[i].LowThreshold = RawNodes[i].LowThreshold;
		TNodes[i].HighThreshold = RawNodes[i].HighThreshold;
		TNodes[i].R = (RawNodes[i].PackedColor & 0x00ff0000) >> 16;
		TNodes[i].G = (RawNodes[i].PackedColor & 0x0000ff00) >> 8;
		TNodes[i].B = (RawNodes[i].PackedColor & 0x000000ff) >> 0;
	}

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			double PixLuminance = NormalizedColorLuminance(R, G, B, TRUE);
			int ColorIndex = -1;
			for (int NodeIndex = 0; NodeIndex < ListCount; NodeIndex++)
			{
				if (!TNodes[NodeIndex].IsValid)
					continue;
				if ((PixLuminance >= TNodes[NodeIndex].LowThreshold) && (PixLuminance <= TNodes[NodeIndex].HighThreshold))
				{
					ColorIndex = NodeIndex;
					break;
				}
			}
			if (ColorIndex < 0)
			{
				Dest[Index + 2] = Src[Index + 2];
				Dest[Index + 1] = Src[Index + 1];
				Dest[Index + 0] = Src[Index + 0];
			}
			else
			{
				Dest[Index + 2] = TNodes[ColorIndex].R;
				Dest[Index + 1] = TNodes[ColorIndex].G;
				Dest[Index + 0] = TNodes[ColorIndex].B;
			}
		}
	}

	delete[] TNodes;
	return Success;
}

/// <summary>
/// Auto adjust the saturation of <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the saturation adjusted image will be drawn.</param>
/// <param name="Saturation">Saturation value.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <param name="CopyOutOfRegion">
/// If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
/// the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
/// </param>
/// <param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
/// <returns>Value indicating operational result.</returns>
int AutoSaturateRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, __int32 Saturation,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
	BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
	BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
	BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
			{
				BYTE R = Src[Index + 2];
				BYTE G = Src[Index + 1];
				BYTE B = Src[Index + 0];
				double Y = (0.3 * R) + (0.59 * G) + (0.11 * B);

				__int32 RY1 = ((int)(70 * (int)R - 59 * (int)G - 11 * (int)B) / 100);
				__int32 GY1 = ((int)(-30 * (int)R + 41 * (int)G - 11 * (int)B) / 100);
				__int32 BY1 = ((int)(-30 * (int)R - 59 * (int)G + 89 * (int)B) / 100);

				__int32 RY = (RY1 * Saturation) / 100;
				__int32 GY = (GY1 * Saturation) / 100;
				__int32 BY = (BY1 * Saturation) / 100;

				R = min(max(RY + Y, 255), 0);
				G = min(max(GY + Y, 255), 0);
				B = min(max(BY + Y, 255), 0);

				Dest[Index + 3] = Src[Index + 3];
				Dest[Index + 2] = R;
				Dest[Index + 1] = G;
				Dest[Index + 0] = B;
			}
			else
			{
				if (CopyOutOfRegion)
				{
					Dest[Index + 3] = Src[Index + 3];
					Dest[Index + 2] = Src[Index + 2];
					Dest[Index + 1] = Src[Index + 1];
					Dest[Index + 0] = Src[Index + 0];
				}
				else
				{
					Dest[Index + 3] = NonOpA;
					Dest[Index + 2] = NonOpR;
					Dest[Index + 1] = NonOpG;
					Dest[Index + 0] = NonOpB;
				}
			}
		}
	}

	return Success;
}

/// <summary>
/// Auto adjust the saturation of <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the saturation adjusted image will be drawn.</param>
/// <param name="Saturation">Saturation value.</param>
/// <returns>Value indicating operational result.</returns>
int AutoSaturate(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, __int32 Saturation)
{
	return AutoSaturateRegion(Source, Width, Height, Stride, Destination, Saturation, 0, 0, Width - 1, Height - 1, TRUE, 0x0);
}

/// <summary>
/// Auto adjust the contrast of <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the adjusted image will be drawn.</param>
/// <param name="Contrast">Contrast value.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <param name="CopyOutOfRegion">
/// If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
/// the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
/// </param>
/// <param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
/// <returns>Value indicating operational result.</returns>
int AutoContrastRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Contrast,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
	BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
	BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
	BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	BYTE ContrastTable[256];
	double working_contast = (100.0 + Contrast) / 100.0;
	working_contast *= working_contast;
	double working_value = 0.0;

	for (int index = 0; index < 256; index++)
	{
		working_value = (double)index;
		working_value /= 255.0;
		working_value -= 0.5;
		working_value *= working_contast;
		working_value += 0.5;
		working_value *= 255;
		if (working_value < 0)
			working_value = 0;
		if (working_value > 255)
			working_value = 255;
		ContrastTable[index] = (BYTE)working_value;
	}

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
			{
				Dest[Index + 3] = Src[Index + 3];
				Dest[Index + 2] = ContrastTable[Src[Index + 2]];
				Dest[Index + 1] = ContrastTable[Src[Index + 1]];
				Dest[Index + 0] = ContrastTable[Src[Index + 0]];
			}
			else
			{
				if (CopyOutOfRegion)
				{
					Dest[Index + 3] = Src[Index + 3];
					Dest[Index + 2] = Src[Index + 2];
					Dest[Index + 1] = Src[Index + 1];
					Dest[Index + 0] = Src[Index + 0];
				}
				else
				{
					Dest[Index + 3] = NonOpA;
					Dest[Index + 2] = NonOpR;
					Dest[Index + 1] = NonOpG;
					Dest[Index + 0] = NonOpB;
				}
			}
		}
	}

	return Success;
}

/// <summary>
/// Auto adjust the contrast of <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the adjusted image will be drawn.</param>
/// <param name="Contrast">Contrast value.</param>
/// <returns>Value indicating operational result.</returns>
int AutoContrast(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Contrast)
{
	return AutoContrastRegion(Source, Width, Height, Stride, Destination, Contrast, 0, 0, Width - 1, Height - 1, FALSE, 0x0);
}

/// <summary>
/// Return a brightness (luminance) image derived from <paramref name="Source"/> for the given region.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the brightness will be drawn.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <param name="CopyOutOfRegion">
/// If True, non-operational pixels will be copied to the destination. Otherwise, pixels of the color <paramref name="PackedOut"/>
/// will be copied.
/// </param>
/// <returns>Value indicating operational result.</returns>
int BrightnessMapRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
	BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
	BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
	BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
			{
				double PixLuminance = GetPixelLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
				//                double PixLuminance = ColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
				BYTE FinalLuminance = (BYTE)(255.0 * PixLuminance);
				Dest[Index + 3] = Src[Index + 3];
				Dest[Index + 2] = FinalLuminance;
				Dest[Index + 1] = FinalLuminance;
				Dest[Index + 0] = FinalLuminance;
			}
			else
			{
				if (CopyOutOfRegion)
				{
					Dest[Index + 3] = Src[Index + 3];
					Dest[Index + 2] = Src[Index + 2];
					Dest[Index + 1] = Src[Index + 1];
					Dest[Index + 0] = Src[Index + 0];
				}
				else
				{
					Dest[Index + 3] = NonOpA;
					Dest[Index + 2] = NonOpR;
					Dest[Index + 1] = NonOpG;
					Dest[Index + 0] = NonOpB;
				}
			}
		}
	}

	return Success;
}

/// <summary>
/// Return a brightness (luminance) image derived from <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the brightness will be drawn.</param>
/// <returns>Value indicating operational result.</returns>
int BrightnessMap(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	return BrightnessMapRegion(Source, Width, Height, Stride, Destination, 0, 0, Width - 1, Height - 1, FALSE, 0x0);
}

/// <summary>
/// Return the mean color for the image pointed to by <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Pointer to the image whose mean color will be returned.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="PackedMeanColor">On success, will contain the image's mean color in packed format.</param>
/// <returns>Value indicating operational success.</returns>
int ImageMeanColorRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, UINT32* PackedMeanColor,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
	if (Source == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	double AlphaSum = 0;
	double RedSum = 0;
	double GreenSum = 0;
	double BlueSum = 0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			AlphaSum += Src[Index + 3];
			RedSum += Src[Index + 2];
			GreenSum += Src[Index + 1];
			BlueSum += Src[Index + 0];
		}
	}

	double TotalPixels = Width * Height * Stride;
	BYTE A = (BYTE)min(((AlphaSum / TotalPixels) * 100.0), 255);
	BYTE R = (BYTE)min(((RedSum / TotalPixels) * 100.0), 255);
	BYTE G = (BYTE)min(((GreenSum / TotalPixels) * 100.0), 255);
	BYTE B = (BYTE)min(((BlueSum / TotalPixels) * 100.0), 255);

	*PackedMeanColor = (A << 24) | (R << 16) | (G << 8) | B;

	return Success;
}

/// <summary>
/// Return the mean color for the image pointed to by <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Pointer to the image whose mean color will be returned.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="PackedMeanColor">On success, will contain the image's mean color in packed format.</param>
/// <returns>Value indicating operational success.</returns>
int ImageMeanColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, UINT32* PackedMeanColor)
{
	return ImageMeanColorRegion(Source, Width, Height, Stride, PackedMeanColor, 0, 0, Width - 1, Height - 1);
}

/// <summary>
/// Return an image with the mean color derived from <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the mean color will be drawn.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <param name="CopyOutOfRegion">
/// If True, non-operational pixels will be copied to the destination. Otherwise, pixels of the color <paramref name="PackedOut"/>
/// will be copied.
/// </param>
/// <returns>Value indicating operational results.</returns>
int MeanImageColorRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (Left < 0)
		return InvalidOperation;
	if (Right >= Width)
		return InvalidOperation;
	if (Top < 0)
		return InvalidOperation;
	if (Bottom >= Height)
		return InvalidOperation;

	BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
	BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
	BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
	BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	UINT32 PackedMean = 0x0;
	int Result = ImageMeanColor(Source, Width, Height, Stride, &PackedMean);
	BYTE mA = (PackedMean & 0xff000000) >> 24;
	BYTE mR = (PackedMean & 0x00ff0000) >> 16;
	BYTE mG = (PackedMean & 0x0000ff00) >> 8;
	BYTE mB = (PackedMean & 0x000000ff) >> 0;

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 0];
			if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
			{
				Dest[Index + 3] = A;
				Dest[Index + 2] = mR;
				Dest[Index + 1] = mG;
				Dest[Index + 0] = mB;
			}
			else
			{
				if (CopyOutOfRegion)
				{
					Dest[Index + 3] = Src[Index + 3];
					Dest[Index + 2] = Src[Index + 2];
					Dest[Index + 1] = Src[Index + 1];
					Dest[Index + 0] = Src[Index + 0];
				}
				else
				{
					Dest[Index + 3] = NonOpA;
					Dest[Index + 2] = NonOpR;
					Dest[Index + 1] = NonOpG;
					Dest[Index + 0] = NonOpB;
				}
			}
		}
	}

	return Success;
}

/// <summary>
/// Return an image with the mean color derived from <paramref name="Source"/>.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the mean color will be drawn.</param>
/// <returns>Value indicating operational result.</returns>
int MeanImageColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	return MeanImageColorRegion(Source, Width, Height, Stride, Destination, 0, 0, Width - 1, Height - 1, TRUE, 0x0);
}

/// <summary>
/// Given an image in <param name="Source"/>, return the calculated mean color.
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <returns>The calculated mean color.</returns>
UINT32 MeanImageColorValue(void *Source, __int32 Width, __int32 Height, __int32 Stride)
{
	if (Source == NULL)
		return NullPointer;
	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;

	double A_Accumulator = 0.0;
	double R_Accumulator = 0.0;
	double G_Accumulator = 0.0;
	double B_Accumulator = 0.0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			A_Accumulator += (double)Src[Index + 3];
			R_Accumulator += (double)Src[Index + 2];
			G_Accumulator += (double)Src[Index + 1];
			B_Accumulator += (double)Src[Index + 0];
		}
	}

	int Count = Width * Height;
	A_Accumulator = A_Accumulator / (double)Count;
	R_Accumulator = R_Accumulator / (double)Count;
	G_Accumulator = G_Accumulator / (double)Count;
	B_Accumulator = B_Accumulator / (double)Count;
	BYTE A = (BYTE)A_Accumulator;
	BYTE R = (BYTE)R_Accumulator;
	BYTE G = (BYTE)G_Accumulator;
	BYTE B = (BYTE)B_Accumulator;
	UINT32 Final = (A << 24) | (R << 16) | (G << 8) | (B << 0);
	return Final;
}

/// <summary>
/// Given an image in <param name="Source"/>, fill <param name="Destination"/> with the calculated mean color.
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="IgnoreAlpha">If true, alpha is set to 0xff in the resultant image regardless of the actual mean value.</param>
/// <param name="MeanColor">Mean calculated color</param>
/// <returns>Operational results.</returns>
int MeanImageColor2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL IgnoreAlpha,
	UINT32 *MeanColor)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	UINT32 CalculatedMeanColor = MeanImageColorValue(Source, Width, Height, Stride);
	BYTE A = IgnoreAlpha ? 0xff : (BYTE)((CalculatedMeanColor & 0xff000000) >> 24);
	BYTE R = (BYTE)((CalculatedMeanColor & 0x00ff0000) >> 16);
	BYTE G = (BYTE)((CalculatedMeanColor & 0x0000ff00) >> 8);
	BYTE B = (BYTE)((CalculatedMeanColor & 0x000000ff) >> 0);
	*MeanColor = CalculatedMeanColor;

	int PixelSize = 4;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

