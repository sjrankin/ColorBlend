using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Iro3.Selectors
{
    public partial class Rotational
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Rotational),
                new UIPropertyMetadata(0.0, new PropertyChangedCallback(HandleValueChanged)));

        private static void HandleValueChanged (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.Value = (double)e.NewValue;
            Instance.DrawUI();
        }

        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, Value);
            }
        }

        public static readonly DependencyProperty RotatorBrushProperty =
            DependencyProperty.Register("RotatorBrush", typeof(Brush), typeof(Rotational),
                new UIPropertyMetadata(Brushes.WhiteSmoke, new PropertyChangedCallback(HandleRotatorBrushChange)));

        private static void HandleRotatorBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.RotatorBrush = (Brush)e.NewValue;
            Instance.DrawUI();
        }

        public Brush RotatorBrush
        {
            get
            {
                return (Brush)GetValue(RotatorBrushProperty);
            }
            set
            {
                SetValue(RotatorBrushProperty, value);
            }
        }

        public static readonly DependencyProperty RotatorBorderBrushProperty =
            DependencyProperty.Register("RotatorBorderBrush", typeof(Brush), typeof(Rotational),
                new UIPropertyMetadata(Brushes.WhiteSmoke, new PropertyChangedCallback(HandleRotatorBorderBrushChange)));

        private static void HandleRotatorBorderBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.RotatorBorderBrush = (Brush)e.NewValue;
            Instance.DrawUI();
        }

        public Brush RotatorBorderBrush
        {
            get
            {
                return (Brush)GetValue(RotatorBorderBrushProperty);
            }
            set
            {
                SetValue(RotatorBorderBrushProperty, value);
            }
        }

        public static readonly DependencyProperty RotatorBorderBrushWidthProperty =
            DependencyProperty.Register("RotatorBorderBrushWidth", typeof(double), typeof(Rotational),
                new UIPropertyMetadata(2.5, new PropertyChangedCallback(HandleRotatorBorderBrushWidthChange)));

        private static void HandleRotatorBorderBrushWidthChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.RotatorBorderBrushWidth = (double)e.NewValue;
            Instance.DrawUI();
        }

        public double RotatorBorderBrushWidth
        {
            get
            {
                return (double)GetValue(RotatorBorderBrushWidthProperty);
            }
            set
            {
                SetValue(RotatorBorderBrushWidthProperty, value);
            }
        }

        public static readonly DependencyProperty IndicatorRadiusProperty =
            DependencyProperty.Register("IndicatorRadius", typeof(double), typeof(Rotational),
                new UIPropertyMetadata(5.0, new PropertyChangedCallback(HandleIndicatorRadiusChange)));

        private static void HandleIndicatorRadiusChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.IndicatorRadius = (double)e.NewValue;
            Instance.DrawUI();
        }

        public double IndicatorRadius
        {
            get
            {
                return (double)GetValue(IndicatorRadiusProperty);
            }
            set
            {
                SetValue(IndicatorRadiusProperty, value);
            }
        }

        public static readonly DependencyProperty IndicatorDistanceProperty =
             DependencyProperty.Register("IndicatorDistance", typeof(double), typeof(Rotational),
               new UIPropertyMetadata(5.0, new PropertyChangedCallback(HandleIndicatorDistanceChange)));

        private static void HandleIndicatorDistanceChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.IndicatorDistance = (double)e.NewValue;
            Instance.DrawUI();
        }

        public double IndicatorDistance
        {
            get
            {
                return (double)GetValue(IndicatorDistanceProperty);
            }
            set
            {
                SetValue(IndicatorDistanceProperty, value);
            }
        }

        public static readonly DependencyProperty ShowIndicatorProperty =
            DependencyProperty.Register("ShowIndicator", typeof(bool), typeof(Rotational),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowIndicatorChange)));

        private static void HandleShowIndicatorChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ShowIndicator = (bool)e.NewValue;
            Instance.DrawUI();
        }

        public bool ShowIndicator
        {
            get
            {
                return (bool)GetValue(ShowIndicatorProperty);
            }
            set
            {
                SetValue(ShowIndicatorProperty, value);
            }
        }

        public static readonly DependencyProperty IndicatorBorderBrushProperty =
    DependencyProperty.Register("IndicatorBorderBrush", typeof(Brush), typeof(Rotational),
        new UIPropertyMetadata(Brushes.Gray, new PropertyChangedCallback(HandleIndicatorBorderBrushChange)));

        private static void HandleIndicatorBorderBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.IndicatorBorderBrush = (Brush)e.NewValue;
            Instance.DrawUI();
        }

        public Brush IndicatorBorderBrush
        {
            get
            {
                return (Brush)GetValue(IndicatorBorderBrushProperty);
            }
            set
            {
                SetValue(IndicatorBorderBrushProperty, value);
            }
        }

        public static readonly DependencyProperty IndicatorBrushProperty =
           DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(Rotational),
               new UIPropertyMetadata(Brushes.WhiteSmoke, new PropertyChangedCallback(HandleIndicatorBrushChange)));

        private static void HandleIndicatorBrushChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.IndicatorBrush = (Brush)e.NewValue;
            Instance.DrawUI();
        }

        public Brush IndicatorBrush
        {
            get
            {
                return (Brush)GetValue(IndicatorBrushProperty);
            }
            set
            {
                SetValue(IndicatorBrushProperty, value);
            }
        }

        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register("ShowValue", typeof(bool), typeof(Rotational),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowValueChange)));

        private static void HandleShowValueChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ShowValue = (bool)e.NewValue;
            Instance.DrawUI();
        }

        public bool ShowValue
        {
            get
            {
                return (bool)GetValue(ShowValueProperty);
            }
            set
            {
                SetValue(ShowValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueFontFamilyProperty =
            DependencyProperty.Register("ValueFontFamily", typeof(FontFamily), typeof(Rotational),
                new UIPropertyMetadata(new FontFamily("Segoe UI"), new PropertyChangedCallback(HandleValueFontFamilyChange)));

        private static void HandleValueFontFamilyChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ValueFontFamily = (FontFamily)e.NewValue;
            Instance.DrawUI();
        }

        public FontFamily ValueFontFamily
        {
            get
            {
                return (FontFamily)GetValue(ValueFontFamilyProperty);
            }
            set
            {
                SetValue(ValueFontFamilyProperty, value);
            }
        }

        public static readonly DependencyProperty ValueFontSizeProperty =
             DependencyProperty.Register("ValueFontSize", typeof(double), typeof(Rotational),
               new UIPropertyMetadata(12.0, new PropertyChangedCallback(HandleValueFontSizeChange)));

        private static void HandleValueFontSizeChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ValueFontSize = (double)e.NewValue;
            Instance.DrawUI();
        }

        public double ValueFontSize
        {
            get
            {
                return (double)GetValue(ValueFontSizeProperty);
            }
            set
            {
                SetValue(ValueFontSizeProperty, value);
            }
        }

        public static readonly DependencyProperty ValueFontWeightProperty =
           DependencyProperty.Register("ValueFontWeight", typeof(FontWeight), typeof(Rotational),
             new UIPropertyMetadata(FontWeights.Normal, new PropertyChangedCallback(HandleValueFontWeightChange)));

        private static void HandleValueFontWeightChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ValueFontWeight = (FontWeight)e.NewValue;
            Instance.DrawUI();
        }

        public FontWeight ValueFontWeight
        {
            get
            {
                return (FontWeight)GetValue(ValueFontWeightProperty);
            }
            set
            {
                SetValue(ValueFontWeightProperty, value);
            }
        }

        public static readonly DependencyProperty ValueForegroundProperty =
            DependencyProperty.Register("ValueForeground", typeof(Brush), typeof(Rotational),
            new UIPropertyMetadata(Brushes.Black, new PropertyChangedCallback(HandleValueForegroundChange)));

        private static void HandleValueForegroundChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ValueForeground = (Brush)e.NewValue;
            Instance.DrawUI();
        }

        public Brush ValueForeground
        {
            get
            {
                return (Brush)GetValue(ValueForegroundProperty);
            }
            set
            {
                SetValue(ValueForegroundProperty, value);
            }
        }

        public static readonly new DependencyProperty ToolTipProperty =
            DependencyProperty.Register("ToolTip", typeof(object), typeof(Rotational),
                new UIPropertyMetadata(null, new PropertyChangedCallback(HandleToolTipChange)));

        private static void HandleToolTipChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            Rotational Instance = Sender as Rotational;
            if (Instance == null)
                return;
            Instance.ToolTip = (object)e.NewValue;
            Instance.DrawUI();
        }

        public new object ToolTip
        {
            get
            {
                return (object)GetValue(ToolTipProperty);
            }
            set
            {
                SetValue(ToolTipProperty, value);
            }
        }
    }
}
