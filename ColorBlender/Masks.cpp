#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Solarizes the only the alpha channel.
/// </summary>
/// <param nanme="Source">Pointer to the source data buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="Luminance">Determines which pixels will have their alpha channel solarized.</param>
/// <returns>Value indication operational results.</returns>
int AlphaSolarize(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Luminance)
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
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 0] = Src[Index + 0];
            double PixelLuminance = ColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
            if (PixelLuminance < Luminance)
                Dest[Index + 3] = 0;
            else
                Dest[Index + 3] = ~Dest[Index + 3];
        }
    }

    return Success;
}

/// <summary>
/// Sets the alpha channel value of a pixel to either 0x0 or 0xff depending if it original value is less than or
/// greater than <paramref name="AlphaLevel"/>.
/// </summary>
/// <param nanme="Source">Pointer to the source data buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="AlphaLevel">Alpha channels greater than or equal to this value will be set to 0xff, and if not, to 0x0.</param>
/// <returns>Value indication operational results.</returns>
int AlphaMaskImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE AlphaLevel)
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
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 0] = Src[Index + 0];
            if (Dest[Index + 3] >= AlphaLevel)
                Dest[Index + 3] = 0xff;
            else
                Dest[Index + 3] = 0x0;
        }
    }

    return Success;
}

/// <summary>
/// Sets the alpha value to <paramref name="AlphaValue"/> if the R, G, and B channels fall into the range specified
/// by <paramref name="LowMaskColor"/> and <paramref name="HighMaskColor"/>.
/// </summary>
/// <param nanme="Source">Pointer to the source data buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="LowMaskColor">Low color mask value.</param>
/// <param name="HighMaskColor">High color mask value.</param>
/// <param name="AlphaMask">The value used to set the alpha channel if the pixel falls into the specified range.</param>
/// <returns>Value indication operational results.</returns>
int MaskByColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    UINT32 LowMaskColor, UINT32 HighMaskColor, BYTE AlphaValue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    BYTE LowMaskR = (LowMaskColor & 0x00ff0000) >> 16;
    BYTE LowMaskG = (LowMaskColor & 0x0000ff00) >> 8;
    BYTE LowMaskB = (LowMaskColor & 0x000000ff) >> 0;
    BYTE HighMaskR = (HighMaskColor & 0x00ff0000) >> 16;
    BYTE HighMaskG = (HighMaskColor & 0x0000ff00) >> 8;
    BYTE HighMaskB = (HighMaskColor & 0x000000ff) >> 0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
            if (
                (R >= LowMaskR && R <= HighMaskR) &&
                (G >= LowMaskG && G <= HighMaskG) &&
                (B >= LowMaskB && B <= HighMaskB)
                )
            {
                Dest[Index + 3] = AlphaValue;
            }
            else
            {
                Dest[Index + 3] = Src[Index + 3];
            }
        }
    }

    return Success;
}

/// <summary>
/// Sets the alpha value of a given pixel proportionally to the luminance of the pixel.
/// </summary>
/// <param nanme="Source">Pointer to the source data buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <returns>Value indication operational results.</returns>
int AlphaFromLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL Invert)
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
            BYTE sR = Src[Index + 2];
            double nR = (double)sR / 255.0;
            BYTE sG = Src[Index + 1];
            double nG = (double)sG / 255.0;
            BYTE sB = Src[Index + 0];
            double nB = (double)sB / 255.0;
            double PixelLum = (0.2126 * nR) + (0.7152 * nG) + (0.0722 * nB);
            if (Invert)
                PixelLum = 1.0 - PixelLum;
            PixelLum *= 255.0;
            BYTE FinalAlpha = min(PixelLum, 0xff);
            Dest[Index + 3] = FinalAlpha;
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 0] = Src[Index + 0];
        }
    }

    return Success;
}

/// <summary>
/// Unconditionally sets all alpha channels to <paramref name="NewAlpha"/>.
/// </summary>
/// <param nanme="Source">Pointer to the source data buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="NewAlpha">The new alpha value for all pixels.</param>
/// <returns>Value indication operational results.</returns>
int SetAlphaChannel(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BYTE NewAlpha)
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
            Dest[Index + 3] = NewAlpha;
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 0] = Src[Index + 0];
        }
    }

    return Success;
}

/// <summary>
/// Unconditionally sets all alpha channels to <paramref name="NewAlpha"/> in an in-place buffer - e.g., no destination buffer is used.
/// </summary>
/// <param nanme="Buffer">Pointer to the in-place buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="NewAlpha">The new alpha value for all pixels.</param>
/// <returns>Value indication operational results.</returns>
int SetAlphaChannelInPlace(void *Buffer, __int32 Width, __int32 Height, __int32 Stride, BYTE NewAlpha)
{
    if (Buffer == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Buf = (BYTE *)Buffer;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Buf[Index + 3] = NewAlpha;
        }
    }

    return Success;
}

/// <summary>
/// Set the pixel in the destination buffer to either the source pixel or the masked pixel value depending on the luminance
/// of the pixel in the source.
/// </summary>
/// <param nanme="Source">Pointer to the source data buffer.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="LuminanceThreshold">Determines which pixel will be written to the destination.</param>
/// <param name="Invert">Determines how pixel selection is done.</param>
/// <param name="MaskPixel">The value of the masked pixel.</param>
/// <returns>Value indication operational results.</returns>
int ConditionalAlphaFromLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double LuminanceThreshold, BOOL Invert, UINT32 MaskPixel)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE MaskA = (MaskPixel & 0xff000000) >> 24;
    BYTE MaskR = (MaskPixel & 0x00ff0000) >> 16;
    BYTE MaskG = (MaskPixel & 0x0000ff00) >> 8;
    BYTE MaskB = (MaskPixel & 0x000000ff) >> 0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            double PLuminance = ColorLuminance(R, G, B);
            BOOL DoMask = FALSE;
            if (Invert)
            {
                if (PLuminance <= LuminanceThreshold)
                    DoMask = TRUE;
            }
            else
            {
                if (PLuminance >= LuminanceThreshold)
                    DoMask = TRUE;
            }
            if (DoMask)
            {
                Dest[Index + 3] = MaskA;
                Dest[Index + 2] = MaskR;
                Dest[Index + 1] = MaskG;
                Dest[Index + 0] = MaskB;
            }
            else
            {
                Dest[Index + 3] = Src[Index + 3];
                Dest[Index + 2] = Src[Index + 2];
                Dest[Index + 1] = Src[Index + 1];
                Dest[Index + 0] = Src[Index + 0];
            }
        }
    }
    return Success;
}

/// <summary>
/// Conditionally applies alpha levels from <paramref name="LuminanceLayer"/> to <paramref name="Base"/> and sets pixels that are
/// not selected to <paramref name="MaskedPixel"/>.
/// </summary>
/// <param name="Base">Pointer to the source data buffer.</param>
/// <param name="LuminanceLayer">Image used for luminance information.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="LuminanceThreshold">
/// Determines which pixels will have their alpha channel set to the alpha channel in <paramref name="Base"/>.
/// </param>
/// <param name="MaskedPixel">The color to use for regions that are masked out.</param>
/// <returns>Value indication operational results.</returns>
int MaskImageFromImageLuminance(void *Base, void *LuminanceLayer, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double LuminanceThreshold, UINT32 MaskedPixel)
{
    if (Base == NULL)
        return NullPointer;
    if (LuminanceLayer == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE *Dest = (BYTE *)Destination;
    BYTE *Bse = (BYTE *)Base;
    BYTE *LL = (BYTE *)LuminanceLayer;
    int PixelSize = 4;

    BYTE mA = (MaskedPixel & 0xff000000) >> 24;
    BYTE mR = (MaskedPixel & 0x00ff0000) >> 16;
    BYTE mG = (MaskedPixel & 0x0000ff00) >> 8;
    BYTE mB = (MaskedPixel & 0x000000ff) >> 0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            double LumLevelLum = ColorLuminance(LL[Index + 2], LL[Index + 1], LL[Index + 1]);
            if (LumLevelLum < LuminanceThreshold)
            {
                Dest[Index + 3] = Bse[Index + 3];
                Dest[Index + 2] = Bse[Index + 2];
                Dest[Index + 1] = Bse[Index + 1];
                Dest[Index + 0] = Bse[Index + 0];
            }
            else
            {
                Dest[Index + 3] = mA;
                Dest[Index + 2] = mR;
                Dest[Index + 1] = mG;
                Dest[Index + 0] = mB;
            }
        }
    }

    return Success;
}

/// <summary>
/// Unconditionally applies alpha channels from <paramref name="AlphaSource"/> to <paramref name="Base"/>.
/// </summary>
/// <param nanme="Base">Pointer to the source data buffer.</param>
/// <param name="AlphaSoource">Image that will be used for alpha sources. Must be same dimensions as <paramref name="Base"/>.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Stride">Stride of the source and destination.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <returns>Value indication operational results.</returns>
int ApplyAlphaFromImage(void *Base, void *AlphaSource, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
    if (Base == NULL)
        return NullPointer;
    if (AlphaSource == NULL) 
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE *Dest = (BYTE *)Destination;
    BYTE *Bse = (BYTE *)Base;
    BYTE *ASrc = (BYTE *)AlphaSource;
    int PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = ASrc[Index + 3];
            Dest[Index + 2] = Bse[Index + 2];
            Dest[Index + 1] = Bse[Index + 1];
            Dest[Index + 0] = Bse[Index + 0];
        }
    }

    return Success;
}

/// <summary>
/// Applies an action to a row or column depending on the specified frequencies.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <returns>Value indicating operational results.</returns>
int ActionByFrequency(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, FrequencyActionBlock FrequencyAction)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    if (FrequencyAction.HorizontalFrequency < 1)
        return InvalidOperation;
    if (FrequencyAction.VerticalFrequency < 1)
        return InvalidOperation;
    if (FrequencyAction.Action == NoAction)
        return NoActionTaken;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;

    BYTE sA = (BYTE)((FrequencyAction.NewColor & 0xff000000) >> 24);
    BYTE sR = (BYTE)((FrequencyAction.NewColor & 0x00ff0000) >> 16);
    BYTE sG = (BYTE)((FrequencyAction.NewColor & 0x0000ff00) >> 8);
    BYTE sB = (BYTE)((FrequencyAction.NewColor & 0x000000ff) >> 0);

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            BOOL PerformAction = FALSE;
            if (Column % FrequencyAction.HorizontalFrequency == 0)
                PerformAction = TRUE;
            if (Row % FrequencyAction.VerticalFrequency == 0)
                PerformAction = TRUE;

            int Index = RowOffset + (Column * PixelSize);
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];

            if (PerformAction)
            {
                switch (FrequencyAction.Action)
                {
                case SetTransparencyAction:
                    A = FrequencyAction.NewAlpha;
                    break;

                case InvertAction:
                    if (FrequencyAction.IncludeAlpha)
                        A = ~A;
                    R = ~R;
                    G = ~G;
                    B = ~B;
                    break;

                case SetLuminanceAction:
                    SetPixelLuminance(&R, &G, &B, FrequencyAction.NewLuminance);
                    break;

                case SetColorAction:
                    switch (FrequencyAction.ColorAlphaAction)
                    {
                    case UseColorAlpha:
                        A = sA;
                        break;

                    case UseSourceAlpha:
                        //Already assigned.
                        break;

                    case UseLuminanceProportionalAlpha:
                    {
                        double Luminance = GetPixelLuminance(R, G, B);
                        A = Luminance / 1.0;
                    }
                    break;

                    default:
                        return InvalidOperation;
                    }
                    R = sR;
                    G = sG;
                    B = sB;
                    break;

                case ProportionalTransparentAction:
                {
                    double Luminance = GetPixelLuminance(R, G, B);
                    A = Luminance / 1.0;
                }
                break;

                default:
                    return InvalidOperation;
                }
            }

            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return Success;
}