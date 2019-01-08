#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int Grayscale_Mean = 0;
const int Grayscale_Brightness = 1;
const int Grayscale_Perceptual = 2;
const int Grayscale_RedChannel = 3;
const int Grayscale_GreenChannel = 4;
const int Grayscale_BlueChannel = 5;
const int Grayscale_CyanChannel = 6;
const int Grayscale_MagentaChannel = 7;
const int Grayscale_YellowChannel = 8;
const int Grayscale_AlphaChannel = 9;
const int Grayscale_BT601 = 10;
const int Grayscale_BT709 = 11;
const int Grayscale_Desaturation = 12;
const int Grayscale_MaxDecomposition = 13;
const int Grayscale_MinDecomposition = 14;

/// <summary>
/// Convert an image to a type of grayscale.
/// </summary>
/// <param name="Buffer">Source image to convert.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="GrayscaleType">Determines the type of grayscale operation to execute.</param>
/// <param name="Left">The left coordinate of the region to convert.</param>
/// <param name="Top">The top coordinate of the region to convert.</param>
/// <param name="Right">The right coordinate of the region to convert.</param>
/// <param name="Bottom">The bottom coordinate of the region to convert.</param>
/// <param name="CopyOutOfRegion">If TRUE, pixels out of the region to convert are the same as in the source image.</param>
/// <param name="PackedOut">If <paramref name="CopyOutOfRegion"/> is FALSE, this value will be used for pixels out of the region.</param>
/// <returns>Value indicating operational success.</returns>
int BufferGrayscaleRegion(void *Buffer, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int GrayscaleType, __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    BOOL CopyOutOfRegion, UINT32 PackedOut)
{
    if (Buffer == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (Left < 0)
        return InvalidOperation;
    if (Right >= Width)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= Height)
        return InvalidOperation;

    BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
    BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
    BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
    BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Buffer;

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
            BYTE GA = A;
            BYTE GR = 0x0;
            BYTE GG = 0x0;
            BYTE GB = 0x0;

            switch (GrayscaleType)
            {
                case Grayscale_Mean:
                {
                    double gm = ((double)R + (double)G + (double)B) / 3.0;
                    GR = (BYTE)gm;
                    GG = (BYTE)gm;
                    GB = (BYTE)gm;
                }
                break;

                case Grayscale_Perceptual:
                {
                    BYTE FinalP = (BYTE)((double)R * 0.30) + (BYTE)((double)G * 0.59) + (BYTE)((double)B * 0.11);
                    GR = FinalP;
                    GG = FinalP;
                    GB = FinalP;
                }
                break;

                case Grayscale_BT601:
                {
                    BYTE Final6 = (BYTE)((double)R * 0.099) + (BYTE)((double)G * 0.587) + (BYTE)((double)B * 0.114);
                    GR = Final6;
                    GG = Final6;
                    GB = Final6;
                }
                break;

                case Grayscale_BT709:
                {
                    BYTE Final7 = (BYTE)((double)R * 0.2126) + (BYTE)((double)G * 0.7152) + (BYTE)((double)B * 0.0722);
                    GR = Final7;
                    GG = Final7;
                    GB = Final7;
                }
                break;

                case Grayscale_Brightness:
                {
                    double GrayLum = ColorLuminance(R, G, B);
                    BYTE FinalLum = (BYTE)(255.0 * GrayLum);
                    GR = FinalLum;
                    GG = FinalLum;
                    GB = FinalLum;
                }
                break;

                case Grayscale_RedChannel:
                    GR = R;
                    GG = R;
                    GB = R;
                    break;

                case Grayscale_GreenChannel:
                    GR = G;
                    GG = G;
                    GB = G;
                    break;

                case Grayscale_BlueChannel:
                    GR = B;
                    GG = B;
                    GB = B;
                    break;

                case Grayscale_CyanChannel:
                {
                    double CyanVal = (double)G + (double)B;
                    BYTE FinalCyan = (BYTE)(CyanVal / 2.0);
                    GR = FinalCyan;
                    GG = FinalCyan;
                    GB = FinalCyan;
                }
                break;

                case Grayscale_MagentaChannel:
                {
                    double MagentaVal = (double)R + (double)B;
                    BYTE FinalMagenta = (BYTE)(MagentaVal / 2.0);
                    GR = FinalMagenta;
                    GG = FinalMagenta;
                    GB = FinalMagenta;
                }
                break;

                case Grayscale_YellowChannel:
                {
                    double YellowVal = (double)R + (double)G;
                    BYTE FinalYellow = (BYTE)(YellowVal / 2.0);
                    GR = FinalYellow;
                    GG = FinalYellow;
                    GB = FinalYellow;
                }
                break;

                case Grayscale_AlphaChannel:
                    GR = A;
                    GG = A;
                    GB = A;
                    break;

                case Grayscale_Desaturation:
                {
                    BYTE Biggest = max(R, max(G, B));
                    BYTE Smallest = min(R, min(G, B));
                    BYTE FinalDesat = (BYTE)(((double)Biggest + (double)Smallest) / 2);
                    GR = FinalDesat;
                    GG = FinalDesat;
                    GB = FinalDesat;
                }
                break;

                case Grayscale_MaxDecomposition:
                {
                    BYTE FinalMaxDecomp = max(R, max(G, B));
                    GR = FinalMaxDecomp;
                    GG = FinalMaxDecomp;
                    GB = FinalMaxDecomp;
                }
                break;

                case Grayscale_MinDecomposition:
                {
                    BYTE FinalMinDecomp = min(R, min(G, B));
                    GR = FinalMinDecomp;
                    GG = FinalMinDecomp;
                    GB = FinalMinDecomp;
                }
                break;

                default:
                    return InvalidOperation;
            }

            if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
            {
                Dest[Index + 3] = GA;
                Dest[Index + 2] = GR;
                Dest[Index + 1] = GG;
                Dest[Index + 0] = GB;
            }
            else
            {
                if (CopyOutOfRegion)
                {
                    Dest[Index + 3] = Src[Index + 3];
                    Dest[Index + 2] = Src[Index + 2];
                    Dest[Index + 1] = Src[Index + 1];
                    Dest[Index + 0] = Src[Index + 0];
                }
                else
                {
                    Dest[Index + 3] = NonOpA;
                    Dest[Index + 2] = NonOpR;
                    Dest[Index + 1] = NonOpG;
                    Dest[Index + 0] = NonOpB;
                }
            }
        }
    }

    return Success;
}

/// <summary>
/// Convert the source into an image with the specified number of channel levels.
/// </summary>
/// <param name="Buffer">Source image to convert.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="LevelCount">Number of channel levels.</param>
/// <param name="Left">The left coordinate of the region to convert.</param>
/// <param name="Top">The top coordinate of the region to convert.</param>
/// <param name="Right">The right coordinate of the region to convert.</param>
/// <param name="Bottom">The bottom coordinate of the region to convert.</param>
/// <param name="CopyOutOfRegion">If TRUE, pixels out of the region to convert are the same as in the source image.</param>
/// <param name="PackedOut">If <paramref name="CopyOutOfRegion"/> is FALSE, this value will be used for pixels out of the region.</param>
/// <returns>Value indication operational results.</returns>
int ColorLevelsRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (LevelCount < 2)
        return InvalidOperation;
    if (LevelCount > 255)
        return InvalidOperation;

    if (Left < 0)
        return InvalidOperation;
    if (Right >= Width)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= Height)
        return InvalidOperation;

    BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
    BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
    BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
    BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    double Factor = 255.0 / (double)(LevelCount - 1);

    BYTE Grays[256];
    for (int i = 0; i < 256; i++)
    {
        double f = floor((double)i / Factor);
        BYTE M = (BYTE)(f * Factor);
        Grays[i] = M;
    }

    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = Left; Column <= Right; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = Src[Index + 3];

            BYTE R = Grays[Src[Index + 2]];
            BYTE G = Grays[Src[Index + 1]];
            BYTE B = Grays[Src[Index + 0]];

            if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
            {
                Dest[Index + 2] = R;
                Dest[Index + 1] = G;
                Dest[Index + 0] = B;
            }
            else
            {
                if (CopyOutOfRegion)
                {
                    Dest[Index + 3] = Src[Index + 3];
                    Dest[Index + 2] = Src[Index + 2];
                    Dest[Index + 1] = Src[Index + 1];
                    Dest[Index + 0] = Src[Index + 0];
                }
                else
                {
                    Dest[Index + 3] = NonOpA;
                    Dest[Index + 2] = NonOpR;
                    Dest[Index + 1] = NonOpG;
                    Dest[Index + 0] = NonOpB;
                }
            }
        }
    }

    return Success;
}

/// <summary>
/// Convert the source into an image with the specified number of channel levels.
/// </summary>
/// <param name="Buffer">Source image to convert.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="LevelCount">Number of channel levels.</param>
/// <returns>Value indication operational results.</returns>
int ColorLevels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount)
{
    return ColorLevelsRegion(Source, Width, Height, Stride, Destination, LevelCount, 0, 0, Width - 1, Height - 1, FALSE, 0x0);
}

/// <summary>
/// Convert an image to a type of grayscale.
/// </summary>
/// <param name="Buffer">Source image to convert.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="GrayscaleType">Determines the type of grayscale operation to execute.</param>
/// <returns>Value indicating operational success.</returns>
int BufferGrayscale(void *Buffer, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int GrayscaleType)
{
    return BufferGrayscaleRegion(Buffer, Width, Height, Stride, Destination, GrayscaleType,
        0, 0, Width - 1, Height - 1, TRUE, 0x0);
}

/// <summary>
/// Convert the source into a grayscale image with the specified number of grays.
/// </summary>
/// <param name="Buffer">Source image to convert.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="LevelCount">Number of gray levels.</param>
/// <param name="Left">The left coordinate of the region to convert.</param>
/// <param name="Top">The top coordinate of the region to convert.</param>
/// <param name="Right">The right coordinate of the region to convert.</param>
/// <param name="Bottom">The bottom coordinate of the region to convert.</param>
/// <param name="CopyOutOfRegion">If TRUE, pixels out of the region to convert are the same as in the source image.</param>
/// <param name="PackedOut">If <paramref name="CopyOutOfRegion"/> is FALSE, this value will be used for pixels out of the region.</param>
/// <returns>Value indication operational results.</returns>
int GrayLevelsRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (LevelCount < 2)
        return InvalidOperation;
    if (LevelCount > 255)
        return InvalidOperation;

    if (Left < 0)
        return InvalidOperation;
    if (Right >= Width)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= Height)
        return InvalidOperation;

    BYTE NonOpA = (PackedOut & 0xff000000) >> 24;
    BYTE NonOpR = (PackedOut & 0x00ff0000) >> 16;
    BYTE NonOpG = (PackedOut & 0x0000ff00) >> 8;
    BYTE NonOpB = (PackedOut & 0x000000ff) >> 0;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    double Factor = 255.0 / (double)(LevelCount - 1);

    BYTE Grays[256];
    for (int i = 0; i < 256; i++)
    {
        double f = floor((double)i / Factor);
        BYTE M = (BYTE)(f * Factor);
        Grays[i] = M;
    }

    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = Left; Column <= Right; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = Src[Index + 3];

            BYTE MeanGray = (Src[Index + 2] + Src[Index + 1] + Src[Index + 0]) / 3;
            BYTE GrayVal = Grays[MeanGray];

            if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
            {
                Dest[Index + 2] = GrayVal;
                Dest[Index + 1] = GrayVal;
                Dest[Index + 0] = GrayVal;
            }
            else
            {
                if (CopyOutOfRegion)
                {
                    Dest[Index + 3] = Src[Index + 3];
                    Dest[Index + 2] = Src[Index + 2];
                    Dest[Index + 1] = Src[Index + 1];
                    Dest[Index + 0] = Src[Index + 0];
                }
                else
                {
                    Dest[Index + 3] = NonOpA;
                    Dest[Index + 2] = NonOpR;
                    Dest[Index + 1] = NonOpG;
                    Dest[Index + 0] = NonOpB;
                }
            }
        }
    }

    return Success;
}

/// <summary>
/// Convert the source into a grayscale image with the specified number of grays.
/// </summary>
/// <param name="Buffer">Source image to convert.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="LevelCount">Number of gray levels.</param>
/// <returns>Value indication operational results.</returns>
int GrayLevels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int LevelCount)
{
    return GrayLevelsRegion(Source, Width, Height, Stride, Destination, LevelCount, 0, 0, Width - 1, Height - 1, FALSE, 0x0);
}