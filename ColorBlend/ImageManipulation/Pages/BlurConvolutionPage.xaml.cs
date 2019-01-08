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
    /// Interaction logic for BlurConvolutionPage.xaml
    /// </summary>
    public partial class BlurConvolutionPage : Page, IFilterPage
    {
        public BlurConvolutionPage () : base()
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

        public BlurConvolutionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private KernelTypes GetBlurType()
        {
            if (Blur3x3.IsChecked.Value)
                return KernelTypes.Blur3x3;
            if (Blur5x5.IsChecked.Value)
                return KernelTypes.Blur5x5;
            if (Gaussian3x3.IsChecked.Value)
                return KernelTypes.Gaussian3x3;
            if (Gaussian5x5.IsChecked.Value)
                return KernelTypes.Gaussian5x5;
            if (MotionBlur.IsChecked.Value)
                return KernelTypes.MotionBlur;
            if (MotionBlurLR.IsChecked.Value)
                return KernelTypes.MotionBlurLR;
            if (MotionBlurRL.IsChecked.Value)
                return KernelTypes.MotionBlurRL;
            return KernelTypes.Blur3x3;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            KernelTypes BlurType = GetBlurType();
            bool OK = CBI.ImageKernelConvolution(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                BlurType);
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
