#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Convert the image pointed to by <paramref name="Source"/> from an implied RGB color space to the color space
/// specified by <paramref name="ToColorSpace"/>.
/// </summary>
/// <param name="Source">Source for the final converted destination.</param>
/// <param name="Width">Width of the Source and Destination.</param>
/// <param name="Height">Height of the Source and Destination.</param>
/// <param name="Stride">Stride of the Source and Destination.</param>
/// <param name="Destination">Where the converted image will be placed.</param>
/// <param name="ToColorSpace">Determines the color space to convert to.</param>
/// <returns>Value indicating operational success.</returns>
int ConvertColorSpace(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE ToColorSpace)
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
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            double ANormal = (double)A / 255.0;
            double RNormal = (double)R / 255.0;
            double GNormal = (double)G / 255.0;
            double BNormal = (double)B / 255.0;
            BYTE FinalA = A;
            BYTE FinalR = 0x0;
            BYTE FinalG = 0x0;
            BYTE FinalB = 0x0;

            switch (ToColorSpace)
            {
                case ToHSL:
                {
                    double H, S, L = 0.0;
                    RGBtoHSL(RNormal, GNormal, BNormal, &H, &S, &L);
                    FinalR = (BYTE)(H * 255.0);
                    FinalG = (BYTE)(S * 255.0);
                    FinalB = (BYTE)(L * 255.0);
                }
                break;

                case ToHSV:
                {
                    double H, S, V = 0.0;
                    RGBtoHSL(RNormal, GNormal, BNormal, &H, &S, &V);
                    FinalR = (BYTE)(H * 255.0);
                    FinalG = (BYTE)(S * 255.0);
                    FinalB = (BYTE)(V * 255.0);
                }
                break;

                case ToLAB:
                {
                    double L, Al, Bl = 0.0;
                    RGBtoCIELAB(RNormal, GNormal, BNormal, &L, &Al, &Bl);
                    FinalR = (BYTE)(L * 255.0);
                    FinalG = (BYTE)(Al * 255.0);
                    FinalB = (BYTE)(Bl * 255.0);
                }
                break;

                case ToXYZ:
                {
                    double X, Y, Z = 0.0;
                    RGBtoXYZ(RNormal, GNormal, BNormal, &X, &Y, &Z);
                    FinalR = (BYTE)(X * 255.0);
                    FinalG = (BYTE)(Y * 255.0);
                    FinalB = (BYTE)(Z * 255.0);
                }
                break;

                case ToCMY:
                {
                    double C, M, Y = 0.0;
                    RGBtoCMY(RNormal, GNormal, BNormal, &C, &M, &Y);
                    FinalR = (BYTE)(C * 255.0);
                    FinalG = (BYTE)(M * 255.0);
                    FinalB = (BYTE)(Y * 255.0);
                }
                break;

                case ToCMYK:
                    break;

                case ToYCbCr:
                {
                    double Y, Cb, Cr = 0.0;
                    RGBtoYCbCr(RNormal, GNormal, BNormal, &Y, &Cb, &Cr);
                    FinalR = (BYTE)(Y * 255.0);
                    FinalG = (BYTE)(Cb * 255.0);
                    FinalB = (BYTE)(Cr * 255.0);
                }
                break;

                case ToYUV:
                {
                    double Y, U, V = 0.0;
                    RGBtoYUV(RNormal, GNormal, BNormal, &Y, &U, &V);
                    FinalR = (BYTE)(Y * 255.0);
                    FinalG = (BYTE)(U * 255.0);
                    FinalB = (BYTE)(V * 255.0);
                }
                break;

                case ToYIQ:
                {
                    double Y, I, Q = 0.0;
                    RGBtoYIQ(RNormal, GNormal, BNormal, &Y, &I, &Q);
                    FinalR = (BYTE)(Y * 255.0);
                    FinalG = (BYTE)(I * 255.0);
                    FinalB = (BYTE)(Q * 255.0);
                }
                break;

                default:
                    return InvalidOperation;
            }

            Dest[Index + 3] = FinalA;
            Dest[Index + 2] = FinalR;
            Dest[Index + 1] = FinalG;
            Dest[Index + 0] = FinalB;
        }
    }
    return Success;
}