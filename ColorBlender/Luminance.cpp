#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int InvertLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, BOOL UseThreshold, BYTE LuminanceThreshold)
{
    if (Destination == NULL)
        return NullPointer;
    if (Source == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            /*
            double Luminance = GetPixelLuminance(R, G, B);
            SetPixelLuminance(&R, &G, &B, (1.0 - Luminance));
            */
            double H = 0.0;
            double S = 0.0;
            double L = 0.0;
            RGBtoHSL2(R, G, B, &H, &S, &L);
            BYTE WL = (BYTE)(L * 255.0);
            if (UseThreshold)
            {
                if (WL >= LuminanceThreshold)
                    L = 1.0 - L;
                //                    L = (double)((0xff - WL) / 255.0);
            }
            else
                //                L = (double)((0xff - WL) / 255.0);
                L = 1.0 - L;
            HSLtoRGB2(H, S, L, &R, &G, &B);
            Dest[Index + 3] = 0xff;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = R;
        }
    }

    return Success;
}