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
    /// Interaction logic for HSLPage.xaml
    /// </summary>
    public partial class HSLPage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HSLPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public HSLPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
        /// Return the relative adjust values.
        /// </summary>
        /// <returns>Relative adjust values for hue, saturation, and luminance.</returns>
        private Tuple<double, double, double> GetRelativeAdjustments()
        {
            string HAdjust = RelativeHue.Text;
            if (string.IsNullOrEmpty(HAdjust))
                HAdjust = "100";
            if (!double.TryParse(HAdjust, out double HValue))
                HValue = 100.0;
            HValue = HValue * 0.01;
            string SAdjust = RelativeSaturation.Text;
            if (string.IsNullOrEmpty(SAdjust))
                SAdjust = "100";
            if (!double.TryParse(HAdjust, out double SValue))
                SValue = 100.0;
            SValue = SValue * 0.01;
            string LAdjust = RelativeLuminance.Text;
            if (string.IsNullOrEmpty(LAdjust))
                LAdjust = "100";
            if (!double.TryParse(LAdjust, out double LValue))
                LValue = 100.0;
            LValue = SValue * 0.01;
            return new Tuple<double, double, double>(HValue, SValue, LValue);
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
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;
            ColorBlenderInterface.ReturnCode Result = ColorBlenderInterface.ReturnCode.NotSet;

            bool CountOK = CBI.CountColors(Original, Original.PixelWidth, Original.PixelHeight, out UInt32 UniqueColors,
                out Result);
            if (CountOK)
            {
                UniqueCountBlock.Text = UniqueColors.ToString();
            }
            CountOK = CBI.GetCommonColors(Original, Original.PixelWidth, Original.PixelHeight, 10, out UInt32 CommonUniqueColors,
                out List<ColorBlenderInterface.ColorCountPair> CommonColors, out Result);

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);
            if (HSLHueShiftCheck.IsChecked.Value)
            {
                string SShiftValue = HueShiftValue.Text;
                if (string.IsNullOrEmpty(SShiftValue))
                    return;
                if (!int.TryParse(SShiftValue, out int HueAdjustmentValue))
                    return;
                OK = CBI.AdjustImageHue(Original, Original.PixelWidth, Original.PixelHeight, DB, HueAdjustmentValue, out Result);
            }
            if (HSLSillyStuffCheck.IsChecked.Value)
            {
                if (SillySwapSL.IsChecked.Value)
                {
                    OK = CBI.SillySaturationLuminanceSwap(Original, Original.PixelWidth, Original.PixelHeight, DB, out Result);
                }
                if (RGBtoHSLtoRGB.IsChecked.Value)
                {
                    OK = CBI.ConvertRGBImageToHSLImage(Original, Original.PixelWidth, Original.PixelHeight,
                        out double[] HSLBuffer, out int DoubleCount, out Result);
                    if (OK)
                    {
                        OK = CBI.ConvertHSLImageToRGBImage(HSLBuffer, DoubleCount, DB, DB.PixelWidth,
                            DB.PixelHeight, out Result);
                    }
                    else
                        return;
                }
                if (RGBtoHSLtoRGB2.IsChecked.Value)
                {
                    OK = CBI.ConvertRGBtoHSLtoRGB(Original, Original.PixelWidth, Original.PixelHeight, DB,
                        out Result);
                }
            }
            if (HSLRelativeAdjustCheck.IsChecked.Value)
            {
                Tuple<double, double, double> Adjustments = GetRelativeAdjustments();
                OK = CBI.AdjustHSLValues(Original, Original.PixelWidth, Original.PixelHeight, DB,
                    Adjustments.Item1, Adjustments.Item2, Adjustments.Item3, out Result);
            }
            if (HSLRestrictionsGroup.IsChecked.Value)
            {
                bool RestrictHue = RestrictHueCheck.IsChecked.Value;
                bool RestrictSaturation = RestrictSaturationCheck.IsChecked.Value;
                bool RestrictLuminance = RestrictLuminanceCheck.IsChecked.Value;
                if (!RestrictHue && !RestrictSaturation && !RestrictLuminance)
                    return;
                int HueSegment = HueRangeEntry.IntValue(10);
                int SatSegment = SaturationRangeEntry.IntValue(20);
                int LumSegment = LuminanceRangeEntry.IntValue(20);
                OK = CBI.RestrictHSLToRanges(Original, Original.PixelWidth, Original.PixelHeight, DB,
                        RestrictHue, HueSegment, RestrictSaturation, SatSegment, RestrictLuminance, LumSegment,
                        out Result);
            }
            if (HSLReductionsGroup.IsChecked.Value)
            {
                bool ReduceSaturation = ReduceSaturationCheck.IsChecked.Value;
                bool ReduceLuminance = ReduceLuminanceCheck.IsChecked.Value;
                int HueReduction = HueReductionEntry.IntValue(360);
                HueReduction = HueReduction.Clamp(1, 360);
                HueReductionEntry.Text = HueReduction.ToString();
                double NewSaturation = SaturationReductionEntry.DoubleValue(0.5);
                NewSaturation = NewSaturation.Clamp(0.0, 1.0);
                SaturationRangeEntry.Text = NewSaturation.ToString();
                double NewLuminance = LuminanceReductionEntry.DoubleValue(1.0);
                NewLuminance = NewLuminance.Clamp(0.0, 1.0);
                LuminanceReductionEntry.Text = NewLuminance.ToString();
                OK = CBI.ReduceHSLColors(Original, Original.PixelWidth, Original.PixelHeight, DB,
                    HueReduction, ReduceSaturation, NewSaturation, ReduceLuminance, NewLuminance,
                    out Result);
            }
            if (HSLAbsoluteAdjustCheck.IsChecked.Value)
            {
                bool SetHue = AbsoluteHueCheck.IsChecked.Value;
                bool SetSaturation = AbsoluteSaturationCheck.IsChecked.Value;
                bool SetLuminance = AbsoluteLuminanceCheck.IsChecked.Value;
#if false
                if (!SetHue && !SetSaturation && !SetLuminance)
                    return;
#endif
                double NewHue = AbsoluteHueAdjust.DoubleValue();
                NewHue = NewHue.Clamp(0.0, 360.0);
                AbsoluteHueAdjust.Text = NewHue.ToString();
                double NewSaturation = AbsoluteSaturationAdjust.DoubleValue();
                NewSaturation = NewSaturation.Clamp(0.0, 1.0);
                AbsoluteSaturationAdjust.Text = NewSaturation.ToString();
                double NewLuminance = AbsoluteLuminanceAdjust.DoubleValue();
                NewLuminance = NewLuminance.Clamp(0.0, 1.0);
                AbsoluteLuminanceAdjust.Text = NewLuminance.ToString();
                OK = CBI.BulkSetHSL(Original, Original.PixelWidth, Original.PixelHeight, DB,
                    SetHue, NewHue, SetSaturation, NewSaturation, SetLuminance, NewLuminance,
                    out Result);
            }
            if (HSLGrayscaleCheck.IsChecked.Value)
            {
                if (HSLConvertToHue.IsChecked.Value)
                    OK = CBI.ConvertRGBImageToHueImage(Original, Original.PixelWidth, Original.PixelHeight, DB,
                        out Result);
                if (HSLConvertToSaturation.IsChecked.Value)
                    OK = CBI.ConvertRGBImageToSaturationImage(Original, Original.PixelWidth, Original.PixelHeight, DB,
                        out Result);
                if (HSLConvertToLuminance.IsChecked.Value)
                    OK = CBI.ConvertRGBImageToLuminanceImage(Original, Original.PixelWidth, Original.PixelHeight, DB,
                        out Result);
                if (HSLConvertToSL.IsChecked.Value)
                    OK = CBI.ConvertRGBImageToSLImage(Original, Original.PixelWidth, Original.PixelHeight, DB,
                        out Result);
            }
            if (HSLRestrictionsGroup2.IsChecked.Value)
            {
                double RangeSize = HueRangeCount2.DoubleValue(1.0);
                OK = CBI.RestrictHuesInImage(Original, Original.PixelWidth, Original.PixelHeight, DB,
                    RangeSize, out Result);
            }
            if (HSLRestrictToRange.IsChecked.Value)
            {
                double RangeLow = HueRangeLow.DoubleValue(0.0,0.0,360.0);
                double RangeHigh = HueRangeHigh.DoubleValue(360.0,0.0,360.0);
                if (RangeLow>RangeHigh)
                {
                    double s = RangeLow;
                    RangeLow = RangeHigh;
                    RangeHigh = s;
                    HueRangeLow.Text = RangeLow.ToString();
                    HueRangeHigh.Text = RangeHigh.ToString();
                }
                OK = CBI.RestrictHuesToRangeInImage(Original, Original.PixelWidth, Original.PixelHeight, DB,
                    RangeLow, RangeHigh, out Result);
            }

            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
            }
            else
            {
                string ErrorMessage = CBI.ErrorMessage((int)Result);
                StringBuilder sb = new StringBuilder();
                sb.Append(ErrorMessage);
                sb.Append(" (");
                sb.Append(Result.ToString());
                sb.Append(")");
                ParentWindow.SetMessage("Error", sb.ToString());
            }
        }

        private void HandleRelativeSliderChanged(object Sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider SL = Sender as Slider;
            if (SL == null)
                return;
            if (RelativeHue == null)
                return;
            if (RelativeSaturation == null)
                return;
            if (RelativeLuminance == null)
                return;
            if (HueShiftValue == null)
                return;
            string SliderName = SL.Name;
            if (string.IsNullOrEmpty(SliderName))
                return;
            int SliderValue = (int)SL.Value;
            switch (SliderName)
            {
                case "HueSlider":
                    RelativeHue.Text = SliderValue.ToString();
                    break;

                case "SaturationSlider":
                    RelativeSaturation.Text = SliderValue.ToString();
                    break;

                case "LuminanceSlider":
                    RelativeLuminance.Text = SliderValue.ToString();
                    break;

                case "HueShiftSlider":
                    HueShiftValue.Text = SliderValue.ToString();
                    break;
            }
        }

        private void HandleRelativeTextChanged(object Sender, TextChangedEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            string TextBoxName = TB.Name;
            if (string.IsNullOrEmpty(TextBoxName))
                return;
            string Value = TB.Text;
            if (string.IsNullOrEmpty(Value))
            {
                Value = "0";
                TB.Text = Value;
            }
            if (!int.TryParse(Value, out int ActualValue))
            {
                ActualValue = 0;
                TB.Text = "0";
            }
            switch (TextBoxName)
            {
                case "RelativeHue":
                    if (ActualValue < 0)
                        ActualValue = 0;
                    if (ActualValue > 100)
                        ActualValue = 100;
                    HueSlider.Value = ActualValue;
                    break;

                case "RelativeSaturation":
                    if (ActualValue < 0)
                        ActualValue = 0;
                    if (ActualValue > 100)
                        ActualValue = 100;
                    SaturationSlider.Value = ActualValue;
                    break;

                case "RelativeLuminance":
                    if (ActualValue < 0)
                        ActualValue = 0;
                    if (ActualValue > 100)
                        ActualValue = 100;
                    LuminanceSlider.Value = ActualValue;
                    break;

                case "HueShiftValue":
                    if (ActualValue < 0)
                        ActualValue = 0;
                    if (ActualValue > 360)
                        ActualValue = 360;
                    HueShiftSlider.Value = ActualValue;
                    break;

                default:
                    return;
            }
        }
    }
}
