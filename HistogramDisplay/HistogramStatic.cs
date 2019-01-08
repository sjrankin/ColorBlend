using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace HistogramDisplay
{
    public partial class HistogramViewer
    {
        public static void GenerateHistogramData (ref HistogramData Data)
        {
            if (Data == null)
                throw new ArgumentNullException("Data");
            if (Data.Img == null)
                throw new ArgumentNullException("Data.Img");
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            int ImageWidth = Data.Img.PixelWidth;
            int ImageHeight = Data.Img.PixelHeight;
            int ImageStride = Data.Img.BackBufferStride;
            Data.RedPercent = new double[Data.BinSize];
            Data.RawRed = new UInt32[Data.BinSize];
            Data.RedSum = 0;
            Data.GreenPercent = new double[Data.BinSize];
            Data.RawGreen = new UInt32[Data.BinSize];
            Data.GreenSum = 0;
            Data.BluePercent = new double[Data.BinSize];
            Data.RawBlue = new UInt32[Data.BinSize];
            Data.BlueSum = 0;
            unsafe
            {
                CBI.MakeHistogram((byte*)Data.Img.BackBuffer, ImageWidth, ImageHeight, ImageStride,
                  Data.BinSize,
                  ref Data.RawRed, ref Data.RedPercent, out Data.RedSum,
                  ref Data.RawGreen, ref Data.GreenPercent, out Data.GreenSum,
                  ref Data.RawBlue, ref Data.BluePercent, out Data.BlueSum);
            }

            Data.MaxRedPercent = 0.0;
            Data.MaxGreenPercent = 0.0;
            Data.MaxBluePercent = 0.0;

            for (int i = 0; i < Data.BinSize; i++)
            {
                if (Data.RedPercent[i] > Data.MaxRedPercent)
                    Data.MaxRedPercent = Data.RedPercent[i];
                if (Data.GreenPercent[i] > Data.MaxGreenPercent)
                    Data.MaxGreenPercent = Data.GreenPercent[i];
                if (Data.BluePercent[i] > Data.MaxBluePercent)
                    Data.MaxBluePercent = Data.BluePercent[i];
            }

            Data.Triplets = new List<HistogramTriplet>();
            for (int i = 0; i < Data.BinSize; i++)
            {
                HistogramTriplet Triplet = new HistogramTriplet(Data.RedPercent[i], Data.GreenPercent[i], Data.BluePercent[i]);
                Triplet.RawRed = Data.RawRed[i];
                Triplet.RawGreen = Data.RawGreen[i];
                Triplet.RawBlue = Data.RawBlue[i];
                Data.Triplets.Add(Triplet);
            }
        }
    }
}
