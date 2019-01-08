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
    /// Interaction logic for ThresholdPage.xaml
    /// </summary>
    public partial class ThresholdPage : Page, IFilterPage
    {
        public ThresholdPage () : base()
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

        public ThresholdPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            bool OK = false;
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);

            if (OneRegionEnable.IsChecked.Value)
            {
                string OneThresh = OneRegionThreshold.Text;
                double OneThreshold = 0.5;
                if (!double.TryParse(OneThresh, out OneThreshold))
                    OneThreshold = 0.5;
                bool InvertThresh = InvertThreshold0.IsChecked.HasValue ? InvertThreshold0.IsChecked.Value : false;
                OK = CBI.ImageThreshold0(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                    OneThreshold, ThresholdColorInput.CurrentColor, InvertThresh);
            }
            else
            {
                if (TwoRegionEnable.IsChecked.Value)
                {
                    string TwoThresh1 = TwoRegionThreshold.Text;
                    double TwoThreshold = 0.5;
                    if (!double.TryParse(TwoThresh1, out TwoThreshold))
                        TwoThreshold = 0.5;
                    Color LowColor = LowColorInput2.CurrentColor;
                    Color HighColor = HighColorInput2.CurrentColor;
                    OK = CBI.ImageThreshold(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                        TwoThreshold, null, LowColor, HighColor);
                }
                else
                {
                    string ThreshLow = LowThresholdInput.Text;
                    double LowThreshold = 0.25;
                    if (!double.TryParse(ThreshLow, out LowThreshold))
                        LowThreshold = 0.25;
                    string ThreshHigh = HighThresholdInput.Text;
                    double HighThreshold = 0.25;
                    if (!double.TryParse(ThreshHigh, out HighThreshold))
                        HighThreshold = 0.25;
                    Color LowColor = LowColorInput.CurrentColor;
                    Color HighColor = HighColorInput.CurrentColor;
                    OK = CBI.ImageThreshold(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, LowThreshold, HighThreshold, LowColor, HighColor);
                }
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

        private void AddThresholdRange (object Sender, RoutedEventArgs e)
        {
            ThresholdEditor TE = new ThresholdEditor();
            TE.ShowDialog();
            if (TE.ValidThreshold)
            {
                ThresholdData TD = new ThresholdData();
                TD.SetThresholdColor(TE.ThresholdColor);
                TD.LowThresholdValue = TE.LowThreshold;
                TD.HighThresholdValue = TE.HighThreshold;
                ThresholdUIList.Items.Add(TD);
            }
        }

        private void RemoveThresholdRange (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            if (ThresholdUIList == null)
                return;
            if (ThresholdUIList.Items.Count < 1)
                return;
            if (ThresholdUIList.SelectedIndex < 0)
                return;
            ThresholdUIList.Items.RemoveAt(ThresholdUIList.SelectedIndex);
        }

        private void ClearThresholdRange (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            if (ThresholdUIList == null)
                return;
            ThresholdUIList.Items.Clear();
        }
    }

    public class ThresholdData
    {
        public ThresholdData ()
        {
            ThresholdColorDisplay = new Border
            {
                Background = Brushes.Purple,
                Height = 22,
                Width = 50,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black,
                CornerRadius = new CornerRadius(2)
            };
        }
        public double LowThresholdValue { get; set; }
        public double HighThresholdValue { get; set; }
        public void SetThresholdColor (Color TheColor)
        {
            ThresholdColorDisplay.Background = new SolidColorBrush(TheColor);
        }
        public Border ThresholdColorDisplay { get; set; }
    }
}
