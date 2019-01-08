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
    /// Interaction logic for SegmentPage.xaml
    /// </summary>
    public partial class SegmentPage : Page, IFilterPage
    {
        public SegmentPage () : base()
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

        public SegmentPage (ImageMan ParentWindow,  ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        int GetTextValue (TextBox TB)
        {
            if (TB == null)
                return -1;
            int v = 0;
            if (!int.TryParse(TB.Text, out v))
                return -1;
            return v;
        }

        Point GetOrigin ()
        {
            if (RectSegOrigin == null)
                return new Point(-1, -1);
            string raw = RectSegOrigin.Text;
            if (string.IsNullOrEmpty(raw))
                return new Point(-1, -1);
            raw = raw.Trim();
            string[] parts = raw.Split(new char[] { ',' });
            if (parts.Length != 2)
                return new Point(-1, -1);
            int x = 0;
            if (!int.TryParse(parts[0], out x))
                return new Point(-1, -1);
            int y = 0;
            if (!int.TryParse(parts[1], out y))
                return new Point(-1, -1);
            return new Point(x, y);
        }

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);
            int CellWidth = GetTextValue(RectWidth);
            if (CellWidth == -1)
                return;
            int CellHeight = GetTextValue(RectHeight);
            if (CellHeight == -1)
                return;
            Point Origin = GetOrigin();
            if (Origin.X == -1 || Origin.Y == -1)
                return;
            SegmentizationMirroring SegmentizeHow = SegmentizationMirroring.SegmentNoMirror;
            if (RectHorizontalMirror.IsChecked.Value)
                SegmentizeHow = SegmentizationMirroring.SegmentHorizontalMirror;
            else
                SegmentizeHow = SegmentizationMirroring.SegmentVerticalMirror;
            OK = CBI.ImageSegmentize(Original, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                DB, CellWidth, CellHeight, Origin, SegmentizeHow);

            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
            {
                ParentWindow.SetMessage("Error", CBI.LastReturnCode.ToString());
            }
        }
    }
}
