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
    // Contains interop wrappers for convolution-related functions.
    public partial class ColorBlenderInterface
    {
        [DllImport("ColorBlender.dll", EntryPoint = "_MasterConvolveWithKernel@88", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int MasterConvolveWithKernel (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, UInt32 PackedBGPixel,
             void* KernelMatrix, int KernelX, int KernelY, double Bias, double Factor,
             bool UseAlpha, bool UseRed, bool UseGreen, bool UseBlue, bool SkipTransparentPixels, bool IncludeTransparentPixels,
             bool UseLuminance, double LuminanceThreshold);

        /// <summary>
        /// Run a convolution kernel on <paramref name="SourceBuffer"/>.
        /// </summary>
        /// <param name="SourceBuffer">The source image.</param>
        /// <param name="DestinationBuffer">Where the result will be written.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Kernel">The kernel type to run.</param>
        /// <param name="SkipTransparentPixels">If true, transparent pixels will not be convolved.</param>
        /// <param name="IncludeTransparentPixels">If false, transparent pixels will not be used in convolution operations.</param>
        /// <param name="UseLuminance">If true, only pixels that have a luminance greater than <paramref name="Luminance"/> will be convolved.</param>
        /// <param name="Luminance">Luminance threshold to use if <paramref name="UseLuminance"/> is true.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool MasterImageKernalConvolution (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
              List<double> DoubleList, int MatrixWidth, int MatrixHeight, double Bias, double Factor,
              bool SkipTransparentPixels, bool IncludeTransparentPixels, bool UseLuminance, double Luminance)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            if (DoubleList == null)
                return false;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                double[] Matrix = new double[MatrixWidth * MatrixHeight];
                for (int i = 0; i < MatrixWidth * MatrixHeight; i++)
                    Matrix[i] = DoubleList[i];
                UInt32 BGColor = 0xff000000;
                fixed (void* Doubles = Matrix)
                {
                    OpReturn = (ReturnCode)MasterConvolveWithKernel(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), BGColor, Doubles, MatrixWidth, MatrixHeight, Bias, Factor,
                        false, true, true, true,
                        SkipTransparentPixels, IncludeTransparentPixels, UseLuminance, Luminance);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Run a convolution kernel on <paramref name="SourceBuffer"/>.
        /// </summary>
        /// <param name="SourceBuffer">The source image.</param>
        /// <param name="DestinationBuffer">Where the result will be written.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Kernel">The kernel type to run.</param>
        /// <param name="SkipTransparentPixels">If true, transparent pixels will not be convolved.</param>
        /// <param name="IncludeTransparentPixels">If false, transparent pixels will not be used in convolution operations.</param>
        /// <param name="UseLuminance">If true, only pixels that have a luminance greater than <paramref name="Luminance"/> will be convolved.</param>
        /// <param name="Luminance">Luminance threshold to use if <paramref name="UseLuminance"/> is true.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool MasterImageKernalConvolution (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
        KernelTypes Kernel, bool SkipTransparentPixels, bool IncludeTransparentPixels, bool UseLuminance, double Luminance)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            KernelDefintion KD = GetKernel(Kernel);
            if (KD == null)
                return false;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                double[] Matrix = new double[KD.Width * KD.Height];
                for (int i = 0; i < KD.Width * KD.Height; i++)
                    Matrix[i] = KD.Matrix[i];
                UInt32 BGColor = 0xff000000;
                fixed (void* Doubles = Matrix)
                {
                    OpReturn = (ReturnCode)MasterConvolveWithKernel(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), BGColor, Doubles, KD.Width, KD.Height, KD.Bias, KD.Factor,
                        false, true, true, true,
                        SkipTransparentPixels, IncludeTransparentPixels, UseLuminance, Luminance);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ConvolveWithKernel2@52", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ConvolveWithKernel2 (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
          UInt32 PackedDestBG, void* Kernel, int KernelX, int KernelY, double Bias, double Factor);

        /// <summary>
        /// Run a convolution kernel on <paramref name="SourceBuffer"/>.
        /// </summary>
        /// <param name="SourceBuffer">The source image.</param>
        /// <param name="DestinationBuffer">Where the result will be written.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Kernel">The kernel type to run.</param>
        /// <param name="DestBGColor">Background color for the destination.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ImageKernelConvolution (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
        KernelTypes Kernel, Nullable<Color> DestBGColor = null)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            KernelDefintion KD = GetKernel(Kernel);
            if (KD == null)
                return false;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                double[] Matrix = new double[KD.Width * KD.Height];
                for (int i = 0; i < KD.Width * KD.Height; i++)
                    Matrix[i] = KD.Matrix[i];
                UInt32 BGColor = DestBGColor.HasValue ? DestBGColor.Value.ToBGRA() : 0xff000000;
                fixed (void* Doubles = Matrix)
                {
                    OpReturn = (ReturnCode)ConvolveWithKernel2(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), BGColor, Doubles, KD.Width, KD.Height, KD.Bias, KD.Factor);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ConvolveWithKernel3@72", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ConvolveWithKernel3 (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, UInt32 PackedBGPixel,
           void* KernelMatrix, int KernelX, int KernelY, double Bias, double Factor, bool SkipTransparentPixels, bool IncludeTransparentPixels,
           bool UseLuminance, double Luminance);

        /// <summary>
        /// Run a convolution kernel on <paramref name="SourceBuffer"/>.
        /// </summary>
        /// <param name="SourceBuffer">The source image.</param>
        /// <param name="DestinationBuffer">Where the result will be written.</param>
        /// <param name="Width">Width of the source and destination.</param>
        /// <param name="Height">Height of the source and destination.</param>
        /// <param name="Stride">Stride of the source and destination.</param>
        /// <param name="Kernel">The kernel type to run.</param>
        /// <param name="DestBGColor">Background color for the destination.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ImageKernelConvolution2 (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
        KernelTypes Kernel, bool SkipTransparentPixels, bool IncludeTransparentPixels, bool UseLuminance,
        double Luminance, Nullable<Color> DestBGColor = null)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            KernelDefintion KD = GetKernel(Kernel);
            if (KD == null)
                return false;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                double[] Matrix = new double[KD.Width * KD.Height];
                for (int i = 0; i < KD.Width * KD.Height; i++)
                    Matrix[i] = KD.Matrix[i];
                UInt32 BGColor = DestBGColor.HasValue ? DestBGColor.Value.ToBGRA() : 0xff000000;
                fixed (void* Doubles = Matrix)
                {
                    OpReturn = (ReturnCode)ConvolveWithKernel3(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(), BGColor, Doubles, KD.Width, KD.Height, KD.Bias, KD.Factor, SkipTransparentPixels,
                        IncludeTransparentPixels, UseLuminance, Luminance);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// List of pre-defined kernels.
        /// </summary>
        /// <remarks>
        /// http://homepages.inf.ed.ac.uk/rbf/HIPR2/filtops.htm
        /// </remarks>
        public List<KernelDefintion> Kernels = new List<KernelDefintion>()
    {
        {new KernelDefintion(KernelTypes.Blur3x3, 1.0, 0.0, 3, 3,
            0.0, 0.2, 0.0,
            0.2, 0.2, 0.2,
            0.0, 0.2, 0.2) },
        {new KernelDefintion(KernelTypes.Blur5x5, 1.0, 0.0, 5, 5,
            0, 0, 1, 0, 0,
            0, 1, 1, 1, 0,
            1, 1, 1, 1, 1,
            0, 1, 1, 1, 0,
            0, 0, 1, 0, 0) },
        {new KernelDefintion(KernelTypes.Gaussian3x3, 1.0, 0.0, 3, 3,
            1, 2, 1,
            2, 4, 2,
            1, 2, 1) },
        {new KernelDefintion(KernelTypes.Gaussian5x5, 1.0, 0.0, 5, 5,
            2, 4, 5, 4, 2,
            4, 9, 12, 9, 4,
            5, 12, 15, 12, 5,
            4, 9, 12, 9, 4,
            2, 4, 5, 4, 2) },
        {new KernelDefintion(KernelTypes.MotionBlur, 1.0, 1.0 / 9.0, 9, 9,
            1, 0, 0, 0, 0, 0, 0, 0, 1,
            0, 1, 0, 0, 0, 0, 0, 1, 0,
            0, 0, 1, 0, 0, 0, 1, 0, 0,
            0, 0, 0, 1, 0, 1, 0, 0, 0,
            0, 0, 0, 0, 1, 0, 0, 0, 0,
            0, 0, 0, 1, 0, 1, 0, 0, 0,
            0, 0, 1, 0, 0, 0, 1, 0, 0,
            0, 1, 0, 0, 0, 0, 0, 1, 0,
            1, 0, 0, 0, 0, 0, 0, 0, 1) },
        {new KernelDefintion(KernelTypes.MotionBlurLR, 1.0, 0.0, 9, 9,
            1, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 1, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 1, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 1, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 1, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 1, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 1, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 1) },
        {new KernelDefintion(KernelTypes.MotionBlurRL, 1.0, 0.0, 9, 9,
             0, 0, 0, 0, 0, 0, 0, 0, 1,
             0, 0, 0, 0, 0, 0, 0, 1, 0,
             0, 0, 0, 0, 0, 0, 1, 0, 0,
             0, 0, 0, 0, 0, 1, 0, 0, 0,
             0, 0, 0, 0, 1, 0, 0, 0, 0,
             0, 0, 0, 1, 0, 0, 0, 0, 0,
             0, 0, 1, 0, 0, 0, 0, 0, 0,
             0, 1, 0, 0, 0, 0, 0, 0, 0,
             1, 0, 0, 0, 0, 0, 0, 0, 0) },
        {new KernelDefintion(KernelTypes.Emboss, 1.0, 128.0, 3, 3,
            2.0, 0.0, 0.0,
            0.0, -1.0, 0.0,
            0.0, 0.0, -1.0 ) },
        {new KernelDefintion(KernelTypes.Emboss45, 1.0, 128.0, 3, 3,
            -1.0, -1.0, 0.0,
            -1.0, 0.0, 1.0,
            0.0, 1.0, 1.0 ) },
        {new KernelDefintion(KernelTypes.EmbossTLtoBR, 1.0, 128.0, 3, 3,
            -1.0, 0.0, 0.0,
             0.0, 0.0, 1.0,
             0.0, 1.0, 1.0) },
        {new KernelDefintion(KernelTypes.IntenseEmboss, 1.0, 128.0, 5, 5,
            -1, -1, -1, -1, 0,
            -1, -1, -1, 0, 1,
            -1, -1, 0, 1, 1,
            -1, 0, 1, 1, 1,
             0, 1, 1, 1,  1 ) },
        {new KernelDefintion(KernelTypes.EdgeDetection, 1.0, 0.0, 3, 3,
             -1, -1, -1,
             -1, 8, -1,
             -1, -1, -1) },
        {new KernelDefintion(KernelTypes.EdgeDetection45, 1.0, 0.0, 5, 5,
             -1,  0,  0,  0,  0,
             0, -2,  0,  0,  0,
             0,  0,  6,  0,  0,
             0,  0,  0, -2,  0,
             0,  0,  0,  0, -1) },
        {new KernelDefintion(KernelTypes.HorizontalEdgeDetection, 1.0, 0.0, 5, 5,
             0,  0,  0,  0,  0,
             0,  0,  0,  0,  0,
             -1, -1,  2,  0,  0,
             0,  0,  0,  0,  0,
             0,  0,  0,  0,  0) },
        {new KernelDefintion(KernelTypes.VerticalEdgeDetection, 1.0, 0.0, 5, 5,
             0,  0, -1,  0,  0,
             0,  0, -1,  0,  0,
             0,  0,  4,  0,  0,
             0,  0, -1,  0,  0,
             0,  0, -1,  0,  0) },
        {new KernelDefintion(KernelTypes.EdgeDetectionULtoBR, 1.0, 0.0, 3, 3,
            -5, 0, 0,
             0, 0, 0,
             0, 0, 5) },
        {new KernelDefintion(KernelTypes.HighPassFilter, 1.0, 0.0, 3, 3,
            -1, -2, -1,
            -2, 12, -2,
            -1, -2, -1) },
        {new KernelDefintion(KernelTypes.LowPass3x3, 1.0, 0.0, 3, 3,
            1, 2, 1,
            2, 4, 2,
            1, 2, 1) },
        {new KernelDefintion(KernelTypes.LowPass5x5, 1.0, 0.0, 5, 5,
             1,  1, 1,  1,  1,
             1,  4, 4,  4,  1,
             1,  4, 12,  4,  1,
             1,  4, 4,  4,  1,
             0,  0, -1,  0,  0) },
        {new KernelDefintion(KernelTypes.Sharpen, 1.0, 1.0 / 8.0, 3, 3,
            -1, -1, -1,
            -1, 9, -1,
            -1, -1, -1) },
        {new KernelDefintion(KernelTypes.Sharpen3x3Factor, 1.0, 0.0, 3, 3,
            0, -2,  0,
            -2, 11, -2,
            0, -2,  0) },
        {new KernelDefintion(KernelTypes.Sharpen5x5, 1.0, 0.0, 5, 5,
           -1, -1, -1, -1, -1,
           -1,  2,  2,  2, -1,
           -1,  2,  8,  2,  1,
           -1,  2,  2,  2, -1,
           -1, -1, -1, -1, -1) },
        {new KernelDefintion(KernelTypes.Identity, 1.0, 1.0 / 9.0, 3, 3,
            1, 1, 1,
            1, 1, 1,
            1, 1, 1) },
        {new KernelDefintion(KernelTypes.Laplace, 1.0, 0.0, 3, 3,
            0, -1, 0,
            -1, -4, -1,
            0, -1, 0) },
        {new KernelDefintion(KernelTypes.LaplaceDiagonals, 1.0, 0.0, 3, 3,
            0.5, 1, 0.5,
            1, -6, 1,
            0.5, 1, 0.5) },
        {new KernelDefintion(KernelTypes.Laplace9x9, 1.0, 0.0, 9, 9,
            -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1,  8,  8 , 8, -1, -1, -1,
            -1, -1, -1,  8,  8 , 8, -1, -1, -1,
            -1, -1, -1,  8,  8 , 8, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1 ) },
        {new KernelDefintion(KernelTypes.SobelHorizontal, 1.0, 0.0, 3, 3,
            -1, -2, -1,
            0, 0, 0,
            1, 2, 1) },
        {new KernelDefintion(KernelTypes.SobelVertical, 1.0, 0.0, 3, 3,
            -1, 0, 1,
            -2, 0, 2,
            -1, 2, 1) },
        {new KernelDefintion(KernelTypes.Mean3x3, 1.0, 1 / 9, 3, 3,
            1, 1, 1,
            1, 1, 1,
            1, 1, 1) },
        {new KernelDefintion(KernelTypes.Mean5x5, 1.0, 1 / 25, 5, 5,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1) },
       {new KernelDefintion(KernelTypes.Prewitt1, 1.0, 0.0, 3, 3,
            -1, -1, 1,
            0, 0, 0,
            1, 1, 1) },
       {new KernelDefintion(KernelTypes.Prewitt2, 1.0, 0.0, 3, 3,
            -1, 0, 1,
            -1, 0, 1,
            -1, 0, 1) },
       {new KernelDefintion(KernelTypes.BayerDecodeCubicA,(1.0 / 255.0),0.0,7,7,
                0, 0, 0, 1, 0, 0, 0,
                0, 0, -9, 0, -9, 0, 0,
                0, -9, 0, 81, 0, -9, 0,
                1, 0, 81, 256, 81, 0, 1,
                0, -9, 0, 81, 0, -9, 0,
                0, 0, -9, 0, -9, 0, 0,
                0, 0, 0, 1, 0, 0, 0) },
      {new KernelDefintion(KernelTypes.BayerDecodeCubicB,(1.0 / 255.0),0.0,7,7,
                1, 0, -9, -16, -9, 0, 1,
                0, 0, 0, 0, 0, 0, 0,
                -9, 0, 81, 144, 81, 0, -9,
                -16, 0, 144, 256, 144, 0, -16,
               -9, 0, 81, 144, 81, 0, -9,
                0, 0, 0, 0, 0, 0, 0,
                1, 0, -9, -16, -9, 0, 1) },
    };

        private KernelDefintion GetKernel (KernelTypes TheKernel)
        {
            foreach (KernelDefintion KD in Kernels)
                if (KD.KernelType == TheKernel)
                    return KD;
            return null;
        }
    }

    /// <summary>
    /// Encapsulates a kernel definition used for convolution.
    /// </summary>
    public class KernelDefintion
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public KernelDefintion ()
        {
            Width = 0;
            Height = 0;
            KernelType = KernelTypes.NotSet;
            Matrix = new List<double>();
            Factor = 1.0;
            Bias = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="KernelType">Kernel type/usage.</param>
        /// <param name="Factor">Factor value.</param>
        /// <param name="Bias">Bias value.</param>
        /// <param name="Width">Width of the kernel. Must be odd.</param>
        /// <param name="Height">Height of the kernel. Must be odd.</param>
        /// <param name="KernelValues">Kernel matrix values.</param>
        public KernelDefintion (ColorBlend.KernelTypes KernelType, double Factor, double Bias, int Width, int Height, params double[] KernelValues)
        {
            this.KernelType = KernelType;
            this.Width = Width;
            this.Height = Height;
            this.Factor = Factor;
            this.Bias = Bias;
            if (KernelValues == null)
                throw new ArgumentNullException("KernelValues");
            if (KernelValues.Length < 1)
                throw new InvalidOperationException("Not enough values.");
            if (KernelValues.Length != Width * Height)
                throw new InvalidOperationException("Conflicting matrix sizes.");
            Matrix = new List<double>();
            for (int i = 0; i < KernelValues.Length; i++)
            {
                Matrix.Add(KernelValues[i]);
            }
        }

        private int _Width = 0;
        /// <summary>
        /// Get or set the width of the kernel. Must be an odd number.
        /// </summary>
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if ((value & 0x1) == 0)
                    throw new InvalidOperationException("Width must be odd.");
                _Width = value;
            }
        }

        private int _Height = 0;
        /// <summary>
        /// Get or set the height of the number. Must be an odd number.
        /// </summary>
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                if ((value & 0x1) == 0)
                    throw new InvalidOperationException("Height must be odd.");
                _Height = value;
            }
        }

        /// <summary>
        /// Get or set the type/usage of the kernel.
        /// </summary>
        public ColorBlend.KernelTypes KernelType { get; set; }

        /// <summary>
        /// Get the kernel's matrix.
        /// </summary>
        public List<double> Matrix { get; internal set; }

        /// <summary>
        /// Get or set the bias.
        /// </summary>
        public double Bias { get; set; }

        /// <summary>
        /// Get or set the factor.
        /// </summary>
        public double Factor { get; set; }
    }

    /// <summary>
    /// Defines various kernel types.
    /// </summary>
    public enum KernelTypes
    {
        NotSet,
        Blur3x3,
        Blur5x5,
        Gaussian3x3,
        Gaussian5x5,
        MotionBlur,
        MotionBlurLR,
        MotionBlurRL,
        Emboss,
        Emboss45,
        EmbossTLtoBR,
        IntenseEmboss,
        EdgeDetection,
        EdgeDetection45,
        HorizontalEdgeDetection,
        VerticalEdgeDetection,
        EdgeDetectionULtoBR,
        HighPassFilter,
        Sharpen,
        Sharpen3x3Factor,
        Sharpen5x5,
        Identity,
        Laplace,
        LaplaceDiagonals,
        Laplace9x9,
        LowPass3x3,
        LowPass5x5,
        SobelHorizontal,
        SobelVertical,
        Mean3x3,
        Mean5x5,
        Prewitt1,
        Prewitt2,
        BayerDecodeCubicA,
        BayerDecodeCubicB,
    }
}
