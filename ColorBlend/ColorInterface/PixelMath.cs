using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace ColorBlend
{
    public partial class ColorBlenderInterface
    {
        /// <summary>
        /// Color blender function to rotate images by 90° clockwise.
        /// </summary>
        /// <param name="SourceBuffer">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="SourceStride">Source image stride.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestinationBuffer">Processed image destination.</param>
        /// <param name="Operation">The operation to perform.</param>
        /// <param name="Constant">The constant to apply with <paramref name="Operation"/>.</param>
        /// <param name="ApplyToAlpha">Apply the operation to the alpha channel.</param>
        /// <param name="ApplyToRed">Apply the operation to the red channel.</param>
        /// <param name="ApplyToGreen">Apply the operation to the green channel.</param>
        /// <param name="ApplyToBlue">Apply the operation to the blue channel.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_PixelMathLogicalOperation@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int PixelMathLogicalOperation(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
             void* DestinationBuffer, int Operation, int Constant,
             bool ApplyToAlpha, bool ApplyToRed, bool ApplyToGreen, bool ApplyToBlue);

        /// <summary>
        /// Rotate the image <paramref name="Source"/> by 90° clockwise.
        /// </summary>
        /// <remarks>
        /// Stride isn't required because the image is rotated on a UInt32 basis, not a byte basis.
        /// </remarks>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="Operation">The operation to apply the constant with.</param>
        /// <param name="Constant">The constant value to apply.</param>
        /// <param name="ApplyToAlpha">Apply the constant/operation to the alpha channel.</param>
        /// <param name="ApplyToRed">Apply the constant/operation to the red channel.</param>
        /// <param name="ApplyToGreen">Apply the constant/operation to the green channel.</param>
        /// <param name="ApplyToBlue">Apply the constant/operation to the blue channel.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool PixelMathLogicalOperationWithConstant(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination,
           int Operation, int Constant, bool ApplyToAlpha, bool ApplyToRed, bool ApplyToGreen, bool ApplyToBlue,
           out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)PixelMathLogicalOperation(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight, Source.BackBufferStride,
                    Destination.BackBuffer.ToPointer(), Operation, Constant, ApplyToAlpha, ApplyToRed, ApplyToGreen, ApplyToBlue);

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
        /// <param name="SourceStride">Source image stride.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestinationBuffer">Processed image destination.</param>
        /// <param name="Operation">The operation to perform.</param>
        /// <param name="Constant">The constant to apply with <paramref name="Operation"/>.</param>
        /// <param name="ApplyToAlpha">Apply the operation to the alpha channel.</param>
        /// <param name="ApplyToRed">Apply the operation to the red channel.</param>
        /// <param name="ApplyToGreen">Apply the operation to the green channel.</param>
        /// <param name="ApplyToBlue">Apply the operation to the blue channel.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_PixelMathOperation@48", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int PixelMathOperation(void* SourceBuffer, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
           void* DestinationBuffer, int Operation, bool NormalizeResults, bool NormalizeValues,
           bool ApplyToAlpha, bool ApplyToRed, bool ApplyToGreen, bool ApplyToBlue);

        /// <summary>
        /// Rotate the image <paramref name="Source"/> by 90° clockwise.
        /// </summary>
        /// <remarks>
        /// Stride isn't required because the image is rotated on a UInt32 basis, not a byte basis.
        /// </remarks>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="Function">The function to apply the constant with.</param>
        /// <param name="NormalizeResults">Normalize the result.</param>
        /// <param name="NormalizeValues">Normalize values before use.</param>
        /// <param name="ApplyToAlpha">Apply the constant/operation to the alpha channel.</param>
        /// <param name="ApplyToRed">Apply the constant/operation to the red channel.</param>
        /// <param name="ApplyToGreen">Apply the constant/operation to the green channel.</param>
        /// <param name="ApplyToBlue">Apply the constant/operation to the blue channel.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool PixelMathApplyFunction(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination,
           int Function, bool NormalizeResults, bool NormalizeValues, bool ApplyToAlpha, bool ApplyToRed, bool ApplyToGreen, bool ApplyToBlue,
           out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)PixelMathOperation(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight, Source.BackBufferStride,
                    Destination.BackBuffer.ToPointer(), Function, NormalizeResults, NormalizeValues, 
                    ApplyToAlpha, ApplyToRed, ApplyToGreen, ApplyToBlue);

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
