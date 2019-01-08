#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

//
// This file contains functions that blend colors together based on the location of a set of "pure" colors. The "pure" colors are
// seeds that allow calculations to determine what colors are in between "pure" colors. Colors are blended in a rectangular buffer
// regardless of the settings for each pure color point (e.g., blending stops at the borders). The format of the buffer is BGRA 
// (32 bits).
//


/// <summary>
/// Expensive version of a NOP.
/// </summary>
inline void DoNothing()
{
}

/// <summary>
/// Calculate and return a brightness/luminance value for the passed color channel values.
/// </summary>
/// <param name="R">The red channel value.</param>
/// <param name="G">The green channel value.</param>
/// <param name="B">The blue channel value.</param>
/// <returns>Luninance of the passed color channels</returns>
inline double PixelLuminance(BYTE R, BYTE G, BYTE B)
{
    return (0.2126 * (double)R) + (0.7152 * (double)G) + (0.0722 * (double)B);
}

/// <summary>
/// Calculate and return a brightness/luminance value for the passed color channel values.
/// </summary>
/// <param name="R">The normalized red channel value.</param>
/// <param name="G">The normalized green channel value.</param>
/// <param name="B">The normalized blue channel value.</param>
/// <returns>Luninance of the passed color channels</returns>
inline double PixelLuminanceSc(double R, double G, double B)
{
    return (0.2126 * (double)R) + (0.7152 * (double)G) + (0.0722 * (double)B);
}

/// <summary>
/// Calculate and return a brightness/luminance value for the passed color channel values. This function is exposed to
/// managed programs but uses the same function as do other functions - that way the same algorithm is used for everything.
/// </summary>
/// <param name="R">The red channel value.</param>
/// <param name="G">The green channel value.</param>
/// <param name="B">The blue channel value.</param>
/// <returns>Luninance of the passed color channels</returns>
inline double ColorLuminance(BYTE R, BYTE G, BYTE B)
{
    double nR = (double)R / 255.0;
    double nG = (double)G / 255.0;
    double nB = (double)B / 255.0;
    return PixelLuminanceSc(nR, nG, nB);
    //return PixelLuminance(R, G, B);
}

/// <summary>
/// Calculate and return a brightness/luminance value for the passed color channel values. This function is exposed to
/// managed programs but uses the same function as do other functions - that way the same algorithm is used for everything.
/// </summary>
/// <param name="R">The normalized red channel value.</param>
/// <param name="G">The normalized green channel value.</param>
/// <param name="B">The normalzied blue channel value.</param>
/// <returns>Luninance of the passed color channels</returns>
inline double ColorLuminanceSc(double R, double G, double B)
{
    return PixelLuminanceSc(R, G, B);
}

/// <summary>
/// Return the delta between the two parameters.
/// </summary>
/// <param name="Op1">First operand.</param>
/// <param name="Op2">Second operation.</param>
/// <returns>The delta between the two operands.</returns>
inline double delta(double Op1, double Op2)
{
    return fabs(Op1 - Op2);
}

/// <summary>
/// Calculates the distance between (X1,Y1) and (X2,Y2).
/// </summary>
/// <param name="X1">First horizontal coordinate.</param>
/// <param name="Y1">First vertical coordinate.</param>
/// <param name="X2">Second horizontal coordinate.</param>
/// <param name="Y2">Second vertical coordinate.</param>
/// <returns>The distance between (X1,Y1) and (X2,Y2).</returns>
inline double Distance(int X1, int Y1, int X2, int Y2)
{
    return sqrt(pow((double)(X1 - X2), 2) + pow((double)(Y1 - Y2), 2));
}

/// <summary>
/// Determines if (X,Y) is a pure color point.
/// </summary>
/// <param name="X">The horizontal coordinate.</param>
/// <param name="Y">The vertical coordinate.</param>
/// <param name="Points">Array pointer to the set of pure color points.</param>
/// <param name="PointCount">Number of pure color points in Points.</param>
/// <returns>The index of the pure color at (X,Y) if at a pure color point, -1 if not at a pure color point.</returns>
int ColorPointIndex(int X, int Y, AbsolutePointStruct* Points, int PointCount)
{
    for (int i = 0; i < PointCount; i++)
    {
        if ((Points[i].X == X) && (Points[i].Y == Y))
            return i;
    }
    return -1;
}

/// <summary>
/// Given a set of colors and their locations, create data that can be used in an Image (BGRA32 format) that has all of the
/// colors blended appropriately.
/// </summary>
/// <param name="Target">The location where the colors will be placed - must be allocated prior to calling this function.</param>
/// <param name="Width">The width of the image/target buffer.</param>
/// <param name="Height">The height of the image/target buffer.</param>
/// <param name="Stride">The stride of the image/target buffer.</param>
/// <param name="PureColorCount">The number of colors used as primary colors for blending.</param>
/// <param name="ColorLocations">
/// Pointer to an array of AbsolutePointStructs that determines the location of the pure colors. No error checking is done.
/// </param>
/// <param name="PureColors">Pointer to an array of PureColorStructs that contain the primary colors from which the blending is generated.</param>
/// <returns>TRUE on success, FALSE on parametric fail.</returns>
bool BlendColors(void *Target, __int32 Width, __int32 Height, __int32 Stride,
    __int32 PureColorCount, void *ColorLocations, void *PureColors)
{
    if (Target == NULL)
        return FALSE;
    if (ColorLocations == NULL)
        return FALSE;
    if (PureColors == NULL)
        return FALSE;
    if (PureColorCount < 1)
        return FALSE;

    __int32 PixelSize = 4;
    BYTE* Buffer = (BYTE*)Target;
    PureColorStruct* ColorSet = (PureColorStruct*)PureColors;
    AbsolutePointStruct* ColorPoints = (AbsolutePointStruct*)ColorLocations;
    double Hypotenuse = sqrt(pow((double)Width, 2) + pow((double)Height, 2));

    for (__int32 Row = 0; Row < Height; Row++)
    {
        __int32 RowOffset = Row * Stride;
        for (__int32 Column = 0; Column < Width; Column++)
        {
            __int32 Index = RowOffset + (Column * PixelSize);
            int ColorIndex = ColorPointIndex(Column, Row, ColorPoints, PureColorCount);
            if (ColorIndex > -1)
            {
                Buffer[Index + 0] = ColorSet[ColorIndex].Blue;
                Buffer[Index + 1] = ColorSet[ColorIndex].Green;
                Buffer[Index + 2] = ColorSet[ColorIndex].Red;
                Buffer[Index + 3] = ColorSet[ColorIndex].Alpha;
            }
            else
            {
                byte iR = 0;
                byte iG = 0;
                byte iB = 0;
                byte iA = 0xff;
                MakeColor(Column, Row, Width, Height, Hypotenuse, PureColorCount, ColorSet, ColorPoints, &iR, &iG, &iB);
                Buffer[Index + 0] = iB;
                Buffer[Index + 1] = iG;
                Buffer[Index + 2] = iR;
                Buffer[Index + 3] = iA;
            }
        }
    }

    return TRUE;
}

/// <summary>
/// IDs of the various channels in an ARGB pixel.
/// </summary>
/// <remarks>
/// The values assigned to the enums are used to shift the source pixel to the proper location in order to
/// extract the channel as efficiently as possible (e.g., in a calculation, not with a bunch of IF statements).
/// </remarks>
enum ChannelIDs
{
    /// <summary>
    /// The alpha channel.
    /// </summary>
    Alpha = 24,
    /// <summary>
    /// The red channel.
    /// </summary>
    Red = 16,
    /// <summary>
    /// The green channel.
    /// </summary>
    Green = 8,
    /// <summary>
    /// The blue channel.
    /// </summary>
    Blue = 0
};

/// <summary>
/// Extract and return the specified channel value from <paramref name="AllChannels"/>.
/// </summary>
/// <param name="AllChannels">The source for channel information. Must be in the form of AARRGGBB.</param>
/// <param name="ChannelID">Determines which channel is extracted and returned.</param>
/// <returns>The specified channel from <paramref name="AllChannels"/>.</returns>
inline byte GetChannel(__int32 AllChannels, ChannelIDs ChannelID)
{
    return (AllChannels >> ChannelID) & 0x000000ff;
}

/// <summary>
/// Merges a given channel's value into the source value and returns the results.
/// </summary>
/// <remarks>
/// This function does not clear channels before merging them so it is best to start with a <paramref name="Source"/> value of 0x0.
/// </remarks>
/// <param name="Source">
/// The pixel value where <paramref name="ChannelValue"/> will be merged. For the purposes of this function, it is
/// assumed that the format of this parameter is AARRGGBB.
/// </param>
/// <param name="ChannelValue">The value to merge with the source, e.g., the channel value.</param>
/// <param name="ChannelID">Determines which channel is being merged (the location of the merge) into <paramref name="Source"/>.</param>
/// <returns><paramref name="Source"/> merged with <paramref name="ChannelValue"/>.</returns>
inline __int32 MergeChannel(__int32 Source, byte ChannelValue, ChannelIDs ChannelID)
{
    return Source | (ChannelValue << ChannelID);
}

/// <summary>
/// Sets one pixel.
/// </summary>
/// <param name="Buffer">The start of the buffer where the pixel will be set.</param>
/// <param name="Index">The offset into the buffer where to set the pixel values.</param>
/// <param name="A">The alpha value.</param>
/// <param name="R">The red value.</param>
/// <param name="G">The green value.</param>
/// <param name="B">The blue value.</param>
inline void SetPixel(BYTE *Buffer, int Index, BYTE A, BYTE R, BYTE G, BYTE B)
{
    Buffer[Index + 0] = B;
    Buffer[Index + 1] = G;
    Buffer[Index + 2] = R;
    Buffer[Index + 3] = A;
}

/// <summary>
/// Return an inverse mask for the given channel. Assumes pixel structure of AARRGGBB.
/// </summary>
/// <returns>Inverse mask for the specified channel.</returns>
inline __int32 InverseChannelMask(ChannelIDs ChannelID)
{
    switch (ChannelID)
    {
        case Alpha:
            return 0x00ffffff;

        case Red:
            return 0xff00ffff;

        case Green:
            return 0xffff00ff;

        case Blue:
            return 0xffffff00;

        default:
            return 0x0;
    }
}

/// <summary>
/// Clears then sets the specified channel to the specified channel value.
/// </summary>
/// <param name="Source">The source pixel value.</param>
/// <param name="ChannelValue">The value to set - replaces old value.</param>
/// <param name="ChannelID">Determines the location of the channel.</param>
/// <returns>New pixel value with the appropriate channel cleared then set with passed data.</returns>
inline __int32 SetChannel(__int32 Source, byte ChannelValue, ChannelIDs ChannelID)
{
    return (Source & InverseChannelMask(ChannelID)) + (ChannelValue << ChannelID);
}

/// <summary>
/// Returns a percentage of a color.
/// </summary>
/// <param name="Percent">The precent of the returned color.</param>
/// <param name="inA">Alpha channel input. Not currently used.</param>
/// <param name="inR">Red channel input.</param>
/// <param name="inG">Green channel input.</param>
/// <param name="inB">Blue channel input.</param>
/// <param name="outA">Alpha channel output.</param>
/// <param name="outR">Red channel output.</param>
/// <param name="outG">Green channel output.</param>
/// <param name="outB">Blue channel output.</param>
/// <param name="AlphaStart">Starting (at the pure color point location) alpha level.</param>
/// <param name="AlphaEnd">Ending alpha level.</param>
/// <param name="UseAlpha">Determines if alpha calculations are made. If not, alpha is always 0xff.</param>
void ColorPercent2(double Percent, byte inA, byte inR, byte inG, byte inB, byte* outA, byte* outR, byte* outG, byte* outB,
    double AlphaStart, double AlphaEnd, bool UseAlpha)
{
    if (UseAlpha)
    {
        if (Percent > 1.0)
        {
            *outA = (byte)(AlphaEnd * 0xff);
        }
        else
        {
            if (Percent <= 0.0)
            {
                *outA = (byte)(AlphaStart * 0xff);
            }
            else
            {
                double AlphaRange = AlphaEnd - AlphaStart;
                *outA = (byte)(Percent * AlphaRange);
            }
        }
    }
    else
        *outA = 0xff;
    *outR = (byte)(Percent*(double)inR);
    *outG = (byte)(Percent*(double)inG);
    *outB = (byte)(Percent*(double)inB);
}

BOOL InRange(int X1, int Y1, int X2, int Y2, double Radius)
{
    double Dist = (X1, Y1, X2, Y2);
    return Dist <= Radius ? true : false;
}

//http://stackoverflow.com/questions/7438263/alpha-compositing-algorithm-blend-modes
void AlphaBlend()
{

}

/// <summary>
/// Create the blended color at the specified point in the buffer.
/// </summary>
/// <param name="Target">The location where the colors will be placed - must be allocated prior to calling this function.</param>
/// <param name="Width">The width of the image/target buffer.</param>
/// <param name="Height">The height of the image/target buffer.</param>
/// <param name="PurePointCount">Number of pure colors in the set of pure colors.</param>
/// <param name="Colors">Points to an array of pure colors.</param>
/// <param name="FinalA">Pointer to the final alpha level.</param>
/// <param name="FinalR">Pointer to the final red level.</param>
/// <param name="FinalG">Pointer to the final green level.</param>
/// <param name="FinalB">Pointer to the final blue level.</param>
/// <returns>TRUE on success, FALSE on parametric fail.</returns>
void MakeColor2(int X, int Y, int Width, int Height, int PurePointCount, PureColorType* Colors,
    byte *FinalA, byte* FinalR, byte* FinalG, byte* FinalB)
{
    int AACcumulator = 0;
    int RAccumulator = 0;
    int GAccumulator = 0;
    int BAccumulator = 0;
    for (int i = 0; i < PurePointCount; i++)
    {
        double Dist = Distance(X, Y, Colors[i].X, Colors[i].Y);
        double DistPercent = 0.0;
        if (Colors[i].UseRadius)
        {
            //if (!InRange (Colors[i].X, Colors[i].Y, Colors[i].Radius, X, Y))
            //    continue;
            DistPercent = (fabs(Dist - Colors[i].Radius)) / Colors[i].Radius;
            if (DistPercent > 1.0)
                continue;
            /*
            //            DistPercent = (Colors[i].Radius - Dist) / Colors[i].Radius;
            DistPercent = (delta (Dist, Colors[i].Radius)) / Colors[i].Radius;
            //            DistPercent = (abs(Dist - Colors[i].Radius)) / Colors[i].Radius;
            if (DistPercent > 1.0)
            continue;
            */
        }
        else
        {
            //            DistPercent = (Colors[i].Hypotenuse - Dist) / Colors[i].Hypotenuse;
            DistPercent = (Dist - Colors[i].Hypotenuse) / Colors[i].Hypotenuse;
        }
        byte cA = 0;
        byte cR = 0;
        byte cG = 0;
        byte cB = 0;
        ColorPercent2(DistPercent, Colors[i].Alpha, Colors[i].Red, Colors[i].Green, Colors[i].Blue, &cA, &cR, &cG, &cB,
            Colors[i].AlphaStart, Colors[i].AlphaEnd, Colors[i].UseAlpha);
        if (Colors[i].UseAlpha)
            AACcumulator += cA;
        else
            AACcumulator += 0xff;
        RAccumulator += cR;
        GAccumulator += cG;
        BAccumulator += cB;
    }
    *FinalA = (byte)(AACcumulator / PurePointCount);
    *FinalR = (byte)(RAccumulator / PurePointCount);
    *FinalG = (byte)(GAccumulator / PurePointCount);
    *FinalB = (byte)(BAccumulator / PurePointCount);
}

/// <summary>
/// Determines if the point X,Y is a pure color point.
/// </summary>
/// <param name="X">Horizontal location of the point to test.</param>
/// <param name="Y">Vertical location of the point to test.</param>
/// <param name="ColorSet">Pointer to the set of pure colors.</param>
/// <param name="PurePointCount">Number of pure colors in ColorSet.</param>
/// <returns>The index of the pure color at X,Y, -1 if not a pure color point.</returns>
int ColorPointIndex2(int X, int Y, PureColorType *ColorSet, int PurePointCount)
{
    for (int i = 0; i < PurePointCount; i++)
    {
        if ((ColorSet[i].X == X) && (ColorSet[i].Y == Y))
            return i;
    }
    return -1;
}

/// <summary>
/// Draw a block/point in <paramref name="Buffer"/>.
/// </summary>
/// <param name="Buffer">The buffer where the line will be drawn.</param>
/// <param name="Width">The width of the buffer.</param>
/// <param name="Height">The height of the buffer.</param>
/// <param name="Stride">The stride of the buffer.</param>
/// <param name="ColorIndex">Determines where and what color the line will be.</param>
/// <param name="ColorSet">Source for line location and color.</param>
void DrawPointIndicator(BYTE* Buffer, __int32 Width, __int32 Height, __int32 Stride, __int32 ColorIndex, PureColorType *ColorSet)
{
    UINT32 MaxValidIndex = ((Width * Stride) * Height) - 1;
    AbsolutePointStruct Points[9];
    //Center row.
    Points[0].X = ColorSet[ColorIndex].X;
    Points[0].Y = ColorSet[ColorIndex].Y;
    Points[1].X = ColorSet[ColorIndex].X - 1;
    Points[1].Y = ColorSet[ColorIndex].Y;
    Points[2].X = ColorSet[ColorIndex].X + 1;
    Points[2].Y = ColorSet[ColorIndex].Y;
    //Top row.
    Points[3].X = ColorSet[ColorIndex].X - 1;
    Points[3].Y = ColorSet[ColorIndex].Y - 1;
    Points[4].X = ColorSet[ColorIndex].X;
    Points[4].Y = ColorSet[ColorIndex].Y - 1;
    Points[5].X = ColorSet[ColorIndex].X + 1;
    Points[5].Y = ColorSet[ColorIndex].Y - 1;
    //Bottom row.
    Points[6].X = ColorSet[ColorIndex].X - 1;
    Points[6].Y = ColorSet[ColorIndex].Y + 1;
    Points[7].X = ColorSet[ColorIndex].X;
    Points[7].Y = ColorSet[ColorIndex].Y + 1;
    Points[8].X = ColorSet[ColorIndex].X + 1;
    Points[8].Y = ColorSet[ColorIndex].Y + 1;

    for (int PointIndex = 0; PointIndex < 9; PointIndex++)
    {
        __int32 PixelIndex = (Points[PointIndex].Y * Stride) + (Points[PointIndex].X * 4);
        if ((UINT32)(PixelIndex + 3) > MaxValidIndex)
            continue;
        Buffer[PixelIndex + 0] = ColorSet[ColorIndex].Blue;
        Buffer[PixelIndex + 1] = ColorSet[ColorIndex].Green;
        Buffer[PixelIndex + 2] = ColorSet[ColorIndex].Red;
        Buffer[PixelIndex + 3] = ColorSet[ColorIndex].Alpha;
    }
}

/// <summary>
/// Given a set of colors and their locations, create data that can be used in an Image (BGRA32 format) that has all of the
/// colors blended appropriately.
/// </summary>
/// <param name="Target">The location where the colors will be placed - must be allocated prior to calling this function.</param>
/// <param name="Width">The width of the image/target buffer.</param>
/// <param name="Height">The height of the image/target buffer.</param>
/// <param name="Stride">The stride of the image/target buffer.</param>
/// <param name="PureColorCount">The number of colors used as primary colors for blending.</param>
/// <param name="PureColors">
/// Pointer to an array of PureColorTypes that contain the primary colors and locations from which the 
/// blending is generated/calculated.
/// </param>
/// <returns>TRUE on success, FALSE on parametric fail.</returns>
bool BlendColors2(void *Target, __int32 Width, __int32 Height, __int32 Stride, __int32 PureColorCount, void *PureColors)
{
    if (Target == NULL)
        return FALSE;
    if (PureColors == NULL)
        return FALSE;
    if (PureColorCount < 1)
        return FALSE;

    __int32 PixelSize = 4;
    BYTE* Buffer = (BYTE*)Target;
    PureColorType* ColorSet = (PureColorType*)PureColors;
    UINT32 MaxValidIndex = ((Width * Stride) * Height) - 1;

    for (__int32 Row = 0; Row < Height; Row++)
    {
        __int32 RowOffset = Row * Stride;
        for (__int32 Column = 0; Column < Width; Column++)
        {
            __int32 Index = RowOffset + (Column * PixelSize);
            if ((UINT32)(Index + 3) > MaxValidIndex)
                return FALSE;
            byte iR = 0;
            byte iG = 0;
            byte iB = 0;
            byte iA = 0xff;
            MakeColor2(Column, Row, Width, Height, PureColorCount, ColorSet, &iA, &iR, &iG, &iB);

            //            SetPixel (Buffer, Index, iA, iR, iG, iB);

            Buffer[Index + 0] = iB;
            Buffer[Index + 1] = iG;
            Buffer[Index + 2] = iR;
            Buffer[Index + 3] = iA;
        }
    }

    /*
    for (__int32 PointIndex = 0; PointIndex < PureColorCount; PointIndex++)
    {
    if (ColorSet[PointIndex].DrawHorizontalIndicator)
    DrawHorizontalLine (Buffer, Width, Height, Stride, PointIndex, ColorSet);
    if (ColorSet[PointIndex].DrawVerticalIndicator)
    DrawVerticalLine (Buffer, Width, Height, Stride, PointIndex, ColorSet);
    if (ColorSet[PointIndex].DrawPointIndicator)
    DrawPointIndicator (Buffer, Width, Height, Stride, PointIndex, ColorSet);
    }
    */

    return TRUE;
}

/// <summary>
/// Return a set of points given the top,left and bottom,right coordinates.
/// </summary>
/// <param name="Left">The left side of the region.</param>
/// <param name="Top">The top of the region.</param>
/// <param name="Right">The right side of the region.</param>
/// <param name="Bottom">The bottom of the region.</param>
/// <param name="Width">The width of the buffer.</param>
/// <param name="Height">The height of the buffer.</param>
/// <param name="Stride">The stride of the buffer.</param>
/// <param name="PointList">Pointer to an array of points generated by this function.</param>
/// <param name="DotCount">The maximum number of dots given the region defintion.</param>
/// <returns>Quantity of points actually generated.</returns>
int MakeDotIndices(int Left, int Top, int Right, int Bottom, int Width, int Height, int Stride,
    AbsolutePointStruct *PointList, int DotCount)
{
    if (PointList == NULL)
        return 0;

    int Index = 0;
    for (int Y = Top; Y <= Bottom; Y++)
    {
        if (Y < 0)
            continue;
        if (Y >= Height)
            continue;
        for (int X = Left; X <= Right; X++)
        {
            if (X < 0)
                continue;
            if (X >= Width)
                continue;
            PointList[Index].X = X;
            PointList[Index].Y = Y;
            Index++;
        }
    }
    return Index;
}

/// <summary>
/// Determines if the logical point (<paramref name="X"/>,<paramref name="Y"/>) is in <paramref name="Plane"/>.
/// </summary>
/// <param name="X">Logical horizontal coordinate.</param>
/// <param name="Y">Logical vertical coordinate.</param>
/// <param name="Plane">The plane that will be tested.</param>
/// <returns>TRUE if (<paramref name="X"/>,<paramref name="Y"/>) is in the plane, FALSE if not.</returns>
BOOL PointInPlane(int X, int Y, PlaneSetStruct *Plane)
{
    if (Plane == NULL)
        return FALSE;
    if ((X<Plane->Left) || (X>Plane->Right))
        return FALSE;
    if ((Y<Plane->Top) || (Y>Plane->Bottom))
        return FALSE;
    return TRUE;
}

/// <summary>
/// Merges a set of sub-planes into the final plane. Assumes that all sub-planes have had their coordinates validated and
/// corrected as necessary.
/// </summary>
/// <remarks>
/// This function assumes all planes have had their coordinates normalized to all non-negative values.
/// </remarks>
/// <param name="Target">The target buffer where there planes will be merged to.</param>
/// <param name="PlaneSet">Array of planes that will be merged.</param>
/// <param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
/// <param name="Width">Width of the target buffer.</param>
/// <param name="Height">Height of the target buffer.</param>
/// <param name="Stride">Stride of the target buffer.</param>
/// <returns>TRUE on success, FALSE on error.</returns>
__int32 MergePlanes(void *Target, void *PlaneSet, int PlaneCount, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride)
{
    if (PlaneCount < 1)
        return Error;
    if (Target == NULL)
        return NullPointer;
    if (PlaneSet == NULL)
        return NullPointer;

    BYTE* Buffer = (BYTE*)Target;
    PlaneSetStruct *Planes = (PlaneSetStruct *)PlaneSet;
    __int32 PixelSize = 4;
    UINT32 MaxValidIndex = (TargetStride * TargetHeight) - 1;

    for (int PlaneIndex = 0; PlaneIndex < PlaneCount; PlaneIndex++)
    {
        int PlanePointer = 0;
        UINT32 MaxPlaneIndex = (Planes[PlaneIndex].Stride * Planes[PlaneIndex].Height) - 1;
        BYTE* PlaneBuffer = (BYTE *)Planes[PlaneIndex].TheBits;
        for (int Row = Planes[PlaneIndex].Top; Row < Planes[PlaneIndex].Bottom; Row++)
        {
            if (Row < 0)
                return NegativeIndex;
            __int32 RowOffset = Row * TargetStride;
            for (int Column = Planes[PlaneIndex].Left; Column < Planes[PlaneIndex].Right; Column++)
            {
                if (Column < 0)
                    return NegativeIndex;
                if (Column > TargetWidth - 1)
                    return IndexOutOfRange;
                __int32 Index = RowOffset + (Column * PixelSize);
                if (Index < 0)
                    return NegativeIndex;
                if ((UINT32)(Index + PixelSize) > MaxValidIndex)
                    return IndexOutOfRange;
                if (((UINT32)PlanePointer + PixelSize) > (UINT32)Planes[PlaneIndex].PlaneSize)
                    return BadSecondaryIndex;
                Buffer[Index + 0] = PlaneBuffer[PlanePointer++];
                Buffer[Index + 1] = PlaneBuffer[PlanePointer++];
                Buffer[Index + 2] = PlaneBuffer[PlanePointer++];
                Buffer[Index + 3] = PlaneBuffer[PlanePointer++];
            }
        }
    }

    return Success;
}

/// <summary>
/// Merge a set of color blobs (in <paramref name="PlaneSet"/>) to <paramref name="Target"/>.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="PlaneSet">The list of color blobs to merge to <paramref name="Target"/>.</param>
/// <param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
/// <returns>Operational success indicator.</returns>
int MergePlanes2(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *PlaneSet, int PlaneCount)
{
    if (PlaneCount < 1)
        return Error;
    if (Target == NULL)
        return NullPointer;
    if (PlaneSet == NULL)
        return NullPointer;

    BYTE* Buffer = (BYTE*)Target;
    PlaneSetStruct *Planes = (PlaneSetStruct *)PlaneSet;
    __int32 PixelSize = 4;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            int OverlapCount = 0;
            double Final[] = { 0.0, 0.0, 0.0, 0.0 };
            BYTE MaxAlpha = 0;
            for (int BlobIndex = 0; BlobIndex < PlaneCount; BlobIndex++)
            {
                //Don't draw planes that are fully not within the view port.
                if (Row < Planes[BlobIndex].Top)
                    continue;
                if (Row > Planes[BlobIndex].Bottom)
                    continue;
                if (Column < Planes[BlobIndex].Left)
                    continue;
                if (Column > Planes[BlobIndex].Right)
                    continue;

                //Accumulate color blob point intersection data.
                int PlanePixelIndex = (Row - Planes[BlobIndex].Top) * Planes[BlobIndex].Stride;
                PlanePixelIndex += (Column - Planes[BlobIndex].Left) * PixelSize;
                BYTE *PlaneBuffer = (BYTE *)Planes[BlobIndex].TheBits;
                if (PlaneBuffer[PlanePixelIndex + 3] > MaxAlpha)
                    MaxAlpha = PlaneBuffer[PlanePixelIndex + 3];
#if TRUE
                if (
                    //                    (PlaneBuffer[PlanePixelIndex + 0] == 0x0) &&
                    //                    (PlaneBuffer[PlanePixelIndex + 1] == 0x0) &&
                    //                    (PlaneBuffer[PlanePixelIndex + 2] == 0x0) &&
                    (PlaneBuffer[PlanePixelIndex + 3] == 0x0)
                    )
                    continue;
#endif
#if FALSE
                double ARatio = (double)PlaneBuffer[PlanePixelIndex + 3] / 255.0;
                Final[0] += ((double)PlaneBuffer[PlanePixelIndex + 0] * ARatio);
                Final[1] += ((double)PlaneBuffer[PlanePixelIndex + 1] * ARatio);
                Final[2] += ((double)PlaneBuffer[PlanePixelIndex + 2] * ARatio);
                Final[3] += ((double)PlaneBuffer[PlanePixelIndex + 3] * ARatio);
#else
                Final[0] += ((double)PlaneBuffer[PlanePixelIndex + 0] / 255.0);
                Final[1] += ((double)PlaneBuffer[PlanePixelIndex + 1] / 255.0);
                Final[2] += ((double)PlaneBuffer[PlanePixelIndex + 2] / 255.0);
                Final[3] += ((double)PlaneBuffer[PlanePixelIndex + 3] / 255.0);
#endif

                OverlapCount++;
            }
            if (OverlapCount > 0)
            {
                Final[0] /= (double)OverlapCount;
                Final[1] /= (double)OverlapCount;
                Final[2] /= (double)OverlapCount;
                Final[3] /= (double)OverlapCount;
#if FALSE
                BYTE FinalB = Final[0];
                BYTE FinalG = Final[1];
                BYTE FinalR = Final[2];
                BYTE FinalA = Final[3];
#else
                BYTE FinalB = (Final[0] * 255.0);
                BYTE FinalG = (Final[1] * 255.0);
                BYTE FinalR = (Final[2] * 255.0);
                BYTE FinalA = (Final[3] * 255.0);
#endif
                Buffer[Index + 0] = FinalB;
                Buffer[Index + 1] = FinalG;
                Buffer[Index + 2] = FinalR;
                Buffer[Index + 3] = FinalA;
            }
        }
    }

    return Success;
}

/// <summary>
/// Merge a set of color blobs (in <paramref name="PlaneSet"/>) to <paramref name="Target"/>. This is a composite action - the
/// order of the planes in the list is relevant - first items are composited first. The background in <paramref name="Target"/>
/// will be merged with the planes and is assumed to be drawn before calling this function.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="PlaneSet">The list of color blobs to merge to <paramref name="Target"/>.</param>
/// <param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
/// <returns>Operational success indicator.</returns>
int MergePlanes4(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *PlaneSet, int PlaneCount,
    void *Results)
{
    if (Target == NULL)
        return NullPointer;
    if (PlaneSet == NULL)
        return NullPointer;
    if (PlaneCount < 1)
        return Error;
    BOOL CollectResults = Results == NULL ? false : true;

    BYTE* Buffer = (BYTE*)Target;
    PlaneSetStruct *Planes = (PlaneSetStruct *)PlaneSet;
    DrawnObject* ObjResults = (DrawnObject *)Results;
    __int32 PixelSize = 4;

    for (int BlobIndex = 0; BlobIndex < PlaneCount; BlobIndex++)
    {
        if (CollectResults)
        {
            ObjResults[BlobIndex].IsValid = TRUE;
            ObjResults[BlobIndex].TargetHeight = TargetHeight;
            ObjResults[BlobIndex].TargetWidth = TargetWidth;
            ObjResults[BlobIndex].TargetStride = TargetStride;
            ObjResults[BlobIndex].ObjectOrder = BlobIndex;
            ObjResults[BlobIndex].X1 = Planes[BlobIndex].Left;
            ObjResults[BlobIndex].Y1 = Planes[BlobIndex].Top;
            ObjResults[BlobIndex].X2 = Planes[BlobIndex].Right;
            ObjResults[BlobIndex].Y2 = Planes[BlobIndex].Bottom;
            ObjResults[BlobIndex].Width = Planes[BlobIndex].Width;
            ObjResults[BlobIndex].Height = Planes[BlobIndex].Height;
            ObjResults[BlobIndex].LeftOut = Planes[BlobIndex].Left < 0 ? TRUE : FALSE;
            ObjResults[BlobIndex].TopOut = Planes[BlobIndex].Top < 0 ? TRUE : FALSE;
            ObjResults[BlobIndex].RightOut = Planes[BlobIndex].Right > TargetWidth - 1 ? TRUE : FALSE;
            ObjResults[BlobIndex].BottomOut = Planes[BlobIndex].Bottom > TargetHeight - 1 ? TRUE : FALSE;
        }

        //Get a pointer to the plane's bits.
        BYTE *PlaneBuffer = (BYTE *)Planes[BlobIndex].TheBits;

        for (int Row = 0; Row < Planes[BlobIndex].Height; Row++)
        {
            if (Row + Planes[BlobIndex].Top < 0)
                continue;
            if (Row + Planes[BlobIndex].Top > TargetHeight - 1)
                continue;

            UINT32 BufferRowOffset = (Row + Planes[BlobIndex].Top) * TargetStride;
            UINT32 PlaneOffset = (Row * Planes[BlobIndex].Stride);
            for (int Column = 0; Column < Planes[BlobIndex].Width; Column++)
            {
                if (Column + Planes[BlobIndex].Left > TargetWidth - 1)
                    continue;
                if (Column + Planes[BlobIndex].Left < 0)
                    continue;

                UINT32 BufferIndex = BufferRowOffset + ((Column + Planes[BlobIndex].Left) * PixelSize);
                UINT32 PlaneIndex = PlaneOffset + (Column * PixelSize);

                BYTE FGBlue = PlaneBuffer[PlaneIndex + 0];
                BYTE FGGreen = PlaneBuffer[PlaneIndex + 1];
                BYTE FGRed = PlaneBuffer[PlaneIndex + 2];
                BYTE FGAlpha = PlaneBuffer[PlaneIndex + 3];
                //If there's nothing to draw skip calculations and buffer assignment and move to the next pixel.
                if (FGAlpha == 0x0)
                    continue;
                BYTE BGBlue = Buffer[BufferIndex + 0];
                BYTE BGGreen = Buffer[BufferIndex + 1];
                BYTE BGRed = Buffer[BufferIndex + 2];
                //BYTE BGAlpha = Buffer[BufferIndex + 3];
                double FAlpha = (double)FGAlpha / 255.0;
                double InvertedAlpha = 1.0 - FAlpha;
                BYTE FinalBlue = (BYTE)(FAlpha * FGBlue) + (BYTE)(InvertedAlpha * BGBlue);
                BYTE FinalGreen = (BYTE)(FAlpha * FGGreen) + (BYTE)(InvertedAlpha * BGGreen);
                BYTE FinalRed = (BYTE)(FAlpha * FGRed) + (BYTE)(InvertedAlpha * BGRed);
                Buffer[BufferIndex + 0] = FinalBlue;
                Buffer[BufferIndex + 1] = FinalGreen;
                Buffer[BufferIndex + 2] = FinalRed;
                Buffer[BufferIndex + 3] = 0xff;
            }
        }
    }

    return Success;
}

/// <summary>
/// Merge a set of color blobs (in <paramref name="PlaneSet"/>) to <paramref name="Target"/>.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="PlaneSet">The list of color blobs to merge to <paramref name="Target"/>.</param>
/// <param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
/// <returns>Operational success indicator.</returns>
int MergePlanes3(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *PlaneSet, int PlaneCount)
{
    if (PlaneCount < 1)
        return Error;
    if (Target == NULL)
        return NullPointer;
    if (PlaneSet == NULL)
        return NullPointer;

    BYTE* Buffer = (BYTE*)Target;
    PlaneSetStruct *Planes = (PlaneSetStruct *)PlaneSet;
    __int32 PixelSize = 4;

    for (int PlaneIndex = 0; PlaneIndex < PlaneCount; PlaneIndex++)
    {
        int PlanePixelIndex = 0;
        BYTE *PlaneBuffer = (BYTE *)Planes[PlaneIndex].TheBits;
        for (int PlaneRow = Planes[PlaneIndex].Top; PlaneRow < Planes[PlaneIndex].Bottom; PlaneRow++)
        {
            if (PlaneRow < 0)
                continue;
            if (PlaneRow > TargetHeight - 1)
                continue;
            int PlaneRowOffset = PlaneRow * Planes[PlaneIndex].Stride;
            for (int PlaneColumn = Planes[PlaneIndex].Left; PlaneColumn < Planes[PlaneIndex].Right; PlaneColumn++)
            {
                if (PlaneColumn < 0)
                    continue;
                if (PlaneColumn > TargetWidth - 1)
                    continue;
                int TargetIndex = PlaneRowOffset + (PlaneColumn * PixelSize);
                //Merge plane pixel with target pixel.
                // Final = FG + BG * (1[0xff] - FG(alpha))
                Buffer[TargetIndex + 0] = PlaneBuffer[PlanePixelIndex + 0] + (Buffer[TargetIndex + 0] * (255 - PlaneBuffer[PlanePixelIndex]));
                Buffer[TargetIndex + 1] = PlaneBuffer[PlanePixelIndex + 1] + (Buffer[TargetIndex + 1] * (255 - PlaneBuffer[PlanePixelIndex]));
                Buffer[TargetIndex + 2] = PlaneBuffer[PlanePixelIndex + 2] + (Buffer[TargetIndex + 2] * (255 - PlaneBuffer[PlanePixelIndex]));
                Buffer[TargetIndex + 3] = PlaneBuffer[PlanePixelIndex + 3] + (Buffer[TargetIndex + 3] * (255 - PlaneBuffer[PlanePixelIndex]));
            }
        }
    }

    return Success;
}

/// <summary>
/// Create a bit mask.
/// </summary>
/// <param name="Target">The buffer of bytes that make up the bit mask.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="Left">The left coordinate of the start of the masked region.</param>
/// <param name="Top">The top coordinate of the start of the masked region.</param>
/// <param name="Width">The width of the masked region.</param>
/// <param name="Height">The height of the masked region.</param>
/// <param name="BitOnValue">The value written to the masked region.</param>
/// <param name="BitOffValue">The value written to the unmasked region.</param>
BOOL CreateBitMask(void *Target, __int32 TargetWidth, __int32 TargetHeight,
    __int32 Left, __int32 Top, __int32 Width, __int32 Height, BYTE BitOnValue, BYTE BitOffValue)
{
    if (Target == NULL)
        return FALSE;

    BYTE* Buffer = (BYTE*)Target;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            BYTE MaskValue = BitOnValue;
            if (Column < Left)
                MaskValue = BitOffValue;
            if (Column > Left + Width)
                MaskValue = BitOffValue;
            if (Row < Top)
                MaskValue = BitOffValue;
            if (Row > Top + Height)
                MaskValue = BitOffValue;
            int Index = (Row * TargetHeight) + Column;
            Buffer[Index] = MaskValue;

        }
    }
    return TRUE;
}

/// <summary>
/// Create a mask from image data in <paramref name="ImageSource"/> and return the masked image in <paramref name="Target"/>.
/// </summary>
/// <param name="Target">Will contain the masked image on success.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="ImageSource">
/// The source image that determines the resultant mask. Must have the same dimensions and stride as <paramref name="Target"/>.
/// </param>
/// <param name="Threshold">
/// Determines if a pixel from <paramref name="ImageSource"/> or the mask values is written to <paramref name="Target"/>. If any
/// color channel value is less than the corresponding color channel value in this parameter, the mask value is written. This parameter
/// is in BGRA format.
/// </param>
/// <param name="AlphaToo">Determines if alpha values are used in determination of target mask values.</param>
/// <param name="MaskA">
/// The alpha value to write if the source pixel is less than <paramref name="Threshold"/>. If <paramref name="AlphaToo"/> is false,
/// 0x0 will be used as the alpha value.
/// </param>
/// <param name="MaskR">The red channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
/// <param name="MaskG">The green channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
/// <param name="MaskB">The blue channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
/// <returns>True on success, false on error.</returns>
BOOL CreateMask(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *ImageSource, UINT32 Threshold, BOOL AlphaToo,
    BYTE MaskA, BYTE MaskR, BYTE MaskG, BYTE MaskB)
{
    if (Target == NULL)
        return FALSE;
    if (ImageSource == NULL)
        return FALSE;

    BYTE PixelSize = 4;
    BYTE *Buffer = (BYTE *)Target;
    BYTE *Source = (BYTE *)ImageSource;
    BYTE AThreshold = (Threshold & 0x000000ff) >> 0;
    BYTE RThreshold = (Threshold & 0x0000ff00) >> 8;
    BYTE GThreshold = (Threshold & 0x00ff0000) >> 16;
    BYTE BThreshold = (Threshold & 0xff000000) >> 24;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + Column * PixelSize;
            if (
                (Source[Index + 0] < BThreshold) ||
                (Source[Index + 1] < GThreshold) ||
                (Source[Index + 2] < RThreshold) ||
                ((Source[Index + 3] < AThreshold) && AlphaToo)
                )
            {
                Buffer[Index + 0] = MaskB;
                Buffer[Index + 1] = MaskG;
                Buffer[Index + 2] = MaskR;
                if (AlphaToo)
                    Buffer[Index + 3] = MaskA;
                else
                    Buffer[Index + 3] = 0x0;
            }
            else
            {
                Buffer[Index + 0] = Source[Index + 0];
                Buffer[Index + 1] = Source[Index + 1];
                Buffer[Index + 2] = Source[Index + 2];
                Buffer[Index + 3] = Source[Index + 3];
            }
        }
    }

    return TRUE;
}

/// <summary>
/// Create a mask from image data in <paramref name="ImageSource"/> and return the masked image in <paramref name="Target"/>.
/// </summary>
/// <param name="Target">Will contain the masked image on success.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="ImageSource">
/// The source image that determines the resultant mask. Must have the same dimensions and stride as <paramref name="Target"/>.
/// </param>
/// <param name="Threshold">
/// The luninance threshold. Source pixels with a luminance less than this value will not be included in the returned buffer.
/// </param>
/// <param name="AlphaToo">Determines if alpha values are used in determination of target mask values.</param>
/// <param name="MaskA">
/// The alpha value to write if the source pixel is less than <paramref name="Threshold"/>. If <paramref name="AlphaToo"/> is false,
/// 0x0 will be used as the alpha value.
/// </param>
/// <param name="MaskR">The red channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
/// <param name="MaskG">The green channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
/// <param name="MaskB">The blue channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
/// <returns>True on success, false on error.</returns>
BOOL CreateMaskFromLuminance(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *ImageSource, double Threshold, BOOL AlphaToo,
    BYTE MaskA, BYTE MaskR, BYTE MaskG, BYTE MaskB)
{
    if (Target == NULL)
        return FALSE;
    if (ImageSource == NULL)
        return FALSE;

    BYTE PixelSize = 4;
    BYTE *Buffer = (BYTE *)Target;
    BYTE *Source = (BYTE *)ImageSource;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + Column * PixelSize;
            double SourceLuminance = PixelLuminance(Source[Index + 2], Source[Index + 1], Source[Index + 0]);
            if (SourceLuminance < Threshold)
            {
                Buffer[Index + 0] = MaskB;
                Buffer[Index + 1] = MaskG;
                Buffer[Index + 2] = MaskR;
                if (AlphaToo)
                    Buffer[Index + 3] = MaskA;
                else
                    Buffer[Index + 3] = 0x0;
            }
            else
            {
                Buffer[Index + 0] = Source[Index + 0];
                Buffer[Index + 1] = Source[Index + 1];
                Buffer[Index + 2] = Source[Index + 2];
                Buffer[Index + 3] = Source[Index + 3];
            }
        }
    }

    return TRUE;
}

/// <summary>
/// Create a mask based on the luminance of a given pixel. Masked pixel alpha value not changed.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Stride">Stride of the source and destination images.</param>
/// <param name="Luminance">Determines if a pixel is part of the mask or not. If not, the pixel is saved as transparent in the destination.</param>
/// <param name="UseMaskedPixel">
/// If TRUE, <paramref name="MaskedPixelColor"/> is used for pixels that meet the luminance requirement, otherwise
/// the original pixel is used.
/// </param>
/// <returns>Value indicating operational results.</returns>
int CreateAlphaMaskFromLuminance(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    double Luminance, BOOL UseMaskedPixel, UINT32 MaskedPixelColor)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;
    BYTE mR = (MaskedPixelColor & 0x00ff0000) >> 16;
    BYTE mG = (MaskedPixelColor & 0x0000ff00) >> 8;
    BYTE mB = (MaskedPixelColor & 0x000000ff) >> 0;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            double PixLuminance = ColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
            if (PixLuminance > Luminance)
            {
                if (UseMaskedPixel)
                {
                    Dest[Index + 3] = Src[Index + 3];
                    Dest[Index + 2] = mR;
                    Dest[Index + 1] = mG;
                    Dest[Index + 0] = mB;
                }
                else
                {
                    Dest[Index + 3] = Src[Index + 3];
                    Dest[Index + 2] = Src[Index + 2];
                    Dest[Index + 1] = Src[Index + 1];
                    Dest[Index + 0] = Src[Index + 0];
                }
            }
            else
            {
                Dest[Index + 3] = 0x0;
                Dest[Index + 2] = 0xff;
                Dest[Index + 1] = 0xff;
                Dest[Index + 0] = 0xff;
            }
        }
    }

    return Success;
}

/// <summary>
/// Clears the specified buffer with the specified color value. Optionally draws a grid.
/// </summary>
/// <param name="Target">The buffer to clear.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="ClearA">The alpha channel value for a cleared color.</param>
/// <param name="ClearR">The red channel value for a cleared color.</param>
/// <param name="ClearG">The green channel value for a cleared color.</param>
/// <param name="ClearB">The blue channel value for a cleared color.</param>
/// <param name="DrawGrid">Determines if a grid is drawn over the cleared buffer.</param>
/// <param name="GridA">The alpha channel value the grid color.</param>
/// <param name="GridR">The red channel value the grid color.</param>
/// <param name="GridG">The green channel value the grid color.</param>
/// <param name="GridB">The blue channel value the grid color.</param>
/// <param name="GridCellWidth">Horizontal distance between grid lines.</param>
/// <param name="GridCellHeight">Vertical distance between grid lines.</param>
/// <returns>True on success, false on error.</returns>
BOOL ClearBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    BYTE ClearA, BYTE ClearR, BYTE ClearG, BYTE ClearB,
    BOOL DrawGrid, BYTE GridA, BYTE GridR, BYTE GridG, BYTE GridB, __int32 GridCellWidth, __int32 GridCellHeight,
    BOOL DrawOutline, BYTE OutA, BYTE OutR, BYTE OutG, BYTE OutB)
{
    if (Target == NULL)
        return FALSE;

    BYTE PixelSize = 4;
    BYTE* Buffer = (BYTE*)Target;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            if (DrawOutline)
            {
                if (Row == 0 || Row == TargetHeight - 1 || Column == 0 || Column == TargetWidth - 1)
                {
                    Buffer[Index + 0] = OutB;
                    Buffer[Index + 1] = OutG;
                    Buffer[Index + 2] = OutR;
                    Buffer[Index + 3] = OutA;
                    continue;
                }
            }
            if (DrawGrid)
            {
                if (
                    (Row % GridCellHeight == 0) ||
                    (Column % GridCellWidth == 0)
                    )
                {
                    Buffer[Index + 0] = GridB;
                    Buffer[Index + 1] = GridG;
                    Buffer[Index + 2] = GridR;
                    Buffer[Index + 3] = GridA;
                    continue;
                }
            }

            Buffer[Index + 0] = ClearB;
            Buffer[Index + 1] = ClearG;
            Buffer[Index + 2] = ClearR;
            Buffer[Index + 3] = ClearA;
        }
    }

    return TRUE;
}

int FillBufferWithBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    UINT32 PackedBGColor)
{
    if (Target == NULL)
        return NullPointer;
    if (Source == NULL)
        return NullPointer;

    int PixelSize = 4;
    BYTE * TargetBuffer = (BYTE *)Target;
    BYTE * SourceBuffer = (BYTE *)Source;
    BYTE DefaultA = (PackedBGColor & 0xff000000) >> 24;
    BYTE DefaultR = (PackedBGColor & 0x00ff0000) >> 16;
    BYTE DefaultG = (PackedBGColor & 0x0000ff00) >> 8;
    BYTE DefaultB = (PackedBGColor & 0x000000ff) >> 0;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int TargetRowIndex = Row * TargetStride;
        int SourceRowIndex = Row * SourceStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int TargetIndex = TargetRowIndex + (Column * PixelSize);
            if (Column > SourceWidth - 1)
            {
                TargetBuffer[TargetIndex + 0] = DefaultB;
                TargetBuffer[TargetIndex + 1] = DefaultG;
                TargetBuffer[TargetIndex + 2] = DefaultR;
                TargetBuffer[TargetIndex + 3] = DefaultA;
            }
            else
            {
                int SourceIndex = SourceRowIndex + (Column * PixelSize);
                TargetBuffer[TargetIndex + 0] = SourceBuffer[SourceIndex + 0];
                TargetBuffer[TargetIndex + 1] = SourceBuffer[SourceIndex + 1];
                TargetBuffer[TargetIndex + 2] = SourceBuffer[SourceIndex + 2];
                TargetBuffer[TargetIndex + 3] = SourceBuffer[SourceIndex + 3];
            }
        }
    }

    return Success;
}

/*
void BlendPixelColors (BYTE SourceA, BYTE SourceR, BYTE SourceG, BYTE SourceB,
BYTE DestinationA, BYTE DestinationR, BYTE DestinationG, BYTE DestinationB,
BYTE *FinalA, BYTE* FinalR, BYTE* FinalG, BYTE* FinalB)
{
double DestinationAlpha = (double) DestinationA / 255.0;
*FinalA = DestinationA + (SourceA * (255 - DestinationA));
*FinalR = (BYTE) ((DestinationAlpha*(double) DestinationR) + ((DestinationAlpha *(double) SourceR) * (255 - DestinationR)));
*FinalR = (BYTE) ((DestinationAlpha*(double) DestinationG) + ((DestinationAlpha *(double) SourceG) * (255 - DestinationR)));
*FinalR = (BYTE) ((DestinationAlpha*(double) DestinationB) + ((DestinationAlpha *(double) SourceB) * (255 - DestinationR)));
}

//in BGRA format
//http://stackoverflow.com/questions/12011081/alpha-blending-2-rgba-colors-in-c
void BlendPixelColors2 (BYTE Final[4], BYTE Foreground[4], BYTE Background[4])
{
BYTE Alpha = Foreground[3] + 1;
BYTE InvertedAlpha = 255 - Foreground[3];
Final[0] = ((Alpha * Foreground[0]) + (InvertedAlpha * Background[0]) >> 8);
Final[1] = ((Alpha * Foreground[1]) + (InvertedAlpha * Background[1]) >> 8);
Final[2] = ((Alpha * Foreground[2]) + (InvertedAlpha * Background[2]) >> 8);
Final[3] = ((Alpha * Foreground[3]) + (InvertedAlpha * Background[3]) >> 8);
}
*/

/// <summary>
/// Crops the source buffer as per the size in the region and returns the result in the target buffer. The cropped region will
/// be aligned to the upper-left corder in the target buffer on completion.
/// </summary>
/// <param name="Target">Will contain the cropped buffer.</param>
/// <param name="TargetWidth">The width of the target buffer.</param>
/// <param name="TargetHeight">The height of the target buffer.</param>
/// <param name="TargetStride">The stride of the target buffer.</param>
/// <param name="Source">The source buffer.</param>
/// <param name="SourceWidth">The width of the source buffer.</param>
/// <param name="SourceHeight">The height of the source buffer.</param>
/// <param name="SourceStride">The stride of the source buffer.</param>
/// <param name="RegionPtr">
/// Determines the final region after the crop. This function assumes the values in Region have been validated by the caller.
/// </param>
/// <returns>TRUE on success, FALSE on failure.</returns>
int CropBuffer(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void* RegionPtr)
{
    if (Target == NULL)
        return NullPointer;
    if (Source == NULL)
        return NullPointer;
    if (RegionPtr == NULL)
        return NullPointer;

    RegionStruct* Region = (RegionStruct*)RegionPtr;
    BYTE* TargetBuffer = (BYTE *)Target;
    BYTE* SourceBuffer = (BYTE *)Source;
    int PixelSize = 4;
    UINT32 MaxValidTargetIndex = ((TargetWidth * TargetStride) * TargetHeight) - 1;
    UINT32 MaxValidSourceIndex = ((SourceWidth * SourceHeight) * SourceStride) - 1;

    int TargetIndex = 0;
    for (int Row = Region->Left; Row >= Region->Right; Row++)
    {
        int RowOffset = Row * SourceStride;
        for (int Column = Region->Top; Column >= Region->Bottom; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            if ((UINT32)(Index + 3) > MaxValidSourceIndex)
                return InvalidOperation;
            if ((UINT32)(TargetIndex + 3) > MaxValidTargetIndex)
                return InvalidOperation;
            TargetBuffer[TargetIndex++] = SourceBuffer[Index + 0];
            TargetBuffer[TargetIndex++] = SourceBuffer[Index + 1];
            TargetBuffer[TargetIndex++] = SourceBuffer[Index + 2];
            TargetBuffer[TargetIndex++] = SourceBuffer[Index + 3];
        }
    }

    return Success;
}

/// <summary>
/// Sets the alpha level of all pixels in <paramref name="Target"/> to <paramref name="NewAlpha"/>.
/// </summary>
/// <param name="Target">The buffer where the alpha levels will be set.</param>
/// <param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
/// <param name="TargetHeight">The height of the target in scanlines.</param>
/// <param name="TargetStride">The stride of the target.</param>
/// <param name="NewAlpha">The new alpha value.</param>
/// <returns>Value that indicates the result of the operation.</returns>
int SetAlpha(void* Target, int TargetWidth, int TargetHeight, int TargetStride, byte NewAlpha)
{
    if (Target == NULL)
        return NullPointer;

    BYTE* Buffer = (BYTE *)Target;
    BYTE PixelSize = 4;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize) + 3;
            Buffer[Index + 3] = NewAlpha;
        }
    }

    return Success;
}

/// <summary>
/// Sets the alpha level of all pixels in <paramref name="Target"/> to <paramref name="NewAlpha"/>.
/// </summary>
/// <param name="Target">The buffer where the alpha levels will be set.</param>
/// <param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
/// <param name="TargetHeight">The height of the target in scanlines.</param>
/// <param name="TargetStride">The stride of the target.</param>
/// <param name="Invert">Determines if the brightness ratio is inverted.</param>
/// <param name="UseExistingAlpha">If true, the current alpha level is used as the base, if false, 0xff is used as the base.</param>
/// <returns>Value that indicates the result of the operation.</returns>
int SetAlphaByBrightness(void* Target, int TargetWidth, int TargetHeight, int TargetStride, BOOL Invert, BOOL UseExistingAlpha)
{
    if (Target == NULL)
        return NullPointer;

    BYTE* Buffer = (BYTE *)Target;
    BYTE PixelSize = 4;
    BYTE A, R, G, B;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            B = Buffer[Index + 0];
            G = Buffer[Index + 1];
            R = Buffer[Index + 2];
            A = Buffer[Index + 3];
            double Luminance = PixelLuminance(R, G, B);
            double Ratio = Luminance / 255.0;
            if (Invert)
                Ratio = 1.0 - Ratio;
            BYTE NewAlpha = 0;
            if (UseExistingAlpha)
                A = (BYTE)((double)A*Ratio);
            else
                A = (BYTE)(255.0 * Ratio);
            Buffer[Index + 3] = A;
        }
    }

    return Success;
}

const __int32 AndMask = 0;
const __int32 OrMask = 1;
const __int32 XorMask = 2;

/// <summary>
/// Apply a mask to each specified channel in <paramref name="Source"/> and save the result to <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source image that will be manipulated.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="LogicalOperator">Determines the logical operation to apply.</param>
/// <param name="AlphaMask">The mask to apply to the alpha channel.</param>
/// <param name="UseAlpha">If TRUE, <paramref name="AlphaMask"/> is applied to the alpha channel. If not, alpha is not modified.</param>
/// <param name="RedMask">The mask to apply to the red channel.</param>
/// <param name="UseRed">If TRUE, <paramref name="RedMask"/> is applied to the red channel. If not, red is not modified.</param>
/// <param name="GreenMask">The mask to apply to the green channel.</param>
/// <param name="UseGreen">If TRUE, <paramref name="GreenMask"/> is applied to the green channel. If not, green is not modified.</param>
/// <param name="BlueMask">The mask to apply to the blue channel.</param>
/// <param name="UseBlue">If TRUE, <paramref name="BlueMask"/> is applied to the blue channel. If not, blue is not modified.</param>
/// <returns>Value indicating operational results.</returns>
int ApplyChannelMasks2(void *Source, int Width, int Height, int Stride, void *Destination,
    int LogicalOperator, BYTE AlphaMask, BOOL UseAlpha, BYTE RedMask, BOOL UseRed, BYTE GreenMask,
    BOOL UseGreen, BYTE BlueMask, BOOL UseBlue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE* Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE PixelSize = 4;

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
            switch (LogicalOperator)
            {
                case AndMask:
                    if (UseAlpha)
                        A = Src[Index + 3] & AlphaMask;
                    if (UseRed)
                        R = Src[Index + 2] & RedMask;
                    if (UseGreen)
                        G = Src[Index + 1] & GreenMask;
                    if (UseBlue)
                        B = Src[Index + 0] & BlueMask;
                    break;

                case OrMask:
                    if (UseAlpha)
                        A = Src[Index + 3] | AlphaMask;
                    if (UseRed)
                        R = Src[Index + 2] | RedMask;
                    if (UseGreen)
                        G = Src[Index + 1] | GreenMask;
                    if (UseBlue)
                        B = Src[Index + 0] | BlueMask;
                    break;

                case XorMask:
                    if (UseAlpha)
                        A = Src[Index + 3] ^ AlphaMask;
                    if (UseRed)
                        R = Src[Index + 2] ^ RedMask;
                    if (UseGreen)
                        G = Src[Index + 1] ^ GreenMask;
                    if (UseBlue)
                        B = Src[Index + 0] ^ BlueMask;
                    break;

                default:
                    return InvalidOperation;
            }

            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return Success;
}

/// <summary>
/// Apply a mask to each specified channel in <paramref name="Source"/> and save the result to <paramref name="Destination"/>.
/// The same mask is applied to each channel.
/// </summary>
/// <param name="Source">Source image that will be manipulated.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="LogicalOperator">Determines the logical operation to apply.</param>
/// <param name="AlphaMask">The mask to apply to the alpha channel.</param>
/// <param name="RedMask">The mask to apply to the red channel.</param>
/// <param name="GreenMask">The mask to apply to the green channel.</param>
/// <param name="BlueMask">The mask to apply to the blue channel.</param>
/// <param name="IncludeAlpha">If TRUE, alpha is modified with the mask, otherwise, it is left alone.</param>
/// <returns>Value indicating operational results.</returns>
int ApplyChannelMasks(void *Source, int Width, int Height, int Stride, void *Destination,
    int LogicalOperator, BYTE AlphaMask, BYTE RedMask, BYTE GreenMask, BYTE BlueMask, BOOL IncludeAlpha)
{
    return ApplyChannelMasks2(Source, Width, Height, Stride, Destination,
        LogicalOperator, AlphaMask, IncludeAlpha, RedMask, TRUE, GreenMask, TRUE, BlueMask, TRUE);
}

const __int32 RollingChannelAnd = 0;
const __int32 RollingChannelOr = 1;
const __int32 RollingChannelXor = 2;

/// <summary>
/// Perform rolling logical operations on channels inside given pixels.
/// </summary>
/// <param name="Source">Source image that will be manipulated.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="LogicalOperator">Determines the logical operation to apply.</param>
/// <param name="RightToLeft">Determines the channel order. If TRUE, channel order is ARGB, otherwise the channel order is BGRA.</param>
/// <param name="Mask">Mask value applied to keep some bits unchanged.</param>
/// <param name="IncludeAlpha">If TRUE, alpha is manipulated as well.</param>
/// <returns>Value indicating operational results.</returns>
int PixelChannelRollingLogicalOperation(void *Source, int Width, int Height, int Stride, void *Destination,
    int LogicalOperator, BOOL RightToLeft, BYTE Mask, BOOL IncludeAlpha)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE* Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE PixelSize = 4;

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
            BYTE As = A;
            BYTE Rs = 0;
            BYTE Gs = 0;
            BYTE Bs = 0;

            switch (LogicalOperator)
            {
                case RollingChannelAnd:
                    if (IncludeAlpha)
                    {
                        if (RightToLeft)
                        {
                            As = ((B & Mask) & (A & Mask)) | (A & ~Mask);
                            Rs = ((A & Mask) & (R & Mask)) | (R & ~Mask);
                            Gs = ((R & Mask) & (G & Mask)) | (G & ~Mask);
                            Bs = ((G & Mask) & (B & Mask)) | (B & ~Mask);
                        }
                        else
                        {
                            As = ((R & Mask) & (A & Mask)) | (A & ~Mask);
                            Rs = ((G & Mask) & (R & Mask)) | (R & ~Mask);
                            Gs = ((B & Mask) & (G & Mask)) | (G & ~Mask);
                            Bs = ((A & Mask) & (B & Mask)) | (B & ~Mask);
                        }
                    }
                    else
                    {
                        if (RightToLeft)
                        {
                            Rs = ((B & Mask) & (R & Mask)) | (R & ~Mask);
                            Gs = ((R & Mask) & (G & Mask)) | (G & ~Mask);
                            Bs = ((G & Mask) & (B & Mask)) | (B & ~Mask);
                        }
                        else
                        {
                            Rs = ((G & Mask) & (R & Mask)) | (R & ~Mask);
                            Gs = ((B & Mask) & (G & Mask)) | (G & ~Mask);
                            Bs = ((R & Mask) & (B & Mask)) | (B & ~Mask);
                        }
                    }
                    break;

                case RollingChannelOr:
                    if (IncludeAlpha)
                    {
                        if (RightToLeft)
                        {
                            As = ((B & Mask) | (A & Mask)) | (A & ~Mask);
                            Rs = ((A & Mask) | (R & Mask)) | (R & ~Mask);
                            Gs = ((R & Mask) | (G & Mask)) | (G & ~Mask);
                            Bs = ((G & Mask) | (B & Mask)) | (B & ~Mask);
                        }
                        else
                        {
                            As = ((R & Mask) | (A & Mask)) | (A & ~Mask);
                            Rs = ((G & Mask) | (R & Mask)) | (R & ~Mask);
                            Gs = ((B & Mask) | (G & Mask)) | (G & ~Mask);
                            Bs = ((A & Mask) | (B & Mask)) | (B & ~Mask);
                        }
                    }
                    else
                    {
                        if (RightToLeft)
                        {
                            Rs = ((B & Mask) | (R & Mask)) | (R & ~Mask);
                            Gs = ((R & Mask) | (G & Mask)) | (G & ~Mask);
                            Bs = ((G & Mask) | (B & Mask)) | (B & ~Mask);
                        }
                        else
                        {
                            Rs = ((G & Mask) | (R & Mask)) | (R & ~Mask);
                            Gs = ((B & Mask) | (G & Mask)) | (G & ~Mask);
                            Bs = ((R & Mask) | (B & Mask)) | (B & ~Mask);
                        }
                    }
                    break;

                case RollingChannelXor:
                    if (IncludeAlpha)
                    {
                        if (RightToLeft)
                        {
                            As = ((B & Mask) ^ (A & Mask)) ^ (A & ~Mask);
                            Rs = ((A & Mask) ^ (R & Mask)) ^ (R & ~Mask);
                            Gs = ((R & Mask) ^ (G & Mask)) ^ (G & ~Mask);
                            Bs = ((G & Mask) ^ (B & Mask)) ^ (B & ~Mask);
                        }
                        else
                        {
                            As = ((R & Mask) ^ (A & Mask)) ^ (A & ~Mask);
                            Rs = ((G & Mask) ^ (R & Mask)) ^ (R & ~Mask);
                            Gs = ((B & Mask) ^ (G & Mask)) ^ (G & ~Mask);
                            Bs = ((A & Mask) ^ (B & Mask)) ^ (B & ~Mask);
                        }
                    }
                    else
                    {
                        if (RightToLeft)
                        {
                            Rs = ((B & Mask) ^ (R & Mask)) ^ (R & ~Mask);
                            Gs = ((R & Mask) ^ (G & Mask)) ^ (G & ~Mask);
                            Bs = ((G & Mask) ^ (B & Mask)) ^ (B & ~Mask);
                        }
                        else
                        {
                            Rs = ((G & Mask) ^ (R & Mask)) ^ (R & ~Mask);
                            Gs = ((B & Mask) ^ (G & Mask)) ^ (G & ~Mask);
                            Bs = ((R & Mask) ^ (B & Mask)) ^ (B & ~Mask);
                        }
                    }
                    break;

                default:
                    return InvalidOperation;
            }

            Dest[Index + 3] = As;
            Dest[Index + 2] = Rs;
            Dest[Index + 1] = Gs;
            Dest[Index + 0] = Bs;
        }
    }

    return Success;
}

/// <summary>
/// Does a variable color inversion of an image.
/// </summary>
/// <param name="Source">Source image to invert.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="InvertAlpha">If TRUE, the alpha channel is always inverted.</param>
/// <param name="InvertRed">If TRUE, the red channel is always inverted.</param>
/// <param name="InvertGreen">If TRUE, the green channel is always inverted.</param>
/// <param name="InvertBlue">If TRUE, the blue channel is always inverted.</param>
/// <param name="UseAlphaThreshold">
/// If TRUE (and if <paramref name="InvertAlpha"/> is TRUE), the alpha channel will be inverted if
/// the source alpha channel value is greater than <paramref name="AlphaThreshold"/>.
/// </param>
/// <param name="AlphaThreshold">The value that determines if the alpha channel is inverted if <paramref name="UseAlphaThreshold"/> is TRUE.</param>
/// <param name="UseRedThreshold">
/// If TRUE (and if <paramref name="InvertRed"/> is TRUE), the red channel will be inverted if
/// the source red channel value is greater than <paramref name="RedThreshold"/>.
/// </param>
/// <param name="RedThreshold">The value that determines if the red channel is inverted if <paramref name="UseRedThreshold"/> is TRUE.</param>
/// <param name="UseGreenThreshold">
/// If TRUE (and if <paramref name="InvertGreen"/> is TRUE), the green channel will be inverted if
/// the green red channel value is greater than <paramref name="GreenThreshold"/>.
/// </param>
/// <param name="GreenThreshold">The value that determines if the green channel is inverted if <paramref name="UseGreenThreshold"/> is TRUE.</param>
/// <param name="UseBlueThreshold">
/// If TRUE (and if <paramref name="InvertBlue"/> is TRUE), the blue channel will be inverted if
/// the source blue channel value is greater than <paramref name="BlueThreshold"/>.
/// </param>
/// <param name="BlueThreshold">The value that determines if the blue channel is inverted if <paramref name="UseBlueThreshold"/> is TRUE.</param>
/// <returns>Value indicating operational results.</returns>
int BufferInverter4(void* Source, int Width, int Height, int Stride, void *Destination, BOOL InvertAlpha, BOOL InvertRed,
    BOOL InvertGreen, BOOL InvertBlue,
    BOOL UseAlphaThreshold, BYTE AlphaThreshold, 
    BOOL UseRedThreshold, BYTE RedThreshold, 
    BOOL UseGreenThreshold, BYTE GreenThreshold,
    BOOL UseBlueThreshold, BYTE BlueThreshold)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE* Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            if (InvertRed)
            {
                if (UseRedThreshold)
                {
                    if (R > RedThreshold)
                        R = ~R;
                }
                else
                    R = ~R;
            }
            if (InvertGreen)
            {
                if (UseGreenThreshold)
                {
                    if (G > GreenThreshold)
                        G = ~G;
                }
                else
                    G = ~G;
            }
            if (InvertBlue)
            {
                if (UseBlueThreshold)
                {
                    if (B > BlueThreshold)
                        B = ~B;
                }
                else
                    B = ~B;
            }
            if (InvertAlpha)
            {
                if (UseAlphaThreshold)
                {
                    if (A > AlphaThreshold)
                        A = ~A;
                }
                A = ~A;
            }
            Dest[Index + 0] = B;
            Dest[Index + 1] = G;
            Dest[Index + 2] = R;
            Dest[Index + 3] = A;
        }
    }
    return Success;
}

/// <summary>
/// Does a simple color inversion of an image.
/// </summary>
/// <param name="Source">Source image to invert.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Where the new image will be written.</param>
/// <param name="InvertAlpha">If TRUE, the alpha channel is inverted as well.</param>
/// <returns>Value indicating operational results.</returns>
int BufferInverter3(void* Source, int Width, int Height, int Stride, void *Destination, BOOL InvertAlpha)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE* Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            R = ~R;
            G = ~G;
            B = ~B;
            if (InvertAlpha)
                A = ~A;
            Dest[Index + 0] = B;
            Dest[Index + 1] = G;
            Dest[Index + 2] = R;
            Dest[Index + 3] = A;
        }
    }
    return Success;
}

/// <summary>
/// Invert the pixels in the buffer.
/// </summary>
/// <param name="Target">The buffer whose pixels will be inverted.</param>
/// <param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
/// <param name="TargetHeight">The height of the target in scanlines.</param>
/// <param name="TargetStride">The stride of the target.</param>
/// <param name="InversionOperation">The type of inversion.</param>
/// <param name="LuminanceThreshold">If a pixel's luminance is greater than this value, the pixel will be inverted.</param>
/// <param name="InvertThreshold">If true, <paramref name="LuminanceThreshold"/> will be inverted prior to use.</param>
/// <param name="AllowInvertAlpha">Determines if alpha is inverted.</param>
/// <returns>Value indicating result of operation.</returns>
int BufferInverter2(void* Source, int Width, int Height, int Stride, void *Destination,
    int InversionOperation, double LuminanceThreshold, bool InvertThreshold, BOOL InvertAlpha,
    BOOL InvertRed, BOOL InvertGreen, BOOL InvertBlue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE* Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE PixelSize = 4;
    BYTE FinalLuminance = InvertThreshold ? 1.0 - LuminanceThreshold : LuminanceThreshold;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];

            switch (InversionOperation)
            {
                case SimpleInvertOperation:
                    R = ~R;
                    G = ~G;
                    B = ~B;
                    if (InvertAlpha)
                        A = ~A;
                    break;

                case ChannelInversionOperation:
                    if (InvertAlpha)
                        A = ~A;
                    if (InvertRed)
                        R = ~R;
                    if (InvertGreen)
                        G = ~G;
                    if (InvertBlue)
                        B = ~B;
                    break;

                case VariableInvertOperation:
                {
                    //Functionally same as solarization
                    double Luminance = PixelLuminance(R, G, B);
                    if (Luminance > FinalLuminance)
                    {
                        R = ~R;
                        G = ~G;
                        B = ~B;
                        if (InvertAlpha)
                            A = ~A;
                    }
                }
                break;

                default:
                    return InvalidOperation;
            }
            Dest[Index + 0] = B;
            Dest[Index + 1] = G;
            Dest[Index + 2] = R;
            Dest[Index + 3] = A;
        }
    }

    return Success;
}

/// <summary>
/// Invert the pixels in the buffer.
/// </summary>
/// <param name="Target">The buffer whose pixels will be inverted.</param>
/// <param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
/// <param name="TargetHeight">The height of the target in scanlines.</param>
/// <param name="TargetStride">The stride of the target.</param>
/// <param name="InversionOperation">The type of inversion.</param>
/// <param name="LuminanceThreshold">If a pixel's luminance is greater than this value, the pixel will be inverted.</param>
/// <param name="InvertThreshold">If true, <paramref name="LuminanceThreshold"/> will be inverted prior to use.</param>
/// <param name="AllowInvertAlpha">Determines if alpha is inverted.</param>
/// <returns>Value indicating result of operation.</returns>
int BufferInverter(void* Target, int TargetWidth, int TargetHeight, int TargetStride,
    int InversionOperation, double LuminanceThreshold, bool InvertThreshold, bool AllowInvertAlpha, BYTE InversionChannels)
{
    if (Target == NULL)
        return NullPointer;

    BYTE* Buffer = (BYTE *)Target;
    BYTE PixelSize = 4;
    BYTE FinalLuminance = InvertThreshold ? 1.0 - LuminanceThreshold : LuminanceThreshold;
    BOOL InvertAlpha = (InversionChannels & AlphaChannel) > 0 ? TRUE : FALSE;
    BOOL InvertRed = (InversionChannels & RedChannel) > 0 ? TRUE : FALSE;
    BOOL InvertGreen = (InversionChannels & GreenChannel) > 0 ? TRUE : FALSE;
    BOOL InvertBlue = (InversionChannels & BlueChannel) > 0 ? TRUE : FALSE;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE A = Buffer[Index + 3];
            BYTE R = Buffer[Index + 2];
            BYTE G = Buffer[Index + 1];
            BYTE B = Buffer[Index + 0];
            double Luminance = 0;
            switch (InversionOperation)
            {
                case SimpleInvertOperation:
                    R = 255 - R;
                    G = 255 - G;
                    B = 255 - G;
                    if (InvertAlpha)
                        A = 255 - A;
                    break;

                case ChannelInversionOperation:
                    if ((InvertAlpha) && (AllowInvertAlpha))
                        A = 255 - A;
                    if (InvertRed)
                        R = 255 - R;
                    if (InvertGreen)
                        G = 255 - G;
                    if (InvertBlue)
                        B = 255 - B;
                    break;

                case VariableInvertOperation:
                    Luminance = PixelLuminance(R, G, B);
                    if (Luminance > FinalLuminance)
                    {
                        R = 255 - R;
                        G = 255 - G;
                        B = 255 - B;
                        if (InvertAlpha)
                            A = 255 - A;
                    }
                    break;

                default:
                    return InvalidOperation;
            }
            Buffer[Index + 0] = B;
            Buffer[Index + 1] = G;
            Buffer[Index + 2] = R;
            Buffer[Index + 3] = A;
        }
    }

    return Success;
}

/// <summary>
/// Crop <paramref name="SourceBuffer"/> with the supplied region and place the result in <paramref name="DestinationBuffer"/>.
/// </summary>
/// <param name="SourceBuffer">Contains the source to be cropped. This buffer is not changed.</param>
/// <param name="DestinationBuffer">
/// Will contain the cropped part of <paramref name="SourceBuffer"/> on exit. The caller must create this buffer which must
/// be the proper size.
/// </param>
/// <param name="BufferWidth">Width of <paramref name="SourceBuffer"/> in pixels.</param>
/// <param name="BufferHeight">Height of <paramref name="SourceBuffer"/> in scan lines.</param>
/// <param name="BufferStride">Width of <paramref name="SourceBuffer"/> in bytes.</param>
/// <param name="X1">Left side of the crop region.</param>
/// <param name="Y1">Top of the crop region.</param>
/// <param name="X2">Right side of the crop region.</param>
/// <param name="Y2">Bottom of the crop region.</param>
/// <returns>Value indicating success.</returns>
int CropBuffer2(void *SourceBuffer, void *DestinationBuffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2)
{
    if (SourceBuffer == NULL)
        return NullPointer;
    if (DestinationBuffer == NULL)
        return NullPointer;
    if (X1 >= X2)
        return NoActionTaken;
    if (Y1 >= Y2)
        return NoActionTaken;
    if ((X1 < 0) || (Y1 < 0) || (X2 < 0) || (Y2 < 0))
        return NoActionTaken;
    if ((X1 > BufferWidth - 1) || (X2 > BufferWidth - 1))
        return NoActionTaken;
    if ((Y1 > BufferHeight - 1) || (Y2 > BufferHeight - 1))
        return NoActionTaken;

    BYTE *Source = (BYTE *)SourceBuffer;
    BYTE *Destination = (BYTE *)DestinationBuffer;
    int RowCount = Y2 - Y1 + 1;
    int ElementWidth = X2 - X1 + 1;
    int PixelSize = 4;
    int LineSize = PixelSize * ElementWidth;
    if (LineSize > BufferStride)
        return NoActionTaken;

#if FALSE
    int DRow = 0;
    int DCol = 0;
    for (int Row = Y1; Row < Y1 + Y2; Row++)
    {
        int RowOffset = Row * BufferStride;
        int DestOffset = (Row + DRow) * BufferStride;
        for (int Column = X1; Column < X1 + X2; Column++)
        {
            int SourceIndex = (Column*PixelSize) + RowOffset;
            int DestIndex = (DCol*PixelSize) + DestOffset;
            DCol++;
            Destination[DestIndex + 0] = Source[SourceIndex + 0];
            Destination[DestIndex + 1] = Source[SourceIndex + 1];
            Destination[DestIndex + 2] = Source[SourceIndex + 2];
            Destination[DestIndex + 3] = Source[SourceIndex + 3];
}
        DCol++;
    }
#else
    int DestRow = 0;
    for (int Row = Y1; Row < Y2 + 1; Row++)
    {
        int SourceIndex = (X1 * PixelSize) + (Row * BufferStride);
        memmove_s(Destination + (LineSize * DestRow), LineSize,
            Source + SourceIndex, LineSize);
        DestRow++;
    }
#endif

    return Success;
}

int CopyBufferRegion(void *SourceBuffer, void *DestinationBuffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2)
{
    return CropBuffer2(SourceBuffer, DestinationBuffer, BufferWidth, BufferHeight, BufferStride, X1, Y1, X2, Y2);
}

/// <summary>
/// Copy part of an image in <paramref name="Source"/> to <paramref name="Destination"/>.
/// </summary>
/// <param name="Source">Source of the region to copy.</param>
/// <param name="SourceWidth">Width of the source.</param>
/// <param name="SourceHeight">Height of the source.</param>
/// <param name="SourceStride">Stride of the source.</param>
/// <param name="Destination">Where the copy will be placed. Must be large enough to hold the copy.</param>
/// <param name="DestinationWidth">Width of the destination.</param>
/// <param name="DestinationHeight">Height of the destination.</param>
/// <param name="DestinationStride">Stride of the destination.</param>
/// <param name="X1">
/// Horizontal coordinate in <paramref name="Source"/> where copying will start. Width of the sub-region to copy
/// is based on <paramref name="DestinationWidth"/>.
/// </param>
/// <param name="Y1">
/// Vertical coordinate in <paramref name="Source"/> where copying will start. Height of the sub-region to copy
/// is based on <paramref name="DestinationHeight"/>.
/// </param>
/// <returns>Value indication operational results.</returns>
int CopySubRegion(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
    __int32 X1, __int32 Y1)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (X1 < 0)
        return InvalidOperation;
    if (Y1 < 0)
        return InvalidOperation;
    if (X1 >= SourceWidth)
        return InvalidOperation;
    if (Y1 >= SourceHeight)
        return InvalidOperation;
    if (X1 + DestinationWidth >= SourceWidth)
        return InvalidOperation;
    if (Y1 + DestinationHeight >= SourceHeight)
        return InvalidOperation;

    return Success;
}

void ProcessInitialize()
{
    InitializeErrorStack("`");
}

//Required DLL entry points. Not explicitly used.
BOOL WINAPI DllMain(HINSTANCE DLLHandle, DWORD Reason, LPVOID NotUsed)
{
    switch (Reason)
    {
        case DLL_PROCESS_ATTACH:
            //Initialize for each new attached process.
            ProcessInitialize();
            break;

        case DLL_THREAD_ATTACH:
            //Initialize for each new attached thread.
            ProcessInitialize();
            break;

        case DLL_THREAD_DETACH:
            //Thread-specific clean-up.
            break;

        case DLL_PROCESS_DETACH:
            //Process-specific clean-up.
            break;
    }

    return TRUE;
}