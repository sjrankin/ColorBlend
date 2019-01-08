#include "ColorBlender.h"
#include "Utilities.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

BYTE RandomizeByte(BYTE Source, BYTE RandomizeMask, UINT32 RandomSeed)
{
    srand(RandomSeed);
    BYTE RValue = (BYTE)rand();
    RValue = RValue & RandomizeMask;
    BYTE Result = (Source & ~RandomizeMask) | RValue;
    return Result;
}

/// <summary>
/// Randomize bits in the specicified channels specified by the supplied bit mask for the specified region in the source.
/// </summary>
/// <remarks>
/// If no channels are selected or if the random mask is 0x0, no action will be taken and the destinaion buffer will not be filled.
/// </remarks>
/// <param name="Buffer">Source image to whose bits will be randomized.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="Left">The left coordinate of the region to convert.</param>
/// <param name="Top">The top coordinate of the region to convert.</param>
/// <param name="Right">The right coordinate of the region to convert.</param>
/// <param name="Bottom">The bottom coordinate of the region to convert.</param>
/// <param name="RandomizeMask">The mask used to determine which bits are randomized. If this value is 0x0, no action is taken.</param>
/// <param name="RandomSeed">Random number generator seed.</param>
/// <param name="IncludeAlpha">Randomize the alpha channel.</param>
/// <param name="IncludeRed">Randomize the red channel.</param>
/// <param name="IncludeGreen">Randomize the green channel.</param>
/// <param name="IncludeBlue">Randomize the blue channel.</param>
/// <returns>Value indicating operational results.</returns>
int RandomizeImageBitsRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    BYTE RandomizeMask, UINT32 RandomSeed, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (!IncludeAlpha && !IncludeRed && !IncludeGreen && !IncludeBlue)
        return NoActionTaken;
    if (RandomizeMask == 0x0)
        return NoActionTaken;
    if (Left < 0)
        return InvalidOperation;
    if (Right >= Width)
        return InvalidOperation;
    if (Top < 0)
        return InvalidOperation;
    if (Bottom >= Height)
        return InvalidOperation;

    int PixelSize = 4;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Src = (BYTE *)Source;

    for (int Row = Top; Row <= Bottom; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = Left; Column <= Right; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            if (IncludeAlpha)
                Dest[Index + 3] = RandomizeByte(Src[Index + 3], RandomizeMask, RandomSeed);
            else
                Dest[Index + 3] = Src[Index + 3];
            if (IncludeRed)
                Dest[Index + 2] = RandomizeByte(Src[Index + 2], RandomizeMask, RandomSeed);
            else
                Dest[Index + 2] = Src[Index + 2];
            if (IncludeGreen)
                Dest[Index + 1] = RandomizeByte(Src[Index + 1], RandomizeMask, RandomSeed);
            else
                Dest[Index + 1] = Src[Index + 1];
            if (IncludeBlue)
                Dest[Index + 0] = RandomizeByte(Src[Index + 0], RandomizeMask, RandomSeed);
            else
                Dest[Index + 0] = Src[Index + 0];
        }
    }

    return Success;
}

/// <summary>
/// Randomize bits in the specicified channels specified by the supplied bit mask.
/// </summary>
/// <remarks>
/// If no channels are selected or if the random mask is 0x0, no action will be taken and the destinaion buffer will not be filled.
/// </remarks>
/// <param name="Buffer">Source image to whose bits will be randomized.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="RandomizeMask">The mask used to determine which bits are randomized. If this value is 0x0, no action is taken.</param>
/// <param name="RandomSeed">Random number generator seed.</param>
/// <param name="IncludeAlpha">Randomize the alpha channel.</param>
/// <param name="IncludeRed">Randomize the red channel.</param>
/// <param name="IncludeGreen">Randomize the green channel.</param>
/// <param name="IncludeBlue">Randomize the blue channel.</param>
/// <returns>Value indicating operational results.</returns>
int RandomizeImageBits1(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE RandomizeMask, UINT32 RandomSeed, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue)
{
    return RandomizeImageBitsRegion(Source, Width, Height, Stride, Destination, 0, 0, Width - 1, Height - 1,
        RandomizeMask, RandomSeed, IncludeAlpha, IncludeRed, IncludeGreen, IncludeBlue);
}

/// <summary>
/// Randomize bits in all channels specified by the supplied bit mask.
/// </summary>
/// <param name="Buffer">Source image to whose bits will be randomized.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="RandomizeMask">The mask used to determine which bits are randomized. If this value is 0x0, no action is taken.</param>
/// <param name="RandomSeed">Random number generator seed.</param>
/// <returns>Value indicating operational results.</returns>
int RandomizeImageBits2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE RandomizeMask, UINT32 RandomSeed)
{
    return RandomizeImageBitsRegion(Source, Width, Height, Stride, Destination, 0, 0, Width - 1, Height - 1,
        RandomizeMask, RandomSeed, TRUE, TRUE, TRUE, TRUE);
}

/// <summary>
/// Determines the number of bytes needed per character given the available bits to use.
/// </summary>
/// <param name="ChannelMask">Available bits per byte.</param>
/// <returns>Number of bytes needed to store one character. Returns -1 on error.</returns>
int BytePerCharacter(BYTE ChannelMask)
{
    int Bits = BitCountTable2(ChannelMask);
    if (Bits <= 0)
        return -1;
    double BAdditional = 8 % Bits;
    return (8 / Bits) + BAdditional;
}

/// <summary>
/// Determines the number of bytes needed to store the string given bits available in <paramref name="ChannelMask"/>.
/// </summary>
/// <param name="ChannelMask">The number of bits to use.</param>
/// <param name="TextLength">Number of characters in the string.</param>
/// <returns>Number of required bytes needed to store a string of characters. Returns -1 on error.</returns>
int BytesRequiredToFit(BYTE ChannelMask, int TextLength)
{
    int BytePerChar = BytePerCharacter(ChannelMask);
    if (BytePerChar <= 0)
        return -1;
    return BytePerChar * TextLength;
}

/// <summary>
/// Determines if the specified string can fit into available space.
/// </summary>
/// <param name="Left">Left side of the region.</param>
/// <param name="Top">Top of the region.</param>
/// <param name="Right">Right side of the region.</param>
/// <param name="Bottom">Bottom of the region.</param>
/// <param name="IncludeAlpha">Determines if the alpha channel is available for use.</param>
/// <param name="IncludeRed">Determines if the red channel is available for use.</param>
/// <param name="IncludeGreen">Determines if the green channel is available for use.</param>
/// <param name="IncludeBlue">Determines if the blue channel is available for use.</param>
/// <param name="ChannelMask">The number of bits to use in each available channel.</param>
/// <param name="TextLength">Number of characters in the string.</param>
/// <returns>TRUE if the text can fit, FALSE if not.</returns>
BOOL CanFit(__int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    BYTE ChannelMask, int TextLength)
{
    int FinalLeft = Left + RelativeXOffset;
    int FinalTop = Top + RelativeYOffset;
    int FinalWidth = Right - FinalLeft;
    int FinalHeight = Bottom - FinalTop;
    int BytesAvailable = (FinalWidth * FinalHeight) *
        (IncludeAlpha ? 1 : 0 + IncludeRed ? 1 : 0 + IncludeGreen ? 1 : 0 + IncludeBlue ? 1 : 0);
    if (BytesRequiredToFit(ChannelMask, TextLength) > BytesAvailable)
        return FALSE;
    return TRUE;
}

void SwapBytes(BYTE &First, BYTE &Second)
{
    if (First == Second)
        return;
    First ^= Second;
    Second ^= First;
    First ^= Second;
}

//http://graphics.stanford.edu/~seander/bithacks.html#MaskedMerge
BYTE SetBits(BYTE Source, BYTE Mask, BYTE Data)
{
    BYTE Final = Source ^ ((Source ^ Data) & Mask);
    return Final;
}

/// <summary>
/// Return the number of zeros on the right size of <paramref name="Mask"/>.
/// </summary>
/// <param name="Mask">The value to count zeros.</param>
/// <returns>The number of zeros on the right size of <paramref name="Mask"/>.</returns>
int MaskOffset(BYTE Mask)
{
    int Count;
    if (Mask)
    {
        Mask = (Mask ^ (Mask - 1)) >> 1;
        for (Count = 0; Mask; Count++)
        {
            Mask >>= 1;
        }
    }
    else
    {
        Count = 0;
    }
    return Count;
}

/// <summary>
/// Return the bits in <paramref name="Source"/> as defined by <paramref name="Mask"/>. Returned bits are shifted right the
/// appropriate number (as defined by <paramref name="Mask"/> bits.
/// </summary>
/// <param name="Source">The value whose bits are desired.</param>
/// <param name="Mask">Defines the bits to return.</param>
/// <returns>The bits in <paramref name="Source"/> as specified by <paramref name="Mask"/>.</returns>
BYTE GetBits(BYTE Source, BYTE Mask)
{
    BYTE Final = Source & Mask;
    Final >>= MaskOffset(Mask);
    return Final;
}

/// <summary>
/// Add a string of 8-bit characters to the supplied buffer by setting bits in the specified bit mask.
/// </summary>
/// <param name="Buffer">Source image to whose bits will be randomized.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="Left">The left coordinate of the region to use.</param>
/// <param name="Top">The top coordinate of the region to use.</param>
/// <param name="Right">The right coordinate of the region to use.</param>
/// <param name="Bottom">The bottom coordinate of the region to use.</param>
/// <param name="RelativeXOffset">Horizontal offset.</param>
/// <param name="RelativeYOffset">Vertical offset.</param>
/// <param name="ChannelMask">Determines where character bits will be written.</param>
/// <param name="IncludeAlpha">Write bits in the alpha channel.</param>
/// <param name="IncludeRed">Write bits in the red channel.</param>
/// <param name="IncludeGreen">Write bits in the green channel.</param>
/// <param name="IncludeBlue">Write bits in the blue channel.</param>
/// <param name="Text">The text to add to the string.</param>
/// <param name="TextLength">Length of the text to add to the string.</param>
int AddStringToRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (Text == NULL)
        return NullPointer;
    if (TextLength < 1)
        return NoActionTaken;
    if (ChannelMask == 0x0)
        return NoActionTaken;
    if (!IncludeAlpha && !IncludeRed && !IncludeGreen & !IncludeBlue)
        return NoActionTaken;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    int PixelSize = 4;

    return Success;
}

int AddString1(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength)
{
    return AddStringToRegion(Source, Width, Height, Stride, Destination,
        0, 0, Width - 1, Height - 1, RelativeXOffset, RelativeYOffset,
        ChannelMask, IncludeAlpha, IncludeRed, IncludeGreen, IncludeBlue,
        Text, TextLength);
}

int AddString2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength)
{
    return AddStringToRegion(Source, Width, Height, Stride, Destination,
        0, 0, Width - 1, Height - 1, 0, 0,
        ChannelMask, IncludeAlpha, IncludeRed, IncludeGreen, IncludeBlue,
        Text, TextLength);
}

int AddString3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, char *Text, int TextLength)
{
    return AddStringToRegion(Source, Width, Height, Stride, Destination,
        0, 0, Width - 1, Height - 1, 0, 0,
        ChannelMask, TRUE, TRUE, TRUE, TRUE,
        Text, TextLength);
}

void SplitData(void *Data, int DataLength, BYTE ChannelMask, BYTE *BrokenData, int &BrokenDataCount)
{
    BrokenDataCount = BytesRequiredToFit(ChannelMask, DataLength);
    BrokenData = (BYTE *)new BYTE[BrokenDataCount];
    int BytesPerByte = BytePerCharacter(ChannelMask);
    for (int i = 0; i < BrokenDataCount; i += BytesPerByte)
    {
        for (int k = 0; k < BytesPerByte; k++)
        {
            int Index = i + k;

        }
    }
}

/// <summary>
/// Merge data from a buffer with the source and place the result in the destination buffer.
/// </summary>
/// <param name="Buffer">Source image to whose bits will be randomized.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="Left">The left coordinate of the region to use.</param>
/// <param name="Top">The top coordinate of the region to use.</param>
/// <param name="Right">The right coordinate of the region to use.</param>
/// <param name="Bottom">The bottom coordinate of the region to use.</param>
/// <param name="RelativeXOffset">Horizontal offset.</param>
/// <param name="RelativeYOffset">Vertical offset.</param>
/// <param name="ChannelMask">Determines where character bits will be written.</param>
/// <param name="IncludeAlpha">Write bits in the alpha channel.</param>
/// <param name="IncludeRed">Write bits in the red channel.</param>
/// <param name="IncludeGreen">Write bits in the green channel.</param>
/// <param name="IncludeBlue">Write bits in the blue channel.</param>
/// <param name="DataSource">Pointer to the buffer of data to add.</param>
/// <param name="DataSourceLength">Number of bytes in <paramref name="DataSource"/>.</param>
int AddDataToRegion(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 Left, __int32 Top, __int32 Right, __int32 Bottom,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    void *DataSource, int DataSourceLength)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (DataSource == NULL)
        return NullPointer;
    if (DataSourceLength < 1)
        return NoActionTaken;
    if (ChannelMask == 0x0)
        return NoActionTaken;
    int DataStride = IncludeAlpha ? 1 : 0 + IncludeRed ? 1 : 0 + IncludeGreen ? 1 : 0 + IncludeBlue ? 1 : 0;
    if (DataStride == 0)
        return NoActionTaken;
    if (!CanFit(Left, Top, Right, Bottom, RelativeXOffset, RelativeYOffset, IncludeAlpha, IncludeRed, IncludeGreen, IncludeBlue,
        ChannelMask, DataSourceLength))
        return InvalidOperation;

    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    //BYTE *Data = (BYTE *)Data;
    int DataIndex = 0;
    int PixelSize = 4;

    Left = Left + RelativeXOffset;
    Top = Top + RelativeYOffset;

    for (int Row = Top; Row < Bottom; Row++)
    {
        int RowOffset = (Row * Stride);
        for (int Column = Left; Column < Right; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;

        }
    }

    return Success;
}

int AddData1(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    __int32 RelativeXOffset, __int32 RelativeYOffset,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength)
{
    return AddDataToRegion(Source, Width, Height, Stride, Destination,
        0, 0, Width - 1, Height - 1, RelativeXOffset, RelativeYOffset,
        ChannelMask, IncludeAlpha, IncludeRed, IncludeGreen, IncludeBlue,
        Text, TextLength);
}

int AddData2(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue,
    char *Text, int TextLength)
{
    return AddDataToRegion(Source, Width, Height, Stride, Destination,
        0, 0, Width - 1, Height - 1, 0, 0,
        ChannelMask, IncludeAlpha, IncludeRed, IncludeGreen, IncludeBlue,
        Text, TextLength);
}

int AddData3(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    BYTE ChannelMask, char *Text, int TextLength)
{
    return AddDataToRegion(Source, Width, Height, Stride, Destination,
        0, 0, Width - 1, Height - 1, 0, 0,
        ChannelMask, TRUE, TRUE, TRUE, TRUE,
        Text, TextLength);
}

int MakeHeader(BYTE SplitSize, UINT32 DataCount, BYTE *Header)
{
    int HeaderSize = 5 * SplitSize == 4 ? 2 : 4;
    Header = new BYTE[HeaderSize];
    BYTE s = 0;
    if (SplitSize == 4)
    {
        Header[0] = (SplitSize & 0xf0) >> 4;
        Header[1] = (SplitSize & 0x0f);
        s = (DataCount & 0xff000000) >> 24;
        Header[2] = (s & 0xf0) >> 4;
        Header[3] = (s & 0x0f);
        s = (DataCount & 0x00ff0000) >> 16;
        Header[4] = (s & 0xf0) >> 4;
        Header[5] = (s & 0x0f);
        s = (DataCount & 0x0000ff00) >> 8;
        Header[6] = (s & 0xf0) >> 4;
        Header[7] = (s & 0x0f);
        s = (DataCount & 0x000000ff) >> 0;
        Header[8] = (s & 0xf0) >> 4;
        Header[9] = (s & 0x0f);
    }
    else
    {
        Header[0] = (SplitSize & 0xc0) >> 6;
        Header[1] = (SplitSize & 0x30) >> 4;
        Header[2] = (SplitSize & 0x0c) >> 2;
        Header[3] = (SplitSize & 0x03);
        s = (DataCount & 0xff000000) >> 24;
        Header[4] = (s & 0xc0) >> 6;
        Header[5] = (s & 0x30) >> 4;
        Header[6] = (s & 0x0c) >> 2;
        Header[7] = (s & 0x03);
        s = (DataCount & 0x00ff0000) >> 16;
        Header[8] = (s & 0xc0) >> 6;
        Header[9] = (s & 0x30) >> 4;
        Header[10] = (s & 0x0c) >> 2;
        Header[11] = (s & 0x03);
        s = (DataCount & 0x0000ff00) >> 8;
        Header[12] = (s & 0xc0) >> 6;
        Header[13] = (s & 0x30) >> 4;
        Header[14] = (s & 0x0c) >> 2;
        Header[15] = (s & 0x03);
        s = (DataCount & 0x000000ff) >> 0;
        Header[16] = (s & 0xc0) >> 6;
        Header[17] = (s & 0x30) >> 4;
        Header[18] = (s & 0x0c) >> 2;
        Header[19] = (s & 0x03);
    }
    return HeaderSize;
}

/// <summary>
/// Merge data (in the form of a BYTE array) with <paramref name="Source"/> into <paramref name="Destination"/>.
/// </summary>
/// <remarks>
/// A header prefixes the data that is merged. The format of the header is Mask Size:Byte Count. Mask size if a byte value and
/// Byte Count is a UINT32 value. The bits used for the header depends on <paramref name="ByTwo"/>.
/// </remarks>
/// <param name="Buffer">Source image to whose bits will be randomized.</param>
/// <param name="Width">Width of the source and destination images.</param>
/// <param name="Height">Height of the source and destination images.</param>
/// <param name="Destination">Destination buffer - where the results are written.</param>
/// <param name="DataBuffer">The data to merge with <paramref name="Source"/>.</param>
/// <param name="DataCount">Number of byte entries in <paramref name="DataBuffer"/>.</param>
/// <param name="ByTwo">Determines the number of bits used to merge.</param>
/// <param name="IncludeAlpha">Write bits in the alpha channel.</param>
/// <param name="IncludeRed">Write bits in the red channel.</param>
/// <param name="IncludeGreen">Write bits in the green channel.</param>
/// <param name="IncludeBlue">Write bits in the blue channel.</param>
/// <returns>Value indicating operational results.</returns>
int DataMerge(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
    void *DataBuffer, UINT32 DataCount, BOOL ByTwo,
    BOOL IncludeAlpha, BOOL IncludeRed, BOOL IncludeGreen, BOOL IncludeBlue)
{
    if (Source == NULL)
        return NullPointer;
    if (Destination == NULL)
        return NullPointer;
    if (DataBuffer == NULL)
        return NullPointer;
    if (!IncludeAlpha && !IncludeRed && !IncludeGreen && !IncludeBlue)
        return InvalidOperation;
    if (DataCount < 1)
        return InvalidOperation;

    int PixelSize = 4;
    BYTE *Src = (BYTE *)Source;
    BYTE *Dest = (BYTE *)Destination;
    BYTE *Data = (BYTE *)DataBuffer;

    int DataMultiplier = ByTwo ? 4 : 2;
    BYTE *SplitBits = (BYTE *)new BYTE[DataCount * DataMultiplier];
    unsigned DataIndex = 0;
    int SplitIndex = 0;

    while (TRUE)
    {
        if (ByTwo)
        {
            SplitBits[SplitIndex++] = (Data[DataIndex] & 0xf0) >> 4;
            SplitBits[SplitIndex++] = (Data[DataIndex] & 0x0f);
            if (SplitIndex == (DataCount * DataMultiplier) - 1)
                break;
        }
        else
        {
            SplitBits[SplitIndex++] = (Data[DataIndex] & 0xc0) >> 6;
            SplitBits[SplitIndex++] = (Data[DataIndex] & 0x30) >> 4;
            SplitBits[SplitIndex++] = (Data[DataIndex] & 0x0c) >> 2;
            SplitBits[SplitIndex++] = (Data[DataIndex] & 0x03);
            if (SplitIndex == (DataCount * DataMultiplier) - 1)
                break;
        }
        DataIndex++;
    }
    DataIndex = 0;

    BYTE *Header = NULL;
    int HeaderSize = MakeHeader(ByTwo ? 2 : 4, DataCount, Header);
    int HeaderIndex = 0;
    BOOL MergeBytes = TRUE;
    BYTE MergeMask = ByTwo ? 0x03 : 0x07;
    if (HeaderSize + (DataCount * ByTwo ? 4 : 2) >= (Height * Stride))
    {
        delete[] Header;
        delete[] SplitBits;
        return InvalidOperation;
    }

    for (int Row = 0; Row < Height; Row++)
    {
        int RowOffset = Row * Stride;
        for (int Column = 0; Column < Width; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;

            if (DataIndex >= (DataCount * DataMultiplier) - 1)
                MergeBytes = FALSE;
            BYTE MergeValue = 0;
            if (MergeBytes)
            {
                if (HeaderIndex < HeaderSize)
                    MergeValue = Header[HeaderIndex++];
                else
                    MergeValue = SplitBits[DataIndex++];
            }

            if (IncludeAlpha)
            {
                if (MergeBytes)
                    Dest[Index + 3] = SetBits(Src[Index + 3], MergeMask, MergeValue);
                else
                    Dest[Index + 3] = Src[Index + 3];
            }
            else
                Dest[Index + 3] = Src[Index + 3];
            if (IncludeRed)
            {
                if (MergeBytes)
                    Dest[Index + 2] = SetBits(Src[Index + 2], MergeMask, MergeValue);
                else
                    Dest[Index + 2] = Src[Index + 2];
            }
            else
                Dest[Index + 2] = Src[Index + 2];
            if (IncludeGreen)
            {
                if (MergeBytes)
                    Dest[Index + 1] = SetBits(Src[Index + 1], MergeMask, MergeValue);
                else
                    Dest[Index + 1] = Src[Index + 1];
            }
            else
                Dest[Index + 1] = Src[Index + 1];
            if (IncludeBlue)
            {
                if (MergeBytes)
                    Dest[Index + 0] = SetBits(Src[Index + 0], MergeMask, MergeValue);
                else
                    Dest[Index + 0] = Src[Index + 0];
            }
            else
                Dest[Index + 0] = Src[Index + 0];
        }
    }

    delete[] Header;
    delete[] SplitBits;
    return Success;
}