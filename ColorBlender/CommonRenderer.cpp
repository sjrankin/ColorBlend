#include "ColorBlender.h"

/// <summary>
/// Renders circular blobs of color.
/// </summary>
/// <param name="Parameters">Parameter block with information on how to render the color blob.</param>
/// <param name="RenderTo">Common object where the blob will be rendered. This function allocates space for the blob.</param>
/// <returns>Result code.</returns>
int RenderColorBlobEx (ColorBlobParameters *Parameters, CommonObject *RenderTo)
{
    if (Parameters == NULL)
        return NullPointer;
    if (RenderTo == NULL)
        return NullPointer;

    BYTE CenterBlue = (BYTE) ((Parameters->BlobColor & 0xff000000) >> 24);
    BYTE CenterGreen = (BYTE) ((Parameters->BlobColor & 0x00ff0000) >> 16);
    BYTE CenterRed = (BYTE) ((Parameters->BlobColor & 0x0000ff00) >> 8);
    BYTE CenterAlpha = (BYTE) ((Parameters->BlobColor & 0x000000ff) >> 0);
    BYTE PixelSize = 4;
    RenderTo->Left = Parameters->X1;
    RenderTo->Top = Parameters->Y1;
    RenderTo->Right = RenderTo->Left + Parameters->Width;
    RenderTo->Bottom = RenderTo->Top + Parameters->Height;
    RenderTo->ObjectBuffer = new byte[Parameters->Width * Parameters->Height * PixelSize];
    memchr(RenderTo->ObjectBuffer, 0xff, Parameters->Width * Parameters->Height * PixelSize);
    int Radius = Parameters->Width / 2;
    int AlphaDelta = (abs (Parameters->CenterAlpha - Parameters->EdgeAlpha));
    int CenterX = Parameters->Width / 2;
    int CenterY = Parameters->Height / 2;

    BYTE iR = CenterRed;
    BYTE iG = CenterGreen;
    BYTE iB = CenterBlue;

    for (int Row = 0; Row < Parameters->Height; Row++)
    {
        int RowOffset = Row * (Parameters->Width * PixelSize);
        for (int Column = 0; Column < Parameters->Width; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            if ((Row == 0) || (Row == Parameters->Height - 1))
            {
                RenderTo->ObjectBuffer[Index + 0] = 0x0;
                RenderTo->ObjectBuffer[Index + 1] = 0x0;
                RenderTo->ObjectBuffer[Index + 2] = 0x0;
                RenderTo->ObjectBuffer[Index + 3] = 0xff;
                continue;
            }
            if ((Column == 0) || (Column == Parameters->Width - 1))
            {
                RenderTo->ObjectBuffer[Index + 0] = 0x0;
                RenderTo->ObjectBuffer[Index + 1] = 0x0;
                RenderTo->ObjectBuffer[Index + 2] = 0x0;
                RenderTo->ObjectBuffer[Index + 3] = 0xff;
                continue;
            }

            BYTE iA = 0x0;

            double Dist = Distance (Column, Row, CenterX, CenterY);
            if (Dist > Radius)
            {
                //RenderTo->ObjectBuffer[Index + 0] = 0x0;
                //RenderTo->ObjectBuffer[Index + 1] = 0x0;
                //RenderTo->ObjectBuffer[Index + 2] = 0x0;
                //RenderTo->ObjectBuffer[Index + 3] = 0x0;
                continue;
            }
//                continue;
            if (Dist == 0)
                iA = CenterAlpha;
            else
            {
                double Percent = Dist / Radius;
                Percent = 1.0 - Percent;
                if (Percent < 0.0)
                    Percent = 0.0;
                iA = (BYTE) (Percent * (double) AlphaDelta);
            }

            RenderTo->ObjectBuffer[Index + 0] = iB;
            RenderTo->ObjectBuffer[Index + 1] = iG;
            RenderTo->ObjectBuffer[Index + 2] = iR;
            RenderTo->ObjectBuffer[Index + 3] = iA;
        }
    }

    return Success;
}

int RenderColorBlock (ColorBlockParameters *Parameters, CommonObject *RenderTo)
{
    if (Parameters == NULL)
        return NullPointer;
    if (RenderTo == NULL)
        return NullPointer;

    RenderTo->Left = Parameters->X1;
    RenderTo->Top = Parameters->Y1;
    RenderTo->Right = RenderTo->Left + RenderTo->ObjectWidth;
    RenderTo->Bottom = RenderTo->Top + RenderTo->ObjectHeight;
    BYTE B = (BYTE) ((Parameters->BGColor & 0xff000000) >> 24);
    BYTE G = (BYTE) ((Parameters->BGColor & 0x00ff0000) >> 16);
    BYTE R = (BYTE) ((Parameters->BGColor & 0x0000ff00) >> 8);
    BYTE A = (BYTE) ((Parameters->BGColor & 0x000000ff) >> 0);

    RenderTo->ObjectBuffer = new byte[Parameters->Width * Parameters->Height * 4];

    for (int Row = 0; Row < Parameters->Height; Row++)
    {
        int RowOffset = Row * (Parameters->Width * 4);
        for (int Column = 0; Column < Parameters->Width; Column++)
        {
            int Index = RowOffset + (Column * 4);
            RenderTo->ObjectBuffer[Index + 0] = B;
            RenderTo->ObjectBuffer[Index + 1] = G;
            RenderTo->ObjectBuffer[Index + 2] = R;
            RenderTo->ObjectBuffer[Index + 3] = A;
        }
    }

    return Success;
}

int RenderLinePlot (LinePlotParameters *Parameters, CommonObject *RenderTo)
{
    if (Parameters == NULL)
        return NullPointer;
    if (RenderTo == NULL)
        return NullPointer;

    RenderTo->Left = Parameters->X1;
    RenderTo->Top = Parameters->Y1;
    RenderTo->Right = Parameters->X2;
    RenderTo->Bottom = Parameters->Y2;
    BYTE B = (BYTE) ((Parameters->LineColor & 0xff000000) >> 24);
    BYTE G = (BYTE) ((Parameters->LineColor & 0x00ff0000) >> 16);
    BYTE R = (BYTE) ((Parameters->LineColor & 0x0000ff00) >> 8);
    BYTE A = (BYTE) ((Parameters->LineColor & 0x000000ff) >> 0);
    BYTE PixelSize = 4;

    __int32 DeltaX = Parameters->X2 - Parameters->X1;
    __int32 DeltaY = Parameters->Y2 - Parameters->Y1;
    RenderTo->ObjectBuffer = new byte[DeltaX * DeltaY * 4];
    RenderTo->ObjectHeight = DeltaY;
    RenderTo->ObjectWidth = DeltaX;
    RenderTo->ObjectStride = RenderTo->ObjectWidth * PixelSize;
    if (Parameters->X1 > Parameters->X2)
    {
        __int32 temp = Parameters->X2;
        Parameters->X2 = Parameters->X1;
        Parameters->X1 = temp;
    }

    for (int XPlot = 0; XPlot < DeltaX; XPlot++)
    {
        int YPlot = DeltaY * (XPlot / DeltaX);
        int Index = (YPlot - 1) * RenderTo->ObjectStride;
        RenderTo->ObjectBuffer[Index + 0] = B;
        RenderTo->ObjectBuffer[Index + 1] = G;
        RenderTo->ObjectBuffer[Index + 2] = R;
        RenderTo->ObjectBuffer[Index + 3] = A;
    }

    return Success;
}

/// <summary>
/// Render all of the objects in <paramref name="ObjectList"/> onto <paramref name="Target"/>.
/// </summary>
/// <param name="Target">
/// The buffer where the rendering will take place. It is assumed that the buffer is large enough for all
/// of the objects, e.g. the dimensions are sufficient.
/// </param>
/// <param name="TargetWidth">The width of the target buffer.</param>
/// <param name="TargetHeight">The height of the target buffer.</param>
/// <param name="TargetStride">The stride of the target buffer.</param>
/// <param name="ObjectList">List of objects to render in <paramref name="Target"/>.</param>
/// <param name="ObjectCount">Number of objects in <paramref name="ObjectList"/>.</param>
/// <returns>Success code.</returns>
int CommonObjectRenderer (BYTE *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    CommonObject* ObjectList, __int32 ObjectCount)
{
    if (Target == NULL)
        return NullPointer;
    if (ObjectList == NULL)
        return NullPointer;
    if (ObjectCount < 1)
        return NoActionTaken;

    for (int i = 0; i < ObjectCount; i++)
    {
        ObjectList[i].Right = ObjectList[i].Left + ObjectList[i].ObjectWidth;
        ObjectList[i].Bottom = ObjectList[i].Top + ObjectList[i].ObjectHeight;
    }

    BYTE PixelSize = 4;

    for (int Row = 0; Row < TargetHeight; Row++)
    {
        int RowOffset = Row * TargetStride;
        for (int Column = 0; Column < TargetWidth; Column++)
        {
            int Index = (Column * PixelSize) + RowOffset;
            //BGRA
            double Final[] = { 0.0, 0.0, 0.0, 0.0 };
            int OverlapCount = 0;
            for (int OIndex = 0; OIndex < ObjectCount; OIndex++)
            {
                if (ObjectList[OIndex].ObjectAction == IgnoreObject)
                    continue;
                if (Row < ObjectList[OIndex].Top)
                    continue;
                if (Row > ObjectList[OIndex].Bottom)
                    continue;
                if (Column < ObjectList[OIndex].Left)
                    continue;
                if (Column > ObjectList[OIndex].Right)
                    continue;
                //BGRA
                int a = Row - ObjectList[OIndex].Left;
                int b = Column - ObjectList[OIndex].Top;
                int ObjectIndex = (b * ObjectList[OIndex].ObjectStride) + (a * PixelSize);
                Final[0] += (double) ((double) ObjectList[OIndex].ObjectBuffer[ObjectIndex + 0] / 255.0);
                Final[1] += (double) ((double) ObjectList[OIndex].ObjectBuffer[ObjectIndex + 1] / 255.0);
                Final[2] += (double) ((double) ObjectList[OIndex].ObjectBuffer[ObjectIndex + 2] / 255.0);
                Final[3] += (double) ((double) ObjectList[OIndex].ObjectBuffer[ObjectIndex + 3] / 255.0);
            }
            if (OverlapCount > 0)
            {
                //Merge/blend the pixels.
                Final[0] /= OverlapCount;
                Final[1] /= OverlapCount;
                Final[2] /= OverlapCount;
                Final[3] /= OverlapCount;
                BYTE FinalB = (Final[0] * 255.0);
                BYTE FinalG = (Final[1] * 255.0);
                BYTE FinalR = (Final[2] * 255.0);
                BYTE FinalA = (Final[3] * 255.0);
                Target[Index + 0] = FinalB;
                Target[Index + 1] = FinalG;
                Target[Index + 2] = FinalR;
                Target[Index + 3] = FinalA;
            }
        }
    }

    return Success;
}

int UpdateBufferAlpha (BYTE *Buffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    MassAlphaParameters *Parameters)
{
    if (Buffer == NULL)
        return NullPointer;
    if (Parameters == NULL)
        return NullPointer;

    BYTE PixelSize = 4;
    BYTE A, R, G, B;
    double Luminance = 0;
    double Ratio = 0;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = (Column * PixelSize + RowOffset);
            switch (Parameters->AlphaOperation)
            {
                case UnitaryAlphaApplication:
                    Buffer[Index + 3] = Parameters->UniformAlpha;
                    break;

                case VariableAlphaApplication:
                    B = Buffer[Index + 0];
                    G = Buffer[Index + 1];
                    R = Buffer[Index + 2];
                    A = Buffer[Index + 3];
                    Luminance = PixelLuminance (R, G, B);
                    Ratio = Luminance / 255.0;
                    if (Parameters->InverseAlpha)
                        Ratio = 1.0 - Ratio;
                    if (Parameters->UseExistingAlpha)
                        A = (BYTE) ((double) A * Ratio);
                    else
                        A = (BYTE) (255.0 * Ratio);
                    Buffer[Index + 3] = A;
                    break;

                default:
                    return InvalidOperation;
            }
        }
    }

    return Success;
}

int InvertRenderBuffer (BYTE *Buffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    InversionParameters *Parameters)
{
    if (Buffer == NULL)
        return NullPointer;
    if (Parameters == NULL)
        return NullPointer;

    BYTE PixelSize = 4;
    BYTE FinalLuminance = Parameters->InvertThreshold ? 255 - Parameters->LuminanceThreshold : Parameters->LuminanceThreshold;
    BOOL InvertAlpha = (Parameters->InversionChannels & AlphaChannel) > 0 ? TRUE : FALSE;
    BOOL InvertRed = (Parameters->InversionChannels & RedChannel) > 0 ? TRUE : FALSE;
    BOOL InvertGreen = (Parameters->InversionChannels & GreenChannel) > 0 ? TRUE : FALSE;
    BOOL InvertBlue = (Parameters->InversionChannels & BlueChannel) > 0 ? TRUE : FALSE;

    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            BYTE A = Buffer[Index + 3];
            BYTE R = Buffer[Index + 2];
            BYTE G = Buffer[Index + 1];
            BYTE B = Buffer[Index + 0];
            double Luminance = 0;
            switch (Parameters->Operation)
            {
                case SimpleInvertOperation:
                    R = 255 - R;
                    G = 255 - G;
                    B = 255 - G;
                    if (InvertAlpha)
                        A = 255 - A;
                    break;

                case ChannelInversionOperation:
                    if ((InvertAlpha) && (Parameters->AllowInvertAlpha))
                        A = 255 - A;
                    if (InvertRed)
                        R = 255 - R;
                    if (InvertGreen)
                        G = 255 - G;
                    if (InvertBlue)
                        B = 255 - B;
                    break;

                case VariableInvertOperation:
                    Luminance = PixelLuminance (R, G, B);
                    if (Luminance > FinalLuminance)
                    {
                        R = 255 - R;
                        G = 255 - G;
                        B = 255 - B;
                        if (InvertAlpha)
                            A = 255 - A;
                    }
                    break;

                default:
                    return InvalidOperation;
            }
            Buffer[Index + 0] = B;
            Buffer[Index + 1] = G;
            Buffer[Index + 2] = R;
            Buffer[Index + 3] = A;
        }
    }

    return Success;
}

int ClearRenderBuffer (BYTE *Buffer, __int32 BufferWidth, __int32 BufferHeight, __int32 BufferStride,
    BufferClearParameters *Parameters)
{
    if (Buffer == NULL)
        return NullPointer;
    if (Parameters == NULL)
        return NullPointer;

    BYTE bgB = (BYTE) ((Parameters->BGColor & 0xff000000) >> 24);
    BYTE bgG = (BYTE) ((Parameters->BGColor & 0x00ff0000) >> 16);
    BYTE bgR = (BYTE) ((Parameters->BGColor & 0x0000ff00) >> 8);
    BYTE bgA = (BYTE) ((Parameters->BGColor & 0x000000ff) >> 0);
    BYTE grB = (BYTE) ((Parameters->GridColor & 0xff000000) >> 24);
    BYTE grG = (BYTE) ((Parameters->GridColor & 0x00ff0000) >> 16);
    BYTE grR = (BYTE) ((Parameters->GridColor & 0x0000ff00) >> 8);
    BYTE grA = (BYTE) ((Parameters->GridColor & 0x000000ff) >> 0);

    BYTE PixelSize = 4;
    for (int Row = 0; Row < BufferHeight; Row++)
    {
        int RowOffset = Row * BufferStride;
        for (int Column = 0; Column < BufferWidth; Column++)
        {
            int Index = RowOffset + (Column * PixelSize);
            if (Parameters->DrawGrid)
            {
                if (
                    (Row % Parameters->VerticalFrequency == 0) ||
                    (Column % Parameters->HorizontalFrequency == 0)
                    )
                {
                    Buffer[Index + 0] = bgB;
                    Buffer[Index + 1] = grG;
                    Buffer[Index + 2] = grR;
                    Buffer[Index + 3] = grA;
                    continue;
                }
            }
            else
            {
                Buffer[Index + 0] = bgB;
                Buffer[Index + 1] = bgG;
                Buffer[Index + 2] = bgR;
                Buffer[Index + 3] = bgA;
            }
        }
    }

    return Success;
}

/// <summary>
/// Return an object list with <paramref name="EntryCount"/> objects.
/// </summary>
/// <param name="EntryCount">Size of the returned object list.</param>
/// <returns>Object list the appropriate size.</returns>
CommonObject* MakeObjectList (int EntryCount)
{
    CommonObject* ObjectList;
    ObjectList = new CommonObject[EntryCount];
    return ObjectList;
}

/// <summary>
/// Delete the object list created with <seealso cref="MakeObjectList"/>.
/// </summary>
/// <param name="ObjectList">The object list to delete.</param>
/// <param name="EntryCount">Number of objects in the object list.</param>
void DeleteObjectList (CommonObject* ObjectList, int EntryCount)
{
    if (ObjectList == NULL)
        return;
    if (EntryCount < 1)
        return;
    for (int i = 0; i < EntryCount; i++)
    {
        if (ObjectList[i].ObjectBuffer != NULL)
            delete[] ObjectList[i].ObjectBuffer;
    }
    delete[] ObjectList;
}

/// <summary>
/// Render the objects in <paramref name="DisplayList"/> on <paramref name="Target"/>.
/// </summary>
/// <param name="Target">The buffer where rendering will take place.</param>
/// <param name="TargetWidth">The width of the target buffer.</param>
/// <param name="TargetHeight">The height of the target buffer.</param>
/// <param name="TargetStride">The stride of the target buffer.</param>
/// <param name="DisplayList">The list of objects to render.</param>
/// <param name="DisplayListCount">Number of objects in <paramref name="DisplayList"/>.</param>
/// <returns>Value indicating result.</returns>
int RenderDisplayList (void* Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    DisplayInstructionList2* DisplayList, __int32 DisplayListCount)
{
    if (Target == NULL)
        return NullPointer;
    if (DisplayList == NULL)
        return NullPointer;
    if (DisplayListCount < 1)
        return NoActionTaken;

    BYTE* Buffer = (BYTE *) Target;
    CommonObject *ObjectList = MakeObjectList (DisplayListCount);

    ColorBlobParameters *CBBP = NULL;
    ColorBlockParameters *CBP = NULL;
    LinePlotParameters *LPP = NULL;
    BufferClearParameters *BCP = NULL;
    MassAlphaParameters *MAP = NULL;
    InversionParameters *IP = NULL;

    for (int i = 0; i < DisplayListCount; i++)
    {
        ObjectList[i].ObjectAction = DisplayList[i].Operand;
        switch (ObjectList[i].ObjectAction)
        {
            case NOP:
                break;

            case DrawColorBlob:
                CBBP = (ColorBlobParameters *) DisplayList[i].Parameters;
                ObjectList[i].Left = CBBP->X1;
                ObjectList[i].Top = CBBP->Y1;
                ObjectList[i].Right = CBBP->X1 + CBBP->Width;
                ObjectList[i].Bottom = CBBP->Y1 + CBBP->Height;
                ObjectList[i].ObjectHeight = CBBP->Height;
                ObjectList[i].ObjectWidth = CBBP->Width;
                ObjectList[i].ObjectStride = CBBP->Width * 4;
                ObjectList[i].ObjectBuffer = new byte[CBBP->Height * (CBBP->Width * 4)];
                RenderColorBlobEx (CBBP, &ObjectList[i]);
                break;

            case DrawColorBlock:
                CBP = (ColorBlockParameters *) DisplayList[i].Parameters;
                ObjectList[i].Left = CBP->X1;
                ObjectList[i].Top = CBP->Y1;
                ObjectList[i].ObjectHeight = CBP->Height;
                ObjectList[i].ObjectWidth = CBP->Width;
                ObjectList[i].Right = CBP->X1 + CBP->Width;
                ObjectList[i].Bottom = CBP->Y1 + CBP->Height;
                ObjectList[i].ObjectBuffer = new byte[(CBP->Height * CBP->Width * 4)];
                ObjectList[i].ObjectStride = CBP->Width * 4;
                RenderColorBlock (CBP, &ObjectList[i]);
                break;

            case PlotLine:
                LPP = (LinePlotParameters *) DisplayList[i].Parameters;
                ObjectList[i].Left = LPP->X1;
                ObjectList[i].Top = LPP->Y1;
                ObjectList[i].ObjectHeight = abs (LPP->Y1 - LPP->Y2 + 1);
                ObjectList[i].ObjectWidth = abs (LPP->X1 - LPP->X2 + 1);
                ObjectList[i].ObjectStride = ObjectList[i].ObjectWidth * 4;
                ObjectList[i].ObjectBuffer = new byte[ObjectList[i].ObjectWidth * ObjectList[i].ObjectStride];
                ObjectList[i].Right = LPP->X2;
                ObjectList[i].Bottom = LPP->Y2;
                RenderLinePlot (LPP, &ObjectList[i]);
                break;

            case DrawBackground:
                BCP = (BufferClearParameters *) DisplayList[i].Parameters;
                ClearRenderBuffer (Buffer, TargetWidth, TargetHeight, TargetStride, BCP);
                break;

            case Debug:
                break;

            case ResizeBuffer:
                break;

            case CopyBuffer:
                break;

            case MassAlpha:
                MAP = (MassAlphaParameters *) DisplayList[i].Parameters;
                UpdateBufferAlpha (Buffer, TargetWidth, TargetHeight, TargetStride, MAP);
                break;

            case InvertBuffer:
                IP = (InversionParameters*) DisplayList[i].Parameters;
                InvertRenderBuffer (Buffer, TargetWidth, TargetHeight, TargetStride, IP);
                break;

            default:
                return UnknownDisplayListOperand;
        }
    }

    CommonObjectRenderer (Buffer, TargetWidth, TargetHeight, TargetStride, ObjectList, DisplayListCount);

    DeleteObjectList (ObjectList, DisplayListCount);

    return Success;
}