using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace ColorBlend
{
    /// <summary>
    /// Encapsulates the rather chaotic imaging API in .Net to something useful.
    /// </summary>
    public class UImage : ICloneable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UImage()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Other">Other UImage to use as source for this UImage.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="Other"/> is null.</exception>
        public UImage(UImage Other)
        {
            if (Other == null)
                throw new ArgumentNullException(nameof(Other));
            if (Other.Loaded)
            {
                Load(Other.LoadedFileName);
            }
        }

        /// <summary>
        /// Clone the current instance.
        /// </summary>
        /// <returns>Object-cast copy of the instance.</returns>
        public object Clone()
        {
            UImage Cloned = new UImage(this);
            return (object)Cloned;
        }

        /// <summary>
        /// Load the image file whose name is <paramref name="FileName"/>.
        /// </summary>
        /// <param name="FileName">Name of the file to load.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool Load(string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
                return false;
            if (!File.Exists(FileName))
                return false;

            Uri ImageURI = new Uri(FileName);
            _ImageSource = new BitmapImage(ImageURI);
            _BitmapImage = _ImageSource as BitmapImage;
            _BitmapSource = _ImageSource as BitmapSource;
            InitializeImage();

            LoadedFileName = FileName;
            Loaded = true;
            return true;
        }

        private void InitializeImage()
        {
            Width = _BitmapImage.PixelWidth;
            Height = _BitmapImage.PixelHeight;
            _WriteableBitmap = new WriteableBitmap(_BitmapImage);
            Stride = _WriteableBitmap.BackBufferStride;

            ImageSizeInBytes = Stride.Value * Height.Value;
            Pixels = new byte[ImageSizeInBytes];
            _BitmapSource.CopyPixels(Pixels, Stride.Value, 0);
            Format = _BitmapImage.Format;

            DPIX = (int)_BitmapImage.DpiX;
            DPIY = (int)_BitmapImage.DpiY;
        }

        /// <summary>
        /// Save the current image with the specified file name.
        /// </summary>
        /// <param name="FileName">Name of the file to save the image in.</param>
        /// <param name="OverwriteExistingFile">If true, existing files are overwritten.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool Save(string FileName, bool OverwriteExistingFile)
        {
            if (string.IsNullOrEmpty(FileName))
                return false;
            if (!OverwriteExistingFile && File.Exists(FileName))
                return false;
            LoadedFileName = FileName;
            return true;
        }

        /// <summary>
        /// Save the image using the same name the file was loaded with.
        /// </summary>
        /// <param name="OverwriteExistingFile">If true, existing files are overwritten.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool Save(bool OverwriteExistingFile = true)
        {
            return Save(LoadedFileName, OverwriteExistingFile);
        }

        /// <summary>
        /// Holds the image source.
        /// </summary>
        private ImageSource _ImageSource = null;

        /// <summary>
        /// Return the image as an image source.
        /// </summary>
        /// <returns>ImageSource of the image.</returns>
        public ImageSource AsImageSource()
        {
            if (!Loaded)
                return null;
            return _ImageSource;
        }

        /// <summary>
        /// Holds the writeable bitmap of the image.
        /// </summary>
        private WriteableBitmap _WriteableBitmap = null;

        /// <summary>
        /// Return the image as a WriteableBitmap.
        /// </summary>
        /// <returns>WriteableBitmap of the image.</returns>
        public WriteableBitmap AsWriteableBitmap()
        {
            if (!Loaded)
                return null;
            return _WriteableBitmap;
        }

        /// <summary>
        /// Holds the bitmap image of the image.
        /// </summary>
        private BitmapImage _BitmapImage = null;

        /// <summary>
        /// Return the image as a BitmapImage.
        /// </summary>
        /// <returns>BitmapImage of the image.</returns>
        public BitmapImage AsBitmapImage()
        {
            if (!Loaded)
                return null;
            return _BitmapImage;
        }

        /// <summary>
        /// Holds the bitmap source of the image.
        /// </summary>
        private BitmapSource _BitmapSource = null;

        /// <summary>
        /// Return the image as a BitmapSource.
        /// </summary>
        /// <returns>BitmapSource of the image.</returns>
        public BitmapSource AsBitmapSource()
        {
            if (!Loaded)
                return null;
            return _BitmapSource;
        }

        /// <summary>
        /// Try to get the image's color at (<paramref name="X"/>,<paramref name="Y"/>).
        /// </summary>
        /// <param name="X">The horizontal coordinate.</param>
        /// <param name="Y">The vertical coordinate.</param>
        /// <param name="TheColor">
        /// On success, the color at (<paramref name="X"/>,<paramref name="Y"/>). On failure,
        /// undefined.
        /// </param>
        /// <returns>True on success, false on failure.</returns>
        public bool TryGetColor(int X, int Y, out Color TheColor)
        {
            TheColor = Colors.Transparent;
            if (!Loaded)
                return false;
            if (X < 0)
                return false;
            if (Y < 0)
                return false;
            if (X > Width - 1)
                return false;
            if (Y > Height - 1)
                return false;
            int Index = Y * Stride.Value;
            Index += X;
            TheColor = Color.FromArgb(Pixels[Index + 3], Pixels[Index + 2], Pixels[Index + 1], Pixels[Index + 0]);
            return true;
        }

        /// <summary>
        /// Return the color at (<paramref name="X"/>,<paramref name="Y"/>).
        /// </summary>
        /// <param name="X">Horizontal coordinate.</param>
        /// <param name="Y">Vertical coordinate.</param>
        /// <returns>The color at the specified coordinate.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if (<paramref name="X"/>,<paramref name="Y"/>) is invalid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no image has been loaded.
        /// </exception>
        public Color this[int X, int Y]
        {
            get
            {
                if (!Loaded)
                    throw new InvalidOperationException("Image not loaded.");
                if (X < 0)
                    throw new IndexOutOfRangeException("X less than 0.");
                if (Y < 0)
                    throw new IndexOutOfRangeException("Y less than 0.");
                if (X > Width - 1)
                    throw new IndexOutOfRangeException("X greater than image width.");
                if (Y > Height - 1)
                    throw new IndexOutOfRangeException("Y greater than image height.");
                int Index = Y * Stride.Value;
                Index += X;
                Color TheColor = Color.FromArgb(Pixels[Index + 3], Pixels[Index + 2], Pixels[Index + 1], Pixels[Index + 0]);
                return TheColor;
            }
        }

        /// <summary>
        /// After <seealso cref="GenerateMetrics"/> is called, will contain the sorted list of colors, sorted
        /// by quantity.
        /// </summary>
        public Dictionary<UInt32, int> ColorMetrics { get; private set; } = new Dictionary<uint, int>();

        /// <summary>
        /// Get the number of unique colors. Null is returned if <seealso cref="GenerateMetrics"/> was
        /// not called before calling this property.
        /// </summary>
        public Nullable<int> UniqueColorCount
        {
            get
            {
                if (ColorMetrics.Count == 0)
                    return null;
                return ColorMetrics.Count;
            }
        }

        /// <summary>
        /// Generate image metrics.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public bool GenerateMetrics()
        {
            if (!Loaded)
                return false;
            Dictionary<UInt32, int> Scratch = new Dictionary<UInt32, int>();
            int PixelSize = 4;
            for (int Row = 0; Row < Height.Value; Row++)
            {
                int RowOffset = Row * Stride.Value;
                for (int Column = 0; Column < Width.Value; Column++)
                {
                    int Index = RowOffset + Column * PixelSize;
                    UInt32 ImageValue = 0;
                    byte A = Pixels[Index + 3];
                    byte R = Pixels[Index + 2];
                    byte G = Pixels[Index + 1];
                    byte B = Pixels[Index + 0];
                    ImageValue = (UInt32)((A << 24) | (R << 16) | (G << 8) | (B << 0));
                    if (Scratch.ContainsKey(ImageValue))
                        Scratch[ImageValue]++;
                    else
                        Scratch.Add(ImageValue, 1);
                }
            }
            List<KeyValuePair<UInt32, int>> sl = Scratch.ToList();
            sl.Sort(delegate (KeyValuePair<UInt32, int> Data1, KeyValuePair<UInt32, int> Data2)
            {
                return Data1.Value.CompareTo(Data2.Value);
            }
            );
            ColorMetrics = sl.ToDictionary(p => p.Key, q => q.Value);
            return true;
        }

        /// <summary>
        /// Contains raw pixel values.
        /// </summary>
        private byte[] Pixels = null;

        #region Properties.
        /// <summary>
        /// Size of the image in pixels.
        /// </summary>
        public int ImageSizeInPixels { get; private set; } = 0;

        /// <summary>
        /// Size of the image in bytes.
        /// </summary>
        public int ImageSizeInBytes { get; private set; } = 0;

        /// <summary>
        /// Get the image loaded flag.
        /// </summary>
        public bool Loaded { get; private set; } = false;

        /// <summary>
        /// Get the name of the file the image was loaded under.
        /// </summary>
        public string LoadedFileName { get; private set; } = "";

        /// <summary>
        /// Get the DPI X value.
        /// </summary>
        public Nullable<int> DPIX { get; private set; } = null;

        /// <summary>
        /// Get the DPI Y value.
        /// </summary>
        public Nullable<int> DPIY { get; private set; } = null;

        /// <summary>
        /// Get the width of the image in pixels.
        /// </summary>
        public Nullable<int> Width { get; private set; } = null;

        /// <summary>
        /// Get the height of the image in scanlines.
        /// </summary>
        public Nullable<int> Height { get; private set; } = null;

        /// <summary>
        /// Get the stride of the image in bytes.
        /// </summary>
        public Nullable<int> Stride { get; private set; } = null;

        /// <summary>
        /// Get the format of the image.
        /// </summary>
        public Nullable<PixelFormat> Format { get; private set; } = null;
        #endregion
    }
}
