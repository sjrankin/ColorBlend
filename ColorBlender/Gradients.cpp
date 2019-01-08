#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int GradientX(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *GradientColorList, __int32 GradientColorCount)
{
    if (Target == NULL)
        return NullPointer;
    if (GradientColorList == NULL)
        return NullPointer;

    BYTE * Buffer = (BYTE *)Target;
    GradientColor *GradientPoints = (GradientColor *)GradientColorList;
    int PixelSize = 4;

    for (int i = 0; i < GradientColorCount; i++)
    {
        if (GradientPoints[i].AbsolutePoint)
        {
            GradientPoints[i].FinalX = GradientPoints[i].X;
            GradientPoints[i].FinalY = GradientPoints[i].Y;
        }
        else
        {
            GradientPoints[i].FinalX = (__int32)(GradientPoints[i].X / (double)TargetWidth);
            GradientPoints[i].FinalY = (__int32)(GradientPoints[i].Y / (double)TargetHeight);
        }
    }

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowIndex = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowIndex + (Column * PixelSize);
            double *Distances = new double[GradientColorCount];

            for (int g = 0; g < GradientColorCount; g++)
            {
                double d = sqrt(pow((double)Column - (double)GradientPoints[g].FinalX, 2) +
                    pow((double)Row - (double)GradientPoints[g].FinalY, 2));
                Distances[g] = d;
            }

            delete[] Distances;
        }
    }
    return Success;
}

/// <summary>
/// Returns a percentage of a color.
/// </summary>
/// <param name="Percent">The precent of the returned color.</param>
/// <param name="inR">Red channel input.</param>
/// <param name="inG">Green channel input.</param>
/// <param name="inB">Blue channel input.</param>
/// <param name="outR">Red channel output.</param>
/// <param name="outG">Green channel output.</param>
/// <param name="outB">Blue channel output.</param>
/// <param name="Invert">Determines if the result is inverted.</param>
/// <returns>Source color at the specified percentage and inversion.</returns>
void ColorPercent(double Percent, byte inR, byte inG, byte inB, byte* outR, byte* outG, byte* outB, bool Invert)
{
    *outR = (byte)(Percent*(double)inR);
    *outG = (byte)(Percent*(double)inG);
    *outB = (byte)(Percent*(double)inB);
    if (Invert)
    {
        *outR = 0xff - *outR;
        *outG = 0xff - *outG;
        *outB = 0xff - *outB;
    }
}

/// <summary>
/// Creates a blended color given a coordinate and pure color set.
/// </summary>
/// <param name="X">The horizontal coordinate.</param>
/// <param name="Y">The vertical coordinate.</param>
/// <param name="Width">The width of the image.</param>
/// <param name="Height">The height of the iamge.</param>
/// <param name="Hypotenuse">The hypotenuse of the image.</param>
/// <param name="PointCount">The number of pure colors.</param>
/// <param name="Colors">Array pointer to the set of pure colors.</param>
/// <param name="Points">Array pointer to the set of pure color coordinates.</param>
/// <param name="FinalR">The red channel value on success.</param>
/// <param name="FinalG">The green channel value on success.</param>
/// <param name="FinalB">The blue channel value on success.</param>
void MakeColor(int X, int Y, int Width, int Height, double Hypotenuse, int PointCount, PureColorStruct* Colors,
    AbsolutePointStruct* Points, byte* FinalR, byte* FinalG, byte* FinalB)
{
    int RAccumulator = 0;
    int GAccumulator = 0;
    int BAccumulator = 0;
    for (int i = 0; i < PointCount; i++)
    {
        double Dist = Distance(X, Y, Points[i].X, Points[i].Y);
        double DistPercent = (Hypotenuse - Dist) / Hypotenuse;
        byte cR = 0;
        byte cG = 0;
        byte cB = 0;
        ColorPercent(DistPercent, Colors[i].Red, Colors[i].Green, Colors[i].Blue, &cR, &cG, &cB, false);
        RAccumulator += cR;
        GAccumulator += cG;
        BAccumulator += cB;
    }
    *FinalR = (byte)(RAccumulator / PointCount);
    *FinalG = (byte)(GAccumulator / PointCount);
    *FinalB = (byte)(BAccumulator / PointCount);
}

int Gradient(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *PureColorList, __int32 PureColorCount)
{
    if (Target == NULL)
        return NullPointer;
    if (PureColorList == NULL)
        return NullPointer;

    BYTE * Buffer = (BYTE *)Target;
    PureColorStruct *ColorList = (PureColorStruct *)PureColorList;
    int PixelSize = 4;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowIndex = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowIndex + (Column * PixelSize);
        }
    }
    return Success;
}