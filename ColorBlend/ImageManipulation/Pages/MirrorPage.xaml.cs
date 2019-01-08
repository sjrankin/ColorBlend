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
    /// Interaction logic for MirrorPage.xaml
    /// </summary>
    public partial class MirrorPage : Page, IFilterPage
    {
        public MirrorPage () : base()
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

        public MirrorPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            Contract.Assert(ParentWindow != null);
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;
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

        Nullable<Point> GetPoint (TextBox Source)
        {
            string raw = Source.Text;
            if (string.IsNullOrEmpty(raw))
                return null;
            raw = raw.Trim();
            string[] parts = raw.Split(new char[] { ',', ' ' });
            if (parts.Length != 2)
                return null;
            int X1 = 0;
            if (!int.TryParse(parts[0], out X1))
                return null;
            int Y1 = 0;
            if (!int.TryParse(parts[1], out Y1))
                return null;
            return new Point(X1, Y1);
        }

        bool ValidateRegion (Point UL, Point LR, int Width, int Height)
        {
            if (UL.X > LR.X)
                return false;
            if (UL.Y > LR.X)
                return false;
            if (UL.X < 0)
                return false;
            if (UL.Y < 0)
                return false;
            if (LR.X > Width - 1)
                return false;
            if (LR.Y > Height - 1)
                return false;
            return true;
        }

        MirrorDirections GetMirrorDirection ()
        {
            ComboBoxItem CBI = MirrorDirectionCombo.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return MirrorDirections.Unknown;
            string raw = CBI.Content as string;
            if (string.IsNullOrEmpty(raw))
                return MirrorDirections.Unknown;
            switch (raw.ToLower())
            {
                case "horizontal":
                    return MirrorDirections.Horizontal;

                case "vertical":
                    return MirrorDirections.Vertical;

                case "both":
                    return MirrorDirections.Both;

                default:
                    return MirrorDirections.Unknown;
            }
        }

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;
            bool ByPixel = PixelUnits.IsChecked.Value;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            MirrorDirections Direction = GetMirrorDirection();
            if (Direction == MirrorDirections.Unknown)
            {
                ParentWindow.SetMessage("Error: Unknown mirror type.");
                return;
            }

            if (RegionalMirroringEnable.IsChecked.Value)
            {
                Nullable<Point> UL = GetPoint(RegionalULPoint);
                if (UL == null)
                {
                    ParentWindow.SetMessage("Error: Bad upper-left point.");
                    return;
                }
                Nullable<Point> LR = GetPoint(RegionalLRPoint);
                if (LR == null)
                {
                    ParentWindow.SetMessage("Error: Bad upper-left point.");
                    return;
                }
                if (!ValidateRegion(UL.Value, LR.Value, Original.PixelWidth, Original.PixelHeight))
                {
                    ParentWindow.SetMessage("Error: Points out of order.");
                    return;
                }
                switch (Direction)
                {
                    case MirrorDirections.Horizontal:
                        OK = CBI.ImageRegionHorizontalMirror(Original, Original.PixelWidth, Original.PixelHeight, DB, UL.Value, LR.Value);
                        break;

                    case MirrorDirections.Vertical:
                        OK = CBI.ImageRegionVerticalMirror(Original, Original.PixelWidth, Original.PixelHeight, DB, UL.Value, LR.Value);
                        break;

                    case MirrorDirections.Both:
                        break;

                    default:
                        ParentWindow.SetMessage("Error in regional mirroring");
                        return;
                }
            }

            if (InteriorMirroringEnable.IsChecked.Value)
            {
            }

            if (WholeImageMirroring.IsChecked.Value)
            {
                switch (Direction)
                {
                    case MirrorDirections.Horizontal:
                        OK = CBI.ImageMirrorHorizontal(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, ByPixel);
                        break;

                    case MirrorDirections.Vertical:
                        OK = CBI.ImageMirrorVertical(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, ByPixel);
                        break;

                    case MirrorDirections.Both:
                        OK = CBI.ImageMirrorBoth(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, ByPixel);
                        break;

                    default:
                        ParentWindow.SetMessage("Error in whole image mirroring");
                        return;
                }
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

        enum MirrorDirections
        {
            Unknown,
            Horizontal,
            Vertical,
            Both
        }
    }
}
