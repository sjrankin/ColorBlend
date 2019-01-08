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
    /// Interaction logic for CroppingPage.xaml
    /// </summary>
    public partial class CroppingPage : Page, IFilterPage
    {
        public CroppingPage () : base()
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

        public CroppingPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

            int Left = 0;
            string LB = LeftCropIn.Text;
            if (string.IsNullOrEmpty(LB))
                LB = "10";
            if (!int.TryParse(LB, out Left))
                Left = 10;
            int Top = 0;
            string TB = TopCropIn.Text;
            if (string.IsNullOrEmpty(TB))
                TB = "10";
            if (!int.TryParse(TB, out Top))
                Top = 10;
            int Right = 0;
            string RB = RightCropIn.Text;
            if (string.IsNullOrEmpty(RB))
                RB = "10";
            if (!int.TryParse(RB, out Right))
                Right = 10;
            int Bottom = 0;
            string BB = BottomCropIn.Text;
            if (string.IsNullOrEmpty(BB))
                BB = "10";
            if (!int.TryParse(BB, out Bottom))
                Bottom = 10;

            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth - (Left + Right),
                Original.PixelHeight - (Top + Bottom), Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;

            OK = CBI.CropImage(Original, DB, Left, Top, Right, Bottom);

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
            LeftCropIn.Text = "0";
            TopCropIn.Text = "0";
            RightCropIn.Text = "0";
            BottomCropIn.Text = "0";
        }
    }
}
