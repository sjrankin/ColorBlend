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
        /// Native routine to get the pixel in an image.
        /// </summary>
        /// <param name="Source">Image buffer.</param>
        /// <param name="SourceWidth">Width of the buffer in pixels.</param>
        /// <param name="SourceHeight">Height of the buffer in scanlines.</param>
        /// <param name="X">Horizontal position.</param>
        /// <param name="Y">Vertical position.</param>
        /// <returns>The color at the specified position.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_GetPixelAtLocation@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe UInt32 GetPixelAtLocation(void* Source, Int32 SourceWidth, Int32 SourceHeight,
            Int32 X, Int32 Y);

        /// <summary>
        /// Native routine to get the pixel in an image.
        /// </summary>
        /// <param name="Source">Image buffer.</param>
        /// <param name="SourceWidth">Width of the buffer in pixels.</param>
        /// <param name="SourceHeight">Height of the buffer in scanlines.</param>
        /// <param name="SourceStride">Stride of the buffer in bytes.</param>
        /// <param name="X">Horizontal position.</param>
        /// <param name="Y">Vertical position.</param>
        /// <returns>The color at the specified position.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_GetPixelAtLocation2@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe UInt32 GetPixelAtLocation2(void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            Int32 X, Int32 Y);

        /// <summary>
        /// Return the pixel in <paramref name="Source"/> at <paramref name="X"/>,<paramref name="Y"/>.
        /// </summary>
        /// <param name="Source">Image where the pixel to return resides.</param>
        /// <param name="SourceWidth">Width of the image.</param>
        /// <param name="SourceHeight">Height of the image.</param>
        /// <param name="X">Horizontal location of the pixel.</param>
        /// <param name="Y">Vertical location of the pixel.</param>
        /// <param name="Result">The color at the specified location.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool GetColorAt(WriteableBitmap Source, int SourceWidth, int SourceHeight,
            int X, int Y, out Color Result)
        {
            Result = Colors.Transparent;
            if (Source == null)
                return false;
            UInt32 ColorValue = 0;
            unsafe
            {
                Source.Lock();
#if true
                ColorValue = GetPixelAtLocation2(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight, 
                    Source.BackBufferStride, X, Y);
#else
                ColorValue = GetPixelAtLocation(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight, X, Y);
#endif
                Source.Unlock();
            }
            Result = ColorValue.FromARGB();
            return true;
        }
    }
}
