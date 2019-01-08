#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int ConvertGray8ToBGRA32(void *Source, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *Destination)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int DestPixelSize = 4;
    int SrcPixelSize = 1;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int DestIndex = 0;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * DestPixelSize);
            {
                BYTE Gray = Src[DestIndex++];
                Dest[Index + 3] = 0xff;
                Dest[Index + 2] = Gray;
                Dest[Index + 1] = Gray;
                Dest[Index + 0] = Gray;
            }
        }
    }

    return Success;
}