#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _HSL_H
#define _HSL_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) void NormalizedRGBtoHSL(double R, double G, double B, double *H, double *S, double *L);
extern "C" __declspec(dllexport) void RGB8ToHSL(BYTE R, BYTE G, BYTE B, double *H, double *S, double *L);
extern "C" __declspec(dllexport) void HSLtoNormalizedRGB(double H, double S, double L, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void HSLtoRGB8(double H, double S, double L, BYTE *R, BYTE *G, BYTE *B);
extern "C" __declspec(dllexport) double CircularDouble(double Value, double Low, double High);
extern "C" __declspec(dllexport) double CircularDouble360(double Value);

#endif
