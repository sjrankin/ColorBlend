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
    /// Interaction logic for SingleImageMaskPage.xaml
    /// </summary>
    public partial class SingleImageMaskPage : Page, IFilterPage
    {
        public SingleImageMaskPage () : base()
        {
            InitializeComponent();
        }

        public SingleImageMaskPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        public void Clear()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return null;
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

        private double GetAlphaBox (TextBox TB)
        {
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
                return 0.0;
            Raw = Raw.Trim();
            if (Raw.Substring(0, 2).ToLower() == "0x")
            {
                Raw = Raw.Substring(2);
                Int16 val = Convert.ToInt16(Raw, 16);
                if (val > 255)
                    return 0.0;
                if (val < 0)
                    return 0.0;
                return (double)val / 255.0;
            }
            double RawValue = 0.0;
            if (!double.TryParse(Raw, out RawValue))
                return 0.0;
            if (RawValue > 1.0)
            {
                if (RawValue > 255.0)
                    return 0;
                return RawValue / 255.0;
            }
            if (RawValue < 0.0)
                return 0.0;
            return RawValue;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;

            double LThreshold = 0;

            if (ColorMask.IsChecked.Value)
            {
                Color MaskColor = AbsoluteColorMask.CurrentColor;
                OK = CBI.MaskWithColor(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    MaskColor);
            }
            if (MaskIfLumGTE.IsChecked.Value)
            {
                if (!double.TryParse(LumGTE.Text, out LThreshold))
                    return;
                OK = CBI.ConditionalAlphaMask(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    LThreshold, false, Colors.Transparent);
            }
            if (MaskIfLumLTE.IsChecked.Value)
            {
                if (!double.TryParse(LumLTE.Text, out LThreshold))
                    return;
                OK = CBI.ConditionalAlphaMask(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    LThreshold, true, Colors.Transparent);
            }
            if (SetAlphaUnconditionallyCheck.IsChecked.Value)
            {
                double FinalAlpha = GetAlphaBox(NewAlpha);
                OK = CBI.UnconditionalAlphaSet(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    (byte)(FinalAlpha * 0xff));
            }
            if (AlphaSolarizeCheck.IsChecked.Value)
            {
                double SolAlpha = GetAlphaBox(SolarizedAlphaValue);
                OK = CBI.AlphaSolarizeImage(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, SolAlpha);
            }
            if (MaskFromLuminance.IsChecked.Value)
            {
                bool DoInvertAlphaLum = InvertAlphaFromLuminance.IsChecked.Value;
                OK = CBI.AlphaFromLuminance(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    DoInvertAlphaLum);
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
