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
    /// Interaction logic for ImageInversionPage.xaml
    /// </summary>
    public partial class ImageInversionPage : Page, IFilterPage
    {
        public ImageInversionPage () : base()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return null;
        }

        public ImageInversionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        public ImageMan ParentWindow = null;
        public ColorBlenderInterface CBI;
        public WriteableBitmap Original;
        public Image ImageSurface;

        byte GetByteValue (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return 0;
            Raw = Raw.Trim();
            if (string.IsNullOrEmpty(Raw))
                return 0;
            if (Raw.Length > 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    return Convert.ToByte(Raw.Substring(2), 16);
                }
            }
            byte Final = 0;
            if (!byte.TryParse(Raw, out Final))
                return 0;
            return Final;
        }

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            bool UseOptional = EnableOptional.IsChecked.HasValue ? EnableOptional.IsChecked.Value : false;
            bool OK = false;
            if (UseOptional)
            {
                bool InvertA = InvertAlphaChannel.IsChecked.HasValue ? InvertAlphaChannel.IsChecked.Value : false;
                bool UseAThreshold = UseAlphaThresholdCheck.IsChecked.HasValue ? UseAlphaThresholdCheck.IsChecked.Value : false;
                bool InvertR = InvertRedChannel.IsChecked.HasValue ? InvertRedChannel.IsChecked.Value : false;
                bool UseRThreshold = UseRedThresholdCheck.IsChecked.HasValue ? UseRedThresholdCheck.IsChecked.Value : false;
                bool InvertG = InvertGreenChannel.IsChecked.HasValue ? InvertGreenChannel.IsChecked.Value : false;
                bool UseGThreshold = UseGreenThresholdCheck.IsChecked.HasValue ? UseGreenThresholdCheck.IsChecked.Value : false;
                bool InvertB = InvertBlueChannel.IsChecked.HasValue ? InvertBlueChannel.IsChecked.Value : false;
                bool UseBThreshold = UseBlueThresholdCheck.IsChecked.HasValue ? UseBlueThresholdCheck.IsChecked.Value : false;
                byte AThreshold = GetByteValue(AlphaThresholdBox.Text);
                byte RThreshold = GetByteValue(RedThresholdBox.Text);
                byte GThreshold = GetByteValue(GreenThresholdBox.Text);
                byte BThreshold = GetByteValue(BlueThresholdBox.Text);
                OK = CBI.ImageInvert4(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                          InvertA, InvertR, InvertG, InvertB, UseAThreshold, AThreshold,
                          UseRThreshold, RThreshold, UseGThreshold, GThreshold, UseBThreshold, BThreshold);
            }
            else
            {
                OK = CBI.ImageColorInvert(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride);
            }
            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }
    }
}
