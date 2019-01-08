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
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page, IFilterPage
    {
        public EditPage () : base()
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

        public EditPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
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

        public bool TryParse (string Raw, out UInt32 Final)
        {
            Final = 0x0;
            if (string.IsNullOrEmpty(Raw))
                return false;

            Raw = Raw.Trim(new char[] { ' ' });
            if (string.IsNullOrEmpty(Raw))
                return false;
            //Change "0x" (or "0X") to "#"
            if (Raw.Length >= 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    Raw = Raw.Substring(2);
                    Raw = "#" + Raw;
                }
            }
            Raw = Raw.Substring(1);

            string A = "ff";
            string R = "";
            string G = "";
            string B = "";
            if (Raw.Length == 6)
            {
                R = Raw.Substring(0, 2);
                G = Raw.Substring(2, 2);
                B = Raw.Substring(4, 2);
            }
            else
                if (Raw.Length == 8)
            {
                A = Raw.Substring(0, 2);
                R = Raw.Substring(2, 2);
                G = Raw.Substring(4, 2);
                B = Raw.Substring(6, 2);
            }
            else
                return false;

            byte av = Convert.ToByte(A, 16);
            byte rv = Convert.ToByte(R, 16);
            byte gv = Convert.ToByte(G, 16);
            byte bv = Convert.ToByte(B, 16);

            Final = (UInt32)((av << 24) | (rv << 16) | (gv << 8) | bv);

            return true;
        }

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);

            if (SelfPasteTest.IsChecked.Value)
            {
                OK = CBI.EditPasteRegion4(DB, DB.PixelWidth, DB.PixelHeight, Original, Original.PixelWidth, Original.PixelHeight,
                    new Point(0, 0), new Point(DB.PixelWidth - 1, DB.PixelHeight - 1));
            }

            if (DWordClear.IsChecked.Value)
            {
                UInt32 ClearValue = 0;
                ClearValue = DWordClearInput.RawColorValue;
                CBI.CopyBufferTo(Original, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, DB);
                OK = CBI.DWordClearBuffer(DB, DB.PixelWidth, DB.PixelHeight, ClearValue);
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
    }
}
