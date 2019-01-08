#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

// Assumes 0 <= range <= RAND_MAX
// Returns in the half-open interval [0, max]
//http://stackoverflow.com/questions/2509679/how-to-generate-a-random-number-from-within-a-range
long random_at_most(long max) {
    unsigned long
        // max <= RAND_MAX < ULONG_MAX, so this is okay.
        num_bins = (unsigned long)max + 1,
        num_rand = (unsigned long)RAND_MAX + 1,
        bin_size = num_rand / num_bins,
        defect = num_rand % num_bins;

    long x;
    // This is carefully written not to overflow
    while (num_rand - defect <= (unsigned long)(x = rand()));

    // Truncated division is intentional
    return x / bin_size;
}

/// <summary>
/// Return a random number between <paramref name="Low"/> and <paramref name="High"/> inclusive.
/// </summary>
/// <param name="Low">Low range value. If not specified, defaults to 0.</param>
/// <param name="High">High range value. If not specified, defaults to RAND_MAX.</param>
/// <returns>Random number between <paramref name="Low"/> and <paramref name="High"/> inclusive.</returns>
int rrand2(int Low = 0, int High = RAND_MAX)
{
    int range = High - Low;
    long r = random_at_most((long)range);
    return r + Low;
}

/// <summary>
/// Return a random color.
/// </summary>
/// <param name="Alpha">On return, a random alpha channel value.</param>
/// <param name="Red">On return, a random red channel value.</param>
/// <param name="Green">On return, a random green channel value.</param>
/// <param name="Blue">On return, a random blue channel value.</param>
void RandomColor(BYTE *Alpha, BYTE *Red, BYTE *Green, BYTE *Blue)
{
    *Alpha = rrand2(0, 255);
    *Red = rrand2(0, 255);
    *Green = rrand2(0, 255);
    *Blue = rrand2(0, 255);
}

void ConstrainedRandomColor(BYTE *Alpha, BYTE MinAlpha, BYTE MaxAlpha, BYTE *Red, BYTE MinRed, BYTE MaxRed,
    BYTE *Green, BYTE MinGreen, BYTE MaxGreen, BYTE *Blue, BYTE MinBlue, BYTE MaxBlue)
{
    *Alpha = rrand2(MinAlpha, MaxAlpha);
    *Red = rrand2(MinRed, MaxRed);
    *Green = rrand2(MinGreen, MaxGreen);
    *Blue = rrand2(MinBlue, MaxBlue);
}

/// <summary>
/// Render a rectangle filled with pixels of random colors.
/// </summary>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="BufferWidth">Width of the destination in pixels.</param>
/// <param name="BufferHeight">Height of the destination in scan lines.</param>
/// <param name="BufferStride">Stride of the destination.</param>
/// <param name="LowAlpha">Lowest valid alpha value.</param>
/// <param name="HighAlpha">Greatest valid alpha value. If LowAlpha and HightAlpha are the same, that value will be used.</param>
/// <param name="LowRed">Lowest valid red value.</param>
/// <param name="HighRed">Greatest valid red value. If LowRed and HightRed are the same, that value will be used.</param>
/// <param name="LowGreen">Lowest valid green value.</param>
/// <param name="HighGreen">Greatest valid green value. If LowGreen and HightGreen are the same, that value will be used.</param>
/// <param name="LowRed">Lowest valid blue value.</param>
/// <param name="HighBlue">Greatest valid blue value. If LowBlue and HightBlue are the same, that value will be used.</param>
/// <returns>Value indicating success.</returns>
int RenderRandomColorRectangle(void *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BYTE LowAlpha, BYTE HighAlpha, BYTE LowRed, BYTE HighRed, BYTE LowGreen, BYTE HighGreen, BYTE LowBlue, BYTE HighBlue,
    UINT32 Seed)
{
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Destination;
    srand(Seed);

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE ARand = rrand2(LowAlpha, HighAlpha);
            BYTE RRand = rrand2(LowRed, HighRed);
            BYTE GRand = rrand2(LowGreen, HighGreen);
            BYTE BRand = rrand2(LowBlue, HighBlue);
            Buffer[Index + 3] = ARand;
            Buffer[Index + 2] = RRand;
            Buffer[Index + 1] = GRand;
            Buffer[Index + 0] = BRand;
        }
    }

    return Success;
}

/// <summary>
/// Render a rectangle that consists of sub-rectangles with random colors.
/// </summary>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="BufferWidth">Width of the destination in pixels.</param>
/// <param name="BufferHeight">Height of the destination in scan lines.</param>
/// <param name="BufferStride">Stride of the destination.</param>
/// <param name="BlockWidth">Width of sub-blocks.</param>
/// <param name="BlockHeight">Height of sub-blocks.</param>
/// <param name="Seed">Random number generator seed.</param>
/// <returns>Value indicating success.</returns>
int RenderRandomSubBlockRectangle(void *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    __int32 BlockWidth, __int32 BlockHeight, UINT32 Seed)
{
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Destination;
    srand(Seed);

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);


        }
    }

    return Success;
}

/// <summary>
/// Return a color that is between a start and end color. If the start and end colors are the same, that color will be used.
/// </summary>
/// <param name="StartColor">The packed start color.</param>
/// <param name="EndColor">The packed end color.</param>
/// <param name="NormalizedDistance">The distance of the color to return.</param>
/// <param name="IgnoreAlpha">If true, alpha is set to 0xff. Otherwise alpha will be modified as well.</param>
/// <param name="FinalColor">Will contain the final color on return.</param>
void ColorInterval(UINT32 StartColor, UINT32 EndColor, double NormalizedDistance, BOOL IgnoreAlpha, UINT32 *FinalColor)
{
    BYTE StartA = (StartColor & 0xff000000) >> 24;
    BYTE StartR = (StartColor & 0x00ff0000) >> 16;
    BYTE StartG = (StartColor & 0x0000ff00) >> 8;
    BYTE StartB = (StartColor & 0x000000ff) >> 0;
    BYTE EndA = (EndColor & 0xff000000) >> 24;
    BYTE EndR = (EndColor & 0x00ff0000) >> 16;
    BYTE EndG = (EndColor & 0x0000ff00) >> 8;
    BYTE EndB = (EndColor & 0x000000ff) >> 0;

    BYTE FinalA;
    if (IgnoreAlpha)
        FinalA = 0xff;
    else
        FinalA = (BYTE)((double)(abs(EndA - StartA)) * NormalizedDistance);

    BYTE FinalR;
    if (EndR == StartR)
        FinalR = EndR;
    else
        FinalR = (BYTE)((double)(abs(EndR - StartR)) * NormalizedDistance);

    BYTE FinalG;
    if (EndG == StartG)
        FinalG = EndG;
    else
        FinalG = (BYTE)((double)(abs(EndG - StartG)) * NormalizedDistance);

    BYTE FinalB;
    if (EndB == StartB)
        FinalB = EndB;
    else
        FinalB = (BYTE)((double)(abs(EndB - StartB)) * NormalizedDistance);

    *FinalColor = (FinalA << 24) | (FinalR << 16) | (FinalG << 8) | FinalB;
}

/// <summary>
/// Render a color gradient rectangle.
/// </summary>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="BufferWidth">Width of the destination in pixels.</param>
/// <param name="BufferHeight">Height of the destination in scan lines.</param>
/// <param name="BufferStride">Stride of the destination.</param>
/// <param name="PackedStartColor">The gradient start color in packed format.</param>
/// <param name="PackedEndColor">The gradient end color in packed format.</param>
/// <param name="IgnoreAlpha">Determines if alpha is modified. If false, all alphas are set to 0xff.</param>
/// <param name="DoHorizontal">Determines the gradient direction.</param>
/// <returns>Value indicating operational result.</returns>
int RenderRampingGradientColorRectangle(void  *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    UINT32 PackedStartColor, UINT32 PackedEndColor, BOOL IgnoreAlpha, BOOL DoHorizontal)
{
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Destination;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            UINT32 Final = 0x0;
            if (DoHorizontal)
            {
                double HNormal = (double)(Column + 1) / (double)BufferWidth;
                ColorInterval(PackedStartColor, PackedEndColor, HNormal, IgnoreAlpha, &Final);
            }
            else
            {
                double VNormal = (double)(Row + 1) / (double)BufferHeight;
                ColorInterval(PackedStartColor, PackedEndColor, VNormal, IgnoreAlpha, &Final);
            }
            Buffer[Index + 3] = (Final & 0xff000000) >> 24;
            Buffer[Index + 2] = (Final & 0x00ff0000) >> 16;
            Buffer[Index + 1] = (Final & 0x0000ff00) >> 8;
            Buffer[Index + 0] = (Final & 0x000000ff) >> 0;
        }
    }

    return Success;
}

/// <summary>
/// Return the appropriate index in the gradient stop list based on the location in <paramref name="Index"/>.
/// </summary>
/// <param name="Stops">Pointer to a list of gradient stops.</param>
/// <param name="StopCount">Number of stops in the gradient stop list.</param>
/// <param name="Index">The location in the image that determines which gradient stop index to return.</param>
/// <returns>The gradient stop index on success, -1 if not found.</returns>
int GradientIndexFromGradientRange(void *Stops, int StopCount, int Index)
{
    if (Stops == NULL)
        return -1;
    GradientStop *Gradients = (GradientStop *)Stops;
    for (int i = 0; i < StopCount; i++)
    {
        if ((Index >= Gradients[i].AbsStart) && (Index <= Gradients[i].AbsEnd))
            return i;
    }
    return  -1;
}

/// <summary>
/// Render an image defined by a list of color gradient stops.
/// </summary>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="BufferWidth">Width of the destination in pixels.</param>
/// <param name="BufferHeight">Height of the destination in scan lines.</param>
/// <param name="BufferStride">Stride of the destination.</param>
/// <param name="IgnoreAlpha">Determines if alpha is used.</param>
/// <param name="DoHorizontal">Determines the axis of the gradients.</param>
/// <param name="Stops">Pointer to an array of color gradient stops.</param>
/// <param name="StopCount">Number of color gradients in <paramref name="Stops"/>.</param>
/// <returns>Value indicating opertional result.</returns>
int RenderLinearGradients(void *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BOOL IgnoreAlpha, BOOL DoHorizontal, void *Stops, int StopCount)
{
    if (Destination == NULL)
        return NullPointer;
    if (Stops == NULL)
        return NullPointer;
    if (StopCount < 2)
        return IndexOutOfRange;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Destination;
    GradientStop *Gradients = (GradientStop *)Stops;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            UINT32 Final = 0x0;
            int GradientIndex = GradientIndexFromGradientRange(Stops, StopCount, DoHorizontal ? Column : Row);
            if (GradientIndex < 0)
                return IndexOutOfRange;
            if (GradientIndex >= StopCount)
                return IndexOutOfRange;

            int RangeIndex = (DoHorizontal ? Column : Row) - Gradients[GradientIndex].AbsStart;
            if (RangeIndex < 0)
                return IndexOutOfRange;
            double RangeNormal = (double)(RangeIndex) / (double)Gradients[GradientIndex].AbsGap;

            BYTE sA, sR, sG, sB;
            BYTE eA, eR, eG, eB;
            sA = (BYTE)((Gradients[GradientIndex].StartColor & 0xff000000) >> 24);
            sR = (BYTE)((Gradients[GradientIndex].StartColor & 0x00ff0000) >> 16);
            sG = (BYTE)((Gradients[GradientIndex].StartColor & 0x0000ff00) >> 8);
            sB = (BYTE)((Gradients[GradientIndex].StartColor & 0x000000ff) >> 0);
            eA = (BYTE)((Gradients[GradientIndex].EndColor & 0xff000000) >> 24);
            eR = (BYTE)((Gradients[GradientIndex].EndColor & 0x00ff0000) >> 16);
            eG = (BYTE)((Gradients[GradientIndex].EndColor & 0x0000ff00) >> 8);
            eB = (BYTE)((Gradients[GradientIndex].EndColor & 0x000000ff) >> 0);

            BYTE fA, fR, fG, fB;
            if (RangeNormal >= 1.0)
            {
                //At or past the end of the current gradient range.
                fA = eA;
                fR = eR;
                fG = eG;
                fB = eB;
            }
            else
                if (RangeNormal <= 0.0)
                {
                    //At or earlier than the start of the current gradient range.
                    fA = sA;
                    fR = sR;
                    fG = sG;
                    fB = sB;
                }
                else
                {
                    //Somewhere in the gradient range.
                    if (sA == eA)
                        fA = sA;
                    else
                        fA = (BYTE)((double)(eA - sA) * RangeNormal) + sA;
                    if (sR == eR)
                        fR = sR;
                    else
                        fR = (BYTE)((double)(eR - sR) * RangeNormal) + sR;
                    if (sG == eG)
                        fG = sG;
                    else
                        fG = (BYTE)((double)(eG - sG) * RangeNormal) + sG;
                    if (sB == eB)
                        fB = sB;
                    else
                        fB = (BYTE)((double)(eB - sB) * RangeNormal) + sB;
                }
            if (IgnoreAlpha)
                fA = 0xff;

            Buffer[Index + 0] = fB;
            Buffer[Index + 1] = fG;
            Buffer[Index + 2] = fR;
            Buffer[Index + 3] = fA;
        }
    }

    return Success;
}

/// <summary>
/// Render a rectangle with ramping colors, e.g., colors whose individual channels are incremented/decremented by position.
/// </summary>
/// <remarks>
/// This function allows the caller to ramp color channels individually rather than as an entire color.
/// </remarks>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="BufferWidth">Width of the destination in pixels.</param>
/// <param name="BufferHeight">Height of the destination in scan lines.</param>
/// <param name="BufferStride">Stride of the destination.</param>
/// <param name="RampAlpha">Determines if alpha will be ramped.</param>
/// <param name="AlphaStart">Start of alpha values.</param>
/// <param name="AlphaIncrement">How to increment alpha by position</param>
/// <param name="NonRampAlpha">Alpha value to use if not ramping alpha.</param>
/// <param name="RampAlpha">Determines if red will be ramped.</param>
/// <param name="redStart">Start of red values.</param>
/// <param name="redIncrement">How to increment red by position</param>
/// <param name="NonRampred">red value to use if not ramping red.</param>
/// <param name="RampAlpha">Determines if green will be ramped.</param>
/// <param name="greenStart">Start of green values.</param>
/// <param name="greenIncrement">How to increment green by position</param>
/// <param name="NonRampgreen">green value to use if not ramping green.</param>
/// <param name="RampAlpha">Determines if blue will be ramped.</param>
/// <param name="blueStart">Start of blue values.</param>
/// <param name="blueIncrement">How to increment blue by position</param>
/// <param name="NonRampblue">blue value to use if not ramping blue.</param>
/// <returns>Value indicating opertional result.</returns>
int RenderRampingColorRectangle(void  *Destination, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BOOL RampAlpha, BYTE AlphaStart, BYTE AlphaIncrement, BYTE NonRampAlpha,
    BOOL RampRed, BYTE RedStart, BYTE RedIncrement, BYTE NonRampRed,
    BOOL RampGreen, BYTE GreenStart, BYTE GreenIncrement, BYTE NonRampGreen,
    BOOL RampBlue, BYTE BlueStart, BYTE BlueIncrement, BYTE NonRampBlue)
{
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE *Buffer = (BYTE *)Destination;
    BYTE RampingAlpha = AlphaStart;
    BYTE RampingRed = RedStart;
    BYTE RampingGreen = GreenStart;
    BYTE RampingBlue = BlueStart;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            if (RampAlpha)
            {
                Buffer[Index + 3] = RampingAlpha;
                RampingAlpha += AlphaIncrement;
            }
            else
            {
                Buffer[Index + 3] = NonRampAlpha;
            }
            if (RampRed)
            {
                Buffer[Index + 2] = RampingRed;
                RampingRed += RedIncrement;
            }
            else
            {
                Buffer[Index + 2] = NonRampRed;
            }
            if (RampGreen)
            {
                Buffer[Index + 1] = RampingGreen;
                RampingGreen += GreenIncrement;
            }
            else
            {
                Buffer[Index + 1] = NonRampGreen;
            }
            if (RampBlue)
            {
                Buffer[Index + 0] = RampingBlue;
                RampingBlue += BlueIncrement;
            }
            else
            {
                Buffer[Index + 0] = NonRampBlue;
            }
        }
    }

    return Success;
}

