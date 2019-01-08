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
using Microsoft.Win32;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for RGBColorMergePage.xaml
    /// </summary>
    public partial class RGBColorMergePage : Page, IFilterPage
    {
        public RGBColorMergePage () : base()
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

        public RGBColorMergePage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            string RedFile = RedChannelImageInput.Text;
            if (string.IsNullOrEmpty(RedFile))
                return;
            string GreenFile = GreenChannelImageInput.Text;
            if (string.IsNullOrEmpty(GreenFile))
                return;
            string BlueFile = BlueChannelImageInput.Text;
            if (string.IsNullOrEmpty(BlueFile))
                return;
            List<OptionalValue> Options = new List<OptionalValue>();
            Options.Add(new OptionalValue("RedChannel", RedFile, typeof(string)));
            Options.Add(new OptionalValue("GreenChannel", GreenFile, typeof(string)));
            Options.Add(new OptionalValue("BlueChannel", BlueFile, typeof(string)));
            //ParentWindow.CommandSink("MergeImages", Options);
        }

        private void BrowseForImage (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            Button B = Sender as Button;
            if (B == null)
                return;
            string Channel = B.Tag as string;
            if (string.IsNullOrEmpty(Channel))
                return;

            OpenFileDialog OFD = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".jpg",
                DereferenceLinks = true,
                Filter = "JPEG Images|*.jpg;*.jpeg|PNG Images|*.png|TIF Images|*.tif;*.tiff|GIF Images|*.gif|Bitmap Images|*.bmp|All Files|*.*",
                FilterIndex = 0,
                InitialDirectory = ParentWindow.LastReferencedDirectory,
                Multiselect = false,
                Title = "Open image for " + Channel + " channel"
            };
            Nullable<bool> OK = OFD.ShowDialog();
            if (OK.HasValue)
            {
                if (OK.Value)
                {
                    switch (Channel)
                    {
                        case "Red":
                            RedChannelImageInput.Text = OFD.FileName;
                            break;

                        case "Green":
                            GreenChannelImageInput.Text = OFD.FileName;
                            break;

                        case "Blue":
                            BlueChannelImageInput.Text = OFD.FileName;
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
