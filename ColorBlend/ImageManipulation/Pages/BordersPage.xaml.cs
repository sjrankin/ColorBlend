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
    /// Interaction logic for BordersPage.xaml
    /// </summary>
    public partial class BordersPage : Page, IFilterPage
    {
        public BordersPage () : base()
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

        public BordersPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        /// <summary>
        /// Get a writeable bitmap from <paramref name="TheImage"/>. Converts the format to BRGA32 if needed.
        /// </summary>
        /// <param name="TheImage">The image from which a writeable bitmap will be returned.</param>
        /// <returns>WriteableBitmap in the format of BGRA32.</returns>
        private WriteableBitmap GetWriteableBitmap (BitmapSource TheImage)
        {
            if (TheImage.Format != PixelFormats.Bgra32)
            {
                FormatConvertedBitmap NewImage = new FormatConvertedBitmap();
                NewImage.BeginInit();
                NewImage.Source = TheImage;
                NewImage.DestinationFormat = PixelFormats.Bgra32;
                NewImage.EndInit();
                return new WriteableBitmap(NewImage);
            }
            else
                return new WriteableBitmap(TheImage);
        }

        private void ExecuteFilterParamaters (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;

            Color BGColor = BorderBackgroundColor.CurrentColor;
            int LeftBorder = 0;
            string LB = LeftBorderIn.Text;
            if (string.IsNullOrEmpty(LB))
                LB = "10";
            if (!int.TryParse(LB, out LeftBorder))
                LeftBorder = 10;
            int TopBorder = 0;
            string TB = TopBorderIn.Text;
            if (string.IsNullOrEmpty(TB))
                TB = "10";
            if (!int.TryParse(TB, out TopBorder))
                TopBorder = 10;
            int RightBorder = 0;
            string RB = RightBorderIn.Text;
            if (string.IsNullOrEmpty(RB))
                RB = "10";
            if (!int.TryParse(RB, out RightBorder))
                RightBorder = 10;
            int BottomBorder = 0;
            string BB = BottomBorderIn.Text;
            if (string.IsNullOrEmpty(BB))
                BB = "10";
            if (!int.TryParse(BB, out BottomBorder))
                BottomBorder = 10;

            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth + LeftBorder + RightBorder,
                Original.PixelHeight + TopBorder + BottomBorder, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;

            OK = CBI.AddBorderToImage(Original, DB, LeftBorder, TopBorder, RightBorder,
                BottomBorder, BGColor);

            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
            {
                string ErrMsg = "";
                CBI.ColorLibraryErrors.TryGetValue((int)CBI.LastReturnCode, out ErrMsg);
                if (string.IsNullOrEmpty(ErrMsg))
                    ErrMsg = "[" + CBI.LastReturnCode.ToString() + "]";
                ParentWindow.SetMessage("Error", ErrMsg);
            }
        }
    }
}
