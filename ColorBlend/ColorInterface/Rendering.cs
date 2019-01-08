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
        [DllImport("ColorBlender.dll", EntryPoint = "_DrawRectangle_Validate@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int DrawRectangle_Validate (void* Source, Int32 Width, Int32 Height, void* Destination,
             Int32 Left, Int32 Top, Int32 Right, Int32 Bottom);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawRectangle@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int DrawRectangle (void* Source, Int32 Width, Int32 Height, void* Destination,
             Int32 Left, Int32 Top, Int32 Right, Int32 Bottom, UInt32 RectangleColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawRectangle2_Validate@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int DrawRectangle2_Validate (void* Destination, Int32 Width, Int32 Height, 
           Int32 Left, Int32 Top, Int32 Right, Int32 Bottom);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawRectangle2@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int DrawRectangle2 (void* Destination, Int32 Width, Int32 Height, 
             Int32 Left, Int32 Top, Int32 Right, Int32 Bottom, UInt32 RectangleColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_BlendColors@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool BlendColors (void* Buffer, Int32 Width, Int32 Height, Int32 Stride,
             Int32 PureColorCount, void* ColorLocations, void* PureColors);

        /// <summary>
        /// Native function that does the color blending.
        /// </summary>
        /// <param name="Buffer">Pixel buffer where the blending occurs.</param>
        /// <param name="Width">Width of the pixel buffer in pixels.</param>
        /// <param name="Height">Height of the pixel buffer in scan lines.</param>
        /// <param name="Stride">Stride of the pixel buffer.</param>
        /// <param name="PureColorCount">Number of pure color points.</param>
        /// <param name="PureColors">Array of pure color points used to blend colors.</param>
        /// <returns>True on success, false on failure.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_BlendColors2@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool BlendColors2 (void* Buffer, Int32 Width, Int32 Height, Int32 Stride,
            Int32 PureColorCount, void* PureColors);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderColorBlob@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe Int32 RenderColorBlob (void* Target, Int32 ImageWidth, Int32 ImageHeight, Int32 ImageStride,
            UInt32 BlobColor, byte CenterAlpha, byte EdgeAlpha, UInt32 PackedEdgeColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_MergePlanes@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe Int32 MergePlanes (void* Target, void* PlaneSet, int PlaneCount, Int32 Width, Int32 Height, Int32 Stride);

        [DllImport("ColorBlender.dll", EntryPoint = "_MergePlanes2@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe Int32 MergePlanes2 (void* Target, Int32 Width, Int32 Height, Int32 Stride, void* PlaneSet, int PlaneCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_MergePlanes3@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe Int32 MergePlanes3 (void* Target, Int32 Width, Int32 Height, Int32 Stride, void* PlaneSet, int PlaneCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_MergePlanes4@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe Int32 MergePlanes4 (void* Target, Int32 Width, Int32 Height, Int32 Stride, void* PlaneSet, int PlaneCount, void* Results);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawHorizontalLine@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool BlendHorizontalLine (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
    Int32 Y, byte A, byte R, byte G, byte B);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawVerticalLine@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool BlendVerticalLine (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            Int32 X, byte A, byte R, byte G, byte B);

        [DllImport("ColorBlender.dll", EntryPoint = "_DrawLine@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool DrawLine (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            bool IsHorizontal, Int32 Coordinate, byte A, byte R, byte G, byte B);


        [DllImport("ColorBlender.dll", EntryPoint = "_DrawBlocks@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool DrawBlocks (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
                void* ColorBlockList, Int32 ColorBlockCount, UInt32 DefaultColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_Gradient@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int Gradient (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
    void* GradientColorList, Int32 GradientColorCount);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderRandomColorRectangle@52", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RenderRandomColorRectangle (void* Destination, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
    byte LowAlpha, byte HighAlpha, byte LowRed, byte HighRed, byte LowGreen, byte HighGreen, byte LowBlue, byte HighBlue,
    UInt32 Seed);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderRampingColorRectangle@80", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RenderRampingColorRectangle (void* Destination, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
                bool RampAlpha, byte AlphaStart, byte AlphaIncrement, byte NonRampAlpha,
                bool RampRed, byte RedStart, byte RedIncrement, byte NonRampRed,
                bool RampGreen, byte GreenStart, byte GreenIncrement, byte NonRampGreen,
                bool RampBlue, byte BlueStart, byte BlueIncrement, byte NonRampBlue);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderRandomSubBlockRectangle@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RenderRandomSubBlockRectangle (void* Destination, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
           Int32 BlockWidth, Int32 BlockHeight, UInt32 Seed);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderRampingGradientColorRectangle@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RenderRampingGradientColorRectangle (void* Destination, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
              UInt32 PackedStartColor, UInt32 PackedEndColor, bool IgnoreAlpha, bool DoHorizontal);

        [DllImport("ColorBlender.dll", EntryPoint = "_RenderLinearGradients@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RenderLinearGradients (void* Destination, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
              bool IgnoreAlpha, bool DoHorizontal, void* Stops, int StopCount);

        /// <summary>
        /// Set-up and call native code to do the color blending.
        /// </summary>
        /// <param name="Buffer">Pixel buffer where the in-place blending takes place.</param>
        /// <param name="Width">Width of <paramref name="Buffer"/> in pixels.</param>
        /// <param name="Height">Height of <paramref name="Buffer"/> in scan lines.</param>
        /// <param name="Stride">Stride of <paramref name="Buffer"/>.</param>
        /// <param name="ColorPoints">List of color points that are used to generate the color blending.</param>
        /// <param name="DrawDecorations">Determines if decorations are drawn.</param>
        /// <param name="PointCount">
        /// Number of points to use for color blending. This should be equal to the number of enabled
        /// points in <paramref name="ColorPoints"/>.
        /// </param>
        /// <returns>True on success, false on failure.</returns>
        public bool Blendy2 (ref byte[] Buffer, int Width, int Height, int Stride, List<ColorPoint> ColorPoints,
            bool DrawDecorations, int PointCount)
        {
            if (PointCount < 1)
                return false;
            PureColorType[] PureColors = new PureColorType[PointCount];
            for (int i = 0; i < PointCount; i++)
                if (ColorPoints[i].Enabled)
                    PureColors[i] = ColorPoints[i].ToStructure();

            bool RenderReturn = true;
            unsafe
            {
                fixed (void* PixelBuffer = Buffer)
                {
                    fixed (void* ColorPointArray = PureColors)
                    {
                        RenderReturn = BlendColors2(PixelBuffer, Width, Height, Stride, PointCount, ColorPointArray);
                    }
                }
            }
            return RenderReturn;
        }

        /// <summary>
        /// Create a color blob in <paramref name="Buffer"/>.
        /// </summary>
        /// <param name="Buffer">
        /// Where the color blob will be drawn. Must be the proper size where "proper size" is defined as
        /// <paramref name="Width"/> * <paramref name="Stride"/> * <paramref name="Height"/>.
        /// </param>
        /// <param name="Width">Width of the buffer in pixel units.</param>
        /// <param name="Height">Height of the buffer in scan lines.</param>
        /// <param name="Stride">Stride of the buffer.</param>
        /// <param name="BlobColor">The color of the blob.</param>
        /// <param name="CenterAlpha">Alpha value in the center of the color blob.</param>
        /// <param name="EdgeAlpha">Alpha value at the edge of the color blob.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool Blendy3 (ref byte[] Buffer, int Width, int Height, int Stride, Color BlobColor, byte CenterAlpha,
            byte EdgeAlpha, Color EdgeColor)
        {
            ReturnCode RenderReturn = ReturnCode.Success;
            UInt32 PackedEdgeColor = EdgeColor.ToBGRA();
            UInt32 FinalBlobColor = BlobColor.ToBGRA();
            unsafe
            {
                fixed (void* PixelBuffer = Buffer)
                {
                    RenderReturn = (ReturnCode)RenderColorBlob(PixelBuffer, Width, Height, Stride,
                        FinalBlobColor, CenterAlpha, EdgeAlpha,
                        PackedEdgeColor);
                }
            }
            LastReturnCode = RenderReturn;
            return RenderReturn == ReturnCode.Success;
        }

        private bool NotVisible (PlaneSet APlane, int Width, int Height)
        {
            if (APlane.CenterX - APlane.Radius < Width - 1)
                return false;
            if (APlane.CenterX + APlane.Radius < Width - 1)
                return false;
            if (APlane.CenterX - APlane.Radius >= 0)
                return false;
            if (APlane.CenterX + APlane.Radius >= 0)
                return false;
            if (APlane.CenterY - APlane.Radius < Height - 1)
                return false;
            if (APlane.CenterY + APlane.Radius < Height - 1)
                return false;
            if (APlane.CenterY - APlane.Radius >= 0)
                return false;
            if (APlane.CenterY + APlane.Radius >= 0)
                return false;
            return true;
        }

        public bool MergeBlobs (ref byte[] FinalBuffer, int Width, int Height, int Stride, List<PlaneSet> Planes)
        {
            ReturnCode MergeReturn = ReturnCode.Success;
            List<PlaneSet> FinalPlaneSet = new List<PlaneSet>();
            foreach (PlaneSet SomePlane in Planes)
            {
                if (NotVisible(SomePlane, Width, Height))
                    continue;
                FinalPlaneSet.Add(SomePlane);
            }
            Planes = FinalPlaneSet;
            if (Planes.Count < 1)
                return false;
            unsafe
            {
                LLPlaneSet[] LocalPlanes = new LLPlaneSet[Planes.Count];
                for (int i = 0; i < Planes.Count; i++)
                {
                    LocalPlanes[i].CenterX = Planes[i].CenterX;
                    LocalPlanes[i].CenterY = Planes[i].CenterY;
                    LocalPlanes[i].Left = Planes[i].CenterX - Planes[i].Radius;
                    if (LocalPlanes[i].Left < 0)
                        LocalPlanes[i].Left = 0;
                    LocalPlanes[i].Top = Planes[i].CenterY - Planes[i].Radius;
                    if (LocalPlanes[i].Top < 0)
                        LocalPlanes[i].Top = 0;
                    LocalPlanes[i].Right = Planes[i].CenterX + Planes[i].Radius;
                    if (LocalPlanes[i].Right > Width - 1)
                        LocalPlanes[i].Right = Width - 1;
                    LocalPlanes[i].Bottom = Planes[i].CenterY + Planes[i].Radius;
                    if (LocalPlanes[i].Bottom > Height - 1)
                        LocalPlanes[i].Bottom = Height - 1;
                    LocalPlanes[i].Height = Planes[i].Height;
                    LocalPlanes[i].Stride = Planes[i].Stride;
                    LocalPlanes[i].PlaneSize = Planes[i].PlaneBits.Length;
                    LocalPlanes[i].PlaneBits = Marshal.UnsafeAddrOfPinnedArrayElement(Planes[i].PlaneBits, 0);
                }

                fixed (void* PixelBuffer = FinalBuffer)
                {
                    fixed (void* PlaneBuffer = LocalPlanes)
                    {
                        MergeReturn = (ReturnCode)MergePlanes(PixelBuffer, PlaneBuffer, Planes.Count, Width, Height, Stride);
                    }
                }
            }
            LastReturnCode = MergeReturn;
            return MergeReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Merge a list of color blob planes.
        /// </summary>
        /// <param name="FinalBuffer">Where the result will be stored.</param>
        /// <param name="Width">Width of <paramref name="FinalBuffer"/>.</param>
        /// <param name="Height">Height of <paramref name="FinalBuffer"/>.</param>
        /// <param name="Stride">Stride of <paramref name="FinalBuffer"/>.</param>
        /// <param name="HorizontalOffset">Horizontal offset to the start of the buffer that is actually displayed.</param>
        /// <param name="VerticalOffset">Vertical offset to the start of the buffer that is actually displayed.</param>
        /// <param name="Planes">List of planes with color blobs.</param>
        /// <param name="Returned">Will contain the return code from the native function.</param>
        /// <returns>True on success, false on error.</returns>
        public bool MergeBlobs2 (ref byte[] FinalBuffer, int Width, int Height, int Stride, int HorizontalOffset,
            int VerticalOffset, List<PlaneSet> Planes, ref ReturnCode Returned)
        {
            if (Planes.Count < 1)
                return false;
            ReturnCode MergeReturn = ReturnCode.NotSet;
            unsafe
            {
                List<GCHandle> Handles = new List<GCHandle>();
                LLPlaneSet[] LocalPlanes = new LLPlaneSet[Planes.Count];
                for (int i = 0; i < Planes.Count; i++)
                {
                    LocalPlanes[i].Left = Planes[i].Left + HorizontalOffset;
                    LocalPlanes[i].Right = Planes[i].Left + Planes[i].Width;
                    LocalPlanes[i].Top = Planes[i].Top + VerticalOffset;
                    LocalPlanes[i].Bottom = LocalPlanes[i].Top + Planes[i].Height;
                    LocalPlanes[i].Height = Planes[i].Height;
                    LocalPlanes[i].Stride = Planes[i].Stride;
                    LocalPlanes[i].PlaneSize = Planes[i].PlaneBits.Length;
                    LocalPlanes[i].PlaneBits = Marshal.UnsafeAddrOfPinnedArrayElement(Planes[i].PlaneBits, 0);
                    Handles.Add(GCHandle.Alloc(Planes[i].PlaneBits, GCHandleType.Pinned));
                    LocalPlanes[i].TheBits = Handles.Last().AddrOfPinnedObject();
                }

                fixed (void* PixelBuffer = FinalBuffer)
                {
                    fixed (void* PlaneBuffer = LocalPlanes)
                    {
                        MergeReturn = (ReturnCode)MergePlanes(PixelBuffer, PlaneBuffer, Planes.Count, Width, Height, Stride);
                        int LastError = Marshal.GetLastWin32Error();
                        int ExcpCode = Marshal.GetExceptionCode();
                    }
                }

                foreach (GCHandle Handle in Handles)
                    Handle.Free();
            }
            Returned = MergeReturn;
            LastReturnCode = MergeReturn;
            return MergeReturn == ReturnCode.Success;
        }

        public bool MergeBlobs3 (ref byte[] FinalBuffer, int Width, int Height, int Stride, int HorizontalOffset, int VerticalOffset,
            List<PlaneSet> Planes, ref ReturnCode Returned)
        {
            if (Planes.Count < 1)
                return false;
            ReturnCode MergeReturn = ReturnCode.NotSet;
            unsafe
            {
                List<GCHandle> Handles = new List<GCHandle>();
                LLPlaneSet[] LocalPlanes = new LLPlaneSet[Planes.Count];
                for (int i = 0; i < Planes.Count; i++)
                {
                    LocalPlanes[i].Left = Planes[i].Left + HorizontalOffset;
                    LocalPlanes[i].Right = Planes[i].Left + Planes[i].Width;
                    LocalPlanes[i].Top = Planes[i].Top + VerticalOffset;
                    LocalPlanes[i].Bottom = LocalPlanes[i].Top + Planes[i].Height;
                    LocalPlanes[i].Height = Planes[i].Height;
                    LocalPlanes[i].Stride = Planes[i].Stride;
                    LocalPlanes[i].PlaneSize = Planes[i].PlaneBits.Length;
                    //Do some Garbage Collector/memory management trickery to get things to work.
                    LocalPlanes[i].PlaneBits = Marshal.UnsafeAddrOfPinnedArrayElement(Planes[i].PlaneBits, 0);
                    Handles.Add(GCHandle.Alloc(Planes[i].PlaneBits, GCHandleType.Pinned));
                    LocalPlanes[i].TheBits = Handles.Last().AddrOfPinnedObject();
                }

                fixed (void* PixelBuffer = FinalBuffer)
                {
                    fixed (void* PlaneBuffer = LocalPlanes)
                    {
                        //MergeReturn = (ReturnCode)MergePlanes3(PixelBuffer, Width, Height, Stride, PlaneBuffer, Planes.Count);
                        MergeReturn = (ReturnCode)MergePlanes2(PixelBuffer, Width, Height, Stride, PlaneBuffer, Planes.Count);
                        //int LastError = Marshal.GetLastWin32Error();
                        //int ExcpCode = Marshal.GetExceptionCode();
                    }
                }

                //Return the buffers to the control of the Garbage Collector.
                foreach (GCHandle Handle in Handles)
                    Handle.Free();
            }
            Returned = MergeReturn;
            LastReturnCode = MergeReturn;
            return MergeReturn == ReturnCode.Success;
        }

        public bool MergeBlobs4 (ref byte[] FinalBuffer, int Width, int Height, int Stride,
              List<PlaneSet> Planes, ref ReturnCode Returned, ref DrawnObject[] Results)
        {
            if (Planes.Count < 1)
                return false;
            ReturnCode MergeReturn = ReturnCode.NotSet;
            unsafe
            {
                List<GCHandle> Handles = new List<GCHandle>();
                LLPlaneSet[] LocalPlanes = new LLPlaneSet[Planes.Count];
                for (int i = 0; i < Planes.Count; i++)
                {
                    LocalPlanes[i].Left = Planes[i].Left - Planes[i].Radius;
                    LocalPlanes[i].Right = Planes[i].Left + Planes[i].Width;
                    LocalPlanes[i].Top = Planes[i].Top - Planes[i].Radius;
                    LocalPlanes[i].Bottom = LocalPlanes[i].Top + Planes[i].Height;
                    LocalPlanes[i].Height = Planes[i].Height;
                    LocalPlanes[i].Width = Planes[i].Width;
                    LocalPlanes[i].Stride = Planes[i].Stride;
                    LocalPlanes[i].PlaneSize = Planes[i].PlaneBits.Length;
                    //Do some Garbage Collector/memory management trickery to get pointers to work.
                    LocalPlanes[i].PlaneBits = Marshal.UnsafeAddrOfPinnedArrayElement(Planes[i].PlaneBits, 0);
                    Handles.Add(GCHandle.Alloc(Planes[i].PlaneBits, GCHandleType.Pinned));
                    LocalPlanes[i].TheBits = Handles.Last().AddrOfPinnedObject();
                }

                //DrawnObject[] ObjectResults = new DrawnObject[Planes.Count];
                DrawnObject[] ObjectResults = null;

                fixed (void* PixelBuffer = FinalBuffer)
                {
                    fixed (void* ObjResults = ObjectResults)
                    {
                        fixed (void* PlaneBuffer = LocalPlanes)
                        {
                            MergeReturn = (ReturnCode)MergePlanes4(PixelBuffer, Width, Height, Stride, PlaneBuffer, Planes.Count, ObjResults);
                        }
                    }
                }

                //Return the buffers to the control of the Garbage Collector.
                foreach (GCHandle Handle in Handles)
                    Handle.Free();
            }
            Returned = MergeReturn;
            LastReturnCode = MergeReturn;
            return MergeReturn == ReturnCode.Success;
        }

        public bool RenderRectangle (WriteableBitmap SourceBuffer,
            WriteableBitmap DestinationBuffer, int Left, int Top, int Right, int Bottom,
            Color LineColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)DrawRectangle(SourceBuffer.BackBuffer.ToPointer(),
                    SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, DestinationBuffer.BackBuffer.ToPointer(),
                    Left, Top, Right, Bottom,
                    LineColor.ToARGB());

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool RenderRectangle2 (WriteableBitmap DestinationBuffer, 
            int Left, int Top, int Right, int Bottom,
            Color LineColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)DrawRectangle2(DestinationBuffer.BackBuffer.ToPointer(),
                    DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight, 
                    Left, Top, Right, Bottom,
                    LineColor.ToARGB());

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Draw a horizontal line from the left to the right.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="Row">The row where the line will be drawn.</param>
        /// <param name="LineColor">The color of the line.</param>
        /// <returns>True on success, false on error.</returns>
        public bool DrawHorizontalLine (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride, int Row, Color LineColor)
        {
            bool DrawnOK = true;
            unsafe
            {
                fixed (void* Buffer = Target)
                {
                    DrawnOK = BlendHorizontalLine(Buffer, TargetWidth, TargetHeight, TargetStride, Row,
                        LineColor.A, LineColor.R, LineColor.G, LineColor.B);
                }
            }
            return DrawnOK;
        }

        /// <summary>
        /// Draw a vertical line from the top to the bottom.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="Column">The column where the line will be drawn.</param>
        /// <param name="LineColor">The color of the line.</param>
        /// <returns>True on success, false on error.</returns>
        public bool DrawVerticalLine (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride, int Column, Color LineColor)
        {
            bool DrawnOK = true;
            unsafe
            {
                fixed (void* Buffer = Target)
                {
                    DrawnOK = BlendVerticalLine(Buffer, TargetWidth, TargetHeight, TargetStride, Column,
                        LineColor.A, LineColor.R, LineColor.G, LineColor.B);
                }
            }
            return DrawnOK;
        }

        /// <summary>
        /// Draw a vertical or horizontal line.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="IsHorizontal">Determines if the line is vertical or horizontal.</param>
        /// <param name="Coordinate">Where the line is drawn.</param>
        /// <param name="LineColor">Color of the line.</param>
        /// <returns>True on success, false on error.</returns>
        public bool DrawALine (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride, bool IsHorizontal,
            int Coordinate, Color LineColor)
        {
            bool DrawnOK = true;
            unsafe
            {
                fixed (void* Buffer = Target)
                {
                    DrawnOK = DrawLine(Buffer, TargetWidth, TargetHeight, TargetStride, IsHorizontal, Coordinate,
                        LineColor.A, LineColor.R, LineColor.G, LineColor.B);
                }
            }
            return DrawnOK;
        }

        /// <summary>
        /// Draw a single line.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="First">First end point.</param>
        /// <param name="Second">Second end point.</param>
        /// <param name="LineColor">Color of the line.</param>
        /// <param name="AntiAlias">Anti-alias flag.</param>
        /// <param name="LineThickness">Line thickness.</param>
        /// <returns>True on success, false on error.</returns>
        public bool DrawALine (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride,
            PointEx First, PointEx Second, Color LineColor, bool AntiAlias, int LineThickness)
        {
            bool DrawnOK = true;
            unsafe
            {
                fixed (void* Buffer = Target)
                {
                    DrawAnyLine(Buffer, TargetWidth, TargetHeight, TargetStride, First.IntX, First.IntY, Second.IntX, Second.IntY,
                        LineColor.A, LineColor.R, LineColor.G, LineColor.B, AntiAlias, LineThickness);
                }
            }
            return DrawnOK;
        }

        /// <summary>
        /// Draw a set of lines.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="LineDefinitions">List of line definitions to draw.</param>
        /// <returns>True on success, false on error.</returns>
        public bool DrawLines (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride, List<LineDefinition> LineDefinitions)
        {
            bool DrawnOK = true;
            foreach (LineDefinition Definition in LineDefinitions)
            {
                DrawnOK = DrawALine(ref Target, TargetWidth, TargetHeight, TargetStride, Definition.IsHorizontal, Definition.Coordinate, Definition.LineColor);
                if (!DrawnOK)
                    break;
            }
            return DrawnOK;
        }

        /// <summary>
        /// Draw a list of color blocks.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="ColorBlocks">List of color blocks to draw.</param>
        /// <param name="DefaultColor">The color to use when no blocks are at a given point.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool DrawColorBlocks (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride,
           List<ColorBlock> ColorBlocks, Color DefaultColor)
        {
            bool DrawnOK = true;
            UInt32 BGColor = (UInt32)((DefaultColor.B << 24) + (DefaultColor.G << 16) + (DefaultColor.R << 8) + (DefaultColor.A));
            unsafe
            {
                ColorBlock[] Blocks = new ColorBlock[ColorBlocks.Count];
                for (int i = 0; i < ColorBlocks.Count; i++)
                    Blocks[i] = ColorBlocks[i];
                fixed (void* Buffer = Target)
                {
                    fixed (void* BlockArray = Blocks)
                    {
                        DrawnOK = DrawBlocks(Buffer, TargetWidth, TargetHeight, TargetStride,
                            BlockArray, Blocks.Length, BGColor);
                    }
                }
            }
            return DrawnOK;
        }

        /// <summary>
        /// Draw a list of color blocks.
        /// </summary>
        /// <param name="Target">Contains target buffer information.</param>
        /// <param name="ColorBlocks">List of color blocks to draw.</param>
        /// <param name="DefaultColor">The color to use when no blocks are at a given point.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool DrawColorBlocks (ref RenderTarget Target, List<ColorBlock> ColorBlocks, Color DefaultColor)
        {
            bool DrawnOK = false;
            UInt32 BGColor = (UInt32)((DefaultColor.B << 24) + (DefaultColor.G << 16) + (DefaultColor.R << 8) + (DefaultColor.A));
            unsafe
            {
                ColorBlock[] Blocks = new ColorBlock[ColorBlocks.Count];
                for (int i = 0; i < ColorBlocks.Count; i++)
                    Blocks[i] = ColorBlocks[i];
                fixed (void* Buffer = Target.Bits)
                {
                    fixed (void* BlockArray = Blocks)
                    {
                        DrawnOK = DrawBlocks(Buffer, Target.Width, Target.Height, Target.Stride,
                            BlockArray, Blocks.Length, BGColor);
                    }
                }
            }
            return DrawnOK;
        }

        public bool RandomColoredBlock (ref byte[] Destination, double Width, double Height, double Stride)
        {
            ReturnCode HistogramReturn = ReturnCode.NotSet;
            unsafe
            {
                fixed (void* Buffer = Destination)
                {
                    uint Seed = (uint)DateTime.Now.Millisecond;
                    HistogramReturn = (ReturnCode)RenderRandomColorRectangle(Buffer, (int)Width, (int)Height, (int)Stride,
                        0xff, 0xff, 0x60, 0xff, 0x60, 0xff, 0x60, 0xff, Seed);
                }
            }
            LastReturnCode = HistogramReturn;
            return HistogramReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Draws a color gradient in <paramref name="Destination"/> using supplied parameters.
        /// </summary>
        /// <param name="Destination">Where the gradient will be drawn.</param>
        /// <param name="Width">Width of the buffer.</param>
        /// <param name="Height">Height of the buffer.</param>
        /// <param name="Stride">Stride of the buffer.</param>
        /// <param name="Horizontal">Determines if the gradient is horizontal or vertical.</param>
        /// <param name="StartColor">Starting gradient color.</param>
        /// <param name="EndColor">Ending gradient color.</param>
        /// <returns>True on success, false on error.</returns>
        public bool GradientGeneration (ref byte[] Destination, double Width, double Height, double Stride, bool Horizontal, Color StartColor, Color EndColor)
        {
            ReturnCode HistogramReturn = ReturnCode.NotSet;
            unsafe
            {
                fixed (void* Buffer = Destination)
                {
                    UInt32 Start = StartColor.ToARGB();
                    UInt32 End = EndColor.ToARGB();
                    HistogramReturn = (ReturnCode)RenderRampingGradientColorRectangle(Buffer, (int)Width, (int)Height, (int)Stride,
                        Start, End, true, Horizontal);
                }
            }
            LastReturnCode = HistogramReturn;
            return HistogramReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Create an image from a series of gradient stops.
        /// </summary>
        /// <param name="Destination">Where the image will be rendered.</param>
        /// <param name="Width">Width of the image.</param>
        /// <param name="Height">Height of the image.</param>
        /// <param name="Stride">Stride of the image.</param>
        /// <param name="Horizontal">Determines if horizonal or vertical gradients will be created.</param>
        /// <param name="Stops">
        /// List of gradient stops. Must be at least two gradient stops. Internally sorted into Location order. If no stop is at 0 or 1,
        /// gradient stops will be added. Any gradient stops whose offset is not normalized are removed.
        /// </param>
        /// <returns>True on success, false on error.</returns>
        public bool GradientGeneration2 (ref byte[] Destination, double Width, double Height, double Stride, bool Horizontal,
            List<GradientStop> Stops)
        {
            if (Stops == null)
                return false;
            if (Stops.Count < 2)
                return false;
            GradientSorter GSorter = new GradientSorter();
            Stops.Sort(GSorter);
            Stops.RemoveAll(OffsetOutOfBounds);
            if (Stops[0].Offset > 0.0)
            {
                GradientStop Stop0 = new GradientStop
                {
                    Offset = 0.0,
                    Color = Stops[0].Color.Percentage(1.0 - Stops[0].Offset)
                };
                Stops.Insert(0, Stop0);
            }
            if (Stops.Last().Offset < 1.0)
            {
                GradientStop StopL = new GradientStop
                {
                    Offset = 1.0,
                    Color = Stops.Last().Color.Percentage(1.0 - Stops.Last().Offset)
                };
                Stops.Add(StopL);
            }
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                CBGradientStop2[] stops = new CBGradientStop2[Stops.Count - 1];
                for (int i = 0; i < Stops.Count - 1; i++)
                {
                    stops[i].AbsStart = (int)((Horizontal ? Width : Height) * Stops[i].Offset);
                    stops[i].AbsEnd = (int)((Horizontal ? Width : Height) * Stops[i + 1].Offset);
                    if (Horizontal)
                    {
                        if (stops[i].AbsEnd >= (int)Width)
                            stops[i].AbsEnd = (int)Width - 1;
                    }
                    else
                    {
                        if (stops[i].AbsEnd >= (int)Height)
                            stops[i].AbsEnd = (int)Height - 1;
                    }
                    stops[i].AbsGap = stops[i].AbsEnd - stops[i].AbsStart;
                    stops[i].StartColor = Stops[i].Color.ToARGB();
                    stops[i].EndColor = Stops[i + 1].Color.ToARGB();
                }
                fixed (void* Buffer = Destination)
                {
                    fixed (void* GStops = stops)
                    {
                        OpReturn = (ReturnCode)RenderLinearGradients(Buffer, (int)Width, (int)Height, (int)Stride,
                            true, Horizontal, GStops, stops.Length);
                    }
                }
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
