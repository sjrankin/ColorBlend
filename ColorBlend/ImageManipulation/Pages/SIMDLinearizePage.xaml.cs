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
using System.Diagnostics.Contracts;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for SIMDLinearizePage.xaml
    /// </summary>
    public partial class SIMDLinearizePage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SIMDLinearizePage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public SIMDLinearizePage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            Contract.Assert(ParentWindow != null);
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;
        }

        /// <summary>
        /// Clear the image.
        /// </summary>
        public void Clear()
        {
            Original = null;
        }

        /// <summary>
        /// Emit a pipeline stage.
        /// </summary>
        /// <returns>Pipeline stage for the rotation of images.</returns>
        public StageBase EmitPipelineStage()
        {
            return null;
        }

        /// <summary>
        /// Holds a reference to the color blender interface.
        /// </summary>
        private ColorBlenderInterface CBI;
        /// <summary>
        /// The original image.
        /// </summary>
        public WriteableBitmap Original = null;
        /// <summary>
        /// The image surface.
        /// </summary>
        private Image ImageSurface;
        /// <summary>
        /// The parent window.
        /// </summary>
        public ImageMan ParentWindow = null;

        /// <summary>
        /// Reset the local image.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ResetLocalImage(object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
            WriteableBitmap Scratch = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
        }

        private RGBChannelOrders GetChannelOrder()
        {
            if (RGBOrder.IsChecked.Value)
                return RGBChannelOrders.RGBOrder;
            if (RBGOrder.IsChecked.Value)
                return RGBChannelOrders.RBGOrder;
            if (GRBOrder.IsChecked.Value)
                return RGBChannelOrders.GRBOrder;
            if (GBROrder.IsChecked.Value)
                return RGBChannelOrders.GBROrder;
            if (BRGOrder.IsChecked.Value)
                return RGBChannelOrders.BRGOrder;
            if (BGROrder.IsChecked.Value)
                return RGBChannelOrders.BGROrder;
            return RGBChannelOrders.RGBOrder;
        }

        /// <summary>
        /// Execute the filter.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ExecuteFilter(object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;
            ColorBlenderInterface.ReturnCode Result = ColorBlenderInterface.ReturnCode.NotSet;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);

            RGBChannelOrders Order = GetChannelOrder();
            OK = CBI.LinearizeChannels(Original, Original.PixelWidth, Original.PixelHeight, DB,
                Order, out Result);

            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
            }
            else
            {
                string ErrorMessage = CBI.ErrorMessage((int)Result);
                StringBuilder sb = new StringBuilder();
                sb.Append(ErrorMessage);
                sb.Append(" (");
                sb.Append(Result.ToString());
                sb.Append(")");
                ParentWindow.SetMessage("Error", sb.ToString());
            }
        }
    }

    /// <summary>
    /// Channel orders for RGB channels.
    /// </summary>
    public enum RGBChannelOrders : int
    {
        /// <summary>
        /// Red, green, blue.
        /// </summary>
        RGBOrder = 0,
        /// <summary>
        /// Red, blue, green.
        /// </summary>
        RBGOrder = 1,
        /// <summary>
        /// Green, red, blue.
        /// </summary>
        GRBOrder = 2,
        /// <summary>
        /// Green, blue, red.
        /// </summary>
        GBROrder = 3,
        /// <summary>
        /// Blue, red, green.
        /// </summary>
        BRGOrder = 4,
        /// <summary>
        /// Blue, green, red.
        /// </summary>
        BGROrder = 5,
    }
}
