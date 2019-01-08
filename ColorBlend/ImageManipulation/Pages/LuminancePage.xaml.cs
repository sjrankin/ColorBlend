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
    /// Interaction logic for LuminancePage.xaml
    /// </summary>
    public partial class LuminancePage : Page, IFilterPage
    {
        public LuminancePage() : base()
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

        public LuminancePage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        byte GetByteValue(string Raw)
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

        private void ResetLocalImage(object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private void ExecuteFilter(object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            bool OK = false;
            bool InvertLum = InvertLuminance.IsChecked.HasValue ? InvertLuminance.IsChecked.Value : false;
            bool UseAThreshold = UseLuminanceThresholdCheck.IsChecked.HasValue ? UseLuminanceThresholdCheck.IsChecked.Value : false;
            byte AThreshold = GetByteValue(LuminanceThresholdBox.Text);
            OK = CBI.LuminanceInversion(Original, Original.PixelWidth, Original.PixelHeight, DB, UseAThreshold, AThreshold);
            //OK = CBI.ImageInvert4(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
            //          InvertLum, InvertR, InvertG, InvertB, UseAThreshold, AThreshold,
            //          UseRThreshold, RThreshold, UseGThreshold, GThreshold, UseBThreshold, BThreshold);

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
