#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

inline void RGBtoScRGB(BYTE R, BYTE G, BYTE B, double *scR, double *scB, double *scG)
{
    *scR = (double)R / 255.0;
    *scG = (double)G / 255.0;
    *scB = (double)B / 255.0;
}

inline void ScRGBtoRGB(double scR, double scG, double scB, BYTE *R, BYTE *G, BYTE *B)
{
    *R = (scR * 255.0);
    *G = (scG * 255.0);
    *B = (scB * 255.0);
}

inline double MaxDouble(double A, double B, double C)
{
    return max(A, max(B, C));
}

inline double MinDouble(double A, double B, double C)
{
    return min(A, min(B, C));
}

/// <summary>
/// Converts a color in YCbCr color space to RGB color space.
/// </summary>
/// <param name="Y">The Y component.</param>
/// <param name="Cb">The Cb component.</param>
/// <param name="Cr">The Cr component.</param>
/// <param name="R">The converted red component.</param>
/// <param name="G">The converted green component.</param>
/// <param name="B">The converted blue component.</param>
void YCbCrtoRGB(double Y, double Cb, double Cr, double *R, double *G, double *B)
{
    *R = Y + (1.371 * (Cr - 128));
    *G = Y - (0.698 * (Cr - 128)) - (0.336 * (Cb - 128));
    *B = Y + (1.732 * (Cr - 128));
}

/// <summary>
/// Converts a color in the RGB color space to the YCbCr color space.
/// </summary>
/// <param name="R">The red component to convert.</param>
/// <param name="G">The green component to convert.</param>
/// <param name="B">The blue component to convert.</param>
/// <param name="Y">Will contain the Y component upon conversion.</param>
/// <param name="Cb">Will contain the Cb component upon conversion.</param>
/// <param name="Cr">Will contain the Cr component upon conversion.</param>
void RGBtoYCbCr(double R, double G, double B, double *Y, double *Cb, double *Cr)
{
    *Y = (double)(R * (77 / 255)) + (G * (150 / 255)) + (B * (29 / 255));
    *Cb = (double)(R * (44 / 255)) - (G * (87 / 255)) + (B * (131 / 255)) + 128;
    *Cr = (double)(R * (131 / 255)) - (G * (110 / 255)) - (B * (21 / 255)) + 128;
}

/// <summary>
/// Convert a color in the RGB color space to the equivalent color in the HSL color space.
/// </summary>
/// <param name="R">The red component to convert.</param>
/// <param name="G">The green component to convert.</param>
/// <param name="B">The blue component to convert.</param>
/// <param name="H">On return will contain the hue value.</param>
/// <param name="S">On return will contain the saturation value.</param>
/// <param name="L">On return will contain the lumaninance value.</param>
void RGBtoHSL(double R, double G, double B, double *H, double *S, double *L)
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
/// Convert a color in the RGB color space to the equivalent color in the HSL color space.
/// </summary>
/// <param name="R">The red component (byte format) to convert.</param>
/// <param name="G">The green component (byte format) to convert.</param>
/// <param name="B">The blue component (byte format) to convert.</param>
/// <param name="H">On return will contain the hue value.</param>
/// <param name="S">On return will contain the saturation value.</param>
/// <param name="L">On return will contain the lumaninance value.</param>
void RGBtoHSL2(BYTE R, BYTE G, BYTE B, double *H, double *S, double *L)
{
    RGBtoHSL((double)R / 255.0, (double)G / 255.0, (double)B / 255.0, H, S, L);
}

double RGBtoHue(double R, double G, double B)
{
    double H, S, L;
    RGBtoHSL(R, G, B, &H, &S, &L);
    return H;
}

double RGBtoHue2(BYTE R, BYTE G, BYTE B)
{
    return RGBtoHue((double)R / 255.0, (double)G / 255.0, (double)B / 255.0);
}

/// <summary>
/// Converts a color in the HSL color space to the equivalent in the RGB color space.
/// </summary>
/// <param name="H">The H component to convert.</param>
/// <param name="S">The S component to convert.</param>
/// <param name="L">The L component to convert.</param>
/// <param name="R">Will contain the red component.</param>
/// <param name="G">Will contain the green component.</param>
/// <param name="B">Will contain the blue component.</param>
void HSLtoRGB(double H, double S, double L, double *R, double *G, double *B)
{
    double C = (1 - fabs((2 * (L - 1)))) * S;
    double X = C * (1 - fabs((double)((int)(H / 60.0) % 2 - 1)));
    double m = L - (C / 2.0);
    if ((H <= 0.0) && (H < 60.0))
    {
        *R = C + m;
        *G = X + m;
        *B = m;
    }
   // if ((H <= 60.0) && (H < 120.0))
        if ((H >= 60.0) && (H < 120.0))
    {
        *R = X + m;
        *G = C + m;
        *B = 0 + m;
    }
//    if ((H <= 120.0) && (H < 180.0))
        if ((H >= 120.0) && (H < 180.0))
    {
        *R = 0 + m;
        *G = C + m;
        *B = X + m;
    }
//    if ((H <= 180.0) && (H < 240.0))
        if ((H >= 180.0) && (H < 240.0))
    {
        *R = 0 + m;
        *G = X + m;
        *B = C + m;
    }
  //  if ((H <= 240.0) && (H < 300.0))
        if ((H >= 240.0) && (H < 300.0))
    {
        *R = X + m;
        *G = 0 + m;
        *B = C + m;
    }
//    if ((H <= 300.0) && (H < 360.0))
        if ((H >= 300.0) && (H < 360.0))
    {
        *R = C + m;
        *G = 0 + m;
        *B = X + m;
    }
}

/// <summary>
/// Converts a color in the HSL color space to the equivalent in the RGB color space.
/// </summary>
/// <param name="H">The H component to convert.</param>
/// <param name="S">The S component to convert.</param>
/// <param name="L">The L component to convert.</param>
/// <param name="R">Will contain the red component in byte format.</param>
/// <param name="G">Will contain the green component in byte format.</param>
/// <param name="B">Will contain the blue component in byte format.</param>
void HSLtoRGB2(double H, double S, double L, BYTE *R, BYTE *G, BYTE *B)
{
    double LR = 0.0;
    double LG = 0.0;
    double LB = 0.0;
    HSLtoRGB(H, S, L, &LR, &LG, &LB);
    *R = (BYTE)(255 * LR);
    *G = (BYTE)(255 * LG);
    *B = (BYTE)(255 * LB);
}

void SetPixelLuminance(BYTE *R, BYTE *G, BYTE *B, double NewLuminance)
{
    double H;
    double S;
    double L;
    RGBtoHSL2(*R, *G, *B, &H, &S, &L);
    HSLtoRGB2(H, S, NewLuminance, R, G, B);
}

/// <summary>
/// Return the luminance of the pixel with the passed colors.
/// </summary>
/// <remarks>
/// Converts the RGB color to HSL and returns the Luminance value.
/// </remarks>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <returns>Luminance of the supplied RGB color.</returns>
double GetPixelLuminance(BYTE R, BYTE G, BYTE B)
{
    double H, S, L;
    RGBtoHSL2(R, G, B, &H, &S, &L);
    return L;
}

/// <summary>
/// Return the saturation of the pixel with the passed colors.
/// </summary>
/// <remarks>
/// Converts the RGB color to HSL and returns the Saturation value.
/// </remarks>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <returns>Saturation of the supplied RGB color.</returns>
double GetPixelSaturation(BYTE R, BYTE G, BYTE B)
{
    double H, S, L;
    RGBtoHSL2(R, G, B, &H, &S, &L);
    return S;
}

/// <summary>
/// Return the hue of the pixel with the passed colors.
/// </summary>
/// <remarks>
/// Converts the RGB color to HSL and returns the Hue value.
/// </remarks>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <returns>Hue of the supplied RGB color.</returns>
double GetPixelHue(BYTE R, BYTE G, BYTE B)
{
    double H, S, L;
    RGBtoHSL2(R, G, B, &H, &S, &L);
    return H;
}

/// <summary>
/// Adjust the hue of the supplied RGB color.
/// </summary>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <param name="Angle">Angle to be added to the hue.</param>
void AdjustHue(BYTE *R, BYTE *G, BYTE *B, int Angle)
{
    double H, S, L;
    RGBtoHSL2(*R, *G, *B, &H, &S, &L);
    H += Angle;
    if (H > 360)
    {
        H -= 360;
    }
    else
    {
        if (H < 0)
            H += 360;
    }
    HSLtoRGB2(H, S, L, R, G, B);
}

/// <summary>
/// Change the hue of the supplied RGB color.
/// </summary>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <param name="Angle">Angle to be added to the hue.</param>
void ChangeHue(BYTE *R, BYTE *G, BYTE *B, int Angle)
{
    double H, S, L;
    RGBtoHSL2(*R, *G, *B, &H, &S, &L);
    H = Angle;
    if (H > 360)
    {
        H -= 360;
    }
    else
    {
        if (H < 0)
            H += 360;
    }
    HSLtoRGB2(H, S, L, R, G, B);
}

double ClampHue(double Hue, double Rotation)
{
	double Rotated = Hue + Rotation;
	if (Rotated < 0.0)
		Rotated = Rotated + 360.0;
	if (Rotated > 360.0)
		Rotated = Rotated - 360.0;
	return Rotated;
}

/// <summary>
/// Adjust the hue of the specified color by adding the supplied offset.
/// </summary>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <param name="AngleOffset">Offset to be added to the hue.</param>
void ShiftHue(BYTE *R, BYTE *G, BYTE *B, int AngleOffset)
{
#if 0
	if (AngleOffset < 0)
		AngleOffset = 0;
	if (AngleOffset > 360)
		AngleOffset = 360;
#endif
	double H, S, L;
	RGB8ToHSL(*R, *G, *B, &H, &S, &L);
	H = ClampHue(H, AngleOffset);
//	H += AngleOffset;
	H = H > 360.0 ? H - 360.0 : H;
	H = H < 0.0 ? H + 360.0 : H;
	HSLtoRGB8(H, S, L, R, G, B);
}

/// <summary>
/// Convert a color in the RGB color space to the equivalent in the HSV color space.
/// </summary>
/// <param name="R">The red component to convert. Value ranges from 0.0 to 1.0.</param>
/// <param name="G">The green component to convert. Value ranges from 0.0 to 1.0.</param>
/// <param name="B">The blue component to convert. Value ranges from 0.0 to 1.0.</param>
/// <param name="H">Will contain the H component. Value will range from 0.0 to 360.0.</param>
/// <param name="S">Will contain the S component. Value will range from 0.0 to 1.0.</param>
/// <param name="V">Will contain the V component. Value will range from 0.0 to 1.0.</param>
void RGBtoHSV(double R, double G, double B, double *H, double *S, double *V)
{
    double Min = min(R, min(G, B));
    double Max = max(R, max(G, B));
    *V = Max;
    double Delta = Max - Min;
    if (Max != 0.0)
        *S = Delta / Max;
    else
    {
        //Black
        *S = 0.0;
        *H = -1;
        return;
    }
    if (R == Max)
        *H = (G - B) / Delta;
    else
        if (G == Max)
            *H = 2 + ((B - R) / Delta);
        else
            *H = 4 + ((R - G) / Delta);
    *H *= 60.0;
    if (*H < 0.0)
        *H += 360.0;
}

/// <summary>
/// Convert a color in the HSV color space to the equivalent in the RGB color space.
/// </summary>
/// <remarks>
/// http://www.cs.rit.edu/~ncs/color/t_convert.html
/// </remarks>
/// <param name="H">The H component to convert. Valid range: 0 - 360.</param>
/// <param name="S">The S component to convert. Valid range: 0.0 - 1.0.</param>
/// <param name="V">The V component to convert. Valid range: 0.0 - 1.0.</param>
/// <param name="R">Will contain the R value.</param>
/// <param name="G">Will contain the G value.</param>
/// <param name="B">Will contain the B value.</param>
void HSVtoRGB(double H, double S, double V, double *R, double *G, double *B)
{
    if (S == 0.0)
    {
        //Monochromatic
        *R = *G = *B = V;
        return;
    }

    H /= 60;
    int Sector = floor(H);
    double Fact = H - Sector;
    double x = V * (1 - S);
    double y = V * (1 - (S * Fact));
    double z = V * (1 - (S * (1 - Fact)));

    switch (Sector)
    {
    case 0:
        *R = V;
        *G = z;
        *B = x;
        break;

    case 1:
        *R = y;
        *G = V;
        *B = x;
        break;

    case 2:
        *R = x;
        *G = V;
        *B = z;
        break;

    case 3:
        *R = x;
        *G = y;
        *B = V;
        break;

    case 4:
        *R = z;
        *G = y;
        *B = V;
        break;

    default:
        *R = V;
        *G = x;
        *B = y;
        break;
    }
}

//http://bahamas10.github.io/ryb/assets/ryb.pdf
double CubicInt(double T, double A, double B)
{
    double Weight = T * T * (3 - (2 * T));
    return A * Weight * (B - A);
}

//http://www.deathbysoftware.com/colors/index.html
void RGBtoRYB(double R, double G, double B, double *Rp, double *Yp, double *Bp)
{
    double White = min(R, min(R, B));
    double lR = R - White;
    double lG = G - White;
    double lB = B - White;
    double MaxGreen = max(lR, max(lG, lB));
    double lY = min(lR, lG);
    lR -= lY;
    lG -= lY;
    if (lB > 0.0 && lG > 0.0)
    {
        lG /= 2.0;
        lB /= 2.0;
    }
    lY += lG;
    lB += lG;
    double MaxYellow = max(lR, max(lY, lB));
    if (MaxYellow > 0.0)
    {
        double lN = MaxGreen / MaxYellow;
        lR *= lN;
        lY *= lN;
        lB *= lN;
    }
    *Rp = floor(lR + White);
    *Yp = floor(lY + White);
    *Bp = floor(lB + White);
}

void RYBtoRGB(double R, double Y, double B, double *Rp, double *Gp, double *Bp)
{
    double x0, x1, x2, x3, y0, y1;
    x0 = CubicInt(B, 1.0, 0.163);
    x1 = CubicInt(B, 1.0, 0.0);
    x2 = CubicInt(B, 1.0, 0.5);
    x3 = CubicInt(B, 1.0, 0.2);
    y0 = CubicInt(Y, x0, x1);
    y1 = CubicInt(Y, x2, x3);
    *Rp = CubicInt(R, y0, y1);

    x0 = CubicInt(B, 1.0f, 0.373f);
    x1 = CubicInt(B, 1.0f, 0.66f);
    x2 = CubicInt(B, 0.0f, 0.0f);
    x3 = CubicInt(B, 0.5f, 0.094f);
    y0 = CubicInt(Y, x0, x1);
    y1 = CubicInt(Y, x2, x3);
    *Gp = CubicInt(R, y0, y1);

    x0 = CubicInt(B, 1.0f, 0.6f);
    x1 = CubicInt(B, 0.0f, 0.2f);
    x2 = CubicInt(B, 0.0f, 0.5f);
    x3 = CubicInt(B, 0.0f, 0.0f);
    y0 = CubicInt(Y, x0, x1);
    y1 = CubicInt(Y, x2, x3);
    *Bp = CubicInt(R, y0, y1);
}

void RGBtoYDbDr(double R, double G, double B, double *Y, double *Db, double *Dr)
{
    *Y = (0.299 * R) + (0.587 * G) + (0.114 * B);
    *Db = (-0.450 * R) - (0.833 * G) + (1.333 * B);
    *Dr = (-1.333 * R) + (1.116 * G) + (0.217 * B);
}

void YDbDrToRGB(double Y, double Db, double Dr, double *R, double *G, double *B)
{
    *R = Y + (0.000092303716148 * Db) - (0.525912630661865 * Dr);
    *G = Y - (0.129132898890590 * Db) + (0.267899328207599 * Dr);
    *B = Y + (0.66479059978955 * Db) - (0.000079202543533 * Dr);
}

//http://en.wikipedia.org/wiki/OSA-UCS

void RGBtoYCgCo(double R, double G, double B, double *Y, double *Cg, double *Co)
{
    *Y = (0.25 * R) + (0.5 * G) + (0.5 * B);
    *Cg = -(0.25 * R) + (0.5 * G) - (0.5 * B);
    *Co = (0.5 * R) - (0.5 * B);
}

void YCgCotoRGB(double Y, double Cg, double Co, double *R, double *G, double *B)
{
    double scratch = Y - Cg;
    *R = scratch + Co;
    *G = Y + Cg;
    *B = scratch - Co;
}
//http://www.wolframalpha.com/input/?i=rgb%28100%2C0%2C0%29+to+ryb
void XYZtoCIULUV(double X, double Y, double Z, double *L, double *U, double *V)
{
    double uprime = (4.0 * X) / (X + (15.0 * Y) + (3.0 * Z));
    double vprime = (9.0 * Y) / (X + (15.0 * Y) + (3.0 * Z));
}

void RGBtoTSL(double R, double G, double B, double *T, double *S, double *L)
{
    *T = 0.0;
    if (G == 0.0)
    {
        *T = 0.0;
    }
    else
    {
        if (G > 0.0)
        {
            *T = (1.0 / (2.0 * 3.1415)) * (atanf((R / G) + (1.0 / 4.0)));
        }
        else
        {
            *T = (1.0 / (2.0 * 3.1415))*(atanf((R / G + (3.0 / 4.0))));
        }
    }
    *S = sqrtf((9.0 / 5.0) * ((R * R) + (G * G)));
    *L = (0.299 * R) + (0.587 * G) + (0.114 * B);
}

void TSLtoRGB(double T, double S, double L, double *R, double *G, double *B)
{
    double x = -(1.0 / tan(2.0 * 3.1415 * T));
    double g = 0.0;
    if (T == 0.0)
    {
        g = 0.0;
    }
    else
    {
        if (T > 0.5)
        {
            g = -(sqrt(5.0 / (9.0 * ((x * x) + 1)))) * S;
        }
        else
        {
            if (T < 0.5)
            {
                g = (sqrt(5.0 / (9.0 * ((x * x) + 1)))) * S;
            }
        }
    }
    double r = 0.0;
    if (T == 0.0)
    {
        r = (sqrt(5.0) / 3.0)* S;
    }
    else
    {
        r = (x * g) + (1.0 / 3.0);
    }
    double k = L / ((0.185 * r) + (0.473 * g) + (0.114));
    *R = k * r;
    *G = k * g;
    *B = k * (1.0 - r - g);
}

void RGBtoYUV(double R, double G, double B, double *Y, double *U, double *V)
{
    *Y = (R * 0.299) + (G * 0.587) + (B * 0.114);
    *U = (R * -0.14713) + (G * -0.28886) + (B * 0.436);
    *V = (R * 0.615) + (G * -0.51499) + (B * -0.10001);
}

void YUVtoRGB(double Y, double U, double V, double *R, double *G, double *B)
{
    *R = (1.0 * Y) + (0.0 * U) + (1.13983 * V);
    *G = (1.0 * Y) + (-0.39465 * U) + (-0.58060 * V);
    *B = (1.0 * Y) + (2.03211 * U) + (0.0 * V);
}

void RGBtoYIQ(double R, double G, double B, double *Y, double *I, double *Q)
{
    *Y = (0.299 * R) + (0.587 * G) + (0.114 * B);
    *I = (0.596 * R) - (0.275 * G) - (0.321 * B);
    *Q = (0.212 * R) - (0.523 * G) + (0.311 * B);
}

void YIQtoRGB(double Y, double I, double Q, double *R, double *G, double *B)
{
    *R = (1.0 * Y) + (0.9563 * I) + (0.6210 * Q);
    *G = (1.0 * Y) - (0.2721 * I) - (0.6474 * Q);
    *B = (1.0 * Y) - (1.1070 * I) + (1.7046 * Q);
}

void RGBtoXYZ(double R, double G, double B, double *X, double *Y, double *Z)
{
    *X = (R * 0.412456) + (G * 0.357580) + (B * 0.180423);
    *Y = (R * 0.212671) + (G * 0.715160) + (B * 0.072169);
    *Z = (R * 0.019334) + (G * 0.119193) + (B * 0.950227);
}

void XYZtoRGB(double X, double Y, double Z, double *R, double *G, double *B)
{
    *R = (X * 3.240479) - (1.537150 * Y) - (0.498535 * Z);
    *G = (X * -0.969256) + (1.875992 * Y) + (0.041556 * Z);
    *B = (X * 0.055648) - (0.204043 * Y) + (1.057311 * Z);
}

void RGBtoCMY(double R, double G, double B, double *C, double *M, double *Y)
{
    *C = 1.0 - R;
    *M = 1.0 - G;
    *Y = 1.0 - B;
}

void CMYtoRGB(double C, double M, double Y, double *R, double *G, double *B)
{
    *R = 1.0 - C;
    *G = 1.0 - M;
    *B = 1.0 - Y;
}

void CMYtoCMYK(double C, double M, double Y, double *Cp, double *Mp, double *Yp, double *Kp)
{
    double tK = 1.0;
    if (C < tK)
        tK = C;
    if (M < tK)
        tK = M;
    if (Y < tK)
        tK = Y;
    if (tK == 0.0)
    {
        *Cp = 0.0;
        *Mp = 0.0;
        *Yp = 0.0;
        *Kp = 0.0;
        return;
    }
    *Cp = (C - tK) / (1.0 - tK);
    *Mp = (M - tK) / (1.0 - tK);
    *Yp = (Y - tK) / (1.0 - tK);
    *Kp = tK;
}

void CMYKtoCMY(double C, double M, double Y, double K, double *Cp, double *Mp, double *Yp)
{
    *Cp = (C * (1.0 - K)) + K;
    *Mp = (M * (1.0 - K)) + K;
    *Yp = (Y * (1.0 - K)) + K;
}

void RGBtoCMYK(double R, double G, double B, double *C, double *M, double *Y, double *K)
{
	/*
    double Cp, Mp, Yp;
    RGBtoCMY(R, G, B, &Cp, &Mp, &Yp);
    CMYtoCMYK(Cp, Mp, Yp, C, M, Y, K);
	*/
	if (R == 0 && G == 0 && B == 0)
	{
		*C = 0.0;
		*M = 0.0;
		*Y = 0.0;
		*K = 1.0;
		return;
	}
	double rn = R / 255.0;
	double gn = G / 255.0;
	double bn = B / 255.0;
	double Cc = 1.0 - rn;
	double Mc = 1.0 - gn;
	double Yc = 1.0 - bn;
	double Min = min(Cc, min(Mc, Yc));
	Cc = (Cc - Min) / (1 - Min);
	Mc = (Mc - Min) / (1 - Min);
	Yc = (Yc - Min) / (1 - Min);
	double Kc = Min;
}

void CMYKtoRGB(double C, double M, double Y, double K, double *R, double *G, double *B)
{
    double Cp, Mp, Yp;
    CMYKtoCMY(C, M, Y, K, &Cp, &Mp, &Yp);
    CMYtoRGB(Cp, Mp, Yp, R, G, B);
}

const double RefX = 95.047;
const double RefY = 100.00;
const double RefZ = 100.00;

void XYZtoCIELab(double X, double Y, double Z, double *L, double *A, double *B)
{
    double Xp = X / RefX;
    double Yp = Y / RefY;
    double Zp = Z / RefZ;

    if (Xp < 0.008856)
        Xp = pow(Xp, (double)(1 / 3));
    else
        Xp = (7.787 * Xp) + (16 / 116);
    if (Yp < 0.008856)
        Yp = pow(Yp, (double)(1 / 3));
    else
        Yp = (7.787 * Yp) + (16 / 116);
    if (Zp < 0.008856)
        Zp = pow(Zp, (double)(1 / 3));
    else
        Zp = (7.787 * Zp) + (16 / 116);
    *L = (116 * Yp) - 16;
    *A = 500 * (Xp - Yp);
    *B = 500 * (Yp - Zp);
}

void CIELABtoXYZ(double L, double A, double B, double *X, double *Y, double *Z)
{
    double Yp = (L + 16) / 116;
    double Xp = (A / 500) + Yp;
    double Zp = Yp - (B / 200);

    if (pow(Xp, 3)>0.008856)
        Xp = pow(Xp, 3);
    else
        Xp = (Xp - (16 / 116) / 7.787);
    if (pow(Yp, 3) > 0.008856)
        Yp = pow(Yp, 3);
    else
        Yp = (Yp - (16 / 116) / 7.787);
    if (pow(Zp, 3) > 0.008856)
        Zp = pow(Zp, 3);
    else
        Zp = (Zp - (16 / 116) / 7.787);

    *X = RefX * Xp;
    *Y = RefY * Yp;
    *Z = RefZ * Zp;
}

void RGBtoCIELAB(double R, double G, double B, double *Lp, double *Ap, double *Bp)
{
    double X, Y, Z;
    RGBtoXYZ(R, G, B, &X, &Y, &Z);
    XYZtoCIELab(X, Y, Z, Lp, Ap, Bp);
}

void CIELABtoRGB(double L, double A, double B, double *Rp, double *Gp, double *Bp)
{
    double X, Y, Z;
    CIELABtoXYZ(L, A, B, &X, &Y, &Z);
    XYZtoRGB(X, Y, Z, Rp, Gp, Bp);
}

int RGBTo(double Rin, double Gin, double Bin, double *Channel1Out, double *Channel2Out, double *Channel3Out, double *Channel4Out,
    BYTE ToFormat)
{
    switch (ToFormat)
    {
    case ToRGB:
        *Channel1Out = Rin;
        *Channel2Out = Gin;
        *Channel3Out = Bin;
        return Success;

    case ToHSL:
        RGBtoHSL(Rin, Gin, Bin, Channel1Out, Channel2Out, Channel3Out);
        return Success;

    case ToHSV:
        RGBtoHSV(Rin, Gin, Bin, Channel1Out, Channel2Out, Channel3Out);
        return Success;

    case ToCMY:
        RGBtoCMY(Rin, Gin, Bin, Channel1Out, Channel2Out, Channel3Out);
        return Success;

    case ToCMYK:
        RGBtoCMYK(Rin, Gin, Bin, Channel1Out, Channel2Out, Channel3Out, Channel4Out);
        return Success;

    case ToXYZ:
        RGBtoXYZ(Rin, Gin, Bin, Channel1Out, Channel2Out, Channel3Out);
        return Success;

    case ToLAB:
        RGBtoCIELAB(Rin, Gin, Bin, Channel1Out, Channel2Out, Channel3Out);
        return Success;

    case ToYCbCr:
        //RGBtoYCbCr ();
        break;
    }
    return InvalidOperation;
}

int ColorSpaceConversion(double Channel1In, double Channel2In, double Channel3In, double Channel4In,
    double *Channel1Out, double *Channel2Out, double *Channel3Out, double *Channel4Out,
    BYTE From, BYTE To)
{
    switch (From)
    {
    case FromRGB:
        RGBTo(Channel1In, Channel2In, Channel3In, Channel1Out, Channel2Out, Channel3Out, Channel4Out, To);
        break;

    case FromHSL:
        break;

    case FromHSV:
        break;

    case FromXYZ:
        break;

    case FromLAB:
        break;

    case FromCMY:
        break;

    case FromCMYK:
        break;
    }
    return Success;
}

int ConvertBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 ConvertFrom, __int32 ConvertTo)
{
    if (Target == NULL)
        return NullPointer;

    BYTE *Buffer = (BYTE *)Target;
    BYTE PixelSize = 4;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE Channel1 = Buffer[Index + 0];
            BYTE Channel2 = Buffer[Index + 1];
            BYTE Channel3 = Buffer[Index + 2];
            BYTE Channel4 = Buffer[Index + 3];
            double scR, scG, scB, scA;
            scA = 1.0;
            switch (ConvertTo)
            {
            case ToRGB:
                switch (ConvertFrom)
                {
                case FromRGB:
                    break;

                case FromCMY:
                    CMYtoRGB(Channel1, Channel2, Channel3, &scR, &scG, &scB);
                    break;

                case FromCMYK:
                    CMYKtoRGB((double)Channel1 / 255.0, (double)Channel2 / 255.0, (double)Channel3 / 255.0, (double)Channel4 / 255.0,
                        &scR, &scG, &scB);
                    break;

                case FromHSL:
                    break;

                case FromHSV:
                    break;

                case FromLAB:
                    break;

                case FromXYZ:
                    break;
                }
                break;
            }
            Buffer[Index + 0] = (BYTE)(scB * 255.0);
            Buffer[Index + 1] = (BYTE)(scG * 255.0);
            Buffer[Index + 2] = (BYTE)(scR * 255.0);
            Buffer[Index + 3] = (BYTE)(scA * 255.0);
        }
    }

    return Success;
}

/// <summary>
/// Calculate the luminance of the passed color.
/// </summary>
/// <param name="R">Red channel value.</param>
/// <param name="G">Green channel value.</param>
/// <param name="B">Blue channel value.</param>
/// <param name="Perceived">Determines if perceptual or objective luminance is calculated.</param>
/// <returns>Luminance of the passed color.</returns>
double ColorLuminance2(BYTE R, BYTE G, BYTE B, BOOL Perceived)
{
    if (Perceived)
    {
        return ((double)R * 0.299) + ((double)G * 0.587) + ((double)B * 0.114);
    }
    else
    {
        return ((double)R * 0.2126) + ((double)G * 0.7152) + ((double)B * 0.0722);
    }
}


double NormalizedColorLuminance(BYTE R, BYTE G, BYTE B, BOOL Perceived)
{
    double Lum = ColorLuminance2(R, G, B, Perceived);
    Lum = Lum / 255.0;
    return Lum;
}

double ColorLuminance3(UINT32 PackedColor, BOOL Perceived)
{
    if (Perceived)
    {
        return ((double)((PackedColor & 0x00ff0000) >> 16) * 0.299) + ((double)((PackedColor & 0x0000ff00) >> 8) * 0.587) + ((double)((PackedColor & 0x000000ff) >> 0) * 0.114);
    }
    else
    {
        return ((double)((PackedColor & 0x00ff0000) >> 16) * 0.2126) + ((double)((PackedColor & 0x0000ff00) >> 8) * 0.7152) + ((double)((PackedColor & 0x000000ff) >> 0) * 0.0722);
    }
}