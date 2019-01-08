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
#if false
        [DllImport("ColorBlender.dll", EntryPoint = "_BayerDecode4x4@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BayerDecode4x4 (void* Source, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
              void* Destination, int P00, int P10, int P01, int P11);

        public bool BayerDecode (WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)BayerDecode4x4(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight,
                    SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(), 1, 1, 1, 1);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
            return false;
        }
#endif

        [DllImport("ColorBlender.dll", EntryPoint = "_BayerDemosaic@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int BayerDemosaic (void* Source, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
               void* Destination, int Pattern, int Method);

        public enum BayerDemosaicMethods
        {
            NearestNeighbor = 0,
            LinearInterpolation = 1,
            CubicInterplation = 2,
        }

        public enum BayerPatterns
        {
            RGGB = 0,
            BGGR = 1,
        }

        public bool BayerDemosaic (WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer,
            BayerPatterns Pattern, BayerDemosaicMethods Method)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)BayerDemosaic(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(),
                    (int)Pattern, (int)Method);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
