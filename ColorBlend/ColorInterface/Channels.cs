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
        [DllImport("ColorBlender.dll", EntryPoint = "_SplitImageIntoChannels@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SplitImageIntoChannels (void* Source, Int32 Width, Int32 Height, Int32 Stride,
            void* AlphaDest, void* RedDest, void* GreenDest, void* BlueDest);

        public bool RGBImageSplit (WriteableBitmap Source, int Width, int Height, int Stride,
            WriteableBitmap AlphaBuffer, WriteableBitmap RedBuffer, WriteableBitmap GreenBuffer, WriteableBitmap BlueBuffer)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                RedBuffer.Lock();
                GreenBuffer.Lock();
                BlueBuffer.Lock();
                Source.Lock();

                OpReturn = (ReturnCode)SplitImageIntoChannels(Source.BackBuffer.ToPointer(), Width, Height, Stride,
                    AlphaBuffer.BackBuffer.ToPointer(), RedBuffer.BackBuffer.ToPointer(), GreenBuffer.BackBuffer.ToPointer(),
                    BlueBuffer.BackBuffer.ToPointer());

                System.Windows.Int32Rect DirtyRectA = new Int32Rect(0, 0, AlphaBuffer.PixelWidth, AlphaBuffer.PixelHeight);
                AlphaBuffer.AddDirtyRect(DirtyRectA);
                AlphaBuffer.Unlock();
                System.Windows.Int32Rect DirtyRectR = new Int32Rect(0, 0, RedBuffer.PixelWidth, RedBuffer.PixelHeight);
                RedBuffer.AddDirtyRect(DirtyRectR);
                RedBuffer.Unlock();
                System.Windows.Int32Rect DirtyRectG = new Int32Rect(0, 0, GreenBuffer.PixelWidth, GreenBuffer.PixelHeight);
                GreenBuffer.AddDirtyRect(DirtyRectG);
                GreenBuffer.Unlock();
                System.Windows.Int32Rect DirtyRectB = new Int32Rect(0, 0, BlueBuffer.PixelWidth, BlueBuffer.PixelHeight);
                BlueBuffer.AddDirtyRect(DirtyRectB);
                BlueBuffer.Unlock();

                Source.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBCombine@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RGBCombine (void* RedSource, void* GreenSource, void* BlueSource, Int32 Width, Int32 Height, Int32 Stride,
            void* Destination, byte AlphaValue);

        public bool RGBImageCombine (WriteableBitmap RedBuffer, WriteableBitmap GreenBuffer, WriteableBitmap BlueBuffer,
            int Width, int Height, int Stride, WriteableBitmap Destination, byte AlphaValue = 255)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                RedBuffer.Lock();
                GreenBuffer.Lock();
                BlueBuffer.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)RGBCombine(RedBuffer.BackBuffer.ToPointer(), GreenBuffer.BackBuffer.ToPointer(),
                    BlueBuffer.BackBuffer.ToPointer(), Width, Height, Stride, Destination.BackBuffer.ToPointer(), AlphaValue);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                BlueBuffer.Unlock();
                GreenBuffer.Unlock();
                RedBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HighlightImageColor@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HighlightImageColor (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
             double HighlightHue, int NonHighlightAction, double HighlightLuminance, ref double HueDelta);

        public enum ImageColorHighlights : int
        {
            HighlightRed = 0,
            HighlightGreen = 1,
            HighlightBlue = 2,
            HighlightYellow = 3,
            HighlightCyan = 4,
            HighlightMagenta = 5,
        }

        public enum ImageColorNonHighlightAction : int
        {
            NonHighlightGrayscale = 0,
            NonHighlightTransparent = 1,
            NonHighlightInvert = 2,
        }

        double RGBtoHue (double R, double G, double B)
        {
            double Max = Math.Max(R, Math.Max(G, B));
            double Min = Math.Min(R, Math.Min(G, B));
            double Delta = Max - Min;
            double Hue = 0.0;

            if (Max == R)
            {
                Hue = 60.0 * ((G - B) / Delta);
//                Hue = 60.0 * (double)((int)((G - B) / Delta)/* % 6*/);
            }
            else
                if (Max == G)
            {
                Hue = 60.0 * (((B - R) / Delta) /*+ 2*/);
            }
            else
            {
                Hue = 60.0 * (((R - G) / Delta) /*+ 4*/);
            }
            if (Hue < 0.0)
                Hue += 360.0;
            return Hue;
        }

        double RGBtoHue2 (byte R, byte G, byte B)
        {
            double sR = (double)R / 255.0;
            double sG = (double)G / 255.0;
            double sB = (double)B / 255.0;
            return RGBtoHue(sR, sG, sB);
        }

        public bool HighlightColorInImage (WriteableBitmap Source, WriteableBitmap Destination, int Width, int Height, int Stride,
          double HighlightHue, ImageColorNonHighlightAction NonHighlightAction, ref double HueDelta,
            Nullable<double> HighlightLuminance = null)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            if (!HighlightLuminance.HasValue)
                HighlightLuminance = 1.0;
            HueDelta = 0.0;
            unsafe
            {
                    Source.Lock();
                    Destination.Lock();
                    OpReturn = (ReturnCode)HighlightImageColor(Source.BackBuffer.ToPointer(), Source.PixelWidth, Source.PixelHeight,
                        Stride, Destination.BackBuffer.ToPointer(), HighlightHue,
                        (int)NonHighlightAction, HighlightLuminance.Value, ref HueDelta);
                    System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                    Destination.AddDirtyRect(DirtyRect);
                    Destination.Unlock();
                    Source.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_AdjustImageHSL@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AdjustImageHSL (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
             double HueAdjustment, double SaturationAdjustment, double LuminanceAdjustment);

        public bool AdjustImageByHSL(WriteableBitmap Source, WriteableBitmap Destination, int Width, int Height,
            double HueAdjustment, double SaturationAdjustment, double LuminanceAdjustment)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;

            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)AdjustImageHSL(Source.BackBuffer.ToPointer(), Source.PixelWidth, Source.PixelHeight,
                        Source.BackBufferStride, Destination.BackBuffer.ToPointer(), HueAdjustment, SaturationAdjustment, 
                        LuminanceAdjustment);

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
