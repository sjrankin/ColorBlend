#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _COLORCONVERSIONS_H
#define _COLORCONVERSIONS_H

#pragma warning (disable : 4244 4800 4901)

//Color conversion functions.
extern "C" __declspec(dllexport) void RYBtoRGB(double R, double Y, double B, double *Rp, double *Gp, double *Bp);
extern "C" __declspec(dllexport) void RGBtoRYB(double R, double G, double B, double *Rp, double *Yp, double *Bp);
extern "C" __declspec(dllexport) void RGBtoYCgCo(double R, double G, double B, double *Y, double *Cg, double *Co);
extern "C" __declspec(dllexport) void YCgCotoRGB(double Y, double Cg, double Co, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void YDbDrToRGB(double Y, double Db, double Dr, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void RGBtoYDbDr(double R, double G, double B, double *Y, double *Db, double *Dr);
extern "C" __declspec(dllexport) void RGBtoTSL(double R, double G, double B, double *T, double *S, double *L);
extern "C" __declspec(dllexport) void TSLtoRGB(double T, double S, double L, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void YCbCrtoRGB(double Y, double Cb, double Cr, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void RGBtoYCbCr(double R, double G, double B, double *Y, double *Cb, double *Cr);
extern "C" __declspec(dllexport) void RGBtoHSL(double R, double G, double B, double *H, double *S, double *L);
extern "C" __declspec(dllexport) void RGBtoHSL2(BYTE R, BYTE G, BYTE B, double *H, double *S, double *L);
extern "C" __declspec(dllexport) void HSLtoRGB(double H, double S, double L, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void HSLtoRGB2(double H, double S, double L, BYTE *R, BYTE *G, BYTE *B);
extern "C" __declspec(dllexport) void RGBtoHSV(double R, double G, double B, double *H, double *S, double *V);
extern "C" __declspec(dllexport) void HSVtoRGB(double H, double S, double V, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void RGBtoYIQ(double R, double G, double B, double *Y, double *I, double *Q);
extern "C" __declspec(dllexport) void YIQtoRGB(double Y, double I, double Q, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void RGBtoXYZ(double R, double G, double B, double *X, double *Y, double *Z);
extern "C" __declspec(dllexport) void XYZtoRGB(double X, double Y, double Z, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void RGBtoCMY(double R, double G, double B, double *C, double *M, double *Y);
extern "C" __declspec(dllexport) void CMYtoRGB(double C, double M, double Y, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void CMYtoCMYK(double C, double M, double Y, double *Cp, double *Mp, double *Yp, double *Kp);
extern "C" __declspec(dllexport) void CMYKtoCMY(double C, double M, double Y, double K, double *Cp, double *Mp, double *Yp);
extern "C" __declspec(dllexport) void RGBtoCMYK(double R, double G, double B, double *C, double *M, double *Y, double *K);
extern "C" __declspec(dllexport) void CMYKtoRGB(double C, double M, double Y, double K, double *R, double *G, double *B);
extern "C" __declspec(dllexport) void XYZtoCIELab(double X, double Y, double Z, double *L, double *A, double *B);
extern "C" __declspec(dllexport) void CIELABtoXYZ(double L, double A, double B, double *X, double *Y, double *Z);
extern "C" __declspec(dllexport) void RGBtoCIELAB(double R, double G, double B, double *Lp, double *Ap, double *Bp);
extern "C" __declspec(dllexport) void CIELABtoRGB(double L, double A, double B, double *Rp, double *Gp, double *Bp);
extern "C" __declspec(dllexport) void RGBtoYUV(double R, double G, double B, double *Y, double *U, double *V);
extern "C" __declspec(dllexport) void YUVtoRGB(double Y, double U, double V, double *R, double *G, double *B);
extern "C" __declspec(dllexport) double ColorLuminance2(BYTE R, BYTE G, BYTE B, BOOL Perceived);
extern "C" __declspec(dllexport) void SetPixelLuminance(BYTE *R, BYTE *G, BYTE *B, double NewLuminance);
extern "C" __declspec(dllexport) double GetPixelLuminance(BYTE R, BYTE G, BYTE B);
extern "C" __declspec(dllexport) double GetPixelSaturation(BYTE R, BYTE G, BYTE B);
extern "C" __declspec(dllexport) double GetPixelHue(BYTE R, BYTE G, BYTE B);
extern "C" __declspec(dllexport) int ConvertColorSpace(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ToColorSpace);
extern "C" __declspec(dllexport) void ChangeHue(BYTE *R, BYTE *G, BYTE *B, int Angle);
extern "C" __declspec(dllexport) void AdjustHue(BYTE *R, BYTE *G, BYTE *B, int Angle);
extern "C" __declspec(dllexport) double ClampHue(double Hue, double Rotation);
extern "C" __declspec(dllexport) void ShiftHue(BYTE *R, BYTE *G, BYTE *B, int AngleOffset);
extern "C" __declspec(dllexport) double RGBtoHue(double R, double G, double B);
extern "C" __declspec(dllexport) double RGBtoHue2(BYTE R, BYTE G, BYTE B);

#endif