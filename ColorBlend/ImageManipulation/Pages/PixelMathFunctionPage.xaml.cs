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
using System.Diagnostics;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for PixelMathFunctionPage.xaml
    /// </summary>
    public partial class PixelMathFunctionPage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PixelMathFunctionPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public PixelMathFunctionPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
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

        private PixelMathFunctions GetFunction()
        {
            if (PixelMathSin.IsChecked.Value)
                return PixelMathFunctions.PixelMathSin;
            if (PixelMathCos.IsChecked.Value)
                return PixelMathFunctions.PixelMathCos;
            if (PixelMathTan.IsChecked.Value)
                return PixelMathFunctions.PixelMathTan;
            if (PixelMathLog.IsChecked.Value)
                return PixelMathFunctions.PixelMathLog;
            if (PixelMathLog10.IsChecked.Value)
                return PixelMathFunctions.PixelMathLog10;
            return PixelMathFunctions.Unknown;
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

            PixelMathFunctions Function = GetFunction();
            if (Function == PixelMathFunctions.Unknown)
                return;
            bool UseAlpha = UseAlphaChannel.IsChecked.Value;
            bool UseRed = UseRedChannel.IsChecked.Value;
            bool UseGreen = UseGreenChannel.IsChecked.Value;
            bool UseBlue = UseBlueChannel.IsChecked.Value;
            if (!UseAlpha && !UseRed && !UseGreen && !UseBlue)
                return;
            bool DoNormalize = NormalizeResults.IsChecked.Value;
            bool DoNormalizeValues = NormalizeValues.IsChecked.Value;

            OK = CBI.PixelMathApplyFunction(Original, Original.PixelWidth, Original.PixelHeight, DB,
                (int)Function, DoNormalize, DoNormalizeValues, UseAlpha, UseRed, UseGreen, UseBlue, out Result);


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

    public enum PixelMathFunctions : int
    {
        Unknown = -1,
        PixelMathLog = 100,
        PixelMathLog10 = 101,
        PixelMathSin = 102,
        PixelMathCos = 103,
        PixelMathTan = 104,
        PixelMathLog2 = 105,
        PixelMathASin = 106,
        PixelMathSinh = 107,
        PixelMathASinh = 108,
        PixelMathACos = 109,
        PixelMathCosH = 110,
        PixelMathACosH = 111,
        PixelMathATan = 112,
        PixelMathTanH = 113,
        PixelMathATanH = 114,
    }
}
