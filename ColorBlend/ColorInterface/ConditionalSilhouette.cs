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
        [DllImport("ColorBlender.dll", EntryPoint = "_SilhouetteIf@80", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SilhouetteIf(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination, bool UseHue,
             double HueThreshold, double HueRange, bool UseSaturation, double SaturationThreshold, double SaturationRange,
             bool UseLuminance, double LuminanceThreshold, bool LuminanceGreaterThan, UInt32 SilhouetteColor);

        public bool DoSilhouetteIf(WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer,
            bool UseHue, double HueThreshold, double HueRange,
            bool UseSaturation, double SaturationThreshold, double SaturationRange,
            bool UseLuminance, double LuminanceThreshold, bool LuminanceGreaterThan,
            Color SilhouetteColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)SilhouetteIf(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(),
                    UseHue, HueThreshold, HueRange, UseSaturation, SaturationThreshold, SaturationRange,
                    UseLuminance, LuminanceThreshold, LuminanceGreaterThan,
                    SilhouetteColor.ToARGB());
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
