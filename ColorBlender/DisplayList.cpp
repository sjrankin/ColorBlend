#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

//Ignore "typedef ignored" warnings.
#pragma warning (disable: 4091)



/// <summary>
/// Encapsulates one display list instruction.
/// </summary>
typedef struct DisplayListInstruction
{
    /// <summary>
    /// The operation to execute.
    /// </summary>
    __int32 Operand;
    /// <summary>
    /// Used to store intermediate results.
    /// </summary>
    void* Buffer;
    /// <summary>
    /// Width of the buffer.
    /// </summary>
    __int32 BufferWidth;
    /// <summary>
    /// Height of the buffer.
    /// </summary>
    __int32 BufferHeight;
    /// <summary>
    /// Stride of the buffer.
    /// </summary>
    __int32 BufferStride;
    /// <summary>
    /// Caller sets to true if it uses Buffer.
    /// </summary>
    BOOL BufferUsed;
    /// <summary>
    /// Any required rendering completed.
    /// </summary>
    BOOL Rendered;
    /// <summary>
    /// First color.
    /// </summary>
    UINT32 Color1;
    /// <summary>
    /// Second color.
    /// </summary>
    UINT32 Color2;
    /// <summary>
    /// Third color.
    /// </summary>
    UINT32 Color3;
    /// <summary>
    /// Fourth color.
    /// </summary>
    UINT32 Color4;
    /// <summary>
    /// First point horizontal.
    /// </summary>
    __int32 X1;
    /// <summary>
    /// First point vertical.
    /// </summary>
    __int32 Y1;
    /// <summary>
    /// Second point horizontal.
    /// </summary>
    __int32 X2;
    /// <summary>
    /// Second point vertical.
    /// </summary>
    __int32 Y2;
    /// <summary>
    /// Third point horizontal.
    /// </summary>
    __int32 X3;
    /// <summary>
    /// Third point vertical.
    /// </summary>
    __int32 Y3;
    /// <summary>
    /// Fourth point horizontal.
    /// </summary>
    __int32 X4;
    /// <summary>
    /// Fourth point vertical.
    /// </summary>
    __int32 Y4;
    /// <summary>
    /// Left side.
    /// </summary>
    __int32 Left;
    /// <summary>
    /// Top side.
    /// </summary>
    __int32 Top;
    /// <summary>
    /// Right side.
    /// </summary>
    __int32 Right;
    /// <summary>
    /// Bottom side.
    /// </summary>
    __int32 Bottom;
    /// <summary>
    /// Width.
    /// </summary>
    __int32 Width;
    /// <summary>
    /// Height.
    /// </summary>
    __int32 Height;
    /// <summary>
    /// Alpha at the center.
    /// </summary>
    BYTE CenterAlpha;
    /// <summary>
    /// Alpha at the edge.
    /// </summary>
    BYTE EdgeAlpha;
    /// <summary>
    /// Return if operation fails without waiting for the rest of the display list to execute.
    /// </summary>
    BOOL ReturnOnFailure;
};

/// <summary>
/// Execute a display list on the passed buffer.
/// </summary>
/// <remarks>
/// The display list is executed in two passes. The first pass renders individual objects and the second pass
/// composites everything together.
/// </remarks>
/// <param name="Target">Pointer to the buffer where the display list will be executed.</param>
/// <param name="TargetWidth">Width of the buffer where drawing will be done.</param>
/// <param name="TargetHeight">Height of the buffer where drawing will be done.</param>
/// <param name="TargetStride">Stride of the buffer where drawing will be done.</param>
/// <param name="RawDisplayList">Pointer to the display list to execute.</param>
/// <param name="DisplayListCount">Number of entries in the display list to execute.</param>
/// <returns>Value indicating final execution status.</returns>
__int32 ExecuteDisplayList (void *Target, __int32 TargetWidth, __int32 TargetHeight, __int32 TargetStride,
    void* RawDisplayList, __int32 DisplayListCount)
{
    if (Target == NULL)
        return NullPointer;
    if (RawDisplayList == NULL)
        return NullPointer;
    if (DisplayListCount < 1)
        return EmptyDisplayList;

    BYTE* Buffer = (BYTE *) Target;
    DisplayListInstruction* DisplayList = (DisplayListInstruction *) RawDisplayList;

    //First pass.
    for (int i = 0; i < DisplayListCount; i++)
    {
        switch (DisplayList[i].Operand)
        {
            case NOP:
                break;

            case DrawColorBlob:
                //Rendered in this pass.
                break;

            case DrawColorBlock:
                //Nothing done in first pass.
                break;

            case PlotLine:
                //Nothing done in first pass.
                break;

            case DrawBackground:
                //Rendered in this pass.
                break;

            case Debug:
                break;

            case ResizeBuffer:
                break;

            case CopyBuffer:
                //Nothing done in first pass.
                break;

            default:
                if (DisplayList[i].ReturnOnFailure)
                    return UnknownDisplayListOperand;
                break;
        }
    }

    //Second pass.
    for (int i = 0; i < DisplayListCount; i++)
    {
        switch (DisplayList[i].Operand)
        {
            case NOP:
                break;

            case DrawColorBlob:
                break;

            case DrawColorBlock:
                break;

            case PlotLine:
                break;

            case DrawBackground:
                //Nothing done in second pass.
                break;

            case Debug:
                break;

            case ResizeBuffer:
                break;

            case CopyBuffer:
                break;

            default:
                if (DisplayList[i].ReturnOnFailure)
                    return UnknownDisplayListOperand;
                break;
        }
    }

    return Success;
}
