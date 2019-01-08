#include <intrin.h>
#include "ColorBlender.h"
#include "Structures.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Rotate an image by 90° clockwise (or 270° counterclockwise).
/// </summary>
const int Rotate90CW = 90;
/// <summary>
/// Rotate an image by 180°.
/// </summary>
const int Rotate180CW = 180;
/// <summary>
/// Rotate an image by 270° clockwise (or 90° counterclockwise).
/// </summary>
const int Rotate270CW = 270;

/// <summary>
/// Rotate the image in <paramref name="Source"/> by the specified number of degrees.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <param name="RotateHow">
/// Number of degrees to rotate the image in a clockwise dirction. Must be one of
/// 90, 180, or 270. Other values will result in an error returned.
/// </param>
/// <returns>Value indication operational results.</returns>
int ImageRotateRightBy(void *Source, __int32 SourceWidth, __int32 SourceHeight,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight,
	int RotateHow)
{
	switch (RotateHow)
	{
	case Rotate90CW:
		return ImageRotateRight90(Source, SourceWidth, SourceHeight, Destination, DestinationWidth, DestinationHeight);

	case Rotate180CW:
		return ImageRotateRight180(Source, SourceWidth, SourceHeight, Destination, DestinationWidth, DestinationHeight);

	case Rotate270CW:
		return ImageRotateRight270(Source, SourceWidth, SourceHeight, Destination, DestinationWidth, DestinationHeight);

	default:
		return BadRotation;
	}
}

/// <summary>
/// Rotate an image by 90 degrees right (clockwise) and place the result in the destination.
/// </summary>
/// <remarks>
/// Stride is not required because this function moves pixels as a whole, eg, 32-bit values, not on a
/// color channel (byte) basis.
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <returns>Value indication operational results.</returns>
int ImageRotateRight90(void *Source, __int32 SourceWidth, __int32 SourceHeight, void *Destination,
	__int32 DestinationWidth, __int32 DestinationHeight)
{
	/*
	__asm
	{
		int 3
	}
	*/
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	if (SourceWidth != DestinationHeight)
		return DimensionalMismatch;
	if (SourceHeight != DestinationWidth)
		return DimensionalMismatch;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	int DestinationRow = 0;
	int DestinationColumn = DestinationWidth - 1;

	for (int SourceRow = 0; SourceRow < SourceHeight; SourceRow++)
	{
		for (int SourceColumn = 0; SourceColumn < SourceWidth; SourceColumn++)
		{
			UINT32 SourceIndex = (SourceRow * SourceWidth) + SourceColumn;
			UINT32 Pixel = Src[SourceIndex];
			UINT32 NewX = (SourceHeight - 1) - SourceRow;
			UINT32 NewY = SourceColumn;
			UINT32 DestinationIndex = (NewY * DestinationWidth) + NewX;
			Dest[DestinationIndex] = Pixel;
		}
	}

	return Success;
}

/// <summary>
/// Rotate an image by 270 degrees right (clockwise) (AKA 90 degrees counterclockwise) and place the result in the destination.
/// </summary>
/// <remarks>
/// Stride is not required because this function moves pixels as a whole, eg, 32-bit values, not on a
/// color channel (byte) basis.
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <returns>Value indication operational results.</returns>
int ImageRotateRight270(void *Source, __int32 SourceWidth, __int32 SourceHeight, void *Destination,
	__int32 DestinationWidth, __int32 DestinationHeight)
{
	/*
	__asm
	{
		int 3
	}
	*/
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	if (SourceWidth != DestinationHeight)
		return DimensionalMismatch;
	if (SourceHeight != DestinationWidth)
		return DimensionalMismatch;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	int DestinationRow = 0;
	int DestinationColumn = DestinationWidth - 1;

	for (int SourceRow = 0; SourceRow < SourceHeight; SourceRow++)
	{
		for (int SourceColumn = 0; SourceColumn < SourceWidth; SourceColumn++)
		{
			UINT32 SourceIndex = (SourceRow * SourceWidth) + SourceColumn;
			UINT32 Pixel = Src[SourceIndex];
			UINT32 NewX = SourceRow;
			UINT32 NewY = (SourceWidth - 1) - SourceColumn;
			UINT32 DestinationIndex = (NewY * DestinationWidth) + NewX;
			Dest[DestinationIndex] = Pixel;
		}
	}

	return Success;
}

/// <summary>
/// Rotate an image by 180 degrees right (clockwise) and place the result in the destination.
/// </summary>
/// <remarks>
/// Stride is not required because this function moves pixels as a whole, eg, 32-bit values, not on a
/// color channel (byte) basis.
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <returns>Value indication operational results.</returns>
int ImageRotateRight180(void *Source, __int32 SourceWidth, __int32 SourceHeight, void *Destination,
	__int32 DestinationWidth, __int32 DestinationHeight)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	if (SourceWidth != DestinationWidth)
		return DimensionalMismatch;
	if (SourceHeight != DestinationHeight)
		return DimensionalMismatch;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	int DestinationRow = 0;
	int DestinationColumn = DestinationWidth - 1;

	for (int SourceRow = 0; SourceRow < SourceHeight; SourceRow++)
	{
		for (int SourceColumn = 0; SourceColumn < SourceWidth; SourceColumn++)
		{
			UINT32 SourceIndex = (SourceRow * SourceWidth) + SourceColumn;
			UINT32 Pixel = Src[SourceIndex];
			UINT32 NewColumn = (SourceWidth - 1) - SourceColumn;
			UINT32 NewRow = (SourceHeight - 1) - SourceRow;
			UINT32 DestinationIndex = (NewRow * SourceWidth) + NewColumn;
			Dest[DestinationIndex] = Pixel;
		}
	}

	return Success;
}

/// <summary>
/// Rotate an image and place the result in the destination.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <param name="DestinationStride">Stride of the destination image.</param>
/// <param name="Rotation">Degrees to rotate.</param>
/// <param name="FillColor">Background color for the destination image.</param>
/// <returns>Value indication operational results.</returns>
int ImageRotate(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	double Rotation, UINT32 FillColor)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

	ClearBuffer2(Dest, DestinationWidth, DestinationHeight, DestinationStride, FillColor);

	int NewRow = DestinationHeight - 1;
	int NewColumn = DestinationWidth - 1;
	for (int OldRow = 0; OldRow < SourceHeight; OldRow++)
	{
		for (int OldColumn = 0; OldColumn < SourceWidth; OldColumn++)
		{
			int SourceIndex = OldColumn + (OldRow * SourceWidth);
			int DestIndex = NewColumn + (NewRow * DestinationWidth);
			Dest[DestIndex] = Src[SourceIndex];
			NewColumn--;
			if (NewColumn < 0)
			{
				NewColumn = DestinationWidth - 1;
				NewRow--;
				if (NewRow < 0)
					return Success;
			}
		}
	}

	return Success;
}

/// <summary>
/// Rotate an image by 90 degrees and place the result in the destination.
/// </summary>
/// <remarks>
/// Stride is not required because this function moves pixels as a whole, eg, 32-bit values, not on a
/// color channel (byte) basis.
/// </remarks>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <returns>Value indication operational results.</returns>
int ImageRotate90(void *Source, __int32 SourceWidth, __int32 SourceHeight,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	//Make sure the source image's width is the same as the destination image's height.
	if (SourceWidth != DestinationHeight)
		return InvalidRegion;
	//Make sure the source image's height is the same as the destination image's width.
	if (SourceHeight != DestinationWidth)
		return InvalidRegion;

	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Dest = (UINT32 *)Destination;

//	double Radians90 = (90 * 3.1415) / 180.0;
	double Radians90 = 1.570796327;
	double Cos90 = cos(Radians90);
	double Sin90 = sin(Radians90);
	double HalfWidth = (double)(int)(SourceWidth / 2.0);
	double HalfHeight = (double)(int)(SourceHeight / 2.0);

	for (int OldRow = 0; OldRow < SourceHeight; OldRow++)
	{
		double RowCos90 = OldRow * Cos90;
		double RowSin90 = OldRow * Sin90;
		for (int OldColumn = 0; OldColumn < SourceWidth; OldColumn++)
		{
			int SourceIndex = OldColumn + (OldRow * SourceWidth);
#if 0
			double NewX = (OldColumn * Cos90) - RowSin90;
			NewX = NewX + HalfWidth;
			NewX--;
			double NewY = (OldColumn * Sin90) + RowCos90;
			NewY = NewY + HalfHeight;
			NewY--;
			int DestIndex = ((int)NewY * DestinationWidth) + (int)NewX;
#else
			double NewX = (OldColumn * Cos90) - (OldRow * Sin90);
			double NewY = (OldColumn * Sin90) + (OldRow * Cos90);
			NewX += HalfWidth;
			NewY += HalfHeight;
			NewX--;
			NewY--;
			NewX = (int)(roundf(NewX));
			NewY = (int)(roundf(NewY));
			UINT32 DestIndex = ((int)NewY * DestinationWidth) + (int)NewX;
#endif
			Dest[DestIndex] = Src[SourceIndex];
		}
	}

	return Success;
}

/// <summary>
/// Rotate an image and place the result in the destination. Rotates the image 90 right or 90 left only.
/// </summary>
/// <param name="Source">Source image.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="Destination">Destination image.</param>
/// <param name="DestinationWidth">Width of the destination image.</param>
/// <param name="DestinationHeight">Height of the destination image.</param>
/// <param name="DestinationStride">Stride of the destination image.</param>
/// <param name="RotateLeft">Determines rotation direction.</param>
/// <returns>Value indicating operational results.</returns>
int CardinalImageRotate(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride,
	BOOL RotateLeft)
{
	return ImageRotate(Source, SourceWidth, SourceHeight, SourceStride, Destination, DestinationWidth,
		DestinationHeight, DestinationStride, RotateLeft ? -90.0 : 90.0, 0xffffffff);
}

/// <summary>
/// Rotate the source buffer 90 degrees to the right and place the result in the supplied destination buffer.
/// </summary>
/// <param name="Source">The image to rotate.</param>
/// <param name="SourceWidth">Width of the source in pixels.</param>
/// <param name="SourceHeight">Height of the source in scan lines.</param>
/// <param name="SourceStride">Stride of the source.</param>
/// <param name="Destination">Where the rectangle will be rendered.</param>
/// <param name="DestinationWidth">Width of the destination in pixels.</param>
/// <param name="DestinationHeight">Height of the destination in scan lines.</param>
/// <param name="DestinationStride">Stride of the destination.</param>
/// <returns>Value indicating operational success.</returns>
int RotateBufferRight(void *Source, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *Destination, __int32 DestinationWidth, __int32 DestinationHeight, __int32 DestinationStride)
{
	if (Source == NULL)
		return NullPointer;
	if (Destination == NULL)
		return NullPointer;

	BYTE *SrcBuffer = (BYTE *)Source;
	BYTE *DestBuffer = (BYTE *)Destination;
	int PixelSize = 4;
	int DestRow = 0;
	int DestColumn = 0;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			int DestIndex = (DestColumn * PixelSize) + (DestRow * DestinationStride);
			DestBuffer[DestIndex + 0] = SrcBuffer[Index + 0];
			DestBuffer[DestIndex + 1] = SrcBuffer[Index + 1];
			DestBuffer[DestIndex + 2] = SrcBuffer[Index + 2];
			DestBuffer[DestIndex + 3] = SrcBuffer[Index + 3];
			DestColumn++;
			if (DestColumn >= DestinationWidth)
			{
				DestColumn = 0;
				DestRow++;
			}
		}
	}
	return Success;
}

