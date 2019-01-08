#ifndef _STRUCTURES_H
#define _STRUCTURES_H

#pragma warning (disable: 4901)

struct GradientColor
{
    UINT32 PointColor;
    double X;
    double Y;
    BOOL AbsolutePoint;
    __int32 FinalX;
    __int32 FinalY;
};

/// <summary>
/// Defines a common structure for rendering of all objects.
/// </summary>
struct CommonObject
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
    __int32 Left;
    /// <summary>
    /// Top side of the object.
    /// </summary>
    __int32 Top;
    /// <summary>
    /// Right side of the object.
    /// </summary>
    __int32 Right;
    /// <summary>
    /// Bottom side of the object.
    /// </summary>
    __int32 Bottom;
};

/// <summary>
/// Display list structure.
/// </summary>
struct DisplayInstructionList2
{
    /// <summary>
    /// The type of object to display.
    /// </summary>
    __int32 Operand;
    /// <summary>
    /// Determines if control is returned to the caller if this particular display list item fails.
    /// </summary>
    BOOL ReturnOnFailure;
    /// <summary>
    /// Pointer to a parameter block specific to Operand.
    /// </summary>
    void* Parameters;
    /// <summary>
    /// Not currently used.
    /// </summary>
    CommonObject* Object;
};

/// <summary>
/// One gradient stop.
/// </summary>
struct GradientStop
{
    /// <summary>
    /// Starting color.
    /// </summary>
    UINT32 StartColor;
    /// <summary>
    /// Ending color.
    /// </summary>
    UINT32 EndColor;
    /// <summary>
    /// Absolute start of the gradient range.
    /// </summary>
    int AbsStart;
    /// <summary>
    /// Absolute end of the gradient range.
    /// </summary>
    int AbsEnd;
    /// <summary>
    /// Absolute gap size.
    /// </summary>
    int AbsGap;
};

struct DrawnObject
{
    BOOL IsValid;
    __int32 ObjectOrder;
    __int32 X1;
    __int32 Y1;
    __int32 X2;
    __int32 Y2;
    __int32 Height;
    __int32 Width;
    UINT32 PrimaryColor;
    BOOL LeftOut;
    BOOL TopOut;
    BOOL RightOut;
    BOOL BottomOut;
    __int32 TargetWidth;
    __int32 TargetHeight;
    __int32 TargetStride;
};

struct ColorBlockParameters
{
    __int32 X1;
    __int32 Y1;
    __int32 Width;
    __int32 Height;
    UINT32 BlockColor;
    UINT32 BGColor;
};

struct ColorBlobParameters
{
    __int32 X1;
    __int32 Y1;
    __int32 Width;
    __int32 Height;
    UINT32 BlobColor;
    BYTE CenterAlpha;
    BYTE EdgeAlpha;
};

struct LinePlotParameters
{
    __int32 X1;
    __int32 Y1;
    __int32 X2;
    __int32 Y2;
    UINT32 LineColor;
    __int32 LineThickness;
    BOOL AntiAlias;
};

struct BufferClearParameters
{
    UINT32 BGColor;
    BOOL DrawGrid;
    UINT32 GridColor;
    __int32 HorizontalFrequency;
    __int32 VerticalFrequency;
};

struct MassAlphaParameters
{
    __int32 AlphaOperation;
    BYTE UniformAlpha;
    BOOL VariableAlpha;
    BOOL InverseAlpha;
    BOOL UseExistingAlpha;
};

struct CopyBufferParameters
{
    BYTE* Source;
    __int32 SourceWidth;
    __int32 SourceHeight;
    __int32 SourceStride;
    __int32 TargetX;
    __int32 TargetY;
};

struct InversionParameters
{
    __int32 Operation;
    BYTE LuminanceThreshold;
    BOOL InvertThreshold;
    BOOL InvertAlpha;
    BYTE InversionChannels;
    BOOL AllowInvertAlpha;
};

/// <summary>
/// Defines a relative `. Assumes values clamped from 0.0 to 1.0.
/// </summary>
struct RelativePointStruct
{
    /// <summary>
    /// Relative horizontal coordinate.
    /// </summary>
    double X;
    /// <summary>
    /// Relative vertical coordinate.
    /// </summary>
    double Y;
};

/// <summary>
/// Determines an absolute point.
/// </summary>
struct AbsolutePointStruct
{
    /// <summary>
    /// Horizontal coordinate.
    /// </summary>
    int X;
    /// <summary>
    /// Vertical coordinate.
    /// </summary>
    int Y;
};

struct ColorType
{
    byte Alpha;
    byte Red;
    byte Green;
    byte Blue;
};

/// <summary>
/// Defines a pure color type.
/// </summary>
struct PureColorType
{
    /// <summary>
    /// Horizontal coordinate.
    /// </summary>
    int X;
    /// <summary>
    /// Vertical coordinate.
    /// </summary>
    int Y;
    /// <summary>
    /// The alpha channel value.
    /// </summary>
    BYTE Alpha;
    /// <summary>
    /// The red channel value.
    /// </summary>
    BYTE Red;
    /// <summary>
    /// The green channel value.
    /// </summary>
    BYTE Green;
    /// <summary>
    /// The blue channel value.
    /// </summary>
    BYTE Blue;
    /// <summary>
    /// The value of the hypotenuse used to calculate color percents. Ignored if UseRadius is true.
    /// </summary>
    double Hypotenuse;
    /// <summary>
    /// The radius that determines the percent value for the colors. Used only if UseRadius is true.
    /// </summary>
    double Radius;
    /// <summary>
    /// Starting alpha value (e.g. the alpha value at X,Y). Ignored if UseAlpha is false.
    /// </summary>
    double AlphaStart;
    /// <summary>
    /// Ending alpha value (at 100% of either the Hypotenuse or Radius). Ignored if UseAlpha is false.
    /// </summary>
    double AlphaEnd;
    /// <summary>
    /// Determines if the Radius value is used.
    /// </summary>
    BOOL UseRadius;
    /// <summary>
    /// Determines if alpha values are calculated.
    /// </summary>
    BOOL UseAlpha;
    /// <summary>
    /// Determines if horizontal indicators are drawn.
    /// </summary>
    BOOL DrawHorizontalIndicator;
    /// <summary>
    /// Determines if vertical indicators are drawn.
    /// </summary>
    BOOL DrawVerticalIndicator;
    /// <summary>
    /// Determines if the point indicator is drawn.
    /// </summary>
    BOOL DrawPointIndicator;
};

/// <summary>
/// Defines a point from which vertical and horizontal lines will be drawn.
/// </summary>
struct LineDefinitionStruct
{
    /// <summary>
    /// Horizontal coordinate.
    /// </summary>
    int X;
    /// <summary>
    /// Vertical coordinate.
    /// </summary>
    int Y;
    /// <summary>
    /// The alpha channel value.
    /// </summary>
    BYTE Alpha;
    /// <summary>
    /// The red channel value.
    /// </summary>
    BYTE Red;
    /// <summary>
    /// The green channel value.
    /// </summary>
    BYTE Green;
    /// <summary>
    /// The blue channel value.
    /// </summary>
    BYTE Blue;
    /// <summary>
    /// Determines if the point indicator is drawn.
    /// </summary>
    bool DrawPointIndicator;
    /// <summary>
    /// Determines if vertical lines are drawn.
    /// </summary>
    bool DrawVerticalLines;
    /// <summary>
    /// Determines if horiztonal lines are drawn.
    /// </summary>
    bool DrawHorizontalLines;
};

/// <summary>
/// Defines a color value.
/// </summary>
struct PureColorStruct
{
    /// <summary>
    /// Determines if the alpha channel should be used.
    /// </summary>
    bool UseAlpha;
    /// <summary>
    /// The alpha channel value.
    /// </summary>
    BYTE Alpha;
    /// <summary>
    /// The red channel value.
    /// </summary>
    BYTE Red;
    /// <summary>
    /// The green channel value.
    /// </summary>
    BYTE Green;
    /// <summary>
    /// The blue channel value.
    /// </summary>
    BYTE Blue;
};

struct GenericImageNode
{
    void *TheBits;
};

struct PlaneSetStruct
{
    void *Plane;
    __int32 PlaneSize;
    __int32 Width;
    __int32 Height;
    __int32 Stride;
    __int32 CenterX;
    __int32 CenterY;
    __int32 Left;
    __int32 Top;
    __int32 Right;
    __int32 Bottom;
    void *TheBits;
};

/// <summary>
/// Defines a region in an image or an entire image depending on usage. Not all fields are necessarily used.
/// </summary>
struct ImageDefintionStruct
{
    /// <summary>
    /// Pointer to the bits in the region.
    /// </summary>
    void *Buffer;
    /// <summary>
    /// Left of the upper-left corner.
    /// </summary>
    UINT32 X;
    /// <summary>
    /// Upper of the upper-left corner.
    /// </summary>
    UINT32 Y;
    /// <summary>
    /// Width of the region.
    /// </summary>
    UINT32 Width;
    /// <summary>
    /// Height of the region.
    /// </summary>
    UINT32 Height;
    /// <summary>
    /// Stride of the region.
    /// </summary>
    UINT32 Stride;
};

/// <summary>
/// Defines a region.
/// </summary>
struct RegionStruct
{
    /// <summary>
    /// The top of the region.
    /// </summary>
    int Top;
    /// <summary>
    /// The left side of the region.
    /// </summary>
    int Left;
    /// <summary>
    /// The bottom of the region.
    /// </summary>
    int Bottom;
    /// <summary>
    /// The right side of the region.
    /// </summary>
    int Right;
};

struct ColorBlock
{
    __int32 Left;
    __int32 Top;
    __int32 Width;
    __int32 Height;
    __int32 Right;
    __int32 Bottom;
    UINT32 BlockColor;
    BYTE A;
    BYTE R;
    BYTE G;
    BYTE B;
};

struct BlobBlock
{
    __int32 Left;
    __int32 Top;
    __int32 Right;
    __int32 Bottom;
    __int32 Width;
    __int32 Height;
    BYTE A;
    BYTE R;
    BYTE G;
    BYTE B;
    BYTE CenterAlpha;
    BYTE EdgeAlpha;
};

struct FrequencyActionBlock
{
    int Action;
    double NewAlpha;
    double NewLuminance;
    UINT32 NewColor;
    int HorizontalFrequency;
    int VerticalFrequency;
    BOOL IncludeAlpha;
    int ColorAlphaAction;
};

#endif