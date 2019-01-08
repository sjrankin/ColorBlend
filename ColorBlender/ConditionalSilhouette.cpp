#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int SilhouetteIf(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL UseHue,
    double HueThreshold, double HueRange, BOOL UseSaturation, double SaturationThreshold, double SaturationRange,
    BOOL UseLuminance, double LuminanceThreshold,
    BOOL LuminanceGreaterThan, UINT32 SilhouetteColor)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (!UseHue && !UseSaturation && !UseLuminance)
        return NoActionTaken;

    if (UseHue)
    {
        if (HueThreshold < 0.0)
            HueThreshold = 0.0;
        if (HueThreshold > 360.0)
            HueThreshold = (double)((int)HueThreshold % 360);
        if (HueRange < 0.0)
            HueRange = 0.0;
        if (HueRange > 360.0)
            HueRange = 360.0;
    }
    if (UseSaturation)
    {
        if (SaturationThreshold < 0.0)
            SaturationThreshold = 0.0;
        if (SaturationThreshold > 1.0)
            SaturationThreshold = 1.0;
        if (SaturationRange < 0.0)
            SaturationRange = 0.0;
        if (SaturationRange > 1.0)
            SaturationRange = 1.0;
    }
    if (UseLuminance)
    {
        if (LuminanceThreshold < 0.0)
            LuminanceThreshold = 0.0;
        if (LuminanceThreshold > 1.0)
            LuminanceThreshold = 0.0;
    }
    BYTE SilhouetteA = (SilhouetteColor & 0xff000000) >> 24;
    BYTE SilhouetteR = (SilhouetteColor & 0x00ff0000) >> 16;
    BYTE SilhouetteG = (SilhouetteColor & 0x0000ff00) >> 8;
    BYTE SilhouetteB = (SilhouetteColor & 0x000000ff) >> 0;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

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
            double H = 0.0;
            double S = 0.0;
            double L = 0.0;
            RGBtoHSL2(R, G, B, &H, &S, &L);
            BOOL ApplySilhouetteColor = FALSE;
            if (UseHue)
            {
                double HalfHueRange = HueRange / 2.0;
                if (H >= HueThreshold - HalfHueRange && H <= HueThreshold + HalfHueRange)
                    ApplySilhouetteColor = TRUE;
            }
            if (UseSaturation)
            {
                double HalfRange = SaturationRange / 2.0;
                if (S >= SaturationThreshold - HalfRange && S <= SaturationThreshold + HalfRange)
                    ApplySilhouetteColor = TRUE;
            }
            if (UseLuminance)
            {
                if (LuminanceGreaterThan)
                {
                    if (L > LuminanceThreshold)
                        ApplySilhouetteColor = TRUE;
                }
                else
                {
                    if (L < LuminanceThreshold)
                        ApplySilhouetteColor = TRUE;
                }
            }
            if (ApplySilhouetteColor)
            {
                Dest[Index + 3] = SilhouetteA;
                Dest[Index + 2] = SilhouetteR;
                Dest[Index + 1] = SilhouetteG;
                Dest[Index + 0] = SilhouetteB;
            }
            else
            {
                Dest[Index + 3] = A;
                Dest[Index + 2] = R;
                Dest[Index + 1] = G;
                Dest[Index + 0] = B;
            }
        }
    }

    return Success;
}