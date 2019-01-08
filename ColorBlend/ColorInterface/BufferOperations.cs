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
        [DllImport("ColorBlender.dll", EntryPoint = "_CopyBuffer2@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CopyBuffer2(void* Source, Int32 SourceWidth, Int32 SourceHeight, void* Destination);

        [DllImport("ColorBlender.dll", EntryPoint = "_CropBuffer@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool CropBuffer (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
            void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride, void* Region);

        [DllImport("ColorBlender.dll", EntryPoint = "_CropBuffer2@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CropBuffer2 (void* SourceBuffer, void* DestinationBuffer, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
             Int32 X1, Int32 Y1, Int32 X2, Int32 Y2);

        [DllImport("ColorBlender.dll", EntryPoint = "_CopyBufferRegion@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CopyBufferRegion (void* SourceBuffer, void* DestinationBuffer, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
              Int32 X1, Int32 Y1, Int32 X2, Int32 Y2);

        [DllImport("ColorBlender.dll", EntryPoint = "_ClearBuffer2@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ClearBuffer2 (void* Destination, Int32 Width, Int32 Height, Int32 Stride, UInt32 FillColor);

        [DllImport("ColorBlender.dll", EntryPoint = "_CopyBufferToBuffer@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CopyBufferToBuffer (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        [DllImport("ColorBlender.dll", EntryPoint = "_CopyRegion@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CopyRegion (void* Source, Int32 Width, Int32 Height, Int32 SourceStride,
            void* Destination, Int32 DestinationWidth, Int32 DestinationStride,
            void* UpperLeft, void* LowerRight);

        public bool CopyBuffer(WriteableBitmap Source, int Width, int Height, WriteableBitmap Destination)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)CopyBuffer2(Source.BackBuffer.ToPointer(), Width, Height, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool CopyImageRegion (WriteableBitmap Source, WriteableBitmap Destination,
        int X1, int Y1, int X2, int Y2)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                AbsolutePointStruct UL = new AbsolutePointStruct
                {
                    X = X1,
                    Y = Y1
                };
                AbsolutePointStruct LR = new AbsolutePointStruct
                {
                    X = X2,
                    Y = Y2
                };
                OpReturn = (ReturnCode)CopyRegion(Source.BackBuffer.ToPointer(),
                    Source.PixelWidth, Source.PixelHeight, Source.BackBufferStride,
                    Destination.BackBuffer.ToPointer(), Destination.PixelWidth,
                    Destination.BackBufferStride, &UL, &LR);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool CopyBufferTo (WriteableBitmap Source, int Width, int Height, int Stride, WriteableBitmap Destination)
        {
            if (!ValidateImages(Source, Destination))
                return false;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)CopyBufferToBuffer(Source.BackBuffer.ToPointer(), Source.PixelWidth, Source.PixelHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ClearBufferRegion2@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ClearBufferRegion (void* Buffer, Int32 Width, Int32 Height, Int32 Stride, UInt32 FillColor,
                 Int32 Left, Int32 Top, Int32 Right, Int32 Bottom);

        [DllImport("ColorBlender.dll", EntryPoint = "_PasteRegion@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int PasteRegion (void* Destination, Int32 DestWidth, Int32 DestHeight, Int32 DestStride,
              void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
              void* UpperLeft, void* LowerRight);

        [DllImport("ColorBlender.dll", EntryPoint = "_ClearBuffer@80", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool ClearBuffer (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
             byte ClearA, byte ClearR, byte ClearG, byte ClearB,
             bool DrawGrid, byte GridA, byte GridR, byte GridG, byte GridB, Int32 GridCellWidth, Int32 GridCellHeight,
             bool DrawOutline, byte OutA, byte OutR, byte OutG, byte OutB);

        /// <summary>
        /// Crop the buffer <paramref name="Source"/>.
        /// </summary>
        /// <param name="Target">Where the cropped buffer will be placed. Must be large enough to hold the result of the crop.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="Source">The buffer to crop.</param>
        /// <param name="SourceWidth">The width of the source buffer.</param>
        /// <param name="SourceHeight">The height of the source buffer.</param>
        /// <param name="SourceStride">The stride of the source buffer.</param>
        /// <param name="Top">Top of the region in <paramref name="Source"/> to crop.</param>
        /// <param name="Left">Left side of the region in <paramref name="Source"/> to crop.</param>
        /// <param name="Bottom">Bottom of the region in <paramref name="Source"/> to crop.</param>
        /// <param name="Right">Right side of the region in <paramref name="Source"/> to crop.</param>
        /// <returns>True on success, false on error.</returns>
        public bool CropBuffer (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride,
                               byte[] Source, int SourceWidth, int SourceHeight, int SourceStride,
                               int Top, int Left, int Bottom, int Right)
        {
            bool CropReturn = true;
            RegionStruct[] CropRegion = new RegionStruct[1];
            CropRegion[0].Top = Top;
            CropRegion[0].Left = Left;
            CropRegion[0].Bottom = Bottom;
            CropRegion[0].Right = Right;

            unsafe
            {
                fixed (void* RegionPtr = CropRegion)
                {
                    fixed (void* TargetBuffer = Target)
                    {
                        fixed (void* SourceBuffer = Source)
                        {
                            CropReturn = CropBuffer(TargetBuffer, TargetWidth, TargetHeight, TargetStride,
                                                    SourceBuffer, SourceWidth, SourceHeight, SourceStride,
                                                    RegionPtr);
                        }
                    }
                }
            }

            return CropReturn;
        }

        public bool DoCropBuffer2 (ref byte[] Source, ref byte[] Destination, int SourceWidth, int SourceHeight,
            int SourceStride, int X1, int Y1, int X2, int Y2)
        {
            ReturnCode CropResult = ReturnCode.NotSet;
            unsafe
            {
                fixed (void* SourcePtr = Source)
                {
                    fixed (void* DestinationPtr = Destination)
                    {
                        CropResult = (ReturnCode)CropBuffer2(SourcePtr, DestinationPtr, SourceWidth, SourceHeight, SourceStride,
                            X1, Y1, X2, Y2);
                    }
                }
            }
            return CropResult == ReturnCode.Success ? true : false;
        }

        public bool CopyBuffer (ref byte[] Source, ref byte[] Destination, int SourceWidth, int SourceHeight,
            int SourceStride, int X1, int Y1, int X2, int Y2)
        {
            return DoCropBuffer2(ref Source, ref Destination, SourceWidth, SourceHeight, SourceStride, X1, Y1, X2, Y2);
        }



        /// <summary>
        /// Clear the buffer using passed data.
        /// </summary>
        /// <param name="Target">Where the drawing will occur.</param>
        /// <param name="TargetWidth">Width of <paramref name="Target"/>.</param>
        /// <param name="TargetHeight">Height of <paramref name="Target"/>.</param>
        /// <param name="TargetStride">Stride of <paramref name="Target"/>.</param>
        /// <param name="ClearColor">The background color of the buffer.</param>
        /// <param name="DrawGrid">Determines if a grid is drawn.</param>
        /// <param name="GridColor">The color of the grid lines if a grid is drawn.</param>
        /// <param name="GridCellWidth">Grid horizontal period.</param>
        /// <param name="GridCellHeight">Grid vertical period.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool DoClearBuffer (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride,
            Color ClearColor, bool DrawGrid, Color GridColor, int GridCellWidth, int GridCellHeight,
            Color OutlineColor)
        {
            bool ClearedOK = true;
            unsafe
            {
                fixed (void* Buffer = Target)
                {
                    ClearedOK = ClearBuffer(Buffer, TargetWidth, TargetHeight, TargetStride,
                                  ClearColor.A, ClearColor.R, ClearColor.G, ClearColor.B,
                                  DrawGrid, GridColor.A, GridColor.R, GridColor.G, GridColor.B,
                                  GridCellWidth, GridCellHeight,
                                  OutlineColor == Colors.Transparent ? false : true,
                                  OutlineColor.A, OutlineColor.R, OutlineColor.G, OutlineColor.B);
                }
            }
            return ClearedOK;
        }

        /// <summary>
        /// Copy <paramref name="Source"/> to <paramref name="Target"/>.
        /// </summary>
        /// <param name="Target">Where the source will be placed.</param>
        /// <param name="TargetWidth">Width of the target.</param>
        /// <param name="TargetHeight">Height of the target.</param>
        /// <param name="TargetStride">Stride of the target.</param>
        /// <param name="Source">Source buffer.</param>
        /// <param name="SourceWidth">Width of the source.</param>
        /// <param name="SourceHeight">Height of the source.</param>
        /// <param name="SourceStride">Stride of the source.</param>
        /// <param name="BGColor">Background color if <paramref name="Source"/> is smaller than <paramref name="Target"/>.
        /// <returns>True on success, false on failure.</returns>
        public bool DoFillBufferWithBuffer (ref byte[] Target, int TargetWidth, int TargetHeight, int TargetStride,
            IntPtr Source, int SourceWidth, int SourceHeight, int SourceStride,
            Color BGColor)
        {
            ReturnCode FillReturn = ReturnCode.Success;
            UInt32 PackedColor = BGColor.ToARGB();
            unsafe
            {
                fixed (void* TargetBuffer = Target)
                {
                    FillReturn = (ReturnCode)FillBufferWithBuffer(TargetBuffer, TargetWidth, TargetHeight, TargetStride,
                        (void*)Source, SourceWidth, SourceHeight, SourceStride,
                        PackedColor);
                }
            }
            return FillReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_SwapImageBuffers@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SwapImageBuffers (void* Buffer1, void* Buffer2, Int32 Width, Int32 Height, Int32 Stride);

        public bool SwapBuffers (WriteableBitmap Buffer1, WriteableBitmap Buffer2, int Width, int Height, int Stride)
        {
            if (!ValidateImages(Buffer1, Buffer2))
                return false;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Buffer1.Lock();
                Buffer2.Lock();
                OpReturn = (ReturnCode)SwapImageBuffers(Buffer1.BackBuffer.ToPointer(), Buffer2.BackBuffer.ToPointer(),
                    Width, Height, Stride);
                System.Windows.Int32Rect DirtyRect1 = new Int32Rect(0, 0, Buffer1.PixelWidth, Buffer1.PixelHeight);
                Buffer1.AddDirtyRect(DirtyRect1);
                Buffer1.Unlock();
                System.Windows.Int32Rect DirtyRect2 = new Int32Rect(0, 0, Buffer2.PixelWidth, Buffer2.PixelHeight);
                Buffer2.AddDirtyRect(DirtyRect2);
                Buffer2.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_PasteRegion4@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int PasteRegion4 (void* Destination, Int32 DestWidth, Int32 DestHeight,
               void* Source, Int32 SourceWidth, Int32 SourceHeight,
               int X1, int Y1, int X2, int Y2);

        public bool EditPasteRegion4 (WriteableBitmap Destination, int DestinationWidth, int DestinationHeight, WriteableBitmap Source,
            int SourceWidth, int SourceHeight, Point UpperLeft, Point LowerRight)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Destination.Lock();
                Source.Lock();
                AbsolutePointStruct UL = new AbsolutePointStruct
                {
                    X = (int)UpperLeft.X,
                    Y = (int)UpperLeft.Y
                };
                AbsolutePointStruct LR = new AbsolutePointStruct
                {
                    X = (int)LowerRight.X,
                    Y = (int)LowerRight.Y
                };
                OpReturn = (ReturnCode)PasteRegion4(Destination.BackBuffer.ToPointer(), DestinationWidth, DestinationHeight,
                    Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight, (int)UpperLeft.X, (int)UpperLeft.Y,
                    (int)LowerRight.X, (int)LowerRight.Y);
                System.Windows.Int32Rect DirtyRect1 = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect1);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool EditPasteRegion4A (WriteableBitmap Destination, int DestinationWidth, int DestinationHeight, WriteableBitmap Source,
             int SourceWidth, int SourceHeight)
        {
            if (!ValidateImages(Destination, Source))
                return false;
            Point UL = new Point(0, 0);
            Point LR = new Point(SourceWidth - 1, SourceHeight - 1);
            return EditPasteRegion4(Destination, DestinationWidth, DestinationHeight, Source, SourceWidth, SourceHeight, UL, LR);
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ClearBufferDWord@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ClearBufferDWord (void* Destination, Int32 DestWidth, Int32 DestHeight, UInt32 DWordValue);

        public bool DWordClearBuffer (WriteableBitmap Destination, int DestinationWidth, int DestinationHeight, UInt32 DWordValue)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Destination.Lock();
                OpReturn = (ReturnCode)ClearBufferDWord(Destination.BackBuffer.ToPointer(), DestinationWidth, DestinationHeight, DWordValue);
                System.Windows.Int32Rect DirtyRect1 = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect1);
                Destination.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_OverlayGrid@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int OverlayGrid (void* Source, Int32 Width, Int32 Height, void* Destination,
           int HorizontalFrequency, int VerticalFrequency, UInt32 GridColor);

        public bool OverlayBufferWithGrid (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height,
            int HorizontalFrequency, int VerticalFrequency, Color GridColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;

            unsafe
            {
                DestinationBuffer.Lock();
                SourceBuffer.Lock();

                OpReturn = (ReturnCode)OverlayGrid(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                     DestinationBuffer.BackBuffer.ToPointer(), HorizontalFrequency, VerticalFrequency, GridColor.ToARGB());

                SourceBuffer.Unlock();
                System.Windows.Int32Rect DirtyRect1 = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect1);
                DestinationBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
