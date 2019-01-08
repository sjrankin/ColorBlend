using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace Iro3.Data.Image
{
    /// <summary>
    /// Stores pixels from WriteableBitmaps.
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageData (int ImagePixelSize = 4)
        {
            Clear();
            _PixelSize = ImagePixelSize;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="DataSource">Source of the pixels.</param>
        public ImageData (ref WriteableBitmap DataSource)
        {
            if (DataSource == null)
                throw new ArgumentNullException("DataSource");
            SetData(ref DataSource, false);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="DataSource">Source of the pixels.</param>
        public ImageData (ImageData DataSource)
        {
            if (DataSource == null)
                throw new ArgumentNullException("DataSource");
            Clear();
            _PixelSize = DataSource.PixelSize;
            Width = DataSource.Width;
            Height = DataSource.Height;
            Stride = DataSource.Stride;
            _RawData = new byte[ImageSizeBytes];
            DataSource.Duplicate(ref _RawData);
        }

        private int _PixelSize = 4;
        /// <summary>
        /// Get the size of pixels in the image. Can be set upon class construction.
        /// </summary>
        public int PixelSize
        {
            get
            {
                return _PixelSize;
            }
        }

        /// <summary>
        /// Get pixel and dimensional information from <paramref name="DataSource"/>.
        /// </summary>
        /// <param name="DataSource">Source of pixels and dimensional information.</param>
        /// <param name="DisposeToo">If true, DataSource is "disposed" (has null assigned to it).</param>
        public void SetData (ref WriteableBitmap DataSource, bool DisposeToo)
        {
            if (DataSource == null)
                return;
            DataSource.Lock();
            Stride = DataSource.BackBufferStride;
            Height = DataSource.PixelHeight;
            Width = DataSource.PixelWidth;
            _RawData = new byte[ImageSizeBytes];
#if true
            unsafe
            {
                Marshal.Copy(DataSource.BackBuffer, _RawData, 0, ImageSizeBytes);
            }
#else
            DataSource.CopyPixels(RawData, Stride, 0);
#endif
            DataSource.Unlock();
            if (DisposeToo)
                DataSource = null;
        }

        /// <summary>
        /// Get the size of the image in bytes (<seealso cref="Stride"/> * <seealso cref="Height"/>).
        /// </summary>
        public int ImageSizeBytes
        {
            get
            {
                return Stride * Height;
            }
        }

        /// <summary>
        /// Get the size of the image in pixels (<seealso cref="Width"/> * <seealso cref="Height"/>).
        /// </summary>
        public int ImageSizePixels
        {
            get
            {
                return Width * Height;
            }
        }

        /// <summary>
        /// Clear pixel data and reset dimensions to initial values.
        /// </summary>
        public void Clear ()
        {
            _RawData = null;
            Width = 0;
            Height = 0;
            Stride = 0;
        }

        /// <summary>
        /// Determines if the data set is empty (e.g., null).
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _RawData == null;
            }
        }

        /// <summary>
        /// Get the width of the image buffer in pixels.
        /// </summary>
        public int Width { get; internal set; }

        /// <summary>
        /// Get the height of the image buffer in scanlines.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        /// Get the width of the image buffer in bytes.
        /// </summary>
        public int Stride { get; internal set; }

        private byte[] _RawData = null;
        /// <summary>
        /// Get the image data buffer.
        /// </summary>
        public byte[] RawData
        {
            get
            {
                return _RawData;
            }
        }

        /// <summary>
        /// Duplicate the current raw data.
        /// </summary>
        /// <param name="Destination">The destination of the duplication.</param>
        public void Duplicate (ref byte[] Destination)
        {
            Destination = new byte[ImageSizeBytes];
#if true
            Destination = (byte[])_RawData.Clone();
#else
            Buffer.BlockCopy(_RawData, 0, Destination, 0, ImageSizeBytes);
#endif
        }
    }
}
