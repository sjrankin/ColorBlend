#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int OverlayGridX(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    int HorizontalFrequency, int VerticalFrequency, UINT32 GridColor)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE grB = (BYTE)((GridColor & 0xff000000) >> 24);
    BYTE grG = (BYTE)((GridColor & 0x00ff0000) >> 16);
    BYTE grR = (BYTE)((GridColor & 0x0000ff00) >> 8);
    BYTE grA = (BYTE)((GridColor & 0x000000ff) >> 0);

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Width;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            if (
                (Row % VerticalFrequency == 0) ||
                (Column % HorizontalFrequency == 0)
                )
            {
                Dest[Index + 0] = grB;
                Dest[Index + 1] = grG;
                Dest[Index + 2] = grR;
                Dest[Index + 3] = grA;
                continue;
            }
            Dest[Index + 0] = Src[Index + 0];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 3] = Src[Index + 3];
        }
    }

    return Success;
}

int DrawRectangle_Validate(void *Source, __int32 Width, __int32 Height, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
    if (Source == NULL)
        ErrorStackPushReturn2(NullPointer, "DrawRectangle", "Source is null.");
    if (Destination == NULL)
        ErrorStackPushReturn2(NullPointer, "DrawRectangle", "Destination is null.");

    if (Left < 0)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle", "Left < 0");
    if (Top < 0)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle", "Top < 0");
    if (Right > Width - 1)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle", "Right > Width - 1");
    if (Bottom > Height - 1)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle", "Bottom > Height - 1");
    if (Left >= Right)
        ErrorStackPushReturn2(ComputedIndexOutOfRange, "DrawRectangle", "Left >= Right");
    if (Top >= Bottom)
        ErrorStackPushReturn2(ComputedIndexOutOfRange, "DrawRectangle", "Top >= Bottom");
    return Success;
}

int DrawRectangle2_Validate (void *Destination, __int32 Width, __int32 Height,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
    if (Destination == NULL)
        ErrorStackPushReturn2(NullPointer, "DrawRectangle2", "Destination is null.");

    if (Left < 0)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle2", "Left < 0");
    if (Top < 0)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle2", "Top < 0");
    if (Right > Width - 1)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle2", "Right > Width - 1");
    if (Bottom > Height - 1)
        ErrorStackPushReturn2(IndexOutOfRange, "DrawRectangle2", "Bottom > Height - 1");
    if (Left >= Right)
        ErrorStackPushReturn2(ComputedIndexOutOfRange, "DrawRectangle2", "Left >= Right");
    if (Top >= Bottom)
        ErrorStackPushReturn2(ComputedIndexOutOfRange, "DrawRectangle2", "Top >= Bottom");
    return Success;
}

int DrawRectangle(void *Source, __int32 Width, __int32 Height, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, UINT32 RectangleColor)
{
    if (DrawRectangle_Validate(Source, Width, Height, Destination, Left, Top, Right, Bottom) != Success)
        return FailedParameterValidation;

    UINT32 *Src = (UINT32 *)Source;
    UINT32 *Dest = (UINT32 *)Destination;

    memcpy(Dest, Src, Width * Height * 4);

    //Draw the vertical lines first.
    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Width;
        int LeftIndex = Left + RowOffset;
        Dest[LeftIndex] = RectangleColor;
        int RightIndex = Right + RowOffset;
        Dest[RightIndex] = RectangleColor;
    }

    //Draw the horizontal lines next.
    for (int Column = Left; Column <= Right; Column++)
    {
        int TopIndex = (Top * Width) + Column;
        int BottomIndex = (Bottom * Width) + Column;
        Dest[TopIndex] = RectangleColor;
        Dest[BottomIndex] = RectangleColor;
    }

    return ErrorStackPushReturn(Success, "DrawRectangle");
}

int DrawRectangle2(void *Destination, __int32 Width, __int32 Height,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, UINT32 RectangleColor)
{
    if (DrawRectangle2_Validate(Destination, Width, Height, Left, Top, Right, Bottom) != Success)
        return FailedParameterValidation;

    UINT32 *Dest = (UINT32 *)Destination;

    //Draw the vertical lines first.
    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Width;
        int LeftIndex = Left + RowOffset;
        Dest[LeftIndex] = RectangleColor;
        int RightIndex = Right + RowOffset;
        Dest[RightIndex] = RectangleColor;
    }

    //Draw the horizontal lines next.
    for (int Column = Left; Column <= Right; Column++)
    {
        int TopIndex = (Top * Width) + Column;
        int BottomIndex = (Bottom * Width) + Column;
        Dest[TopIndex] = RectangleColor;
        Dest[BottomIndex] = RectangleColor;
    }

    return ErrorStackPushReturn(Success, "DrawRectangle2");
}

int OverlayGrid(void *Source, __int32 Width, __int32 Height, void *Destination,
    int HorizontalFrequency, int VerticalFrequency, UINT32 GridColor)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    UINT32 *Src = (UINT32 *)Source;
    UINT32 *Dest = (UINT32 *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Width;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = Column + RowOffset;
            if (
                (Row % VerticalFrequency == 0) ||
                (Column % HorizontalFrequency == 0)
                )
            {
                Dest[Index] = GridColor;
            }
            else
            {
                Dest[Index] = Src[Index];
            }
        }
    }

    return Success;
}

/// <summary>
/// Draws a set of color blocks using alpha blending.
/// </summary>
/// <param name="Target">Where the drawing will take place.</param>
/// <param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
/// <param name="TargetHeight">The height of the target in scanlines.</param>
/// <param name="TargetStride">The stride of the target.</param>
/// <param name="ColorBlockList">Array of information on how to draw the color blocks.</param>
/// <param name="ColorBlockCount">Number of color blocks in the ColorBlockList.</param>
/// <param name="DefaultColor">The background color when there are no blocks. Format is BGRA.</param>
/// <returns>True on success, false on error.</returns>
BOOL DrawBlocks(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *ColorBlockList, __int32 ColorBlockCount, UINT32 DefaultColor)
{
    if (Target == NULL)
        return FALSE;
    if (ColorBlockList == NULL)
        return FALSE;
    if (ColorBlockCount < 1)
        return FALSE;

    BYTE DefaultB = (DefaultColor & 0xff000000) >> 24;
    BYTE DefaultG = (DefaultColor & 0x00ff0000) >> 16;
    BYTE DefaultR = (DefaultColor & 0x0000ff00) >> 8;
    BYTE DefaultA = (DefaultColor & 0x000000ff) >> 0;
    BYTE *Buffer = (BYTE *)Target;
    int PixelSize = 4;
    ColorBlock *BlockList = (ColorBlock *)ColorBlockList;
    for (int i = 0; i < ColorBlockCount; i++)
    {
        BlockList[i].Right = BlockList[i].Left + BlockList[i].Width;
        BlockList[i].Bottom = BlockList[i].Top + BlockList[i].Height;
        BlockList[i].A = BlockList[i].BlockColor & 0xff;
        BlockList[i].R = (BlockList[i].BlockColor & 0x0000ff00) >> 8;
        BlockList[i].G = (BlockList[i].BlockColor & 0x00ff0000) >> 16;
        BlockList[i].B = (BlockList[i].BlockColor & 0xff000000) >> 24;
    }

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            //BGRA
            double Final[] = { 0.0, 0.0, 0.0, 0.0 };
            int OverlapCount = 0;
            BYTE MaxAlpha = 0x0;
            for (int BlockIndex = 0; BlockIndex < ColorBlockCount; BlockIndex++)
            {
                if (Row < BlockList[BlockIndex].Top)
                    continue;
                if (Row > BlockList[BlockIndex].Bottom)
                    continue;
                if (Column < BlockList[BlockIndex].Left)
                    continue;
                if (Column > BlockList[BlockIndex].Right)
                    continue;

#if TRUE
#if TRUE
                double ARatio = (double)BlockList[BlockIndex].A / 255.0;
                Final[0] += (double)((double)BlockList[BlockIndex].B * ARatio);
                Final[1] += (double)((double)BlockList[BlockIndex].G * ARatio);
                Final[2] += (double)((double)BlockList[BlockIndex].R * ARatio);
                Final[3] += (double)((double)BlockList[BlockIndex].A * ARatio);
                if (BlockList[BlockIndex].A > MaxAlpha)
                    MaxAlpha = BlockList[BlockIndex].A;
#else
                Final[0] += (double)BlockList[BlockIndex].B;
                Final[1] += (double)BlockList[BlockIndex].G;
                Final[2] += (double)BlockList[BlockIndex].R;
                Final[3] += (double)BlockList[BlockIndex].A;
#endif
#else
                Final[0] += ((double)BlockList[BlockIndex].B / 255.0);
                Final[1] += ((double)BlockList[BlockIndex].G / 255.0);
                Final[2] += ((double)BlockList[BlockIndex].R / 255.0);
                Final[3] += ((double)BlockList[BlockIndex].A / 255.0);
#endif
                OverlapCount++;
            }
            if (OverlapCount > 0)
            {
#if TRUE
                Buffer[Index + 0] = (BYTE)(Final[0] / OverlapCount);
                Buffer[Index + 1] = (BYTE)(Final[1] / OverlapCount);
                Buffer[Index + 2] = (BYTE)(Final[2] / OverlapCount);
                Buffer[Index + 3] = MaxAlpha;// (BYTE)(Final[3] / OverlapCount);
#else
                Final[0] /= OverlapCount;
                Final[1] /= OverlapCount;
                Final[2] /= OverlapCount;
                Final[3] /= OverlapCount;
                BYTE FinalB = (Final[0] * 255.0);
                BYTE FinalG = (Final[1] * 255.0);
                BYTE FinalR = (Final[2] * 255.0);
                BYTE FinalA = (Final[3] * 255.0);
                Buffer[Index + 0] = FinalB;
                Buffer[Index + 1] = FinalG;
                Buffer[Index + 2] = FinalR;
                Buffer[Index + 3] = FinalA;
#endif
            }
            else
            {
                //No blocks cover this pixel so draw the default color.
                Buffer[Index + 0] = DefaultB;
                Buffer[Index + 1] = DefaultG;
                Buffer[Index + 2] = DefaultR;
                Buffer[Index + 3] = DefaultA;
            }
        }
    }

    return TRUE;
}

/// <summary>
/// Draw a horizontal or vertical line.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="IsHorizontal">Determines if a horizontal or vertical line will be drawn.</param>
/// <param name="Coordinate">The column where the line will be drawn.</param>
/// <param name="A">The alpha value of the line to draw.</param>
/// <param name="R">The red value of the line to draw.</param>
/// <param name="G">The green value of the line to draw.</param>
/// <param name="B">The blue value of the line to draw.</param>
BOOL DrawLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    BOOL IsHorizontal, __int32 Coordinate, BYTE A, BYTE R, BYTE G, BYTE B)
{
    if (IsHorizontal)
        return DrawHorizontalLine(Target, TargetWidth, TargetHeight, TargetStride, Coordinate, A, R, G, B);
    else
        return DrawVerticalLine(Target, TargetWidth, TargetHeight, TargetStride, Coordinate, A, R, G, B);
}

BOOL DrawAnyLine2(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, UINT32 PackedLineColor,
    BOOL AntiAlias, __int32 LineThickness)
{
    if (Target == NULL)
        return FALSE;

    BYTE A = (PackedLineColor & 0xff000000) >> 24;
    BYTE R = (PackedLineColor & 0x00ff0000) >> 16;
    BYTE G = (PackedLineColor & 0x0000ff00) >> 8;
    BYTE B = (PackedLineColor & 0x000000ff) >> 0;
    BYTE *Buffer = (BYTE*)Target;
    BYTE PixelSize = 4;
    __int32 DeltaX = X2 - X1;
    __int32 DeltaY = Y2 - Y1;
    if (X1 > X2)
    {
        __int32 temp = X2;
        X2 = X1;
        X1 = temp;
    }
    UINT32 MaxIndex = (TargetStride * TargetHeight) - 1;

    for (__int32 XPlot = X1; XPlot < X2 + 1; XPlot++)
    {
        __int32 YPlot = Y1 + DeltaY * (XPlot - X1) / DeltaX;
        __int32 Index = (YPlot - 1) * TargetStride;
        Index += XPlot * PixelSize;
        if ((UINT32)(Index + PixelSize) > MaxIndex)
            continue;
        Buffer[Index + 0] = B;
        Buffer[Index + 1] = G;
        Buffer[Index + 2] = R;
        Buffer[Index + 3] = A;
    }

    return TRUE;
}

//http://en.wikipedia.org/wiki/Xiaolin_Wu%27s_line_algorithm
/// <summary>
/// Draw a line from the two specfied points using the specified color. Parts of lines that extend beyond the bounds set by
/// <paramref name="TargetWidth"/> and <paramref name="TargetHeight"/> are not drawn.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="X1">First horizontal coordinate.</param>
/// <param name="Y1">First vertical coordinate.</param>
/// <param name="X2">Second horizontal coordinate.</param>
/// <param name="Y2">Second vertical coordinate.</param>
/// <param name="A">The alpha value of the line to draw.</param>
/// <param name="R">The red value of the line to draw.</param>
/// <param name="G">The green value of the line to draw.</param>
/// <param name="B">The blue value of the line to draw.</param>
/// <returns>True on success, false on error.</returns>
BOOL DrawAnyLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 X1, __int32 Y1, __int32 X2, __int32 Y2, BYTE A, BYTE R, BYTE G, BYTE B,
    BOOL AntiAlias, __int32 LineThickness)
{
    UINT32 PackedColor = (A << 24) | (R << 16) | (G << 8) | (B << 0);
    return DrawAnyLine2(Target, TargetWidth, TargetHeight, TargetStride, X1, Y1, X2, Y2, PackedColor, AntiAlias, LineThickness);
}

/// <summary>
/// Draw a horizontal line. Does alpha blending.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="Y">The row where the line will be drawn.</param>
/// <param name="A">The alpha value of the line to draw.</param>
/// <param name="R">The red value of the line to draw.</param>
/// <param name="G">The green value of the line to draw.</param>
/// <param name="B">The blue value of the line to draw.</param>
BOOL DrawHorizontalLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 Y, BYTE A, BYTE R, BYTE G, BYTE B)
{
    if (Target == NULL)
        return FALSE;
    if (Y < 0)
        return FALSE;
    if (Y > TargetHeight - 1)
        return FALSE;
    BYTE* TargetBuffer = (BYTE *)Target;
    int PixelSize = 4;
    __int32 RowOffset = Y * TargetStride;
    double Alpha = (double)A / 255.0;

    for (__int32 Column = 0; Column < TargetWidth; Column++)
    {
        __int32 Index = RowOffset + (Column * PixelSize);
        TargetBuffer[Index + 0] = B;
        TargetBuffer[Index + 1] = G;
        TargetBuffer[Index + 2] = R;
        TargetBuffer[Index + 3] = A;
        /*
        double DestinationAlpha = (double) TargetBuffer[Index + 3] / 255.0;
        BYTE FinalB = (B * (Alpha)) + (TargetBuffer[Index + 0] * (DestinationAlpha) *(255 - A));
        BYTE FinalG = (G * (Alpha)) + (TargetBuffer[Index + 1] * (DestinationAlpha) *(255 - A));
        BYTE FinalR = (R * (Alpha)) + (TargetBuffer[Index + 2] * (DestinationAlpha) *(255 - A));
        BYTE FinalA = A + (TargetBuffer[Index + 3] * (255 - A));
        TargetBuffer[Index + 0] = FinalB;
        TargetBuffer[Index + 1] = FinalG;
        TargetBuffer[Index + 2] = FinalR;
        TargetBuffer[Index + 3] = FinalA;
        */
    }

    return TRUE;
}

/// <summary>
/// Draw a vertical line. Does alpha blending.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="X">The column where the line will be drawn.</param>
/// <param name="A">The alpha value of the line to draw.</param>
/// <param name="R">The red value of the line to draw.</param>
/// <param name="G">The green value of the line to draw.</param>
/// <param name="B">The blue value of the line to draw.</param>
BOOL DrawVerticalLine(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    __int32 X, BYTE A, BYTE R, BYTE G, BYTE B)
{
    if (Target == NULL)
        return FALSE;
    if (X < 0)
        return FALSE;
    if (X > TargetWidth - 1)
        return FALSE;
    BYTE* TargetBuffer = (BYTE *)Target;
    int PixelSize = 4;
    __int32 ColumnOffset = X * PixelSize;
    double Alpha = (double)A / 255.0;

    for (__int32 Row = 0; Row < TargetHeight; Row++)
    {
        __int32 Index = (Row * TargetStride) + ColumnOffset;
        TargetBuffer[Index + 0] = B;
        TargetBuffer[Index + 1] = G;
        TargetBuffer[Index + 2] = R;
        TargetBuffer[Index + 3] = A;
        /*
        double DestinationAlpha = (double) TargetBuffer[Index + 3] / 255.0;
        BYTE FinalB = (B * (Alpha)) + (TargetBuffer[Index + 0] * (DestinationAlpha) *(255 - A));
        BYTE FinalG = (G * (Alpha)) + (TargetBuffer[Index + 1] * (DestinationAlpha) *(255 - A));
        BYTE FinalR = (R * (Alpha)) + (TargetBuffer[Index + 2] * (DestinationAlpha) *(255 - A));
        BYTE FinalA = A + (TargetBuffer[Index + 3] * (255 - A));
        TargetBuffer[Index + 0] = FinalB;
        TargetBuffer[Index + 1] = FinalG;
        TargetBuffer[Index + 2] = FinalR;
        TargetBuffer[Index + 3] = FinalA;
        */
    }

    return TRUE;
}

/// <summary>
/// Draw a vertical line in <paramref name="Buffer"/>.
/// </summary>
/// <param name="Buffer">The buffer where the line will be drawn.</param>
/// <param name="Width">The width of the buffer.</param>
/// <param name="Height">The height of the buffer.</param>
/// <param name="Stride">The stride of the buffer.</param>
/// <param name="ColorIndex">Determines where and what color the line will be.</param>
/// <param name="ColorSet">Source for line location and color.</param>
void DrawVerticalLine(BYTE* Buffer, __int32 Width, __int32 Height, __int32 Stride, __int32 ColorIndex, PureColorType *ColorSet)
{
    UINT32 MaxValidIndex = ((Width * Stride) * Height) - 1;
    for (int Y = 0; Y < Height; Y++)
    {
        int YLineIndex = (Y * Stride) + (ColorSet[ColorIndex].X * 4);
        if ((UINT32)(YLineIndex + 3) > MaxValidIndex)
            continue;
        Buffer[YLineIndex + 0] = ColorSet[ColorIndex].Blue;
        Buffer[YLineIndex + 1] = ColorSet[ColorIndex].Green;
        Buffer[YLineIndex + 2] = ColorSet[ColorIndex].Red;
        Buffer[YLineIndex + 3] = ColorSet[ColorIndex].Alpha;
    }
}

/// <summary>
/// Draw a horizontal line in <paramref name="Buffer"/>.
/// </summary>
/// <param name="Buffer">The buffer where the line will be drawn.</param>
/// <param name="Width">The width of the buffer.</param>
/// <param name="Height">The height of the buffer.</param>
/// <param name="Stride">The stride of the buffer.</param>
/// <param name="ColorIndex">Determines where and what color the line will be.</param>
/// <param name="ColorSet">Source for line location and color.</param>
void DrawHorizontalLine(BYTE* Buffer, __int32 Width, __int32 Height, __int32 Stride, __int32 ColorIndex, PureColorType *ColorSet)
{
    UINT32 MaxValidIndex = ((Width * Stride) * Height) - 1;
    for (int X = 0; X < Width; X++)
    {
        int XLineIndex = (ColorSet[ColorIndex].Y * Stride) + (X * 4);
        if ((UINT32)(XLineIndex + 3) > MaxValidIndex)
            continue;
        Buffer[XLineIndex + 0] = ColorSet[ColorIndex].Blue;
        Buffer[XLineIndex + 1] = ColorSet[ColorIndex].Green;
        Buffer[XLineIndex + 2] = ColorSet[ColorIndex].Red;
        Buffer[XLineIndex + 3] = ColorSet[ColorIndex].Alpha;
    }
}

/// <summary>
/// Render a color blob in the provided buffer. The buffer defines the size of the blob to render.
/// </summary>
/// <param name="Target">Where the blob will be rendered.</param>
/// <param name="ImageWidth">The width of the blob/target buffer.</param>
/// <param name="ImageHeight">The height of the blob/target buffer.</param>
/// <param name="ImageStride">The stride of the blob/target buffer.</param>
/// <param name="BlobColor">Packed blob color.</param>
/// <param name="CenterAlpha">The alpha value at the center of the blob.</param>
/// <param name="EdgeAlpha">The alpha value at the edge of the blob.</param>
/// <param name="EdgeColor">
/// The color of the border to draw around the edge of the enclosing rectangle. If this color's alpha is 0x0, no edge drawing is done.
/// </param>
/// <returns>TRUE on success, FALSE on error.</returns>
__int32 RenderColorBlob(void *Target, __int32 ImageWidth, __int32 ImageHeight, __int32 ImageStride,
    UINT32 BlobColor, byte CenterAlpha, byte EdgeAlpha, UINT32 EdgeColor)
{
    if (Target == NULL)
        return NullPointer;
    __int32 PixelSize = 4;
    BYTE* Buffer = (BYTE*)Target;
    //UINT32 MaxValidIndex = ((ImageWidth * ImageStride) * ImageHeight) - 1;
    int Radius = ImageWidth / 2;
    int AlphaDelta = (CenterAlpha - EdgeAlpha);
    int AlphaDirection = AlphaDelta >= 0 ? 1 : -1;
    AlphaDelta = abs(AlphaDelta);
    __int32 CenterX = ImageWidth / 2;
    __int32 CenterY = ImageHeight / 2;
    BYTE EdgeB = (BYTE)((EdgeColor & 0xff000000) >> 24);
    BYTE EdgeG = (BYTE)((EdgeColor & 0x00ff0000) >> 16);
    BYTE EdgeR = (BYTE)((EdgeColor & 0x0000ff00) >> 8);
    BYTE EdgeA = (BYTE)((EdgeColor & 0x000000ff) >> 0);
    BYTE CenterBlue = (BYTE)((BlobColor & 0xff000000) >> 24);
    BYTE CenterGreen = (BYTE)((BlobColor & 0x00ff0000) >> 16);
    BYTE CenterRed = (BYTE)((BlobColor & 0x0000ff00) >> 8);

    for (__int32 Row = 0; Row < ImageHeight; Row++)
    {
        __int32 RowOffset = Row * ImageStride;
        for (__int32 Column = 0; Column < ImageWidth; Column++)
        {
            __int32 Index = RowOffset + (Column * PixelSize);
            //            if ((UINT32)(Index + PixelSize) > MaxValidIndex)
            //                return BadIndex;

            if (EdgeA > 0x0)
            {
                if (
                    (Row == 0) || (Row == ImageHeight - 1) || (Column == 0) || (Column == ImageWidth - 1)
                    )
                {
                    Buffer[Index + 0] = EdgeB;
                    Buffer[Index + 1] = EdgeG;
                    Buffer[Index + 2] = EdgeR;
                    Buffer[Index + 3] = EdgeA;
                    continue;
                }
            }

            byte iR = CenterRed;
            byte iG = CenterGreen;
            byte iB = CenterBlue;
            byte iA = 0x0;

            double Dist = Distance(Column, Row, CenterX, CenterY);
            if (Dist > Radius)
            {
                Buffer[Index + 0] = 0xff;
                Buffer[Index + 1] = 0xff;
                Buffer[Index + 2] = 0xff;
                Buffer[Index + 3] = 0x0;
                continue;
            }
            if (Dist == 0)
                iA = CenterAlpha;
            else
            {
                double Percent = Dist / Radius;
                //if (AlphaDirection == 1)
                Percent = 1.0 - Percent;
                if (Percent < 0.0)
                    Percent = 0.0;
                if (AlphaDelta == 0)
                    iA = 0xff;
                else
                    iA = (byte)(Percent * (double)AlphaDelta);
            }

            Buffer[Index + 0] = iB;
            Buffer[Index + 1] = iG;
            Buffer[Index + 2] = iR;
            Buffer[Index + 3] = iA;
        }
    }

    return Success;
}