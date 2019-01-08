#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _FORWARDS_H
#define _FORWARDS_H

#pragma warning (disable : 4244 4800 4901)

/*
Internal forward declarations.
*/
double Distance(int X1, int Y1, int X2, int Y2);
double PixelLuminance(BYTE R, BYTE G, BYTE B);
double ColorLuminance3(UINT32 PackedColor, BOOL Perceived);
double PixelLuminanceSc(double R, double G, double B);
double NormalizedColorLuminance(BYTE R, BYTE G, BYTE B, BOOL Perceived);
void RGBtoScRGB(BYTE R, BYTE G, BYTE B, double *scR, double *scB, double *scG);
void ScRGBtoRGB(double scR, double scG, double scB, BYTE *R, BYTE *G, BYTE *B);
double MaxDouble(double A, double B, double C);
double MinDouble(double A, double B, double C);
void MakeColor(int X, int Y, int Width, int Height, double Hypotenuse, int PointCount, PureColorStruct* Colors,
    AbsolutePointStruct* Points, byte* FinalR, byte* FinalG, byte* FinalB);
int rrand(int Low = 0, int High = RAND_MAX);
int BitCountTable2(BYTE SourceValue);

#endif