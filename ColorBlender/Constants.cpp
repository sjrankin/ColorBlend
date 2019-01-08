#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
/*
#include <stack>
*/
#include "ColorBlender.h"

/// <summary>
/// Return a string description for the passed error/return code.
/// </summary>
/// <param name="ErrorConstant">The error/return code the description is desired for.</param>
/// <returns>Description of the error/return code. If the code is unknown, an empty string is returned.</returns>
BSTR ErrorConstantToString(__int32 ErrorConstant)
{
    if (ErrorConstant < NotSet)
    {
        return ::SysAllocString((const OLECHAR*)L"");
    }

    switch (ErrorConstant)
    {
    case NotSet:
        return ::SysAllocString((const OLECHAR*)L"Error code not set.");

    case Success:
        return ::SysAllocString((const OLECHAR*)L"Operation was successful.");

    case Error:
        return ::SysAllocString((const OLECHAR*)L"General, unspecified error.");

    case BadIndex:
        return ::SysAllocString((const OLECHAR*)L"Index supplied to function invalid.");

    case NullPointer:
        return ::SysAllocString((const OLECHAR*)L"Pointer supplied to function is null.");

    case NegativeIndex:
        return ::SysAllocString((const OLECHAR*)L"Index is negative.");

    case BadSecondaryIndex:
        return ::SysAllocString((const OLECHAR*)L"Secondary index is invalid.");

    case IndexOutOfRange:
        return ::SysAllocString((const OLECHAR*)L"Supplied or computed index is out of range.");

    case DisplayListOperationFailed:
        return ::SysAllocString((const OLECHAR*)L"Display list operation failed.");

    case EmptyDisplayList:
        return ::SysAllocString((const OLECHAR*)L"Display list is empty when non-emptly list expected.");

    case VirtualEmptyDisplayList:
        return ::SysAllocString((const OLECHAR*)L"Virtual empty display list.");

    case UnknownDisplayListOperand:
        return ::SysAllocString((const OLECHAR*)L"Display list contained unknown operand.");

    case InvalidOperation:
        return ::SysAllocString((const OLECHAR*)L"Operation was invalid.");

    case NoActionTaken:
        return ::SysAllocString((const OLECHAR*)L"No action taken because input did not result in anything to do.");

    case NormalNonNormal:
        return ::SysAllocString((const OLECHAR*)L"A normal value was not normal.");

    case NotImplemented:
        return ::SysAllocString((const OLECHAR*)L"Requested action is not implemented.");

    case ImageMismatch:
        return ::SysAllocString((const OLECHAR*)L"Images do not match.");

    case ImagesMatch:
        return ::SysAllocString((const OLECHAR*)L"Images match.");

    case HeaderNotFound:
        return ::SysAllocString((const OLECHAR*)L"Expected header not found.");

    case HeaderBadDataType:
        return ::SysAllocString((const OLECHAR*)L"Invalid data type found in header.");

    case ComputedIndexOutOfRange:
        return ::SysAllocString((const OLECHAR*)L"Computed index out of valid range.");

    case NoPixelsSelected:
        return ::SysAllocString((const OLECHAR*)L"No pixels selected with input range.");

    case InvalidRegion:
        return ::SysAllocString((const OLECHAR*)L"Supplied or computed region is invalid.");

    case UnknownErrorCode:
        return ::SysAllocString((const OLECHAR*)L"Supplied error code undefined.");

    case ErrorStackIsEmpty:
        return ::SysAllocString((const OLECHAR *)L"The error stack is empty - cannot pop.");

    case ErrorStackIsFull:
        return ::SysAllocString((const OLECHAR *)L"The error stack is full - cannot push.");

    case ValueTooSmall:
        return ::SysAllocString((const OLECHAR *)L"Value for operation too small.");

    case ValueTooBig:
        return ::SysAllocString((const OLECHAR *)L"Value for operation too bigs.");

    case FailedParameterValidation:
        return ::SysAllocString((const OLECHAR *)L"Parameter validation failed - check error stack.");

	case DimensionalMismatch:
		return ::SysAllocString((const OLECHAR *)L"Image dimensions do not match in context.");

	case BadRotation:
		return ::SysAllocString((const OLECHAR *)L"Bad rotational value.");

    default:
        return ::SysAllocString((const OLECHAR*)L"");
    }

    return ::SysAllocString((const OLECHAR*)L"");
}

