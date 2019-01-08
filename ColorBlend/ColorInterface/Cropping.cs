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
        [DllImport("ColorBlender.dll", EntryPoint = "_ImageCrop@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageCrop (void* Source, Int32 Width, Int32 Height, Int32 Stride,
              Int32 Left, Int32 Top, Int32 Right, Int32 Bottom,  void* Destination);

        public bool CropImage (WriteableBitmap SourceBuffer, 
            WriteableBitmap DestinationBuffer, int Left, int Top, int Right, int Bottom)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)ImageCrop(SourceBuffer.BackBuffer.ToPointer(),
                    SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    Left, Top, Right, Bottom,
                    DestinationBuffer.BackBuffer.ToPointer());

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
