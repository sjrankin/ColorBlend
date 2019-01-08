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
    /// Interaction logic for FrequencyMaskPage.xaml
    /// </summary>
    public partial class FrequencyMaskPage : Page, IFilterPage
    {
        public FrequencyMaskPage () : base()
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

        public FrequencyMaskPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private int GetFrequencyValue (TextBox TB)
        {
            if (TB == null)
                throw new ArgumentNullException("TB");
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
            {
                Raw = "1";
                TB.Text = Raw;
            }
            int Frequency = 0;
            bool ParsedOK = int.TryParse(Raw, out Frequency);
            if (!ParsedOK)
            {
                TB.Text = "1";
                return 1;
            }
            if (Frequency < 1)
            {
                TB.Text = "1";
                return 1;
            }
            return Frequency;
        }

        ColorBlenderInterface.FrequencyActionBlock MakeActionBlock ()
        {
            ColorBlenderInterface.FrequencyActionBlock Block = new ColorBlenderInterface.FrequencyActionBlock
            {
                HorizontalFrequency = GetFrequencyValue(HorizontalFrequencyInput),
                VerticalFrequency = GetFrequencyValue(VerticalFrequencyInput)
            };
            if (TransparentCheck.IsChecked.Value)
            {
                Block.Action = 1;
                if (TransparentLuminosity.IsChecked.Value)
                    Block.Action = 5;
                string RawAlpha = AlphaFreqInput.Text;
                if (string.IsNullOrEmpty(RawAlpha))
                {
                    RawAlpha = "1.0";
                    AlphaFreqInput.Text = RawAlpha;
                }
                double ActualAlpha = 0.0;
                bool AlphaOK = double.TryParse(RawAlpha, out ActualAlpha);
                if (!AlphaOK)
                {
                    ActualAlpha = 1.0;
                    AlphaFreqInput.Text = "1.0";
                }
                Block.NewAlpha = ActualAlpha;
            }
            else
                if (InversionCheck.IsChecked.Value)
            {
                Block.Action = 2;
                Block.IncludeAlpha = IncludeAlphaInversion.IsChecked.Value;
            }
            else
                if (LuminanceCheck.IsChecked.Value)
            {
                Block.Action = 3;
                string RawLum = LumFreqInput.Text;
                if (string.IsNullOrEmpty(RawLum))
                {
                    RawLum = "1.0";
                    LumFreqInput.Text = RawLum;
                }
                double ActualLum = 0.0;
                bool LumOK = double.TryParse(RawLum, out ActualLum);
                if (!LumOK)
                {
                    ActualLum = 1.0;
                    LumFreqInput.Text = "1.0";
                }
                Block.NewLuminance = ActualLum;
            }
            else
                if (ColorCheck.IsChecked.Value)
            {
                Block.Action = 4;
                Block.NewColor = FreqColorInput.CurrentColor.ToARGB();
                if (UseColorAlpha.IsChecked.Value)
                    Block.ColorAlphaAction = 0;
                if (SourceAlpha.IsChecked.Value)
                    Block.ColorAlphaAction = 1;
                if (ProportionalAlpha.IsChecked.Value)
                    Block.ColorAlphaAction = 2;
            }
            else
                throw new InvalidOperationException();
            return Block;
        }

        private void ExecuteFilter (object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);

            bool OK = false;
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
               PixelFormats.Bgra32, null);
            ColorBlenderInterface.FrequencyActionBlock ActionBlock = MakeActionBlock();
            OK = CBI.FrequencyAction(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride, ActionBlock);

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
