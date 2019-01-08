using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for SteganographyReadPage.xaml
    /// </summary>
    public partial class SteganographyReadPage : Page, IFilterPage
    {
        public SteganographyReadPage () : base()
        {
            InitializeComponent();
            StegTypeOut.Text = "Click execute to determine type.";
            StegValueOut.Text = "";
            HeaderOffsetInput.Text = "0";
            DataOffsetInput.Text = "0";
        }

        public void Clear ()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return null;
        }

        public SteganographyReadPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;
        }

        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;
        public ImageMan ParentWindow = null;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private bool IsButtonChecked (RadioButton RB)
        {
            if (RB == null)
                return false;
            if (!RB.IsChecked.HasValue)
                return false;
            return RB.IsChecked.Value;
        }

        /// <summary>
        /// Get a writeable bitmap from <paramref name="TheImage"/>. Converts the format to BRGA32 if needed.
        /// </summary>
        /// <param name="TheImage">The image from which a writeable bitmap will be returned.</param>
        /// <returns>WriteableBitmap in the format of BGRA32.</returns>
        private WriteableBitmap GetWriteableBitmap (BitmapSource TheImage)
        {
            if (TheImage.Format != PixelFormats.Bgra32)
            {
                FormatConvertedBitmap NewImage = new FormatConvertedBitmap();
                NewImage.BeginInit();
                NewImage.Source = TheImage;
                NewImage.DestinationFormat = PixelFormats.Bgra32;
                NewImage.EndInit();
                return new WriteableBitmap(NewImage);
            }
            else
                return new WriteableBitmap(TheImage);
        }

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            StegTypeOut.Text = "";
            StegValueOut.Text = "";
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            bool OK = false;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            if (!CBI.HasSteganography(Original, 0))
            {
                ParentWindow.SetMessage("Error.");
                StegTypeOut.Text = "No steganography detected.";
                return;
            }

            ColorBlenderInterface.SteganographicType SType = ColorBlenderInterface.SteganographicType.NotDetected;
            bool FoundType = CBI.GetSteganographicDataType(Original, 0, out SType);
            if (!FoundType)
            {
                ParentWindow.SetMessage("Error.");
                StegTypeOut.Text = "Could not find type.";
                return;
            }

            switch (SType)
            {
                case ColorBlenderInterface.SteganographicType.Constant:
                    byte StoredConstant = 0;
                    OK = CBI.GetSteganographicConstant(Original, Original.PixelWidth, Original.PixelHeight, out StoredConstant);
                    if (!OK)
                    {
                        ParentWindow.SetMessage("Error extracting constant.");
                        StegTypeOut.Text = "";
                    }
                    StegTypeOut.Text = "Found steganographic constant.";
                    StegValueOut.Text = "0x" + StoredConstant.ToString("x2");
                    break;

                default:
                    break;
            }

            //            OK = CBI.SteganographyReading(Original, Original.PixelWidth, Original.PixelHeight, DB);

            bool ShowGrid = ShowGridCheck.IsChecked.Value;
            WriteableBitmap GDB = null;
            if (ShowGrid)
            {
                GDB = new WriteableBitmap(DB.PixelWidth, DB.PixelHeight, Original.DpiX, Original.DpiY, PixelFormats.Bgra32, null);
                OK = CBI.OverlayBufferWithGrid(DB, GDB, DB.PixelWidth, DB.PixelHeight, 32, 32, Colors.Red);
            }

            if (OK)
            {
                if (ShowGrid)
                {
                    ImageSurface.Source = GDB;
                    ParentWindow.DrawHistogram(DB);
                }
                else
                {
                    ImageSurface.Source = DB;
                    ParentWindow.DrawHistogram(DB);
                }
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }

        private void MergeImage (object sender, RoutedEventArgs e)
        {

        }
    }
}
