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

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ColorInputControl.xaml. User control to allow users to edit colors.
    /// </summary>
    public partial class ColorInputControl : UserControl
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColorInputControl ()
        {
            CommonInitialization(Colors.White);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="StartingColor">Initial color.</param>
        public ColorInputControl (Color StartingColor)
        {
            CommonInitialization(StartingColor);
        }

        /// <summary>
        /// Initialization common to all constructors.
        /// </summary>
        /// <param name="StartingColor">Initial color.</param>
        private void CommonInitialization (Color StartingColor)
        {
            InitializeComponent();
            SetColor(StartingColor);
            UStandardColors = new Dictionary<string, Color>();
            foreach (KeyValuePair<string, Color> StdColor in StandardColors)
                UStandardColors.Add(StdColor.Key.ToUpper(), StdColor.Value);
        }

        /// <summary>
        /// Sets the color visually and internally. Adds an appropriate tool tip to the color. Updates the color's value.
        /// </summary>
        /// <param name="TheColor">The color to set.</param>
        private void SetColor (Color TheColor)
        {
            ColorDisplay.Background = new SolidColorBrush(TheColor);
            _CurrentColor = TheColor;
            ColorInput.Text = ToString();
            ColorDisplay.ToolTip = MakeColorToolTip(_CurrentColor);
        }

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
        /// Return a string-equivalent of the color.
        /// </summary>
        /// <returns>String of the hex value of the color, preceeded by '#'.</returns>
        public override string ToString ()
        {
            StringBuilder C = new StringBuilder("#");
            C.Append(_CurrentColor.A.ToString("x2"));
            C.Append(_CurrentColor.R.ToString("x2"));
            C.Append(_CurrentColor.G.ToString("x2"));
            C.Append(_CurrentColor.B.ToString("x2"));
            return C.ToString();
        }

        private Color _CurrentColor = Colors.Transparent;
        /// <summary>
        /// Get or set the current color.
        /// </summary>
        public Color CurrentColor
        {
            get
            {
                return _CurrentColor;
            }
            set
            {
                _CurrentColor = value;
                SetColor(_CurrentColor);
            }
        }

        /// <summary>
        /// The input box portion of the control lost focus - treat the event the same as a return key press, e.g., read the
        /// text and create a color from the result.
        /// </summary>
        /// <param name="Sender">The text box that lost focus.</param>
        /// <param name="e">Not used.</param>
        private void HandleLostFocus (object Sender, RoutedEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            Color NewColor = Colors.Transparent;
            bool ParsedOK = TryParse(TB.Text, out NewColor);
            if (!ParsedOK)
                return;
            NotifyColorChange(NewColor);
        }

        /// <summary>
        /// Intercept key presses, specifically return and escape.
        /// Return: Take the text of the text box and interpret it as a color value (whether numeric or hex value).
        /// Escape: Reset the text to the CurrentValue - escape acts as an undo key.
        /// </summary>
        /// <param name="Sender">The text box where the key was pressed.</param>
        /// <param name="e">Event data.</param>
        private void HandleKeyPress (object Sender, KeyEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                Color NewColor = Colors.Transparent;
                bool ParsedOK = TryParse(TB.Text, out NewColor);
                if (!ParsedOK)
                    return;
                NotifyColorChange(NewColor);
            }
            if (e.Key == Key.Escape)
            {
                TB.Text = ToString();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Notify anyone who subscribes to our events of a color change.
        /// </summary>
        /// <param name="NewColor">The new color.</param>
        private void NotifyColorChange (Color NewColor)
        {
            if (HandleColorChanged == null)
                return;
            ColorInputColorChangedEventArgs args = new ColorInputColorChangedEventArgs();
            args.NewColor = NewColor;
            args.OldColor = CurrentColor;
            SetColor(NewColor);
            HandleColorChanged(this, args);
        }

        /// <summary>
        /// Handle mouse key clicks in the color box.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleColorClick (object Sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// Attempt to parse <paramref name="Raw"/> as either a hex color value or a standard color name. If the string begins with
        /// the '#' character then this method treats the string as a numeric value. Otherwise, the string is treated as a color name.
        /// </summary>
        /// <param name="Raw">The string to convert.</param>
        /// <param name="Final">The final, converted color.</param>
        /// <returns>True if <paramref name="Raw"/> was successfully parsed, false if not.</returns>
        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            if (string.IsNullOrEmpty(Raw))
                return false;

            Raw = Raw.Trim(new char[] { ' ' });
            if (string.IsNullOrEmpty(Raw))
                return false;
            if (Raw[0] != '#')
            {
                string RawName = Raw.ToUpper();
                if (UStandardColors.ContainsKey(RawName))
                {
                    Final = UStandardColors[RawName];
                    return true;
                }
                return false;
            }

            string A = "ff";
            string R = "";
            string G = "";
            string B = "";
            if (Raw.Length == 6)
            {
                R = Raw.Substring(0, 2);
                G = Raw.Substring(2, 2);
                B = Raw.Substring(4, 2);
            }
            else
                if (Raw.Length == 8)
            {
                A = Raw.Substring(0, 2);
                R = Raw.Substring(2, 2);
                G = Raw.Substring(4, 2);
                B = Raw.Substring(6, 2);
            }

            byte av = Convert.ToByte(A, 16);
            byte rv = Convert.ToByte(R, 16);
            byte gv = Convert.ToByte(G, 16);
            byte bv = Convert.ToByte(B, 16);

            Final = Color.FromArgb(av, rv, gv, bv);

            return true;
        }

        /// <summary>
        /// Delegate definition for color change events.
        /// </summary>
        /// <param name="Sender">The instance of the control where the color changed.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleColorChangedEvents (object Sender, ColorInputColorChangedEventArgs e);
        /// <summary>
        /// Event triggered when the current color is changed.
        /// </summary>
        public event HandleColorChangedEvents HandleColorChanged;

        /// <summary>
        /// Dictionary of standard colors based on <seealso cref="StandardColors"/> but with all color names normalized.
        /// </summary>
        private Dictionary<string, Color> UStandardColors;
        /// <summary>
        /// Dicationary of standard colors.
        /// </summary>
        private Dictionary<string, Color> StandardColors = new Dictionary<string, Color>()
        {
            {"AliceBlue", Colors.AliceBlue},
{"AntiqueWhite", Colors.AntiqueWhite},
{"Aqua", Colors.Aqua},
{"Aquamarine", Colors.Aquamarine},
{"Azure", Colors.Azure},
{"Beige", Colors.Beige},
{"Bisque", Colors.Bisque},
{"Black", Colors.Black},
{"BlanchedAlmond",Colors.BlanchedAlmond},
{"Blue", Colors.Blue},
{"BlueViolet", Colors.BlueViolet},
{"Brown", Colors.Brown},
{"BurlyWood", Colors.BurlyWood},
{"CadetBlue", Colors.CadetBlue},
{"Chartreuse", Colors.Chartreuse},
{"Chocolate", Colors.Chocolate},
{"Coral", Colors.Coral},
{"CornflowerBlue", Colors.CornflowerBlue},
{"Cornsilk", Colors.Cornsilk},
{"Crimson", Colors.Crimson},
{"Cyan", Colors.Cyan},
{"DarkBlue", Colors.DarkBlue},
{"DarkCyan", Colors.DarkCyan},
{"DarkGoldenrod", Colors.DarkGoldenrod},
{"DarkGray", Colors.DarkGray},
{"DarkGreen", Colors.DarkGreen},
{"DarkKhaki", Colors.DarkKhaki},
{"DarkMagenta", Colors.DarkMagenta},
{"DarkOliveGreen", Colors.DarkOliveGreen},
{"DarkOrange", Colors.DarkOrange},
{"DarkOrchid", Colors.DarkOrchid},
{"DarkRed", Colors.DarkRed},
{"DarkSalmon", Colors.DarkSalmon},
{"DarkSeaGreen", Colors.DarkSeaGreen},
{"DarkSlateBlue", Colors.DarkSlateBlue},
{"DarkSlateGray", Colors.DarkSlateGray},
{"DarkTurquoise", Colors.DarkTurquoise},
{"DarkViolet", Colors.DarkViolet},
{"DeepPink",Colors.DeepPink},
{"DeepSkyBlue", Colors.DeepSkyBlue},
{"DimGray", Colors.DimGray},
{"DodgerBlue", Colors.DodgerBlue},
{"Firebrick", Colors.Firebrick},
{"FloralWhite", Colors.FloralWhite},
{"ForestGreen", Colors.ForestGreen},
{"Fuchsia", Colors.Fuchsia},
{"Gainsboro", Colors.Gainsboro},
{"GhostWhite", Colors.GhostWhite},
{"Gold", Colors.Gold},
{"Goldenrod", Colors.Goldenrod},
{"Gray", Colors.Gray},
{"Green", Colors.Green},
{"GreenYellow",Colors.GreenYellow},
{"Honeydew", Colors.Honeydew},
{"HotPink", Colors.HotPink},
{"IndianRed", Colors.IndianRed},
{"Indigo",Colors.Indigo},
{"Ivory", Colors.Ivory},
{"Khaki", Colors.Khaki},
{"Lavender", Colors.Lavender},
{"LavenderBlush", Colors.LavenderBlush},
{"LawnGreen", Colors.LawnGreen},
{"LemonChiffon", Colors.LemonChiffon},
{"LightBlue", Colors.LightBlue},
{"LightCoral", Colors.LightCoral},
{"LightCyan", Colors.LightCyan},
{"LightGoldenrodYellow", Colors.LightGoldenrodYellow},
{"LightGray", Colors.LightGray},
{"LightGreen", Colors.LightGreen},
{"LightPink", Colors.LightPink},
{"LightSalmon", Colors.LightSalmon},
{"LightSeaGreen", Colors.LightSeaGreen},
{"LightSkyBlue", Colors.LightSkyBlue},
{"LightSlateGray", Colors.LightSlateGray},
{"LightSteelBlue",Colors.LightSteelBlue},
{"LightYellow", Colors.LightYellow},
{"Lime", Colors.Lime},
{"LimeGreen", Colors.LimeGreen},
{"Linen", Colors.Linen},
{"Magenta", Colors.Magenta},
{"Maroon", Colors.Maroon},
{"MediumAquamarine", Colors.MediumAquamarine},
{"MediumBlue", Colors.MediumBlue},
{"MediumOrchid", Colors.MediumOrchid},
{"MediumPurple",Colors.MediumPurple},
{"MediumSeaGreen", Colors.MediumSeaGreen},
{"MediumSlateBlue", Colors.MediumSlateBlue},
{"MediumSpringGreen", Colors.MediumSpringGreen},
{"MediumTurquoise", Colors.MediumTurquoise},
{"MediumVioletRed", Colors.MediumVioletRed},
{"MidnightBlue", Colors.MidnightBlue},
{"MintCream",Colors.MintCream},
{"MistyRose", Colors.MistyRose},
{"Moccasin", Colors.Moccasin},
{"NavajoWhite", Colors.NavajoWhite},
{"Navy", Colors.Navy},
{"OldLace", Colors.OldLace},
{"Olive", Colors.Olive},
{"OliveDrab", Colors.OliveDrab},
{"Orange", Colors.Orange},
{"OrangeRed", Colors.OrangeRed},
{"Orchid", Colors.Orchid},
{"PaleGoldenrod", Colors.PaleGoldenrod},
{"PaleGreen",Colors.PaleGreen},
{"PaleTurquoise", Colors.PaleTurquoise},
{"PaleVioletRed", Colors.PaleVioletRed},
{"PapayaWhip", Colors.PapayaWhip},
{"PeachPuff", Colors.PeachPuff},
{"Peru", Colors.Peru},
{"Pink", Colors.Pink},
{"Plum", Colors.Plum},
{"PowderBlue", Colors.PowderBlue},
{"Purple", Colors.Purple},
{"Red", Colors.Red},
{"RosyBrown", Colors.RosyBrown},
{"RoyalBlue", Colors.RoyalBlue},
{"SaddleBrown", Colors.SaddleBrown},
{"Salmon", Colors.Salmon},
{"SandyBrown", Colors.SandyBrown},
{"SeaGreen", Colors.SeaGreen},
{"SeaShell", Colors.SeaShell},
{"Sienna", Colors.Sienna},
{"Silver", Colors.Silver},
{"SkyBlue", Colors.SkyBlue},
{"SlateBlue",Colors.SlateBlue},
{"SlateGray", Colors.SlateGray},
{"Snow", Colors.Snow},
{"SpringGreen", Colors.SpringGreen},
{"SteelBlue", Colors.SteelBlue},
{"Tan",Colors.Tan},
{"Teal", Colors.Teal},
{"Thistle", Colors.Thistle},
{"Tomato", Colors.Tomato},
{"Transparent", Colors.Transparent},
{"Turquoise", Colors.Turquoise},
{"Violet", Colors.Violet},
{"Wheat", Colors.Wheat},
{"White", Colors.White},
{"WhiteSmoke", Colors.WhiteSmoke},
{"Yellow",Colors.Yellow},
{"YellowGreen", Colors.YellowGreen},
        };
    }

    /// <summary>
    /// Event data for color change events.
    /// </summary>
    public class ColorInputColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColorInputColorChangedEventArgs () : base()
        {
            NewColor = Colors.Transparent;
            OldColor = Colors.Transparent;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NewColor">The new color.</param>
        /// <param name="OldColor">The old color.</param>
        public ColorInputColorChangedEventArgs (Color NewColor, Color OldColor) : base()
        {
            this.NewColor = NewColor;
            this.OldColor = OldColor;
        }

        /// <summary>
        /// The new color.
        /// </summary>
        public Color NewColor { get; internal set; }

        /// <summary>
        /// The old color.
        /// </summary>
        public Color OldColor { get; internal set; }
    }
}
