#include "ColorBlender.h"

/// <summary>
/// Mirror an image vertically (e.g., top and bottom). This funtion works on whole pixels so stide is not needed.
/// </summary>
/// <param name="Source">Image source.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Destination">Where the final image will be placed.</param>
/// <param name="Pivot">If non-zero, the Y coordinate where the mirroring will take place.</param>
/// <returns>Value indicating operational results.</returns>
int SimpleVerticalKaleidoscope(void *Source, __int32 Width, __int32 Height, void *Destination, __int32 Pivot)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (Pivot > Height - 1)
        return InvalidOperation;
    if (Pivot < 1)
        Pivot = Height / 2;
    int TopHeight = Pivot;
    int BottomHeight = Height - Pivot - 1;

    UINT32 *Src = (UINT32 *)Source;
    UINT32 *Dest = (UINT32 *)Destination;
    UINT32 *Top = new UINT32[Width * TopHeight];
    UINT32 *Bottom = new UINT32[Width * BottomHeight];

    int OpResult = NotSet;
    OpResult = CopyBufferRegionPixel(Source, Width, Height, Top, 0, 0, Width - 1, TopHeight);
    if (OpResult == Success)
    {
        OpResult = VerticalMirrorPixelRegion(Source, Width, Height, Bottom, 0, TopHeight + 1, Width - 1, BottomHeight);
        if (OpResult == Success)
        {
            OpResult = CopyBufferRegionPixel(Top, Width, TopHeight, Destination, 0, 0, Width - 1, TopHeight);
            if (OpResult == Success)
            {
                OpResult = CopyBufferRegionPixel(Bottom, Width, BottomHeight, Destination, 0, TopHeight + 1, Width - 1, BottomHeight);
            }
        }
    }

    delete[] Top;
    delete[] Bottom;
    return OpResult;
}

/// <summary>
/// Mirror an image horizontally (e.g., left and right). This funtion works on whole pixels so stide is not needed.
/// </summary>
/// <param name="Source">Image source.</param>
/// <param name="Width">Width of the source and destination.</param>
/// <param name="Height">Height of the source and destination.</param>
/// <param name="Destination">Where the final image will be placed.</param>
/// <param name="Pivot">If non-zero, the X coordinate where the mirroring will take place.</param>
/// <returns>Value indicating operational results.</returns>
int SimpleHorizontalKaleidoscope(void *Source, __int32 Width, __int32 Height, void *Destination, __int32 Pivot)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    if (Pivot > Width - 1)
        return InvalidOperation;
    if (Pivot < 1)
        Pivot = Width / 2;
    int LeftWidth = Pivot;
    int RightWidth = Width - Pivot - 1;

    UINT32 *Src = (UINT32 *)Source;
    UINT32 *Dest = (UINT32 *)Destination;

    int OpResult = NotSet;
    OpResult = CopyBufferRegionPixel(Source, Width, Height, Destination, 0, 0, LeftWidth, Height);
    if (OpResult)
    {
        UINT32 *Right = new UINT32[RightWidth * Height];
        OpResult = HorizontalMirrorPixelRegion(Source, Width, Height, Destination, Pivot + 1, 0, Width, Height);
    }

    return OpResult;
}