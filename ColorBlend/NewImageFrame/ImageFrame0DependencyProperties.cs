using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace ColorBlend
{
    public partial class ImageFrame0
    {
        /// <summary>
        /// Identifies the FitImageToFrame dependency property.
        /// </summary>
        public static readonly DependencyProperty FitImageToFrameProperty =
            DependencyProperty.Register("FitImageToFrame", typeof(bool), typeof(ImageFrame0),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleFitImageToFrameChange)));

        /// <summary>
        /// Handle changes to FitImageToFrame.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleFitImageToFrameChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.FitImageToFrame = (bool)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Fit the image into the frame.
        /// </summary>
        [Category("ImageFrame")]
        [Description("Determines if image is sized to fit in the frame.")]
        public bool FitImageToFrame
        {
            get
            {
                return (bool)GetValue(FitImageToFrameProperty);
            }
            set
            {
                SetValue(FitImageToFrameProperty, value);
            }
        }

        /// <summary>
        /// Identifies the GridSpacing dependency property.
        /// </summary>
        public static readonly DependencyProperty GridSpacingProperty =
            DependencyProperty.Register("GridSpacing", typeof(double), typeof(ImageFrame0),
                new UIPropertyMetadata(0.0, new PropertyChangedCallback(HandleGridSpacingChange)));

        /// <summary>
        /// Handle changes to GridSpacing.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleGridSpacingChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.GridSpacing = (double)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the square grid spacing size.
        /// </summary>
        [Category("ImageFrame")]
        [Description("Grid spacing square size.")]
        public double GridSpacing
        {
            get
            {
                return (double)GetValue(GridSpacingProperty);
            }
            set
            {
                SetValue(GridSpacingProperty, value);
            }
        }

        /// <summary>
        /// Identifies the GridThickness dependency property.
        /// </summary>
        public static readonly DependencyProperty GridThicknessProperty =
            DependencyProperty.Register("GridThickness", typeof(Thickness), typeof(ImageFrame0),
                new UIPropertyMetadata(new Thickness(0.0), new PropertyChangedCallback(HandleGridThicknessChange)));

        /// <summary>
        /// Handle changes to GridThickness.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleGridThicknessChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.GridThickness = (Thickness)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the thickness of grid lines.
        /// </summary>
        [Category("ImageFrame")]
        [Description("Thickness of the grid.")]
        public Thickness GridThickness
        {
            get
            {
                return (Thickness)GetValue(GridThicknessProperty);
            }
            set
            {
                SetValue(GridThicknessProperty, value);
            }
        }

        /// <summary>
        /// Identifies the GridBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty GridBrushProperty =
            DependencyProperty.Register("GridBrush", typeof(Brush), typeof(ImageFrame0),
                new UIPropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(HandleGridBrushChange)));

        /// <summary>
        /// Handle changes to GridBrush.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleGridBrushChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.GridBrush = (Brush)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the brush used to draw the grid lines.
        /// </summary>
        [Category("ImageFrame")]
        [Description("The brush used to draw grid lines.")]
        public Brush GridBrush
        {
            get
            {
                return (Brush)GetValue(GridBrushProperty);
            }
            set
            {
                SetValue(GridBrushProperty, value);
            }
        }

        /// <summary>
        /// Identifies the BackgroundBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(ImageFrame0),
                new UIPropertyMetadata(Brushes.White, new PropertyChangedCallback(HandleBackgroundBrushChange)));

        /// <summary>
        /// Handle changes to BackgroundBrush.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleBackgroundBrushChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.BackgroundBrush = (Brush)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the brush used to draw the background. Ignored if <seealso cref="ShowCheckerboardBackground"/> is true.
        /// </summary>
        [Category("ImageFrame")]
        [Description("Sets the background brush. Ignored if checkerboards are shown.")]
        public Brush BackgroundBrush
        {
            get
            {
                return (Brush)GetValue(BackgroundBrushProperty);
            }
            set
            {
                SetValue(BackgroundBrushProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ShowCheckerboardBackground dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowCheckerboardBackgroundProperty =
            DependencyProperty.Register("ShowCheckerboardBackground", typeof(bool), typeof(ImageFrame0),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowCheckerboardBackgroundChange)));

        /// <summary>
        /// Handle changes to ShowCheckerboardBackground.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleShowCheckerboardBackgroundChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.ShowCheckerboardBackground = (bool)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Show or hide the checkerboard background.
        /// </summary>
        [Category("ImageFrame")]
        [Description("Determines visibility of the checkerboard background.")]
        public bool ShowCheckerboardBackground
        {
            get
            {
                return (bool)GetValue(ShowCheckerboardBackgroundProperty);
            }
            set
            {
                SetValue(ShowCheckerboardBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Identifies the CheckerboardColorSquareSize dependency property.
        /// </summary>
        public static readonly DependencyProperty CheckerboardColorSquareSizeProperty =
            DependencyProperty.Register("CheckerboardColorSquareSize", typeof(double), typeof(ImageFrame0),
                new UIPropertyMetadata(24.0, new PropertyChangedCallback(HandleCheckerboardColorSquareSizeChange)));

        /// <summary>
        /// Handle changes to CheckerboardColorSquareSize.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleCheckerboardColorSquareSizeChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.CheckerboardColorSquareSize = (double)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the size of the checkerboard color square.
        /// </summary>
        [Category("ImageFrame")]
        [Description("Size of the checkerboard color square.")]
        public double CheckerboardColorSquareSize
        {
            get
            {
                return (double)GetValue(CheckerboardColorSquareSizeProperty);
            }
            set
            {
                SetValue(CheckerboardColorSquareSizeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the CheckerboardFirstColor dependency property.
        /// </summary>
        public static readonly DependencyProperty CheckerboardFirstColorProperty =
            DependencyProperty.Register("CheckerboardFirstColor", typeof(SolidColorBrush), typeof(ImageFrame0),
                new UIPropertyMetadata(Brushes.WhiteSmoke, new PropertyChangedCallback(HandleCheckerboardFirstColorChange)));

        /// <summary>
        /// Handle changes to CheckerboardFirstColor.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleCheckerboardFirstColorChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.CheckerboardFirstColor = (SolidColorBrush)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the brush used to draw the first color.
        /// </summary>
        [Category("ImageFrame")]
        [Description("SolidColorBrush for the first color.")]
        public SolidColorBrush CheckerboardFirstColor
        {
            get
            {
                return (SolidColorBrush)GetValue(CheckerboardFirstColorProperty);
            }
            set
            {
                SetValue(CheckerboardFirstColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the CheckerboardSecondColor dependency property.
        /// </summary>
        public static readonly DependencyProperty CheckerboardSecondColorProperty =
            DependencyProperty.Register("CheckerboardSecondColor", typeof(SolidColorBrush), typeof(ImageFrame0),
                new UIPropertyMetadata(Brushes.LightGray, new PropertyChangedCallback(HandleCheckerboardSecondColorChange)));

        /// <summary>
        /// Handle changes to CheckerboardSecondColor.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleCheckerboardSecondColorChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.CheckerboardSecondColor = (SolidColorBrush)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the brush used to draw the second color.
        /// </summary>
        [Category("ImageFrame")]
        [Description("SolidColorBrush for the second color.")]
        public SolidColorBrush CheckerboardSecondColor
        {
            get
            {
                return (SolidColorBrush)GetValue(CheckerboardSecondColorProperty);
            }
            set
            {
                SetValue(CheckerboardSecondColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the DisplayImage dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayImageProperty =
            DependencyProperty.Register("DisplayImage", typeof(UImage), typeof(ImageFrame0),
                new UIPropertyMetadata(null, new PropertyChangedCallback(HandleDisplayImageChange)));

        /// <summary>
        /// Handle changes to DisplayImage.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        internal static void HandleDisplayImageChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ImageFrame0 IF = Sender as ImageFrame0;
            if (IF == null)
                return;
            IF.DisplayImage = (UImage)e.NewValue;
            IF.DrawUI();
        }

        /// <summary>
        /// Get or set the image to display.
        /// </summary>
        [Category("ImageFrame")]
        [Description("The image to display.")]
        public UImage DisplayImage
        {
            get
            {
                return (UImage)GetValue(DisplayImageProperty);
            }
            set
            {
                SetValue(DisplayImageProperty, value);
            }
        }
    }
}
