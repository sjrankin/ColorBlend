#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Generates an octree based on the passed image.
/// </summary>
/// <remarks>
/// http://www.microsoft.com/msj/archive/S3F1.aspx
/// http://rosettacode.org/wiki/Color_quantization/C
/// http://www.codeproject.com/Articles/109133/Octree-Color-Palette
/// </remarks>
/// <param name="Source">Pointer to the source image from which the octree is generated.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Count">Desired number of colors in the resultant octree.</param>
/// <param name="OTree">On success, the octree structure.</param>
/// <param name="OTreeCount">TBD.</param>
/// <returns>Value indicating operational success.</returns>
int Octree(void *Source, __int32 Width, __int32 Height, __int32 Stride, int Count, void *OTree, int *OTreeCount)
{
    if (Source == NULL)
        return NullPointer;
    if (Count < 2)
        return InvalidOperation;
    if (Count > 256)
        return InvalidOperation;

    return Success;
}

/// <summary>
/// Reduce the packed color to a color from the passed octree.
/// </summary>
/// <param name="OTree">Octree with the set of desired colors.</param>
/// <param name="PackedColor">Packed ARGB color to reduce.</param>
/// <returns>Reduced color based on <paramref name="PackedColor"/>.</returns>
UINT32 ReduceColor(void *OTree, UINT32 PackedColor)
{
    return 0x0;
}

/// <summary>
/// Reduce the passed color to a color from the passed octree.
/// </summary>
/// <remarks>
/// Packs the passed color and calls ReduceColor.
/// </remarks>
/// <param name="OTree">Octree with the set of desired colors.</param>
/// <param name="Red">Red channel of the color to reduce.</param>
/// <param name="Green">Green channel of the color to reduce.</param>
/// <param name="Blue">Blue channel of the color to reduce.</param>
/// <returns>Reduced color based on the passed color.</returns>
UINT32 ReduceColor2(void *OTree, BYTE Red, BYTE Green, BYTE Blue)
{
    return ReduceColor(OTree, 0xff000000 | Red << 16 | Green << 8 | Blue << 0);
}

/// <summary>
/// Reduce the colors in an image using an octree.
/// </summary>
/// <param name="Source">Pointer to the source image from which the octree is generated.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Resultant (non-indexed) image with reduced colors.</param>
/// <param name="OTree">The resultant octree.</param>
/// <param name="OTreeCount">TBD</param>
/// <returns>Value indicating operational success.</returns>
int ReduceColors(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    void *OTree, int OTreeCount)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (OTree == NULL)
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
            UINT32 Reduced = ReduceColor2(OTree, Src[Index + 2], Src[Index + 1], Src[Index + 0]);
            Dest[Index + 3] = Src[Index + 3];
            Dest[Index + 2] = (Reduced & 0x00ff0000) >> 16;
            Dest[Index + 1] = (Reduced & 0x0000ff00) >> 8;
            Dest[Index + 0] = (Reduced & 0x000000ff) >> 0;
        }
    }

    return Success;
}


/// http://web.cs.wpi.edu/~matt/courses/cs563/talks/color_quant/CQindex.html

/// <summary>
/// Reduce the color count in the source image using a median cut algorithm and place the
/// result in the destination image.
/// </summary>
/// <remarks>
/// <paramref name="Destination"/> must have the same dimensions as <paramref name="Source"/>, including stride. Use
/// MedianCutToIndexed to return an indexed image.
/// </remarks>
/// <param name="Source">Pointer to the source image to be reduced.</param>
/// <param name="Width">Width of the images.</param>
/// <param name="Height">Height of the images.</param>
/// <param name="Stride">Stride of the images.</param>
/// <param name="Destination">Where the resultant image will be placed.</param>
/// <returns>Value indicating operational success.</returns>
int MedianCut(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    return Success;
}

/// <summary>
/// Reduce the color count in the source image using a median cut algorithm and place the
/// result in the indexed destination image.
/// </summary>
/// <remarks>
/// The dimensions (but not stride) must be the same for <paramref name="Source"/> and <paramref name="IndexedDestination"/>.
/// </remarks>
/// <param name="Source">Pointer to the source image to be reduced.</param>
/// <param name="Width">Width of the images.</param>
/// <param name="Height">Height of the images.</param>
/// <param name="Stride">Stride of the images.</param>
/// <param name="IndexedDestination">
/// Where the resultant image will be placed. The stride of this image is the same as the width of the image.
/// </param>
/// <param name="PaletteData">Will contain the palette for the indexed image.</param>
/// <param name="PaletteSize">Size of the final palette.</param>
/// <returns>Value indicating operational success.</returns>
int MedianCutToIndexed(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *IndexedDestination, void *PaletteData, int PaletteSize)
{
    if (Source == NULL)
        return NullPointer;
    if (IndexedDestination == NULL)
        return NullPointer;
    if (PaletteData == NULL)
        return NullPointer;
    if ((PaletteSize < 1) || (PaletteSize > 256))
        return InvalidOperation;

    return Success;
}

/// <summary>
/// Returns mean brightness of the passed image.
/// </summary>
/// <param name="Source">Pointer to the source image to be measured.</param>
/// <param name="Width">Width of the images.</param>
/// <param name="Height">Height of the images.</param>
/// <param name="Stride">Stride of the images.</param>
/// <param name="Left">Left coordinate of the operational region.</param>
/// <param name="Top">Top coordinate of the operational region.</param>
/// <param name="Right">Right coordinate of the operational region.</param>
/// <param name="Bottom">Bottom coordinate of the operational region.</param>
/// <returns>Mean brightness of the image.</returns>
double OverallBrightnessRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
    if (Source == NULL)
        return NullPointer;
    if (Left < 0)
        return InvalidOperation;
    if (Right >= Width)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= Height)
        return InvalidOperation;

    int PixelSize = 4;
    int PixelCount = 0;
    double Accumulator = 0.0;
    BYTE *Src = (BYTE *)Source;

    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = Left; Column <= Right; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            if ((Column >= Left) && (Column <= Right) && (Row >= Top) && (Row <= Bottom))
            {
                Accumulator += ColorLuminance(Src[Index + 2], Src[Index + 1], Src[Index + 0]);
                PixelCount++;
            }
        }
    }

    double Final = Accumulator / (double)PixelCount;
    return Final;
}

/// <summary>
/// Returns mean brightness of the passed image.
/// </summary>
/// <param name="Source">Pointer to the source image to be measured.</param>
/// <param name="Width">Width of the images.</param>
/// <param name="Height">Height of the images.</param>
/// <param name="Stride">Stride of the images.</param>
/// <returns>Mean brightness of the image.</returns>
double OverallBrightness(void *Source, __int32 Width, __int32 Height, __int32 Stride)
{
    return OverallBrightnessRegion(Source, Width, Height, Stride, 0, 0, Width - 1, Height - 1);
}

