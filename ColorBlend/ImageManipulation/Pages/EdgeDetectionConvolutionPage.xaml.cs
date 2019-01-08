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
    /// Interaction logic for EdgeDetectionConvolutionPage.xaml
    /// </summary>
    public partial class EdgeDetectionConvolutionPage : Page, IFilterPage
    {
        public EdgeDetectionConvolutionPage () : base()
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

        public EdgeDetectionConvolutionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        KernelTypes GetEdgeType()
        {
            if (EdgeDetection.IsChecked.Value)
                return KernelTypes.EdgeDetection;
            if (EdgeDetection45.IsChecked.Value)
                return KernelTypes.EdgeDetection45;
            if (VerticalEdgeDetection.IsChecked.Value)
                return KernelTypes.VerticalEdgeDetection;
            if (HorizontalEdgeDetection.IsChecked.Value)
                return KernelTypes.HorizontalEdgeDetection;
            if (EdgeDetectionULtoBR.IsChecked.Value)
                return KernelTypes.EdgeDetectionULtoBR;
            if (Laplace.IsChecked.Value)
                return KernelTypes.Laplace;
            if (LaplaceDiagonals.IsChecked.Value)
                return KernelTypes.LaplaceDiagonals;
            if (Laplace9x9.IsChecked.Value)
                return KernelTypes.Laplace9x9;
            return KernelTypes.EdgeDetection;
        }

        double GetInputDouble (TextBox Source)
        {
            if (Source == null)
                throw new ArgumentNullException("Source");
            string SourceValue = Source.Text;
            double Final = 0.0;
            bool OK = double.TryParse(SourceValue, out Final);
            if (!OK)
                return 0.0;
            return Final;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            KernelTypes EdgeType = GetEdgeType();
            double LuminanceValue = GetInputDouble(LuminanceThresholdInput);
            bool SkipTransparent = SkipTransparentPix.IsChecked.Value;
            bool UseLuminance = UseLuminanceThresholdCheck.IsChecked.Value;
            bool IncludeTransparent = UseTransparentPix.IsChecked.Value;

            bool OK = CBI.ImageKernelConvolution2(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                EdgeType, SkipTransparent, IncludeTransparent, UseLuminance, LuminanceValue);
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
