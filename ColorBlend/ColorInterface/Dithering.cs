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
        public enum DitheringTypes : int
        {
            UnknownType = -1,
            FloydSteinberg = 0,
            FalseFloydSteinberg = 1,
            Atkinson = 2,
            JarvisJudiceNinke = 3,
            Stucki = 4,
            Burkes = 5,
            Sierra1 = 6,
            Sierra2 = 7,
            Sierra3 = 8
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_DoDither@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int DoDither (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
           int DitherType, bool AsGrayscale);

        public bool Dithering (WriteableBitmap SourceBuffer, int Width, int Height, WriteableBitmap DestinationBuffer,
            DitheringTypes DitherType = DitheringTypes.FloydSteinberg, bool AsGrayscale = true)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)DoDither(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), (int)DitherType, AsGrayscale);
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
