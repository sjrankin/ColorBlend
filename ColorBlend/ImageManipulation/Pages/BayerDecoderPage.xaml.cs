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
    /// Interaction logic for BayerDecoderPage.xaml
    /// </summary>
    public partial class BayerDecoderPage : Page, IFilterPage
    {
        public BayerDecoderPage () : base()
        {
            InitializeComponent();
        }

        public void Clear ()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return null;
        }

        public BayerDecoderPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            //            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            ColorBlenderInterface.BayerPatterns Pattern = ColorBlenderInterface.BayerPatterns.RGGB;
            if (BGGR.IsChecked.HasValue)
                if (BGGR.IsChecked.Value)
                    Pattern = ColorBlenderInterface.BayerPatterns.BGGR;
            ColorBlenderInterface.BayerDemosaicMethods Method = ColorBlenderInterface.BayerDemosaicMethods.NearestNeighbor;
            if (IsButtonChecked(Nearest))
                Method = ColorBlenderInterface.BayerDemosaicMethods.NearestNeighbor;
            if (IsButtonChecked(Linear))
                Method = ColorBlenderInterface.BayerDemosaicMethods.LinearInterpolation;
            if (IsButtonChecked(Cubic))
                Method = ColorBlenderInterface.BayerDemosaicMethods.CubicInterplation;

            OK = CBI.BayerDemosaic(Original, Original.PixelWidth, Original.PixelHeight, DB, Pattern, Method);

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
//                    ParentWindow.ImageSurface.Source = GDB;
                    ImageSurface.Source = GDB;
                    ParentWindow.DrawHistogram(DB);
                }
                else
                {
//                    ParentWindow.ImageSurface.Source = DB;
                    ImageSurface.Source = DB;
                    ParentWindow.DrawHistogram(DB);
                }
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }
    }
}
