using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using HistogramDisplay;

namespace Histogram
{
    public class HistogramGenerator
    {
        public HistogramGenerator ()
        {
        }

        double[] RedPercent = null;
        double[] GreenPercent = null;
        double[] BluePercent = null;
        UInt32[] RawRed = null;
        UInt32[] RawGreen = null;
        UInt32[] RawBlue = null;
        UInt32 RedSum = 0;
        UInt32 GreenSum = 0;
        UInt32 BlueSum = 0;

        public void Generate(WriteableBitmap Img, int BinSize)
        {
            int ImageWidth = Img.PixelWidth;
            int ImageHeight = Img.PixelHeight;
            int ImageStride = Img.BackBufferStride;
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            RedPercent = new double[BinSize];
            RawRed = new UInt32[BinSize];
            RedSum = 0;
            GreenPercent = new double[BinSize];
            RawGreen = new UInt32[BinSize];
            GreenSum = 0;
            BluePercent = new double[BinSize];
            RawBlue = new UInt32[BinSize];
            BlueSum = 0;

            unsafe
            {
                unsafe
                {
                    CBI.MakeHistogram((byte*)Img.BackBuffer, ImageWidth, ImageHeight, ImageStride,
                      BinSize,
                      ref RawRed, ref RedPercent, out RedSum,
                      ref RawGreen, ref GreenPercent, out GreenSum,
                      ref RawBlue, ref BluePercent, out BlueSum);
                }
            }
        }
    }
}
