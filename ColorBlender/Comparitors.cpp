#include "ColorBlender.h"
#include <stdint.h>

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Compare a region in two images with a simple byte-by-byte comparison.
/// </summary>
/// <param name="Image1">First image to compare.</param>
/// <param name="Image2">Second image to compare.</param>
/// <param name="Width">Width of both images.</param>
/// <param name="Height">Height of both images.</param>
/// <param name="Stride">Stride of both images.</param>
/// <param name="Left">The left coordinate of the region to compare.</param>
/// <param name="Top">The top coordinate of the region to compare.</param>
/// <param name="Right">The right coordinate of the region to compare.</param>
/// <param name="Bottom">The bottom coordinate of the region to compare.</param>
/// <param name="MismatchIndex">
/// On miscompares, will contain the index of the first mismatch. Otherwise the contents are not defined. The index is an index
/// into the entire image, not the specified region.
/// </param>
/// <returns>On image equality, returns ImagesMatch. Otherwise returns ImageMismatch.</returns>
int ImageDeltaComparisonRegion(void *Image1, void *Image2, __int32 Width, __int32 Height, __int32 Stride, 
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, UINT32 *MismatchIndex)
{
    if (Image1 == NULL)
        return NullPointer;
    if (Image2 == NULL)
        return NullPointer;
    if (MismatchIndex == NULL)
        return NullPointer;
    if (Left < 0)
        return InvalidOperation;
    if (Right >= Width)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= Height)
        return InvalidOperation;

    *MismatchIndex = UINT32_MAX;
    BYTE *Img1 = (BYTE *)Image1;
    BYTE *Img2 = (BYTE *)Image2;

    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = Left; Column <= Right; Column++)
        {
            int Index = RowOffset + Column;
            if (Img1[Index] != Img2[Index])
            {
                *MismatchIndex = Index;
                return ImageMismatch;
            }
        }
    }

    return ImagesMatch;
}

/// <summary>
/// Compare two images with a simple byte-by-byte comparison.
/// </summary>
/// <param name="Image1">First image to compare.</param>
/// <param name="Image2">Second image to compare.</param>
/// <param name="Width">Width of both images.</param>
/// <param name="Height">Height of both images.</param>
/// <param name="Stride">Stride of both images.</param>
/// <param name="MismatchIndex">On miscompares, will contain the index of the first mismatch. Otherwise the contents are not defined.</param>
/// <returns>On image equality, returns ImagesMatch. Otherwise returns ImageMismatch.</returns>
int ImageDeltaComparison(void *Image1, void *Image2, __int32 Width, __int32 Height, __int32 Stride, UINT32 *MismatchIndex)
{
    return ImageDeltaComparisonRegion(Image1, Image2, Width, Height, Stride, 0, 0, Width - 1, Height - 1, MismatchIndex);
}