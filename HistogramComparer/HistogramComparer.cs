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
    public class HistogramComparer
    {
        public HistogramComparer ()
        {
        }

        double Compare(WriteableBitmap Image1, WriteableBitmap Image2)
        {
            if (Image1 == null)
                throw new ArgumentNullException("Image1");
            if (Image2 == null)
                throw new ArgumentNullException("Image2");
            return double.NaN;
        }

        double Compare(HistogramData Image1, HistogramData Image2)
        {
            if (Image1 == null)
                throw new ArgumentNullException("Image1");
            if (Image2 == null)
                throw new ArgumentNullException("Image2");
            return double.NaN;
        }
    }
}
