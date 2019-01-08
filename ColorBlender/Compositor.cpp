#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)


/// <summary>
/// Merge a set of color blobs (in <paramref name="Objectset"/>) to <paramref name="Target"/>. This is a composite action - the
/// order of the Objects in the list is relevant - first items are composited first. The background in <paramref name="Target"/>
/// will be merged with the Objects and is assumed to be drawn before calling this function.
/// </summary>
/// <param name="Target">Where the drawing will be done.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="Objectset">The list of color blobs to merge to <paramref name="Target"/>.</param>
/// <param name="PlaneCount">Number of Objects in <paramref name="Objectset"/>.</param>
/// <returns>Operational success indicator.</returns>
int Compositor(void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride, void *CommonObjectSet, int ObjectCount)
{
    if (Target == NULL)
        return NullPointer;
    if (CommonObjectSet == NULL)
        return NullPointer;
    if (ObjectCount < 1)
        return Error;

    BYTE* Buffer = (BYTE*)Target;
    CommonObject *Objects = (CommonObject *)CommonObjectSet;
    __int32 PixelSize = 4;

    for (int ObjectIndex = 0; ObjectIndex < ObjectCount; ObjectIndex++)
    {
        //Get a pointer to the plane's bits.
        BYTE *PlaneBuffer = (BYTE *)Objects[ObjectIndex].ObjectBuffer;

        for (int Row = 0; Row < Objects[ObjectIndex].ObjectHeight; Row++)
        {
            if (Row + Objects[ObjectIndex].Top < 0)
                continue;
            if (Row + Objects[ObjectIndex].Top > TargetHeight - 1)
                continue;

            UINT32 BufferRowOffset = (Row + Objects[ObjectIndex].Top) * TargetStride;
            UINT32 PlaneOffset = (Row * Objects[ObjectIndex].ObjectStride);
            for (int Column = 0; Column < Objects[ObjectIndex].ObjectWidth; Column++)
            {
                if (Column + Objects[ObjectIndex].Left > TargetWidth - 1)
                    continue;
                if (Column + Objects[ObjectIndex].Left < 0)
                    continue;

                UINT32 BufferIndex = BufferRowOffset + ((Column + Objects[ObjectIndex].Left) * PixelSize);
                UINT32 PlaneIndex = PlaneOffset + (Column * PixelSize);

                BYTE FGBlue = PlaneBuffer[PlaneIndex + 0];
                BYTE FGGreen = PlaneBuffer[PlaneIndex + 1];
                BYTE FGRed = PlaneBuffer[PlaneIndex + 2];
                BYTE FGAlpha = PlaneBuffer[PlaneIndex + 3];
                //If there's nothing to draw skip calculations and buffer assignment and move to the next pixel.
                if (FGAlpha == 0x0)
                    continue;
                BYTE BGBlue = Buffer[BufferIndex + 0];
                BYTE BGGreen = Buffer[BufferIndex + 1];
                BYTE BGRed = Buffer[BufferIndex + 2];
                //BYTE BGAlpha = Buffer[BufferIndex + 3];
                double FAlpha = (double)FGAlpha / 255.0;
                double InvertedAlpha = 1.0 - FAlpha;
                BYTE FinalBlue = (BYTE)(FAlpha * FGBlue) + (BYTE)(InvertedAlpha * BGBlue);
                BYTE FinalGreen = (BYTE)(FAlpha * FGGreen) + (BYTE)(InvertedAlpha * BGGreen);
                BYTE FinalRed = (BYTE)(FAlpha * FGRed) + (BYTE)(InvertedAlpha * BGRed);
                Buffer[BufferIndex + 0] = FinalBlue;
                Buffer[BufferIndex + 1] = FinalGreen;
                Buffer[BufferIndex + 2] = FinalRed;
                Buffer[BufferIndex + 3] = 0xff;
            }
        }
    }

    return Success;
}