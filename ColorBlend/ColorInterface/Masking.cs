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
        [DllImport("ColorBlender.dll", EntryPoint = "_CreateBitMask@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool CreateBitMask (void* Target, Int32 TargetWidth, Int32 TargetHeight,
       Int32 Left, Int32 Top, Int32 Width, Int32 Height, byte BitOnValue, byte BitOffValue);

        [DllImport("ColorBlender.dll", EntryPoint = "_CreateMask@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool CreateMask (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
              void* ImageSource, UInt32 Threshold, bool AlphaToo,
              byte MaskA, byte MaskR, byte MaskG, byte MaskB);

        [DllImport("ColorBlender.dll", EntryPoint = "_CreateMaskFromLuminance@48", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool CreateMaskFromLuminance (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride,
              void* ImageSource, double Threshold, bool AlphaToo,
              byte MaskA, byte MaskR, byte MaskG, byte MaskB);

        [DllImport("ColorBlender.dll", EntryPoint = "_AlphaFromLuminance@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AlphaFromLuminance (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool Invert);

        /// <summary>
        /// Create an image whose alpha channel levels are based on the luminance levels of the source.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Invert">Invert luminance threshold.</param>
        /// <returns>True on success, false on error.</returns>
        public bool AlphaFromLuminance (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, bool Invert)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)AlphaFromLuminance(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), Invert);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_SetAlphaChannel@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SetAlphaChannel (void* Target, Int32 TargetWidth, Int32 TargetHeight, Int32 TargetStride, void* Destination, byte NewAlpha);

        /// <summary>
        /// Set all pixels to the supplied alpha level.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="NewAlpha">The alpha level the image will be set to.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool UnconditionalAlphaSet (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, byte NewAlpha)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SetAlphaChannel(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), NewAlpha);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_SetAlphaChannelInPace@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SetAlphaChannelInPlace (void* Buffer, Int32 Width, Int32 Height, Int32 TStride, byte NewAlpha);

        /// <summary>
        /// Set all pixels to the supplied alpha level in a buffer.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="NewAlpha">The alpha level the image will be set to.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool AlphaChannelInPlaceSet (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride, byte NewAlpha)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SetAlphaChannelInPlace(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                                                              NewAlpha);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_MaskByColor@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int MaskByColor (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
                 UInt32 LowMaskColor, UInt32 HighMaskColor, byte AlphaValue);

        /// <summary>
        /// Change all pixels in the source whose color is <paramref name="MaskColor"/> to transparent.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="MaskColor">The color that will be converted to transparent in the destination.</param>
        /// <returns>True on succes, false on failure.</returns>
        public bool MaskWithColor (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            Color MaskColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)MaskByColor(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), MaskColor.ToARGB(), MaskColor.ToARGB(), 0x0);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ConditionalAlphaFromLuminance@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ConditionalAlphaFromLuminance (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
           double LuminanceThreshold, bool Invert, UInt32 MaskPixel);

        /// <summary>
        /// Change the alpha levels of pixels to transparent if they meet the luminance threshold in <paramref name="Threshold"/>.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Threshold">Luminance level that determines whether a pixel is turned transparent.</param>
        /// <param name="Invert">Invert luminance threshold.</param>
        /// <param name="MaskPixel">The color to use for masked pixels.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ConditionalAlphaMask (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
                double Threshold, bool Invert, Color MaskPixel)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                UInt32 MaskPixelColor = MaskPixel.ToARGB();
                OpReturn = (ReturnCode)ConditionalAlphaFromLuminance(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), Threshold, Invert, MaskPixelColor);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_AlphaSolarize@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AlphaSolarize (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, double Luminance);

        /// <summary>
        /// Solarize the alpha level.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Threshold">Determines if the alpha channel is solarized or not.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool AlphaSolarizeImage (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
               double Threshold)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)AlphaSolarize(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), Threshold);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FrequencyActionBlock
        {
            public int Action;
            public double NewAlpha;
            public double NewLuminance;
            public UInt32 NewColor;
            public int HorizontalFrequency;
            public int VerticalFrequency;
            public bool IncludeAlpha;
            public int ColorAlphaAction;
        };

        [DllImport("ColorBlender.dll", EntryPoint = "_ActionByFrequency@68", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ActionByFrequency (void* Source, Int32 Width, Int32 Height, Int32 Stride,
              void* Destination,  FrequencyActionBlock FrequencyAction);

        public bool FrequencyAction (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
              FrequencyActionBlock ActionBlock)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ActionByFrequency(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(),  ActionBlock);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
