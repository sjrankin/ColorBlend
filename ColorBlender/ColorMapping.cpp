#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

//http://en.wikipedia.org/wiki/Color_mapping
int ColorMap1Region(void *Source1, void *Source2, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom, BOOL CopyOutOfRegion, UINT32 PackedOut)
{
    return Success;
}

int ColorMap1(void *Source1, void *Source2, __int32 Width, __int32 Height, __int32 Stride, void *Destination)
{
    return ColorMap1Region(Source1, Source2, Width, Height, Stride, Destination, 0, 0, Width - 1, Height - 1, FALSE, 0x0);
}