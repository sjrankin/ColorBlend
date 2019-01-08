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
        [DllImport("ColorBlender.dll", EntryPoint = "_LinearizeChannelsIntoImage@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int LinearizeChannelsIntoImage(void* RedChannel, void* GreenChannel, void* BlueChannel, Int32 ChannelSize,
           void* Destination, Int32 Width, Int32 Height, Int32 Stride, Int32 ChannelOrder);

        public bool LinearizeChannels(WriteableBitmap Source, int SourceWidth, int SourceHeight,
            WriteableBitmap Destination, RGBChannelOrders ChannelOrder, out ColorBlenderInterface.ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = OpReturn;
            int ImageSize = SourceWidth * SourceHeight;

            unsafe
            {
                Source.Lock();
                Destination.Lock();

                AlignColorChannels(Source, SourceWidth, SourceHeight,
                    out byte[] RedChannel, out byte[] GreenChannel, out byte[] BlueChannel,
                    out ReturnCode AlignResult);
                if (AlignResult != ReturnCode.Success)
                {
                    Destination.Unlock();
                    Source.Unlock();
                    LastReturnCode = OpReturn;
                    Result = OpReturn;
                    return OpReturn == ReturnCode.Success;
                }

                fixed (void* RedPtr = RedChannel)
                {
                    fixed (void* GreenPtr = GreenChannel)
                    {
                        fixed (void* BluePtr = BlueChannel)
                        {
                            OpReturn = (ReturnCode)LinearizeChannelsIntoImage(RedPtr, GreenPtr, BluePtr, ImageSize,
                                  Destination.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                                  Source.BackBufferStride, (int)ChannelOrder);
                        }
                    }
                }

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }

            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Color blender function to rotate images by 90° clockwise.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="SourceStride">Stride of the source image.</param>
        /// <param name="RedChannel">Will contain red channel information.</param>
        /// <param name="GreenChannel">Will contain green channel information.</param>
        /// <param name="BlueChannel">Will contain blue channel information.</param>
        /// <returns>Value indicating operational results.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_AlignChannels@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AlignChannels(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
             void* RedChannel, void* GreenChannel, void* BlueChannel);

        /// <summary>
        /// Create aligned color channel information.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="RedChannel">Red channel contents.</param>
        /// <param name="GreenChannel">Green channel contents.</param>
        /// <param name="BlueChannel">Blue channel contents.</param>
        /// <param name="Result">Operation result value.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool AlignColorChannels(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           out byte[] RedChannel, out byte[] GreenChannel, out byte[] BlueChannel,
           out ColorBlenderInterface.ReturnCode Result)
        {
            int ImageSize = SourceWidth * SourceHeight;
            RedChannel = new byte[ImageSize];
            GreenChannel = new byte[ImageSize];
            BlueChannel = new byte[ImageSize];
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                fixed (void* RedPtr = RedChannel)
                {
                    fixed (void* GreenPtr = GreenChannel)
                    {
                        fixed (void* BluePtr = BlueChannel)
                        {
                            OpReturn = (ReturnCode)AlignChannels(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                                Source.BackBufferStride, RedPtr, GreenPtr, BluePtr);
                        }
                    }
                }
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
