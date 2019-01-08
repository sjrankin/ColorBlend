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
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestinationWidth">Width of the destination image.</param>
        /// <param name="DestinationHeight">Height of the destination image.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_ImageRotateRight90@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageRotateRight90(void* Source, Int32 SourceWidth, Int32 SourceHeight,
              void* Destination, Int32 DestinationWidth, Int32 DestinationHeight);

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
        /// <param name="DestWidth">Width of the destination image.</param>
        /// <param name="DestHeight">Height of the destination image.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool RotateImageRightBy90(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, int DestWidth, int DestHeight, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)ImageRotateRight90(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Destination.BackBuffer.ToPointer(), DestWidth, DestHeight);

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
        /// Color blender function to rotate images by 180° clockwise.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestinationWidth">Width of the destination image.</param>
        /// <param name="DestinationHeight">Height of the destination image.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_ImageRotateRight180@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageRotateRight180(void* Source, Int32 SourceWidth, Int32 SourceHeight,
              void* Destination, Int32 DestinationWidth, Int32 DestinationHeight);

        /// <summary>
        /// Rotate the image <paramref name="Source"/> by 180° clockwise.
        /// </summary>
        /// <remarks>
        /// Stride isn't required because the image is rotated on a UInt32 basis, not a byte basis.
        /// </remarks>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestWidth">Width of the destination image.</param>
        /// <param name="DestHeight">Height of the destination image.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool RotateImageRightBy180(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, int DestWidth, int DestHeight, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)ImageRotateRight180(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Destination.BackBuffer.ToPointer(), DestWidth, DestHeight);

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
        /// Color blender function to rotate images by 180° clockwise.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestinationWidth">Width of the destination image.</param>
        /// <param name="DestinationHeight">Height of the destination image.</param>
        /// <returns></returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_ImageRotateRight270@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageRotateRight270(void* Source, Int32 SourceWidth, Int32 SourceHeight,
              void* Destination, Int32 DestinationWidth, Int32 DestinationHeight);

        /// <summary>
        /// Rotate the image <paramref name="Source"/> by 270° clockwise.
        /// </summary>
        /// <remarks>
        /// Stride isn't required because the image is rotated on a UInt32 basis, not a byte basis.
        /// </remarks>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestWidth">Width of the destination image.</param>
        /// <param name="DestHeight">Height of the destination image.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool RotateImageRightBy270(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, int DestWidth, int DestHeight, out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)ImageRotateRight270(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Destination.BackBuffer.ToPointer(), DestWidth, DestHeight);

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
        /// Color blender function to rotate images by 180° clockwise.
        /// </summary>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestinationWidth">Width of the destination image.</param>
        /// <param name="DestinationHeight">Height of the destination image.</param>
        /// <param name="RotateHow">How many degrees (limited to cardinal values) to rotate the image.</param>
        /// <returns>Value indicating operational result.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_ImageRotateRightBy@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageRotateRightBy(void* Source, Int32 SourceWidth, Int32 SourceHeight,
              void* Destination, Int32 DestinationWidth, Int32 DestinationHeight, int RotateHow);

        /// <summary>
        /// Rotate the image <paramref name="Source"/> by the indicated number of degrees.
        /// </summary>
        /// <remarks>
        /// Stride isn't required because the image is rotated on a UInt32 basis, not a byte basis.
        /// </remarks>
        /// <param name="Source">Source image.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="Destination">Destination image.</param>
        /// <param name="DestWidth">Width of the destination image.</param>
        /// <param name="DestHeight">Height of the destination image.</param>
        /// <param name="RotateBy">Determines how to rotate the image.</param>
        /// <param name="Result">Operational result.</param>
        /// <returns>Value indicating success or failure.</returns>
        public bool RotateImageRightBy(WriteableBitmap Source, int SourceWidth, int SourceHeight,
           WriteableBitmap Destination, int DestWidth, int DestHeight, RotateRightByDegrees RotateBy,
           out ReturnCode Result)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            if (RotateBy == RotateRightByDegrees.By0)
                return false;
            unsafe
            {
                Source.Lock();
                Destination.Lock();

                OpReturn = (ReturnCode)ImageRotateRightBy(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                    Destination.BackBuffer.ToPointer(), DestWidth, DestHeight, (int)RotateBy);

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

    /// <summary>
    /// Number of degrees to rotate images. Limited to cardinal values.
    /// </summary>
    public enum RotateRightByDegrees : int
    {
        /// <summary>
        /// Indicates no rotation.
        /// </summary>
        By0 = 0,
        /// <summary>
        /// Rotate right (clockwise) by 90°.
        /// </summary>
        RightBy90 = 90,
        /// <summary>
        /// Rotate left (counterclockwise) by 270°.
        /// </summary>
        LeftBy270 = 90,
        /// <summary>
        /// Rotate by 180°.
        /// </summary>
        By180 = 180,
        /// <summary>
        /// Rotate right (clockwise) by 270°.
        /// </summary>
        RightBy270 = 270,
        /// <summary>
        /// Rotate left (counterclockwise) by 90°.
        /// </summary>
        LeftBy90 = 270
    }
}
