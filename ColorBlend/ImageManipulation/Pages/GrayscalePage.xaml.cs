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
    /// Interaction logic for GrayscalePage.xaml
    /// </summary>
    public partial class GrayscalePage : Page, IFilterPage
    {
        public GrayscalePage () : base()
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

        public GrayscalePage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private ColorBlenderInterface.GrayscaleTypes GetGrayType ()
        {
            ColorBlenderInterface.GrayscaleTypes GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_Mean;
            if (BrightnessMap.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_Brightness;
            if (Mean.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_Mean;
            if (Perceptual.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_Perceptual;
            if (Desaturation.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_Desaturation;
            if (MaxDecomposition.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_MaxDecomposition;
            if (MinDecomposition.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_MinDecomposition;
            if (AlphaChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_AlphaChannel;
            if (RedChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_RedChannel;
            if (GreenChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_GreenChannel;
            if (BlueChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_BlueChannel;
            if (CyanChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_CyanChannel;
            if (MagentaChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_MagentaChannel;
            if (YellowChannel.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_YellowChannel;
            if (ShadeLevels.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_ShadeLevel;
            if (BT601.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_BT601;
            if (BT709.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_BT709;
            if (GrayLevelCheck.IsChecked.Value)
                GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_GrayLevels;
            return GrayscaleType;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool InvertResult = InvertGray.IsChecked.Value;
            string LvlC = "0";
            LvlC = GrayLevelInput.Text;
            if (string.IsNullOrEmpty(LvlC))
                return;
            if (!LvlC.ContainsValidInteger())
                return;
            int Levels = 0;
            if (!int.TryParse(LvlC, out Levels))
                return;
            ColorBlenderInterface.GrayscaleTypes GrayType = GetGrayType();
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                PixelFormats.Bgra32, null);
            bool OK = false;
            if (GrayType == ColorBlenderInterface.GrayscaleTypes.Grayscale_GrayLevels)
                OK = CBI.ImageGrayscaleLevels(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, Levels);
            else
                OK = CBI.ImageGrayscale(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, (int)GrayType);
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
