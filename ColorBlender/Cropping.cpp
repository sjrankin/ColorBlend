#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int ImageCrop(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    void *Destination)
{
    if (Source == NULL)
        return ErrorStackPushReturn2(NullPointer, "ImageCrop", "Source is NULL.");
    if (Destination == NULL)
        return ErrorStackPushReturn2(NullPointer, "ImageCrop", "Destination is NULL.");
    if (Left < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Left < 0");
    if (Top < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Top < 0");
    if (Right < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Right < 0");
    if (Bottom < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Bottom < 0");
    if (Left == 0 && Top == 0 && Right == 0 && Bottom == 0)
        return ErrorStackPushReturn2(NOP, "ImageCrop", "Nothing to do.");
    if (Left + Right > Width)
        return ErrorStackPushReturn2(ValueTooBig, "ImageCrop", "Left + Right crop values > width.");
    if (Top + Bottom > Height)
        return ErrorStackPushReturn2(ValueTooBig, "ImageCrop", "Top + Bottom crop values > height.");

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;
    __int32 DestWidth = Width - (Left + Right);
    __int32 DestHeight = Height - (Top + Bottom);
    __int32 DestStride = DestWidth * 4;
    int DestRow = 0;
    int DestColumn = 0;

    for (int Row = Top; Row < Height - Bottom; Row++)
    {
        int SourceRowOffset = Row * Stride;
        int DestRowOffset = DestRow * DestStride;
        for (int Column = Left; Column < Width - Right; Column++)
        {
            int SourceIndex = (Column * PixelSize) + SourceRowOffset;
            int DestIndex = (DestColumn * PixelSize) + DestRowOffset;
            Dest[DestIndex + 3] = Src[SourceIndex + 3];
            Dest[DestIndex + 2] = Src[SourceIndex + 2];
            Dest[DestIndex + 1] = Src[SourceIndex + 1];
            Dest[DestIndex + 0] = Src[SourceIndex + 0];
            DestColumn++;
        }
        DestRow++;
        DestColumn = 0;
    }

    return ErrorStackPushReturn(Success, "ImageCrop");
}