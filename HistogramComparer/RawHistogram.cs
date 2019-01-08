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

namespace Histogram
{
    public class RawHistogram
    {
        public RawHistogram ()
        {
        }

        public RawHistogram(string FileName, WriteableBitmap Img)
        {
            if (string.IsNullOrEmpty(FileName))
                throw new ArgumentNullException("FileName");
            if (Img == null)
                throw new ArgumentNullException("Img");
            this.FileName = FileName;
            this.Img = Img;
        }

        public string FileName = "";
        public WriteableBitmap Img = null;
        public double[] RedPercent = null;
        public double[] GreenPercent = null;
        public double[] BluePercent = null;
        public UInt32[] RawRed = null;
        public UInt32[] RawGreen = null;
        public UInt32[] RawBlue = null;
        public UInt32 RedSum = 0;
        public UInt32 GreenSum = 0;
        public UInt32 BlueSum = 0;
    }
}
