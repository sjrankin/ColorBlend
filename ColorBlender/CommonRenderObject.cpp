#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"


const __int32 ObjectMerge = 0;
const __int32 ObjectBlt = 1;
const __int32 IgnoreObject = 2;

/// <summary>
/// Defines a common structure for rendering of all objects.
/// </summary>
typedef struct CommonObject
{
    /// <summary>
    /// Tells the renderer what action to take with this object.
    /// </summary>
    __int32 ObjectAction;
    /// <summary>
    /// The buffer that will be blended/rendered.
    /// </summary>
    BYTE* ObjectBuffer;
    /// <summary>
    /// The width of the buffer.
    /// </summary>
    __int32 ObjectWidth;
    /// <summary>
    /// The height of the buffer.
    /// </summary>
    __int32 ObjectHeight;
    /// <summary>
    /// The stride of the buffer.
    /// </summary>
    __int32 ObjectStride;
    /// <summary>
    /// Left side of the object.
    /// </summary>
    __int32 X1;
    /// <summary>
    /// Top side of the object.
    /// </summary>
    __int32 Y1;
};

