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
    public class HistogramData
    {
        public HistogramData ()
        {
        }

        public HistogramData(string FileName)
        {
            this.FileName = FileName;
        }

        public string FileName = "";

        public WriteableBitmap Img = null;

        public double[] Combined = null;

        public double[] RedPercent = null;

        public double[] GreenPercent = null;

        public double[] BluePercent = null;

        public UInt32[] RawRed = null;

        public UInt32[] RawGreen = null;

        public UInt32[] RawBlue = null;

        public UInt32 RedSum = 0;

        public UInt32 GreenSum = 0;

        public UInt32 BlueSum = 0;

        public UInt32 GraySum = 0;

        public int BinSize = 256;

        public List<HistogramTriplet> Triplets = null;

        public double MaxRedPercent = 0.0;

        public double MaxGreenPercent = 0.0;

        public double MaxBluePercent = 0.0;
    }
}
