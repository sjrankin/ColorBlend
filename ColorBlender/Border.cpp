#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int AddBorder(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    __int32 LeftBorder, __int32 TopBorder, __int32 RightBorder, __int32 BottomBorder,
    void *Destination, UINT32 BGColor)
{
    if (Source == NULL)
        return ErrorStackPushReturn2(NullPointer, "AddBorder", "Source is null");
    if (Destination == NULL)
        return ErrorStackPushReturn2(NullPointer, "AddBorder", "Destination is null");
    if (LeftBorder < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "AddBorder", "LeftBorder < 0");
    if (TopBorder < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "AddBorder", "TopBorder < 0");
    if (RightBorder < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "AddBorder", "RightBorder < 0");
    if (BottomBorder < 0)
        return ErrorStackPushReturn2(ValueTooSmall, "AddBorder", "BottomBorder < 0");

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;
    BYTE A = (BGColor & 0xff000000) >> 24;
    BYTE R = (BGColor & 0x00ff0000) >> 16;
    BYTE G = (BGColor & 0x0000ff00) >> 8;
    BYTE B = (BGColor & 0x000000ff) >> 0;

    //Draw the new image's background.
    __int32 DestHeight = Height + TopBorder + BottomBorder;
    __int32 DestWidth = Width + LeftBorder + RightBorder;
    __int32 DestStride = DestWidth * 4;
    for (int Row = 0; Row < DestHeight; Row++)
    {
        int RowOffset = Row * DestStride;
        for (int Column = 0; Column < DestWidth; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    //Blit the old image onto the new image.
    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        int DestRowOffset = (Row + TopBorder) * DestStride;
        for (int Column = 0; Column < Width; Column++)
        {
            int SourceIndex = (Column * PixelSize) + RowOffset;
            int DestIndex = ((Column + LeftBorder) * PixelSize) + DestRowOffset;
            Dest[DestIndex + 3] = Src[SourceIndex + 3];
            Dest[DestIndex + 2] = Src[SourceIndex + 2];
            Dest[DestIndex + 1] = Src[SourceIndex + 1];
            Dest[DestIndex + 0] = Src[SourceIndex + 0];
        }
    }

    return ErrorStackPushReturn(Success, "AddBorder");
}