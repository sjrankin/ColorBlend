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
    /// Interaction logic for ColorReductionPage.xaml
    /// </summary>
    public partial class ColorReductionPage : Page, IFilterPage
    {
        public ColorReductionPage () : base()
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

        public ColorReductionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        public ImageMan ParentWindow = null;
        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            ImageSurface.Source = (WriteableBitmap)Original;
        }

        private void ExecuteColorReduction (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            string Which = "";
            string sCount = "0";
            string LCount = "0";
            if (OctreeCheck.IsChecked.Value)
            {
                Which = "Octree";
                sCount = OctreeColorCount.Text;
                if (string.IsNullOrEmpty(sCount))
                    return;
            }
            else
            {
                if (MedianCutCheck.IsChecked.Value)
                {
                    Which = "Median Cut";
                    sCount = MedianCutColorCount.Text;
                    if (string.IsNullOrEmpty(sCount))
                        return;
                }
                else
                {
                    Which = "Color Levels";
                    LCount = ColorLevelCount.Text;
                    if (string.IsNullOrEmpty(LCount))
                        return;
                }
            }
            List<OptionalValue> Options = new List<OptionalValue>();
            Options.Add(new OptionalValue("ReductionType", Which, typeof(string)));
            Options.Add(new OptionalValue("ColorCount", sCount, typeof(int)));
            Options.Add(new OptionalValue("LevelCount", LCount, typeof(int)));
            //ParentWindow.CommandSink("ColorReduction", Options);
        }
    }
}
