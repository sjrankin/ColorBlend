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
        [DllImport("ColorBlender.dll", EntryPoint = "_RegionalOperation@48", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RegionalOperation (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
             UInt32 RegionWidth, UInt32 RegionHeight, bool DoAlpha, bool DoRed, bool DoGreen, bool DoBlue,
             UInt32 Operator);

        public bool ImageRegionalOperation (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
         int Operator, UInt32 RegionWidth, UInt32 RegionHeight, bool DoAlpha, bool DoRed, bool DoGreen, bool DoBlue)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)RegionalOperation(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(),
                    RegionWidth, RegionHeight,
                    DoAlpha, DoRed, DoGreen, DoBlue, (UInt32)Operator);
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
