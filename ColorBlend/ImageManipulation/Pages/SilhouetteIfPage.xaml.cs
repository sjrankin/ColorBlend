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
    /// Interaction logic for SilhouetteIfPage.xaml
    /// </summary>
    public partial class SilhouetteIfPage : Page, IFilterPage
    {
        public SilhouetteIfPage () : base()
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

        public SilhouetteIfPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            bool OK = false;

            bool DoHue = HueCheck.IsChecked.Value;
            double HueIValue = 0.0;
            double HueRange = 0.0;
            if (DoHue)
            {
                if(!double.TryParse(HueValue.Text, out HueIValue))
                {
                    HueIValue = 0.0;
                }
                if (!double.TryParse(HueRangeInput.Text,out HueRange))
                {
                    HueRange = 0.0;
                }
            }
            bool DoSaturation = SaturationCheck.IsChecked.Value;
            double SatIValue = 0.0;
            double SatIRange = 0.0;
            if (DoSaturation)
            {
                if (!double.TryParse(SaturationValue.Text, out SatIValue))
                {
                    SatIValue = 0.0;
                }
                if (!double.TryParse(SaturationRange.Text,out SatIRange))
                {
                    SatIRange = 0.0;
                }
            }
            bool DoLuminance = LuminanceCheck.IsChecked.Value;
            double LumIValue = 0.0;
            bool LumGreater = false;
            if (DoLuminance)
            {
                if (!double.TryParse(LuminanceValue.Text,out LumIValue))
                {
                    LumIValue = 0.0;
                }
                LumGreater = LuminanceDirection.IsChecked.Value;
            }
            Color SilColor = SilhouetteColor.CurrentColor;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            OK = CBI.DoSilhouetteIf(Original, Original.PixelWidth, Original.PixelHeight, DB,
                DoHue, HueIValue, HueRange, DoSaturation, SatIValue, SatIRange, DoLuminance, LumIValue, LumGreater,
                SilColor);

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
