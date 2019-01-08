using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Markup;
using Iro3.Data.ColorSpaces;

namespace Iro3.Controls.ColorInput
{
    /// <summary>
    /// Implements a simple color input control.
    /// </summary>
    [ContentProperty("CurrentColor")]
    [DebuggerDisplay("Color: {StringValue}, {CurrentColorSpace}")]
    public partial class SimpleColor : ContentControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SimpleColor () : base()
        {
            UStandardColors = new Dictionary<string, Color>();
            foreach (KeyValuePair<string, Color> StdColor in StandardColors)
                UStandardColors.Add(StdColor.Key.ToUpper(), StdColor.Value);
            MakeControl();
        }

        /// <summary>
        /// Create the control.
        /// </summary>
        private void MakeControl ()
        {
            this.Margin = new Thickness(1);
            Infrastructure = new Grid();
            ColumnDefinition CD0 = new ColumnDefinition();
            CD0.Tag = "ColorColumn";
            CD0.Width = new GridLength(40.0, GridUnitType.Pixel);
            Infrastructure.ColumnDefinitions.Add(CD0);

            ColumnDefinition CD1 = new ColumnDefinition();
            CD1.Width = new GridLength(15.0, GridUnitType.Pixel);
            CD1.Tag = "ColorSpaceColumn";
            Infrastructure.ColumnDefinitions.Add(CD1);

            ColumnDefinition CD2 = new ColumnDefinition();
            CD2.Width = new GridLength(150.0, GridUnitType.Pixel);
            CD2.Tag = "InputColumn";
            Infrastructure.ColumnDefinitions.Add(CD2);

            //Column 0
            ColorBox = new Border();
            Grid.SetColumn(ColorBox, 0);
            Infrastructure.Children.Add(ColorBox);
            ColorBox.Background = Brushes.White;
            ColorBox.BorderBrush = Brushes.Black;
            ColorBox.BorderThickness = new Thickness(1);
            ColorBox.CornerRadius = new CornerRadius(2);
            ColorInfrastructure = new Grid();
            ColorBox.Child = ColorInfrastructure;
            ColorViewer = new Border();
            ColorViewer.Background = Brushes.White;
            ColorViewerBackground = new Border();
            ColorViewerBackground.Background = Brushes.Transparent;
            ColorInfrastructure.Children.Add(ColorViewerBackground);
            ColorInfrastructure.Children.Add(ColorViewer);
            OverlayedColorSpaceLabel = new TextBlock();
            Grid.SetColumn(OverlayedColorSpaceLabel, 0);
            Infrastructure.Children.Add(OverlayedColorSpaceLabel);
            OverlayedColorSpaceLabel.Text = "";
            OverlayedColorSpaceLabel.VerticalAlignment = VerticalAlignment.Center;
            OverlayedColorSpaceLabel.HorizontalAlignment = HorizontalAlignment.Center;
            OverlayedColorSpaceLabel.Background = Brushes.Transparent;
            OverlayedColorSpaceLabel.Foreground = Brushes.Transparent;

            //Column 1
            ColorSpaceLabel = new TextBlock();
            ColorSpaceLabel.Width = 15.0;
            Infrastructure.Children.Add(ColorSpaceLabel);
            Grid.SetColumn(ColorSpaceLabel, 1);
            ColorSpaceLabel.Text = "test test";
            ColorSpaceLabel.VerticalAlignment = VerticalAlignment.Center;
            ColorSpaceLabel.HorizontalAlignment = HorizontalAlignment.Left;
            ColorSpaceLabel.Background = Brushes.Transparent;
            ColorSpaceLabel.Foreground = Brushes.Black;
            ColorSpaceLabel.FontSize = 9.0;
            ColorSpaceLabel.FontWeight = FontWeights.Thin;
            ColorSpaceLabel.Margin = new Thickness(1);
            RotateTransform RT = new RotateTransform();
            RT.Angle = 270;
            TransformGroup TG = new TransformGroup();
            TG.Children.Add(RT);
            ColorSpaceLabel.LayoutTransform = RT;

            //Column 2
            ColorInput = new TextBox();
            Grid.SetColumn(ColorInput, 2);
            Infrastructure.Children.Add(ColorInput);
            ColorInput.Margin = new Thickness(2, 0, 0, 0);
            ColorInput.VerticalContentAlignment = VerticalAlignment.Center;
            ColorInput.FontFamily = new FontFamily("Consolas");
            ColorInput.FontSize = 16;
            ColorInput.FontWeight = FontWeights.Bold;
            ColorInput.Foreground = Brushes.Black;
            ColorInput.ToolTip = "Enter an ARGB hex color code or a standard color name (no spaces in name, any casing).";
            ColorInput.LostFocus += HandleInputLostFocus;
            ColorInput.KeyDown += HandleInputKeyPress;

            //Set the menu to enable changing color spaces.
            SetContextMenu();

            LocalColor = new RGB();

            this.Content = Infrastructure;
        }

        /// <summary>
        /// Add or remove the context menu.
        /// </summary>
        private void SetContextMenu ()
        {
            if (IsContextMenuEnabled)
            {
                ControlMenu = new ContextMenu();
                Infrastructure.ContextMenu = ControlMenu;
                MenuItem0 = new MenuItem();
                ControlMenu.Items.Add(MenuItem0);
                MenuItem0.Header = "Color spaces";
                AddColorSpaceMenuItems(MenuItem0);
                ControlMenu.Items.Add(new Separator());
                MenuItem MI = new MenuItem();
                MI.Header = "Show known color names";
                MI.IsChecked = false;
                MI.Click += SetColorNameVisibility;
                ControlMenu.Items.Add(MI);
            }
            else
            {
                Infrastructure.ContextMenu = null;
            }
        }

        /// <summary>
        /// Handle menu clicks for showing or not showing color names.
        /// </summary>
        /// <param name="Sender">The menu item that was clicked.</param>
        /// <param name="e">Not used.</param>
        private void SetColorNameVisibility (object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            MI.IsChecked = !MI.IsChecked;
            ShowAvailableColorName = MI.IsChecked;
        }

        /// <summary>
        /// Add color space menu items.
        /// </summary>
        /// <param name="Parent">Parent menu item.</param>
        private void AddColorSpaceMenuItems (MenuItem Parent)
        {
            foreach (ColorSpaces ColorSpace in Converters.KnownColorSpaces)
            {
                MenuItem MI = new MenuItem();
                MI.IsCheckable = true;
                MI.IsChecked = ColorSpace == ColorSpaces.RGB ? true : false;
                if (LocalColor == null)
                    MI.Header = "???";
                else
                    MI.Header = LocalColor.ColorLabel;
                MI.Click += ColorSpaceMenuItemClick;
                Parent.Items.Add(MI);
            }
        }

        private IColorSpace LocalColor { get; set; }

        /// <summary>
        /// Handle clicks in the color space context menu item.
        /// </summary>
        /// <param name="Sender">The menu item that was clicked.</param>
        /// <param name="e">Not used.</param>
        private void ColorSpaceMenuItemClick (object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string NewColorSpaceName = MI.Header as string;
            if (string.IsNullOrEmpty(NewColorSpaceName))
                return;
            foreach (MenuItem CMI in MenuItem0.Items)
            {
                string LName = CMI.Header as string;
                if (string.IsNullOrEmpty(LName))
                    return;
                if (LName == NewColorSpaceName)
                    CMI.IsChecked = true;
                else
                    CMI.IsChecked = false;
            }
            ColorSpace = IroColorSpace.GetSpaceFromName(NewColorSpaceName);
        }

        /// <summary>
        /// Given a string value, return the index in the set of column definitions where it is found.
        /// </summary>
        /// <param name="ColumnTag">Determines the index to return.</param>
        /// <returns>Index of the column whose tag value is <paramref name="ColumnTag"/> on success, -1 if not found or on error.</returns>
        private int GetColumnIndex (string ColumnTag)
        {
            if (string.IsNullOrEmpty(ColumnTag))
                return -1;
            int Index = 0;
            foreach (ColumnDefinition CD in Infrastructure.ColumnDefinitions)
            {
                string CDName = CD.Tag as string;
                if (CDName == null)
                    return -1;
                if (CDName == ColumnTag)
                    return Index;
                Index++;
            }
            return -1;
        }

        //http://juicystudio.com/article/luminositycontrastratioalgorithm.php
        public Color ContrastingColor (Color BaseColor)
        {
            HSL BaseHSL = new HSL(new RGB(255, BaseColor.R, BaseColor.G, BaseColor.B));

            if (BaseHSL.L >= 0.5)
                return Colors.Black;
            else
                return Colors.White;
        }

        /// <summary>
        /// Draw the control.
        /// </summary>
        private void DrawControl ()
        {
            if (IsColorBoxVisible)
            {
                Infrastructure.ColumnDefinitions[GetColumnIndex("ColorColumn")].Width = new GridLength(ColorBoxWidth, GridUnitType.Pixel);
            }
            else
            {
                Infrastructure.ColumnDefinitions[GetColumnIndex("ColorColumn")].Width = new GridLength(0.0, GridUnitType.Pixel);
            }
            if (ColorSpaceLabelOverColor)
            {
                Infrastructure.ColumnDefinitions[GetColumnIndex("ColorSpaceColumn")].Width = new GridLength(0.0, GridUnitType.Pixel);
                ColorSpaceLabel.Text = "";
                OverlayedColorSpaceLabel.FontWeight = ColorSpaceLabelFontWeight;
                OverlayedColorSpaceLabel.Text = LocalColor.ColorLabel;
                OverlayedColorSpaceLabel.Foreground = new SolidColorBrush(ContrastingColor(CurrentColor));
            }
            else
            {
                OverlayedColorSpaceLabel.Text = "";
                OverlayedColorSpaceLabel.Foreground = Brushes.Transparent;
                OverlayedColorSpaceLabel.Background = Brushes.Transparent;
                if (ShowColorSpaceLabel)
                {
                    Infrastructure.ColumnDefinitions[GetColumnIndex("ColorSpaceColumn")].Width = new GridLength(15.0, GridUnitType.Pixel);
                    ColorSpaceLabel.Text = LocalColor.ColorLabel;
                    ColorSpaceLabel.ToolTip = "Current color space: " + ColorSpaceLabel.Text;
                }
                else
                {
                    Infrastructure.ColumnDefinitions[GetColumnIndex("ColorSpaceColumn")].Width = new GridLength(0.0, GridUnitType.Pixel);
                    ColorSpaceLabel.Text = "";
                }
            }
            Infrastructure.ColumnDefinitions[GetColumnIndex("InputColumn")].Width = new GridLength(ColorInputWidth, GridUnitType.Pixel);
            ColorInput.Margin = new Thickness(InternalGap, 0, 0, 0);
            if (EnableCheckerboard)
                ColorViewerBackground.Background = CheckerboardPatternBrush(CheckWidth, CheckHeight,
                    new SolidColorBrush(Check1Color), new SolidColorBrush(Check2Color));
            else
                ColorViewerBackground.Background = Brushes.Transparent;

            ColorViewer.Background = new SolidColorBrush(CurrentColor);
            ColorBox.ToolTip = MakeColorToolTip(CurrentColor);
            if (ShowAvailableColorName)
            {
                ColorInput.Text = GetColorName(CurrentColor);
            }
            else
            {
                //ColorSpaceBase.GetInputString()
                ColorInput.Text = ToString();
            }
            ColorInput.IsReadOnly = StaticView;
            if (IsEnabled)
            {
                ColorInput.IsEnabled = true;
                ColorBox.BorderBrush = Brushes.Black;
                ColorBox.Background = Brushes.Transparent;
            }
            else
            {
                ColorInput.IsEnabled = false;
                ColorBox.BorderBrush = Brushes.DarkGray;
                ColorBox.Background = SystemColors.InactiveCaptionBrush;
            }
        }

        private void DrawInColorSpace ()
        {
            /*
            switch (ColorSpaces)
            {
            }
            */
        }

        /// <summary>
        /// Reset the current color to the RGB base color.
        /// </summary>
        public void ResetColorToBase ()
        {
            CurrentColor = BaseColor;
        }

        #region Control parts.
        private Grid Infrastructure;
        private Border ColorBox;
        private Grid ColorInfrastructure;
        private Border ColorViewer;
        private Border ColorViewerBackground;
        private TextBox ColorInput;
        private TextBlock ColorSpaceLabel;
        private ContextMenu ControlMenu;
        private MenuItem MenuItem0;
        private TextBlock OverlayedColorSpaceLabel;
        #endregion

        /// <summary>
        /// Make a nice-looking tool tip for the color box. If the color is found in the set of standard colors, the appropriate
        /// color name will be used. Otherwise, the hex color value will be used.
        /// </summary>
        /// <param name="TheColor">The color from which the text for the tool tip will be derived.</param>
        /// <returns>Tool tip string.</returns>
        private string MakeColorToolTip (Color TheColor)
        {
            if (StandardColors.ContainsValue(TheColor))
            {
                foreach (KeyValuePair<string, Color> Std in StandardColors)
                    if (Std.Value == TheColor)
                        return Std.Key;
            }
            return ToString();
        }

        /// <summary>
        /// Get the raw value of the current color. Form is ARGB.
        /// </summary>
        public UInt32 RawColorValue
        {
            get
            {
                UInt32 v = (UInt32)((int)CurrentColor.A << 24 | (int)CurrentColor.R << 16 | (int)CurrentColor.G << 8 | (int)CurrentColor.B);
                return v;
            }
        }

        /// <summary>
        /// Return a checkerboard brush.
        /// </summary>
        /// <param name="Width">Overall width of the brush.</param>
        /// <param name="Height">Overall height of the brush.</param>
        /// <param name="Color0">First brush.</param>
        /// <param name="Color1">Second brush.</param>
        /// <returns>Drawing brush.</returns>
        private DrawingBrush CheckerboardPatternBrush (double Width, double Height,
            SolidColorBrush Color0, SolidColorBrush Color1)
        {
            double HalfWidth = Width / 2;
            double HalfHeight = Height / 2;
            DrawingBrush TheBrush = new DrawingBrush();
            TheBrush.AlignmentX = AlignmentX.Left;
            TheBrush.AlignmentY = AlignmentY.Top;
            TheBrush.Stretch = Stretch.None;
            TheBrush.ViewportUnits = BrushMappingMode.Absolute;
            TheBrush.TileMode = TileMode.Tile;
            TheBrush.Viewport = new Rect(new Point(0, 0), new Point(Width, Height));
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing();
            Overall.Brush = Color0;
            RectangleGeometry RG0 = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = RG0;
            DG.Children.Add(Overall);

            GeometryDrawing Other = new GeometryDrawing();
            Other.Brush = Color1;
            RectangleGeometry RG1 = new RectangleGeometry(new Rect(0, 0, HalfWidth, HalfHeight));
            RectangleGeometry RG2 = new RectangleGeometry(new Rect(HalfWidth, HalfHeight, HalfWidth, HalfHeight));
            GeometryGroup GG = new GeometryGroup();
            GG.Children.Add(RG1);
            GG.Children.Add(RG2);
            Other.Geometry = GG;
            DG.Children.Add(Other);

            return TheBrush;
        }

        /// <summary>
        /// Programmatically set the color space and color value at the same time.
        /// </summary>
        /// <param name="NewColorSpace">The new color space.</param>
        /// <param name="RawColorValue">The new color value.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool SetColorSpaceAndColor (ColorSpaces NewColorSpace, string RawColorValue)
        {
            if (string.IsNullOrEmpty(RawColorValue))
                return false;
            Color Result = Colors.Transparent;
            bool OK = IroColorSpace.ParseColorInput(RawColorValue, NewColorSpace, out Result);
            if (!OK)
                return false;
            IColorSpace BC = IroColorSpace.MakeColor(NewColorSpace);
            ColorInput.Text = BC.ToInputString();
            CurrentColor = BC.ToRGBColor();
            return true;
        }

        /// <summary>
        /// Programmatically set the color and space value at the same time.
        /// </summary>
        /// <param name="NewColorAndSpace">The new color and color space.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool SetColorSpaceAndColor (IColorSpace NewColorAndSpace)
        {
            if (NewColorAndSpace == null)
                return false;
            LocalColor = NewColorAndSpace;
            ColorInput.Text = LocalColor.ToInputString();
            CurrentColor = LocalColor.ToRGBColor();
            return true;
        }

        /// <summary>
        /// Property that returns a string value of the contents of the color. Included for debugging purposes.
        /// </summary>
        public string StringValue
        {
            get
            {
                return ToString();
            }
        }

        public string CurrentColorSpace
        {
            get
            {
                return ColorSpace.ToString();
            }
        }

        /// <summary>
        /// Return the hex value of the color in a string.
        /// </summary>
        /// <param name="Prefix">Hex value prefix.</param>
        /// <returns>Color as a string.</returns>
        public string ToString (string Prefix)
        {
            StringBuilder C = new StringBuilder(Prefix);
            C.Append(CurrentColor.A.ToString("x2"));
            C.Append(CurrentColor.R.ToString("x2"));
            C.Append(CurrentColor.G.ToString("x2"));
            C.Append(CurrentColor.B.ToString("x2"));
            return C.ToString();
        }

        /// <summary>
        /// Return the hex value of the color in a string.
        /// </summary>
        /// <returns>"#"-prefixed hex value in a string.</returns>
        public override string ToString ()
        {
            return ToString("#");
        }

        public string MakeInputString ()
        {
            return "";
        }
    }
}
