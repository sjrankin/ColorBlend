#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Convert the passed RGB value to an HSL value.
/// </summary>
/// <param name="R">Red channel value. Normalized.</param>
/// <param name="G">Green channel value. Normalized.</param>
/// <param name="B">Blue channel value. Normalized.</param>
/// <param name="H">The resultant hue value.</param>
/// <param name="S">The resultant saturation value.</param>
/// <param name="L">The resultant luminance value.</param>
void NormalizedRGBtoHSL(double R, double G, double B, double *H, double *S, double *L)
{
	double Max = max(R, max(G, B));
	double Min = min(R, min(G, B));
	*L = (Max + Min) / 2.0;
	double Delta = Max - Min;
	*S = 0.0;
	*H = 0.0;
	if (Delta == 0)
	{
		*H = 0.0;
		*S = 0.0;
	}
	else
	{
		*S = Delta / (1.0 - fabs((2.0 * *L) - 1.0));
		if (Max == R)
		{
			*H = 60.0 * fmod(((G - B) / Delta), 6.0);
		}
		if (Max == G)
		{
			*H = 60.0 * (((B - R) / Delta) + 2.0);
		}
		if (Max == B)
		{
			*H = 60.0 * (((R - G) / Delta) + 4.0);
		}
	}
	if (*H < 0.0)
		*H = 0.0;
	if (*H > 360.0)
		*H = 360.0;
}

/// <summary>
/// Convert the passed RGB value to an HSL value.
/// </summary>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <param name="H">The resultant hue value.</param>
/// <param name="S">The resultant saturation value.</param>
/// <param name="L">The resultant luminance value.</param>
void RGB8ToHSL(BYTE R, BYTE G, BYTE B, double *H, double *S, double *L)
{
	double NR = (double)R / 255.0;
	double NG = (double)G / 255.0;
	double NB = (double)B / 255.0;
	NormalizedRGBtoHSL(NR, NG, NB, H, S, L);
}

/// <summary>
/// Convert the passed HSL color to a normalized RGB color.
/// </summary>
/// <param name="H">The hue value.</param>
/// <param name="S">The saturation value.</param>
/// <param name="L">The luminance value.</param>
/// <param name="R">Resultant normalized red channel value.</param>
/// <param name="G">Resultant normalized green channel value.</param>
/// <param name="B">Resultant normalized blue channel value.</param>
void HSLtoNormalizedRGB(double H, double S, double L, double *R, double *G, double *B)
{
	double AbsTerm = fabs((2.0 * L) - 1.0);
	double C = (1.0 - AbsTerm) * S;
	double ModTerm = fmod(H / 60.0, 2.0);
	double X = C * (1.0 - fabs(ModTerm - 1.0));
	double m = L - (C / 2.0);
	double Rp = 0.0;
	double Gp = 0.0;
	double Bp = 0.0;
	if ((H >= 0.0) && (H < 60.0))
	{
		Rp = C;
		Gp = X;
		Bp = 0.0;
	}
	else
		if ((H >= 60.0) && (H < 120.0))
		{
			Rp = X;
			Gp = C;
			Bp = 0.0;
		}
		else
			if ((H >= 120.0) && (H < 180.0))
			{
				Rp = 0.0;
				Gp = C;
				Bp = X;
			}
			else
				if ((H >= 180.0) && (H < 240.0))
				{
					Rp = 0.0;
					Gp = X;
					Bp = C;
				}
				else
					if ((H >= 240.0) && (H < 300.0))
					{
						Rp = X;
						Gp = 0.0;
						Bp = C;
					}
					else
					{
						Rp = C;
						Gp = 0.0;
						Bp = X;
					}
	*R = Rp + m;
	*G = Gp + m;
	*B = Bp + m;
}

/// <summary>
/// Convert the passed HSL color to an RGB color.
/// </summary>
/// <param name="H">The hue value.</param>
/// <param name="S">The saturation value.</param>
/// <param name="L">The luminance value.</param>
/// <param name="R">Resultant red channel value.</param>
/// <param name="G">Resultant green channel value.</param>
/// <param name="B">Resultant blue channel value.</param>
void HSLtoRGB8(double H, double S, double L, BYTE *R, BYTE *G, BYTE *B)
{
	double NR = 0.0;
	double NG = 0.0;
	double NB = 0.0;
	HSLtoNormalizedRGB(H, S, L, &NR, &NG, &NB);
	*R = (BYTE)(NR * 255.0);
	*G = (BYTE)(NG * 255.0);
	*B = (BYTE)(NB * 255.0);
}

double CircularDouble(double Value, double Low, double High)
{
	if (Low > High)
	{
		double s = Low;
		Low = High;
		High = s;
	}
	if ((Value >= Low) && (Value <= High))
		return Value;
	double Range = fabs(High - Low);
	if (Value < Low)
	{
		Value = fabs(Value - Low);
		double Modl = fmod(Value, Range);
		Value = High - Modl;
	}
	else
	{
		Value = Value - Low;
		double Mod = fmod(Value, Range);
		Value = Low + Mod;
	}
	return Value;
}

double CircularDouble360(double Value)
{
	return CircularDouble(Value, 0.0, 360.0);
}

