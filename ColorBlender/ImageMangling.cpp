#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include <float.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Describes a row to be moved.
/// </summary>
struct RowDescription
{
    /// <summary>
    /// Row index of the source row.
    /// </summary>
    int SourceRowStart;
    /// <summary>
    /// Row index of the destination row.
    /// </summary>
    int DestinationRowStart;
    /// <summary>
    /// Height of the row to move.
    /// </summary>
    unsigned int RowHeight;
};

/// <summary>
/// Copy rows from <paramref name="Source"/> to <paramref name="Destination"/> as described by <paramref name="RowDescriptions"/>.
/// </summary>
/// <remarks>
/// Intended to be used to shuffle rows randomly.
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the image.</param>
/// <param name="Height">Height of the image.</param>
/// <param name="Stride">Stride of the image.</param>
/// <param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
/// <param name="RowDescriptions">Pointer to an array of descriptions that controls how rows are shuffle.</param>
/// <param name="RowDescriptionCount">Contains the number of entries in <paramref name="RowDescriptions"/>.</param>
/// <returns>Value indicating operational success.</returns>
int ShuffleRows(void *Source, __int32 Width, __int32 Height, __int32 Stride,
    void *Destination, void *RowDescriptions, int RowDescriptionCount)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (RowDescriptions == NULL)
        return NullPointer;
    if (RowDescriptionCount < 1)
        return InvalidOperation;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    RowDescription *Rows = (RowDescription *)RowDescriptions;

    for (int RowIndex = 0; RowIndex < RowDescriptionCount; RowIndex++)
    {
        int SourceRow = Rows[RowIndex].SourceRowStart;
        int DestRow = Rows[RowIndex].DestinationRowStart;
        for (unsigned Row = 0; Row < Rows[RowIndex].RowHeight; Row++)
        {
            int SourceIndex = SourceRow * Stride;
            SourceRow++;
            int DestIndex = DestRow * Stride;
            DestRow++;
            memmove_s(Dest + DestIndex, Stride, Src + SourceIndex, Stride);
        }
    }

    return Success;
}