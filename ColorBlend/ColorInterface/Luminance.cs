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
        [DllImport("ColorBlender.dll", EntryPoint = "_InvertLuminance@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int InvertLuminance(void* Source, Int32 Width, Int32 Height, Int32 Stride,
              void* Destination, bool UseThreshold, byte LuminanceThreshold);

        public bool LuminanceInversion(WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer,
            bool EnableThreshold, byte ThresholdValue)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;

            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)InvertLuminance(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(),
                    EnableThreshold, ThresholdValue);
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
