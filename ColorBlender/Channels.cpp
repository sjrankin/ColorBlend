#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Rotates the value of pixels in the buffer. Values are constrained to individual pixels.
/// </summary>
/// <param name="Target">Target buffer that will have its pixels rotated.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
/// <param name="ShiftBy">How the pixel bits are shifted.</param>
/// <returns>Value indicating sucess.</returns>
int ChannelShift(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, int ShiftBy)
{
    if (ShiftBy == 0)
        return NoActionTaken;
    if (Target == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Target;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            UINT32 FullPixel = (Buffer[Index + 3] << 24) | (Buffer[Index + 2] << 16) | (Buffer[Index + 1] << 8) | (Buffer[Index + 0] << 0);
            if (ShiftBy < 0)
                FullPixel = RotateLeft32(FullPixel, ShiftBy);
            else
                FullPixel = RotateRight32(FullPixel, ShiftBy);
            Buffer[Index + 3] = (FullPixel & 0xff000000) >> 24;
            Buffer[Index + 2] = (FullPixel & 0x00ff0000) >> 16;
            Buffer[Index + 1] = (FullPixel & 0x0000ff00) >> 8;
            Buffer[Index + 0] = (FullPixel & 0x000000ff) >> 0;
        }
    }
    return Success;
}

inline BYTE BMask(int Value)
{
    return (BYTE)pow(2.0, Value) - 1;
}

/// <summary>
/// Migrate color data from pixel to pixel.
/// </summary>
/// <param name="Target">Target buffer that will have its pixels rotated.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
/// <param name="MigrateBy">How the pixel bits are shifted.</param>
/// <param name="MigrateAlpha">If true, alpha bits will be migrated.</param>
/// <param name="MigrateRed">If true, red bits will be migrated.</param>
/// <param name="MigrateGreen">If true, blue bits will be migrated.</param>
/// <param name="MigrateBlue">If true, green bits will be migrated.</param>
/// <returns>Value indicating sucess.</returns>
int ChannelMigrate(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, int MigrateBy,
    BOOL MigrateAlpha, BOOL MigrateRed, BOOL MigrateGreen, BOOL MigrateBlue)
{
    if (MigrateBy == 0)
        return NoActionTaken;
    if (Target == NULL)
        return NullPointer;
    if (!MigrateAlpha && !MigrateRed && !MigrateGreen && !MigrateBlue)
        return NoActionTaken;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Target;
    UINT32 BufferSize = BufferWidth * BufferHeight;
    UINT32 PixelIndex = 0;

    BYTE As = 0;
    BYTE Rs = 0;
    BYTE Gs = 0;
    BYTE Bs = 0;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            PixelIndex++;
            int Index = RowOffset + (Column * PixelSize);
            if (MigrateAlpha)
            {
                BYTE A = Buffer[Index + 3];
                BYTE Ar = A & BMask(MigrateBy);
                BYTE Ad = A >> MigrateBy;
                Ad |= As;
                Buffer[Index + 3] = Ad;
                As = Ar;
            }
            if (MigrateRed)
            {
                BYTE R = Buffer[Index + 2];
                BYTE Rr = R & BMask(MigrateBy);
                BYTE Rd = R >> MigrateBy;
                Rd |= Rs;
                Buffer[Index + 2] = Rd;
                Rs = Rr;
            }
            if (MigrateGreen)
            {
                BYTE G = Buffer[Index + 1];
                BYTE Gr = G & BMask(MigrateBy);
                BYTE Gd = G >> MigrateBy;
                Gd |= Gs;
                Buffer[Index + 1] = Gd;
                Gs = Gr;
            }
            if (MigrateBlue)
            {
                BYTE B = Buffer[Index + 1];
                BYTE Br = B & BMask(MigrateBy);
                BYTE Bd = B >> MigrateBy;
                Bd |= Bs;
                Buffer[Index + 1] = Bd;
                Bs = Br;
            }
        }
    }

    return Success;
}

/// <summary>
/// Migrate pixels through the buffer. Atomic unit is a pixel, not a channel.
/// </summary>
/// <param name="Target">Target buffer that will have its pixels migrated.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
int PixelMigrate(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, int MigrateBy, bool IgnoreAlpha)
{
    return Success;
}

/// <summary>
/// Swap channels in a given pixel.
/// </summary>
/// <param name="Target">Target buffer that will have its pixel channels swapped.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
/// <param name="SourceIndices">Indicates the source of a sequential channel.</param>
/// <param name="DestIndices">Incidates the destination of a squential channel.</param>
/// <param name="IndexCount">Number of indices in both SourceIndices and DestIndices.</param>
/// <returns>Value indicating success.</returns>
int ChannelSwap(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *SourceIndices, void *DestIndices, int IndexCount)
{
    if (Target == NULL)
        return NullPointer;
    if (SourceIndices == NULL)
        return NullPointer;
    if (DestIndices == NULL)
        return NullPointer;
    if (IndexCount < 1)
        return NoActionTaken;
    if (IndexCount > 4)
        return BadIndex;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Target;
    int *Source = (int *)SourceIndices;
    int *Dest = (int *)DestIndices;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE *S = new BYTE[4];
            S[0] = Buffer[Index + 3];
            S[1] = Buffer[Index + 2];
            S[2] = Buffer[Index + 1];
            S[3] = Buffer[Index + 0];
            BYTE *D = new BYTE[4];
            for (int i = 0; i < IndexCount; i++)
            {
                D[Dest[i]] = S[Source[i]];
            }
            Buffer[Index + 3] = S[3];
            Buffer[Index + 2] = S[2];
            Buffer[Index + 1] = S[1];
            Buffer[Index + 0] = S[0];
        }
    }

    return Success;
}

/// <summary>
/// Swap channels in a given pixel.
/// </summary>
/// <param name="Source">Image source for the operation.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
/// <param name="SourceIndices">Indicates the source of a sequential channel.</param>
/// <param name="DestIndices">Incidates the destination of a squential channel.</param>
/// <param name="IndexCount">Number of indices in both SourceIndices and DestIndices.</param>
/// <returns>Value indicating success.</returns>
int ChannelSwap3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int SwapOrder)
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
            int Index = RowOffset + (Column * PixelSize);
            Dest[Index + 3] = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            switch (SwapOrder)
            {
            case SortRBG:
                Dest[Index + 2] = R;
                Dest[Index + 1] = B;
                Dest[Index + 0] = G;
                break;

            case SortGRB:
                Dest[Index + 2] = G;
                Dest[Index + 1] = R;
                Dest[Index + 0] = B;
                break;

            case SortGBR:
                Dest[Index + 2] = G;
                Dest[Index + 1] = B;
                Dest[Index + 0] = R;
                break;

            case SortBRG:
                Dest[Index + 2] = B;
                Dest[Index + 1] = R;
                Dest[Index + 0] = G;
                break;

            case SortBGR:
                Dest[Index + 2] = B;
                Dest[Index + 1] = G;
                Dest[Index + 0] = R;
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

const int SwapIf_Unconditional = 0;
const int SwapIf_LuminanceGTE = 1;
const int SwapIf_LuminanceLTE = 2;
const int SwapIf_R_GTE_GB = 3;
const int SwapIf_G_GTE_RB = 4;
const int SwapIf_B_GTE_RG = 5;
const int SwapIf_NotSet = 6;

/// <summary>
/// Swap channels in a given pixel.
/// </summary>
/// <param name="Source">Image source for the operation.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
/// <param name="SourceIndices">Indicates the source of a sequential channel.</param>
/// <param name="DestIndices">Incidates the destination of a squential channel.</param>
/// <param name="IndexCount">Number of indices in both SourceIndices and DestIndices.</param>
/// <returns>Value indicating success.</returns>
int ChannelSwap4(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, int SwapOrder,
    double LuminanceThreshold, int Conditional)
{
    if (Conditional == SwapIf_Unconditional || Conditional == SwapIf_NotSet)
        return ChannelSwap3(Source, Width, Height, Stride, Destination, SwapOrder);

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
            int Index = RowOffset + (Column * PixelSize);
            Dest[Index + 3] = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];

            BOOL DoSwap = false;
            double PixelLuminance = ColorLuminance(R, G, B);
            switch (Conditional)
            {
            case SwapIf_LuminanceGTE:
                if (PixelLuminance >= LuminanceThreshold)
                    DoSwap = TRUE;
                break;

            case SwapIf_LuminanceLTE:
                if (PixelLuminance <= LuminanceThreshold)
                    DoSwap = TRUE;
                break;

            case SwapIf_R_GTE_GB:
                if (R == max(R, max(G, B)))
                    DoSwap = TRUE;
                break;

            case SwapIf_G_GTE_RB:
                if (G == max(R, max(G, B)))
                    DoSwap = TRUE;
                break;

            case SwapIf_B_GTE_RG:
                if (B == max(R, max(G, B)))
                    DoSwap = TRUE;
                break;

            default:
                return InvalidOperation;
            }

            if (!DoSwap)
            {
                Dest[Index + 2] = R;
                Dest[Index + 1] = G;
                Dest[Index + 0] = B;
                continue;
            }

            switch (SwapOrder)
            {
            case SortRBG:
                Dest[Index + 2] = R;
                Dest[Index + 1] = B;
                Dest[Index + 0] = G;
                break;

            case SortGRB:
                Dest[Index + 2] = G;
                Dest[Index + 1] = R;
                Dest[Index + 0] = B;
                break;

            case SortGBR:
                Dest[Index + 2] = G;
                Dest[Index + 1] = B;
                Dest[Index + 0] = R;
                break;

            case SortBRG:
                Dest[Index + 2] = B;
                Dest[Index + 1] = R;
                Dest[Index + 0] = G;
                break;

            case SortBGR:
                Dest[Index + 2] = B;
                Dest[Index + 1] = G;
                Dest[Index + 0] = R;
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

const int ExecOpLuminance = 0;

struct ExecutionOptions
{
    int ExecOption;
    double LuminanceThreshold;
    BOOL InvertLuminanceThreshold;
};

/// <summary>
/// Swap channels with optional thresholds.
/// </summary>
/// <param name="Source">Source image for swapping data.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Strie">Stride of the source and destination.</param>
/// <param name="SwapOrder">How to swap the channels. No RGB order available as that would be silly.</param>
/// <param name="ExecOptions">Optional threshold instructions for when to swap.</param>
/// <returns>Value indicating operational success.</returns>
int ChannelSwap2(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, int SwapOrder, void *ExecOptions)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if ((SwapOrder < SortRBG) || (SwapOrder > SortBGR))
        return InvalidOperation;
    BOOL OptionsPresent = ExecOptions == NULL ? FALSE : TRUE;
    ExecutionOptions *Options = (ExecutionOptions *)ExecOptions;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = (Row * Stride);
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            BOOL DoSwap = true;
            if (OptionsPresent)
            {
                BOOL ThresholdMet = TRUE;
                double PixLuminance = 0.0;
                switch (Options->ExecOption)
                {
                case ExecOpLuminance:
                    PixLuminance = PixelLuminance(R, G, B);
                    ThresholdMet = PixLuminance >= Options->LuminanceThreshold;
                    if (Options->InvertLuminanceThreshold)
                        ThresholdMet = !ThresholdMet;
                    DoSwap = ThresholdMet;
                    break;

                default:
                    return InvalidOperation;
                }
            }

            if (DoSwap)
            {
                switch (SwapOrder)
                {
                case SortRBG:
                    Dest[Index + 3] = A;
                    Dest[Index + 2] = R;
                    Dest[Index + 1] = B;
                    Dest[Index + 0] = G;
                    break;

                case SortGRB:
                    Dest[Index + 3] = A;
                    Dest[Index + 2] = G;
                    Dest[Index + 1] = R;
                    Dest[Index + 0] = B;
                    break;

                case SortGBR:
                    Dest[Index + 3] = A;
                    Dest[Index + 2] = G;
                    Dest[Index + 1] = B;
                    Dest[Index + 0] = R;
                    break;

                case SortBRG:
                    Dest[Index + 3] = A;
                    Dest[Index + 2] = B;
                    Dest[Index + 1] = R;
                    Dest[Index + 0] = G;
                    break;

                case SortBGR:
                    Dest[Index + 3] = A;
                    Dest[Index + 2] = B;
                    Dest[Index + 1] = G;
                    Dest[Index + 0] = R;
                    break;

                default:
                    return InvalidOperation;
                }
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

/// <summary>
/// Shuffle the array.
/// </summary>
/// <param name="array">The array to shuffle.</param>
/// <param name="n">Size of <paramref name="array"/>.</param>
void shuffle(BYTE *array, size_t n)
{
    if (n > 1)
    {
        size_t i;
        for (i = 0; i < n - 1; i++)
        {
            size_t j = i + rand() / (RAND_MAX / (n - i) + 1);
            BYTE t = array[j];
            array[j] = array[i];
            array[i] = t;
        }
    }
}

/// <summary>
/// Shuffle the parameters randomly.
/// </summary>
/// <param name="Red">Red channel data.</param>
/// <param name="Green">Green channel data.</param>
/// <param name="Blue">Blue channel data.</param>
void Random3(BYTE *Red, BYTE *Green, BYTE *Blue)
{
    BYTE* Ar = new BYTE[3];
    Ar[0] = *Red;
    Ar[1] = *Green;
    Ar[2] = *Blue;
    shuffle(Ar, 3);
    *Red = Ar[0];
    *Green = Ar[1];
    *Blue = Ar[2];
    delete[] Ar;
}

/// <summary>
/// Shuffle the parameters randomly.
/// </summary>
/// <param name="Alpha">Alpha channel data.</param>
/// <param name="Red">Red channel data.</param>
/// <param name="Green">Green channel data.</param>
/// <param name="Blue">Blue channel data.</param>
void Random4(BYTE *Alpha, BYTE *Red, BYTE *Green, BYTE *Blue)
{
    BYTE* Ar = new BYTE[4];
    Ar[0] = *Alpha;
    Ar[1] = *Red;
    Ar[2] = *Green;
    Ar[3] = *Blue;
    shuffle(Ar, 4);
    *Alpha = Ar[0];
    *Red = Ar[1];
    *Green = Ar[2];
    *Blue = Ar[3];
    delete[] Ar;
}

/// <summary>
/// Swap the channels of the buffer randomly.
/// </summary>
/// <param name="Target">Target buffer that will have its pixel channels randomized.</param>
/// <param name="BufferWidth">Width of the target buffer.</param>
/// <param name="BufferHeight">Height of the target buffer.</param>
/// <param name="BufferStride">Stride of the buffer.</param>
/// <param name="Seed">Random number seed.</param>
/// <param name="IncludeAlpha">If true, the alpha channel will be included in the random swaps.</param>
int RandomChannelSwap(void *Target, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride, UINT Seed, BOOL IncludeAlpha)
{
    if (Target == NULL)
        return NullPointer;
    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Target;
    srand(Seed);

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE Alpha = Buffer[Index + 3];
            BYTE Red = Buffer[Index + 2];
            BYTE Green = Buffer[Index + 1];
            BYTE Blue = Buffer[Index + 0];
            if (IncludeAlpha)
            {
                Random4(&Alpha, &Red, &Green, &Blue);
                Buffer[Index + 3] = Alpha;
                Buffer[Index + 2] = Red;
                Buffer[Index + 1] = Green;
                Buffer[Index + 0] = Blue;
            }
            else
            {
                Random3(&Red, &Green, &Blue);
                Buffer[Index + 2] = Red;
                Buffer[Index + 1] = Green;
                Buffer[Index + 0] = Blue;
            }
        }
    }

    return Success;
}

/// <summary>
/// Select which RGB channels are shown.
/// </summary>
/// <param name="Source">Image source for the operation.</param>
/// <param name="BufferWidth">Width of the source and destination buffer.</param>
/// <param name="BufferHeight">Height of the source and destination buffer.</param>
/// <param name="BufferStride">Stride of the source and destination buffer.</param>
/// <param name="SelectRed">If TRUE, the red channel will be selected.</param>
/// <param name="SelectGreen">If TRUE, the green channel will be selected.</param>
/// <param name="SelectBlue">If TRUE, the blue channel will be selected.</param>
/// <returns>Value indicating operational status.</returns>
int SelectRGBChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SelectRed,
    BOOL SelectGreen, BOOL SelectBlue, BOOL AsGray)
{
    if (Source == NULL)
        return NullPointer;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = 0xff;
            if (SelectRed)
                Dest[Index + 2] = Src[Index + 2];
            else
                Dest[Index + 2] = 0;
            if (SelectGreen)
                Dest[Index + 1] = Src[Index + 1];
            else
                Dest[Index + 1] = 0;
            if (SelectBlue)
                Dest[Index + 0] = Src[Index + 0];
            else
                Dest[Index + 0] = 0;
            if (((int)SelectRed + (int)SelectGreen + (int)SelectBlue) > 1)
                continue;
            if (AsGray)
            {
                if (SelectRed)
                {
                    Dest[Index + 1] = Dest[Index + 2];
                    Dest[Index + 0] = Dest[Index + 2];
                }
                if (SelectGreen)
                {
                    Dest[Index + 2] = Dest[Index + 1];
                    Dest[Index + 0] = Dest[Index + 1];
                }
                if (SelectBlue)
                {
                    Dest[Index + 2] = Dest[Index + 0];
                    Dest[Index + 1] = Dest[Index + 0];
                }
            }
        }
    }

    return Success;
}

/// <summary>
/// Select which HSL channels are shown.
/// </summary>
/// <param name="Source">Image source for the operation.</param>
/// <param name="BufferWidth">Width of the source and destination buffer.</param>
/// <param name="BufferHeight">Height of the source and destination buffer.</param>
/// <param name="BufferStride">Stride of the source and destination buffer.</param>
/// <param name="SelectHue">If TRUE, the hue channel will be selected.</param>
/// <param name="SelectSaturation">If TRUE, the saturation channel will be selected.</param>
/// <param name="SelectLuminance">If TRUE, the luminance channel will be selected.</param>
/// <returns>Value indicating operational status.</returns>
int SelectHSLChannels(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SelectHue,
    BOOL SelectSaturation, BOOL SelectLuminance, bool AsGray, int ChannelOrder)
{
    if (Source == NULL)
        return NullPointer;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            int RED = Index + 2;
            int GREEN = Index + 1;
            int BLUE = Index + 0;
            double H = 0.0;
            double S = 0.0;
            double L = 0.0;
            RGBtoHSL2(Src[RED], Src[GREEN], Src[BLUE], &H, &S, &L);
            BYTE FinalH = SelectHue ? (BYTE)((H / 360.0) * 255.0) : 0;
            BYTE FinalS = SelectSaturation ? (BYTE)(S * 255.0) : 0;
            BYTE FinalL = SelectLuminance ? (BYTE)(L * 255.0) : 0;
            Dest[Index + 3] = 0xff;
            if (SelectHue)
                Dest[RED] = FinalH;
            else
                Dest[RED] = 0;
            if (SelectSaturation)
                Dest[GREEN] = FinalS;
            else
                Dest[GREEN] = 0;
            if (SelectLuminance)
                Dest[BLUE] = FinalL;
            else
                Dest[BLUE] = 0;
            if (AsGray)
            {
                BYTE Gray = (Dest[RED] + Dest[GREEN] + Dest[BLUE]) / 3;
                Dest[RED] = Gray;
                Dest[GREEN] = Gray;
                Dest[BLUE] = Gray;
            }
            else
            {
                switch (ChannelOrder)
                {
                case 0: //HSL
                    Dest[RED] = FinalH;
                    Dest[GREEN] = FinalS;
                    Dest[BLUE] = FinalL;
                    break;

                case 1: //HLS
                    Dest[RED] = FinalH;
                    Dest[GREEN] = FinalL;
                    Dest[BLUE] = FinalS;
                    break;

                case 2: //SHL
                    Dest[RED] = FinalS;
                    Dest[GREEN] = FinalH;
                    Dest[BLUE] = FinalL;
                    break;

                case 3: //SLH
                    Dest[RED] = FinalS;
                    Dest[GREEN] = FinalL;
                    Dest[BLUE] = FinalH;
                    break;

                case 4: //LHS
                    Dest[RED] = FinalL;
                    Dest[GREEN] = FinalH;
                    Dest[BLUE] = FinalS;
                    break;

                case 5: //LSH
                    Dest[RED] = FinalL;
                    Dest[GREEN] = FinalS;
                    Dest[BLUE] = FinalH;
                    break;

                default:
                    return InvalidOperation;
                }
            }
        }
    }

    return Success;
}

/// <summary>
/// Combine three images, each representing a red, green, or blue channel, and return the result.
/// </summary>
/// <param name="RedSource">Pointer to the red channel image buffer.</param>
/// <param name="GreenSource">Pointer to the green channel image buffer.</param>
/// <param name="BlueSource">Pointer to the blue channel image buffer.</param>
/// <param name="Width">Width of all four buffers.</param>
/// <param name="Height">Height of all four buffers.</param>
/// <param name="Stride">Stride of all four buffers.</param>
/// <param name="Destination">Where the combined image will be written.</param>
/// <returns>Value indicating operational result.</returns>
int RGBCombine(void *RedSource, void *GreenSource, void *BlueSource, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE AlphaValue)
{
    if (RedSource == NULL)
        return NullPointer;
    if (GreenSource == NULL)
        return NullPointer;
    if (BlueSource == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Red = (BYTE *)RedSource;
    BYTE *Green = (BYTE *)GreenSource;
    BYTE *Blue = (BYTE *)BlueSource;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = AlphaValue;
            Dest[Index + 2] = Red[Index + 2];
            Dest[Index + 1] = Green[Index + 1];
            Dest[Index + 0] = Blue[Index + 0];
        }
    }

    return Success;
}

/// <summary>
/// Apply a brightness map to the source image and return it in the destination image.
/// </summary>
/// <remarks>
/// http://en.literateprograms.org/RGB_to_HSV_color_space_conversion_%28C%29
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="IlluminationMap">The brightness map to apply. Only the red channel is used.</param>
/// <param name="Width">Width of the source, destination, and brightness map images.</param>
/// <param name="Height">Height of the source, destination, and brightness map images.</param>
/// <param name="Stride">Stride of the source, destination, and brightness map images.</param>
/// <param name="Destination">Where the result will be written.</param>
/// <returns>Value indicating operational results.</returns>
int ApplyBrightnessMap(void *Source, void *IlluminationMap, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
    if (Source == NULL)
        return NullPointer;
    if (IlluminationMap == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Map = (BYTE *)IlluminationMap;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            double MapPercent = (double)Map[Index + 2] / 255.0;
            Dest[Index + 2] = 255 - (BYTE)((double)Src[Index + 2] * MapPercent);
            Dest[Index + 1] = 255 - (BYTE)((double)Src[Index + 1] * MapPercent);
            Dest[Index + 0] = 255 - (BYTE)((double)Src[Index + 0] * MapPercent);
        }
    }

    return Success;
}

/// <summary>
/// Apply a gamma correction to the source image and return it in the destination image.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Destination">Where the result will be written.</param>
/// <param name="Gamma">Normalized gamma value to apply. This value is clamped to 0.0 to 1.0.</param>
/// <param name="IncludeAlpha">If TRUE, alpha is also gamma corrected.</param>
/// <returns>Value indicating operational results.</returns>
int GammaCorrection(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double Gamma, BOOL IncludeAlpha)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    if (Gamma < 0.0)
        Gamma = 0.0;
    if (Gamma > 1.0)
        Gamma = 1.0;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            if (IncludeAlpha)
                Dest[Index + 3] = (BYTE)(pow((double)Src[Index + 3], Gamma));
            else
                Dest[Index + 3] = Src[Index + 3];
            Dest[Index + 2] = (BYTE)(pow((double)Src[Index + 2], Gamma));
            Dest[Index + 1] = (BYTE)(pow((double)Src[Index + 1], Gamma));
            Dest[Index + 0] = (BYTE)(pow((double)Src[Index + 0], Gamma));
        }
    }

    return Success;
}

int AdjustSaturation(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, double SaturationValue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    if (SaturationValue < 0.0)
        SaturationValue = 0.0;
    if (SaturationValue > 1.0)
        SaturationValue = 1.0;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = Src[Index + 3];
            double H = 0.0;
            double S = 0.0;
            double L = 0.0;
            RGBtoHSL2(Src[Index + 2], Src[Index + 1], Src[Index + 0], &H, &S, &L);
            L = L * SaturationValue;
            BYTE R = 0;
            BYTE G = 0;
            BYTE B = 0;
            HSLtoRGB2(H, S, L, &R, &G, &B);
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return Success;
}

const int HighlightRed = 0;
const int HighlightGreen = 1;
const int HighlightBlue = 2;
const int HighlightYellow = 3;
const int HighlightCyan = 4;
const int HighlightMagenta = 5;

const int NonHighlightGrayscale = 0;
const int NonHighlightTransparent = 1;
const int NonHighlightInverse = 2;

int HighlightImageColor(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double TargetHue, int NonHighlightAction, double HighlightLuminance, double &HueDelta)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE A, R, G, B;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            int ALPHA = Index + 3;
            int RED = Index + 2;
            int GREEN = Index + 1;
            int BLUE = Index + 0;

            double Hue = RGBtoHue2(Src[RED], Src[GREEN], Src[BLUE]);
            BYTE Mean = (Src[RED] + Src[GREEN] + Src[BLUE]) / 3;
            A = Src[ALPHA];
            R = Mean;
            G = Mean;
            B = Mean;

            double HDelta = fabs(TargetHue - Hue);
            HueDelta = HDelta;
            if (HDelta <= 30.0)
            {
                R = Src[RED];
                G = Src[GREEN];
                B = Src[BLUE];
                if (HighlightLuminance != 1.0)
                {
                    SetPixelLuminance(&R, &G, &B, HighlightLuminance);
                }
            }
            else
            {
                if (NonHighlightAction == NonHighlightTransparent)
                {
                    A = 0;
                }
                else
                    if (NonHighlightAction == NonHighlightInverse)
                    {
                        R = 0xff - Src[RED];
                        G = 0xff - Src[GREEN];
                        B = 0xff - Src[BLUE];
                    }
            }

            Dest[ALPHA] = A;
            Dest[RED] = R;
            Dest[GREEN] = G;
            Dest[BLUE] = B;
        }
    }

    return Success;
}

int AdjustImageHSL(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double HueAdjustment, double SaturationAdjustment, double LuminanceAdjustment)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    if (HueAdjustment == 0.0 && SaturationAdjustment == 0.0 && LuminanceAdjustment == 0.0)
        return NoActionTaken;

    //Normalize hue.
    HueAdjustment = (HueAdjustment + 1.0) / 2.0;

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
            double SourceHue = 0.0;
            double SourceSaturation = 0.0;
            double SourceLuminance = 0.0;
            RGBtoHSL2(R, G, B, &SourceHue, &SourceSaturation, &SourceLuminance);
            double FinalHue = SourceHue;
            double FinalSaturation = SourceSaturation;
            double FinalLuminance = SourceLuminance;

            if (HueAdjustment != 0.0)
            {
                FinalHue = (SourceHue + (360.0 * HueAdjustment));
                if (FinalHue > 360.0)
                    FinalHue -= 360.0;
            }
            if (SaturationAdjustment != 0.0)
            {
                FinalSaturation = SourceHue + SaturationAdjustment;
                if (FinalSaturation < 0.0)
                    FinalSaturation = 0.0;
                if (FinalSaturation > 1.0)
                    FinalSaturation = 1.0;
            }
            if (LuminanceAdjustment != 0.0)
            {
                FinalLuminance = SourceHue + LuminanceAdjustment;
                if (FinalLuminance < 0.0)
                    FinalLuminance = 0.0;
                if (FinalLuminance > 1.0)
                    FinalLuminance = 1.0;
            }

            HSLtoRGB2(FinalHue, FinalSaturation, FinalLuminance, &R, &G, &B);

            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return Success;
}
