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
        [DllImport("ColorBlender.dll", EntryPoint = "_PixelChannelRollingLogicalOperation@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int PixelChannelRollingLogicalOperation (void* Source, int Width, int Height, int Stride, void* Destination,
  int LogicalOperator, bool RightToLeft, byte Mask, bool IncludeAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_AccumulateDoubleBlock@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AccumulateDoubleBlock (void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
    void* Accumulator);

        [DllImport("ColorBlender.dll", EntryPoint = "_DoubleBlockOperation@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AccumulateDoubleBlock (void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
                    Int32 Operator, double Operand, bool IncludeAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_ByteBlocksOperation@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ByteBlocksOperation (void* Destination, Int32 DestinationWidth, Int32 DestinationHeight, Int32 DestinationStride,
            void* BufferA, void* BufferB, Int32 Operator, bool IncludeAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_ByteBlockOperationByChannel@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ByteBlockOperationByChannel (void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
         Int32 Operator, Int32 Operand, bool DoAlpha, bool DoRed, bool DoGreen, bool DoBlue);

        public bool ImageByteOperationByChannel (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
      int Operator, int Operand, bool DoAlpha, bool DoRed, bool DoGreen, bool DoBlue)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ByteBlockOperationByChannel(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), Operator, Operand, DoAlpha, DoRed, DoGreen, DoBlue);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ByteBlockOperation@32", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ByteBlockOperation (void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride, void* Destination,
            Int32 Operator, Int32 Operand, bool IncludeAlpha);

        public bool ImageByteOperation (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
                 int Operator, int Operand)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ByteBlockOperation(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                    DestinationBuffer.BackBuffer.ToPointer(), Operator, Operand, false);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }
            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ApplyChannelMask@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ApplyChannelMasks (void* Source, int Width, int Height, int Stride, void* Destination,
   int LogicalOperator, byte AlphaMask, byte RedMask, byte GreenMask, byte BlueMask, bool IncludeAlpha);

        [DllImport("ColorBlender.dll", EntryPoint = "_ApplyChannelMask2@56", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ApplyChannelMasks2 (void* Source, int Width, int Height, int Stride, void* Destination,
          int LogicalOperator, byte AlphaMask, bool UseAlpha, byte RedMask, bool UseRed, byte GreenMask,
          bool UseGreen, byte BlueMask, bool UseBlue);

        public bool ImagePixelChannelMask (WriteableBitmap SourceBuffer, WriteableBitmap DestinationBuffer, int Width, int Height, int Stride,
    LogicalOperations LogOp, Nullable<byte> AlphaValue, Nullable<byte> RedValue, Nullable<byte> GreenValue, Nullable<byte> BlueValue)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();
                OpReturn = (ReturnCode)ApplyChannelMasks2(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth, SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride,
                        DestinationBuffer.BackBuffer.ToPointer(),
                        (int)LogOp,
                        AlphaValue.HasValue ? (byte)AlphaValue.Value : (byte)0x0, AlphaValue.HasValue,
                        RedValue.HasValue ? (byte)RedValue.Value : (byte)0x0, RedValue.HasValue,
                        GreenValue.HasValue ? (byte)GreenValue.Value : (byte)0x0, GreenValue.HasValue,
                        BlueValue.HasValue ? (byte)BlueValue.Value : (byte)0x0, BlueValue.HasValue);
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
