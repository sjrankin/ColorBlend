#include <intrin.h>
#include "ColorBlender.h"
#include "Structures.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Convert the image in <paramref name="SourceBuffer"/> from RGB to HSL. HSL data are returned in
/// <paramref name="DoubleBuffer"/>.
/// </summary>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DoubleBuffer">
/// Buffer of doubles, three doubles (in HSL order) for each RGB pixel in <paramref name="SourceBuffer"/>.
/// </param>
/// <returns>Value indicating operational results.</returns>
int GetHSLImage(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DoubleBuffer)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DoubleBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	double *Destination = (double *)DoubleBuffer;
	int PixelSize = 4;
	int DoubleIndex = 0;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			Destination[DoubleIndex++] = H;
			Destination[DoubleIndex++] = S;
			Destination[DoubleIndex++] = L;
		}
	}

	return Success;
}

/// <summary>
/// Convert a buffer of double values (representing HSL colors) to a buffer of RGB values.
/// </summary>
/// <param name="HSLBuffer">Buffer of HSL double values, one double each for H, S, and L.</param>
/// <param name="DoubleCount">Number of doubles in <paramref name="HSLBuffer"/>.</param>
/// <param name="Destination">Where the RGB data will be placed.</param>
/// <param name="DestinationWidth">Width of the RGB image.</param>
/// <param name="DestinationHeight">Height of the RGB image.</param>
/// <param name="DestinationStride">Stride of the RGB image.</param>
/// <returns>Value indicating operational results.</returns>
int MakeRGBFromHSL(void *HSLBuffer, UINT32 DoubleCount, void *Destination, __int32 DestinationWidth,
	__int32 DestinationHeight, __int32 DestinationStride)
{
	if (HSLBuffer == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Dest = (BYTE *)Destination;
	double *HSL = (double *)HSLBuffer;

	int PixelSize = 4;
	int DoubleIndex = 0;

	for (int Row = 0; Row < DestinationHeight; Row++)
	{
		int RowOffset = Row * DestinationStride;
		for (int Column = 0; Column < DestinationWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			double H = HSL[DoubleIndex++];
			double S = HSL[DoubleIndex++];
			double L = HSL[DoubleIndex++];
			BYTE R = 0;
			BYTE G = 0;
			BYTE B = 0;
			HSLtoRGB8(H, S, L, &R, &G, &B);
			Dest[Index + 3] = 0xff;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

const int HSL_Hue = 0;
const int HSL_Saturation = 1;
const int HSL_Luminance = 2;

/// <summary>
/// Adust the HSL values of the image by the specified multipliers.
/// </summary>
/// <param name="SurfaceBuffer">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
/// <param name="HMultiplier">Value to multiply the hue by.</param>
/// <param name="SMultiplier">Value to multiply the saturation by.</param>
/// <param name="LMultiplier">Value to multiply the luminance by.</param>
/// <returns>Value indicating operational results.</returns>
int AdjustImageHSLValues(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, double HMultipler, double SMultiplier, double LMultiplier)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;
	__int32 PixelSize = 4;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			H = H * HMultipler;
			if (H < 0)
				H = H + 360.0;
			if (H > 360)
				H = H - 360;
			S = S * SMultiplier;
			if (S < 0.0)
				S = 0.0;
			if (S > 1.0)
				S = 1.0;
			L = L * LMultiplier;
			if (L < 0.0)
				L = 0.0;
			if (L > 1.0)
				L = 1.0;
			HSLtoRGB8(H, S, L, &R, &G, &B);
			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}
	return Success;
}

/// <summary>
/// Shift the hue of each pixel in the image.
/// </summary>
/// <param name="SurfaceBuffer">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
/// <param name="HueShiftValue">How much to shift the hue by.</param>
/// <returns>Value indicating operational results.</returns>
int ImageHueShift(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int HueShiftValue)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;
	__int32 PixelSize = 4;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			ShiftHue(&R, &G, &B, HueShiftValue);
			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}

	return Success;
}

/// <summary>
/// Restict a value to a range.
/// </summary>
/// <param name="SourceValue">
/// The value to restrict. If the value is greater than <paramref name="MaxRange"/>, the value is
/// changed to <paramref name="MaxRange"/>.
/// </param>
/// <param name="MaxRange">The maximum value in the range.</param>
/// <param name="Segments">Number of segments to split <paramref name="MaxRange"/> into.</param>
/// <returns>Restricted value.</returns>.
double DoRestrict(double SourceValue, double MaxRange, int Segments)
{
	if (SourceValue > MaxRange)
		SourceValue = MaxRange;
	double SegmentSize = MaxRange / (double)Segments;
	if (SegmentSize == 0.0)
		return SourceValue;
	int SegmentIndex = (int)(SourceValue / SegmentSize);
	double Result = SegmentSize * (double)SegmentIndex;
	return Result;
}

/// <summary>
/// Restrict various HSL channel values to certain ranges.
/// </summary>
/// <param name="SurfaceBuffer">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
/// <param name="HueRangeSize">Determines how to restrict hue values. Should be an even divisor of 360.0.</param>
/// <param name="SaturationRangeSize">Determines how to restrice saturation values.</param>
/// <param name="LuminanceRangeSize">Determines how to restrice luminance values.</param>
/// <param name="RestrictHue">Determines if hues are restricted.</param>
/// <param name="RestrictSaturation">Determines if saturations are restricted.</param>
/// <param name="RestrictLuminance">Determines if luminances are restricted.</param>
/// <returns>Value indicating operational success.</returns>
int RestrictHSL(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int HueRangeSize, int SaturationRangeSize, int LuminanceRangeSize,
	BOOL RestrictHue, BOOL RestrictSaturation, BOOL RestrictLuminance)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;
	__int32 PixelSize = 4;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);

			if (RestrictHue)
			{
				H = DoRestrict(H, 360.0, HueRangeSize);
			}
			if (RestrictSaturation)
			{
				S = DoRestrict(S, 1.0, SaturationRangeSize);
			}
			if (RestrictLuminance)
			{
				L = DoRestrict(L, 1.0, LuminanceRangeSize);
			}

			HSLtoRGB8(H, S, L, &R, &G, &B);
			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}

	return Success;
}

/// <summary>
/// Swap saturation and luminance values for each pixel.
/// </summary>
/// <param name="SurfaceBuffer">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
/// <returns>Value indicating operational results.</returns>
int Silly_SwapSaturationLuminance(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;
	__int32 PixelSize = 4;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			double temp = S;
			S = L;
			L = temp;
			HSLtoRGB8(H, S, L, &R, &G, &B);
			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}
	return Success;
}

/// <summary>
/// Round <paramref name="ToRound"/> to the nearest multiple of <paramref name="Multiple"/>.
/// </summary>
/// <param name="ToRound">The value to round.</param>
/// <param name="Multiple">Determines how to round off <paramref name="ToRound"/>.</param>
/// <returns>Rounded value.</returns>
double RoundToClosest(double ToRound, double Multiple)
{
	if (Multiple == 0.0)
		return ToRound;
	double F1 = ToRound / Multiple;
	int I1 = (int)F1;
	int I2 = I1 * (int)Multiple;
	double R1 = (double)fmod(ToRound, Multiple);
	BOOL RoundUp = R1 > (Multiple / 2.0) ? TRUE : FALSE;
	return (double)I2 + (RoundUp ? Multiple : 0.0);
}

/// <summary>
/// Swap saturation and luminance values for each pixel.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
/// <param name="HueRanges">Number of hue ranges to reduce the hue channel to.</param>
/// <param name="ReduceSaturation">Determines if saturation is reduced to a single value.</param>
/// <param name="SaturationValue">New saturation value if saturation is reduced.</param>
/// <param name="ReduceLuminance">Determines if luminance is reduced to a single value.</param>
/// <param name="LuminanceValue">New luminance value if luminance is reduced.</param>
/// <returns>Value indicating operational results.</returns>
int HSLColorReduction(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, int HueRanges, BOOL ReduceSaturation, double SaturationValue,
	BOOL ReduceLuminance, double LuminanceValue)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (HueRanges < 0)
		return InvalidOperation;

	if (HueRanges < 0)
		HueRanges = 0;
	if (HueRanges > 360)
		HueRanges = 360;
	if (SaturationValue < 0.0)
		SaturationValue = 0.0;
	if (SaturationValue > 1.0)
		SaturationValue = 1.0;
	if (LuminanceValue < 0.0)
		LuminanceValue = 0.0;
	if (LuminanceValue > 1.0)
		LuminanceValue = 1.0;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	__int32 PixelSize = 4;
	//double HueMultiples = 360.0 / (double)HueRanges;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			H = RoundToClosest(H, HueRanges);
			//			H = RoundToClosest(H, HueMultiples);
			if (ReduceSaturation)
				S = SaturationValue;
			if (ReduceLuminance)
				L = LuminanceValue;
			HSLtoRGB8(H, S, L, &R, &G, &B);
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

/// <summary>
/// Set all HSL values as specified.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
/// <param name="SetHue">Determines if the hue is changed.</param>
/// <param name="NewHue">New hue value.</param>
/// <param name="SetSaturation">Determines if the saturation is changed.</param>
/// <param name="NewSaturation">New saturation value.</param>
/// <param name="SetLuminance">Determines if the luminance is changed.</param>
/// <param name="NewLuminance">New luminance value.</param>
/// <returns>Value indicating operational results.</returns>
int HSLBulkSet(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, BOOL SetHue, double NewHue, BOOL SetSaturation, double NewSaturation,
	BOOL SetLuminance, double NewLuminance)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	/*
	if (!SetHue && !SetSaturation && !SetLuminance)
		return NoAction;
		*/

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			if (SetHue)
				H = NewHue;
			if (SetSaturation)
				S = NewSaturation;
			if (SetLuminance)
				L = NewLuminance;
			HSLtoRGB8(H, S, L, &R, &G, &B);
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

const int HSLMultiple = 0;
const int HSLAdd = 1;
const int HSLDivide = 2;
const int HSLSubtract = 3;
const int HSLReplace = 4;

/// <summary>
/// Instructions on how to conditionally change colors in HSL mode.
/// </summary>
struct ConditionalHSLAdjustment
{
	/// <summary>
	/// Determines the hue low range for conditional changes.
	/// </summary>
	double RangeLow;
	/// <summary>
	/// Determines the hue high range for conditional changes.
	/// </summary>
	double RangeHigh;
	/// <summary>
	/// Determines if the hue is modified.
	/// </summary>
	BOOL ModifyHue;
	/// <summary>
	/// Operand to apply to the source hue value.
	/// </summary>
	double HueOperand;
	/// <summary>
	/// Operation to apply the operand to the hue.
	/// </summary>
	int HueOperation;
	/// <summary>
	/// Determines if the saturation is modified.
	/// </summary>
	BOOL ModifySaturation;
	/// <summary>
	/// Operand to apply to the source saturation value.
	/// </summary>
	double SaturationOperand;
	/// <summary>
	/// Operation to apply the operand to the saturation.
	/// </summary>
	int SaturationOperation;
	/// <summary>
	/// Determines if the luminance is modified.
	/// </summary>
	BOOL ModifyLuminance;
	/// <summary>
	/// Operand to apply to the source luminance value.
	/// </summary>
	double LuminanceOperand;
	/// <summary>
	/// Operation to apply the operand to the luminance.
	/// </summary>
	int LuminanceOperation;
};

/// <summary>
/// Modify an HSL value in <paramref name="Source"/> according to <paramref name="Operator"/> and
/// <paramref name="Operand"/>.
/// </summary>
/// <param name="Source">Value to modify.</param>
/// <param name="Operand">Operand used to modify <paramref name="Source"/>.</param>
/// <param name="Operator">How to apply <paramref name="Operand"/> to <paramref name="Source"/>.</param>
/// <param name="ClampLow">Low clamping value.</param>
/// <param name="ClampHigh">High clamping value.</param>
/// <returns>Modified value.</returns>
double ModifyHSLValue(double Source, double Operand, int Operator, double ClampLow, double ClampHigh)
{
	double Result = 0.0;
	switch (Operator)
	{
	case HSLReplace:
		Result = Operand;
		break;

	case HSLMultiple:
		Result = Source * Operand;
		break;

	case HSLDivide:
		if (Operand == 0.0)
			Result = 0.0;
		else
			Result = Source / Operand;
		break;

	case HSLAdd:
		Result = Source + Operand;
		break;

	case HSLSubtract:
		Result = Source - Operand;
		break;
	}

	if (Result < ClampLow)
		Result = ClampLow;
	if (Result > ClampHigh)
		Result = ClampHigh;

	return Result;
}

/// <summary>
/// Conditionally modify H, S, or L values based on the conditions in <paramref name="Conditions"/>. Whether
/// or not modification takes place depends on the hue matching the conditional range.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
/// <param name="Conditions">Pointer to an array of ConditionalHSLAdjustment structures.</param>
/// <param name="ConditionalCount">
/// Number of ConditionalHSLAdustment structures are in the array pointed to by
/// <paramref name="Conditions"/>.
/// </param>
/// <returns>Value indicating operational results.</returns>
int HSLConditionalModify(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	void *Conditions, int ConditionalCount)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (Conditions == NULL)
		return NullPointer;
	if (ConditionalCount <= 0)
		return NoAction;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	ConditionalHSLAdjustment *Conditional = (ConditionalHSLAdjustment *)Conditions;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);

			for (int i = 0; i < ConditionalCount; i++)
			{
				if (H < Conditional[i].RangeLow)
					continue;
				if (H > Conditional[i].RangeHigh)
					continue;
				if (Conditional[i].ModifyHue)
				{
					H = ModifyHSLValue(H, Conditional[i].HueOperand, Conditional[i].HueOperation, 0.0, 360.0);
				}
				if (Conditional[i].ModifySaturation)
				{
					S = ModifyHSLValue(S, Conditional[i].SaturationOperand, Conditional[i].SaturationOperation, 0.0, 1.0);
				}
				if (Conditional[i].ModifyLuminance)
				{
					L = ModifyHSLValue(L, Conditional[i].LuminanceOperand, Conditional[i].LuminanceOperation, 0.0, 1.0);
				}
			}

			HSLtoRGB8(H, S, L, &R, &G, &B);
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

/// <summary>
/// Convert each pixel in <pararef name="Source"/> from RGB to HSL and back to RGB.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
/// <returns>Value indicating operational results.</returns>
int RGBtoHSLtoRGB(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			HSLtoRGB8(H, S, L, &R, &G, &B);
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}

	return Success;
}

int RGBImageToHueImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;
	double Adjustment = 255.0 / 360.0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			double AH = H * Adjustment;
			BYTE NewChannel = (BYTE)AH;
			Dest[Index + 3] = A;
			Dest[Index + 2] = NewChannel;
			Dest[Index + 1] = NewChannel;
			Dest[Index + 0] = NewChannel;
		}
	}

	return Success;
}

int RGBImageToSaturationImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			BYTE NewChannel = (BYTE)(S * 255.0);
			Dest[Index + 3] = A;
			Dest[Index + 2] = NewChannel;
			Dest[Index + 1] = NewChannel;
			Dest[Index + 0] = NewChannel;
		}
	}

	return Success;
}

int RGBImageToLuminanceImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			BYTE NewChannel = (BYTE)(L * 255.0);
			Dest[Index + 3] = A;
			Dest[Index + 2] = NewChannel;
			Dest[Index + 1] = NewChannel;
			Dest[Index + 0] = NewChannel;
		}
	}

	return Success;
}

int RGBImageToSLImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;
	int PixelSize = 4;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Src[Index + 3];
			BYTE R = Src[Index + 2];
			BYTE G = Src[Index + 1];
			BYTE B = Src[Index + 2];
			double H = 0.0;
			double S = 0.0;
			double L = 0.0;
			RGB8ToHSL(R, G, B, &H, &S, &L);
			BYTE NewChannel = (BYTE)(((L * 255.0) + (S * 255.0)) / 2.0);
			Dest[Index + 3] = A;
			Dest[Index + 2] = NewChannel;
			Dest[Index + 1] = NewChannel;
			Dest[Index + 0] = NewChannel;
		}
	}

	return Success;
}

/// <summary>
/// Restrict various HSL channel values to certain ranges.
/// </summary>
/// <param name="SurfaceBuffer">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
/// <param name="HueCount">Number of hues to restrict the image to.</param>
/// <returns>Value indicating operational success.</returns>
int RestrictHues2(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, double HueCount)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;
	__int32 PixelSize = 4;
	if (HueCount <= 0.0)
		HueCount = 1.0;
	if (HueCount > 360.0)
		HueCount = 360.0;
	double RangeSize = 360.0 / HueCount;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);

			double VPercent = H / RangeSize;
			H = floorf(VPercent) * RangeSize;

			HSLtoRGB8(H, S, L, &R, &G, &B);
			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}

	return Success;
}

/// <summary>
/// Restrict various HSL channel values to certain ranges.
/// </summary>
/// <param name="SurfaceBuffer">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
/// <param name="LowHue">The low hue range value.</param>
/// <param name="HighHue">the high hue range value.</param>
/// <returns>Value indicating operational success.</returns>
int RestrictHueRange(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, double LowHue, double HighHue)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;
	__int32 PixelSize = 4;
	if (LowHue > HighHue)
		return InvalidOperation;
	if (LowHue < 0.0)
		return InvalidOperation;
	if (HighHue > 360.0)
		return InvalidOperation;
	double HueRange = HighHue - LowHue;
	if (HueRange > 360.0)
		HueRange = 360.0;
	double RangeMultiplier = HueRange / 360.0;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];
			double H, S, L;
			RGB8ToHSL(R, G, B, &H, &S, &L);

			H = (H * RangeMultiplier) + LowHue;

			HSLtoRGB8(H, S, L, &R, &G, &B);
			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}

	return Success;
}