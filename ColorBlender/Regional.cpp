#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include <float.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const __int32 RegionGreatest = 0;
const __int32 RegionLeast = 1;
const __int32 RegionBrightest = 2;
const __int32 RegionDarkest = 3;
const __int32 RegionMean = 4;

int RegionalOperation(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    UINT32 RegionWidth, UINT32 RegionHeight, bool DoAlpha, bool DoRed, bool DoGreen, bool DoBlue,
    UINT32 Operator)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (RegionWidth * RegionHeight == 0)
        return NoActionTaken;
    if ((RegionWidth & 0x1) == 0)
        return InvalidOperation;
    if ((RegionHeight & 0x1) == 0)
        return InvalidOperation;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;
    int WidthHalfSpan = RegionWidth / 2;
    int HeightHalfSpan = RegionHeight / 2;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            Dest[Index + 3] = Src[Index + 3];
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 0] = Src[Index + 0];

            int KHStart = Column - WidthHalfSpan;
            if (KHStart < 0)
                KHStart = 0;
            int KHEnd = Column + WidthHalfSpan;
            if (KHEnd >= Width)
                KHEnd = Width - 1;

            int KVStart = Row - HeightHalfSpan;
            if (KVStart < 0)
                KVStart = 0;
            int KVEnd = Row + HeightHalfSpan;
            if (KVEnd >= Height)
                KVEnd = Height - 1;

            double GreatestLuminance = 0.0;
            double LeastLuminance = 1.0;
            double AAcc = 0.0;
            double RAcc = 0.0;
            double GAcc = 0.0;
            double BAcc = 0.0;
            BYTE WorkingA = 0;
            BYTE WorkingR = 0;
            BYTE WorkingG = 0;
            BYTE WorkingB = 0;
            if (Operator == RegionBrightest)
            {
                WorkingA = 0;
                WorkingR = 0;
                WorkingG = 0;
                WorkingB = 0;
            }
            if (Operator == RegionDarkest)
            {
                WorkingA = 255;
                WorkingR = 255;
                WorkingG = 255;
                WorkingB = 255;
            }


            for (int KY = KVStart; KY <= KVEnd; KY++)
            {
                int KYOffset = KY * Stride;
                for (int KX = KHStart; KX <= KHEnd; KX++)
                {
                    int KIndex = (KX * PixelSize) + KYOffset;

                    switch (Operator)
                    {
                    case RegionMean:
                        AAcc += Src[KIndex + 3];
                        RAcc += Src[KIndex + 2];
                        GAcc += Src[KIndex + 1];
                        BAcc += Src[KIndex + 0];
                        break;

                    case RegionBrightest:
                    {
                        double BTemp = ColorLuminance(Src[KIndex + 2], Src[KIndex + 1], Src[KIndex + 0]);
                        if (BTemp > GreatestLuminance)
                        {
                            GreatestLuminance = BTemp;
                            WorkingA = Src[KIndex + 3];
                            WorkingR = Src[KIndex + 2];
                            WorkingG = Src[KIndex + 1];
                            WorkingB = Src[KIndex + 0];
                        }
                    }
                    break;

                    case RegionDarkest:
                    {
                        double DTemp = ColorLuminance(Src[KIndex + 2], Src[KIndex + 1], Src[KIndex + 0]);
                        if (DTemp < GreatestLuminance)
                        {
                            LeastLuminance = DTemp;
                            WorkingA = Src[KIndex + 3];
                            WorkingR = Src[KIndex + 2];
                            WorkingG = Src[KIndex + 1];
                            WorkingB = Src[KIndex + 0];
                        }
                    }
                    break;

                    case RegionGreatest:
                        WorkingA = max(WorkingA, Src[KIndex + 3]);
                        WorkingR = max(WorkingR, Src[KIndex + 2]);
                        WorkingG = max(WorkingG, Src[KIndex + 1]);
                        WorkingB = max(WorkingB, Src[KIndex + 0]);
                        break;

                    case RegionLeast:
                        WorkingA = min(WorkingA, Src[KIndex + 3]);
                        WorkingR = min(WorkingR, Src[KIndex + 2]);
                        WorkingG = min(WorkingG, Src[KIndex + 1]);
                        WorkingB = min(WorkingB, Src[KIndex + 0]);
                        break;
                    }
                }
            }

            if (Operator == RegionMean)
            {
                double Area = (double)RegionWidth * (double)RegionHeight;
                WorkingA = (BYTE)(AAcc / Area);
                WorkingR = (BYTE)(RAcc / Area);
                WorkingG = (BYTE)(GAcc / Area);
                WorkingB = (BYTE)(BAcc / Area);
            }

            for (int KY = KVStart; KY <= KVEnd; KY++)
            {
                int KYOffset = KY * Stride;
                for (int KX = KHStart; KX <= KHEnd; KX++)
                {
                    int KIndex = (KX * PixelSize) + KYOffset;
                    if (DoAlpha)
                        Src[KIndex + 3] = WorkingA;
                    if (DoRed)
                        Src[KIndex + 2] = WorkingR;
                    if (DoGreen)
                        Src[KIndex + 1] = WorkingG;
                    if (DoBlue)
                        Src[KIndex + 0] = WorkingB;
                }
            }
        }
    }

    return Success;
}