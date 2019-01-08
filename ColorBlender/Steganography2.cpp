#include "ColorBlender.h"
#include "Utilities.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int ImageData = 1;
const int TextData = 2;
const int BinaryData = 3;
const int ConstantData = 4;

BYTE Masks[] = { 0x0, 0x1, 0x3, 0x7, 0xf, 0x1f, 0x3f, 0x7f };
BYTE ReverseMasks[] = { 0xff, 0xfe, 0xfc, 0xf8, 0xf0, 0xe0, 0xc0, 0x80 };
BYTE MarkerID[] = { 0xD8, 0x6F, 0xC5, 0x9D ,0x81, 0x9B ,0x4D, 0x58 ,0x9A, 0xEB ,0xAA, 0x4C, 0xB4, 0x05, 0x48, 0x5E };

struct SteganographicHeader
{
    BYTE Marker[16];
    __int32 DataType;
    __int32 DataOffset;
    __int32 DataLength;
    __int32 MaskSize;
    __int32 ImageWidth;
    __int32 ImageHeight;
    __int32 ImageStride;
    BYTE UseRed;
    BYTE UseGreen;
    BYTE UseBlue;
    __int32 HeaderVersion;
};

int ExtractSteganographicHeader(void *Source, __int32 Offset, void *Header)
{
    memcpy_s(Header, sizeof(SteganographicHeader), Source, sizeof(SteganographicHeader));
    return Success;
}

/// <summary>
/// Create a new byte value from <paramref name="Original"/> and the value in <paramref name="Source"/> and return
/// the result.
/// </summary>
/// <param name="Original">The original value of the byte from the passed buffer.</param>
/// <param name="Source">
/// The value to merge with <paramref name="Original"/>. Value is masked apprpropriately to avoid overwriting data.
/// </param>
/// <param name="MaskSize">Determines how much of the original data is used to store <paramref name="Source"/>.</param>
/// <returns>
/// New, merged value. 0 is returned on error.
/// </returns>
inline BYTE MergePixelData(BYTE Original, BYTE Source, BYTE MaskSize)
{
    if (MaskSize > 7 || MaskSize < 0)
        return 0;
    BYTE Final = Original & ReverseMasks[MaskSize];
    Source &= Masks[MaskSize];
    Final |= Source;
    return Final;
}

/// <summary>
/// Given a source byte, extract and return the value determined by the mask.
/// </summary>
/// <param name="Source">The source byte whose data will be extracted.</param>
/// <param name="MaskSize">Determines the location of the data in the source.</param>
/// <returns>The data in the source byte.</returns>
inline BYTE ExtractPixelData(BYTE Source, BYTE MaskSize)
{
    if (MaskSize > 7 || MaskSize < 0)
        return 0;
    BYTE Final = Source & Masks[MaskSize];
    return Final;
}

BYTE* CreateStream(BYTE *Raw, __int32 RawLength, __int32 MaskSize, __int32 *StreamLength)
{
    //    *StreamLength = (__int32)(RawLength * ((8.0 / (double)MaskSize) + 1));
    *StreamLength = RawLength * 4;
    BYTE *TStream = new BYTE[*StreamLength];

    __int32 TIndex = 0;
    for (int i = 0;i < RawLength; i++)
    {
        BYTE B1 = (Raw[i] & 0xc0) >> 6;
        BYTE B2 = (Raw[i] & 0x30) >> 4;
        BYTE B3 = (Raw[i] & 0x0c) >> 2;
        BYTE B4 = (Raw[i] & 0x03);
        TStream[TIndex++] = MergePixelData(Raw[i], B1, 2);
        TStream[TIndex++] = MergePixelData(Raw[i], B2, 2);
        TStream[TIndex++] = MergePixelData(Raw[i], B3, 2);
        TStream[TIndex++] = MergePixelData(Raw[i], B4, 2);
    }

    return TStream;
}

int SteganographyAddStream(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *FinalImage, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    BYTE *StreamSource, __int32 StreamLength, __int32 DataOffset,
    BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset, __int32 DataType)
{
    SteganographicHeader Header;
    for (int i = 0; i < 16; i++)
        Header.Marker[i] = MarkerID[i];
    Header.DataType = DataType;
    Header.DataOffset = DataOffset;
    Header.DataLength = StreamLength;
    Header.MaskSize = MaskSize;
    Header.UseRed = UseRed;
    Header.UseGreen = UseGreen;
    Header.UseBlue = UseBlue;
    Header.ImageHeight = SourceHeight;
    Header.ImageWidth = SourceHeight;
    Header.ImageStride = SourceStride;
    Header.HeaderVersion = 0x100;

    __int32 HeaderStreamLength = 0;
    BYTE *HeaderStream = CreateStream((BYTE *)&Header, sizeof(Header), MaskSize, &HeaderStreamLength);
    __int32 FinalStreamLength = 0;
    BYTE *MainStream = CreateStream(StreamSource, StreamLength, MaskSize, &FinalStreamLength);

    BYTE *Source = (BYTE *)SourceImage;
    BYTE *Final = (BYTE *)FinalImage;
    __int32 HeaderStreamIndex = 0;
    __int32 MainStreamIndex = 0;
    __int32 SourceImageSize = (SourceWidth * SourceHeight) * SourceStride;
    for (int k = 0; k < SourceImageSize; k++)
    {
        BYTE SourceByte = Source[k];
        if (HeaderStreamIndex < HeaderStreamLength)
        {
            MergePixelData(SourceByte, HeaderStream[HeaderStreamIndex], MaskSize);
            HeaderStreamIndex++;
        }
        if (MainStreamIndex < FinalStreamLength)
        {
            MergePixelData(SourceByte, MainStream[MainStreamIndex], MaskSize);
            MainStreamIndex++;
        }
        Final[k] = SourceByte;
    }

    delete[] HeaderStream;
    delete[] MainStream;

    return Success;
}

int SteganographyAddConstant(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *FinalImage, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    BYTE Constant,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset)
{
    BYTE *Source = (BYTE *)SourceImage;
    BYTE *Final = (BYTE *)FinalImage;

    SteganographicHeader Header;
    for (int i = 0; i < 16; i++)
        Header.Marker[i] = MarkerID[i];
    Header.DataType = ConstantData;
    Header.DataOffset = Offset;
    Header.DataLength = 0;
    Header.MaskSize = MaskSize;
    Header.UseRed = UseRed;
    Header.UseGreen = UseGreen;
    Header.UseBlue = UseBlue;
    Header.ImageHeight = 0;
    Header.ImageWidth = 0;
    Header.ImageStride = 0;
    Header.HeaderVersion = 0x100;

    __int32 SourceImageSize = (SourceWidth * SourceHeight) * SourceStride;
    __int32 HeaderStreamLength = 0;
    BYTE *HeaderStream = CreateStream((BYTE *)&Header, sizeof(Header), MaskSize, &HeaderStreamLength);
    __int32 HeaderStreamIndex = 0;

    for (int k = 0; k < SourceImageSize; k++)
    {
        BYTE SourceByte = Source[k];
        if (HeaderStreamIndex < HeaderStreamLength)
        {
            MergePixelData(SourceByte, HeaderStream[HeaderStreamIndex], MaskSize);
            HeaderStreamIndex++;
        }
        else
        {
            MergePixelData(SourceByte, Constant, MaskSize);
        }
        Final[k] = SourceByte;
    }

    return Success;
}

int SteganographyAddString(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    char *Message, __int32 MessageLength,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset)
{
    BYTE *SStream = (BYTE *)Message;
    return SteganographyAddStream(SourceImage, SourceWidth, SourceHeight, SourceStride,
        Final, FinalWidth, FinalHeight, FinalStride,
        SStream, MessageLength,
        Offset, UseRed, UseGreen, UseBlue,
        MaskSize, HeaderOffset, TextData);
}

int SteganographyFastAddString(void *SourceImage, __int32 SourceWidth, __int32 SourceHeader, __int32 SourceStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    char *Message, __int32 MessageLength)
{
    return SteganographyAddString(SourceImage, SourceWidth, SourceHeader, SourceStride,
        Final, FinalWidth, FinalHeight, FinalStride,
        Message, MessageLength, 0, TRUE, TRUE, TRUE, 2, 0);
}

int SteganographyAddImage(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset)
{
    BYTE *StreamSource = (BYTE *)SourceImage;
    __int32 ImageByteLength = SourceWidth * SourceWidth * SourceStride;
    return SteganographyAddStream(Target, TargetWidth, TargetHeight, TargetStride,
        Final, FinalWidth, FinalHeight, FinalStride,
        StreamSource, ImageByteLength,
        Offset, UseRed, UseGreen, UseBlue,
        MaskSize, HeaderOffset, ImageData);
}

int SteganographyFastAddImage(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride)
{
    return SteganographyAddImage(SourceImage, SourceWidth, SourceHeight, SourceStride,
        Target, TargetWidth, TargetHeight, TargetStride,
        Final, FinalWidth, FinalHeight, FinalStride,
        0, TRUE, TRUE, TRUE,
        2, 0);
}

int SteganographyAddBinaryData(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    void *Final, __int32 FinalWidth, __int32 FinalHeight, __int32 FinalStride,
    void *Binary, __int32 BinaryLength,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset)
{
    BYTE *BinaryStream = (BYTE *)Binary;
    return SteganographyAddStream(SourceImage, SourceWidth, SourceHeight, SourceStride,
        Final, FinalWidth, FinalHeight, FinalStride,
        BinaryStream, BinaryLength,
        Offset, UseRed, UseGreen, UseBlue,
        MaskSize, HeaderOffset, BinaryData);
}

const int NotPresent = 0;
const int ImagePresent = 1;
const int TextPresent = 2;
const int BinaryPresent = 3;
const int ConstantPresent = 4;
const int UnknownPresent = 99;
const int HeaderPresent = 100;

int BinaryMatch(BYTE *Source, __int32 Offset)
{
    if (Offset < 0)
        return HeaderNotFound;
    unsigned Start = Offset;
    for (unsigned i = Start; i < Start + sizeof(SteganographicHeader); i++)
        if (Source[i] != MarkerID[i - Offset])
            return NotPresent;
    return HeaderPresent;
}

int SteganographyPresent(void *Source, __int32 HeaderOffset)
{
    return BinaryMatch((BYTE *)Source, HeaderOffset);
}

int SearchForSteganographicHeader(void *Source, __int32 SourceSize, __int32 *HeaderLocation)
{
    for (int i = 0; i < SourceSize - 16; i++)
        if (BinaryMatch((BYTE *)Source, i))
        {
            *HeaderLocation = i;
            return HeaderPresent;
        }
    *HeaderLocation = NULL;
    return NotPresent;
}

int GetSteganographicType(void *Source, __int32 Offset = 0)
{
    if (!SteganographyPresent(Source, Offset))
        return NotPresent;
    struct SteganographicHeader Header;
    ExtractSteganographicHeader(Source, Offset, &Header);
    switch (Header.DataType)
    {
    case ImageData:
        return ImagePresent;

    case TextData:
        return TextPresent;

    case BinaryData:
        return BinaryPresent;

    case ConstantData:
        return ConstantPresent;
    }
    return UnknownPresent;
}

int SteganographyGetConstant(void *SourceImage, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
    __int32 Offset, BOOL UseRed, BOOL UseGreen, BOOL UseBlue,
    __int32 MaskSize, __int32 HeaderOffset, BYTE *Constant)
{
    *Constant = NULL;
    if (!SteganographyPresent(SourceImage, Offset))
        return HeaderNotFound;
    struct SteganographicHeader Header;
    ExtractSteganographicHeader(SourceImage, Offset, &Header);
    if (Header.DataType != ConstantData)
        return HeaderBadDataType;
    int FirstIndex = Offset + sizeof(SteganographicHeader);
    BYTE *Source = (BYTE *)SourceImage;
    BYTE B1 = Source[FirstIndex];
    B1 = ExtractPixelData(B1, MaskSize);
    BYTE B2 = Source[FirstIndex + 1];
    B2 = ExtractPixelData(B2, MaskSize);
    BYTE B3 = Source[FirstIndex + 2];
    B3 = ExtractPixelData(B3, MaskSize);
    BYTE B4 = Source[FirstIndex + 3];
    B4 = ExtractPixelData(B4, MaskSize);
    *Constant = (B1 << 6) || (B2 << 4) || (B3 << 2) || B4;
    return Success;
}
