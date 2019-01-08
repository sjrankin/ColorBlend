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
    /// Interaction logic for BlockSegmentsPage.xaml
    /// </summary>
    public partial class BlockSegmentsPage : Page, IFilterPage
    {
        public BlockSegmentsPage () : base()
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

        public BlockSegmentsPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;

            SegmentTypes SegmentType = SegmentTypes.SegmentMeanColor;
            if ((bool)MedianColorButton.IsChecked)
                SegmentType = SegmentTypes.SegmentMedianColor;
            if ((bool)BrightestColorButton.IsChecked)
                SegmentType = SegmentTypes.SegmentBrightestColor;
            if ((bool)DarkestColorButton.IsChecked)
                SegmentType = SegmentTypes.SegmentDarkestColor;
            if ((bool)LuminanceButton.IsChecked)
                SegmentType = SegmentTypes.SegmentLuminence;

            SegmentShapeTypes ShapeType = SegmentShapeTypes.Rectange;
            if ((bool)RectangularShapeButton.IsChecked)
                ShapeType = SegmentShapeTypes.Rectange;
            if ((bool)SquareShapeButton.IsChecked)
                ShapeType = SegmentShapeTypes.Square;
            if ((bool)CircleShapeButton.IsChecked)
                ShapeType = SegmentShapeTypes.Circle;
            if ((bool)EllipseShapeButton.IsChecked)
                ShapeType = SegmentShapeTypes.Ellipse;

            int BlocksX = 0;
            int BlocksY = 0;
            string HB = HBlockSizeIn.Text;
            if (string.IsNullOrEmpty(HB))
                HB = "100";
            if (!int.TryParse(HB, out BlocksX))
                return;
            string VB = VBlockSizeIn.Text;
            if (string.IsNullOrEmpty(VB))
                VB = "100";
            if (!int.TryParse(VB, out BlocksY))
                return;
            int ShapeMgn = 0;
            string SM = ShapeMarginIn.Text;
            if (string.IsNullOrEmpty(SM))
                SM = "0";
            if (!int.TryParse(SM, out ShapeMgn))
                return;
            bool EnableGradientTrans = EnableGradientTransparency.IsChecked.Value;
            bool EnableTrans = EnableTransparencyOverride.IsChecked.Value;
            string OT = SegmentAlphaIn.Text;
            double OverTrans = 1.0;
            if (string.IsNullOrEmpty(OT))
                OT = "1.0";
            if (!double.TryParse(OT, out OverTrans))
                return;
            bool InvertSpatially = InvertSpatialRegion.IsChecked.Value;

            OK = CBI.ImageSegmentBlock(Original, BlocksX, BlocksY, DB, SegmentType,
               ShapeMgn, ShapeType, ShapeBackgroundColor.CurrentColor,EnableTrans,
               EnableGradientTrans,OverTrans,InvertSpatially);

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

        /// <summary>
        /// Handle the suggest pixelate size button click.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void GenerateGCDBlock (object Sender, RoutedEventArgs e)
        {
            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            double Width = Math.Round(Original.Width);
            double Height = Math.Round(Original.Height);
            double G = GCD(Width, Height);
            int iG = (int)G;
            if (iG < 8)
                iG = 8;
            HBlockSizeIn.Text = iG.ToString();
            VBlockSizeIn.Text = iG.ToString();
        }

        /// <summary>
        /// Return the greatest common denominator between <paramref name="A"/> and
        /// <paramref name="B"/> using Euclid's algorithm.
        /// </summary>
        /// <param name="A">First number.</param>
        /// <param name="B">Second number.</param>
        /// <returns>Greatest common denomiator between <paramref name="A"/> and <paramref name="B"/>.</returns>
        private double GCD (double A, double B)
        {
            while (B != 0.0)
            {
                double T = B;
                B = A % B;
                A = T;
            }
            return A;
        }
    }
}
