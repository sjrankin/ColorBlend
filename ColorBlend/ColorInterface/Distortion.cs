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
        private void GetRotationalExtent (PointEx SourceUpperLeft, PointEx SourceLowerRight, ref PointEx FinalUpperLeft,
            ref PointEx FinalLowerRight, double Angle)
        {
            if (SourceUpperLeft == null || SourceLowerRight == null || FinalUpperLeft == null || FinalLowerRight == null)
                return;
            FinalUpperLeft = SourceUpperLeft.RotatePoint(Angle);
            FinalLowerRight = SourceLowerRight.RotatePoint(Angle);
        }

        private bool LocalVMirror (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride)
        {
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;

            SourceBuffer.Lock();
            DestinationBuffer.Lock();

            unsafe
            {
                byte* Src = (byte*)SourceBuffer.BackBuffer;
                byte* Dest = (byte*)DestinationBuffer.BackBuffer;
                for (int Row = 0; Row < Height; Row++)
                {
                    int RowOffset = Row * Stride;
                    int DestOffset = (Height - Row - 1) * Stride;
                    for (int Column = 0; Column < Width; Column++)
                    {
                        int SourceIndex = (Column * 4) + RowOffset;
                        int DestIndex = (Column * 4) + DestOffset;
                        Dest[DestIndex + 3] = Src[SourceIndex + 3];
                        Dest[DestIndex + 2] = Src[SourceIndex + 2];
                        Dest[DestIndex + 1] = Src[SourceIndex + 1];
                        Dest[DestIndex + 0] = Src[SourceIndex + 0];
                    }
                }
            }

            System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
            DestinationBuffer.AddDirtyRect(DirtyRect);
            SourceBuffer.Unlock();
            DestinationBuffer.Unlock();

            return true;
        }

        private bool LocalVBMirror (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            bool SetAlpha, byte AlphaValue)
        {
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;

            SourceBuffer.Lock();
            DestinationBuffer.Lock();

            unsafe
            {
                byte* Src = (byte*)SourceBuffer.BackBuffer;
                byte* Dest = (byte*)DestinationBuffer.BackBuffer;
                for (int Row = 0; Row < Height; Row++)
                {
                    int RowOffset = Row * Stride;
                    int DestOffset = (Height - Row - 1) * Stride;
                    for (int Column = 0; Column < Stride; Column++)
                    {
                        int DestIndex = (Stride - (Column + 1)) + DestOffset;
                        Dest[DestIndex] = Src[Column + RowOffset];
                    }
                }

                if (SetAlpha)
                {
                    for (int Row = 0; Row < Height; Row++)
                    {
                        int RowOffset = Row * Stride;
                        for (int Column = 0; Column < Width; Column++)
                        {
                            int Index = (Column * 4) + RowOffset;
                            Dest[Index + 3] = AlphaValue;
                        }
                    }
                }
            }

            System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
            DestinationBuffer.AddDirtyRect(DirtyRect);
            SourceBuffer.Unlock();
            DestinationBuffer.Unlock();

            return true;
        }

        private bool LocalHMirror (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride)
        {
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;

            SourceBuffer.Lock();
            DestinationBuffer.Lock();

            unsafe
            {
                byte* Src = (byte*)SourceBuffer.BackBuffer;
                byte* Dest = (byte*)DestinationBuffer.BackBuffer;
                for (int Row = 0; Row < Height; Row++)
                {
                    int RowOffset = Row * Stride;
                    for (int Column = 0; Column < Width; Column++)
                    {
                        int SourceIndex = (Column * 4) + RowOffset;
                        int DestIndex = ((Width - Column - 1) * 4) + RowOffset;
                        Dest[DestIndex + 3] = Src[SourceIndex + 3];
                        Dest[DestIndex + 2] = Src[SourceIndex + 2];
                        Dest[DestIndex + 1] = Src[SourceIndex + 1];
                        Dest[DestIndex + 0] = Src[SourceIndex + 0];
                    }
                }
            }
            System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
            DestinationBuffer.AddDirtyRect(DirtyRect);
            SourceBuffer.Unlock();
            DestinationBuffer.Unlock();

            return true;
        }

        private bool LocalHBMirror (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            bool SetAlpha, byte AlphaValue)
        {
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;

            SourceBuffer.Lock();
            DestinationBuffer.Lock();

            unsafe
            {
                byte* Src = (byte*)SourceBuffer.BackBuffer;
                byte* Dest = (byte*)DestinationBuffer.BackBuffer;

                for (int Row = 0; Row < Height; Row++)
                {
                    int RowOffset = Row * Stride;
                    for (int Column = 0; Column < Stride; Column++)
                    {
                        int DestIndex = (Stride - (Column + 1)) + RowOffset;
                        Dest[DestIndex] = Src[Column + RowOffset];
                    }
                }

                if (SetAlpha)
                {
                    for (int Row = 0; Row < Height; Row++)
                    {
                        int RowOffset = Row * Stride;
                        for (int Column = 0; Column < Width; Column++)
                        {
                            int Index = (Column * 4) + RowOffset;
                            Dest[Index + 3] = AlphaValue;
                        }
                    }
                }
            }

            System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
            DestinationBuffer.AddDirtyRect(DirtyRect);
            SourceBuffer.Unlock();
            DestinationBuffer.Unlock();

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HorizontalMirrorByte@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HorizontalMirrorByte (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool SetAlpha, byte AlphaValue);

        [DllImport("ColorBlender.dll", EntryPoint = "_HorizontalMirrorPixel@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HorizontalMirror (void* Source, Int32 Width, Int32 Height, void* Destination);

        public bool ImageMirrorHorizontal (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
                                           bool ByPixel = true)
        {
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;
#if false
            bool OK = false;
            if (ByPixel)
                OK = LocalHMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride);
            else
                OK = LocalHBMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride, true, 0xff);
            return OK;
#else
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                if (ByPixel)
                    OpReturn = (ReturnCode)HorizontalMirror(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                        DestinationBuffer.BackBuffer.ToPointer());
                else
                    OpReturn = (ReturnCode)HorizontalMirrorByte(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), true, 0xff);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
#endif
        }


        [DllImport("ColorBlender.dll", EntryPoint = "_VerticalMirrorPixel@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int VerticalMirror (void* Source, Int32 Width, Int32 Height, void* Destination);

        [DllImport("ColorBlender.dll", EntryPoint = "_VerticalMirrorByte@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int VerticalMirrorByte (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool SetAlpha, byte AlphaValue);

        public bool ImageMirrorVertical (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            bool ByPixel = true)
        {
#if false
            bool OK = false;
            if (ByPixel)
                OK = LocalVMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride);
            else
                OK = LocalVBMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride, true, 0xff);
            return OK;
#else
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                if (ByPixel)
                    OpReturn = (ReturnCode)VerticalMirror(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                            DestinationBuffer.BackBuffer.ToPointer());
                else
                    OpReturn = (ReturnCode)VerticalMirrorByte(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                         DestinationBuffer.BackBuffer.ToPointer(), true, 0xff);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
#endif
        }


        [DllImport("ColorBlender.dll", EntryPoint = "_ULtoLRPixel@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ULtoLR (void* Source, Int32 Width, Int32 Height, void* Destination);

        [DllImport("ColorBlender.dll", EntryPoint = "_ULtoLRByte@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ULtoLRByte (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool SetAlpha, byte AlphaValue);

        public bool ImageMirrorBoth (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            bool ByPixel = true)
        {
#if false
            bool OK = false;
            if (ByPixel)
            {
                OK = LocalHMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride);
                OK = SwapBuffers(SourceBuffer, DestinationBuffer, Width, Height, Stride);
                OK = LocalVMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride);
            }
            else
            {
                OK = LocalVBMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride, true, 0xff);
                OK = SwapBuffers(SourceBuffer, DestinationBuffer, Width, Height, Stride);
                OK = LocalHBMirror(SourceBuffer, DestinationBuffer, Width, Height, Stride, true, 0xff);
            }
            return OK;
#else
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                if (ByPixel)
                    OpReturn = (ReturnCode)ULtoLR(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                            DestinationBuffer.BackBuffer.ToPointer());
                else
                    OpReturn = (ReturnCode)ULtoLRByte(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                       DestinationBuffer.BackBuffer.ToPointer(), true, 0xff);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
#endif
        }

        /// <summary>
        /// Mirror an image.
        /// </summary>
        /// <param name="SourceBuffer">The image to mirror.</param>
        /// <param name="DestinationBuffer">The destination of the mirroring operation.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="MirrorHow">The mirroring operation.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ImageMirror (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
           Mirroring MirrorHow)
        {
            if (!ValidateImages(SourceBuffer, DestinationBuffer))
                return false;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                switch (MirrorHow)
                {
                    case Mirroring.HorizontalByte:
                        OpReturn = (ReturnCode)HorizontalMirrorByte(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                            DestinationBuffer.BackBuffer.ToPointer(), true, 0xff);
                        break;

                    case Mirroring.HorizontalPixel:
                        OpReturn = (ReturnCode)HorizontalMirror(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                             DestinationBuffer.BackBuffer.ToPointer());
                        break;

                    case Mirroring.VerticalByte:
                        OpReturn = (ReturnCode)VerticalMirrorByte(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                             DestinationBuffer.BackBuffer.ToPointer(), true, 0xff);
                        break;

                    case Mirroring.VerticalPixel:
                        OpReturn = (ReturnCode)VerticalMirror(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                            DestinationBuffer.BackBuffer.ToPointer());
                        break;

                    case Mirroring.BothByte:
                        OpReturn = (ReturnCode)ULtoLRByte(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                       DestinationBuffer.BackBuffer.ToPointer(), true, 0xff);
                        break;

                    case Mirroring.BothPixel:
                        OpReturn = (ReturnCode)ULtoLR(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                            DestinationBuffer.BackBuffer.ToPointer());
                        break;
                }

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_SegmentizeImage@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SegmentizeImage (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
                     Int32 CellWidth, Int32 CellHeight, Int32 CellOriginX, Int32 CellOriginY, Int32 SegmentPattern);

        public bool ImageSegmentize (WriteableBitmap SourceBuffer, int Width, int Height, int Stride,
            WriteableBitmap DestinationBuffer, int CellWidth, int CellHeight, Point CellOrigin, SegmentizationMirroring SegmentHow)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)SegmentizeImage(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                             DestinationBuffer.BackBuffer.ToPointer(), CellWidth, CellHeight, (int)CellOrigin.X, (int)CellOrigin.Y, (int)SegmentHow);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HorizontalMirrorPixelRegion@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HorizonalMirrorPixelRegion (void* Source, Int32 Width, Int32 Height, void* Destination, int X1, int Y1, int X2, int Y2);

        public bool ImageRegionHorizontalMirror (WriteableBitmap SourceBuffer, int Width, int Height, 
              WriteableBitmap DestinationBuffer, Point UL, Point LR)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)HorizonalMirrorPixelRegion(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                             DestinationBuffer.BackBuffer.ToPointer(), (int)UL.X, (int)UL.Y, (int)LR.X, (int)LR.Y);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_VerticalMirrorPixelRegion@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int VerticalMirrorPixelRegion (void* Source, Int32 Width, Int32 Height, void* Destination, int X1, int Y1, int X2, int Y2);

        public bool ImageRegionVerticalMirror (WriteableBitmap SourceBuffer, int Width, int Height, 
              WriteableBitmap DestinationBuffer, Point UL, Point LR)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)VerticalMirrorPixelRegion(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                             DestinationBuffer.BackBuffer.ToPointer(), (int)UL.X, (int)UL.Y, (int)LR.X, (int)LR.Y);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll",EntryPoint ="_SquishImage@40",ExactSpelling =true,CallingConvention =CallingConvention.StdCall)]
        static extern unsafe int SquishImage (void* Source, Int32 Width, Int32 Height, Int32 Stride,
               void* Destination, Int32 DestWidth, Int32 DestHeight, Int32 DestStride,
               Int32 HorizontalFrequency, Int32 VerticalFrequency);

        public bool ImageSquisher(WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer,
            int DestWidth,int DestHeight,int HorizontalFrequency, int VerticalFrequency)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)SquishImage(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                    SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(), DestinationBuffer.PixelWidth,
                    DestinationBuffer.PixelHeight, DestinationBuffer.BackBufferStride,
                    HorizontalFrequency, VerticalFrequency);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }

    public enum Mirroring : int
    {
        HorizontalPixel,
        HorizontalByte,
        VerticalPixel,
        VerticalByte,
        BothPixel,
        BothByte
    }

    public enum SegmentizationMirroring : int
    {
        SegmentNoMirror = 0,
        SegmentHorizontalMirror = 1,
        SegmentVerticalMirror = 2,
    }
}
