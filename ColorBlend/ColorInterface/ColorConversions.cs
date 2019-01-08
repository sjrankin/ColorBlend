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
        #region RGB and HSL
#if true
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoHSL2@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoHSL2A(byte R, byte G, byte B, double* H, double* S, double* L);

        public bool ConvertRGBtoHSL2A(byte Red, byte Green, byte Blue, out double Hue, out double Saturation, out double Luminance)
        {
            Hue = 0.0;
            Saturation = 0.0;
            Luminance = 0.0;

            unsafe
            {
                double rawH = 0.0;
                double* H = (double*)&rawH;
                double rawS = 0.0;
                double* S = (double*)&rawS;
                double rawL = 0.0;
                double* L = (double*)&rawL;
                RGBtoHSL2A(Red, Green, Blue, H, S, L);
                Hue = rawH;
                Saturation = rawS;
                Luminance = rawL;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoHSL@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoHSL (double R, double G, double B, double* H, double* S, double* L);

        /// <summary>
        /// Convert the RGB color (<paramref name="Red"/>, <paramref name="Green"/>, <paramref name="Blue"/>) to an HSL value.
        /// </summary>
        /// <param name="Red">Red channel value.</param>
        /// <param name="Green">Green channel value.</param>
        /// <param name="Blue">Blue channel value.</param>
        /// <param name="Hue">Hue equivalent of the RGB color.</param>
        /// <param name="Saturation">Saturation equivalent of the RGB color.</param>
        /// <param name="Luminance">Luminance equivalent of the RGB color.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ConvertRGBtoHSLByte (byte Red, byte Green, byte Blue, out double Hue, out double Saturation, out double Luminance)
        {
            Hue = 0.0;
            Saturation = 0.0;
            Luminance = 0.0;

            unsafe
            {
                double rawH = 0.0;
                double* H = (double*)&rawH;
                double rawS = 0.0;
                double* S = (double*)&rawS;
                double rawL = 0.0;
                double* L = (double*)&rawL;
                RGBtoHSL2(Red, Green, Blue, H, S, L);
                Hue = rawH;
                Saturation = rawS;
                Luminance = rawL;
            }

            return true;
        }
#endif

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoHSL@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoHSL2 (double R, double G, double B, double* H, double* S, double* L);

        public bool ConvertRGBtoHSLDouble (double Red, double Green, double Blue, out double Hue, out double Saturation, out double Luminance)
        {
            Hue = 0.0;
            Saturation = 0.0;
            Luminance = 0.0;

            unsafe
            {
                double rawH = 0.0;
                double* H = (double*)&rawH;
                double rawS = 0.0;
                double* S = (double*)&rawS;
                double rawL = 0.0;
                double* L = (double*)&rawL;
                RGBtoHSL2(Red, Green, Blue, H, S, L);
                Hue = rawH;
                Saturation = rawS;
                Luminance = rawL;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HSLtoRGB2@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void HSLtoRGB2 (double H, double S, double L, byte* R, byte* G, byte* B);

        public bool ConvertHSLToRGBByte (double Hue, double Saturation, double Luminance, out byte Red, out byte Green, out byte Blue)
        {
            Red = 0;
            Green = 0;
            Blue = 0;

            unsafe
            {
                byte rawR = 0;
                byte* R = (byte*)&rawR;
                byte rawG = 0;
                byte* G = (byte*)&rawG;
                byte rawB = 0;
                byte* B = (byte*)&rawB;
                HSLtoRGB2(Hue, Saturation, Luminance, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HSLtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void HSLtoRGB (double H, double S, double L, double* R, double* G, double* B);

        public bool ConvertHSLToRGBDouble (double Hue, double Saturation, double Luminance, out double Red, out double Green, out double Blue)
        {
            Red = 0.0;
            Green = 0.0;
            Blue = 0.0;

            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                HSLtoRGB(Hue, Saturation, Luminance, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and HSV
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoHSV@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoHSV (double R, double G, double B, double* H, double* S, double* V);

        public bool ConvertRGBtoHSV (byte Red, byte Green, byte Blue, out double Hue, out double Saturation, out double Value)
        {
            Hue = 0.0;
            Saturation = 0.0;
            Value = 0.0;

            unsafe
            {
                double rawH = 0.0;
                double* H = (double*)&rawH;
                double rawS = 0.0;
                double* S = (double*)&rawS;
                double rawL = 0.0;
                double* L = (double*)&rawL;
                RGBtoHSV(Red, Green, Blue, H, S, L);
                Hue = rawH;
                Saturation = rawS;
                Value = rawL;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HSVtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void HSVtoRGB (double H, double S, double V, double* R, double* G, double* B);

        public bool ConvertHSVToRGB (double Hue, double Saturation, double Value, out double Red, out double Green, out double Blue)
        {
            Red = 0.0;
            Green = 0.0;
            Blue = 0.0;

            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                HSVtoRGB(Hue, Saturation, Value, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and YUV
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoYUV@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoYUV (double R, double G, double B, double* Y, double* U, double* V);

        public bool ConvertRGBtoYUV (byte Red, byte Green, byte Blue, out double Y, out double U, out double V)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoYUV(Red, Green, Blue, ly, lu, lv);
                Y = rawY;
                U = rawU;
                V = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_YUVtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void YUVtoRGB (double Y, double U, double V, double* R, double* G, double* B);

        public bool ConvertYUVtoRGB (double Y, double U, double V, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                YUVtoRGB(Y, U, V, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and YCbCr
        [DllImport("ColorBlender.dll", EntryPoint = "_YCbCrtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void YCbCrtoRGB (double Y, double Cb, double Cr, double* R, double* G, double* B);

        public bool ConvertYCbCrtoRGB (double Y, double Cb, double Cr, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                YUVtoRGB(Y, Cb, Cr, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoYCbCr@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoYCbCr (double R, double G, double B, double* Y, double* U, double* V);

        public bool ConvertRGBtoYCbCr (byte Red, byte Green, byte Blue, out double Y, out double Cb, out double Cr)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoYUV(Red, Green, Blue, ly, lu, lv);
                Y = rawY;
                Cb = rawU;
                Cr = rawV;
            }

            return true;
        }
#endregion

#region RGB and YIQ
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoYIQ@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoYIQ (double R, double G, double B, double* Y, double* I, double* Q);

        public bool ConvertRGBtoYIQ (byte Red, byte Green, byte Blue, out double Y, out double I, out double Q)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoYIQ(Red, Green, Blue, ly, lu, lv);
                Y = rawY;
                I = rawU;
                Q = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_YIQtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void YIQtoRGB (double Y, double I, double Q, double* R, double* G, double* B);

        public bool ConvertYIQtoRGB (double Y, double I, double Q, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                YIQtoRGB(Y, I, Q, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and XYZ
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoXYZ@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoXYZ (double R, double G, double B, double* X, double* Y, double* Z);

        public bool ConvertRGBtoXYZ (byte Red, byte Green, byte Blue, out double X, out double Y, out double Z)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoXYZ(Red, Green, Blue, ly, lu, lv);
                X = rawY;
                Y = rawU;
                Z = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_XYZtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void XYZtoRGB (double X, double Y, double Z, double* R, double* G, double* B);

        public bool ConvertXYZtoRGB (double X, double Y, double Z, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                XYZtoRGB(X, Y, Z, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and CIELab
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoCIELAB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoCIELAB (double R, double G, double B, double* L, double* A, double* lB);

        public bool ConvertRGBtoCIELAB (byte Red, byte Green, byte Blue, out double L, out double A, out double B)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoCIELAB(Red, Green, Blue, ly, lu, lv);
                L = rawY;
                A = rawU;
                B = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_CIELABtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void CIELABtoRGB (double L, double A, double B, double* R, double* G, double* Bl);

        public bool ConvertCIELABtoRGB (double L, double A, double B, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* Bl = (double*)&rawB;
                CIELABtoRGB(L, A, B, R, G, Bl);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region XYZ and CIELab
        [DllImport("ColorBlender.dll", EntryPoint = "_XYZtoCIELAB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void XYZtoCIELAB (double R, double G, double B, double* L, double* A, double* lB);

        public bool ConvertXYZtoCIELAB (double X, double Y, double Z, out double L, out double A, out double B)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                XYZtoCIELAB(X, Y, Z, ly, lu, lv);
                L = rawY;
                A = rawU;
                B = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_CIELABtoXYZ@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void CIELABtoXYZ (double L, double A, double B, double* X, double* Y, double* Z);

        public bool ConvertCIELABtoXYZ (double L, double A, double B, out double X, out double Y, out double Z)
        {
            unsafe
            {
                double rawX = 0.0;
                double* lx = (double*)&rawX;
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawZ = 0.0;
                double* lz = (double*)&rawZ;
                CIELABtoXYZ(L, A, B, lx, ly, lz);
                X = rawX;
                Y = rawY;
                Z = rawZ;
            }

            return true;
        }
#endregion

#region RGB and CMY
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoCMY@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoCMY (double R, double G, double B, double* C, double* M, double* Y);

        public bool ConvertRGBtoCMY (byte Red, byte Green, byte Blue, out double C, out double M, out double Y)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoCMY(Red, Green, Blue, ly, lu, lv);
                C = rawY;
                M = rawU;
                Y = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_CMYtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void CMYtoRGB (double C, double M, double Y, double* R, double* G, double* B);

        public bool ConvertCMYtoRGB (double C, double M, double Y, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                CMYtoRGB(C, M, Y, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and CMYK
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoCMYK@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoCMYK (double R, double G, double B, double* C, double* M, double* Y, double* K);

        public bool ConvertRGBtoCMYK (byte Red, byte Green, byte Blue, out double C, out double M, out double Y, out double K)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double *)&rawY;
                double rawU = 0.0;
                double* lu = (double *)&rawU;
                double rawV = 0.0;
                double* lv = (double *)&rawV;
                double rawK = 0.0;
                double* lk = (double *)&rawK;
                RGBtoCMYK(Red, Green, Blue, ly, lu, lv, lk);
                C = rawY;
                M = rawU;
                Y = rawV;
                K = rawK;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_CMYKtoRGB@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void CMYKtoRGB (double C, double M, double Y, double K, double* R, double* G, double* B);

        public bool ConvertCMYKtoRGB (double C, double M, double Y, double K, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* R = (double*)&rawR;
                double rawG = 0.0;
                double* G = (double*)&rawG;
                double rawB = 0.0;
                double* B = (double*)&rawB;
                CMYKtoRGB(C, M, Y, K, R, G, B);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and RYB
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoRYB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoRYB (double R, double G, double B, double* Rp, double* Yp, double* Bp);

        public bool ConvertRGBtoRYB (byte Red, byte Green, byte Blue, out double R, out double Y, out double B)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoRYB(Red, Green, Blue, ly, lu, lv);
                R = rawY;
                Y = rawU;
                B = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RYBtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RYBtoRGB (double R, double Y, double B, double* Rp, double* Gp, double* Bp);

        public bool ConvertRYBtoRGB (double R, double Y, double B, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* lR = (double*)&rawR;
                double rawG = 0.0;
                double* lG = (double*)&rawG;
                double rawB = 0.0;
                double* lB = (double*)&rawB;
                RYBtoRGB(R, Y, B, lR, lG, lB);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and TSL
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoTSL@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoTSL (double R, double G, double B, double* T, double* S, double* L);

        public bool ConvertRGBtoTSL (byte Red, byte Green, byte Blue, out double T, out double S, out double L)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoTSL(Red, Green, Blue, ly, lu, lv);
                T = rawY;
                S = rawU;
                L = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_TSLtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void TSLtoRGB (double T, double S, double L, double* Rp, double* Gp, double* Bp);

        public bool ConvertTSLtoRGB (double T, double S, double L, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* lR = (double*)&rawR;
                double rawG = 0.0;
                double* lG = (double*)&rawG;
                double rawB = 0.0;
                double* lB = (double*)&rawB;
                TSLtoRGB(T, S, L, lR, lG, lB);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and YDbDr
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoYDbDr@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoYDbDr (double R, double G, double B, double* Y, double* Db, double* Dr);

        public bool ConvertRGBtoYDbDr (byte Red, byte Green, byte Blue, out double Y, out double Db, out double Dr)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoYDbDr(Red, Green, Blue, ly, lu, lv);
                Y = rawY;
                Db = rawU;
                Dr = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_YDbDrtoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void YDbDrtoRGB (double Y, double Db, double Dr, double* R, double* G, double* B);

        public bool ConvertYDbDrtoRGB (double Y, double Db, double Dr, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* lR = (double*)&rawR;
                double rawG = 0.0;
                double* lG = (double*)&rawG;
                double rawB = 0.0;
                double* lB = (double*)&rawB;
                YDbDrtoRGB(Y, Db, Dr, lR, lG, lB);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion

#region RGB and YCgCo
        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoYCgCo@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void RGBtoYCgCo (double R, double G, double B, double* Y, double* Cg, double* Co);

        public bool ConvertRGBtoYCgCo (byte Red, byte Green, byte Blue, out double Y, out double Cg, out double Co)
        {
            unsafe
            {
                double rawY = 0.0;
                double* ly = (double*)&rawY;
                double rawU = 0.0;
                double* lu = (double*)&rawU;
                double rawV = 0.0;
                double* lv = (double*)&rawV;
                RGBtoYCgCo(Red, Green, Blue, ly, lu, lv);
                Y = rawY;
                Cg = rawU;
                Co = rawV;
            }

            return true;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_YCgCotoRGB@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void YCgCotoRGB (double Y, double Cg, double Co, double* R, double* G, double* B);

        public bool ConvertYCgCotoRGB (double Y, double Cg, double Co, out double Red, out double Green, out double Blue)
        {
            unsafe
            {
                double rawR = 0.0;
                double* lR = (double*)&rawR;
                double rawG = 0.0;
                double* lG = (double*)&rawG;
                double rawB = 0.0;
                double* lB = (double*)&rawB;
                YCgCotoRGB(Y, Cg, Co, lR, lG, lB);
                Red = rawR;
                Green = rawG;
                Blue = rawB;
            }

            return true;
        }
#endregion
    }
}
