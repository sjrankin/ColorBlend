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
    /// Interaction logic for RollingChannelMeanPage.xaml
    /// </summary>
    public partial class RollingChannelMeanPage : Page, IFilterPage
    {
        public RollingChannelMeanPage () : base()
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

        public RollingChannelMeanPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            ImageSurface.Source = (WriteableBitmap)Original;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            string RedW = RedChannelWindowSizeInput.Text;
            if (string.IsNullOrEmpty(RedW))
                return;
            string GreenW = GreenChannelWindowSizeInput.Text;
            if (string.IsNullOrEmpty(GreenW))
                return;
            string BlueW = BlueChannelWindowSizeInput.Text;
            if (string.IsNullOrEmpty(BlueW))
                return;
            int RedWindowSize = 0;
            if (!int.TryParse(RedW, out RedWindowSize))
                return;
            int GreenWindowSize = 0;
            if (!int.TryParse(GreenW, out GreenWindowSize))
                return;
            int BlueWindowSize = 0;
            if (!int.TryParse(BlueW, out BlueWindowSize))
                return;
            List<OptionalValue> Options = new List<OptionalValue>();
            Options.Add(new OptionalValue("RedWindow", RedWindowSize.ToString(), typeof(int)));
            Options.Add(new OptionalValue("GreenWindow", GreenWindowSize.ToString(), typeof(int)));
            Options.Add(new OptionalValue("BlueWindow", BlueWindowSize.ToString(), typeof(int)));
            //ParentWindow.CommandSink("RollingMeanChannels", Options);
        }
    }
}
