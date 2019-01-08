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
    /// Interaction logic for SubHistogramPage.xaml
    /// </summary>
    public partial class SubHistogramPage : Page, IFilterPage
    {
        public SubHistogramPage () : base()
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

        public SubHistogramPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
            ImageSurface.MouseMove += ImageSurface_MouseMove;
            ImageMousePositionChanged += NewMousePosition;
            //ParentWindow.ImageSurface.ImageMousePositionChanged += NewMousePosition;
        }

        private void ImageSurface_MouseMove (object Sender, MouseEventArgs e)
        {
            Image I = Sender as Image;
            if (I == null)
                return;
            if (ImageMousePositionChanged == null)
                return;
            Point MousePoint = e.GetPosition(I);
            double ViewPortWidth = I.ActualWidth;
            double ViewPortHeight = I.ActualHeight;
            NewMousePosition(this, new ImageMousePositionChangeArgs(MousePoint.X,
                MousePoint.Y, ViewPortWidth, ViewPortHeight));
        }

        public delegate void ImageMousePositionChangedEvent (object Sender, ImageMousePositionChangeArgs e);
        public event ImageMousePositionChangedEvent ImageMousePositionChanged;

        private void NewMousePosition (object Sender, ImageMousePositionChangeArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            string LB = WidthIn.Text;
            if (string.IsNullOrEmpty(LB))
                LB = "10";
            if (!int.TryParse(LB, out SegmentWidth))
                SegmentWidth = 10;
            string TB = HeightIn.Text;
            if (string.IsNullOrEmpty(TB))
                TB = "10";
            if (!int.TryParse(TB, out SegmentHeight))
                SegmentHeight = 10;
            if (SegmentWidth == 0 || SegmentHeight == 0)
                return;
            if (e == null)
                return;
            if (e.ViewportHeight == 0.0 || e.ViewportWidth == 0.0)
                return;
            if (SubHistogramViewer == null)
                return;

            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            WriteableBitmap DB = new WriteableBitmap(SegmentWidth,
              SegmentHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            SegmentImage.Source = DB;
            WriteableBitmap DB2 = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight,
                Original.DpiX, Original.DpiY, PixelFormats.Bgra32, null);

            double IX = ParentWindow.GetImage().PixelWidth;
            double IY = ParentWindow.GetImage().PixelHeight;
            int MX = (int)Math.Round(e.MouseX);
            int MY = (int)Math.Round(e.MouseY);
            ControlCoordOut.Text = MX.ToString() + "," + MY.ToString();
            double XMultiplier = IX / e.ViewportWidth;
            double YMultiplier = IY / e.ViewportHeight;
            int ImX = (int)Math.Round(MX * XMultiplier);
            int ImY = (int)Math.Round(MY * YMultiplier);
            ImageCoordOut.Text = ImX.ToString() + "," + ImY.ToString();
            int CX = (int)Math.Round((double)ImX / (double)SegmentWidth);
            int CY = (int)Math.Round((double)ImY / (double)SegmentHeight);
            SegmentCoordOut.Text = CX.ToString() + "," + CY.ToString();

            int X1 = CX * SegmentWidth;
            int Y1 = CY * SegmentHeight;
            int X2 = X1 + SegmentWidth;
            if (X2 > IX - 1)
                X2 = (int)IX - 1;
            int Y2 = Y1 + SegmentHeight;
            if (Y2 > IY - 1)
                Y2 = (int)IY - 1;

            bool OK = CBI.CopyImageRegion(Original, DB, X1, Y1, X2, Y2);

            ParentWindow.DrawHistogram(DB, SubHistogramViewer);

            if (HighlightSegmentCheck.IsChecked.Value)
            {
                if (ParentWindow.FitImageToSize)
                {
                    //Figure out the displayed image size to the actual image size ratio
                    //and update the rectangle coordinates.
                    double XRatio = ParentWindow.ImageSurface.ActualWidth / (double)Original.PixelWidth;
                    double YRatio = ParentWindow.ImageSurface.ActualHeight / (double)Original.PixelHeight;
                    X1 = (int)((double)X1 * XRatio);
                    X2 = (int)((double)X2 * XRatio);
                    Y1 = (int)((double)Y1 * YRatio);
                    Y2 = (int)((double)Y2 * YRatio);
                }
                CBI.RenderRectangle(Original, DB2, X1, Y1, X2, Y2, Colors.Red);
                ImageSurface.Source = DB2;
            }
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

        private int SegmentWidth = 8;
        private int SegmentHeight = 8;

        private void ExecuteFilterParamaters (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;

            string LB = WidthIn.Text;
            if (string.IsNullOrEmpty(LB))
                LB = "10";
            if (!int.TryParse(LB, out SegmentWidth))
                SegmentWidth = 10;
            string TB = HeightIn.Text;
            if (string.IsNullOrEmpty(TB))
                TB = "10";
            if (!int.TryParse(TB, out SegmentHeight))
                SegmentHeight = 10;

            BitmapSource BS = (BitmapSource)ParentWindow.GetImageControl().Source;
            Original = GetWriteableBitmap(BS);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth,
                Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            bool OK = false;

            int SegmentsX = Original.PixelWidth / SegmentWidth;
            int SegmentsY = Original.PixelHeight / SegmentHeight;

            OK = CBI.ImageSegmentBlock2(Original, SegmentsX, SegmentsY, DB,
                OutlineSegmentsCheck.IsChecked.Value, Colors.Red,
                false, 0, 0, Colors.Transparent);
            //OK = CBI.AddBorderToImage(Original, DB, LeftBorder, TopBorder, RightBorder,
            //    BottomBorder, BGColor);

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
