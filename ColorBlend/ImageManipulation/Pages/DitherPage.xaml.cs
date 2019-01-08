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
    /// Interaction logic for DitherPage.xaml
    /// </summary>
    public partial class DitherPage : Page, IFilterPage
    {
        public DitherPage() : base()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage()
        {
            return null;
        }

        public DitherPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private void ResetLocalImage(object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private bool IsButtonChecked(RadioButton RB)
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
        private WriteableBitmap GetWriteableBitmap(BitmapSource TheImage)
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

        private ColorBlenderInterface.DitheringTypes GetDitherType()
        {
            if (FloydSteinbergDithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.FloydSteinberg;

            if (FalseFloydSteinbergDithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.FalseFloydSteinberg;

            if (AtkinsonDithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.Atkinson;

            if (JarvisJudiceNinkeDithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.JarvisJudiceNinke;

            if (StuckiDithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.Stucki;

            if (BurkesDithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.Burkes;

            if (Sierra1Dithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.Sierra1;

            if (Sierra2Dithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.Sierra2;

            if (Sierra3Dithering.IsChecked.Value)
                return ColorBlenderInterface.DitheringTypes.Sierra3;

            return ColorBlenderInterface.DitheringTypes.UnknownType;
        }

        private void ExecuteFilter(object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            bool OK = false;

            ColorBlenderInterface.DitheringTypes DitherType = GetDitherType();
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            OK = CBI.Dithering(Original, Original.PixelWidth, Original.PixelHeight, DB);

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
    }
}
