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
    /// Interaction logic for SwapChannelsPage.xaml
    /// </summary>
    public partial class SwapChannelsPage : Page, IFilterPage
    {
        public SwapChannelsPage () : base()
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

        public SwapChannelsPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private ColorBlenderInterface.ConditionalChannelSwaps GetConditional()
        {
            if (ConditionalNotSet.IsChecked.Value)
                return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_NotSet;
            if (SwapLuminanceGTE.IsChecked.Value)
                return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_LuminanceGTE;
            if (SwapLuminanceLTE.IsChecked.Value)
                return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_LuminanceLTE;
            if (SwapLuminanceR_GT_GB.IsChecked.Value)
                return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_R_GTE_GB;
            if (SwapLuminanceG_GT_RB.IsChecked.Value)
                return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_G_GTE_RB;
            if (SwapLuminanceB_GT_RG.IsChecked.Value)
                return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_B_GTE_RG;

            return ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_NotSet;
        }

        double GetLuminanceThreshold(string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return 0.0;
            double RawValue = 0.0;
            if (!double.TryParse(Raw, out RawValue))
                return 0.0;
            return RawValue;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = ParentWindow.GetImage();
            ComboBoxItem CI = ChannelOrder.SelectedItem as ComboBoxItem;
            if (CI == null)
                return;
            string Order = CI.Content as string;
            if (string.IsNullOrEmpty(Order))
                return;
            Order = Order.ToLower();
            if (!ChannelOrders.ContainsKey(Order))
                return;
            int SwapOrder = ChannelOrders[Order];

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;
            bool DoConditionals = EnableConditionals.IsChecked.HasValue ? EnableConditionals.IsChecked.Value : false;
            if (DoConditionals)
            {
                ColorBlenderInterface.ConditionalChannelSwaps Condition = GetConditional();
                double LumThreshold = 0.0;
                if (Condition== ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_LuminanceGTE)
                {
                    LumThreshold = GetLuminanceThreshold(LumGTE.Text);
                }
                if (Condition== ColorBlenderInterface.ConditionalChannelSwaps.SwapIf_LuminanceLTE)
                {
                    LumThreshold = GetLuminanceThreshold(LumLTE.Text);
                }
                OK = CBI.ImageChannelSwapConditionally(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, SwapOrder, LumThreshold, Condition);
            }
            else
            {
                OK = CBI.ImageChannelSwap(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, SwapOrder);
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

        private Dictionary<string, int> ChannelOrders = new Dictionary<string, int>()
        {
            {"rbg", 1 },
            {"grb", 2 },
            {"gbr", 3 },
            {"brg", 4 },
            {"bgr", 5 }
        };
    }
}
