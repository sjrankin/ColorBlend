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
    /// Interaction logic for ChannelSelectionPage.xaml
    /// </summary>
    public partial class ChannelSelectionPage : Page, IFilterPage
    {
        public ChannelSelectionPage () : base()
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

        public ChannelSelectionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        public ImageMan ParentWindow = null;
        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            ImageSurface.Source = ParentWindow.GetImage();
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;
            bool AsGray = AsGrayCheck.IsChecked.HasValue ? AsGrayCheck.IsChecked.Value : false;
            if (EnableRGB.IsChecked.Value)
            {
                bool SelectRed = RGB_Red.IsChecked.HasValue ? RGB_Red.IsChecked.Value : true;
                bool SelectGreen = RGB_Green.IsChecked.HasValue ? RGB_Green.IsChecked.Value : true;
                bool SelectBlue = RGB_Blue.IsChecked.HasValue ? RGB_Blue.IsChecked.Value : true;
                OK = CBI.ImageSelectRGBChannels(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    SelectRed, SelectGreen, SelectBlue, AsGray);
            }
            else
            if (EnableHSL.IsChecked.Value)
            {
                string HSLChannelOrder = "HSL";
                ComboBoxItem CBItem = HSLOrder.SelectedItem as ComboBoxItem;
                if (CBItem == null)
                {
                    HSLChannelOrder = "HSL";
                    HSLOrder.SelectedIndex = 0;
                }
                else
                {
                    HSLChannelOrder = CBItem.Content as string;
                    if (string.IsNullOrEmpty(HSLChannelOrder))
                    {
                        HSLChannelOrder = "HSL";
                        HSLOrder.SelectedIndex = 0;
                    }
                }
                bool SelectHue = HSL_Hue.IsChecked.HasValue ? HSL_Hue.IsChecked.Value : true;
                bool SelectSaturation = HSL_Saturation.IsChecked.HasValue ? HSL_Saturation.IsChecked.Value : true;
                bool SelectLuminance = HSL_Luminance.IsChecked.HasValue ? HSL_Luminance.IsChecked.Value : true;
                OK = CBI.ImageSelectHSLChannels(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    SelectHue, SelectSaturation, SelectLuminance, AsGray, HSLChannelOrder);
            }
            else
            if (EnableCMYK.IsChecked.Value)
            {
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
