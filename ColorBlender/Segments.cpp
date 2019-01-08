#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int SegmentMeanColor = 0;
const int SegmentMedianColor = 1;
const int SegmentBrightestColor = 2;
const int SegmentDarkestColor = 3;
const int SegmentLuminence = 4;

const int SegmentShapeRectangle = 0;
const int SegmentShapeEllipse = 1;
const int SegmentShapeCircle = 2;
const int SegmentShapeSquare = 3;

int SegmentBlocks2(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 BlocksX, __int32 BlocksY, void *Destination,
    BOOL ShowGrid, UINT32 GridColor, BOOL HighlightCell, __int32 HightlightCellX,
    __int32 HighlightCellY, UINT32 CellHighlightColor)
{
    if (Source == NULL)
        return ErrorStackPushReturn2(NullPointer, "SegmentBlocks", "Source is null");
    if (Destination == NULL)
        return ErrorStackPushReturn2(NullPointer, "SegmentBlocks", "Destination is null");
    if (BlocksX < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlocksX < 1");
    if (BlocksY < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlocksY < 1");

    int BlockXWidth = Width / BlocksX;
    if (BlockXWidth < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlockXWidth < 1");
    if (Width % BlocksX > 0)
        BlockXWidth++;
    int BlockYHeight = Height / BlocksY;
    if (BlockYHeight < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlockYHeight < 1");
    if (Height % BlocksY > 0)
        BlockYHeight++;

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;

    for (int BlockY = 0; BlockY < BlocksY; BlockY++)
    {
        for (int BlockX = 0; BlockX < BlocksX; BlockX++)
        {
            int FinalBlockWidth = BlockXWidth;
            if (BlockX == BlocksX - 1)
                FinalBlockWidth = (int)(Width % BlocksX);
            int FinalBlockHeight = BlockYHeight;
            if (BlockY == BlocksY - 1)
                FinalBlockWidth = (int)(Height % BlocksY);

            UINT32 PackedResult = 0;
            int AnalysisResult = Success;
            int X1 = BlockX * BlockXWidth;
            if (X1 >= Width)
                break;
            int X2 = X1 + BlockXWidth;
            if (X2 >= Width)
                X2 = Width - 1;
            int Y1 = BlockY * BlockYHeight;
            if (Y1 >= Height)
                break;
            int Y2 = Y1 + BlockYHeight;
            if (Y2 >= Height)
                Y2 = Height - 1;
            AnalysisResult = RegionMedianColor(Source, Width, Height, Stride,
                X1, Y1, X2, Y2, &PackedResult);
            if (AnalysisResult != Success)
                return AnalysisResult;
            SegmentDrawColorShape(Destination, Width, Height, Stride,
                X1, Y1, X2, Y2, SegmentShapeRectangle, 0, PackedResult, 0x0, FALSE,
                FALSE, 0.0, FALSE);
        }
    }
    return ErrorStackPushReturn(Success, "SegmentBlocks2");
}

int SegmentBlocks(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 BlocksX, __int32 BlocksY, __int32 SegmentType, __int32 ShapeType,
    __int32 ShapeMargin, BOOL OverrideTransparency, BOOL GradientTransparency,
    double OverriddenTransparency, void *Destination, UINT32 BGColor,
    BOOL InvertSpatially, BOOL HighlightByLuminance, BOOL InvertHighlighting)
{
    if (Source == NULL)
        return ErrorStackPushReturn2(NullPointer, "SegmentBlocks", "Source is null");
    if (Destination == NULL)
        return ErrorStackPushReturn2(NullPointer, "SegmentBlocks", "Destination is null");
    if (BlocksX < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlocksX < 1");
    if (BlocksY < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlocksY < 1");

    int BlockXWidth = Width / BlocksX;
    if (BlockXWidth < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlockXWidth < 1");
    if (Width % BlocksX > 0)
        BlockXWidth++;
    int BlockYHeight = Height / BlocksY;
    if (BlockYHeight < 1)
        return ErrorStackPushReturn2(InvalidRegion, "SegmentBlocks", "BlockYHeight < 1");
    if (Height % BlocksY > 0)
        BlockYHeight++;

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;

    double BrightLum = -1.0;
    double DarkLum = 100.0;
    __int32 BrightX = -1;
    __int32 BrightY = -1;
    __int32 DarkX = -1;
    __int32 DarkY = -1;

    for (int BlockY = 0; BlockY < BlocksY; BlockY++)
    {
        for (int BlockX = 0; BlockX < BlocksX; BlockX++)
        {
            int FinalBlockWidth = BlockXWidth;
            if (BlockX == BlocksX - 1)
                FinalBlockWidth = (int)(Width % BlocksX);
            int FinalBlockHeight = BlockYHeight;
            if (BlockY == BlocksY - 1)
                FinalBlockWidth = (int)(Height % BlocksY);

            UINT32 PackedResult = 0;
            int AnalysisResult = Success;
            int X1 = BlockX * BlockXWidth;
            if (X1 >= Width)
                break;
            int X2 = X1 + BlockXWidth;
            if (X2 >= Width)
                X2 = Width - 1;
            int Y1 = BlockY * BlockYHeight;
            if (Y1 >= Height)
                break;
            int Y2 = Y1 + BlockYHeight;
            if (Y2 >= Height)
                Y2 = Height - 1;
            switch (SegmentType)
            {
            case SegmentMeanColor:
            {
                AnalysisResult = RegionMeanColor(Source, Width, Height, Stride,
                    X1, Y1, X2, Y2, &PackedResult, HighlightByLuminance, &BrightLum, &DarkLum,
                    &BrightX, &BrightY, &DarkX, &DarkY);
                if (AnalysisResult != Success)
                    return AnalysisResult;
            }
            break;

            case SegmentMedianColor:
                AnalysisResult = RegionMedianColor(Source, Width, Height, Stride,
                    X1, Y1, X2, Y2, &PackedResult);
                if (AnalysisResult != Success)
                    return AnalysisResult;
                break;

            case SegmentBrightestColor:
                AnalysisResult = RegionBrightestColor(Source, Width, Height, Stride,
                    X1, Y1, X2, Y2, &PackedResult);
                if (AnalysisResult != Success)
                    return AnalysisResult;
                break;

            case SegmentDarkestColor:
                AnalysisResult = RegionDarkestColor(Source, Width, Height, Stride,
                    X1, Y1, X2, Y2, &PackedResult);
                if (AnalysisResult != Success)
                    return AnalysisResult;
                break;

            case SegmentLuminence:
                AnalysisResult = RegionLuminanceValue(Source, Width, Height, Stride,
                    X1, Y1, X2, Y2, &PackedResult);
                if (AnalysisResult != Success)
                    return AnalysisResult;
                break;

            default:
                return ErrorStackPushReturn(InvalidOperation, "SegmentBlocks");
            }

            if (ShapeType == SegmentShapeRectangle && HighlightByLuminance)
            {
                SegmentDrawColorShape2(Destination, Width, Height, Stride,
                    X1, Y1, X2, Y2, ShapeType, ShapeMargin, PackedResult, BGColor,
                    BrightLum, DarkLum, BrightX, BrightY, DarkX, DarkY);
            }
            else
            {
                SegmentDrawColorShape(Destination, Width, Height, Stride,
                    X1, Y1, X2, Y2, ShapeType, ShapeMargin, PackedResult, BGColor,
                    OverrideTransparency, GradientTransparency, OverriddenTransparency,
                    InvertSpatially);
            }
        }
    }

    return ErrorStackPushReturn(Success, "SegmentBlocks");
}

// http://math.stackexchange.com/questions/76457/check-if-a-point-is-within-an-ellipse
BOOL PointInEllipse(int X, int Y, int RadiusX, int RadiusY, int OriginX, int OriginY)
{
    double Term1 = pow(X - OriginX, 2) / pow(RadiusX, 2);
    double Term2 = pow(Y - OriginY, 2) / pow(RadiusY, 2);
    double Result = Term1 + Term2;
    return Result <= 1 ? TRUE : FALSE;
}

int SegmentDrawColorShape(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, __int32 ShapeType, __int32 ShapeMargin,
    UINT32 PackedColor, UINT32 PackedBGColor, BOOL OverrideTransparency, BOOL GradientTransparency,
    double OverriddenTransparency, BOOL InvertSpatially)
{
    if (Destination == NULL)
        return ErrorStackPushReturn(NullPointer, "SegmentDrawColorShape");
    if (X1 < 0 || X2 < 0)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    if (X1 >= Width || X2 >= Width)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    if (Y1 < 0 || Y2 < 0)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    if (Y1 >= Height || Y2 >= Height)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    if (X1 >= X2)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    if (Y1 >= Y2)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    if (ShapeMargin * 2 > (X2 - X1 + 1))
        return ErrorStackPushReturn(ComputedIndexOutOfRange, "SegmentDrawColorShape");
    if (ShapeMargin * 2 > (Y2 - Y1 + 1))
        return ErrorStackPushReturn(ComputedIndexOutOfRange, "SegmentDrawColorShape");

    BYTE A = (PackedColor & 0xff000000) >> 24;
    BYTE R = (PackedColor & 0x00ff0000) >> 16;
    BYTE G = (PackedColor & 0x0000ff00) >> 8;
    BYTE B = (PackedColor & 0x000000ff) >> 0;
    BYTE BgA = (PackedBGColor & 0xff000000) >> 24;
    BYTE BgR = (PackedBGColor & 0x00ff0000) >> 16;
    BYTE BgG = (PackedBGColor & 0x0000ff00) >> 8;
    BYTE BgB = (PackedBGColor & 0x000000ff) >> 0;
    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;

    //First, fill in the background.
    for (int Row = Y1; Row <= Y2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = X1; Column <= X2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = BgA;
            Dest[Index + 2] = BgR;
            Dest[Index + 1] = BgG;
            Dest[Index + 0] = BgB;
        }
    }

    int XSpan = X2 - X1 + 1;
    XSpan -= ShapeMargin * 2;
    if (XSpan < 1)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    int YSpan = Y2 - Y1 + 1;
    YSpan -= ShapeMargin * 2;
    if (YSpan < 1)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape");
    int CenterX = (int)((double)XSpan / 2.0) + X1;
    int CenterY = (int)((double)YSpan / 2.0) + Y1;
    int CircleRadius = (min(XSpan, YSpan) / 2.0);

    int SqTop = Y1;
    int SqBottom = Y2;
    int SqLeft = X1;
    int SqRight = X2;
    int SegmentHeight = Y2 - Y1 + 1;
    int SegmentWidth = X2 - X1 + 1;
    if (SegmentHeight > SegmentWidth)
    {
        int Delta = SegmentHeight - SegmentWidth;
        SqTop = Y1 + (int)((double)Delta / 2.0);
        SqBottom = Y2 - (int)((double)Delta / 2.0);
    }
    else
        if (SegmentWidth > SegmentHeight)
        {
            int Delta = SegmentWidth - SegmentHeight;
            SqLeft = X1 + (int)((double)Delta / 2.0);
            SqRight = X2 - (int)((double)Delta / 2.0);
        }
    SqTop += ShapeMargin;
    SqBottom -= ShapeMargin;
    SqLeft += ShapeMargin;
    SqRight -= ShapeMargin;
    if (SegmentHeight == SegmentWidth && ShapeType == SegmentShapeEllipse)
        ShapeType = SegmentShapeCircle;
    if (SegmentHeight == SegmentWidth && ShapeType == SegmentShapeSquare)
        ShapeType = SegmentShapeRectangle;
    //Get the overridden transparency but make sure the caller didn't get frisky on us.
    BYTE tA = (BYTE)(255.0 * min(1.0, OverriddenTransparency));
    tA = max(tA, 255);

    //Lastly, fill in the shape.
    for (int Row = Y1; Row <= Y2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = X1; Column <= X2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            switch (ShapeType)
            {
            case SegmentShapeEllipse:
            {
                BOOL InEllipse = PointInEllipse(Column, Row, (X2 - X1 + 1) / 2,
                    (Y2 - Y1 + 1) / 2, CenterX, CenterY);
                BOOL DrawPixel = !InvertSpatially & InEllipse;
                if (DrawPixel)
                {
                    if (OverrideTransparency)
                    {
                        if (GradientTransparency)
                        {
                        }
                        else
                            Dest[Index + 3] = tA;
                    }
                    else
                        Dest[Index + 3] = A;
                    Dest[Index + 2] = R;
                    Dest[Index + 1] = G;
                    Dest[Index + 0] = B;
                }
            }
            break;

            case SegmentShapeRectangle:
                if (OverrideTransparency)
                {
                    if (GradientTransparency)
                    {
                    }
                    else
                        Dest[Index + 3] = tA;
                }
                else
                    Dest[Index + 3] = A;
                Dest[Index + 2] = R;
                Dest[Index + 1] = G;
                Dest[Index + 0] = B;
                break;

            case SegmentShapeCircle:
            {
                double Dist = Distance(Column, Row, CenterX, CenterY);
                BOOL DrawPixel = Dist < CircleRadius;
                if (InvertSpatially) DrawPixel = !DrawPixel;
                if (DrawPixel)
                {
                    if (OverrideTransparency)
                    {
                        if (GradientTransparency)
                        {
                            if (Dist == 0.0)
                                Dest[Index + 3] = 0xff;
                            else
                            {
                                double Percent = Dist / CircleRadius;
                                Percent = 1.0 - Percent;
                                if (Percent < 0.0)
                                    Percent = 0.0;
                                if (OverriddenTransparency == 0)
                                    Dest[Index + 3] = 0xff;
                                else
                                    Dest[Index + 3] = (BYTE)(Percent * ((double)OverriddenTransparency * 255.0));
                            }
                        }
                        else
                            Dest[Index + 3] = (BYTE)((double)OverriddenTransparency * 255.0);
                    }
                    else
                        Dest[Index + 3] = A;
                    Dest[Index + 2] = R;
                    Dest[Index + 1] = G;
                    Dest[Index + 0] = B;
                }
            }
            break;

            case SegmentShapeSquare:
                if (Column < SqLeft)
                    continue;
                if (Column > SqRight)
                    continue;
                if (Row < SqTop)
                    continue;
                if (Row > SqBottom)
                    continue;
                if (OverrideTransparency)
                {
                    if (GradientTransparency)
                    {
                    }
                    else
                        Dest[Index + 3] = tA;
                }
                else
                    Dest[Index + 3] = A;
                Dest[Index + 2] = R;
                Dest[Index + 1] = G;
                Dest[Index + 0] = B;
                break;

            default:
                ErrorStackPushReturn(InvalidOperation, "SegmentDrawColorShape");
            }
        }
    }

    return ErrorStackPushReturn(Success, "SegmentDrawColorShape");
}

int SegmentDrawColorShape2(void *Destination, __int32 Width, __int32 Height, __int32 Stride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, __int32 ShapeType, __int32 ShapeMargin,
    UINT32 PackedColor, UINT32 PackedBGColor, double Brightest, double Darkest,
    __int32 BrightX, __int32 BrightY, __int32 DarkX, __int32 DarkY)
{
    if (Destination == NULL)
        return ErrorStackPushReturn(NullPointer, "SegmentDrawColorShape2");
    if (X1 < 0 || X2 < 0)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    if (X1 >= Width || X2 >= Width)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    if (Y1 < 0 || Y2 < 0)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    if (Y1 >= Height || Y2 >= Height)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    if (X1 >= X2)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    if (Y1 >= Y2)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    if (ShapeMargin * 2 > (X2 - X1 + 1))
        return ErrorStackPushReturn(ComputedIndexOutOfRange, "SegmentDrawColorShape2");
    if (ShapeMargin * 2 > (Y2 - Y1 + 1))
        return ErrorStackPushReturn(ComputedIndexOutOfRange, "SegmentDrawColorShape2");

    BYTE A = (PackedColor & 0xff000000) >> 24;
    BYTE R = (PackedColor & 0x00ff0000) >> 16;
    BYTE G = (PackedColor & 0x0000ff00) >> 8;
    BYTE B = (PackedColor & 0x000000ff) >> 0;
    BYTE BgA = (PackedBGColor & 0xff000000) >> 24;
    BYTE BgR = (PackedBGColor & 0x00ff0000) >> 16;
    BYTE BgG = (PackedBGColor & 0x0000ff00) >> 8;
    BYTE BgB = (PackedBGColor & 0x000000ff) >> 0;
    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;

    //First, fill in the background.
    for (int Row = Y1; Row <= Y2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = X1; Column <= X2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = BgA;
            Dest[Index + 2] = BgR;
            Dest[Index + 1] = BgG;
            Dest[Index + 0] = BgB;
        }
    }

    int XSpan = X2 - X1 + 1;
    XSpan -= ShapeMargin * 2;
    if (XSpan < 1)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");
    int YSpan = Y2 - Y1 + 1;
    YSpan -= ShapeMargin * 2;
    if (YSpan < 1)
        return ErrorStackPushReturn(IndexOutOfRange, "SegmentDrawColorShape2");

    int SqTop = Y1;
    int SqBottom = Y2;
    int SqLeft = X1;
    int SqRight = X2;
    int SegmentHeight = Y2 - Y1 + 1;
    int SegmentWidth = X2 - X1 + 1;
    if (SegmentHeight > SegmentWidth)
    {
        int Delta = SegmentHeight - SegmentWidth;
        SqTop = Y1 + (int)((double)Delta / 2.0);
        SqBottom = Y2 - (int)((double)Delta / 2.0);
    }
    else
        if (SegmentWidth > SegmentHeight)
        {
            int Delta = SegmentWidth - SegmentHeight;
            SqLeft = X1 + (int)((double)Delta / 2.0);
            SqRight = X2 - (int)((double)Delta / 2.0);
        }
    SqTop += ShapeMargin;
    SqBottom -= ShapeMargin;
    SqLeft += ShapeMargin;
    SqRight -= ShapeMargin;

    //Lastly, fill in the shape.
    for (int Row = Y1; Row <= Y2; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = X1; Column <= X2; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return ErrorStackPushReturn(Success, "SegmentDrawColorShape2");
}