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
        [DllImport("ColorBlender.dll", EntryPoint = "_SteganographyAddConstant@28", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SteganographyAddConstant (void* SourceImage, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            void* FinalImage, Int32 FinalWidth, Int32 FinalHeight, Int32 FinalStride,
            byte Constant,
            Int32 Offset, bool UseRed, bool UseGreen, bool UseBlue,
            Int32 MaskSize, Int32 HeaderOffset);

        [DllImport("ColorBlender.dll", EntryPoint = "_SteganographyGetConstant@44", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SteganographyGetConstant (void* SourceImage, Int32 SourceWidth, Int32 SourceHeight, Int32 SourceStride,
            Int32 Offset, bool UseRed, bool UseGreen, bool UseBlue,
            Int32 MaskSize, Int32 HeaderOffset, byte* Constant);

        [DllImport("ColorBlender.dll", EntryPoint = "_SteganographyPresent@8", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int SteganographyPresent (void* SourceImage, Int32 HeaderOffset);

        [DllImport("ColorBlender.dll", EntryPoint = "_GetSteganographicType@8", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int GetSteganographicType (void* SourceImage, Int32 Offset);

        private Guid SteganographicID = new Guid("D86FC59D-819B-4D58-9AEB-AA4CB405485E");

        /// <summary>
        /// Determines if steganography is present (meaning, the type of steganography that we write, not necessarily anyone
        /// else writes).
        /// </summary>
        /// <param name="SourceBuffer">The image to check for steganography.</param>
        /// <param name="HeaderOffset">Location of the header in the image.</param>
        /// <returns>True if the image contains steganography, false if not.</returns>
        public bool HasSteganography (WriteableBitmap SourceBuffer, int HeaderOffset = 0)
        {
            unsafe
            {
                SourceBuffer.Lock();
                SteganographicType StegType = (SteganographicType)SteganographyPresent(SourceBuffer.BackBuffer.ToPointer(), (Int32)HeaderOffset);
                SourceBuffer.Unlock();
                if (StegType != SteganographicType.HeaderPresent)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get the type of steganography contain in <paramref name="SourceBuffer"/>.
        /// </summary>
        /// <param name="SourceBuffer">The image whose type of steganography will be returned.</param>
        /// <param name="Offset">Location of the header.</param>
        /// <param name="StegType">The type of steganography found in <paramref name="SourceBuffer."/>.</param>
        /// <returns>True if a valid/known type of steganography was found, false if not (probably no header was found).</returns>
        public bool GetSteganographicDataType (WriteableBitmap SourceBuffer, int Offset, out SteganographicType StegType)
        {
            StegType = SteganographicType.NotDetected;
            unsafe
            {
                SourceBuffer.Lock();
                int TypeValue = GetSteganographicType(SourceBuffer.BackBuffer.ToPointer(), Offset);
                SourceBuffer.Unlock();
                switch (TypeValue)
                {
                    case 1:
                        StegType = SteganographicType.Image;
                        return true;

                    case 2:
                        StegType = SteganographicType.Text;
                        return true;

                    case 3:
                        StegType = SteganographicType.Binary;
                        return true;

                    case 4:
                        StegType = SteganographicType.Constant;
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add a constant to <paramref name="SourceBuffer"/> via steganographic means.
        /// </summary>
        /// <param name="SourceBuffer">Image source.</param>
        /// <param name="SourceWidth">Width of the source image.</param>
        /// <param name="SourceHeight">Height of the source image.</param>
        /// <param name="DestinationBuffer">Destination image.</param>
        /// <param name="DestWidth">Width of the destination image.</param>
        /// <param name="DestHeight">Height of the destination image.</param>
        /// <param name="Constant">The constant to write. All bytes after the header will be modified.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool AddSteganographicConstant (WriteableBitmap SourceBuffer, int SourceWidth, int SourceHeight,
            WriteableBitmap DestinationBuffer, int DestWidth, int DestHeight, byte Constant)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();
                DestinationBuffer.Lock();

                OpReturn = (ReturnCode)SteganographyAddConstant(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, DestinationBuffer.BackBuffer.ToPointer(),
                    DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight, DestinationBuffer.BackBufferStride,
                    Constant, 0, true, true, true, 2, 0);

                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, DestinationBuffer.PixelWidth, DestinationBuffer.PixelHeight);
                DestinationBuffer.AddDirtyRect(DirtyRect);
                DestinationBuffer.Unlock();
                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Return the steganographic constant in <paramref name="SourceBuffer"/>.
        /// </summary>
        /// <param name="SourceBuffer">The image that contains the cosntant.</param>
        /// <param name="SourceWidth">The width of the source image.</param>
        /// <param name="SourceHeight">The height of the source image.</param>
        /// <param name="Constant">On success, will contain the constant.</param>
        /// <returns>True on success, false on failure. If false is returned, <paramref name="Constant"/> is undefined.</returns>
        public bool GetSteganographicConstant (WriteableBitmap SourceBuffer, int SourceWidth, int SourceHeight, out byte Constant)
        {
            Constant = 0;
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                SourceBuffer.Lock();

                byte SConstant;
                OpReturn = (ReturnCode)SteganographyGetConstant(SourceBuffer.BackBuffer.ToPointer(), SourceBuffer.PixelWidth,
                    SourceBuffer.PixelHeight, SourceBuffer.BackBufferStride, 0, true, true, true, 2, 0, &SConstant);
                Constant = SConstant;

                SourceBuffer.Unlock();
            }

            LastReturnCode = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        public bool CreateSteganography (WriteableBitmap DestBuffer, int DestWidth, int DestHeight,
            WriteableBitmap SourceBuffer, int SourceWidth, int SourceHeight,
            SteganographicChannels Channel, int MaskSize, int Offset = 0)
        {
            return false;
        }

        public bool CreateSteganography (WriteableBitmap DestBuffer, int DestWidth, int DestHeight,
            string Message,
            SteganographicChannels Channel, int MaskSize, int Offset = 0)
        {
            return false;
        }

        public SteganographicType ContainsSteganography (WriteableBitmap Buffer, int Width, int Height)
        {
            return SteganographicType.NotDetected;
        }

        public void GetSteganographicHeader (WriteableBitmap Buffer, int Width, int Height)
        {
            if (ContainsSteganography(Buffer, Width, Height) == SteganographicType.NotDetected)
                return;
        }

        public bool ExtractSteganographicImage (WriteableBitmap SourceBuffer, int SourceWidth, int SourceHeight,
            WriteableBitmap DestBuffer, int DestWidth, int DestHeight)
        {
            return false;
        }

        public string ExtractSteganographicMessage (WriteableBitmap Buffer, int Width, int Height)
        {
            return "";
        }

        public enum SteganographicType : int
        {
            NotDetected = 0,
            Image = 1,
            Text = 2,
            Binary = 3,
            Constant = 4,
            Unknown = 99,
            HeaderPresent = 100
        }

        public enum SteganographicChannels : int
        {
            ChannelRGB = 0,
            ChannelRG = 1,
            ChannelRB = 2,
            ChannelGB = 3,
            ChannelR = 4,
            ChannelG = 5,
            ChannelB = 6,
            RotateChannel = 7
        }
    }
}
