using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace ColorBlend
{
    public partial class ColorBlenderInterface
    {
        /// <summary>
        /// Shift the hue of each pixel.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="SourceStride">Stride of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="ShiftValue">How much to adust the hue by.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_ImageHueShift@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageHueShift(void* Source, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
              void* Destination, Int32 ShiftValue);

        /// <summary>
        /// Shift the hue of each pixel.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="HueAdjustmentValue">Value to adjust the hue by.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool AdjustImageHue(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, int HueAdjustmentValue, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)ImageHueShift(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer(), HueAdjustmentValue);

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
        /// Swap the saturation and luminance channels for each pixel.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="SourceStride">Stride of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_AdjustImageHSLValues@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int AdjustImageHSLValues(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
           void* DestinationBuffer, double HMultipler, double SMultiplier, double LMultiplier);

        /// <summary>
        /// Shift the hue of each pixel.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="HueAdjustmentValue">Value to adjust the hue by.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool AdjustHSLValues(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, double HMultiplier, double SMultiplier, double LMultiplier, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)AdjustImageHSLValues(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer(), HMultiplier, SMultiplier, LMultiplier);

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
        /// Swap the saturation and luminance channels for each pixel.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="SourceStride">Stride of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_Silly_SwapSaturationLuminance@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int Silly_SwapSaturationLuminance(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
              void* DestinationBuffer);

        /// <summary>
        /// Shift the hue of each pixel.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="HueAdjustmentValue">Value to adjust the hue by.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool SillySaturationLuminanceSwap(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)Silly_SwapSaturationLuminance(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());

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
        /// Convert an HSL image to an RGB image.
        /// </summary>
        /// <param name="HSLBuffer">Buffer of HSL values in double format.</param>
        /// <param name="DoubleCount">Number of doubles in <paramref name="HSLBuffer"/>.</param>
        /// <param name="Destination">Destination buffer.</param>
        /// <param name="DestinationWidth">Width of the destination image.</param>
        /// <param name="DestinationHeight">Height of the destination image.</param>
        /// <param name="DestinationStride">Stride of the destination image.</param>
        /// <returns>Value indicating operational results.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_MakeRGBFromHSL@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int MakeRGBFromHSL(void* HSLBuffer, UInt32 DoubleCount, void* Destination, Int32 DestinationWidth,
           Int32 DestinationHeight, Int32 DestinationStride);

        /// <summary>
        /// Convert an HSL value (in the form of an array of doubles) to an RGB image.
        /// </summary>
        /// <param name="Source">Source image to convert.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="HSLDoubles">Will contain the buffer of HSL doubles.</param>
        /// <param name="DoubleCount">Number of doubles in <paramref name="HSLDoubles"/>.</param>
        /// <param name="Result">Actual result value.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ConvertHSLImageToRGBImage(double[] HSLDoubles, int DoubleCount,
            WriteableBitmap Destination, int DestinationWidth, int DestinationHeight, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Destination.Lock();
                fixed (void* HSLBuffer = HSLDoubles)
                {
                    OpReturn = (ReturnCode)MakeRGBFromHSL(HSLBuffer, (uint)DoubleCount,
                        Destination.BackBuffer.ToPointer(), DestinationWidth, DestinationHeight,
                        Destination.BackBufferStride);
                }
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Convert an RGB image to an HSL image.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="SourceWidth">Width of the source.</param>
        /// <param name="SourceHeight">Height of the source.</param>
        /// <param name="SourceStride">Stride of the source.</param>
        /// <param name="DoubleBuffer">HSL double buffer.</param>
        /// <returns>Value indicating operational results.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_GetHSLImage@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int GetHSLImage(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            void* DoubleBuffer);

        /// <summary>
        /// Convert an RGB image to a buffer of HSL double values (one double per H, S, and L).
        /// </summary>
        /// <param name="Source">Source image to convert.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="HSLDoubles">Will contain the buffer of HSL doubles.</param>
        /// <param name="DoubleCount">Number of doubles in <paramref name="HSLDoubles"/>.</param>
        /// <param name="Result">Actual result value.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ConvertRGBImageToHSLImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           out double[] HSLDoubles, out int DoubleCount, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            DoubleCount = (Source.BackBufferStride * SourceHeight) * 3;
            HSLDoubles = new double[DoubleCount];
            unsafe
            {
                Source.Lock();
                fixed (void* HSLBuffer = HSLDoubles)
                {
                    OpReturn = (ReturnCode)GetHSLImage(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                        Source.BackBufferStride, HSLBuffer);
                }
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Restrict an image's colors to certain HSL ranges.
        /// </summary>
        /// <param name="Source">Source image to restrict.</param>
        /// <param name="SourceWidth">Width of the image.</param>
        /// <param name="SourceHeight">Height of the image.</param>
        /// <param name="SourceStride">Stride of the image.</param>
        /// <param name="DestinationBuffer">Where the processed image will be placed.</param>
        /// <param name="HueRangeSize">Number of hue ranges.</param>
        /// <param name="SaturationRangeSize">Number of saturation ranges.</param>
        /// <param name="LuminanceRangeSize">Number of luminance ranges.</param>
        /// <param name="RestrictHue">Restrict the hue.</param>
        /// <param name="RestrictSaturation">Restrict the saturation.</param>
        /// <param name="RestrictLuminance">Restrict the luminance.</param>
        /// <returns>Value indicating operational results.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_RestrictHSL@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RestrictHSL(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
          void* DestinationBuffer, int HueRangeSize, int SaturationRangeSize, int LuminanceRangeSize,
          bool RestrictHue, bool RestrictSaturation, bool RestrictLuminance);

        /// <summary>
        /// Restrict an image's colors to certain HSL ranges.
        /// </summary>
        /// <param name="Source">Source image to restrict.</param>
        /// <param name="SourceWidth">Width of the image.</param>
        /// <param name="SourceHeight">Height of the image.</param>
        /// <param name="Destination">Where the new image will be placed.</param>
        /// <param name="RestrictHue">Restrict the hue to certain range segments.</param>
        /// <param name="HueRange">Number of hue segments.</param>
        /// <param name="RestrictSaturation">Restrict the saturation to certain range segments.</param>
        /// <param name="SaturationRange">Number of saturation segments.</param>
        /// <param name="RestrictLuminance">Restrict the luminance to certain range segments.</param>
        /// <param name="LuminanceRange">Number of luminance segments.</param>
        /// <param name="Result">Operational result value.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool RestrictHSLToRanges(WriteableBitmap Source, int SourceWidth, int SourceHeight,
            WriteableBitmap Destination, bool RestrictHue, int HueRange, bool RestrictSaturation, int SaturationRange,
            bool RestrictLuminance, int LuminanceRange, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RestrictHSL(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                        Source.BackBufferStride, Destination.BackBuffer.ToPointer(), HueRange, LuminanceRange, SaturationRange,
                        RestrictHue, RestrictSaturation, RestrictLuminance);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HSLColorReduction@48", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HSLColorReduction(void* Source, Int32 Width, Int32 Height, Int32 Stride,
            void* Destination, int HueRanges, bool ReduceSaturation, double SaturationValue,
            bool ReduceLuminance, double LuminanceValue);

        public bool ReduceHSLColors(WriteableBitmap Source, int SourceWidth, int SourceHeight,
            WriteableBitmap Destination, int HueRanges, bool ReduceSaturation, double NewSaturation,
            bool ReduceLuminance, double NewLuminance, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)HSLColorReduction(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                        Source.BackBufferStride, Destination.BackBuffer.ToPointer(),
                        HueRanges, ReduceSaturation, NewSaturation, ReduceLuminance, NewLuminance);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_HSLBulkSet@56", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HSLBulkSet(void* Source, Int32 Width, Int32 Height, Int32 Stride,
                 void* Destination, bool SetHue, double NewHue, bool SetSaturation, double NewSaturation,
                 bool SetLuminance, double NewLuminance);

        public bool BulkSetHSL(WriteableBitmap Source, int SourceWidth, int SourceHeight,
               WriteableBitmap Destination, bool SetHue, double NewHue, bool SetSaturation,
               double NewSaturation, bool SetLuminance, double NewLuminance, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)HSLBulkSet(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                        Source.BackBufferStride, Destination.BackBuffer.ToPointer(),
                        SetHue, NewHue, SetSaturation, NewSaturation, SetLuminance, NewLuminance);
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
        /// Instructions on how to conditionally change colors in HSL mode.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ConditionalHSLAdjustment
        {
            /// <summary>
            /// Determines the hue low range for conditional changes.
            /// </summary>
            public double RangeLow;
            /// <summary>
            /// Determines the hue high range for conditional changes.
            /// </summary>
            public double RangeHigh;
            /// <summary>
            /// Determines if the hue is modified.
            /// </summary>
            public bool ModifyHue;
            /// <summary>
            /// Operand to apply to the source hue value.
            /// </summary>
            public double HueOperand;
            /// <summary>
            /// Operation to apply the operand to the hue.
            /// </summary>
            public int HueOperation;
            /// <summary>
            /// Determines if the saturation is modified.
            /// </summary>
            public bool ModifySaturation;
            /// <summary>
            /// Operand to apply to the source saturation value.
            /// </summary>
            public double SaturationOperand;
            /// <summary>
            /// Operation to apply the operand to the saturation.
            /// </summary>
            public int SaturationOperation;
            /// <summary>
            /// Determines if the luminance is modified.
            /// </summary>
            public bool ModifyLuminance;
            /// <summary>
            /// Operand to apply to the source luminance value.
            /// </summary>
            public double LuminanceOperand;
            /// <summary>
            /// Operation to apply the operand to the luminance.
            /// </summary>
            public int LuminanceOperation;
        };

        [DllImport("ColorBlender.dll", EntryPoint = "_HSLConditionalModify@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int HSLConditionalModify(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination,
           void* Conditions, int ConditionalCount);

        public bool ModifyHSLConditionally(WriteableBitmap Source, int SourceWidth, int SourceHeight,
             WriteableBitmap Destination, List<ConditionalHSLAdjustment> Conditions, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            ConditionalHSLAdjustment[] CondArray = Conditions.ToArray();
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                fixed (void* ConditionalArray = CondArray)
                {
                    OpReturn = (ReturnCode)HSLConditionalModify(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                            Source.BackBufferStride, Destination.BackBuffer.ToPointer(),
                            ConditionalArray, Conditions.Count);
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

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBtoHSLtoRGB@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RGBtoHSLtoRGB(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        public bool ConvertRGBtoHSLtoRGB(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RGBtoHSLtoRGB(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBImageToHueImage@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RGBImageToHueImage(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        public bool ConvertRGBImageToHueImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
             WriteableBitmap Destination, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RGBImageToHueImage(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBImageToSaturationImage@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RGBImageToSaturationImage(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        public bool ConvertRGBImageToSaturationImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RGBImageToSaturationImage(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBImageToLuminanceImage@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RGBImageToLuminanceImage(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        public bool ConvertRGBImageToLuminanceImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
            WriteableBitmap Destination, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RGBImageToLuminanceImage(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RGBImageToSLImage@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RGBImageToSLImage(void* Source, Int32 Width, Int32 Height, Int32 Stride, void* Destination);

        public bool ConvertRGBImageToSLImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RGBImageToSLImage(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer());
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RestrictHues2@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RestrictHues2(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
          void* DestinationBuffer, double HueCount);

        public bool RestrictHuesInImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, double HueCount, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RestrictHues2(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer(), HueCount);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_RestrictHueRange@36", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int RestrictHuesToRange(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
           void* DestinationBuffer, double LowHue, double HighHue);

        public bool RestrictHuesToRangeInImage(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, double LowHue, double HighHue, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();
                OpReturn = (ReturnCode)RestrictHuesToRange(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Source.BackBufferStride, Destination.BackBuffer.ToPointer(), LowHue, HighHue);
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
                Source.Unlock();
            }
            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}

public enum HSLOperators : int
{
    HSLMultiple = 0,
    HSLAdd = 1,
    HSLDivide = 2,
    HSLSubtract = 3,
    HSLReplace = 4,
}
