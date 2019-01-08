#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include <float.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int ArithemticMean = 0;
const int ArithmeticBrightest = 1;
const int ArithmeticDarkest = 2;
const int ArithmeticMedian = 3;
const int ArithmeticSum = 4;
const int ClosestLuminance = 5;
const int FarthestLuminance = 6;
const int GreatestAlpha = 7;
const int LeastAlpha = 8;
const int MeanAlpha = 9;
const int MedianAlpha = 10;
const int ClosestAlpha = 11;
const int FarthestAlpha = 12;
const int ArithmeticAnd = 13;
const int ArithmeticOr = 14;
const int ArithmeticXor = 15;

/// <summary>
/// Convert a block from bytes to doubles, one double per pixel channel.
/// </summary>
/// <param name="Source">The source byte block to convert.</param>
/// <param name="SourceWidth">The width of the source block.</param>
/// <param name="SourceHeight">The height of the source block.</param>
/// <param name="SourceStride">The stride of the source block.</param>
/// <param name="Destination">Pointer to the double buffer - must be same size and dimensions as <paramref name="Source"/>.</param>
/// <returns>Value indicating result of operation.</returns>
int ConvertBlockToDouble(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void* Destination)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;

    BYTE PixelSize = 4;
    BYTE* SourceBuffer = (BYTE*)Source;
    double* DestBuffer = (double*)Destination;

    for (int Row = 0; Row < SourceHeight; Row++)
    {
        int RowOffset = Row * SourceStride;
        for (int Column = 0; Column < SourceWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            DestBuffer[Index + 3] = ((double)SourceBuffer[Index + 3] / 255.0);
            DestBuffer[Index + 2] = ((double)SourceBuffer[Index + 2] / 255.0);
            DestBuffer[Index + 1] = ((double)SourceBuffer[Index + 1] / 255.0);
            DestBuffer[Index + 0] = ((double)SourceBuffer[Index + 0] / 255.0);
        }
    }

    return Success;
}

/// <summary>
/// Accumulate blocks of doubles.
/// </summary>
/// <param name="Source">The source byte block to convert.</param>
/// <param name="SourceWidth">The width of the source block.</param>
/// <param name="SourceHeight">The height of the source block.</param>
/// <param name="SourceStride">The stride of the source block.</param>
/// <param name="Accumulator">Stores accumulated values - must be same size and dimensions as <paramref name="Source"/>.</param>
/// <returns>Value indicating result of operation.</returns>
int AccumulateDoubleBlock(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Accumulator)
{
    if (Source == NULL)
        return NullPointer;
    if (Accumulator == NULL)
        return NullPointer;

    BYTE PixelSize = 4;
    BYTE* SourceBuffer = (BYTE*)Source;
    double* Acc = (double*)Accumulator;

    for (int Row = 0; Row < SourceHeight; Row++)
    {
        int RowOffset = Row * SourceStride;
        for (int Column = 0; Column < SourceWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            Acc[Index + 3] += SourceBuffer[Index + 3];
            Acc[Index + 2] += SourceBuffer[Index + 2];
            Acc[Index + 1] += SourceBuffer[Index + 1];
            Acc[Index + 0] += SourceBuffer[Index + 0];
        }
    }

    return Success;
}

const __int32 BlockAdd = 0;
const __int32 BlockSubtract = 1;
const __int32 BlockMultiply = 2;
const __int32 BlockDivide = 3;
const __int32 BlockModulo = 4;
const __int32 BlockAnd = 5;
const __int32 BlockOr = 6;
const __int32 BlockXor = 7;
const __int32 BlockBrightest = 8;
const __int32 BlockDarkest = 9;
const __int32 BlockBiggestRed = 10;
const __int32 BlockSmallestRed = 11;
const __int32 BlockBiggestGreen = 12;
const __int32 BlockSmallestGreen = 13;
const __int32 BlockBiggestBlue = 14;
const __int32 BlockSmallestBlue = 15;
const __int32 BlockBiggestCyan = 16;
const __int32 BlockSmallestCyan = 17;
const __int32 BlockBiggestMagenta = 18;
const __int32 BlockSmallestMagenta = 19;
const __int32 BlockBiggestYellow = 20;
const __int32 BlockSmallestYellow = 21;
const __int32 BlockShr = 22;
const __int32 BlockShl = 23;
const __int32 BlockRol = 24;
const __int32 BlockRor = 25;
const __int32 BlockMaxRG = 26;
const __int32 BlockMaxRB = 27;
const __int32 BlockMaxGB = 28;
const __int32 BlockMinRG = 29;
const __int32 BlockMinRB = 30;
const __int32 BlockMinGB = 31;
const __int32 BlockBiggest = 32;
const __int32 BlockSmallest = 33;

/// <summary>
/// Apply an operand to each double in <paramref name="Source"/>.
/// </summary>
/// <param name="Source">The source byte block to convert.</param>
/// <param name="SourceWidth">The width of the source block.</param>
/// <param name="SourceHeight">The height of the source block.</param>
/// <param name="SourceStride">The stride of the source block.</param>
/// <param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
/// <param name="Operand">The value applied via the <paramref name="Operator"/>.</param>
/// <param name="IncludeAlpha">Determines if alpha is operated upon.</param>
/// <returns>Value indicating result of operation.</returns>
int DoubleBlockOperation(void* Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 Operator, double Operand, bool IncludeAlpha)
{
    if (Source == NULL)
        return NullPointer;
    if (Operand == 0.0)
    {
        if ((Operator == BlockDivide) || (Operator == BlockModulo))
        {
            return InvalidOperation;
        }
    }

    BYTE PixelSize = 4;
    double* SourceBuffer = (double*)Source;

    for (int Row = 0; Row < SourceHeight; Row++)
    {
        int RowOffset = Row * SourceStride;
        for (int Column = 0; Column < SourceWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            switch (Operator)
            {
            case BlockAdd:
                SourceBuffer[Index + 0] += Operand;
                SourceBuffer[Index + 1] += Operand;
                SourceBuffer[Index + 2] += Operand;
                if (IncludeAlpha)
                    SourceBuffer[Index + 3] += Operand;
                break;

            case BlockSubtract:
                SourceBuffer[Index + 0] -= Operand;
                SourceBuffer[Index + 1] -= Operand;
                SourceBuffer[Index + 2] -= Operand;
                if (IncludeAlpha)
                    SourceBuffer[Index + 3] -= Operand;
                break;

            case BlockMultiply:
                SourceBuffer[Index + 0] *= Operand;
                SourceBuffer[Index + 1] *= Operand;
                SourceBuffer[Index + 2] *= Operand;
                if (IncludeAlpha)
                    SourceBuffer[Index + 3] *= Operand;
                break;

            case BlockDivide:
                SourceBuffer[Index + 0] /= Operand;
                SourceBuffer[Index + 1] /= Operand;
                SourceBuffer[Index + 2] /= Operand;
                if (IncludeAlpha)
                    SourceBuffer[Index + 3] /= Operand;
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

/// <summary>
/// Apply an operand to each selected channel byte in <paramref name="Source"/>.
/// </summary>
/// <param name="Source">The source byte block to convert.</param>
/// <param name="SourceWidth">The width of the source block.</param>
/// <param name="SourceHeight">The height of the source block.</param>
/// <param name="SourceStride">The stride of the source block.</param>
/// <param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
/// <param name="OperandValue">The value applied via the <paramref name="Operator"/>.</param>
/// <param name="DoAlpha">Determines if alpha is operated upon.</param>
/// <param name="DoRed">Determines if red is operated upon.</param>
/// <param name="DoGreen">Determines if green is operated upon.</param>
/// <param name="DoBlue">Determines if blue is operated upon.</param>
/// <returns>Value indicating result of operation.</returns>
int ByteBlockOperationByChannel(void* Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Operator, __int32 OperandValue, BOOL DoAlpha, BOOL DoRed, BOOL DoGreen, BOOL DoBlue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (!DoAlpha && !DoRed && !DoGreen && !DoBlue)
        return NoActionTaken;
    BYTE Operand = (BYTE)OperandValue;
    if (Operand == 0.0)
    {
        if ((Operator == BlockDivide) || (Operator == BlockModulo))
        {
            return InvalidOperation;
        }
    }

    BYTE PixelSize = 4;
    BYTE* Src = (BYTE*)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            Dest[Index + 3] = Src[Index + 3];
            Dest[Index + 2] = Src[Index + 2];
            Dest[Index + 1] = Src[Index + 1];
            Dest[Index + 0] = Src[Index + 0];

            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];

            switch (Operator)
            {
            case BlockAdd:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B + Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G + Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R + Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A + Operand);
                break;

            case BlockSubtract:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B - Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G - Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R - Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A - Operand);
                break;

            case BlockMultiply:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B * Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G * Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R * Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A * Operand);
                break;

            case BlockDivide:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)((double)B / (double)Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)((double)G / (double)Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)((double)R / (double)Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)((double)A / (double)Operand);
                break;

            case BlockModulo:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B % Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G % Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R % Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A % Operand);
                break;

            case BlockAnd:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B & Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G & Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R & Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A & Operand);
                break;

            case BlockOr:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B | Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G | Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R | Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A | Operand);
                break;

            case BlockXor:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B ^ Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G ^ Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R ^ Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A ^ Operand);
                break;

            case BlockShl:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B << Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G << Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R << Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A << Operand);
                break;

            case BlockShr:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(B >> Operand);
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(G >> Operand);
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(R >> Operand);
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(A >> Operand);
                break;

            case BlockRol:
                break;

            case BlockRor:
                break;

            case BlockMaxRG:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(max(R,G));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(max(R, G));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(max(R, G));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(max(R, G));
                break;

            case BlockMaxRB:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(max(R, B));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(max(R, B));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(max(R, B));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(max(R, B));
                break;

            case BlockMaxGB:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(max(G, B));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(max(G, B));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(max(G, B));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(max(G, B));
                break;

            case BlockMinRG:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(min(R, G));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(min(R, G));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(min(R, G));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(min(R, G));
                break;

            case BlockMinRB:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(min(R, B));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(min(R, B));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(min(R, B));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(min(R, B));
                break;

            case BlockMinGB:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(min(G, B));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(min(G, B));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(min(G, B));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(min(G, B));
                break;

            case BlockBiggest:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(max(R,max(G, B)));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(max(R, max(G, B)));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(max(R, max(G, B)));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(max(R, max(G, B)));
                break;

            case BlockSmallest:
                if (DoBlue)
                    Dest[Index + 0] = (BYTE)(min(R, min(G, B)));
                if (DoGreen)
                    Dest[Index + 1] = (BYTE)(min(R, min(G, B)));
                if (DoRed)
                    Dest[Index + 2] = (BYTE)(min(R, min(G, B)));
                if (DoAlpha)
                    Dest[Index + 3] = (BYTE)(min(R, min(G, B)));
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

/// <summary>
/// Apply an operand to each byte in <paramref name="Source"/>.
/// </summary>
/// <param name="Source">The source byte block to convert.</param>
/// <param name="SourceWidth">The width of the source block.</param>
/// <param name="SourceHeight">The height of the source block.</param>
/// <param name="SourceStride">The stride of the source block.</param>
/// <param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
/// <param name="Operand">The value applied via the <paramref name="Operator"/>.</param>
/// <param name="IncludeAlpha">Determines if alpha is operated upon.</param>
/// <returns>Value indicating result of operation.</returns>
int ByteBlockOperation(void* Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Operator, BYTE Operand, bool IncludeAlpha)
{
    return ByteBlockOperationByChannel(Source, Width, Height, Stride, Destination, Operator, Operand, IncludeAlpha, TRUE, TRUE, TRUE);
    /*
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (Operand == 0.0)
    {
        if ((Operator == BlockDivide) || (Operator == BlockModulo))
        {
            return InvalidOperation;
        }
    }

    BYTE PixelSize = 4;
    BYTE* Src = (BYTE*)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            Dest[Index + 3] = Src[Index + 3];
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];

            switch (Operator)
            {
            case BlockAdd:
                Dest[Index + 0] = (BYTE)(B + Operand);
                Dest[Index + 1] = (BYTE)(G + Operand);
                Dest[Index + 2] = (BYTE)(R + Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A + Operand);
                break;

            case BlockSubtract:
                Dest[Index + 0] = (BYTE)(B - Operand);
                Dest[Index + 1] = (BYTE)(G - Operand);
                Dest[Index + 2] = (BYTE)(R - Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A - Operand);
                break;

            case BlockMultiply:
                Dest[Index + 0] = (BYTE)(B * Operand);
                Dest[Index + 1] = (BYTE)(G * Operand);
                Dest[Index + 2] = (BYTE)(R * Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A * Operand);
                break;

            case BlockDivide:
                Dest[Index + 0] = (BYTE)((double)B / (double)Operand);
                Dest[Index + 1] = (BYTE)((double)G / (double)Operand);
                Dest[Index + 2] = (BYTE)((double)R / (double)Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)((double)A / (double)Operand);
                break;

            case BlockModulo:
                Dest[Index + 0] = (BYTE)(B % Operand);
                Dest[Index + 1] = (BYTE)(G % Operand);
                Dest[Index + 2] = (BYTE)(R % Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A % Operand);
                break;

            case BlockAnd:
                Dest[Index + 0] = (BYTE)(B & Operand);
                Dest[Index + 1] = (BYTE)(G & Operand);
                Dest[Index + 2] = (BYTE)(R & Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A & Operand);
                break;

            case BlockOr:
                Dest[Index + 0] = (BYTE)(B | Operand);
                Dest[Index + 1] = (BYTE)(G | Operand);
                Dest[Index + 2] = (BYTE)(R | Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A | Operand);
                break;

            case BlockXor:
                Dest[Index + 0] = (BYTE)(B ^ Operand);
                Dest[Index + 1] = (BYTE)(G ^ Operand);
                Dest[Index + 2] = (BYTE)(R ^ Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A ^ Operand);
                break;

            case BlockShl:
                Dest[Index + 0] = (BYTE)(B << Operand);
                Dest[Index + 1] = (BYTE)(G << Operand);
                Dest[Index + 2] = (BYTE)(R << Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A << Operand);
                break;

            case BlockShr:
                Dest[Index + 0] = (BYTE)(B >> Operand);
                Dest[Index + 1] = (BYTE)(G >> Operand);
                Dest[Index + 2] = (BYTE)(R >> Operand);
                if (IncludeAlpha)
                    Dest[Index + 3] = (BYTE)(A >> Operand);
                break;

            case BlockRol:
                break;

            case BlockRor:
                break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
    */
}

/// <summary>
/// Apply two buffers to each other and store the result in a third buffer. All buffers must be the same size and dimensions.
/// </summary>
/// <param name="Destination">The destination of the operation.</param>
/// <param name="DestinationWidth">The width of the destination block.</param>
/// <param name="DestinationHeight">The height of the destination block.</param>
/// <param name="DestinationStride">The stride of the destination block.</param>
/// <param name="BufferA">First operand buffer.</param>
/// <param name="BufferB">Second operand buffer.</param>
/// <param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
/// <param name="IncludeAlpha">Determines if alpha is operated upon.</param>
/// <returns>Value indicating result of operation.</returns>
int ByteBlocksOperation(void* Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
    void* BufferA, void* BufferB, __int32 Operator, BOOL IncludeAlpha)
{
    if (Destination == NULL)
        return NullPointer;
    if (BufferA == NULL)
        return NullPointer;
    if (BufferB == NULL)
        return NullPointer;

    BYTE PixelSize = 4;
    BYTE* DestBuffer = (BYTE*)Destination;
    BYTE* Buffer1 = (BYTE*)BufferA;
    BYTE* Buffer2 = (BYTE*)BufferB;

    for (int Row = 0; Row < DestinationHeight; Row++)
    {
        int RowOffset = Row * DestinationStride;
        for (int Column = 0; Column < DestinationWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            switch (Operator)
            {
            case BlockAdd:
                DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] + Buffer2[Index + 0]);
                DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] + Buffer2[Index + 1]);
                DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] + Buffer2[Index + 2]);
                if (IncludeAlpha)
                    DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] + Buffer2[Index + 3]);
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockSubtract:
                DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] - Buffer2[Index + 0]);
                DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] - Buffer2[Index + 1]);
                DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] - Buffer2[Index + 2]);
                if (IncludeAlpha)
                    DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] - Buffer2[Index + 3]);
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockMultiply:
                DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] * Buffer2[Index + 0]);
                DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] * Buffer2[Index + 1]);
                DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] * Buffer2[Index + 2]);
                if (IncludeAlpha)
                    DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] * Buffer2[Index + 3]);
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockDivide:
                if (Buffer2[Index + 0 > 0])
                    DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] / Buffer2[Index + 0]);
                else
                    DestBuffer[Index + 0] = 0xff;
                if (Buffer2[Index + 1] > 0)
                    DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] + Buffer2[Index + 1]);
                else
                    DestBuffer[Index + 1] = 0xff;
                if (Buffer2[Index + 2] > 0)
                    DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] + Buffer2[Index + 2]);
                else
                    DestBuffer[Index + 2] = 0xff;
                if (IncludeAlpha)
                {
                    if (Buffer2[Index + 3] > 0)
                        DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] + Buffer2[Index + 3]);
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockModulo:
                if (Buffer2[Index + 0 > 0])
                    DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] % Buffer2[Index + 0]);
                else
                    DestBuffer[Index + 0] = 0;
                if (Buffer2[Index + 1] > 0)
                    DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] % Buffer2[Index + 1]);
                else
                    DestBuffer[Index + 1] = 0;
                if (Buffer2[Index + 2] > 0)
                    DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] % Buffer2[Index + 2]);
                else
                    DestBuffer[Index + 2] = 0;
                if (IncludeAlpha)
                {
                    if (Buffer2[Index + 3] > 0)
                        DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] % Buffer2[Index + 3]);
                    else
                        DestBuffer[Index + 3] = 0;
                }
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockAnd:
                DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] & Buffer2[Index + 0]);
                DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] & Buffer2[Index + 1]);
                DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] & Buffer2[Index + 2]);
                if (IncludeAlpha)
                    DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] & Buffer2[Index + 3]);
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockOr:
                DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] | Buffer2[Index + 0]);
                DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] | Buffer2[Index + 1]);
                DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] | Buffer2[Index + 2]);
                if (IncludeAlpha)
                    DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] | Buffer2[Index + 3]);
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockXor:
                DestBuffer[Index + 0] = (BYTE)(Buffer1[Index + 0] ^ Buffer2[Index + 0]);
                DestBuffer[Index + 1] = (BYTE)(Buffer1[Index + 1] ^ Buffer2[Index + 1]);
                DestBuffer[Index + 2] = (BYTE)(Buffer1[Index + 2] ^ Buffer2[Index + 2]);
                if (IncludeAlpha)
                    DestBuffer[Index + 3] = (BYTE)(Buffer1[Index + 3] ^ Buffer2[Index + 3]);
                else
                    DestBuffer[Index + 3] = 0xff;
                break;

            case BlockBrightest:
            {
                double Lum1 = ColorLuminance(Buffer1[Index + 2], Buffer1[Index + 1], Buffer1[Index + 0]);
                double Lum2 = ColorLuminance(Buffer2[Index + 2], Buffer2[Index + 1], Buffer2[Index + 0]);
                if (Lum1 >= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockDarkest:
            {
                double Lum1 = ColorLuminance(Buffer1[Index + 2], Buffer1[Index + 1], Buffer1[Index + 0]);
                double Lum2 = ColorLuminance(Buffer2[Index + 2], Buffer2[Index + 1], Buffer2[Index + 0]);
                if (Lum1 <= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockBiggestRed:
                if (Buffer1[Index + 2] >= Buffer2[Index + 2])
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                break;

            case BlockSmallestRed:
                if (Buffer1[Index + 2] <= Buffer2[Index + 2])
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                break;

            case BlockBiggestGreen:
                if (Buffer1[Index + 1] >= Buffer2[Index + 1])
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                break;

            case BlockSmallestGreen:
                if (Buffer1[Index + 1] <= Buffer2[Index + 1])
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                break;

            case BlockBiggestBlue:
                if (Buffer1[Index + 0] <= Buffer2[Index + 0])
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                break;

            case BlockSmallestBlue:
                if (Buffer1[Index + 0] <= Buffer2[Index + 0])
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                break;

            case BlockBiggestCyan:
            {
                double Lum1 = ColorLuminance(0xff, Buffer1[Index + 1], Buffer1[Index + 0]);
                double Lum2 = ColorLuminance(0xff, Buffer2[Index + 1], Buffer2[Index + 0]);
                if (Lum1 >= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockSmallestCyan:
            {
                double Lum1 = ColorLuminance(0xff, Buffer1[Index + 1], Buffer1[Index + 0]);
                double Lum2 = ColorLuminance(0xff, Buffer2[Index + 1], Buffer2[Index + 0]);
                if (Lum1 <= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockBiggestMagenta:
            {
                double Lum1 = ColorLuminance(Buffer1[Index + 2], 0xff, Buffer1[Index + 0]);
                double Lum2 = ColorLuminance(Buffer2[Index + 2], 0xff, Buffer2[Index + 0]);
                if (Lum1 >= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockSmallestMagenta:
            {
                double Lum1 = ColorLuminance(Buffer1[Index + 2], 0xff, Buffer1[Index + 0]);
                double Lum2 = ColorLuminance(Buffer2[Index + 2], 0xff, Buffer2[Index + 0]);
                if (Lum1 <= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockBiggestYellow:
            {
                double Lum1 = ColorLuminance(Buffer1[Index + 2], Buffer1[Index + 1], 0xff);
                double Lum2 = ColorLuminance(Buffer2[Index + 2], Buffer2[Index + 1], 0xff);
                if (Lum1 >= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            case BlockSmallestYellow:
            {
                double Lum1 = ColorLuminance(Buffer1[Index + 2], Buffer1[Index + 1], 0xff);
                double Lum2 = ColorLuminance(Buffer2[Index + 2], Buffer2[Index + 1], 0xff);
                if (Lum1 <= Lum2)
                {
                    DestBuffer[Index + 0] = Buffer1[Index + 0];
                    DestBuffer[Index + 1] = Buffer1[Index + 1];
                    DestBuffer[Index + 2] = Buffer1[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer1[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
                else
                {
                    DestBuffer[Index + 0] = Buffer2[Index + 0];
                    DestBuffer[Index + 1] = Buffer2[Index + 1];
                    DestBuffer[Index + 2] = Buffer2[Index + 2];
                    if (IncludeAlpha)
                        DestBuffer[Index + 3] = Buffer2[Index + 3];
                    else
                        DestBuffer[Index + 3] = 0xff;
                }
            }
            break;

            default:
                return InvalidOperation;
            }
        }
    }

    return Success;
}

int ChannelShiftBits(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL ShiftRight,
    int ShiftAmount, BOOL IncludeAlpha)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (ShiftAmount == 0)
        return NoActionTaken;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];
            if (IncludeAlpha)
            {
                if (ShiftRight)
                    A = A >> ShiftAmount;
                else
                    A = A << ShiftAmount;
            }
            if (ShiftRight)
            {
                R = R >> ShiftAmount;
                G = G >> ShiftAmount;
                B = B >> ShiftAmount;
            }
            else
            {
                R = R << ShiftAmount;
                G = G << ShiftAmount;
                B = B << ShiftAmount;
            }
            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return Success;
}

int MakeMasqk(int bitstorotate, BOOL RotateRight)
{
    int mask = 0;
    int c;

    if (bitstorotate == 0)
        return 0;

    c = 0x80000000;
    mask = (c >> bitstorotate);
    if (RotateRight)
    {
        mask = (c >> (32 - bitstorotate));
        mask = ~mask;
    }
    else
        mask = (c >> bitstorotate);

    return mask;
}

BYTE MakeMask(BYTE Value, BYTE Location)
{
    BYTE v = (BYTE)pow((double)Value, (double)2.0);
    v = (v << 8) - Location;
    return v;
}

BYTE RotateRight(BYTE Value, int Count)
{
    if (Count == 0)
        return Value;
    return 0;
}

int ChannelRollBits(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL RollRight,
    int RollAmount, BOOL IncludeAlpha)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (RollAmount == 0)
        return NoActionTaken;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            BYTE A = Src[Index + 3];
            BYTE R = Src[Index + 2];
            BYTE G = Src[Index + 1];
            BYTE B = Src[Index + 0];


            Dest[Index + 3] = A;
            Dest[Index + 2] = R;
            Dest[Index + 1] = G;
            Dest[Index + 0] = B;
        }
    }

    return Success;
}

/// <summary>
/// Return the the mean pixel based on luminance.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The mean pixel.</returns>
UINT32 MeanPixel(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;
    UINT32 Alpha = 0;
    UINT32 Red = 0;
    UINT32 Green = 0;
    UINT32 Blue = 0;
    for (int i = 0; i < PixelCount; i++)
    {
        Alpha += (PackedPixels[i] & 0xff000000) >> 24;
        Red += (PackedPixels[i] & 0x00ff0000) >> 16;
        Green += (PackedPixels[i] & 0x0000ff00) >> 8;
        Blue += (PackedPixels[i] & 0x000000ff) >> 0;
    }
    Alpha = Alpha / PixelCount;
    Red = Red / PixelCount;
    Green = Green / PixelCount;
    Blue = Blue / PixelCount;
    return Alpha << 24 | Red << 16 | Green << 8 | Blue;
}

struct LumPixel
{
    UINT32 PixelData;
    double Luminance;
};

/// <summary>
/// Return the the median pixel based on luminance.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The median pixel.</returns>
UINT32 MedianPixel(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;
    LumPixel* Pixels = new LumPixel[PixelCount];
    for (int i = 0; i < PixelCount; i++)
    {
        Pixels[i].PixelData = PackedPixels[i];
        Pixels[i].Luminance = ColorLuminance3(Pixels[i].PixelData, FALSE);
    }
    //Use a stupid bubble sort to sort the pixels by luminance. Don't need anything more sophisticated.
    BOOL Swapped = FALSE;
    do
    {
        for (int i = 1; i < PixelCount - 1; i++)
        {
            if (Pixels[i - 1].Luminance>Pixels[i].Luminance)
            {
                LumPixel temp;
                temp.Luminance = Pixels[i - 1].Luminance;
                temp.PixelData = Pixels[i - 1].PixelData;
                Pixels[i - 1].Luminance = Pixels[i].Luminance;
                Pixels[i - 1].PixelData = Pixels[i].PixelData;
                Pixels[i].Luminance = temp.Luminance;
                Pixels[i].PixelData = temp.PixelData;
                Swapped = TRUE;
            }
        }
    } while (!Swapped);

    UINT32 FinalPixel = 0x0;
    if (PixelCount % 2 == 1)
        FinalPixel = Pixels[(PixelCount / 2) + 1].PixelData;
    else
    {
        UINT32 P0 = Pixels[(PixelCount / 2)].PixelData;
        UINT32 P1 = Pixels[(PixelCount / 2) + 1].PixelData;
        FinalPixel = 0xff000000;
        FinalPixel |= (((P0 & 0x00ff0000 >> 16) + (P1 & 0x00ff0000 >> 16) / 2) << 16);
        FinalPixel |= (((P0 & 0x0000ff00 >> 8) + (P1 & 0x0000ff00 >> 8) / 2) << 8);
        FinalPixel |= (((P0 & 0x000000ff >> 0) + (P1 & 0x000000ff >> 0) / 2) << 0);
    }

    delete[] Pixels;

    return FinalPixel;
}

/// <summary>
/// Return the the pixel with the brightest luminance.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The brightest pixel.</returns>
UINT32 BrightestPixel(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    UINT32 Brightest = 0x0;
    double BrightestLuminance = 0.0;
    for (int i = 0; i < PixelCount; i++)
    {
        double Lum = ColorLuminance3(PackedPixels[i], FALSE);
        if (Lum > BrightestLuminance)
        {
            BrightestLuminance = Lum;
            Brightest = PackedPixels[i];
        }
    }
    return Brightest;
}

/// <summary>
/// Return the the pixel with the darkest luminance.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The darkest pixel.</returns>
UINT32 DarkestPixel(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    UINT32 Darkest = 0xffffffff;
    double DarkestLuminance = DBL_MAX;
    for (int i = 0; i < PixelCount; i++)
    {
        double Lum = ColorLuminance3(PackedPixels[i], FALSE);
        if (Lum < DarkestLuminance)
        {
            DarkestLuminance = Lum;
            Darkest = PackedPixels[i];
        }
    }
    return Darkest;
}

/// <summary>
/// Return the the sum of all of the passed pixels. Each channel clamped to 0xff.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The sum of the passed pixels.</returns>
UINT32 PixelSum(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;
    UINT32 Alpha = 0;
    UINT32 Red = 0;
    UINT32 Green = 0;
    UINT32 Blue = 0;
    for (int i = 0; i < PixelCount; i++)
    {
        Alpha += (PackedPixels[i] & 0xff000000) >> 24;
        Red += (PackedPixels[i] & 0x00ff0000) >> 16;
        Green += (PackedPixels[i] & 0x0000ff00) >> 8;
        Blue += (PackedPixels[i] & 0x000000ff) >> 0;
    }
    if (Alpha > 255)
        Alpha = 255;
    if (Red > 255)
        Red = 255;
    if (Green > 255)
        Green = 255;
    if (Blue > 255)
        Blue = 255;
    return Alpha << 24 | Red << 16 | Green << 8 | Blue;
}

/// <summary>
/// Return the sum of each channel in a buffer consisting of four doubles per pixel.
/// </summary>
/// <param name="DoublePixels">Points to the buffer with four doubles per pixel.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="DoublePixels"/>.</param>
/// <param name="AlphaSum">On return, the sum of all alpha channel values.</param>
/// <param name="RedSum">On return, the sum of all red channel values.</param>
/// <param name="GreenSum">On return, the sum of all green channel values.</param>
/// <param name="BlueSum">On return, the sum of all blue channel values.</param>
/// <returns>Value indicating operational success.</returns>
int PixelSumDouble(double *DoublePixels, int PixelCount, double *AlphaSum, double *RedSum, double *GreenSum, double *BlueSum)
{
    if (DoublePixels == NULL)
        return NullPointer;
    if (PixelCount < 1)
        return InvalidOperation;

    AlphaSum = 0;
    RedSum = 0;
    GreenSum = 0;
    BlueSum = 0;
    int PixelSize = 4;

    int ChannelIndex = 0;
    for (int i = 0; i < PixelCount * PixelSize; i += PixelSize)
    {
        *AlphaSum += DoublePixels[i + 3];
        *RedSum += DoublePixels[i + 2];
        *GreenSum += DoublePixels[i + 1];
        *BlueSum += DoublePixels[i + 0];
    }

    return Success;
}

/// <summary>
/// Return the pixel whose luminance is the closest to <paramref name="LuminanceTarget"/>.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <param name="LuminanceTarget">Determines which pixel is returned.</param>
/// <returns>The pixel that has a luminance closest to <paramref name="LuminanceTarget"/>.</returns>
UINT32 ClosestPixelLuminance(UINT32 *PackedPixels, int PixelCount, double LuminanceTarget)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    UINT32 Closest = 0x0;
    double ClosestLuminance = 0.0;
    for (int i = 0; i < PixelCount; i++)
    {
        double Lum = ColorLuminance3(PackedPixels[i], FALSE);
        if (fabs(Lum - LuminanceTarget) < ClosestLuminance)
        {
            ClosestLuminance = fabs(Lum - LuminanceTarget);
            Closest = PackedPixels[i];
        }
    }
    return Closest;
}

/// <summary>
/// Return the pixel whose luminance is the farthest away from <paramref name="LuminanceTarget"/>.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <param name="LuminanceTarget">Determines which pixel is returned.</param>
/// <returns>The pixel that is the farthest away from <paramref name="LuminanceTarget"/>.</returns>
UINT32 LeastClosestPixelLuminance(UINT32 *PackedPixels, int PixelCount, double LuminanceTarget)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    UINT32 BigDelta = 0x0;
    double BiggestDeltaLuminance = 0.0;
    for (int i = 0; i < PixelCount; i++)
    {
        double Lum = ColorLuminance3(PackedPixels[i], FALSE);
        if (fabs(Lum - LuminanceTarget) > BiggestDeltaLuminance)
        {
            BiggestDeltaLuminance = fabs(Lum - LuminanceTarget);
            BigDelta = PackedPixels[i];
        }
    }
    return BigDelta;
}

/// <summary>
/// Return the pixel whose alpha value is smallest of the passed set of pixels.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The pixel that has the smallest alpha value.</returns>
UINT32 SmallestAlphaPixel(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    BYTE Smallest = 0xff;
    UINT32 SmallestAlphaPix = 0x0;
    for (int i = 0; i < PixelCount; i++)
    {
        BYTE AlphaValue = (PackedPixels[i] & 0xff000000) >> 24;
        if (AlphaValue < Smallest)
        {
            Smallest = AlphaValue;
            SmallestAlphaPix = PackedPixels[i];
        }
    }
    return SmallestAlphaPix;
}

/// <summary>
/// Return the pixel whose alpha value is greatest of the passed set of pixels.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <returns>The pixel that has the greatest alpha value.</returns>
UINT32 GreatestAlphaPixel(UINT32 *PackedPixels, int PixelCount)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    BYTE Biggest = 0x0;
    UINT32 BiggestAlphaPix = 0x0;
    for (int i = 0; i < PixelCount; i++)
    {
        BYTE AlphaValue = (PackedPixels[i] & 0xff000000) >> 24;
        if (AlphaValue > Biggest)
        {
            Biggest = AlphaValue;
            BiggestAlphaPix = PackedPixels[i];
        }
    }
    return BiggestAlphaPix;
}

/// <summary>
/// Return the pixel whose alpha value is the closest to a specific value.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <param name="FarthestFrom">
/// Determines which pixel is returned. The pixel whose alpha value is the closest to this returned.
/// </param>
/// <returns>The pixel that is closest to <paramref name="FarthestFrom"/>.</returns>
UINT32 ClosestAlphaPixel(UINT32 *PackedPixels, int PixelCount, BYTE ClosestTo)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    BYTE Closest = 0xff;
    UINT32 ClosestPixel = 0x0;
    for (int i = 0; i < PixelCount; i++)
    {
        BYTE AlphaValue = (PackedPixels[i] & 0xff000000) >> 24;
        if (abs(AlphaValue - ClosestTo) < Closest)
        {
            Closest = abs(AlphaValue - ClosestTo);
            ClosestPixel = PackedPixels[i];
        }
    }
    return ClosestPixel;
}

/// <summary>
/// Return the pixel whose alpha value is the farther away from the specific value.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <param name="FarthestFrom">
/// Determines which pixel is returned. The pixel whose alpha value is the farthest away from this returned.
/// </param>
/// <returns>The pixel that is farthest away from <paramref name="FarthestFrom"/>.</returns>
UINT32 FarthestAlphaPixel(UINT32 *PackedPixels, int PixelCount, BYTE FarthestFrom)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;

    BYTE Farthest = 0x0;
    UINT32 FarthestPixel = 0x0;
    for (int i = 0; i < PixelCount; i++)
    {
        BYTE AlphaValue = (PackedPixels[i] & 0xff000000) >> 24;
        if (abs(AlphaValue - FarthestFrom) > Farthest)
        {
            Farthest = abs(AlphaValue - FarthestFrom);
            FarthestPixel = PackedPixels[i];
        }
    }
    return FarthestPixel;
}

/// <summary>
/// Perform an arithmetical logical operation on all channels of the passed pixels.
/// </summary>
/// <param name="PackedPixels">Array of pixels to process.</param>
/// <param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
/// <param name="Operation">The arithmetic logical operation to perform on all of the pixels.</param>
/// <returns>Pixel based on the arithmetic logical operation.</returns>
UINT32 PixelLogicalOperation(UINT32 *PackedPixels, int PixelCount, int Operation)
{
    if (PackedPixels == NULL)
        return 0;
    if (PixelCount < 1)
        return 0;
    if ((Operation != ArithmeticAnd) && (Operation != ArithmeticOr) && (Operation != ArithmeticXor))
        return 0;

    BYTE Red = 0;
    BYTE Green = 0;
    BYTE Blue = 0;

    for (int i = 0; i < PixelCount; i++)
    {
        BYTE lRed = (PackedPixels[i] & 0x00ff0000) >> 16;
        BYTE lGreen = (PackedPixels[i] & 0x0000ff00) >> 0;
        BYTE lBlue = (PackedPixels[i] & 0x000000ff) >> 0;
        switch (Operation)
        {
        case ArithmeticAnd:
            Red &= lRed;
            Green &= lGreen;
            Blue &= lBlue;
            break;

        case ArithmeticOr:
            Red |= lRed;
            Green |= lGreen;
            Blue |= lBlue;
            break;

        case ArithmeticXor:
            Red ^= lRed;
            Green ^= lGreen;
            Blue ^= lBlue;
            break;
        }
    }

    return 0xff000000 | (Red << 16) | (Green << 8) | (Blue << 0);
}

struct MassArithmeticExtra
{
    double Luminance;
    BYTE Channel;
};

/// <summary>
/// Perform an arithmetic type operation on the pixels in the image.
/// </summary>
/// <remarks>
/// All images must have the same dimensions and stride.
/// </remarks>
/// <param name="Destination">Will contain the result of the arithmetic processing.</param>
/// <param name="Width">Width of the image buffer.</param>
/// <param name="Height">Height of the image buffer.</param>
/// <param name="Stride">Stride of the image buffer.</param>
/// <param name="ImageSet">Pointer to an array of images to process.</param>
/// <param name="ImageCount">Number of images in <paramref name="ImageSet"/>.</param>
/// <param name="Operation">The operation to perform.</param>
/// <param name="ExtraData">Context-sensitive data - not all operations require this data.</param>
/// <returns>Value indication operational success.</returns>
int MassImageArithmetic(void *Destination, __int32 Width, __int32 Height, __int32 Stride, void *ImageSet, int ImageCount,
    int Operation, void *ExtraData)
{
    if (Destination == NULL)
        return NullPointer;
    if (ImageSet == NULL)
        return NullPointer;
    if (ImageCount < 1)
        return InvalidOperation;
    MassArithmeticExtra *Extra = NULL;
    if ((Operation == ClosestLuminance) || (Operation == FarthestLuminance))
    {
        if (ExtraData == NULL)
            return NullPointer;
        Extra = (MassArithmeticExtra *)ExtraData;
    }

    BYTE *Dest = (BYTE *)Destination;
    GenericImageNode *Images = (GenericImageNode *)ImageSet;
    int PixelSize = 4;

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            UINT32 *PixelList = new UINT32[ImageCount];
            for (int i = 0; i < ImageCount; i++)
            {
                BYTE *ImageBits = (BYTE *)Images[i].TheBits;
                PixelList[i] = (ImageBits[Index + 3] << 24) |
                    (ImageBits[Index + 2] << 16) |
                    (ImageBits[Index + 1] << 8) |
                    (ImageBits[Index + 0] << 0);
            }
            UINT32 Final = 0xff000000;
            switch (Operation)
            {
            case ArithemticMean:
                Final = MeanPixel(PixelList, ImageCount);
                break;

            case ArithmeticMedian:
                Final = MedianPixel(PixelList, ImageCount);
                break;

            case ArithmeticBrightest:
                Final = BrightestPixel(PixelList, ImageCount);
                break;

            case ArithmeticDarkest:
                Final = DarkestPixel(PixelList, ImageCount);
                break;

            case ArithmeticSum:
                Final = PixelSum(PixelList, ImageCount);
                break;

            case ClosestLuminance:
                Final = ClosestPixelLuminance(PixelList, ImageCount, Extra->Luminance);
                break;

            case GreatestAlpha:
                Final = GreatestAlphaPixel(PixelList, ImageCount);
                break;

            case LeastAlpha:
                Final = SmallestAlphaPixel(PixelList, ImageCount);
                break;

            case FarthestAlpha:
                Final = FarthestAlphaPixel(PixelList, ImageCount, Extra->Channel);
                break;

            case ClosestAlpha:
                Final = ClosestAlphaPixel(PixelList, ImageCount, Extra->Channel);
                break;

            case ArithmeticAnd:
            case ArithmeticOr:
            case ArithmeticXor:
                Final = PixelLogicalOperation(PixelList, ImageCount, Operation);
                break;

            default:
                return InvalidOperation;
            }
            delete[] PixelList;

            Dest[Index + 3] = (Final & 0xff000000) >> 24;
            Dest[Index + 2] = (Final & 0x00ff0000) >> 16;
            Dest[Index + 1] = (Final & 0x0000ff00) >> 8;
            Dest[Index + 0] = (Final & 0x000000ff) >> 0;
        }
    }

    return Success;
}

