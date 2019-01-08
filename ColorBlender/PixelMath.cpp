#include <intrin.h>
#include "ColorBlender.h"
#include "Structures.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)

const int PixelMathLogicalAnd = 0;
const int PixelMathLogicalOr = 1;
const int PixelMathLogicalXor = 2;
const int PixelMathArithmeticAdd = 3;
const int PixelMathArithmeticSubtract = 4;
const int PixelMathArithmeticMultiply = 5;
const int PixelMathArithmeticDivide = 6;
const int PixelMathArithmeticMod = 7;
const int pixelMathRandomWithMask = 8;

/// <summary>
/// Execute the specified operation with the specified constant on the passed value.
/// </summary>
/// <param name="Value">The value the operation and constant is applied to.</param>
/// <param name="Operation">The operation to perform.</param>
/// <param name="Constant">The constant to apply to <pararef name="Value"/> with the specified operation.</param>
/// <returns>Modified value.</returns>
BYTE ExecuteOperation(BYTE Value, int Operation, int Constant)
{
	int Scratch = 0;

	switch (Operation)
	{
	case PixelMathLogicalAnd:
		return Value & Constant;

	case PixelMathLogicalOr:
		return Value | Constant;

	case PixelMathLogicalXor:
		return Value ^ Constant;

	case PixelMathArithmeticAdd:
		Scratch = Value + Constant;
		if (Scratch > 0xff)
			Scratch = 0xff;
		return Scratch;

	case PixelMathArithmeticSubtract:
		Scratch = Value - Constant;
		if (Scratch < 0)
			Scratch = 0;
		return Scratch;

	case PixelMathArithmeticMultiply:
		Scratch = Value * Constant;
		if (Scratch > 0xff)
			Scratch = 0xff;
		return Scratch;

	case PixelMathArithmeticDivide:
		if (Constant == 0)
			return Value;
		Scratch = Value / Constant;
		return Scratch;

	case PixelMathArithmeticMod:
		if (Constant == 0)
			return Value;
		Scratch = (int)fmod(Value, Constant);
		return Scratch;

	case pixelMathRandomWithMask:
		unsigned int number;
		errno_t err = rand_s(&number);
		if (err != 0)
			return Value;
		double sr = (double)number / (double)UINT_MAX;
		int RandomBits = (int)(sr * 255.0);
		Scratch = (Value & ~Constant) | (RandomBits & Constant);
		return Scratch;
	}
	return 0;
}

/// <summary>
/// Perform a logical or arithmetic operation on color channels in <paramref name="SourceBuffer"/>.
/// </summary>
/// <param name="SourceBuffer">The source image buffer.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">The destination buffer. Must have same dimensions as the source buffer.</param>
/// <param name="Operation">The operation to perform.</param>
/// <param name="Constant">The constant to apply.</param>
/// <param name="ApplyToRed">Apply the operation/constant to the red channel.</param>
/// <param name="ApplyToGreen">Apply the operation/constant to the green channel.</param>
/// <param name="ApplyToBlue">Apply the operation/constant to the blue channel.</param>
int PixelMathLogicalOperation(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int Operation, int Constant,
	BOOL ApplyToAlpha, BOOL ApplyToRed, BOOL ApplyToGreen, BOOL ApplyToBlue)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;
	if (Operation < PixelMathLogicalAnd || Operation > pixelMathRandomWithMask)
		return InvalidOperation;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;

	int PixelSize = 4;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];

			if (ApplyToAlpha)
				A = ExecuteOperation(A, Operation, Constant);
			if (ApplyToRed)
				R = ExecuteOperation(R, Operation, Constant);
			if (ApplyToGreen)
				G = ExecuteOperation(G, Operation, Constant);
			if (ApplyToBlue)
				B = ExecuteOperation(B, Operation, Constant);

			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}

	return Success;
}

const int PixelMathLog = 100;
const int PixelMathLog10 = 101;
const int PixelMathSin = 102;
const int PixelMathCos = 103;
const int PixelMathTan = 104;
const int PixelMathLog2 = 105;
const int PixelMathASin = 106;
const int PixelMathSinh = 107;
const int PixelMathASinh = 108;
const int PixelMathACos = 109;
const int PixelMathCosH = 110;
const int PixelMathACosH = 111;
const int PixelMathATan = 112;
const int PixelMathTanH = 113;
const int PixelMathATanH = 114;

const double SinRange = 1.999902067;
const double CosRange = 1.999960826;
const double TanRange = 246.4884734;
const double NormalizeSinRange = 0.841470985;
const double NormalizedCosRange = 1.999960826;
const double NormalizedTanRange = 246.4884734;
const double NormalizedASinRange = 1.570796327;
const double NormalizedACosRange = 1.570796327;
const double NormalizedATanRange = 0.785398163;
const double LnRange = 5.541263545;
const double NormalizedLnRange = 2.40654018;

/// <summary>
/// Execute the specified operation on the passed value.
/// </summary>
/// <param name="Value">The value the operation is applied to.</param>
/// <param name="Operation">The operation to perform.</param>
/// <param name="NormalizeResults">Normalize the value (between 0 and 255) before returning it.</param>
/// <param name="NormalizeValues">Normalize the value before calculations.</param>
/// <returns>Modified value.</returns>
BYTE ExecuteOperation2(BYTE Value, int Operation, BOOL NormalizeResults, BOOL NormalizeValues)
{
	double DValue = (double)Value;
	if (NormalizeValues)
		DValue = DValue / 255.0;
	double Result = 0.0;

	switch (Operation)
	{
	case PixelMathSin:
		Result = sin(DValue);
		break;

	case PixelMathASin:
		Result = asin(DValue);
		break;

	case PixelMathSinh:
		Result = sinh(DValue);
		break;

	case PixelMathASinh:
		Result = asinh(DValue);
		break;

	case PixelMathCos:
		Result = cos(DValue);
		break;

	case PixelMathACos:
		Result = acos(DValue);
		break;

	case PixelMathCosH:
		Result = cosh(DValue);
		break;

	case PixelMathACosH:
		Result = acosh(DValue);
		break;

	case PixelMathTan:
		Result = tan(DValue);
		break;

	case PixelMathATan:
		Result = atan(DValue);
		break;

	case PixelMathTanH:
		Result = tanh(DValue);
		break;

	case PixelMathATanH:
		Result = atanh(DValue);
		break;

	case PixelMathLog:
		if (DValue == 0.0)
			Result = 0.0;
		else
			Result = log(DValue);
		break;

	case PixelMathLog10:
		if (DValue == 0.0)
			Result = 0.0;
		else
			Result = log10(DValue);
		break;

	case PixelMathLog2:
		if (DValue == 0.0)
			Result = 0.0;
		else
			Result = log2(DValue);
		break;

	default:
		return Value;
	}

	if (NormalizeResults)
	{

	}
}

/// <summary>
/// Perform a mathmematical operation on color channels in <paramref name="SourceBuffer"/>.
/// </summary>
/// <param name="SourceBuffer">The source image buffer.</param>
/// <param name="SourceWidth">Width of the source image.</param>
/// <param name="SourceHeight">Height of the source image.</param>
/// <param name="SourceStride">Stride of the source image.</param>
/// <param name="DestinationBuffer">The destination buffer. Must have same dimensions as the source buffer.</param>
/// <param name="Operation">The operation to perform.</param>
/// <param name="NormalizeResults">Normalize results to something visible.</param>
/// <param name="NormalizeValues">Normalize values before calculations.</param>
/// <param name="ApplyToRed">Apply the operation/constant to the red channel.</param>
/// <param name="ApplyToGreen">Apply the operation/constant to the green channel.</param>
/// <param name="ApplyToBlue">Apply the operation/constant to the blue channel.</param>
int PixelMathOperation(void *SourceBuffer, __int32 SourceWidth, __int32 SourceHeight, __int32 SourceStride,
	void *DestinationBuffer, int Operation, BOOL NormalizeResults, BOOL NormalizeValues,
	BOOL ApplyToAlpha, BOOL ApplyToRed, BOOL ApplyToGreen, BOOL ApplyToBlue)
{
	if (SourceBuffer == NULL)
		return NullPointer;
	if (DestinationBuffer == NULL)
		return NullPointer;
	if (Operation < PixelMathLog || Operation > PixelMathLog10)
		return InvalidOperation;

	BYTE *Source = (BYTE *)SourceBuffer;
	BYTE *Destination = (BYTE *)DestinationBuffer;

	int PixelSize = 4;

	for (int Row = 0; Row < SourceHeight; Row++)
	{
		int RowOffset = Row * SourceStride;
		for (int Column = 0; Column < SourceWidth; Column++)
		{
			int Index = (Column * PixelSize) + RowOffset;
			BYTE A = Source[Index + 3];
			BYTE R = Source[Index + 2];
			BYTE G = Source[Index + 1];
			BYTE B = Source[Index + 0];

			if (ApplyToRed)
				R = ExecuteOperation2(R, Operation, NormalizeResults, NormalizeValues);
			if (ApplyToGreen)
				G = ExecuteOperation2(G, Operation, NormalizeResults, NormalizeValues);
			if (ApplyToBlue)
				B = ExecuteOperation2(B, Operation, NormalizeResults, NormalizeValues);

			Destination[Index + 3] = A;
			Destination[Index + 2] = R;
			Destination[Index + 1] = G;
			Destination[Index + 0] = B;
		}
	}

	return Success;
}