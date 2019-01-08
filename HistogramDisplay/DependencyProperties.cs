using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;

namespace HistogramDisplay
{
    public partial class HistogramViewer
    {
        public static readonly DependencyProperty ShowRedProperty =
            DependencyProperty.Register("ShowRed", typeof(bool), typeof(HistogramViewer),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowRedChange)));

        public static void HandleShowRedChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.ShowRed = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Determines if the red channel is displayed.")]
        public bool ShowRed
        {
            get
            {
                return (bool)GetValue(ShowRedProperty);
            }
            set
            {
                SetValue(ShowRedProperty, value);
            }
        }

        public static readonly DependencyProperty ShowGreenProperty =
           DependencyProperty.Register("ShowGreen", typeof(bool), typeof(HistogramViewer),
            new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowGreenChange)));

        public static void HandleShowGreenChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.ShowGreen = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Determines if the green channel is displayed.")]
        public bool ShowGreen
        {
            get
            {
                return (bool)GetValue(ShowGreenProperty);
            }
            set
            {
                SetValue(ShowGreenProperty, value);
            }
        }

        public static readonly DependencyProperty ShowBlueProperty =
           DependencyProperty.Register("ShowBlue", typeof(bool), typeof(HistogramViewer),
            new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowBlueChange)));

        public static void HandleShowBlueChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.ShowBlue = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Determines if the blue channel is displayed.")]
        public bool ShowBlue
        {
            get
            {
                return (bool)GetValue(ShowBlueProperty);
            }
            set
            {
                SetValue(ShowBlueProperty, value);
            }
        }

        public static readonly DependencyProperty ShowGrayProperty =
            DependencyProperty.Register("ShowGray", typeof(bool), typeof(HistogramViewer),
            new UIPropertyMetadata(false, new PropertyChangedCallback(HandleShowGrayChange)));

        public static void HandleShowGrayChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.ShowGray = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Determines if the gray channel is displayed. If true, only gray is shown.")]
        public bool ShowGray
        {
            get
            {
                return (bool)GetValue(ShowGrayProperty);
            }
            set
            {
                SetValue(ShowGrayProperty, value);
            }
        }

        public static readonly DependencyProperty HistogramBackgroundProperty =
            DependencyProperty.Register("HistogramBackground", typeof(Brush), typeof(HistogramViewer),
                new UIPropertyMetadata(Brushes.WhiteSmoke, new PropertyChangedCallback(HandleHistogramBackgroundChange)));

        public static void HandleHistogramBackgroundChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.HistogramBackground = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the background of the histogram.")]
        public Brush HistogramBackground
        {
            get
            {
                return (Brush)GetValue(HistogramBackgroundProperty);
            }
            set
            {
                SetValue(HistogramBackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty OutlineBrushProperty =
            DependencyProperty.Register("OutlineBrush", typeof(Brush), typeof(HistogramViewer),
                new UIPropertyMetadata(Brushes.Black, new PropertyChangedCallback(HandleOutlineBrushChange)));

        public static void HandleOutlineBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.OutlineBrush = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the outline of the histogram.")]
        public Brush OutlineBrush
        {
            get
            {
                return (Brush)GetValue(OutlineBrushProperty);
            }
            set
            {
                SetValue(OutlineBrushProperty, value);
            }
        }

        public static readonly DependencyProperty OutlineThicknessProperty =
            DependencyProperty.Register("OutlineThickness", typeof(double), typeof(HistogramViewer),
                new UIPropertyMetadata(1.0, new PropertyChangedCallback(HandleOutlineThicknessChange)));

        public static void HandleOutlineThicknessChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.OutlineThickness = (double)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The thickness of the histogram outline.")]
        public double OutlineThickness
        {
            get
            {
                return (double)GetValue(OutlineThicknessProperty);
            }
            set
            {
                SetValue(OutlineThicknessProperty, value);
            }
        }

        public static readonly DependencyProperty IsOutlinedProperty =
            DependencyProperty.Register("IsOutlined", typeof(bool), typeof(HistogramViewer),
                new UIPropertyMetadata(false, new PropertyChangedCallback(HandleIsOutlinedChanged)));

        public static void HandleIsOutlinedChanged (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.IsOutlined = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Determines if the histogram outline is drawn.")]
        public bool IsOutlined
        {
            get
            {
                return (bool)GetValue(IsOutlinedProperty);
            }
            set
            {
                SetValue(IsOutlinedProperty, value);
            }
        }

        public static readonly DependencyProperty HistogramProperty =
            DependencyProperty.Register("Histogram", typeof(ObservableCollection<HistogramTriplet>), typeof(HistogramViewer),
                new UIPropertyMetadata(null, new PropertyChangedCallback(HandleHistogramChange)));

        public static void HandleHistogramChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.Histogram = (ObservableCollection<HistogramTriplet>)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Histogram data in triplets.")]
        public ObservableCollection<HistogramTriplet> Histogram
        {
            get
            {
                return (ObservableCollection<HistogramTriplet>)GetValue(HistogramProperty);
            }
            private set
            {
                SetValue(HistogramProperty, value);
            }
        }

        public static readonly new DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BroderBrush", typeof(Brush), typeof(HistogramViewer),
                new UIPropertyMetadata(Brushes.Black, new PropertyChangedCallback(HandleBorderBrushChange)));

        public static void HandleBorderBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.BorderBrush = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the control's border.")]
        public new Brush BorderBrush
        {
            get
            {
                return (Brush)GetValue(BorderBrushProperty);
            }
            set
            {
                SetValue(BorderBrushProperty, value);
            }
        }

        public static readonly new DependencyProperty BorderThicknessProperty =
           DependencyProperty.Register("BroderThickness", typeof(Thickness), typeof(HistogramViewer),
            new UIPropertyMetadata(new Thickness(1), new PropertyChangedCallback(HandleBorderThicknessChange)));

        public static void HandleBorderThicknessChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.BorderThickness = (Thickness)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The thickness of the control's border.")]
        public new Thickness BorderThickness
        {
            get
            {
                return (Thickness)GetValue(BorderThicknessProperty);
            }
            set
            {
                SetValue(BorderThicknessProperty, value);
            }
        }

        public static readonly DependencyProperty BorderCornerRadiusProperty =
          DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(HistogramViewer),
           new UIPropertyMetadata(new CornerRadius(0), new PropertyChangedCallback(HandleBorderCornerRadiusChange)));

        public static void HandleBorderCornerRadiusChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.CornerRadius = (CornerRadius)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The corner radius of the border.")]
        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(BorderCornerRadiusProperty);
            }
            set
            {
                SetValue(BorderCornerRadiusProperty, value);
            }
        }

        public static readonly DependencyProperty ShowCheckerboardBackgroundProperty =
            DependencyProperty.Register("ShowCheckerboardBackground", typeof(bool), typeof(HistogramViewer),
                new UIPropertyMetadata(false, new PropertyChangedCallback(HandleShowCheckerboardBackgroundChange)));

        public static void HandleShowCheckerboardBackgroundChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.ShowCheckerboardBackground = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("If true, the background is a checkerboard pattern.")]
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

        public static readonly DependencyProperty CheckerboardBackgroundSizeProperty =
          DependencyProperty.Register("CheckerboardBackgroundSize", typeof(double), typeof(HistogramViewer),
           new UIPropertyMetadata(16.0, new PropertyChangedCallback(HandleCheckerboardBackgroundSizeChange)));

        public static void HandleCheckerboardBackgroundSizeChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.CheckerboardBackgroundSize = (double)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The height and width of each square in a checkerboard pattern.")]
        public double CheckerboardBackgroundSize
        {
            get
            {
                return (double)GetValue(CheckerboardBackgroundSizeProperty);
            }
            set
            {
                SetValue(CheckerboardBackgroundSizeProperty, value);
            }
        }

        public static readonly DependencyProperty CheckerboardBackgroundBrush0Property =
          DependencyProperty.Register("CheckerboardBackgroundBrush0", typeof(Brush), typeof(HistogramViewer),
            new UIPropertyMetadata(Brushes.LightGray, new PropertyChangedCallback(HandleCheckerboardBackgroundBrush0Change)));

        public static void HandleCheckerboardBackgroundBrush0Change (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.CheckerboardBackgroundBrush0 = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The first checkerboard pattern brush.")]
        public Brush CheckerboardBackgroundBrush0
        {
            get
            {
                return (Brush)GetValue(CheckerboardBackgroundBrush0Property);
            }
            set
            {
                SetValue(CheckerboardBackgroundBrush0Property, value);
            }
        }

        public static readonly DependencyProperty CheckerboardBackgroundBrush1Property =
            DependencyProperty.Register("CheckerboardBackgroundBrush1", typeof(Brush), typeof(HistogramViewer),
               new UIPropertyMetadata(Brushes.DarkGray, new PropertyChangedCallback(HandleCheckerboardBackgroundBrush1Change)));

        public static void HandleCheckerboardBackgroundBrush1Change (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.CheckerboardBackgroundBrush1 = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The second checkerboard pattern brush.")]
        public Brush CheckerboardBackgroundBrush1
        {
            get
            {
                return (Brush)GetValue(CheckerboardBackgroundBrush1Property);
            }
            set
            {
                SetValue(CheckerboardBackgroundBrush1Property, value);
            }
        }

        public static readonly DependencyProperty AbsolutePercentProperty =
            DependencyProperty.Register("AbsolutePercent", typeof(bool), typeof(HistogramViewer),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleAbsolutePercentChange)));

        public static void HandleAbsolutePercentChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.AbsolutePercent = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Determines if absolute or relative percentages are used for drawing histograms.")]
        public bool AbsolutePercent
        {
            get
            {
                return (bool)GetValue(AbsolutePercentProperty);
            }
            set
            {
                SetValue(AbsolutePercentProperty, value);
            }
        }

        public static readonly DependencyProperty ChannelDisplayAlphaLevelOverrideProperty =
            DependencyProperty.Register("ChannelDisplayAlphaLevelOverride", typeof(Nullable<double>), typeof(HistogramViewer),
                new UIPropertyMetadata(0.5, new PropertyChangedCallback(HandleChannelDisplayAlphaLevelOverrideChange)));

        public static void HandleChannelDisplayAlphaLevelOverrideChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            Nullable<double> dtemp = (Nullable<double>)e.NewValue;
            if (dtemp.HasValue)
            {
                dtemp = Math.Min(dtemp.Value, 0.0);
                dtemp = Math.Max(dtemp.Value, 1.0);
            }
            HD.ChannelDisplayAlphaLevelOverride = dtemp;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("If non-null, the alpha level override for channel displays. Clamped to 0.0 to 1.0.")]
        public Nullable<double> ChannelDisplayAlphaLevelOverride
        {
            get
            {
                return (Nullable<double>)GetValue(ChannelDisplayAlphaLevelOverrideProperty);
            }
            set
            {
                SetValue(ChannelDisplayAlphaLevelOverrideProperty, value);
            }
        }

        public static readonly DependencyProperty RedBrushProperty =
           DependencyProperty.Register("RedBrush", typeof(Brush), typeof(HistogramViewer),
           new UIPropertyMetadata(Brushes.Red, new PropertyChangedCallback(HandleRedBrushChange)));

        public static void HandleRedBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.RedBrush = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the red channel.")]
        public Brush RedBrush
        {
            get
            {
                return (Brush)GetValue(RedBrushProperty);
            }
            set
            {
                SetValue(RedBrushProperty, value);
            }
        }

        public static readonly DependencyProperty GreenBrushProperty =
            DependencyProperty.Register("GreenBrush", typeof(Brush), typeof(HistogramViewer),
            new UIPropertyMetadata(Brushes.Green, new PropertyChangedCallback(HandleGreenBrushChange)));

        public static void HandleGreenBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.GreenBrush = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the green channel.")]
        public Brush GreenBrush
        {
            get
            {
                return (Brush)GetValue(GreenBrushProperty);
            }
            set
            {
                SetValue(GreenBrushProperty, value);
            }
        }

        public static readonly DependencyProperty BlueBrushProperty =
            DependencyProperty.Register("BlueBrush", typeof(Brush), typeof(HistogramViewer),
                new UIPropertyMetadata(Brushes.Blue, new PropertyChangedCallback(HandleBlueBrushChange)));

        public static void HandleBlueBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.BlueBrush = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the blue channel.")]
        public Brush BlueBrush
        {
            get
            {
                return (Brush)GetValue(BlueBrushProperty);
            }
            set
            {
                SetValue(BlueBrushProperty, value);
            }
        }

        public static readonly DependencyProperty GrayBrushProperty =
            DependencyProperty.Register("GrayBrush", typeof(Brush), typeof(HistogramViewer),
            new UIPropertyMetadata(Brushes.DarkGray, new PropertyChangedCallback(HandleGrayBrushChange)));

        public static void HandleGrayBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.GrayBrush = (Brush)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("The brush used to paint the gray channel.")]
        public Brush GrayBrush
        {
            get
            {
                return (Brush)GetValue(GrayBrushProperty);
            }
            set
            {
                SetValue(GrayBrushProperty, value);
            }
        }

        public static readonly DependencyProperty DebugProperty =
            DependencyProperty.Register("Debug", typeof(bool), typeof(HistogramViewer),
                new UIPropertyMetadata(false, new PropertyChangedCallback(HandleDebugChange)));

        public static void HandleDebugChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            HistogramViewer HD = Sender as HistogramViewer;
            if (HD == null)
                return;
            HD.Debug = (bool)e.NewValue;
            HD.DrawUI();
        }

        [Category("HistogramViewer")]
        [Description("Sets the debug state of the control.")]
        public bool Debug
        {
            get
            {
                return (bool)GetValue(DebugProperty);
            }
            set
            {
                SetValue(DebugProperty, value);
            }
        }
    }
}
