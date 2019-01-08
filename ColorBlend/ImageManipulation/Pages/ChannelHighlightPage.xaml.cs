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
    /// Interaction logic for ChannelHighlightPage.xaml
    /// </summary>
    public partial class ChannelHighlightPage : Page, IFilterPage
    {
        public ChannelHighlightPage () : base()
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

        public ChannelHighlightPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private byte ToByte (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return 0;
            Raw = Raw.Trim();
            if (Raw.Substring(0, 2).ToLower() == "0x")
                return Convert.ToByte(Raw.Substring(2), 16);
            byte Temp = 0x0;
            if (!byte.TryParse(Raw, out Temp))
                return 0;
            return Temp;
        }

        private double MakeHue (Color FromColor)
        {
            double Max = Math.Max(FromColor.R, Math.Max(FromColor.G, FromColor.B));
            double Min = Math.Min(FromColor.R, Math.Min(FromColor.G, FromColor.B));
            int Delta = (int)(Max - Min);
            double Hue = double.NaN;
            if (Max == FromColor.R)
            {
                Hue = 60 * ((FromColor.G - FromColor.B) / Delta);
            }
            else
                if (Max == FromColor.G)
            {
                Hue = 60 * (((FromColor.B - FromColor.R) / Delta) + 2);
            }
            else
            {
                Hue = 60 * (((FromColor.R - FromColor.G) / Delta) + 4);
            }
            if (Hue < 0)
                Hue += 360;
            return Hue;
        }

        ColorBlenderInterface.ImageColorNonHighlightAction NonHighlightAction ()
        {
            if (BGTransparent.IsChecked.Value)
                return ColorBlenderInterface.ImageColorNonHighlightAction.NonHighlightTransparent;
            if (BGInvert.IsChecked.Value)
                return ColorBlenderInterface.ImageColorNonHighlightAction.NonHighlightInvert;
            return ColorBlenderInterface.ImageColorNonHighlightAction.NonHighlightGrayscale;
        }

        private Dictionary<ColorBlenderInterface.ImageColorHighlights, double> ColorHueMap = new Dictionary<ColorBlenderInterface.ImageColorHighlights, double>()
        {
            {ColorBlenderInterface.ImageColorHighlights.HighlightRed , 0.0},
            {ColorBlenderInterface.ImageColorHighlights.HighlightGreen, 120.0 },
            {ColorBlenderInterface.ImageColorHighlights.HighlightBlue, 240.0 },
            {ColorBlenderInterface.ImageColorHighlights.HighlightYellow, 60.0 },
            {ColorBlenderInterface.ImageColorHighlights.HighlightCyan, 180.0 },
            {ColorBlenderInterface.ImageColorHighlights.HighlightMagenta, 300.0 }
        };

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            ColorBlenderInterface.ImageColorNonHighlightAction NonHightlight = NonHighlightAction();
            //            ColorBlenderInterface.ImageColorHighlights HighlightColor = (ColorBlenderInterface.ImageColorHighlights)ChannelSelectionBox.SelectedIndex;
            double HighlightHue = ColorHueMap[(ColorBlenderInterface.ImageColorHighlights)ChannelSelectionBox.SelectedIndex];
            //double HueTest = MakeHue(Colors.Magenta);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                PixelFormats.Bgra32, null);
            string RawLum = HighlightLuminanceBox.Text;
            double HighlightLuminance = 1.0;
            if (string.IsNullOrEmpty(RawLum))
            {
                HighlightLuminanceBox.Text = "1.0";
                HighlightLuminance = 1.0;
            }
            else
            {
                if (!double.TryParse(RawLum, out HighlightLuminance))
                {
                    HighlightLuminanceBox.Text = "1.0";
                    HighlightLuminance = 1.0;
                }
            }
            double HueDelta = 0.0;
            bool OK = CBI.HighlightColorInImage(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                HighlightHue, NonHightlight, ref HueDelta, HighlightLuminance);
            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK - " + HueDelta.ToString());
            }
            else
                ParentWindow.SetMessage("Error");
        }
    }
}
