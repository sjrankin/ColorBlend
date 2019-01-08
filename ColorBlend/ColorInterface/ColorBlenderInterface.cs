using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace ColorBlend
{
    public partial class ColorBlenderInterface
    {
        public ColorBlenderInterface()
        {
            DisplayList = new List<DisplayListInstruction2>();
            ResetReturnCode();
            CreateErrorList();
        }

        public ReturnCode LastReturnCode { get; private set; }

        public void ResetReturnCode()
        {
            LastReturnCode = ReturnCode.NotSet;
        }

        public enum MultiPixelOperations : int
        {
            ArithemticMean = 0,
            ArithmeticBrightest = 1,
            ArithmeticDarkest = 2,
            ArithmeticMedian = 3,
            ArithmeticSum = 4,
            ClosestLuminance = 5,
            FarthestLuminance = 6,
            GreatestAlpha = 7,
            LeastAlpha = 8,
            MeanAlpha = 9,
            MedianAlpha = 10,
            ClosestAlpha = 11,
            FarthestAlpha = 12,
            ArithmeticAnd = 13,
            ArithmeticOr = 14,
            ArithmeticXor = 15,
        }

        /// <summary>
        /// Contains extra data for mass image arithmetic operations if needed.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct MassArithmeticExtra
        {
            /// <summary>
            /// Luminance value. Usage is context sensitive.
            /// </summary>
            public double Luminance;
            /// <summary>
            /// Color channel value. Usage is context sensitive.
            /// </summary>
            public byte Channel;
        }

        [Flags]
        public enum ColorChannels : byte
        {
            Alpha = 1,
            Red = 2,
            Green = 4,
            Blue = 8
        }

        /// <summary>
        /// Base gradient stop structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct CBGradientStop
        {
            /// <summary>
            /// Gradient stop color.
            /// </summary>
            public UInt32 PackedColor;
            /// <summary>
            /// Gradient stop location (percent between start and stop).
            /// </summary>
            public double Location;
            /// <summary>
            /// Absolute starting location of the gradient.
            /// </summary>
            public int GStart;
            /// <summary>
            /// Absolute ending location of the gradient.
            /// </summary>
            public int GEnd;
        }

        struct CBGradientStop2
        {
            /// <summary>
            /// Starting color.
            /// </summary>
            public UInt32 StartColor;
            /// <summary>
            /// Ending color.
            /// </summary>
            public UInt32 EndColor;
            /// <summary>
            /// Absolute start of the gradient range.
            /// </summary>
            public int AbsStart;
            /// <summary>
            /// Absolute end of the gradient range.
            /// </summary>
            public int AbsEnd;
            /// <summary>
            ///  Absolute gap size.
            /// </summary>
            public int AbsGap;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct GradientColor
        {
            /// <summary>
            /// The color of the gradient point in packed format: ARGB.
            /// </summary>
            UInt32 PackedPointColor;
            /// <summary>
            /// Horizontal location of the point.
            /// </summary>
            double X;
            /// <summary>
            /// Vertical location of the point.
            /// </summary>
            double Y;
            /// <summary>
            /// Determines if X and Y are absolute points or relative points (relative to the drawing surface).
            /// </summary>
            bool AbsolutePoint;
            /// <summary>
            /// The point used for plotting colors. Calculated by the plot function.
            /// </summary>
            Int32 FinalX;
            /// <summary>
            /// The point used for plotting colors. Calculated by the plot function.
            /// </summary>
            Int32 FinalY;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DrawnObject
        {
            bool IsValid;
            int ObjectOrder;
            int X1;
            int Y1;
            int X2;
            int Y2;
            int Height;
            int Width;
            UInt32 PrimaryColor;
            bool LeftOut;
            bool TopOut;
            bool RightOut;
            bool BottomOut;
            int TargetWidth;
            int TargetHeight;
            int TargetStride;
        }

        /// <summary>
        /// Pure color point structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PureColorType
        {
            /// <summary>
            /// The X coordinate.
            /// </summary>
            public int X;
            /// <summary>
            /// The Y coordinate.
            /// </summary>
            public int Y;
            /// <summary>
            /// Point's alpha value.
            /// </summary>
            public byte Alpha;
            /// <summary>
            /// Point's red value.
            /// </summary>
            public byte Red;
            /// <summary>
            /// Point's green value.
            /// </summary>
            public byte Green;
            /// <summary>
            /// Point's blue value.
            /// </summary>
            public byte Blue;
            /// <summary>
            /// Hypotenuse used to calculate color percentage.
            /// </summary>
            public double Hypotenuse;
            /// <summary>
            /// Radius used to calculate color percentage (when UseRadius is true).
            /// </summary>
            public double Radius;
            /// <summary>
            /// Alpha level at the pure color's origin.
            /// </summary>
            public double AlphaStart;
            /// <summary>
            /// Alpha level at the end of the percentage (either Hypotenuse or Radius).
            /// </summary>
            public double AlphaEnd;
            /// <summary>
            /// Determines if Radius or Hypotenuse is used for color percentage calculations.
            /// </summary>
            public bool UseRadius;
            /// <summary>
            /// Determines if alpha values are used.
            /// </summary>
            public bool UseAlpha;
            /// <summary>
            /// Determines if horizontal indicators are drawn.
            /// </summary>
            public bool DrawHorizontalIndicators;
            /// <summary>
            /// Determines if vertical indicators are drawn.
            /// </summary>
            public bool DrawVerticalIndicators;
            /// <summary>
            /// Determines if point indicators are drawn.
            /// </summary>
            public bool DrawPointIndicator;
            /// <summary>
            /// Upper-left's upper part.
            /// </summary>
            public int Top;
            /// <summary>
            /// Upper-left's left part.
            /// </summary>
            public int Left;
            /// <summary>
            /// Width of the blob.
            /// </summary>
            public int Width;
            /// <summary>
            /// Height of the blob.
            /// </summary>
            public int Height;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RelativePointStruct
        {
            public double X;
            public double Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct AbsolutePointStruct
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PureColorStruct
        {
            public bool UseAlpha;
            public byte Alpha;
            public byte Red;
            public byte Green;
            public byte Blue;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LineDefintionStruct
        {
            public int X;
            public int Y;
            public byte Alpha;
            public byte Red;
            public byte Green;
            public byte Blue;
            public bool DrawPointIndicator;
            public bool DrawVerticalLines;
            public bool DrawHoriztonalLines;
        }

        /// <summary>
        /// One octree node.
        /// </summary>
        public struct OctreeNode
        {
            /// <summary>
            /// The color of the node.
            /// </summary>
            public UInt32 PackedColor;
            /// <summary>
            /// Color count.
            /// </summary>
            public UInt32 Count;
        }

        /// <summary>
        /// Defines one plane.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct LLPlaneSet
        {
            /// <summary>
            /// Plane data.
            /// </summary>
            public IntPtr PlaneBits;
            /// <summary>
            /// Size of the plane in bytes.
            /// </summary>
            public Int32 PlaneSize;
            /// <summary>
            /// Width of the plane.
            /// </summary>
            public Int32 Width;
            /// <summary>
            /// Height of the plane.
            /// </summary>
            public Int32 Height;
            /// <summary>
            /// Stride of the buffer.
            /// </summary>
            public Int32 Stride;
            /// <summary>
            /// Location of the center of the plane in the final merged plane.
            /// </summary>
            public Int32 CenterX;
            /// <summary>
            /// Location of the center of the plane in the final merged plane.
            /// </summary>
            public Int32 CenterY;
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
            public IntPtr TheBits;
        }

        public struct GenericImageNode
        {
            public IntPtr TheBits;
        }

        public struct PlaneSet
        {
            public byte[] PlaneBits;
            public Int32 Width;
            public Int32 Height;
            public Int32 Stride;
            public Int32 CenterX;
            public Int32 CenterY;
            public Int32 Radius;
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RegionStruct
        {
            public int Top;
            public int Left;
            public int Bottom;
            public int Right;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ColorBlock
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Width;
            public Int32 Height;
            public Int32 Right;
            public Int32 Bottom;
            public UInt32 BlockColor;
            public byte A;
            public byte R;
            public byte G;
            public byte B;
        };

        public struct GradientPoint
        {
            public Int32 X;
            public Int32 Y;
            public Int32 Width;
            public Int32 Height;
            public Int32 Stride;
            public UInt32 PackedGradientColor;
            public byte[] Region;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ThresholdListType
        {
            public double LowThreshold;
            public double HighThreshold;
            public UInt32 PackedColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DisplayListInstruction
        {
            public int Operand;
            unsafe public void* Source;
            public int SourceWidth;
            public int SourceHeight;
            public int SourceStride;
            public bool BufferUsed;
            public bool Rendered;
            public UInt32 Color1;
            public UInt32 Color2;
            public UInt32 Color3;
            public UInt32 Color4;
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
            public int X3;
            public int Y3;
            public int X4;
            public int Y4;
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public int Right;
            public int Bottom;
            public byte CenterAlpha;
            public byte EdgeAlpha;
            public bool ReturnOnFailure;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DisplayListInstruction2
        {
            public DisplayListInstructions Operand;
            public bool ReturnOnFailure;
            unsafe public void* Parameters;
            unsafe public void* Internal;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ColorBlobParameters
        {
            public int X1;
            public int Y1;
            public int Width;
            public int Height;
            public UInt32 BlobColor;
            public byte CenterAlpha;
            public byte EdgeAlpha;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ColorBlockParameters
        {
            public int X1;
            public int Y1;
            public int Width;
            public int Height;
            public UInt32 BlockColor;
            public UInt32 BGColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LinePlotParameters
        {
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
            public UInt32 LineColor;
            public int LineThickness;
            public bool AntiAlias;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BufferClearParameters
        {
            public UInt32 BGColor;
            public bool DrawGrid;
            public UInt32 GridColor;
            public Int32 HorizontalFrequency;
            public Int32 VerticalFrequency;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MassAlphaParameters
        {
            public byte UniformAlpha;
            public bool VariableAlpha;
            public bool InverseAlpha;
            public bool UseExistingAlpha;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CopyBufferParameters
        {
            unsafe public void* Source;
            public Int32 SourceWidth;
            public Int32 SourceHeight;
            public Int32 SourceStride;
            public Int32 TargetX;
            public Int32 TargetY;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ResizeBufferParameters
        {
            unsafe public void* Source;
            public Int32 SourceWidth;
            public Int32 SourceHeight;
            public Int32 SourceStride;
            unsafe public void* Destination;
            public Int32 DestinationWidth;
            public Int32 DestinationHeight;
            public Int32 DestinationStride;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct InversionParameters
        {
            public InversionOperations Operation;
            public byte LuminanceThreshold;
            public bool InvertThreshold;
            public bool InvertAlpha;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CompositorObject
        {
            /// <summary>
            /// Tells the renderer what action to take with this object.
            /// </summary>
            public DisplayListInstructions ObjectAction;
            /// <summary>
            /// The buffer that will be blended/rendered.
            /// </summary>
            public unsafe void* ObjectBuffer;
            /// <summary>
            /// The width of the buffer.
            /// </summary>
            public Int32 ObjectWidth;
            /// <summary>
            /// The height of the buffer.
            /// </summary>
            public Int32 ObjectHeight;
            /// <summary>
            /// The stride of the buffer.
            /// </summary>
            public Int32 ObjectStride;
            /// <summary>
            /// Left side of the object.
            /// </summary>
            public Int32 Left;
            /// <summary>
            /// Top side of the object.
            /// </summary>
            public Int32 Top;
            /// <summary>
            /// Right side of the object.
            /// </summary>
            public Int32 Right;
            /// <summary>
            /// Bottom side of the object.
            /// </summary>
            public Int32 Bottom;
        }

        /// <summary>
        /// Valid display list instructions.
        /// </summary>
        public enum DisplayListInstructions : int
        {
            /// <summary>
            /// Do nothing.
            /// </summary>
            NOP = 0,
            /// <summary>
            /// Draw a color blob.
            /// </summary>
            DrawColorBlob = 1,
            /// <summary>
            /// Draw a color block.
            /// </summary>
            DrawColorBlock = 2,
            /// <summary>
            /// Draw a line.
            /// </summary>
            PlotLine = 3,
            /// <summary>
            /// Draw the background.
            /// </summary>
            DrawBackground = 4,
            /// <summary>
            /// Debug.
            /// </summary>
            Debug = 5,
            /// <summary>
            /// Resize the buffer.
            /// </summary>
            ResizeBuffer = 6,
            /// <summary>
            /// Copy alpha.
            /// </summary>
            CopyBuffer = 7,
            /// <summary>
            /// Mass alpha.
            /// </summary>
            MassAlpha = 8,
            /// <summary>
            /// Invert the buffer.
            /// </summary>
            InvertBuffer = 9,
        }

        public enum InversionOperations : int
        {
            SimpleInvertOperation = 0,
            VariableInvertOperation = 1,
            ChannelInversionOperation = 2,
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ColorLuminance@12", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe double ColorLuminance(byte R, byte G, byte B);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawAnyLine@56", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool DrawAnyLine(void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            Int32 X1, Int32 Y1, Int32 X2, Int32 Y2, byte A, byte R, byte G, byte B,
            bool AntiAlias, Int32 LineThickness);

        [DllImport("ColorBlender.dll", EntryPoint = "_BufferInverter@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BufferInverter(void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            int InversionOperation, double LuminanceThreshold, bool InvertThreshold, bool AllowInvertAlpha, byte InversionChannels);

        [DllImport("ColorBlender.dll", EntryPoint = "_BufferInverter2@52", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BufferInverter2(void* Source, int Width, int Height, int Stride, void* Destination,
            int InversionOperation, double LuminanceThreshold, bool InvertThreshold,
            bool InvertAlpha, bool InvertRed, bool InvertGreen, bool InvertBlue);

        [DllImport("ColorBlender.dll", EntryPoint = "_ConvertBlockToDouble@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ConvertBlockToDouble(void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            void* Destination);

        [DllImport("ColorBlender.dll", EntryPoint = "_ExecuteDisplayList@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ExecuteDisplayList(void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            void* RawDisplayList, Int32 DisplayListCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderDisplayList@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RenderDisplayList(void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            void* DisplayList, Int32 DisplayListEntries);

        [DllImport("ColorBlender.dll", EntryPoint = "_FillBufferWithBuffer@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int FillBufferWithBuffer(void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            UInt32 PackedEmptyColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_ChannelShift@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ChannelShift(void* Target, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride, int ShiftBy);

        [DllImport("ColorBlender.dll", EntryPoint = "_ChannelSwap@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ChannelSwap(void* Target, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
             void* SourceIndices, void* DestIndices, int IndexCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_ChannelSwap4@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ChannelSwap4(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, int SwapOrder,
            double LuminanceThreshold, int Conditional);

        [DllImport("ColorBlender.dll", EntryPoint = "_ChannelSwap3@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ChannelSwap3(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, int SwapOrder);

        [DllImport("ColorBlender.dll", EntryPoint = "_SolarizeImage@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SolarizeImage(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, double Threshold, bool Invert);

        [DllImport("ColorBlender.dll", EntryPoint = "_ColorThreshold0@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ColorThreshold0(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
            double Threshold, UInt32 PackedColor, bool InvertThreshold);

        [DllImport("ColorBlender.dll", EntryPoint = "_ColorThreshold@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ColorThreshold(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
           double Threshold, UInt32 PackedLowColor, UInt32 PackedHighColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_ColorThreshold2@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ColorThreshold2(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
           double LowThreshold, UInt32 PackedLowColor, double HighThreshold, UInt32 PackedHighColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_ColorThreshold3@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ColorThreshold3(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
              void* ThresholdList, int ListCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_BufferGrayscale@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BufferGrayscale(void* Buffer, Int32 Width, Int32 Height, Int32 Stride,
                 void* Destination, int GrayscaleType);

        [DllImport("ColorBlender.dll", EntryPoint = "_BufferGrayscaleRegion@48", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BufferGrayscaleRegion(void* Buffer, Int32 Width, Int32 Height, Int32 Stride,
               void* Destination, int GrayscaleType, Int32 Left, Int32 Top, Int32 Right, Int32 Bottom,
               bool CopyOutOfRegion, UInt32 PackedOut);

        [DllImport("ColorBlender.dll", EntryPoint = "_GrayLevels@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int GrayLevels(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, int LevelCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_ColorLevels@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ColorLevels(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, int LevelCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_SortChannels2@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SortChannels2(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
            bool UseLuminance, double LuminanceThreshold);

        [DllImport("ColorBlender.dll", EntryPoint = "_SortChannels@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SortChannels(void* Source, Int32 Width, Int32 Height, Int32 Stride,
               void* Destination, int SortHow, bool StoreSortHowAsAlpha, bool InvertAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_InvertImage@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int InvertImage(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool IncludeAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_SepiaTone@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SepiaTone(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        [DllImport("ColorBlender.dll", EntryPoint = "_AutoContrast@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AutoContrast(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, double Contrast);

        [DllImport("ColorBlender.dll", EntryPoint = "_AutoSaturate@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AutoSaturate(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, Int32 SaturationValue);

        [DllImport("ColorBlender.dll", EntryPoint = "_MeanImageColor@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int MeanImageColor(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);



        [DllImport("ColorBlender.dll", EntryPoint = "_BufferInverter3@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BufferInverter3(void* Source, int Width, int Height, int Stride, void* Destination, bool InvertAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_BufferInverter4@68", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BufferInverter4(void* Source, int Width, int Height, int Stride, void* Destination, bool InvertAlpha, bool InvertRed,
             bool InvertGreen, bool InvertBlue,
             bool UseAlphaThreshold, byte AlphaThreshold,
             bool UseRedThreshold, byte RedThreshold,
             bool UseGreenThreshold, byte GreenThreshold,
             bool UseBlueThreshold, byte BlueThreshold);

        [DllImport("ColorBlender.dll", EntryPoint = "_SelectRGBChannels@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SelectRGBChannels(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool SelectRed,
             bool SelectGreen, bool SelectBlue, bool AsGray);

        public enum LogicalOperations : int
        {
            LogicalAnd = 0,
            LogicalOr = 1,
            LogicalXor = 2
        }

        public enum ReturnCode : int
        {
            NotSet = -1,
            Success = 0,
            Error = 1,
            BadIndex = 2,
            NullPointer = 3,
            NegativeIndex = 4,
            BadSecondaryIndex = 5,
            IndexOutOfRange = 6,
            DisplayListOperationFailed = 7,
            EmptyDisplayList = 8,
            VirtualEmptyDisplayList = 9,
            UnknownDisplayListOperand = 10,
            InvalidOperation = 11,
            NoActionTaken = 12,
            NormalNonNormal = 13,
            NotImplemented = 14,
            ImageMismatch = 15,
            ImagesMatch = 16,
            HeaderNotFound = 17,
            HeaderBadDataType = 18,
        }

        public bool ImageSelectRGBChannels(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            bool SelectRed, bool SelectGreen, bool SelectBlue, bool AsGray)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SelectRGBChannels(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(),
                    SelectRed, SelectGreen, SelectBlue, AsGray);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }


        [DllImport("ColorBlender.dll", EntryPoint = "_SelectHSLChannels@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SelectHSLChannels(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
            bool SelectHue, bool SelectSaturation, bool SelectLuminance, bool AsGray, int ChannelOrder);

        public bool ImageSelectHSLChannels(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
                bool SelectHue, bool SelectSaturation, bool SelectLuminance, bool AsGray, string ChannelOrder)
        {
            if (string.IsNullOrEmpty(ChannelOrder))
                ChannelOrder = "HSL";
            int FinalChannelOrder = 0;
            switch (ChannelOrder.ToUpper())
            {
                case "HSL":
                    FinalChannelOrder = 0;
                    break;

                case "HLS":
                    FinalChannelOrder = 1;
                    break;

                case "SHL":
                    FinalChannelOrder = 2;
                    break;

                case "SLH":
                    FinalChannelOrder = 3;
                    break;

                case "LHS":
                    FinalChannelOrder = 4;
                    break;

                case "LSH":
                    FinalChannelOrder = 5;
                    break;

                default:
                    FinalChannelOrder = 0;
                    break;
            }
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SelectHSLChannels(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(),
                    SelectHue, SelectSaturation, SelectLuminance, AsGray, FinalChannelOrder);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageInvert4(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, bool InvertAlpha,
            bool InvertRed, bool InvertGreen, bool InvertBlue, bool UseAlphaThreshold, byte AlphaThreshold, bool UseRedThreshold, byte RedThreshold,
            bool UseGreenThreshold, byte GreenThreshold, bool UseBlueThreshold, byte BlueThreshold)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)BufferInverter4(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(),
                        InvertAlpha, InvertRed, InvertGreen, InvertBlue,
                        UseAlphaThreshold, AlphaThreshold,
                        UseRedThreshold, RedThreshold,
                        UseGreenThreshold, GreenThreshold,
                        UseBlueThreshold, BlueThreshold);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageInvert3(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, bool InvertAlpha)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)BufferInverter3(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), InvertAlpha);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageMeanImageColor(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)MeanImageColor(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_MeanImageColor2@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int MeanImageColor2(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, 
            bool IgnoreAlpha, UInt32 *MeanColor);

        public bool ImageMeanImageColor2(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, out Color MeanColorOfImage)
        {
            MeanColorOfImage = Colors.Transparent;
            ReturnCode OpReturn = ReturnCode.NotSet;
            UInt32 CalculatedMean = 0;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)MeanImageColor2(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), true, &CalculatedMean);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            MeanColorOfImage = CalculatedMean.FromARGB();
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageAutoSaturate(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)AutoSaturate(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), 100);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageAutoContrast(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)AutoContrast(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), 0.5);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageSepiaTone(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                SepiaTone(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageChannelSort(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height,
            int Stride, bool UseLuminance, double LuminanceThreshold)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SortChannels2(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), UseLuminance, LuminanceThreshold);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public enum ConditionalChannelSwaps : int
        {
            SwapIf_Unconditional = 0,
            SwapIf_LuminanceGTE = 1,
            SwapIf_LuminanceLTE = 2,
            SwapIf_R_GTE_GB = 3,
            SwapIf_G_GTE_RB = 4,
            SwapIf_B_GTE_RG = 5,
            SwapIf_NotSet = 6,
        }

        public bool ImageChannelSwapConditionally(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, int SwapOrder,
            double LuminanceThreshold, ConditionalChannelSwaps Conditional)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ChannelSwap4(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), SwapOrder, LuminanceThreshold, (int)Conditional);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageChannelSwap(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, int SwapOrder)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ChannelSwap3(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), SwapOrder);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageColorLevels(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            int Levels)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ColorLevels(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), Levels);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageGrayscaleLevels(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            int Levels)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)GrayLevels(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), Levels);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageGrayscale(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            int GrayscaleOperation)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)BufferGrayscale(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), GrayscaleOperation);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageThreshold0(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            double ColorThresholdValue, Color ThresholdColor, bool Invert)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                UInt32 PackedColor = ThresholdColor.ToARGB();
                OpReturn = (ReturnCode)ColorThreshold0(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), ColorThresholdValue, PackedColor, Invert);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageThreshold3(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            List<ThresholdListType> Thresholds)
        {
            if (Thresholds == null)
                return false;
            if (Thresholds.Count < 1)
                return false;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                ThresholdListType[] LocalT = new ThresholdListType[Thresholds.Count];
                for (int i = 0; i < Thresholds.Count; i++)
                {
                    LocalT[i].LowThreshold = Thresholds[i].LowThreshold;
                    LocalT[i].HighThreshold = Thresholds[i].HighThreshold;
                    LocalT[i].PackedColor = Thresholds[i].PackedColor.ToARGB();
                }
                fixed (void* TNodes = LocalT)
                {
                    OpReturn = (ReturnCode)ColorThreshold3(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                            DestinationBuffer.BackBuffer.ToPointer(), TNodes, Thresholds.Count);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageThreshold(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            double Threshold1, Nullable<double> Threshold2, Color LowColor, Color HighColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                UInt32 PackedLowColor = LowColor.ToARGB();
                UInt32 PackedHighColor = HighColor.ToARGB();
                if (Threshold2.HasValue)
                {
                    OpReturn = (ReturnCode)ColorThreshold2(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), Threshold1, PackedLowColor, Threshold2.Value, PackedHighColor);
                }
                else
                {
                    OpReturn = (ReturnCode)ColorThreshold(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), Threshold1, PackedLowColor, PackedHighColor);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool Solarize(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            double Threshold, bool InvertThreshold)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SolarizeImage(SourceBuffer.BackBuffer.ToPointer(), Width, Height, Stride,
                    DestinationBuffer.BackBuffer.ToPointer(), Threshold, InvertThreshold);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }


        /// <summary>
        /// Invert the pixels of the specified block.
        /// </summary>
        /// <param name="Target">The block whose pixels will be inverted.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="Operation">The type of inversion.</param>
        /// <param name="LuminanceThreshold">Threshold for luminance inversion.</param>
        /// <param name="InvertThreshold">Determines if <paramref name="LuminanceThreshold"/> is inverted.</param>
        /// <param name="InvertAlpha">Determines if alpha channels are inverted.</param>
        /// <param name="InversionChannels">Determines the color channels to invert.</param>
        /// <returns></returns>
        public bool InvertBlock(ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride, InversionOperations Operation,
            byte LuminanceThreshold, bool InvertThreshold, bool InvertAlpha, byte InversionChannels)
        {
            ReturnCode InvertReturn = ReturnCode.NotSet;
            unsafe
            {
                fixed (void* Buffer = Target)
                {
                    InvertReturn = (ReturnCode)BufferInverter(Buffer, TargetWidth, TargetHeight, TargetStride, (int)Operation,
                        LuminanceThreshold, InvertThreshold, InvertAlpha, InversionChannels);
                }
            }
            return InvertReturn == ReturnCode.Success;
        }

        public bool InvertBlock2(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
               InversionOperations Operation, double LuminanceThreshold, bool InvertThreshold, bool InvertAlpha, bool InvertRed,
               bool InvertGreen, bool InvertBlue)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)BufferInverter2(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), (int)Operation,
                        LuminanceThreshold, InvertThreshold, InvertAlpha, InvertRed, InvertGreen, InvertBlue);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();

            }
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageColorInvert(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            bool IncludeAlpha = false)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)InvertImage(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), IncludeAlpha);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();

            }
            return OpReturn == ReturnCode.Success;
        }

        public List<DisplayListInstruction2> DisplayList { get; internal set; }

        public void AddColorBlockInstruction()
        {
            DisplayListInstruction2 DisplayInstruction = new DisplayListInstruction2
            {
                Operand = DisplayListInstructions.DrawColorBlock
            };

            DisplayList.Add(DisplayInstruction);
        }

        private static bool OffsetOutOfBounds(GradientStop GS)
        {
            if (GS.Offset < 0.0)
                return true;
            if (GS.Offset > 1.0)
                return true;
            return false;
        }

        /// <summary>
        /// Gradient stop sorter.
        /// </summary>
        public class GradientSorter : IComparer<GradientStop>
        {
            /// <summary>
            /// Compare two gradient stop's offset.
            /// </summary>
            /// <param name="GS1">First gradient stop.</param>
            /// <param name="GS2">Second gradient stop.</param>
            /// <returns>Result of a CompareTo call.</returns>
            public int Compare(GradientStop GS1, GradientStop GS2)
            {
                if (GS1 == null)
                {
                    if (GS2 == null)
                        return 0;
                    else
                        return -1;
                }
                else
                {
                    if (GS2 == null)
                        return 1;
                    return GS1.Offset.CompareTo(GS2.Offset);
                }
            }
        }



        public enum GrayscaleTypes : int
        {
            Grayscale_Mean = 0,
            Grayscale_Brightness = 1,
            Grayscale_Perceptual = 2,
            Grayscale_RedChannel = 3,
            Grayscale_GreenChannel = 4,
            Grayscale_BlueChannel = 5,
            Grayscale_CyanChannel = 6,
            Grayscale_MagentaChannel = 7,
            Grayscale_YellowChannel = 8,
            Grayscale_AlphaChannel = 9,
            Grayscale_BT601 = 10,
            Grayscale_BT709 = 11,
            Grayscale_Desaturation = 12,
            Grayscale_MaxDecomposition = 13,
            Grayscale_MinDecomposition = 14,
            Grayscale_ShadeLevel = 15,
            Grayscale_GrayLevels = 16
        }


    }

    public class LineDefinition
    {
        public LineDefinition()
        {
            Coordinate = 0;
            LineColor = Colors.Black;
            IsHorizontal = true;
        }

        public LineDefinition(int Coordinate, Color LineColor, bool IsHorizontal)
        {
            this.Coordinate = Coordinate;
            this.LineColor = LineColor;
            this.IsHorizontal = IsHorizontal;
        }

        public int Coordinate { get; set; }
        public Color LineColor { get; set; }
        public bool IsHorizontal { get; set; }
    }
}



