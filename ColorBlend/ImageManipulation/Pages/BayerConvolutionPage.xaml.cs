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
    /// Interaction logic for BayerConvolutionPage.xaml
    /// </summary>
    public partial class BayerConvolutionPage : Page, IFilterPage
    {
        public BayerConvolutionPage () : base()
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

        public BayerConvolutionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
            {
                ParentWindow.SetMessage("Huh?");
                return;
            }
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = CBI.ImageKernelConvolution(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                KernelTypes.BayerDecodeCubicA);
            if (!OK)
            {
                ParentWindow.SetMessage("Error");
                return;
            }
            WriteableBitmap Final = new WriteableBitmap(DB.PixelWidth, DB.PixelHeight, DB.DpiX, DB.DpiY,
               PixelFormats.Bgra32, null);
            OK = CBI.ImageKernelConvolution(DB, Final, DB.PixelWidth, DB.PixelHeight, DB.BackBufferStride,
              KernelTypes.BayerDecodeCubicB);
            if (OK)
            {
                ParentWindow.ImageSurface.Source = Final;
//                ImageSurface.Source = Final;
                ParentWindow.DrawHistogram(Final);
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }
    }
}
