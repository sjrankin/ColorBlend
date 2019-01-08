#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include <float.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Clamp <paramref name="Value"/> to a BYTE.
/// </summary>
/// <param name="Value">The value to clamp.</param>
/// <returns>Value of <paramref name="Value"/> clamped to BYTE range.</returns>
inline BYTE ByteClamp(int Value)
{
    return (BYTE)max(min(Value, 0), 0xff);
}

/// <summary>
/// Modify <paramref name="Source"/> with the supplied kernel and return the results in <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Pointer to the source image. Not modified by this function.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Where the modified image will be written.</param>
/// <param name="PackedBGPixel">Pixel used to clear the destination buffer.</param>
/// <param name="Kernel">The kernel to execute against <paramref name="Source"/>. Pointer to an array of doubles (but treated as a matrix).</param>
/// <param name="KernelX">Width of the kernel's matrix.</param>
/// <param name="KernelY">Height of the kernel's matrix.</param>
/// <param name="UseAlpha">If TRUE, the alpha channel will be convolved.</param>
/// <param name="UseRed">If TRUE, the red channel will be convolved.</param>
/// <param name="UseGreen">If TRUE, the green channel will be convolved.</param>
/// <param name="UseBlue">If TRUE, the blue channel will be convolved.</param>
/// <param name="SkipTransparentPixels">If TRUE, transparent pixels will not be modified.</param>
/// <returns>Value indicating result of operation.</returns>
int MasterConvolveWithKernel(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, UINT32 PackedBGPixel,
    void *KernelMatrix, int KernelX, int KernelY, double Bias, double Factor,
    BOOL UseAlpha, BOOL UseRed, BOOL UseGreen, BOOL UseBlue, BOOL SkipTransparentPixels, BOOL IncludeTransparentPixels,
    BOOL UseLuminance, double LuminanceThreshold)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (KernelMatrix == NULL)
        return NullPointer;
    if (KernelX < 1)
        return InvalidOperation;
    if (KernelY < 1)
        return InvalidOperation;
    if ((KernelX & 0x1) == 0)
        return InvalidOperation;
    if ((KernelY & 0x1) == 0)
        return InvalidOperation;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    double *Kernel = (double *)KernelMatrix;
    int PixelSize = 4;
    int WidthHalfSpan = KernelX / 2;
    int HeightHalfSpan = KernelY / 2;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            if (UseLuminance)
            {
                double PixelLuminance = ColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
                if (PixelLuminance > LuminanceThreshold)
                    continue;
            }
            if (SkipTransparentPixels)
            {
                if (Src[Index + 3] == 0x0)
                {
                    Dest[Index + 3] = Src[Index + 3];
                    Dest[Index + 2] = Src[Index + 2];
                    Dest[Index + 1] = Src[Index + 1];
                    Dest[Index + 0] = Src[Index + 0];
                    continue;
                }
            }

            int KHStart = Column - WidthHalfSpan;
            if (KHStart < 0)
                KHStart = 0;
            int KHEnd = Column + WidthHalfSpan;
            if (KHEnd >= Width)
                KHEnd = Width - 1;

            int KVStart = Row - HeightHalfSpan;
            if (KVStart < 0)
                KVStart = 0;
            int KVEnd = Row + HeightHalfSpan;
            if (KVEnd >= Height)
                KVEnd = Height - 1;

            double AAcc = 0.0;
            double RAcc = 0.0;
            double GAcc = 0.0;
            double BAcc = 0.0;

            int Kdx = 0;
            for (int KY = KVStart; KY <= KVEnd; KY++)
            {
                int KYOffset = KY * Stride;
                for (int KX = KHStart; KX <= KHEnd; KX++)
                {
                    int KIndex = (KX * PixelSize) + KYOffset;
                    if (!IncludeTransparentPixels)
                        if (Src[KIndex + 3] == 0)
                            continue;
                    AAcc += (double)Src[KIndex + 3] * Kernel[Kdx];
                    RAcc += (double)Src[KIndex + 2] * Kernel[Kdx];
                    GAcc += (double)Src[KIndex + 1] * Kernel[Kdx];
                    BAcc += (double)Src[KIndex + 0] * Kernel[Kdx];
                    Kdx++;
                }
            }
            AAcc = AAcc * Factor + Bias;
            AAcc = min(max(AAcc, 0), 255);
            RAcc = RAcc * Factor + Bias;
            RAcc = min(max(RAcc, 0), 255);
            GAcc = GAcc * Factor + Bias;
            GAcc = min(max(GAcc, 0), 255);
            BAcc = BAcc * Factor + Bias;
            BAcc = min(max(BAcc, 0), 255);
            if (UseAlpha)
                Dest[Index + 3] = (BYTE)AAcc;
            else
                Dest[Index + 3] = Src[Index + 3];
            if (UseRed)
                Dest[Index + 2] = (BYTE)RAcc;
            else
                Dest[Index + 2] = Src[Index + 2];
            if (UseGreen)
                Dest[Index + 1] = (BYTE)GAcc;
            else
                Dest[Index + 1] = Src[Index + 1];
            if (UseBlue)
                Dest[Index + 0] = (BYTE)BAcc;
            else
                Dest[Index + 0] = Src[Index + 0];
        }
    }
    return Success;
}

/// <summary>
/// Modify <paramref name="Source"/> with the supplied kernel and return the results in <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Pointer to the source image. Not modified by this function.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Where the modified image will be written.</param>
/// <param name="PackedBGPixel">Pixel used to clear the destination buffer.</param>
/// <param name="Kernel">The kernel to execute against <paramref name="Source"/>. Pointer to an array of doubles (but treated as a matrix).</param>
/// <param name="KernelX">Width of the kernel's matrix.</param>
/// <param name="KernelY">Height of the kernel's matrix.</param>
/// <param name="Bias">Matrix bias.</param>
/// <param name="Factor">Matrix factor.</param>
/// <param name="SkipTransparentPixels">If TRUE, transparent pixels will not be convolved.</param>
/// <param name="UseLuminance">If TRUE, convolution will depend on the original pixel's luminance.</param>
/// <param name="Luminance">The luminance threshold that determines if convolution will occur on a given pixel, if <paramref name="UseLuminance"/> is TRUE.</param>
/// <returns>Value indicating result of operation.</returns>
int ConvolveWithKernel3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, UINT32 PackedBGPixel,
    void *KernelMatrix, int KernelX, int KernelY, double Bias, double Factor, BOOL SkipTransparentPixels, BOOL IncludeTransparentPixels,
    BOOL UseLuminance, double Luminance)
{
    return MasterConvolveWithKernel(Source, Width, Height, Stride, Destination, PackedBGPixel, KernelMatrix, KernelX, KernelY, Bias, Factor,
        FALSE, TRUE, TRUE, TRUE, SkipTransparentPixels, IncludeTransparentPixels, UseLuminance, Luminance);
}

/// <summary>
/// Modify <paramref name="Source"/> with the supplied kernel and return the results in <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Pointer to the source image. Not modified by this function.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Where the modified image will be written.</param>
/// <param name="PackedBGPixel">Pixel used to clear the destination buffer.</param>
/// <param name="Kernel">The kernel to execute against <paramref name="Source"/>. Pointer to an array of doubles (but treated as a matrix).</param>
/// <param name="KernelX">Width of the kernel's matrix.</param>
/// <param name="KernelY">Height of the kernel's matrix.</param>
/// <param name="Bias">Matrix bias.</param>
/// <param name="Factor">Matrix factor.</param>
/// <returns>Value indicating result of operation.</returns>
int ConvolveWithKernel2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, UINT32 PackedBGPixel,
    void *KernelMatrix, int KernelX, int KernelY, double Bias, double Factor)
{
    return MasterConvolveWithKernel(Source, Width, Height, Stride, Destination, PackedBGPixel, KernelMatrix, KernelX, KernelY, Bias, Factor,
        FALSE, TRUE, TRUE, TRUE, FALSE, FALSE, FALSE, 0.0);
}

