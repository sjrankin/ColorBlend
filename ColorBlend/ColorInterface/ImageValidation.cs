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
        /// <summary>
        /// See if <paramref name="Image1"/> and <paramref name="Image2"/> have the same dimensions.
        /// </summary>
        /// <param name="Image1">First image.</param>
        /// <param name="Image2">Second image.</param>
        /// <returns>True if the images have the same dimensions, false if not.</returns>
        public bool ValidateImages (WriteableBitmap Image1, WriteableBitmap Image2)
        {
            if (Image1 == null || Image2 == null)
                return false;
            if (Image1.BackBufferStride != Image2.BackBufferStride)
                return false;
            if (Image1.DpiX != Image2.DpiX)
                return false;
            if (Image1.DpiY != Image2.DpiY)
                return false;
            if (Image1.PixelWidth != Image2.PixelWidth)
                return false;
            if (Image1.PixelHeight != Image2.PixelHeight)
                return false;
            return true;
        }
    }
}
