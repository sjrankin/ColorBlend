#include "ColorBlender.h"
#include "Structures.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const __int32 SegmentNoMirror = 0;
const __int32 SegmentHorizontalMirror = 1;
const __int32 SegmentVerticalMirror = 2;

int SegmentizeImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 CellWidth, __int32 CellHeight, __int32 CellOriginX, __int32 CellOriginY, __int32 SegmentPattern)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    int PixelSize = 4;
    UINT32 *Src = (UINT32 *)Source;
    UINT32 *Dest = (UINT32 *)Destination;
    int HorizontalSegmentCount = Width / (CellWidth * 2);
    int HorizontalSegmentRemainder = Width % (CellWidth * 2);
    int VerticalSegmentCount = Height / (CellHeight * 2);
    int VerticalSegmentRemainder = Height % (CellHeight * 2);
    int CellSize = CellWidth * CellHeight;

    UINT32 *Cell = new UINT32[CellSize];
    int Result = CopyBufferRegion(Source, Cell, Width, Height, Stride, CellOriginX, CellOriginY, CellOriginX + CellWidth,
        CellOriginX + CellHeight);
    if (Result != Success)
    {
        delete[] Cell;
        return Result;
    }

    int SegmentWidth = 0;
    int SegmentHeight = 0;
    if (SegmentPattern == SegmentHorizontalMirror)
    {
        SegmentWidth = CellWidth * 2;
        SegmentHeight = CellHeight;
    }
    else
    {
        SegmentWidth = CellWidth;
        SegmentHeight = CellHeight * 2;
    }
    UINT32 *Segment = new UINT32[SegmentWidth * SegmentHeight * PixelSize];

    AbsolutePointStruct *UL = new AbsolutePointStruct();
    UL->X = 0;
    UL->Y = 0;
    AbsolutePointStruct *LR = new AbsolutePointStruct();
    LR->X = CellWidth - 1;
    LR->Y = CellHeight - 1;

    PasteRegion4(Destination, Width, Height, Segment, SegmentWidth, SegmentHeight, UL->X,UL->Y,LR->X,LR->Y);
    return Success;

    PasteRegion2(Segment, SegmentWidth, SegmentHeight, SegmentWidth * PixelSize,
        Cell, CellWidth, CellHeight, CellWidth * PixelSize, UL, LR);
    UINT32 *MCell = NULL;
    if (SegmentPattern == SegmentHorizontalMirror)
    {
        MCell = new UINT32[CellSize];
        HorizontalMirrorPixel(Cell, CellWidth, CellHeight, MCell);
        UL->X = CellWidth;
        UL->Y = 0;
        LR->X = SegmentWidth - 1;
        LR->Y = CellHeight - 1;
        PasteRegion2(Segment, SegmentWidth, SegmentHeight, SegmentWidth * PixelSize,
            MCell, CellWidth, CellHeight, CellWidth * PixelSize, UL, LR);
    }
    else
    {
        MCell = new UINT32[CellSize];
        VerticalMirrorPixel(Cell, CellWidth, CellHeight, MCell);
        UL->X = 0;
        UL->Y = CellHeight;
        LR->X = SegmentWidth - 1;
        LR->Y = SegmentHeight - 1;
        PasteRegion2(Segment, SegmentWidth, SegmentHeight, SegmentWidth * PixelSize,
            MCell, CellWidth, CellHeight, CellWidth * PixelSize, UL, LR);
    }

    for (int Row = 0; Row < VerticalSegmentCount + 1; Row++)
    {
        for (int Column = 0; Column < HorizontalSegmentCount + 1; Column++)
        {
            UL->X = Row + Column;
            UL->Y = Row;
            LR->X = UL->X + SegmentWidth;
            LR->Y = UL->Y + SegmentHeight;
            PasteRegion4(Dest, Width, Height, Segment, SegmentWidth, SegmentHeight, UL->X, UL->Y, LR->X,LR->Y);
        }
    }

    delete UL;
    delete LR;
    delete[] MCell;
    delete[] Segment;
    delete[] Cell;
    return Success;
}