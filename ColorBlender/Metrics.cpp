#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

/// <summary>
/// Contains one color count value.
/// </summary>
struct ColorResult
{
	/// <summary>
	/// The color value (with alpha zeroed out).
	/// </summary>
	UINT32 ColorValue;
	/// <summary>
	/// The number of instances of the color.
	/// </summary>
	UINT32 ColorCount;
	/// <summary>
	/// Structure is initialized.
	/// </summary>
	BOOL Initialized;
};

/// <summary>
/// Return a list of a specified number of colors based on color counts.
/// </summary>
/// <param name="ColorCounts">Array of counts for all colors in the color space.</param>
/// <param name="MaxColors">Size of <paramref name="ColorCounts"/>.</param>
/// <param name="GreatestCount">Determines the number of colors to return.</param>
/// <param name="Results">Where the results are returned.</param>
/// <returns>Value indicating operational results.</returns>
int GetLargestQuantities(UINT32 *ColorCounts, int MaxColors, int GreatestCount, ColorResult *Results)
{
	if (ColorCounts == NULL)
		return NullPointer;
	if (Results == NULL)
		return NullPointer;

	for (int i = 0; i < MaxColors; i++)
	{
		if (ColorCounts[i] == 0)
			continue;
		int SmallestValue = MAXINT;
		int IndexOfSmallestValue = 0;
		for (int k = 0; k < GreatestCount; k++)
		{
			if (Results[k].ColorCount < SmallestValue)
			{
				SmallestValue = Results[k].ColorCount;
				IndexOfSmallestValue = k;
			}
		}
		if (ColorCounts[i] < Results[IndexOfSmallestValue].ColorCount)
			continue;
		Results[IndexOfSmallestValue].ColorValue = i;
		Results[IndexOfSmallestValue].ColorCount = ColorCounts[i];
		Results[IndexOfSmallestValue].Initialized = TRUE;
	}
}

/// <summary>
/// Counts and returns the number of unique colors in the passed image. Alpha is ignored and zeroed out in all calculations.
/// </summary>
/// <param name="Source">Source image buffer.</param>
/// <param name="Width">Width of the image in pixels.</param>
/// <param name="Height">Height of the image in scanlines.</param>
/// <param name="UniqueColorCount">On success, will contain the number of unique colors.</param>
/// <param name="Results">
/// Array of color/count pairs. Size of array is dependent on the number of unique colors and
/// <paramref name="ColorsToReturn"/>. Results must be allocated by the caller.
/// </param>
/// <param name="ColorsToReturn">
/// The number of colors to return. Colors are sorted in descending count order before being returned.
/// </param>
/// <returns>Value indicating operational result.</returns>
int ReturnUniqueColors(void *Source, __int32 Width, __int32 Height, UINT32 *UniqueColorCount,
	void *Results, __int32 ColorsToReturn)
{
	if (Source == NULL)
		return NullPointer;
	if (Results == NULL)
		return NullPointer;

	int MaxColors = 256 * 256 * 256;

	ColorResult *Counts = (ColorResult *)Results;
	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Map = (UINT32 *)new UINT32[MaxColors];
	for (int i = 0; i < MaxColors; i++)
		Map[i] = 0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Height;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = RowOffset + Column;
			UINT32 Pixel = Src[Index];
			Pixel = Pixel & 0x00ffffff;
			Map[Pixel]++;
		}
	}

	int Count = 0;
	for (int i = 0; i < MaxColors; i++)
	{
		if (Map[i] == 0)
			continue;
		Count++;
	}
	*UniqueColorCount = Count;

	int FinalResultCount = Count < ColorsToReturn ? Count : ColorsToReturn;
	int OpResult = GetLargestQuantities(Map, MaxColors, FinalResultCount, Counts);

	delete[] Map;
	return Success;
}

/// <summary>
/// Count the number of unique colors in the passed image. Alpha is ignored and zeroed out in all calculations.
/// </summary>
/// <param name="Source">Source image buffer.</param>
/// <param name="Width">Width of the image in pixels.</param>
/// <param name="Height">Height of the image in scanlines.</param>
/// <param name="UniqueColorCount">On success, will contain the number of unique colors.</param>
/// <returns>Value indicating operational result.</returns>
int CountUniqueColors(void *Source, __int32 Width, __int32 Height, UINT32 *UniqueColorCount)
{
	if (Source == NULL)
		return NullPointer;

	int MaxColors = 256 * 256 * 256;
	UINT32 *Src = (UINT32 *)Source;
	UINT32 *Map = (UINT32 *)new UINT32[MaxColors];
	for (int i = 0; i < MaxColors; i++)
		Map[i] = 0;

	for (int Row = 0; Row < Height; Row++)
	{
		int RowOffset = Row * Height;
		for (int Column = 0; Column < Width; Column++)
		{
			int Index = RowOffset + Column;
			UINT32 Pixel = Src[Index];
			Pixel = Pixel & 0x00ffffff;
			Map[Pixel]++;
		}
	}

	int Count = 0;
	for (int i = 0; i < MaxColors; i++)
	{
		if (Map[i] == 0)
			continue;
		Count++;
	}
	*UniqueColorCount = Count;

	delete[] Map;
	return Success;
}

