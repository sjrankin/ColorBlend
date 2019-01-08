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
    /// Interaction logic for ScalingPage.xaml
    /// </summary>
    public partial class ScalingPage : Page, IFilterPage
    {
        public ScalingPage () : base()
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

        public ScalingPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        double GetDimension (TextBox TB)
        {
            if (TB == null)
                throw new ArgumentNullException("TB");
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
            {
                Raw = "100";
                TB.Text = Raw;
            }
            double Final = 0.0;
            bool ParsedOK = double.TryParse(Raw, out Final);
            if (!ParsedOK)
            {
                Final = 100.0;
                TB.Text = "100";
            }
            return Final;
        }

        private int GetScalingMethod ()
        {
            if (NearestNeighbor.IsChecked.Value)
                return 1;
            if (Bilinear.IsChecked.Value)
                return 2;
            return 0;
        }

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;

            int NewX = (int)GetDimension(NewWidth);
            int NewY = (int)GetDimension(NewHeight);
            bool ShowGrid = ShowGridCheck.IsChecked.Value;
            int ScalingMethod = GetScalingMethod();

            WriteableBitmap DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            OK = CBI.ImageScale(Original, Original.PixelWidth, Original.PixelHeight,
                DB, NewX, NewY, ScalingMethod);

            WriteableBitmap GDB = null;
            if (ShowGrid)
            {
                GDB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY, PixelFormats.Bgra32, null);
                OK = CBI.OverlayBufferWithGrid(DB, GDB, NewX, NewY, 32, 32, Colors.Red);
            }

            if (OK)
            {
                if (ShowGrid)
                {
                    ImageSurface.Source = GDB;
                    ParentWindow.DrawHistogram(DB);     //Draw the histogram of the buffer before grid lines were drawn on top of it.
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
