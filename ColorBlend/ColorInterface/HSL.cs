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
        [DllImport("ColorBlender.dll", EntryPoint = "_IsolateHues@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int IsolateHues(void* Source, Int32 Width, Int32 Height, Int32 Stride,
             void* Destination, double HueRangeStart, double HueRangeEnd, int IsolateForegroundOp,
             int IsolateBackgroundOp);

        public enum HueIsolationOps : int
        {
            Copy = 5,
            Black = 2,
            White = 3,
            Gray = 1,
            Clear = 0,
            Desaturate = 6,
            Deluminate = 7,
            Grayscale = 4,
            Unknown = -1
        }

        public bool IsolateHueRange(WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
            double RangeStart, double RangeEnd, HueIsolationOps InRangeOp, HueIsolationOps OutOfRangeOp,
            out ColorBlenderInterface.ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)IsolateHues(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), RangeStart, RangeEnd, (int)InRangeOp, (int)OutOfRangeOp);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_NormalizedRGBtoHSL@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void NormalizedRGBtoHSL(double R, double G, double B, double* H, double* S, double* L);

        public void NativeRGBtoHSL(byte R, byte G, byte B, out double H, out double S, out double L)
        {
            H = 0.0;
            S = 0.0;
            L = 0.0;
            unsafe
            {
                double Rn = R / 255.0;
                double Gn = G / 255.0;
                double Bn = B / 255.0;
                double HResult = 0.0;
                double SResult = 0.0;
                double LResult = 0.0;
                NormalizedRGBtoHSL(Rn, Gn, Bn, &HResult, &SResult, &LResult);
                H = HResult;
                S = SResult;
                L = LResult;
            }
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HSLtoNormalizedRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void HSLtoNormalizedRGB(double H, double S, double L, double* R, double* G, double* B);

        /// <summary>
        /// Convert an HSL value into an RGB value.
        /// </summary>
        /// <param name="H">Source hue channel.</param>
        /// <param name="S">Source saturation channel.</param>
        /// <param name="L">Source luminance channel.</param>
        /// <param name="R">Resultant red channel.</param>
        /// <param name="G">Resultant green channel.</param>
        /// <param name="B">Resultant blue channel.</param>
        public void NativeHSLtoRGB(double H, double S, double L, out byte R, out byte G, out byte B)
        {
            R = 0x0;
            G = 0x0;
            B = 0x0;
            unsafe
            {
                double Rn = 0.0;
                double Gn = 0.0;
                double Bn = 0.0;
                HSLtoNormalizedRGB(H, S, L, &Rn, &Gn, &Bn);
                R = (byte)(Rn * 255.0);
                G = (byte)(Gn * 255.0);
                B = (byte)(Bn * 255.0);
            }
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ShiftHue@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void ShiftHue(byte* R, byte* G, byte* B, int AngleOffset);

        /// <summary>
        /// Rotate the RGB value by <paramref name="Rotation"/> degrees and return the result.
        /// </summary>
        /// <param name="R">Source red channel.</param>
        /// <param name="G">Source green channel.</param>
        /// <param name="B">Source blue channel.</param>
        /// <param name="NewR">Resultant red channel.</param>
        /// <param name="NewG">Resultant green channel.</param>
        /// <param name="NewB">Resultant blue channel.</param>
        /// <param name="Rotation">How far to rotate the source color.</param>
        public void RotateHSLHue(byte R, byte G, byte B, out byte NewR, out byte NewG, out byte NewB,
            int Rotation)
        {
            NewR = 0x0;
            NewG = 0x0;
            NewB = 0x0;
            unsafe
            {
                ShiftHue(&R, &G, &B, Rotation);
                NewR = R;
                NewG = G;
                NewB = B;
            }
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ClampHue@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe double ClampHue(double Hue, double Rotation);

        public double ClampedHue(double StartingHue, double Rotation)
        {
            unsafe
            {
                return ClampHue(StartingHue, Rotation);
            }
        }
    }
}
