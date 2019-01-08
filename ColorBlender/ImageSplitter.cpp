#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

int ImageSplit(void *Source, __int32 Width, __int32 Height, void *Results, int SubCount)
{
    if (Source == NULL)
        return NullPointer;
    if (Results == NULL)
        return NullPointer;
    if (SubCount < 1)
        return NoActionTaken;
    ImageDefintionStruct *Images = (ImageDefintionStruct *)Results;
    if (Images == NULL)
        return NullPointer;

    UINT32 *Src = (UINT32 *)Source;

    for (int i = 0; i < SubCount; i++)
    {
        ImageDefintionStruct SubImage = Images[i];
        int Result = CopyBufferRegionPixel(Source, Width, Height, SubImage.Buffer, SubImage.X, SubImage.Y,
            SubImage.X + SubImage.Width - 1, SubImage.Y + SubImage.Height - 1);
        if (Result != Success)
            return Result;
    }

    return Success;
}

/// <summary>
/// Combine Images in <paramref name="Sources"/> into the destination buffer. Images are blitted without regard to transparency,
/// e.g., no alpha compositing is done.
/// </summary>
/// <param name="Destination">Combination destination.</param>
/// <param name="Width">Width of the destination. No sub image width may be larger than this value.</param>
/// <param name="Width">Height of the destination. No sub image height may be larger than this value.</param>
/// <param name="Sources">List of sub images to combine.</param>
/// <param name="Subcount">Number of images in <paramref name="Sources"/>.</param>
/// <param name="BGColor">Packed color used to clear the destination before combining is done.</param>
/// <returns>Value indicating operational results.</returns>
int ImageCombine(void *Destination, __int32 Width, __int32 Height, void *Sources, int SubCount, UINT32 BGColor)
{
    if (Destination == NULL)
        return NullPointer;
    if (SubCount < 1)
        return NoActionTaken;
    ImageDefintionStruct *SourceList = (ImageDefintionStruct *)Sources;
    if (SourceList == NULL)
        return NullPointer;

    ClearBufferDWord(Destination, Width, Height, BGColor);
    UINT32 *Dest = (UINT32 *)Destination;
    for (unsigned i = 0; i < (unsigned)SubCount; i++)
    {
        ImageDefintionStruct SubImage = SourceList[i];
        if (SubImage.Width + SubImage.X > (unsigned)Width)
            return InvalidOperation;
        if (SubImage.Height + SubImage.Y > (unsigned)Height)
            return InvalidOperation;
        int Result = CopyBufferRegionPixel(SubImage.Buffer, SubImage.Width, SubImage.Height, Dest,
            SubImage.X, SubImage.Y, SubImage.X + SubImage.Width - 1, SubImage.Y + SubImage.Height - 1);
        if (Result != Success)
            return Result;
    }

    return Success;
}