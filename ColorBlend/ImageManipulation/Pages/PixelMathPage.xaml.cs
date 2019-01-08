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
    /// Interaction logic for PixelMathPage.xaml
    /// </summary>
    public partial class PixelMathPage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PixelMathPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public PixelMathPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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
        /// Get the constant value.
        /// </summary>
        /// <param name="Value">On success, the constant value. On failure, undefined.</param>
        /// <returns>True on success, false on failure.</returns>
        private bool TryGetConstant(out int Value)
        {
            Value = 0;
            if (ConstantValue == null)
                return false;
            string ConstantString = ConstantValue.Text;
            if (string.IsNullOrEmpty(ConstantString))
                return false;
            ConstantString = ConstantString.ToUpper();
            if (ConstantString.Contains("0X"))
                ConstantString = ConstantString.Replace("0X", "");
            ConstantString = ConstantString.Trim();
            Value = Convert.ToInt32(ConstantString, 16);
            return true;
        }

        private PixelMathOperations GetOperation()
        {
            if (PixelMathAnd.IsChecked.Value)
                return PixelMathOperations.LogicalAndConstant;
            if (PixelMathOr.IsChecked.Value)
                return PixelMathOperations.LogicalOrConstant;
            if (PixelMathXor.IsChecked.Value)
                return PixelMathOperations.LogicalXorConstant;
            if (PixelMathAdd.IsChecked.Value)
                return PixelMathOperations.ArithmeticAddConstant;
            if (PixelMathSubtract.IsChecked.Value)
                return PixelMathOperations.ArithmeticSubtractConstant;
            if (PixelMathMultiply.IsChecked.Value)
                return PixelMathOperations.ArithmeticMultiplyConstant;
            if (PixelMathDivide.IsChecked.Value)
                return PixelMathOperations.ArithmeticDivideConstant;
            if (PixelMathMod.IsChecked.Value)
                return PixelMathOperations.ArithmeticModConstant;
            if (PixelMathRandom.IsChecked.Value)
                return PixelMathOperations.ArithmeticRandomize;
            return PixelMathOperations.Unknown;
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

            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);

            PixelMathOperations Operation = GetOperation();
            if (Operation == PixelMathOperations.Unknown)
                return;
            OK = TryGetConstant(out int ConstantValue);
            if (!OK)
                return;
            bool UseAlpha = UseAlphaChannel.IsChecked.Value;
            bool UseRed = UseRedChannel.IsChecked.Value;
            bool UseGreen = UseGreenChannel.IsChecked.Value;
            bool UseBlue = UseBlueChannel.IsChecked.Value;
            if (!UseAlpha && !UseRed && !UseGreen && !UseBlue)
                return;

            OK = CBI.PixelMathLogicalOperationWithConstant(Original, Original.PixelWidth, Original.PixelHeight, DB,
                (int)Operation, ConstantValue, UseAlpha, UseRed, UseGreen, UseBlue, out Result);

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
    }

    public enum PixelMathOperations : int
    {
        Unknown = -1,
        LogicalAndConstant = 0,
        LogicalOrConstant = 1,
        LogicalXorConstant = 2,
        ArithmeticAddConstant = 3,
        ArithmeticSubtractConstant = 4,
        ArithmeticMultiplyConstant = 5,
        ArithmeticDivideConstant = 6,
        ArithmeticModConstant = 7,
        ArithmeticRandomize = 8,
    }
}
