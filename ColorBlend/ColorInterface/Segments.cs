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
    public enum SegmentTypes : int
    {
        SegmentMeanColor = 0,
        SegmentMedianColor = 1,
        SegmentBrightestColor = 2,
        SegmentDarkestColor = 3,
        SegmentLuminence = 4,
    }

    public enum SegmentShapeTypes : int
    {
        Rectange = 0,
        Ellipse = 1,
        Circle = 2,
        Square = 3
    }

    public partial class ColorBlenderInterface
    {
        [DllImport("ColorBlender.dll", EntryPoint = "_SegmentBlocks@72", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SegmentBlocks (void* Source, Int32 Width, Int32 Height, Int32 Stride,
              Int32 BlocksX, Int32 BlocksY, Int32 SegmentType, Int32 ShapeType, Int32 ShapeMargin,
              bool OverrideTransparency, bool GradientTransparency, double OverriddenTransparency,
              void* Destination, UInt32 BGColor, bool InvertSpatially, bool HighlightByLuminance, bool InvertLuminance);

        [DllImport("ColorBlender.dll", EntryPoint = "_SegmentBlocks2@52", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SegmentBlocks2 (void* Source, Int32 Width, Int32 Height, Int32 Stride,
              Int32 BlocksX, Int32 BlocksY, void* Destination,
              bool ShowGrid, UInt32 GridColor, bool HighlightCell, Int32 HightlightCellX,
              Int32 HighlightCellY, UInt32 CellHighlightColor);

        public bool ImageSegmentBlock (WriteableBitmap SourceBuffer, int BlockX, int BlockY,
            WriteableBitmap DestinationBuffer, SegmentTypes SegmentType, int ShapeMargin = 0, SegmentShapeTypes ShapeType = SegmentShapeTypes.Rectange,
            Color? BGColor = null, bool OverrideTransparency = false, bool GradientTransparency = false,
            double OverriddenTransparency = 10, bool InvertSpatially = false,
            bool HighlightByLuminance = false, bool InvertHighlight = false)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            if (BGColor == null)
                BGColor = Colors.Transparent;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)SegmentBlocks(SourceBuffer.BackBuffer.ToPointer(),
                    SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    BlockX, BlockY, (int)SegmentType, (int)ShapeType, ShapeMargin,
                    OverrideTransparency, GradientTransparency, OverriddenTransparency,
                    DestinationBuffer.BackBuffer.ToPointer(), BGColor.Value.ToARGB(),
                    InvertSpatially, HighlightByLuminance, InvertHighlight);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageSegmentBlock2 (WriteableBitmap Source, int BlockX, int BlockY,
            WriteableBitmap Destination, bool ShowGrid, Color GridColor,
            bool HighlightCell, int HighlightCellX, int HighlightCellY,
            Color HighlightColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)SegmentBlocks2(Source.BackBuffer.ToPointer(),
                    Source.PixelWidth, Source.PixelHeight, Source.BackBufferStride,
                    BlockX, BlockY, Destination.BackBuffer.ToPointer(), ShowGrid,
                    GridColor.ToARGB(), HighlightCell, HighlightCellX, HighlightCellY,
                    HighlightColor.ToARGB());

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
