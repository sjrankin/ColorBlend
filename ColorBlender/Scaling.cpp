#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int ScaleImage(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight, int ScalingMethod)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (DestWidth == 0 || DestHeight == 0)
        return InvalidOperation;
    if (Width == DestWidth && Height == DestHeight)
        return NoActionTaken;

    switch (ScalingMethod)
    {
    case NoScaling:
        return NoActionTaken;

    case NearestNeighbor:
        return NearestNeighborScaling(Source, Width, Height, Stride, Destination, DestWidth, DestHeight);

    case Bilinear:
        return BilinearScaling(Source, Width, Height, Stride, Destination, DestWidth, DestHeight);
    }

    return InvalidOperation;
}

int NearestNeighborScaling(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (DestWidth == 0 || DestHeight == 0)
        return InvalidOperation;
    if (Width == DestWidth && Height == DestHeight)
        return NoActionTaken;

    UINT32 *Src = (UINT32 *)Source;
    UINT32 *Dest = (UINT32 *)Destination;

    double HorizontalRatio = (double)Width / (double)DestWidth;
    double VerticalRatio = (double)Height / (double)DestHeight;

    for (int Row = 0; Row < DestHeight; Row++)
    {
        for (int Column = 0; Column < DestWidth; Column++)
        {
            double NewX = floorf((double)Column * HorizontalRatio);
            double NewY = floorf((double)Row * VerticalRatio);
            Dest[(Row * DestWidth) + Column] = Src[(int)((NewY * Width) + NewX)];
        }
    }

    return Success;
}

int BilinearScaling(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, __int32 DestWidth, __int32 DestHeight)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (DestWidth == 0 || DestHeight == 0)
        return InvalidOperation;
    if (Width == DestWidth && Height == DestHeight)
        return NoActionTaken;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    double HorizontalRatio = ((double)(Width - 1)) / DestWidth;
    double VerticalRatio = ((double)(Height - 1)) / DestHeight;
    int DestIndex = 0;

    for (int Row = 0; Row < DestHeight; Row++)
    {
        for (int Column = 0; Column < DestWidth; Column++)
        {
            int X = (int)(HorizontalRatio * Column);
            int Y = (int)(VerticalRatio * Row);
            double XDiff = (HorizontalRatio * Column) - X;
            double YDiff = (VerticalRatio * Row) - Y;
            int Index = (Y * Width) + X;
            int a = Src[Index];
            int b = Src[Index + 1];
            int c = Src[Index + Width];
            int d = Src[Index + Width + 1];
            double Blue = (a & 0xff) * (1 - XDiff) * (1 - YDiff) +
                (b & 0xff) * (XDiff)* (1 - YDiff) +
                (c & 0xff) * (YDiff)* (1 - XDiff) +
                (d & 0xff) * (XDiff * YDiff);
            double Green = ((a >> 8) & 0xff) * (1 - XDiff) * (1 - YDiff) + 
                ((b >> 8) & 0xff) * (XDiff) * (1 - YDiff) +
                ((c >> 8) & 0xff) * (YDiff) * (1 - XDiff) + 
                ((d >> 8) & 0xff) * (XDiff * YDiff);
            double Red = ((a >> 16) & 0xff) * (1 - XDiff) * (1 - YDiff) + 
                ((b >> 16) & 0xff) * (XDiff) * (1 - YDiff) +
                ((c >> 16) & 0xff) * (YDiff) * (1 - XDiff) + 
                ((d >> 16) & 0xff) * (XDiff * YDiff);
            Dest[DestIndex++] = 0xff000000 |
                                ((((int)Red) << 16) & 0xff0000) |
                                ((((int)Green) << 8) & 0xff00) |
                                ((int)Blue);
        }
    }

    return Success;
}
