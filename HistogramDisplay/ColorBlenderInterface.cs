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

namespace HistogramDisplay
{
    public class ColorBlenderInterface
    {
        [DllImport("ColorBlender.dll", EntryPoint = "_CreateHistogramRegion@72", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CreateHistogramRegion (void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
          Int32 BinCount,
          void* RawRed, void* PercentRed, ref UInt32 RedCount,
          void* RawGreen, void* PercentGreen, ref UInt32 GreenCount,
          void* RawBlue, void* PercentBlue, ref UInt32 BlueCount,
          Int32 Left, Int32 Top, Int32 Right, Int32 Bottom);

        [DllImport("ColorBlender.dll", EntryPoint = "_CreateHistogram@56", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CreateHistogram (void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            Int32 BinCount,
            void* RawRed, void* PercentRed, ref UInt32 TotalRed,
            void* RawGreen, void* PercentGreen, ref UInt32 TotalGreen,
            void* RawBlue, void* PercentBlue, ref UInt32 TotalBlue);

        /// <summary>
        /// Create a histogram based on <paramref name="Source"/>.
        /// </summary>
        /// <param name="Source">Pointer to a buffer from which the histogram will be created.</param>
        /// <param name="SourceWidth">Width of the buffer in pixels.</param>
        /// <param name="SourceHeight">Height of the buffer in scan lines.</param>
        /// <param name="SourceStride">Stride of the buffer.</param>
        /// <param name="BinCount">Number of bins for the histogram.</param>
        /// <param name="RawRed">On success the raw number of red pixels per bin.</param>
        /// <param name="RedPercent">On success the percent red pixels per bin.</param>
        /// <param name="TotalRed">On success the total number of non-0 red pixels.</param>
        /// <param name="RawGreen">On success the raw number of green pixels per bin.</param>
        /// <param name="GreenPercent">On success the percent green pixels per bin.</param>
        /// <param name="TotalGreen">On success the total number of non-0 green pixels.</param>
        /// <param name="RawBlue">On success the raw number of blue pixels per bin.</param>
        /// <param name="BluePercent">On success the percent blue pixels per bin.</param>
        /// <param name="TotalBlue">On success the total number of non-blue pixels per bin.</param>
        /// <returns>True on success, false on error.</returns>
        unsafe public bool MakeHistogram (byte* Source, int SourceWidth, int SourceHeight, int SourceStride,
            int BinCount,
            ref UInt32[] RawRed, ref double[] RedPercent, out UInt32 TotalRed,
            ref UInt32[] RawGreen, ref double[] GreenPercent, out UInt32 TotalGreen,
            ref UInt32[] RawBlue, ref double[] BluePercent, out UInt32 TotalBlue)
        {
            ReturnCode HistogramReturn = ReturnCode.NotSet;

            TotalRed = 0;
            TotalGreen = 0;
            TotalBlue = 0;

            unsafe
            {
                fixed (void* RedData = RawRed)
                {
                    fixed (void* PercentRed = RedPercent)
                    {
                        fixed (void* GreenData = RawGreen)
                        {
                            fixed (void* PercentGreen = GreenPercent)
                            {
                                fixed (void* BlueData = RawBlue)
                                {
                                    fixed (void* PercentBlue = BluePercent)
                                    {
                                        HistogramReturn = (ReturnCode)CreateHistogram(Source, SourceWidth, SourceHeight, SourceStride,
                                            BinCount,
                                            RedData, PercentRed, ref TotalRed,
                                            GreenData, PercentGreen, ref TotalGreen,
                                            BlueData, PercentBlue, ref TotalBlue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return HistogramReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Create a histogram based on <paramref name="Buffer"/>.
        /// </summary>
        /// <param name="Source">Array of bytes from which the histogram will be created.</param>
        /// <param name="SourceWidth">Width of the buffer in pixels.</param>
        /// <param name="SourceHeight">Height of the buffer in scan lines.</param>
        /// <param name="SourceStride">Stride of the buffer.</param>
        /// <param name="BinCount">Number of bins for the histogram.</param>
        /// <param name="RawRed">On success the raw number of red pixels per bin.</param>
        /// <param name="RedPercent">On success the percent red pixels per bin.</param>
        /// <param name="TotalRed">On success the total number of non-0 red pixels.</param>
        /// <param name="RawGreen">On success the raw number of green pixels per bin.</param>
        /// <param name="GreenPercent">On success the percent green pixels per bin.</param>
        /// <param name="TotalGreen">On success the total number of non-0 green pixels.</param>
        /// <param name="RawBlue">On success the raw number of blue pixels per bin.</param>
        /// <param name="BluePercent">On success the percent blue pixels per bin.</param>
        /// <param name="TotalBlue">On success the total number of non-blue pixels per bin.</param>
        /// <returns>True on success, false on error.</returns>
        unsafe public bool MakeHistogram (ref byte[] Buffer, int SourceWidth, int SourceHeight, int SourceStride,
            int BinCount,
            ref UInt32[] RawRed, ref double[] RedPercent, out UInt32 TotalRed,
            ref UInt32[] RawGreen, ref double[] GreenPercent, out UInt32 TotalGreen,
            ref UInt32[] RawBlue, ref double[] BluePercent, out UInt32 TotalBlue)
        {
            ReturnCode HistogramReturn = ReturnCode.NotSet;

            TotalRed = 0;
            TotalGreen = 0;
            TotalBlue = 0;

            unsafe
            {
                fixed (void* Source = Buffer)
                {
                    fixed (void* RedData = RawRed)
                    {
                        fixed (void* PercentRed = RedPercent)
                        {
                            fixed (void* GreenData = RawGreen)
                            {
                                fixed (void* PercentGreen = GreenPercent)
                                {
                                    fixed (void* BlueData = RawBlue)
                                    {
                                        fixed (void* PercentBlue = BluePercent)
                                        {
                                            HistogramReturn = (ReturnCode)CreateHistogram(Source, SourceWidth, SourceHeight, SourceStride,
                                                BinCount,
                                                RedData, PercentRed, ref TotalRed,
                                                GreenData, PercentGreen, ref TotalGreen,
                                                BlueData, PercentBlue, ref TotalBlue);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return HistogramReturn == ReturnCode.Success;
        }

        public enum ReturnCode : int
        {
            NotSet = -1,
            Success = 0,
            Error = 1,
            BadIndex = 2,
            NullPointer = 3,
            NegativeIndex = 4,
            BadSecondaryIndex = 5,
            IndexOutOfRange = 6,
            DisplayListOperationFailed = 7,
            EmptyDisplayList = 8,
            VirtualEmptyDisplayList = 9,
            UnknownDisplayListOperand = 10,
            InvalidOperation = 11,
            NoActionTaken = 12,
            NormalNonNormal = 13,
            NotImplemented = 14,
            ImageMismatch = 15,
            ImagesMatch = 16,
        }
    }
}
