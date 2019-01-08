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
    /// Interaction logic for SolarizationPage.xaml
    /// </summary>
    public partial class SolarizationPage : Page, IFilterPage
    {
        public SolarizationPage () : base()
        {
            InitializeComponent();
        }

        public void Clear ()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return SolStage;
        }

        public void PopulateFromStage(StageBase Stage)
        {
            if (Stage == null)
                return;
        }

        private StageBase SolStage = null;

        public SolarizationPage(ImageMan ParentWindow, StageBase Stage)
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            SolStage = Stage;
            PopulateFromStage(Stage);
        }

        public SolarizationPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);

            double SThreshold = 0.5;
            if (!double.TryParse(SolarizeThresholdInput.Text, out SThreshold))
                SThreshold = 0.5;
            bool DoInvert = InvertThreshold.IsChecked.HasValue ? InvertThreshold.IsChecked.Value : false;
            bool OK = CBI.Solarize(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, SThreshold, DoInvert);

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
