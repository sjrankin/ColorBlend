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
    /// Interaction logic for UserConvolutionPage.xaml
    /// </summary>
    public partial class UserConvolutionPage : Page, IFilterPage
    {
        public UserConvolutionPage () : base()
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

        public UserConvolutionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            double Bias = GetInputDouble(UserKernelBiasInput);
            double Factor = GetInputDouble(UserKernelFactorInput);
            List<double> Matrix = new List<double>();
            Matrix.Add(GetInputDouble(M00));
            Matrix.Add(GetInputDouble(M10));
            Matrix.Add(GetInputDouble(M20));
            Matrix.Add(GetInputDouble(M01));
            Matrix.Add(GetInputDouble(M11));
            Matrix.Add(GetInputDouble(M21));
            Matrix.Add(GetInputDouble(M02));
            Matrix.Add(GetInputDouble(M12));
            Matrix.Add(GetInputDouble(M22));
            double LuminanceValue = GetInputDouble(LuminanceThresholdInput);
            bool SkipTransparent = SkipTransparentPix.IsChecked.Value;
            bool UseLuminance = UseLuminanceThresholdCheck.IsChecked.Value;
            bool IncludeTransparent = UseTransparentPix.IsChecked.Value;

            bool OK = CBI.MasterImageKernalConvolution(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                Matrix, 3, 3, Bias, Factor, SkipTransparent, IncludeTransparent, UseLuminance, LuminanceValue);
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
