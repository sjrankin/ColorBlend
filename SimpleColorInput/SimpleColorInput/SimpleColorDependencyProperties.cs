using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using Iro3.Data.ColorSpaces;

namespace Iro3.Controls.ColorInput
{
    public partial class SimpleColor
    {
        /// <summary>
        /// Identifies the CurrentColor property.
        /// </summary>
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(Color), typeof(SimpleColor),
                new UIPropertyMetadata(Colors.White, new PropertyChangedCallback(HandleCurrentColorChange)));

        /// <summary>
        /// Handle changes to CurrentColor.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleCurrentColorChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            bool Canceled = Caller.NotifyColorChange(Caller.CurrentColor, (Color)e.NewValue);
            if (Canceled)
                return;
            Caller.CurrentColor = (Color)e.NewValue;
            Caller.BaseColor = (Color)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the current color.
        /// </summary>
        [Category("Simple Color")]
        [Description("The current color.")]
        public Color CurrentColor
        {
            get
            {
                return (Color)GetValue(CurrentColorProperty);
            }
            set
            {
                SetValue(CurrentColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the BaseColor dependency property.
        /// </summary>
        public static readonly DependencyProperty BaseColorProperty =
            DependencyProperty.Register("BaseColor", typeof(Color), typeof(SimpleColor),
                new PropertyMetadata(Colors.White, new PropertyChangedCallback(HandleBaseColorChange)));

        /// <summary>
        /// Handle changes to BaseColor.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleBaseColorChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.BaseColor = (Color)e.NewValue;
        }

        /// <summary>
        /// Get the base RGB color.
        /// </summary>
        [Category("Simple Color")]
        [Description("The base RGB color. Indirectly updated when CurrentColor is set.")]
        public Color BaseColor
        {
            get
            {
                return (Color)GetValue(BaseColorProperty);
            }
            private set
            {
                SetValue(BaseColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the SuppressChangeNotificationEvent dependency property.
        /// </summary>
        public static readonly DependencyProperty SuppressChangeNotificationEventProperty =
            DependencyProperty.Register("SuppressChangeNotificationEvent", typeof(bool), typeof(SimpleColor),
                new PropertyMetadata(false, new PropertyChangedCallback(HandleSuppressChangeNotificationEventChange)));

        /// <summary>
        /// Handle changes to SuppressChangeNotificationEvent.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleSuppressChangeNotificationEventChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.SuppressChangeNotificationEvent = (bool)e.NewValue;
        }

        /// <summary>
        /// If set to true, suppresses the next color change notification event. Will be reset to false when the color is set.
        /// </summary>
        [Category("Simple Color")]
        [Description("If set to true, suppresses the next color change notification event. Will be reset to false when the color is set.")]
        public bool SuppressChangeNotificationEvent
        {
            get
            {
                return (bool)GetValue(SuppressChangeNotificationEventProperty);
            }
            set
            {
                SetValue(SuppressChangeNotificationEventProperty, value);
            }
        }

        /// <summary>
        /// Identifies the IsColorBoxVisible property.
        /// </summary>
        public static readonly DependencyProperty IsColorBoxVisibleProperty =
            DependencyProperty.Register("IsColorBoxVisible", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleIsColorBoxVisibleChange)));

        /// <summary>
        /// Handle changes to IsColorBoxVisible.
        /// </summary>
        /// <param name="Sender">Instance where the value changed.</param>
        /// <param name="e">Event data.</param>
        public static void HandleIsColorBoxVisibleChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.IsColorBoxVisible = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the value that determines if the color box is visible.
        /// </summary>
        [Category("Simple Color")]
        [Description("Determines if the color box is visible.")]
        public bool IsColorBoxVisible
        {
            get
            {
                return (bool)GetValue(IsColorBoxVisibleProperty);
            }
            set
            {
                SetValue(IsColorBoxVisibleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ColorBoxWidth property.
        /// </summary>
        public static readonly DependencyProperty ColorBoxWidthProperty =
            DependencyProperty.Register("ColorBoxWidth", typeof(double), typeof(SimpleColor),
                new UIPropertyMetadata(40.0, new PropertyChangedCallback(HandleColorBoxWidthChange)));

        /// <summary>
        /// Handle changes to ColorBoxWidth.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleColorBoxWidthChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ColorBoxWidth = (double)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the width of the color box part of the control.
        /// </summary>
        [Category("Simple Color")]
        [Description("Width of the color box portion of the control.")]
        public double ColorBoxWidth
        {
            get
            {
                return (double)GetValue(ColorBoxWidthProperty);
            }
            set
            {
                SetValue(ColorBoxWidthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ColorInputWidth property.
        /// </summary>
        public static readonly DependencyProperty ColorInputWidthProperty =
            DependencyProperty.Register("ColorInputWidth", typeof(double), typeof(SimpleColor),
                new UIPropertyMetadata(150.0, new PropertyChangedCallback(HandleColorInputWidthChange)));

        /// <summary>
        /// Handle changes to ColorInputWidth.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleColorInputWidthChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ColorInputWidth = (double)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the width of the input box part of the control.
        /// </summary>
        [Category("Simple Color")]
        [Description("Width of the input box portion of the control.")]
        public double ColorInputWidth
        {
            get
            {
                return (double)GetValue(ColorInputWidthProperty);
            }
            set
            {
                SetValue(ColorInputWidthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the InternalGap dependency property.
        /// </summary>
        public static readonly DependencyProperty InternalGapProperty =
            DependencyProperty.Register("InternalGap", typeof(double), typeof(SimpleColor),
                new UIPropertyMetadata(2.0, new PropertyChangedCallback(HandleInternalGapChange)));

        /// <summary>
        /// Handle changes to InternalGap.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleInternalGapChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.InternalGap = (double)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the horizontal width between internal control parts.
        /// </summary>
        [Category("Simple Color")]
        [Description("Width of the horizontal internal gap between control parts.")]
        public double InternalGap
        {
            get
            {
                return (double)GetValue(InternalGapProperty);
            }
            set
            {
                SetValue(InternalGapProperty, value);
            }
        }

        /// <summary>
        /// Identifies the StaticView property.
        /// </summary>
        public static readonly DependencyProperty StaticViewProperty =
            DependencyProperty.Register("StaticView", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(false, new PropertyChangedCallback(HandleStaticViewChange)));

        /// <summary>
        /// Handle changes to StaticView.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleStaticViewChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.StaticView = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Determines if the control is static or not.
        /// </summary>
        [Category("Simple Color")]
        [Description("Determines if the control is static or not.")]
        public bool StaticView
        {
            get
            {
                return (bool)GetValue(StaticViewProperty);
            }
            set
            {
                SetValue(StaticViewProperty, value);
            }
        }

        /// <summary>
        /// Identifies the EnableCheckerboard dependency property.
        /// </summary>
        public static readonly DependencyProperty EnableCheckerboardProperty =
            DependencyProperty.Register("EnableCheckerboard", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleEnableCheckerboardChange)));

        /// <summary>
        /// Handle changes to EnableCheckerboard.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleEnableCheckerboardChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.EnableCheckerboard = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Enable or disable the checkerboard background pattern.
        /// </summary>
        [Category("Simple Color")]
        [Description("Determines if the color part has a checkerboard background pattern.")]
        public bool EnableCheckerboard
        {
            get
            {
                return (bool)GetValue(EnableCheckerboardProperty);
            }
            set
            {
                SetValue(EnableCheckerboardProperty, value);
            }
        }

        /// <summary>
        /// Identifies the CheckWidth dependency property.
        /// </summary>
        public static readonly DependencyProperty CheckWidthProperty =
            DependencyProperty.Register("CheckWidth", typeof(double), typeof(SimpleColor),
                new UIPropertyMetadata(8.0, new PropertyChangedCallback(HandleCheckWidthChange)));

        /// <summary>
        /// Handle changes to CheckWidth.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleCheckWidthChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.CheckWidth = (double)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// The width of each check in the checkerboard background pattern.
        /// </summary>
        [Category("Simple Color")]
        [Description("The width of each check in the checkerboard background pattern.")]
        public double CheckWidth
        {
            get
            {
                return (double)GetValue(CheckWidthProperty);
            }
            set
            {
                SetValue(CheckWidthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the CheckHeight dependency property.
        /// </summary>
        public static readonly DependencyProperty CheckHeightProperty =
            DependencyProperty.Register("CheckHeight", typeof(double), typeof(SimpleColor),
                new UIPropertyMetadata(8.0, new PropertyChangedCallback(HandleCheckHeightChange)));

        /// <summary>
        /// Handle changes to CheckHeight.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleCheckHeightChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.CheckHeight = (double)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// The height of each check in the checkerboard background pattern.
        /// </summary>
        [Category("Simple Color")]
        [Description("The height of each check in the checkerboard background pattern.")]
        public double CheckHeight
        {
            get
            {
                return (double)GetValue(CheckHeightProperty);
            }
            set
            {
                SetValue(CheckHeightProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Check1Color dependency property.
        /// </summary>
        public static readonly DependencyProperty Check1ColorProperty =
            DependencyProperty.Register("Check1Color", typeof(Color), typeof(SimpleColor),
                new UIPropertyMetadata(Colors.WhiteSmoke, new PropertyChangedCallback(HandleCheck1ColorChange)));

        /// <summary>
        /// Handle changes to Check1Color.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleCheck1ColorChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.Check1Color = (Color)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// The first checkerboard color.
        /// </summary>
        [Category("Simple Color")]
        [Description("The first checkerboard color.")]
        public Color Check1Color
        {
            get
            {
                return (Color)GetValue(Check1ColorProperty);
            }
            set
            {
                SetValue(Check1ColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Check2Color dependency property.
        /// </summary>
        public static readonly DependencyProperty Check2ColorProperty =
            DependencyProperty.Register("Check2Color", typeof(Color), typeof(SimpleColor),
                new UIPropertyMetadata(Colors.LightGray, new PropertyChangedCallback(HandleCheck2ColorChange)));

        /// <summary>
        /// Handle changes to Check2Color.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleCheck2ColorChange (object Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.Check2Color = (Color)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// The second checkerboard color.
        /// </summary>
        [Category("Simple Color")]
        [Description("The second checkerboard color.")]
        public Color Check2Color
        {
            get
            {
                return (Color)GetValue(Check2ColorProperty);
            }
            set
            {
                SetValue(Check2ColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the IsEnabled dependency property.
        /// </summary>
        public static readonly new DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleIsEnabledChange)));

        /// <summary>
        /// Handle changes to IsEnabled.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleIsEnabledChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.IsEnabled = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Determines the enable state of the control.
        /// </summary>
        [Category("Simple Color")]
        [Description("Sets the enabled state for the control.")]
        public new bool IsEnabled
        {
            get
            {
                return (bool)GetValue(IsEnabledProperty);
            }
            set
            {
                SetValue(IsEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ShowAvailableColorName property.
        /// </summary>
        public static readonly DependencyProperty ShowAvailableColorNameProperty =
            DependencyProperty.Register("ShowAvailableColorName", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(false, new PropertyChangedCallback(HandleShowAvailableColorNameChange)));

        /// <summary>
        /// Handle changes to ShowAvailableColorName.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleShowAvailableColorNameChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ShowAvailableColorName = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// If True, color names are shown if the color value matches a known name. Otherwise, just numeric values are shown.
        /// </summary>
        [Category("Simple Color")]
        [Description("Show or hide color names for known colors.")]
        public bool ShowAvailableColorName
        {
            get
            {
                return (bool)GetValue(ShowAvailableColorNameProperty);
            }
            set
            {
                SetValue(ShowAvailableColorNameProperty, value);
            }
        }

        /// <summary>
        /// Identifies the IsContextMenuEnabled property.
        /// </summary>
        public static readonly DependencyProperty IsContextMenuEnabledProperty =
            DependencyProperty.Register("IsContextMenuEnabled", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleIsContextMenuEnabledChange)));

        /// <summary>
        /// Handle changes to IsContextMenuEnabled.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleIsContextMenuEnabledChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.IsContextMenuEnabled = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Determines if the context menu is enabled/visible or not.
        /// </summary>
        [Category("Simple Color")]
        [Description("Determines if the context menu is enabled/visible or not.")]
        public bool IsContextMenuEnabled
        {
            get
            {
                return (bool)GetValue(IsContextMenuEnabledProperty);
            }
            set
            {
                SetValue(IsContextMenuEnabledProperty, value);
            }
        }

        #region Color space-related dependency properties.
        /// <summary>
        /// Identifies the ShowColorSpaceLabel dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowColorSpaceLabelProperty =
            DependencyProperty.Register("ShowColorSpaceLabel", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleShowColorSpaceLabelChange)));

        /// <summary>
        /// Handle changes to ShowColorSpaceLabel.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Event data.</param>
        public static void HandleShowColorSpaceLabelChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ShowColorSpaceLabel = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Determines if the color space label is visible.
        /// </summary>
        [Category("Simple Color")]
        [Description("Determines if the color space label is visible.")]
        public bool ShowColorSpaceLabel
        {
            get
            {
                return (bool)GetValue(ShowColorSpaceLabelProperty);
            }
            set
            {
                SetValue(ShowColorSpaceLabelProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ColorSpaceLabelOverColor property.
        /// </summary>
        public static readonly DependencyProperty ColorSpaceLabelOverColorProperty =
            DependencyProperty.Register("ColorSpaceLabelOverColor", typeof(bool), typeof(SimpleColor),
                new UIPropertyMetadata(true, new PropertyChangedCallback(HandleColorSpaceLabelOverColorChange)));

        /// <summary>
        /// Handle changes to ColorSpaceLabelOverColor.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleColorSpaceLabelOverColorChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ColorSpaceLabelOverColor = (bool)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Determines the location of the color space label.
        /// </summary>
        [Category("Simple Color")]
        [Description("Determines if the color space label is over the color or beside it.")]
        public bool ColorSpaceLabelOverColor
        {
            get
            {
                return (bool)GetValue(ColorSpaceLabelOverColorProperty);
            }
            set
            {
                SetValue(ColorSpaceLabelOverColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ColorSpaceLabelFontWeight property.
        /// </summary>
        public static readonly DependencyProperty ColorSpaceLabelFontWeightProperty =
            DependencyProperty.Register("ColorSpaceLabelFontWeight", typeof(FontWeight), typeof(SimpleColor),
                new UIPropertyMetadata(FontWeights.Normal, new PropertyChangedCallback(HandleColorSpaceLabelFontWeightChange)));

        /// <summary>
        /// Handle changes to ColorSpaceLabelFontWeight.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleColorSpaceLabelFontWeightChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ColorSpaceLabelFontWeight = (FontWeight)e.NewValue;
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the font weight for the color space label.
        /// </summary>
        [Category("Simple Color")]
        [Description("Color space label font weight.")]
        public FontWeight ColorSpaceLabelFontWeight
        {
            get
            {
                return (FontWeight)GetValue(ColorSpaceLabelFontWeightProperty);
            }
            set
            {
                SetValue(ColorSpaceLabelFontWeightProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ColorSpace dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorSpaceProperty =
            DependencyProperty.Register("ColorSpace", typeof(ColorSpaces), typeof(SimpleColor),
                new UIPropertyMetadata(ColorSpaces.RGB, new PropertyChangedCallback(HandleColorSpaceChanged)));

        /// <summary>
        /// Handle changes to ColorSpace.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleColorSpaceChanged (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.ColorSpace = (ColorSpaces)e.NewValue;
            Caller.LocalColor = IroColorSpace.MakeColor(Caller.ColorSpace);
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the current color space.
        /// </summary>
        [Category("Simple Color")]
        [Description("The current color space.")]
        public ColorSpaces ColorSpace
        {
            get
            {
                return (ColorSpaces)GetValue(ColorSpaceProperty);
            }
            set
            {
                SetValue(ColorSpaceProperty, value);
            }
        }

        /// <summary>
        /// Identifies the ColorSpace2 property.
        /// </summary>
        public static readonly DependencyProperty ColorSpace2Property =
            DependencyProperty.Register("ColorSpace2", typeof(IColorSpace), typeof(SimpleColor),
                new UIPropertyMetadata(new RGB(0xffffffff), new PropertyChangedCallback(HandleColorSpace2Change)));

        /// <summary>
        /// Handle changes to ColorSpace2.
        /// </summary>
        /// <param name="Sender">Instance where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public static void HandleColorSpace2Change(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            SimpleColor Caller = Sender as SimpleColor;
            if (Caller == null)
                return;
            Caller.LocalColor = (IColorSpace)e.NewValue;
            Caller.CurrentColor = Caller.LocalColor.ToRGBColor();
            Caller.DrawControl();
        }

        /// <summary>
        /// Get or set the color space/color.
        /// </summary>
        [Category("Simple Color")]
        [Description("The current color space and color.")]
        public IColorSpace ColorSpace2
        {
            get
            {
                return (IColorSpace)GetValue(ColorSpace2Property);
            }
            set
            {
                SetValue(ColorSpace2Property, value);
            }
        }
        #endregion
    }
}
