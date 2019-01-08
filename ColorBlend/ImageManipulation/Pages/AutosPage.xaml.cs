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
    /// Interaction logic for AutosPage.xaml
    /// </summary>
    public partial class AutosPage : Page, IFilterPage
    {
        public AutosPage() : base()
        {
            InitializeComponent();
            MeanColorSample.Background = Brushes.Transparent;
            MeanColorSample.ToolTip = "Mean color not yet calculated";
            MeanColorRGBText.Text = "";
            MeanColorHueText.Text = "";
        }

        public void Clear()
        {
            Original = null;
            MeanColorSample.Background = Brushes.Transparent;
            MeanColorSample.ToolTip = "Mean color not yet calculated";
            MeanColorRGBText.Text = "";
            MeanColorHueText.Text = "";
        }

        public StageBase EmitPipelineStage()
        {
            return null;
        }

        public AutosPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
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

        private void ExecuteAutos(object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            //            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;
            if (SepiaTone.IsChecked.Value)
            {
                OK = CBI.ImageSepiaTone(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride);
            }
            else
                if (AutoContrast.IsChecked.Value)
            {
                OK = CBI.ImageAutoContrast(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride);
            }
            else
                if (AutoSaturation.IsChecked.Value)
            {
                OK = CBI.ImageAutoSaturate(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride);
            }
            else
                if (MeanImageColor.IsChecked.Value)
            {
                OK = CBI.ImageMeanImageColor2(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, out Color MeanColor);
                StringBuilder sb = new StringBuilder();
                sb.Append(MeanColor.A);
                sb.Append(",");
                sb.Append(MeanColor.R);
                sb.Append(",");
                sb.Append(MeanColor.G);
                sb.Append(",");
                sb.Append(MeanColor.B);
                MeanColorSample.ToolTip = sb.ToString();
                MeanColorSample.Background = new SolidColorBrush(MeanColor);
                MeanColorRGBText.Text = sb.ToString();
                CBI.NativeRGBtoHSL(MeanColor.R, MeanColor.G, MeanColor.B, out double H, out double S, out double L);
                StringBuilder sb2 = new StringBuilder();
                sb2.Append(H.ToString("N2"));
                sb2.Append("°");
                MeanColorHueText.Text = sb2.ToString();
            }
            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
            {
                ParentWindow.SetMessage("Error");
            }
        }
    }
}
