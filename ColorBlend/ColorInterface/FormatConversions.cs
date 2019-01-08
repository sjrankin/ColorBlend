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
        [DllImport("ColorBlender.dll", EntryPoint = "_ConvertGray8ToBGRA32@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ConvertGray8ToBGRA32 (void* Source, Int32 BufferWidth, Int32 BufferHeight, Int32 BufferStride,
            void* Destination);

        public bool ConvertGray8toARGB32 (WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ConvertGray8ToBGRA32(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer());
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
