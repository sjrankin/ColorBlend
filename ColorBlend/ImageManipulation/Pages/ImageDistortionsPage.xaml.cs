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
    /// Interaction logic for ImageDistortionsPage.xaml
    /// </summary>
    public partial class ImageDistortionsPage : Page, IFilterPage
    {
        public ImageDistortionsPage () : base()
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

        public void PopulateFromStage (StageBase Stage)
        {
            if (Stage == null)
                return;
        }

        private StageBase SolStage = null;

        public ImageDistortionsPage (ImageMan ParentWindow, StageBase Stage)
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            SolStage = Stage;
            PopulateFromStage(Stage);
        }

        public ImageDistortionsPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private int GetFrequencyValue (TextBox TB)
        {
            if (TB == null)
                throw new ArgumentNullException("TB");
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
            {
                Raw = "1";
                TB.Text = Raw;
            }
            int Frequency = 0;
            bool ParsedOK = int.TryParse(Raw, out Frequency);
            if (!ParsedOK)
            {
                TB.Text = "1";
                return 1;
            }
            if (Frequency < 1)
            {
                TB.Text = "1";
                return 1;
            }
            return Frequency;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;
            WriteableBitmap DB = null;
            if (ImageSquishCheck.IsChecked.Value)
            {
                int HFreq = GetFrequencyValue(HorizontalFrequencyInput);
                int VFreq = GetFrequencyValue(VerticalFrequencyInput);
                if (HFreq < Original.PixelWidth && VFreq < Original.PixelHeight)
                {
                    int NewWidth = (Original.PixelWidth / HFreq);
                    NewWidth += Original.PixelWidth & HFreq;
                    int NewHeight = (Original.PixelHeight / VFreq);
                    NewHeight += Original.PixelHeight % VFreq;
                    DB = new WriteableBitmap(NewWidth, NewHeight, Original.DpiX, Original.DpiY, PixelFormats.Bgra32, null);
                    OK = CBI.ImageSquisher(Original, Original.PixelWidth, Original.PixelHeight,
                        DB, DB.PixelWidth, DB.PixelHeight, HFreq, VFreq);
                }
            }

            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK  " + DB.PixelWidth.ToString() + "x" + DB.PixelHeight.ToString());
            }
            else
                ParentWindow.SetMessage("Error");
        }
    }
}
