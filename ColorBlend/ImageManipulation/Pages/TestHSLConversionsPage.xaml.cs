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
    /// Interaction logic for TestHSLConversionsPage.xaml
    /// </summary>
    public partial class TestHSLConversionsPage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TestHSLConversionsPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public TestHSLConversionsPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            Contract.Assert(ParentWindow != null);
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;
        }

        /// <summary>
        /// Clear the image.
        /// </summary>
        public void Clear()
        {
            Original = null;
        }

        /// <summary>
        /// Emit a pipeline stage.
        /// </summary>
        /// <returns>Pipeline stage for the rotation of images.</returns>
        public StageBase EmitPipelineStage()
        {
            return null;
        }

        /// <summary>
        /// Holds a reference to the color blender interface.
        /// </summary>
        private ColorBlenderInterface CBI;
        /// <summary>
        /// The original image.
        /// </summary>
        public WriteableBitmap Original = null;
        /// <summary>
        /// The image surface.
        /// </summary>
        private Image ImageSurface;
        /// <summary>
        /// The parent window.
        /// </summary>
        public ImageMan ParentWindow = null;

        /// <summary>
        /// Reset the local image.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ResetLocalImage(object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
            WriteableBitmap Scratch = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
        }

        /// <summary>
        /// Execute the filter.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ExecuteFilter(object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;

            if (RGB2HSL.IsChecked.Value)
            {
                byte R = GetByteValue(RedInputBox);
                byte G = GetByteValue(GreenInputBox);
                byte B = GetByteValue(BlueInputBox);
                CBI.NativeRGBtoHSL(R, G, B, out double H, out double S, out double L);
                UpdateHSLSample(H, S, L);
                HueInputBox.Text = H.ToString();
                SaturationInputBox.Text = S.ToString();
                LuminanceInputBox.Text = L.ToString();
            }
            else
            {
                double HueValue = GetDoubleValue(HueInputBox, 0.0, 360.0);
                double SatValue = GetDoubleValue(SaturationInputBox, 0.0, 1.0);
                double LumValue = GetDoubleValue(LuminanceInputBox, 0.0, 1.0);
                CBI.NativeHSLtoRGB(HueValue, SatValue, LumValue, out byte R, out byte G, out byte B);
                UpdateRGBSample(R, G, B);
                RedInputBox.Text = R.ToString();
                GreenInputBox.Text = G.ToString();
                BlueInputBox.Text = B.ToString();
            }
        }

        private Color MakeRGBFromHSL(double H, double S, double L)
        {
            double C = (1.0 - Math.Abs((2.0 * L) - 1.0)) * S;
            double X = C * (1.0 - Math.Abs(((H / 60.0) % 2.0) - 1.0));
            double m = L - (C / 2.0);
            double Rp = 0.0;
            double Gp = 0.0;
            double Bp = 0.0;
            if ((H >= 0.0) && (H < 60.0))
            {
                Rp = C;
                Gp = X;
                Bp = 0.0;
            }
            else
                if ((H >= 60.0) && (H < 120.0))
            {
                Rp = X;
                Gp = C;
                Bp = 0.0;
            }
            else
                if ((H >= 120.0) && (H < 180.0))
            {
                Rp = 0.0;
                Gp = C;
                Bp = X;
            }
            else
                if ((H >= 180.0) && (H < 240.0))
            {
                Rp = 0.0;
                Gp = X;
                Bp = C;
            }
            else
                if ((H >= 240.0) && (H < 300.0))
            {
                Rp = X;
                Gp = 0.0;
                Bp = C;
            }
            else
            {
                Rp = C;
                Gp = 0.0;
                Bp = X;
            }
            byte R = (byte)((Rp + m) * 255.0);
            byte G = (byte)((Gp + m) * 255.0);
            byte B = (byte)((Bp + m) * 255.0);
            Color Final = Color.FromRgb(R, G, B);
            return Final;
        }

        private void UpdateRGBSample(byte R, byte G, byte B)
        {
            if (RGBSample == null)
                return;
            SolidColorBrush SCB = new SolidColorBrush(Color.FromRgb(R, G, B));
            RGBSample.Background = SCB;
        }

        private void UpdateHSLSample(double H, double S, double L, Border Sample = null)
        {
            if (HSLSample == null)
                return;
            Color Converted = MakeRGBFromHSL(H, S, L);
            SolidColorBrush SCB = new SolidColorBrush(Converted);
            if (Sample == null)
                Sample = HSLSample;
            Sample.Background = SCB;
        }

        private byte GetByteValue(TextBox TB)
        {
            if (TB == null)
                return 0;
            string TVText = TB.Text;
            if (string.IsNullOrEmpty(TVText))
            {
                TB.Text = "0";
                TVText = "0";
            }
            if (!int.TryParse(TVText, out int Final))
            {
                Final = 0;
                TB.Text = "0";
            }
            if (Final < 0)
            {
                Final = 0;
                TB.Text = "0";
            }
            if (Final > 255)
            {
                Final = 255;
                TB.Text = "255";
            }
            return (byte)Final;
        }

        private double GetDoubleValue(TextBox TB, double Min, double Max)
        {
            if (TB == null)
                return 0.0;
            string TVText = TB.Text;
            if (string.IsNullOrEmpty(TVText))
            {
                TB.Text = "0.0";
                TVText = "0.0";
            }
            if (!double.TryParse(TVText, out double Final))
            {
                Final = 0.0;
                TB.Text = "0.0";
            }
            if (Final < Min)
            {
                Final = Min;
                TB.Text = Min.ToString();
            }
            if (Final > Max)
            {
                Final = Max;
                TB.Text = Max.ToString();
            }
            return Final;
        }

        private void HandleRelativeTextChanged(object Sender, TextChangedEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            string TextBoxName = TB.Name;
            if (string.IsNullOrEmpty(TextBoxName))
                return;
            string TextValue = TB.Text;
            if (string.IsNullOrEmpty(TextValue))
            {
                TextValue = "0";
                TB.Text = "0";
            }
            switch (TextBoxName)
            {
                case "RedInputBox":
                case "GreenInputBox":
                case "BlueInputBox":
                    {
                        byte R = GetByteValue(RedInputBox);
                        byte G = GetByteValue(GreenInputBox);
                        byte B = GetByteValue(BlueInputBox);
                        UpdateRGBSample(R, G, B);
                    }
                    break;

                case "HueInputBox":
                case "SaturationInputBox":
                case "LuminanceInputBox":
                    double HueValue = GetDoubleValue(HueInputBox, 0.0, 360.0);
                    double SatValue = GetDoubleValue(SaturationInputBox, 0.0, 1.0);
                    double LumValue = GetDoubleValue(LuminanceInputBox, 0.0, 1.0);
                    UpdateHSLSample(HueValue, SatValue, LumValue);
                    break;

                default:
                    return;
            }
        }

        private void HandleHSLHueShiftSliderValueChanged(object Sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider HSSlider = Sender as Slider;
            if (HSSlider == null)
                return;
            int SliderValue = (int)HSSlider.Value;
            double H = GetDoubleValue(HueShiftInputBox, 0.0, 360.0);
            double S = GetDoubleValue(SaturationShiftInputBox, 0.0, 1.0);
            double L = GetDoubleValue(LuminanceShiftInputBox, 0.0, 1.0);
            double NewHue = CBI.ClampedHue(H, (double)SliderValue);
            UpdateHSLSample(NewHue, S, L, HSLShiftSample);
            StringBuilder sb = new StringBuilder();
            sb.Append(NewHue.ToString("N3"));
            sb.Append(",");
            sb.Append(S.ToString("N2"));
            sb.Append(",");
            sb.Append(L.ToString("N2"));
            HueShiftResult.Text = sb.ToString();
            CurrentShiftValueBlock.Text = SliderValue.ToString();
        }
    }
}
