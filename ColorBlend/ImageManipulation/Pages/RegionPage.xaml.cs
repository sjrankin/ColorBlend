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
    /// Interaction logic for RegionPage.xaml
    /// </summary>
    public partial class RegionPage : Page, IFilterPage
    {
        public RegionPage () : base()
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

        public RegionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private int GetOperation ()
        {
            ComboBoxItem CBI = OperationCombo.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return -1;
            string raw = CBI.Name as string;
            if (string.IsNullOrEmpty(raw))
                return -1;
            raw = raw.ToLower();
            if (!RegionOps.ContainsKey(raw))
                return -1;
            return RegionOps[raw];
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            int Operation = GetOperation();
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);
            bool DoAlpha = DoAlphaCheck.IsChecked.Value;
            bool DoRed = DoRedCheck.IsChecked.Value;
            bool DoGreen = DoGreenCheck.IsChecked.Value;
            bool DoBlue = DoBlueCheck.IsChecked.Value;
            int RWidth = 0;
            if (!int.TryParse(RegionWidthInput.Text, out RWidth))
                return;
            int RHeight = 0;
            if (!int.TryParse(RegionHeightInput.Text, out RHeight))
                return;
            bool OK = false;
            OK = CBI.ImageRegionalOperation(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                Operation, (UInt32)RWidth, (UInt32)RHeight, DoAlpha, DoRed, DoGreen, DoBlue);
            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }

        private Dictionary<string, int> RegionOps = new Dictionary<string, int>()
        {
            {"greatest", 0 },
            {"least", 1 },
            {"brightest", 2 },
            {"darkest", 3 },
            {"mean", 4 },
        };
    }
}
