#include "ColorBlender.h"
#include "Structures.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Create a horizontal mirror image of a region in <paramref name="Source"/> and return it in <paramref name="Destination"/>. Mirroring is
/// done at a pixel level.
/// </summary>
/// <remarks>
/// Stride is not needed since we're working at a pixel level.
/// </remarks>
/// <param name="Source">Pointer to the image to horizontally mirror.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the mirrored image.</param>
/// <param name="X1">Left side of the region.</param>
/// <param name="Y1">Top of the region.</param>
/// <param name="X2">Right side of the region.</param>
/// <param name="Y2">Bottom of the region.</param>
/// <returns>Value indicating operational success.</returns>
int HorizontalMirrorPixelRegion(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int Y1, int X2, int Y2)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	for (int Row = Y1; Row <= Y2; Row++)
	{
		int RowOffset = Row * Width;
		int DestRowOffset = (Row - Y1) * Width;
		for (int Column = X1; Column <= X2; Column++)
		{
			int SrcIndex = Column + RowOffset;
			int DestIndex = (Width - (Column - X1) - 1) + DestRowOffset;
			Dest[DestIndex] = Src[SrcIndex];
		}
	}

	return Success;
}

/// <summary>
/// Create a horizontal mirror image of <paramref name="Source"/> and return it in <paramref name="Destination"/>. Mirroring is
/// done at a pixel level.
/// </summary>
/// <remarks>
/// Stride is not needed since we're working at a pixel level.
/// </remarks>
/// <param name="Source">Pointer to the image to horizontally mirror.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the mirrored image.</param>
/// <returns>Value indicating operational success.</returns>
int HorizontalMirrorPixel(void *Source, __int32 Width, __int32 Height, void *Destination)
{
	return HorizontalMirrorPixelRegion(Source, Width, Height, Destination, 0, 0, Width - 1, Height - 1);
	/*
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Width;
		for (int Column = 0; Column < Width; Column++)
		{
			int SrcIndex = Column + RowOffset;
			int DestIndex = (Width - Column - 1) + RowOffset;
			Dest[DestIndex] = Src[SrcIndex];
		}
	}

	return Success;
	*/
}

/// <summary>
/// Create a horizontal mirror image of <paramref name="Source"/> and return it in <paramref name="Destination"/>. Mirroring is
/// done at a byte level.
/// </summary>
/// <param name="Source">Pointer to the image to horizontally mirror.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the mirrored image.</param>
/// <returns>Value indicating operational success.</returns>
int HorizontalMirrorByte(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SetAlpha, BYTE AlphaValue)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = 0; Column < Stride; Column++)
		{
			int DestIndex = (Stride - (Column + 1)) + RowOffset;
			Dest[DestIndex] = Src[Column + RowOffset];
		}
	}

	if (SetAlpha)
	{
		int ReturnValue = SetAlphaChannelInPlace(Destination, Width, Height, Stride, AlphaValue);
		if (ReturnValue != Success)
			return ReturnValue;
	}

	return Success;
}

/// <summary>
/// Create a vertical mirror image of a region in <paramref name="Source"/> and return it in <paramref name="Destination"/>. 
/// Mirroring is done a pixel level.
/// </summary>
/// <remarks>
/// Don't need stride since we're working on a pixel level.
/// </remarks>
/// <param name="Source">Pointer to the image to vertically mirror.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the mirrored image.</param>
/// <param name="X1">Left side of the region.</param>
/// <param name="Y1">Top of the region.</param>
/// <param name="X2">Right side of the region.</param>
/// <param name="Y2">Bottom of the region.</param>
/// <returns>Value indicating operational success.</returns>
int VerticalMirrorPixelRegion(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int Y1, int X2, int Y2)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	for (int Column = X1; Column <= X2; Column++)
	{
		for (int Row = Y1; Row <= Y2; Row++)
		{
			int RowOffset = Row * Width;
			int SourceIndex = RowOffset + Column;
			int DestIndex = ((Height - (Row - Y1) - 1) * Width) + (Column - X1);
			Dest[DestIndex] = Src[SourceIndex];
		}
	}

	return Success;
}

/// <summary>
/// Create a vertical mirror image of <paramref name="Source"/> and return it in <paramref name="Destination"/>. Mirroring is done
/// on a pixel level.
/// </summary>
/// <remarks>
/// Don't need stride since we're working on a pixel level.
/// </remarks>
/// <param name="Source">Pointer to the image to vertically mirror.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the mirrored image.</param>
/// <returns>Value indicating operational success.</returns>
int VerticalMirrorPixel(void *Source, __int32 Width, __int32 Height, void *Destination)
{
	return VerticalMirrorPixelRegion(Source, Width, Height, Destination, 0, 0, Width - 1, Height - 1);
	/*
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	for (int Column = 0; Column < Width; Column++)
	{
		for (int Row = 0; Row < Height; Row++)
		{
			int RowOffset = Row * Width;
			int SourceIndex = RowOffset + Column;
			int DestIndex = ((Height - Row - 1) * Width) + Column;
			Dest[DestIndex] = Src[SourceIndex];
		}
	}

	return Success;
	*/
}

/// <summary>
/// Create a vertical mirror image of <paramref name="Source"/> and return it in <paramref name="Destination"/>. Mirroring is done
/// on a byte level.
/// </summary>
/// <param name="Source">Pointer to the image to vertically mirror.</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the mirrored image.</param>
/// <returns>Value indicating operational success.</returns>
int VerticalMirrorByte(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SetAlpha, BYTE AlphaValue)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	int PixelSize = 4;
	BYTE *Src = (BYTE *)Source;
	BYTE *Dest = (BYTE *)Destination;

	for (int Column = 0; Column < Width; Column++)
	{
		for (int Row = 0; Row < Height; Row++)
		{
			int SourceIndex = (Row * Stride) + (Column * PixelSize);
			int DestIndex = ((Height - Row - 1) * Stride) + (Column * PixelSize);
			Dest[DestIndex + 0] = Src[SourceIndex + 3];
			Dest[DestIndex + 1] = Src[SourceIndex + 2];
			Dest[DestIndex + 2] = Src[SourceIndex + 1];
			Dest[DestIndex + 3] = Src[SourceIndex + 0];
		}
	}

	if (SetAlpha)
	{
		int ReturnValue = SetAlphaChannelInPlace(Destination, Width, Height, Stride, AlphaValue);
		if (ReturnValue != Success)
			return ReturnValue;
	}

	return Success;
}

int ULtoLRRegion(void *Source, __int32 Width, __int32 Height, void *Destination, int X1, int Y1, int X2, int Y2)
{
	byte *Scratch = new byte[Height * Width * 8];
	int Result = VerticalMirrorPixelRegion(Source, Width, Height, Scratch, X1, Y1, X2, Y2);
	if (Result == Success)
	{
		Result = HorizontalMirrorPixelRegion(Scratch, Width, Height, Destination, X1, Y1, X2, Y2);
	}
	delete[] Scratch;
	return Result;
}

/// <summary>
/// Mirror the image vertically then horizontally on a pixel level.
/// </summary>
/// <param name="Source">Pointer to the source image</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the final image.</param>
/// <returns>Value indicating operational success.</returns>
int ULtoLRPixel(void *Source, __int32 Width, __int32 Height, void *Destination)
{
	byte *Scratch = new byte[Height * Width * 8];
	int Result = VerticalMirrorPixel(Source, Width, Height, Scratch);
	if (Result == Success)
	{
		Result = HorizontalMirrorPixel(Scratch, Width, Height, Destination);
	}
	delete[] Scratch;
	return Result;
}

/// <summary>
/// Mirror the image vertically then horizontally on a byte level.
/// </summary>
/// <param name="Source">Pointer to the source image</param>
/// <param name="Width">Width of the source and destination buffers.</param>
/// <param name="Height">Height of the source and destination buffers.</param>
/// <param name="Stride">Stride of the source and destination buffers.</param>
/// <param name="Destination">Pointer to the buffer that will contained the final image.</param>
/// <returns>Value indicating operational success.</returns>
int ULtoLRByte(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination, BOOL SetAlpha, BYTE AlphaValue)
{
	byte *Scratch = new byte[Height * Stride];
	int Result = VerticalMirrorByte(Source, Width, Height, Stride, Scratch, SetAlpha, AlphaValue);
	if (Result == Success)
	{
		Result = HorizontalMirrorByte(Scratch, Width, Height, Stride, Destination, SetAlpha, AlphaValue);
	}
	delete[] Scratch;
	return Result;
}

int ClearBufferRegion(void *Destination, __int32 Width, __int32 Height, __int32 Stride, UINT32 FillColor,
	__int32 Left, __int32 Top, __int32 Right, __int32 Bottom)
{
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
	BYTE A = (FillColor & 0xff000000) >> 24;
	BYTE R = (FillColor & 0x00ff0000) >> 16;
	BYTE G = (FillColor & 0x0000ff00) >> 8;
	BYTE B = (FillColor & 0x000000ff) >> 0;

	for (int Row = Top; Row <= Bottom; Row++)
	{
		int RowOffset = Row * Stride;
		for (int Column = Left; Column <= Right; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			Dest[Index + 3] = A;
			Dest[Index + 2] = R;
			Dest[Index + 1] = G;
			Dest[Index + 0] = B;
		}
	}
	return Success;
}

/// <summary>
/// Translate a region in a buffer to a different location and return the result in a new buffer.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="RegionLeft">Left-side of the region to move.</param>
/// <param name="RegionTop">Top of the region to move.</param>
/// <param name="RegionRight">Right-side of the region to move.</param>
/// <param name="RegionBottom">Bottom of the region to move.</param>
/// <param name="NewX">New X coordinate of the upper-left corner of the translated region.</param>
/// <param name="NewY">New Y coordinate of the upper-left corner of the translated region.</param>
/// <param name="DoCopy">If TRUE, the region is copied. If FALSE, the old region is filled with <paramref name="GapColor"/>.</param>
/// <param name="GapColor">ARGB color to fill the region's original location if <paramref name="DoCopy"/> is FALSE.</param>
/// <returns>Value indicating operational results.</returns>
int TranslateRegionInImage(void *Source, __int32 Width, __int32 Height, __int32 Stride, void *Destination,
	__int32 RegionLeft, __int32 RegionTop, __int32 RegionRight, __int32 RegionBottom,
	__int32 NewX, __int32 NewY, BOOL DoCopy, UINT32 GapColor)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	if (RegionLeft >= RegionRight)
		return NoActionTaken;
	if (RegionTop >= RegionBottom)
		return NoActionTaken;

	RegionLeft += NewX;
	RegionRight += NewX;
	RegionTop += NewY;
	RegionBottom += NewY;

	if (RegionLeft > Width)
		return NoActionTaken;
	if (RegionTop > Height)
		return NoActionTaken;
	if (RegionBottom < 0)
		return NoActionTaken;
	if (RegionRight < 0)
		return NoActionTaken;

	int PixelSize = 4;
	int RegionWidth = RegionRight - RegionLeft + 1;
	int RegionHeight = RegionBottom - RegionTop + 1;
	int RegionStride = RegionWidth * PixelSize;
	BYTE *Region = new BYTE[RegionHeight * RegionStride];
	AbsolutePointStruct *UL = new AbsolutePointStruct();
	UL->X = RegionLeft;
	UL->Y = RegionTop;
	AbsolutePointStruct *LR = new AbsolutePointStruct();
	LR->X = RegionRight;
	LR->Y = RegionBottom;

	CopyRegion(Source, Width, Height, Stride, Region, RegionWidth, RegionStride, UL, LR);
	CopyBufferToBuffer(Source, Width, Height, Stride, Destination);
	if (!DoCopy)
		ClearBufferRegion2(Destination, RegionWidth, RegionHeight, RegionStride, GapColor, RegionLeft, RegionTop, RegionRight, RegionBottom);
	PasteRegion(Destination, Width, Height, Stride, Region, RegionWidth, RegionHeight, RegionStride, UL, LR);

	return Success;
}

/// <summary>
/// Squishes an image by removing scan lines or vertical columns according to a user-defined frequency.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="Width">Width of the source image.</param>
/// <param name="Height">Height of the source image.</param>
/// <param name="Stride">Stride of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestWidth">Width of the destination image.</param>
/// <param name="DestHeight">Height of the destination image.</param>
/// <param name="DestStride">Stride of the destination image.</param>
/// <param name="HorizontalFrequency">The frequency of included columns.</param>
/// <param name="VerticalFrequency">The frequency of included rows.</param>
/// <returns>Value indicating operational results.</returns>
int SquishImage(void *Source, __int32 Width, __int32 Height, __int32 Stride,
	void *Destination, __int32 DestWidth, __int32 DestHeight, __int32 DestStride,
	__int32 HorizontalFrequency, __int32 VerticalFrequency)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;
	if (HorizontalFrequency < 1)
		return InvalidOperation;
	if (VerticalFrequency < 1)
		return InvalidOperation;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;
	int PixelSize = 4;
	int DestRow = 0;
	int DestColumn = 0;

	for (int Row = 0; Row < Height; Row++)
	{
		if (Row % VerticalFrequency != 0)
			continue;
		int RowOffset = Row * Width;
		int DestOffset = DestRow * DestWidth;
		DestRow++;
		for (int Column = 0; Column < Width; Column++)
		{
			if (Column % HorizontalFrequency != 0)
				continue;

			int SourceIndex = RowOffset + Column;
			int DestIndex = DestOffset + DestColumn;
			DestColumn++;
			Dest[DestIndex] = Src[SourceIndex];
		}
		DestColumn = 0;
	}

	return Success;
}

