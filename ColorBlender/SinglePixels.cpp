#include <intrin.h>
#include "ColorBlender.h"
#include "Structures.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Return the pixel at the specified location in the image.
/// </summary>
/// <param name="Source">Image where the pixel lives.</param>
/// <param name="SourceWidth">Width of the image in pixels.</param>
/// <param name="SourceHeight">Height of the image in scanlines.</param>
/// <param name="X">Horizontal pixel location.</param>
/// <param name="Y">Vertical pixel location.</param>
/// <returns>The pixel (in BGRA format) at the specified location.</returns>
UINT32 GetPixelAtLocation(void *Source, __int32 SourceWidth, __int32 SourceHeight, 
	__int32 X, __int32 Y)
{
	if (Source == NULL)
		return 0;
	if (X < 0)
		X = 0;
	if (X > SourceWidth - 1)
		X = SourceWidth - 1;
	if (Y < 0)
		Y = 0;
	if (Y > SourceHeight - 1)
		Y = SourceHeight - 1;

	UINT32 *Src = (UINT32 *)Source;

	UINT32 Index = Y * SourceWidth;
	Index += X;

	return Src[Index];
}

/// <summary>
/// Return the pixel at the specified location in the image.
/// </summary>
/// <param name="Source">Image where the pixel lives.</param>
/// <param name="SourceWidth">Width of the image in pixels.</param>
/// <param name="SourceHeight">Height of the image in scanlines.</param>
/// <param name="SourceStride">Stride of the image in bytes.</param>
/// <param name="X">Horizontal pixel location.</param>
/// <param name="Y">Vertical pixel location.</param>
/// <returns>The pixel (in BGRA format) at the specified location.</returns>
UINT32 GetPixelAtLocation2(void *Source, __int32 SourceWidth, __int32 SourceHeight,__int32 SourceStride,
	__int32 X, __int32 Y)
{
	if (Source == NULL)
		return 0;
	if (X < 0)
		X = 0;
	if (X > SourceWidth - 1)
		X = SourceWidth - 1;
	if (Y < 0)
		Y = 0;
	if (Y > SourceHeight - 1)
		Y = SourceHeight - 1;

	BYTE *Src = (BYTE *)Source;
	int PixelSize = 4;

	UINT32 Index = Y * SourceStride;
	Index += (X * PixelSize);

	BYTE A = Src[Index + 3];
	BYTE R = Src[Index + 2];
	BYTE G = Src[Index + 1];
	BYTE B = Src[Index + 0];

	UINT32 Result = (A << 24) | (R << 16) | (G << 8) | (B << 0);

	return Result;
}

