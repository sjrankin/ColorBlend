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
    /// Interaction logic for ChannelMaskPage.xaml
    /// </summary>
    public partial class ChannelMaskPage : Page,IFilterPage
    {
        public ChannelMaskPage () : base()
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

        public ChannelMaskPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        public ImageMan ParentWindow = null;
        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            ImageSurface.Source = (WriteableBitmap)Original;
        }

        private byte ToByte(string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return 0;
            Raw = Raw.Trim();
            if (Raw.Substring(0,2).ToLower()=="0x")
                return Convert.ToByte(Raw.Substring(2), 16);
            byte Temp = 0x0;
            if (!byte.TryParse(Raw, out Temp))
                return 0;
            return Temp;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            int AValue = ToByte(AlphaValueInput.Text);
            int RValue = ToByte(RedValueInput.Text);
            int GValue = ToByte(GreenValueInput.Text);
            int BValue = ToByte(BlueValueInput.Text);
            bool DoAlpha = UseAlphaValue.IsChecked.HasValue ? UseAlphaValue.IsChecked.Value : false;
            bool DoRed = UseRedValue.IsChecked.HasValue ? UseRedValue.IsChecked.Value : false;
            bool DoGreen = UseGreenValue.IsChecked.HasValue ? UseGreenValue.IsChecked.Value : false;
            bool DoBlue = UseBlueValue.IsChecked.HasValue ? UseBlueValue.IsChecked.Value : false;
            List<OptionalValue> Options = new List<OptionalValue>();
            Options.Add(new OptionalValue("UseAlpha", DoAlpha.ToString(), typeof(bool)));
            Options.Add(new OptionalValue("UseRed", DoRed.ToString(), typeof(bool)));
            Options.Add(new OptionalValue("UseGreen", DoGreen.ToString(), typeof(bool)));
            Options.Add(new OptionalValue("UseBlue", DoBlue.ToString(), typeof(bool)));
            Options.Add(new OptionalValue("AlphaValue", AValue.ToString(), typeof(byte)));
            Options.Add(new OptionalValue("RedValue", RValue.ToString(), typeof(byte)));
            Options.Add(new OptionalValue("GreenValue", GValue.ToString(), typeof(byte)));
            Options.Add(new OptionalValue("BlueValue", BValue.ToString(), typeof(byte)));
            //ParentWindow.CommandSink("MaskChannels", Options);
        }
    }
}
