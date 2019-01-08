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
        [DllImport("ColorBlender.dll", EntryPoint = "_AddBorder@40", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AddBorder (void* Source, Int32 Width, Int32 Height, Int32 Stride,
              Int32 LeftBorder, Int32 TopBorder, Int32 RightBorder, Int32 BottomBorder,  void* Destination,
              UInt32 BGColor);

        public bool AddBorderToImage (WriteableBitmap SourceBuffer, 
            WriteableBitmap DestinationBuffer, int LeftBorder, int TopBorder, int RightBorder, int BottomBorder,
            Color? BGColor = null)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            if (BGColor == null)
                BGColor = Colors.White;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)AddBorder(SourceBuffer.BackBuffer.ToPointer(),
                    SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    LeftBorder, TopBorder, RightBorder, BottomBorder,
                    DestinationBuffer.BackBuffer.ToPointer(), BGColor.Value.ToARGB());

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
