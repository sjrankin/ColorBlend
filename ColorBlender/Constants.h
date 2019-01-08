#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _CONSTANTS_H
#define _CONSTANTS_H

const __int32 ObjectMerge = 0;
const __int32 ObjectBlt = 1;
const __int32 IgnoreObject = 2;

extern "C" __declspec(dllexport) BSTR ErrorConstantToString(__int32 ErrorConstant);

enum FrequencyActions
{
    NoAction = 0,
    SetTransparencyAction = 1,
    InvertAction = 2,
    SetLuminanceAction = 3,
    SetColorAction = 4,
    ProportionalTransparentAction = 5
};

enum ColorAlphaActions
{
    UseColorAlpha = 0,
    UseSourceAlpha = 1,
    UseLuminanceProportionalAlpha = 2
};

enum ScalingTypes
{
    NoScaling = 0,
    NearestNeighbor = 1,
    Bilinear = 2,
};

/*
Display list instructions.
*/
/// <summary>
/// No operation.
/// </summary>
const __int32 NOP = 0;
/// <summary>
/// Draw a color blob.
/// </summary>
const __int32 DrawColorBlob = 1;
/// <summary>
/// Draw a color block.
/// </summary>
const __int32 DrawColorBlock = 2;
/// <summary>
/// Plot a line.
/// </summary>
const __int32 PlotLine = 3;
/// <summary>
/// Draw the background/clear the buffer.
/// </summary>
const __int32 DrawBackground = 4;
/// <summary>
/// Debug opcode.
/// </summary>
const __int32 Debug = 5;
/// <summary>
/// Crop (or resize) a buffer.
/// </summary>
const __int32 ResizeBuffer = 6;
/// <summary>
/// Copy data from one buffer to another.
/// </summary>
const __int32 CopyBuffer = 7;
/// <summary>
/// Set the alpha level of all pixels in a block.
/// </summary>
const __int32 MassAlpha = 8;
/// <summary>
/// Inverts the value of the pixels in a block.
/// </summary>
const __int32 InvertBuffer = 9;

/*
Result codes.
*/
/// <summary>
/// Initial result code value.
/// </summary>
const __int32 NotSet = -1;
/// <summary>
/// Result was successful.
/// </summary>
const __int32 Success = 0;
/// <summary>
/// Error encountered.
/// </summary>
const __int32 Error = 1;
/// <summary>
/// Bad index encountered.
/// </summary>
const __int32 BadIndex = 2;
/// <summary>
/// NULL pointer was encountered.
/// </summary>
const __int32 NullPointer = 3;
/// <summary>
/// Calculated negative index.
/// </summary>
const __int32 NegativeIndex = 4;
/// <summary>
/// Bad secondary index.
/// </summary>
const __int32 BadSecondaryIndex = 5;
/// <summary>
/// Index out of range.
/// </summary>
const __int32 IndexOutOfRange = 6;
/// <summary>
/// Given display list operation failed - returned due to display list operation return flag.
/// </summary>
const __int32 DisplayListOperationFailed = 7;
/// <summary>
/// This display list was empty.
/// </summary>
const __int32 EmptyDisplayList = 8;
/// <summary>
/// The display list did not contain any executable operands.
/// </summary>
const __int32 VirtualEmptyDisplayList = 9;
/// <summary>
/// Unknown display list operand encountered.
/// </summary>
const __int32 UnknownDisplayListOperand = 10;
/// <summary>
/// Invalid operation encountered.
/// </summary>
const __int32 InvalidOperation = 11;
/// <summary>
/// No action was done.
/// </summary>
const __int32 NoActionTaken = 12;
/// <summary>
/// A value expected to be normal was not.
/// </summary>
const __int32 NormalNonNormal = 13;
/// <summary>
/// Something is not implemented.
/// </summary>
const __int32 NotImplemented = 14;
/// <summary>
/// Image comparison result for mismatches.
/// </summary>
const __int32 ImageMismatch = 15;
/// <summary>
/// Image comparison result for matches.
/// </summary>
const __int32 ImagesMatch = 16;
/// <summary>
/// No header found.
/// </summary>
const __int32 HeaderNotFound = 17;
/// <summary>
/// Unexpected data type found in header.
/// </summary>
const __int32 HeaderBadDataType = 18;
/// <summary>
/// Computed index or range out of range of original data.
/// </summary>
const __int32 ComputedIndexOutOfRange = 19;
/// <summary>
/// No pixels selected for a given operation.
/// </summary>
const __int32 NoPixelsSelected = 20;
/// <summary>
/// Invalid region specified, directly or indirectly.
/// </summary>
const __int32 InvalidRegion = 21;
/// <summary>
/// Unknown error code.
/// </summary>
const __int32 UnknownErrorCode = 22;
/// <summary>
/// The error stack is empty - unable to pop.
/// </summary>
const __int32 ErrorStackIsEmpty = 23;
/// <summary>
/// The error stack is full - unable to push.
/// </summary>
const __int32 ErrorStackIsFull = 24;
/// <summary>
/// Value is too small for operation.
/// </summary>
const __int32 ValueTooSmall = 25;
/// <summary>
/// Value is too big for operation.
/// </summary>
const __int32 ValueTooBig = 26;
/// <summary>
/// Parameter validation failed - check error stack.
/// </summary>
const __int32 FailedParameterValidation = 27;
/// <summary>
/// Image dimensions don't match as required.
/// </summary>
const __int32 DimensionalMismatch = 28;
/// <summary>
/// Invalid rotational value specified.
/// </summary>
const __int32 BadRotation = 29;


const __int32 SimpleInvertOperation = 0;
const __int32 VariableInvertOperation = 1;
const __int32 ChannelInversionOperation = 2;

const __int32 UnitaryAlphaApplication = 0;
const __int32 VariableAlphaApplication = 1;

const BYTE AlphaChannel = 1;
const BYTE RedChannel = 2;
const BYTE GreenChannel = 4;
const BYTE BlueChannel = 8;

/*
enum Channels
{
    Alpha = 1,
    Red = 2,
    Green = 3,
    Blue = 4
};
*/

const BYTE ToHSL = 0;
const BYTE FromHSL = ToHSL;
const BYTE ToHSV = 1;
const BYTE FromHSV = ToHSV;
const BYTE ToRGB = 2;
const BYTE FromRGB = ToRGB;
const BYTE ToLAB = 3;
const BYTE FromLAB = ToLAB;
const BYTE ToXYZ = 4;
const BYTE FromXYZ = ToXYZ;
const BYTE ToCMY = 5;
const BYTE FromCMY = ToCMY;
const BYTE ToCMYK = 6;
const BYTE FromCMYK = ToCMYK;
const BYTE ToYCbCr = 7;
const BYTE FromYCbCr = ToYCbCr;
const BYTE ToYUV = 8;
const BYTE FromYUV = ToYUV;
const BYTE ToYIQ = 9;
const BYTE FromYIQ = ToYIQ;

const int SortRGB = 0;
const int SortRBG = 1;
const int SortGRB = 2;
const int SortGBR = 3;
const int SortBRG = 4;
const int SortBGR = 5;

#endif