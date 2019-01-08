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
    /// Interaction logic for ColorSpaceConversionPage.xaml
    /// </summary>
    public partial class ColorSpaceConversionPage : Page, IFilterPage
    {
        public ColorSpaceConversionPage () : base()
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

        public ColorSpaceConversionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            ComboBoxItem CI = ColorSpaceCombo.SelectedItem as ComboBoxItem;
            if (CI == null)
                return;
            string NewColorSpace = CI.Content as string;
            if (string.IsNullOrEmpty(NewColorSpace))
                return;
            List<OptionalValue> Options = new List<OptionalValue>();
            Options.Add(new OptionalValue("ToColorSpace", NewColorSpace, typeof(string)));
            //ParentWindow.CommandSink("ConvertColorSpace", Options);
        }
    }
}
