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
        [DllImport("ColorBlender.dll", EntryPoint = "_ScaleImage@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ScaleImage (void* Source, Int32 Width, Int32 Height, Int32 Stride,
            void* Destination, Int32 DestWidth, Int32 DestHeight, int ScalingMethod);

        [DllImport("ColorBlender.dll", EntryPoint = "_NearestNeighborScaling@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int NearestNeighborScaling (void* Source, Int32 Width, Int32 Height, Int32 Stride,
            void* Destination, Int32 DestWidth, Int32 DestHeight);

        [DllImport("ColorBlender.dll", EntryPoint = "_BilinearScaling@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BilinearScaling (void* Source, Int32 Width, Int32 Height, Int32 Stride,
            void* Destination, Int32 DestWidth, Int32 DestHeight);

        public bool ImageScale (WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer,
            int DestWidth, int DestHeight, int ScalingType)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)ScaleImage(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                    SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(), DestinationBuffer.PixelWidth,
                    DestinationBuffer.PixelHeight, ScalingType);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool ImageScale (WriteableBitmap Buffer, int Width, int Height, int DestWidth, int DestHeight, int ScalingType)
        {
            WriteableBitmap Scratch = new WriteableBitmap(DestWidth, DestHeight, Buffer.DpiX, Buffer.DpiY, PixelFormats.Bgra32, null);
            bool OK = ImageScale(Buffer, Width, Height, Scratch, DestWidth, DestHeight, ScalingType);
            if (OK)
                Buffer = new WriteableBitmap(Scratch as BitmapSource);
            return OK;
        }
    }
}
