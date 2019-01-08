#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int BayerRGGB = 0;
const int BayerBGGR = 1;
const int BayerDemosaicNearest = 0;
const int BayerDemosaicLinear = 1;
const int BayerDemosaicCubic = 2;

int BayerNearestDemosaic(void *Source, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *Destination, int Pattern)
{
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE MeanR = 0x0;
    BYTE MeanG = 0x0;
    BYTE MeanB = 0x0;
    int GTemp0 = 0;
    int GTemp1 = 0;
    int GTemp = 0;
    int PixelSize = 4;
    if (BufferWidth % 2 != 0)
        BufferWidth--;
    if (BufferHeight % 2 != 0)
        BufferHeight--;
    //ClearBufferDWord(Destination, BufferWidth, BufferHeight, 0x0);

    for (int Row = 0; Row < BufferHeight; Row += 2)
    {
        int RowOffset0 = Row * BufferStride;
        int RowOffset1 = (Row + 1) * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column += 2)
        {
            int UL = RowOffset0 + (Column * PixelSize);
            int UR = UL + PixelSize;
            int LL = RowOffset1 + (Column * PixelSize);
            int LR = LL + PixelSize;
            switch (Pattern)
            {
            case BayerRGGB:
                MeanR = (Src[UL + 0] + Src[UL + 1] + Src[UL + 2]) / 3;
                MeanB = (Src[LR + 0] + Src[LR + 1] + Src[LR + 2]) / 3;
                GTemp0 = Src[UR + 0] + Src[UR + 1] + Src[UR + 2];
                GTemp1 = Src[LL + 0] + Src[LL + 1] + Src[LL + 2];
                GTemp = GTemp0 + GTemp1;
                MeanG = (BYTE)(GTemp / 6);
                //Upper-left (red)
                Dest[UL + 3] = 0xff;
                Dest[UL + 2] = MeanR;
                Dest[UL + 1] = MeanG;
                Dest[UL + 0] = MeanB;
                //Upper-right (green)
                Dest[UR + 3] = 0xff;
                Dest[UR + 2] = MeanR;
                Dest[UR + 1] = (GTemp0 / 3);
                Dest[UR + 0] = MeanB;
                //Lower-left (green)
                Dest[LL + 3] = 0xff;
                Dest[LL + 2] = MeanR;
                Dest[LL + 1] = (GTemp1 / 3);
                Dest[LL + 0] = MeanB;
                //Lower-right (blue)
                Dest[LR + 3] = 0xff;
                Dest[LR + 2] = MeanR;
                Dest[LR + 1] = MeanG;
                Dest[LR + 0] = MeanB;
                break;

            case BayerBGGR:
                MeanR = (Src[LR + 0] + Src[LR + 1] + Src[LR + 2]) / 3;
                MeanB = (Src[UL + 0] + Src[UL + 1] + Src[UL + 2]) / 3;
                GTemp0 = Src[UR + 0] + Src[UR + 1] + Src[UR + 2];
                GTemp1 = Src[LL + 0] + Src[LL + 1] + Src[LL + 2];
                GTemp = GTemp0 + GTemp1;
                MeanG = (BYTE)(GTemp / 6);
                //Upper-left (blue)
                Dest[UL + 3] = 0xff;
                Dest[UL + 2] = MeanR;
                Dest[UL + 1] = MeanG;
                Dest[UL + 0] = MeanB;
                //Upper-right (green)
                Dest[UR + 3] = 0xff;
                Dest[UR + 2] = MeanR;
                Dest[UR + 1] = (GTemp0 / 3);
                Dest[UR + 0] = MeanB;
                //Lower-left (green)
                Dest[LL + 3] = 0xff;
                Dest[LL + 2] = MeanR;
                Dest[LL + 1] = (GTemp1 / 3);
                Dest[LL + 0] = MeanB;
                //Lower-right (red)
                Dest[LR + 3] = 0xff;
                Dest[LR + 2] = MeanR;
                Dest[LR + 1] = MeanG;
                Dest[LR + 0] = MeanB;
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

int BayerLinearDemosaic(void *Source, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *Destination, int Pattern)
{
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;
    int WidthRemainder = BufferWidth % 3;
    int HeightRemainder = BufferHeight % 3;
    BufferWidth -= WidthRemainder;
    BufferHeight -= HeightRemainder;
    BYTE G5 = 0;
    BYTE B5 = 0;
    BYTE B2 = 0;
    BYTE B4 = 0;
    BYTE B6 = 0;
    BYTE B8 = 0;
    BYTE G1 = 0;
    BYTE G3 = 0;
    BYTE G7 = 0;
    BYTE G9 = 0;
    BYTE R5 = 0;
    BYTE R2 = 0;
    BYTE R4 = 0;
    BYTE R6 = 0;
    BYTE R8 = 0;
    int Index1 = 0;
    int Index2 = 0;
    int Index3 = 0;
    int Index4 = 0;
    int Index5 = 0;
    int Index6 = 0;
    int Index7 = 0;
    int Index8 = 0;
    int Index9 = 0;

    for (int Row = 0; Row < BufferHeight; Row += 3)
    {
        int RowOffset0 = Row * BufferStride;
        int RowOffset1 = (Row + 1) * BufferStride;
        int RowOffset2 = (Row + 2) * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column += 3)
        {
            Index1 = (Column * PixelSize) + RowOffset0;
            Index2 = Index1 + PixelSize;
            Index3 = Index2 + PixelSize;
            Index4 = (Column * PixelSize) + RowOffset1;
            Index5 = Index4 + PixelSize;
            Index6 = Index5 + PixelSize;
            Index7 = (Column * PixelSize) + RowOffset2;
            Index8 = Index7 + PixelSize;
            Index9 = Index8 + PixelSize;

            switch (Pattern)
            {
            case BayerRGGB:
                //1,1
                G5 = (Src[Index2 + 1] + Src[Index4 + 1] + Src[Index6 + 1] + Src[Index8 + 1]) / 4;
                R5 = (Src[Index1 + 2] + Src[Index3 + 2] + Src[Index6 + 2] + Src[Index8 + 2]) / 4;
                Dest[Index5 + 3] = 0xff;
                Dest[Index5 + 2] = R5;
                Dest[Index5 + 1] = G5;
                Dest[Index5 + 0] = Src[Index5 + 0];
                //1,0
                R2 = (Src[Index1 + 2] + Src[Index3 + 2]) / 2;
                Dest[Index2 + 3] = 0xff;
                Dest[Index2 + 2] = R2;
                Dest[Index2 + 1] = Src[Index2 + 1];
                Dest[Index2 + 0] = Src[Index5 + 0];
                //0,1
                R4 = (Src[Index1 + 2] + Src[Index7 + 2]) / 2;
                Dest[Index4 + 3] = 0xff;
                Dest[Index4 + 2] = R4;
                Dest[Index4 + 1] = Src[Index4 + 1];
                Dest[Index4 + 0] = Src[Index5 + 0];
                //2,1
                R6 = (Src[Index3 + 2] + Src[Index9 + 2]) / 2;
                Dest[Index6 + 3] = 0xff;
                Dest[Index6 + 2] = R6;
                Dest[Index6 + 1] = Src[Index6 + 1];
                Dest[Index6 + 0] = Src[Index5 + 0];
                //1,2
                R8 = (Src[Index7 + 2] + Src[Index9 + 2]) / 2;
                Dest[Index8 + 3] = 0xff;
                Dest[Index8 + 2] = R8;
                Dest[Index8 + 1] = Src[Index8 + 1];
                Dest[Index8 + 0] = Src[Index5 + 0];;
                //0,0
                G1 = (Src[Index2 + 1] + Src[Index4 + 1]) / 2;
                Dest[Index1 + 3] = 0xff;
                Dest[Index1 + 2] = Src[Index1 + 2];
                Dest[Index1 + 1] = G1;
                Dest[Index1 + 0] = Src[Index5 + 0];
                //2,0
                G3 = (Src[Index2 + 1] + Src[Index6 + 1]) / 2;
                Dest[Index3 + 3] = 0xff;
                Dest[Index3 + 2] = Src[Index3 + 2];
                Dest[Index3 + 1] = G3;
                Dest[Index3 + 0] = Src[Index5 + 0];
                //0,2
                G7 = (Src[Index4 + 1] + Src[Index8 + 1]) / 2;
                Dest[Index7 + 3] = 0xff;
                Dest[Index7 + 2] = Src[Index7 + 2];
                Dest[Index7 + 1] = G7;
                Dest[Index7 + 0] = Src[Index5 + 0];
                //2,2
                G9 = (Src[Index6 + 1] + Src[Index8 + 1]) / 2;
                Dest[Index9 + 3] = 0xff;
                Dest[Index9 + 2] = Src[Index9 + 2];
                Dest[Index9 + 1] = G9;
                Dest[Index9 + 0] = Src[Index5 + 0];
                break;

            case BayerBGGR:
                //1,1
                G5 = (Src[Index2 + 1] + Src[Index4 + 1] + Src[Index6 + 1] + Src[Index8 + 1]) / 4;
                B5 = (Src[Index2 + 0] + Src[Index4 + 0] + Src[Index6 + 0] + Src[Index8 + 0]) / 4;
                Dest[Index5 + 3] = 0xff;
                Dest[Index5 + 2] = Src[Index5 + 2];
                Dest[Index5 + 1] = G5;
                Dest[Index5 + 0] = B5;
                //1,0
                B2 = (Src[Index1 + 0] + Src[Index3 + 0]) / 2;
                Dest[Index2 + 3] = 0xff;
                Dest[Index2 + 2] = Src[Index5 + 2];
                Dest[Index2 + 1] = Src[Index2 + 1];
                Dest[Index2 + 0] = B2;
                //0,1
                B4 = (Src[Index1 + 0] + Src[Index7 + 0]) / 2;
                Dest[Index4 + 3] = 0xff;
                Dest[Index4 + 2] = Src[Index5 + 2];
                Dest[Index4 + 1] = Src[Index4 + 1];
                Dest[Index4 + 0] = B4;
                //2,1
                B6 = (Src[Index3 + 0] + Src[Index9 + 0]) / 2;
                Dest[Index6 + 3] = 0xff;
                Dest[Index6 + 2] = Src[Index5 + 2];
                Dest[Index6 + 1] = Src[Index6 + 1];
                Dest[Index6 + 0] = B6;
                //1,2
                B8 = (Src[Index7 + 0] + Src[Index9 + 0]) / 2;
                Dest[Index8 + 3] = 0xff;
                Dest[Index8 + 2] = Src[Index5 + 2];
                Dest[Index8 + 1] = Src[Index8 + 1];
                Dest[Index8 + 0] = B8;
                //0,0
                G1 = (Src[Index2 + 1] + Src[Index4 + 1]) / 2;
                Dest[Index1 + 3] = 0xff;
                Dest[Index1 + 2] = Src[Index5 + 2];
                Dest[Index1 + 1] = G1;
                Dest[Index1 + 0] = Src[Index1 + 0];
                //2,0
                G3 = (Src[Index2 + 1] + Src[Index6 + 1]) / 2;
                Dest[Index3 + 3] = 0xff;
                Dest[Index3 + 2] = Src[Index5 + 2];
                Dest[Index3 + 1] = G3;
                Dest[Index3 + 0] = Src[Index3 + 0];
                //0,2
                G7 = (Src[Index4 + 1] + Src[Index8 + 1]) / 2;
                Dest[Index7 + 3] = 0xff;
                Dest[Index7 + 2] = Src[Index5 + 2];
                Dest[Index7 + 1] = G7;
                Dest[Index7 + 0] = Src[Index7 + 0];
                //2,2
                G9 = (Src[Index6 + 1] + Src[Index8 + 1]) / 2;
                Dest[Index9 + 3] = 0xff;
                Dest[Index9 + 2] = Src[Index5 + 2];
                Dest[Index9 + 1] = G9;
                Dest[Index9 + 0] = Src[Index9 + 0];
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

int BayerCubicDemosaic(void *Source, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *Destination, int Pattern)
{
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
        }
    }

    return Success;
}

int BayerDemosaic(void *Source, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    void *Destination, int Pattern, int Method)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    int Result = InvalidOperation;

    switch (Method)
    {
    case BayerDemosaicNearest:
        Result = BayerNearestDemosaic(Source, BufferWidth, BufferHeight, BufferStride, Destination, Pattern);
        break;

    case BayerDemosaicLinear:
        Result = BayerLinearDemosaic(Source, BufferWidth, BufferHeight, BufferStride, Destination, Pattern);
        break;

    case BayerDemosaicCubic:
        Result = BayerCubicDemosaic(Source, BufferWidth, BufferHeight, BufferStride, Destination, Pattern);
        break;

    default:
        Result = InvalidOperation;
        break;
    }

    return Result;
}