using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Diagnostics;

namespace Iro3.Data.ColorSpaces
{
    /// <summary>
    /// Encapsulates an HSL color.
    /// </summary>
    [DebuggerDisplay("{NiceHSL}")]
    public class HSL : IEquatable<HSL>, IColorSpace
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HSL ()
        {
            CommonInitialization(0, 0, 0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="H">Initial hue component.</param>
        /// <param name="S">Initial saturation component.</param>
        /// <param name="L">Initial luminance component.</param>
        public HSL (double H, double S, double L)
        {
            CommonInitialization(H, S, L);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Source">Used as source for components.</param>
        public HSL (HSL Source)
        {
            if (Source == null)
                throw new ArgumentNullException("Source");
            CommonInitialization(Source.H, Source.S, Source.L);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RGBSource">After conversion to HSL, used as source for components.</param>
        public HSL (RGB RGBSource)
        {
            if (RGBSource == null)
                throw new ArgumentNullException("RGBSource");
            HSL Temp = RGBSource.ToHSL();
            CommonInitialization(Temp.H, Temp.S, Temp.L);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Other">Other color space color to use as the source. Converted so precision may be lost.</param>
        public HSL (IColorSpace Other)
        {
            if (Other == null)
                throw new ArgumentNullException("Other");
            HSL Temp = Other.ToRGB().ToHSL();
            CommonInitialization(Temp.H, Temp.S, Temp.L);
        }

        /// <summary>
        /// Initialization common to all components.
        /// </summary>
        /// <param name="InitialH">Initial hue component.</param>
        /// <param name="InitialS">Initial saturation component.</param>
        /// <param name="InitialL">Initial luminance component.</param>
        private void CommonInitialization (double InitialH, double InitialS, double InitialL)
        {
            ID = Guid.NewGuid();
            Name = "";
            ColorSpace = ColorSpaces.HSL;
            H = InitialH;
            S = InitialS;
            L = InitialL;
            CanConvertTo = new HashSet<ColorSpaces>();
            CanConvertTo.Add(ColorSpaces.RGB);
        }

        /// <summary>
        /// Get a set of what this class can convert to.
        /// </summary>
        public HashSet<ColorSpaces> CanConvertTo { get; internal set; }

        /// <summary>
        /// Get or set the color's ID.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Get or set the color's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get the color space.
        /// </summary>
        public ColorSpaces ColorSpace { get; internal set; }

        /// <summary>
        /// Returns a list of component values in H, S, L order.
        /// </summary>
        /// <returns>List of component values.</returns>
        public List<double> GetValues ()
        {
            List<double> v = new List<double>();
            v.Add(H);
            v.Add(S);
            v.Add(L);
            return v;
        }

        /// <summary>
        /// Determines equality between this instance and <paramref name="OtherHSL"/>.
        /// </summary>
        /// <param name="OtherHSL">The other instance to compare to this.</param>
        /// <returns>True if <paramref name="OtherHSL"/> equals this instance, false if not.</returns>
        public bool Equals (HSL OtherHSL)
        {
            if (OtherHSL == null)
                return false;
            return (OtherHSL.H == this.H && OtherHSL.S == this.S && OtherHSL.L == this.L);
        }

        /// <summary>
        /// Determines equality between this instance and potentially another HSL instance.
        /// </summary>
        /// <param name="Other">The other color space to compare to this one.</param>
        /// <returns>True if <paramref name="Other"/> equals this instance, false if not.</returns>
        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as HSL);
        }

        /// <summary>
        /// Get the color space label.
        /// </summary>
        public string ColorLabel
        {
            get
            {
                return StaticColorLabel;
            }
        }

        /// <summary>
        /// Get the static color space label.
        /// </summary>
        internal static string StaticColorLabel
        {
            get
            {
                return "HSL";
            }
        }

        /// <summary>
        /// Returns the format description.
        /// </summary>
        /// <returns></returns>
        public string FormatDescription ()
        {
            return "";
        }

        double _H = 0.0;
        /// <summary>
        /// Get or set the hue component. Clamped to 0.0 to 360.0.
        /// </summary>
        public double H
        {
            get
            {
                return _H;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 360.0)
                    value = 360.0;
                _H = value;
            }
        }

        /// <summary>
        /// Get or set the hue component. Clamped to 0.0 to 360.0.
        /// </summary>
        public double Hue
        {
            get
            {
                return H;
            }
            set
            {
                H = value;
            }
        }

        double _S = 0.0;
        /// <summary>
        /// Get or set the saturation component. Clamped to 0.0 to 1.0.
        /// </summary>
        public double S
        {
            get
            {
                return _S;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _S = value;
            }
        }

        /// <summary>
        /// Get or set the saturation component. Clamped to 0.0 to 1.0.
        /// </summary>
        public double Saturation
        {
            get
            {
                return S;
            }
            set
            {
                S = value;
            }
        }

        /// <summary>
        /// Get or set the luminance component. Clamped to 0.0 to 1.0.
        /// </summary>
        double _L = 0.0;
        public double L
        {
            get
            {
                return _L;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _L = value;
            }
        }

        /// <summary>
        /// Get or set the luminance component. Clamped to 0.0 to 1.0.
        /// </summary>
        public double Luminance
        {
            get
            {
                return L;
            }
            set
            {
                L = value;
            }
        }

        /// <summary>
        /// Returns the contents of the instance formatted as a string ready for input.
        /// </summary>
        /// <returns></returns>
        public string ToInputString ()
        {
            return ToString();
        }

        /// <summary>
        /// Used for debugging.
        /// </summary>
        public string NiceHSL
        {
            get
            {
                return ToString();
            }
        }

        /// <summary>
        /// Return the contents of the instance as a string.
        /// </summary>
        /// <returns>Contents as a string.</returns>
        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(H.ToString("n2"));
            sb.Append("°, ");
            sb.Append(S.ToString("n2"));
            sb.Append(", ");
            sb.Append(L.ToString("n2"));
            return sb.ToString();
        }

        /// <summary>
        /// Conver the current HSL value to RGB.
        /// </summary>
        /// <returns>RGB equivalent of the current HSL value.</returns>
        public RGB ToRGB ()
        {
            double scR = 0.0;
            double scG = 0.0;
            double scB = 0.0;
            double C = (1 - Math.Abs((2 * (L - 1)))) * S;
            double X = C * (1 - Math.Abs((double)((int)(H / 60.0) % 2 - 1)));
            double m = L - (C / 2.0);
            if ((H <= 0.0) && (H < 60.0))
            {
                scR = C + m;
                scG = X + m;
                scB = m;
            }
            if ((H <= 60.0) && (H < 120.0))
            {
                scR = X + m;
                scG = C + m;
                scB = 0 + m;
            }
            if ((H <= 120.0) && (H < 180.0))
            {
                scR = 0 + m;
                scG = C + m;
                scB = X + m;
            }
            if ((H <= 180.0) && (H < 240.0))
            {
                scR = 0 + m;
                scG = X + m;
                scB = C + m;
            }
            if ((H < 240.0) && (H < 300.0))
            {
                scR = X + m;
                scG = 0 + m;
                scB = C + m;
            }
            if ((H <= 300.0) && (H < 360.0))
            {
                scR = C + m;
                scG = 0 + m;
                scB = X + m;
            }
            RGB IsARGB = new RGB(1.0, scR, scG, scB);
            return IsARGB;
        }

        /// <summary>
        /// Return the color structure equivalent of the current HSL color.
        /// </summary>
        /// <returns>Color structure equivalent of the current color.</returns>
        public Color ToColor ()
        {
            return ToRGB().ToRGBColor();
        }

        /// <summary>
        /// Return the luminance of the passed RGB color.
        /// </summary>
        /// <param name="R">Red channel.</param>
        /// <param name="G">Green channel.</param>
        /// <param name="B">Blue channel.</param>
        /// <returns>Luminance calculated from the passed color.</returns>
        public static double ToLuminance (double R, double G, double B)
        {
            HSL hsl = new HSL(new RGB(1.0, R, G, B));
            return hsl.L;
        }

        /// <summary>
        /// Return the luminance of the passed RGB color.
        /// </summary>
        /// <param name="RGBColor">The color whose luminance will be returned.</param>
        /// <returns>Luminance calculated from the passed color.</returns>
        public static double ToLuminance (Color RGBColor)
        {
            return ToLuminance((double)RGBColor.R / 255.0, (double)RGBColor.G / 255.0, (double)RGBColor.B / 255.0);
        }

        /// <summary>
        /// Return the luminance of the passed RGB color.
        /// </summary>
        /// <param name="RGBColor">The color whose luminance will be returned.</param>
        /// <returns>Luminance calculated from the passed color.</returns>
        public static double ToLuminance (RGB RGBColor)
        {
            return RGBColor.ToHSL().L;
        }

        /// <summary>
        /// Return the saturation of the passed RGB color.
        /// </summary>
        /// <param name="R">Red channel.</param>
        /// <param name="G">Green channel.</param>
        /// <param name="B">Blue channel.</param>
        /// <returns>Saturation calculated from the passed color.</returns>
        public static double ToSaturation (double R, double G, double B)
        {
            HSL hsl = new HSL(new RGB(1.0, R, G, B));
            return hsl.S;
        }

        /// <summary>
        /// Return the saturation of the passed RGB color.
        /// </summary>
        /// <param name="RGBColor">The color whose luminance will be returned.</param>
        /// <returns>Saturation calculated from the passed color.</returns>
        public static double ToSaturation (Color RGBColor)
        {
            return ToSaturation((double)RGBColor.R / 255.0, (double)RGBColor.G / 255.0, (double)RGBColor.B / 255.0);
        }

        /// <summary>
        /// Return the saturation of the passed RGB color.
        /// </summary>
        /// <param name="RGBColor">The color whose luminance will be returned.</param>
        /// <returns>Saturation calculated from the passed color.</returns>
        public static double ToSaturation (RGB RGBColor)
        {
            return RGBColor.ToHSL().S;
        }

        /// <summary>
        /// Return the hue of the passed RGB color.
        /// </summary>
        /// <param name="R">Red channel.</param>
        /// <param name="G">Green channel.</param>
        /// <param name="B">Blue channel.</param>
        /// <returns>Hue calculated from the passed color.</returns>
        public static double ToHue (double R, double G, double B)
        {
            HSL hsl = new HSL(new RGB(1.0, R, G, B));
            return hsl.H;
        }

        /// <summary>
        /// Return the hue of the passed RGB color.
        /// </summary>
        /// <param name="RGBColor">The color whose luminance will be returned.</param>
        /// <returns>Hue calculated from the passed color.</returns>
        public static double ToHue (Color RGBColor)
        {
            return ToHue((double)RGBColor.R / 255.0, (double)RGBColor.G / 255.0, (double)RGBColor.B / 255.0);
        }

        /// <summary>
        /// Return the hue of the passed RGB color.
        /// </summary>
        /// <param name="RGBColor">The color whose luminance will be returned.</param>
        /// <returns>Hue calculated from the passed color.</returns>
        public static double ToHue (RGB RGBColor)
        {
            return RGBColor.ToHSL().H;
        }

        /// <summary>
        /// Change the hue of the passed RGB color and return a new color.
        /// </summary>
        /// <param name="RawRGB">Color to change.</param>
        /// <param name="NewSaturation">New saturation value.</param>
        /// <returns>Adjusted color.</returns>
        public static Color SetHue (Color RawRGB, double NewHue)
        {
            HSL hsl = new RGB((double)RawRGB.R / 255.0, (double)RawRGB.G / 255.0, (double)RawRGB.B / 255.0).ToHSL();
            hsl.H = NewHue;
            return hsl.ToColor();
        }

        /// <summary>
        /// Change the hue of the passed RGB color/color space and return a new RGB color/color space.
        /// </summary>
        /// <param name="Value">The value to convert.</param>
        /// <param name="NewHue">The new hue.</param>
        /// <returns>Adjusted RGB color/color space.</returns>
        public static RGB SetHue (RGB Value, double NewHue)
        {
            if (Value == null)
                throw new ArgumentNullException("Value");
            HSL hsl = Value.ToHSL();
            hsl.H = NewHue;
            return hsl.ToRGB();
        }

        /// <summary>
        /// Change the saturation of the passed RGB color and return a new color.
        /// </summary>
        /// <param name="RawRGB">Color to change.</param>
        /// <param name="NewSaturation">New saturation value.</param>
        /// <returns>Adjusted color.</returns>
        public static Color SetSaturation (Color RawRGB, double NewSaturation)
        {
            HSL hsl = new RGB((double)RawRGB.R / 255.0, (double)RawRGB.G / 255.0, (double)RawRGB.B / 255.0).ToHSL();
            hsl.S = NewSaturation;
            return hsl.ToColor();
        }

        /// <summary>
        /// Change the saturation of the passed RGB color/color space and return a new RGB color/color space.
        /// </summary>
        /// <param name="Value">The value to convert.</param>
        /// <param name="NewSaturation">The new saturation.</param>
        /// <returns>Adjusted RGB color/color space.</returns>
        public static RGB SetSaturation (RGB Value, double NewSaturation)
        {
            if (Value == null)
                throw new ArgumentNullException("Value");
            HSL hsl = Value.ToHSL();
            hsl.S = NewSaturation;
            return hsl.ToRGB();
        }

        /// <summary>
        /// Change the luminance of the passed RGB color and return a new color.
        /// </summary>
        /// <param name="RawRGB">Color to change.</param>
        /// <param name="NewSaturation">New saturation value.</param>
        /// <returns>Adjusted color.</returns>
        public static Color SetLuminance (Color RawRGB, double NewLuminance)
        {
            HSL hsl = new RGB((double)RawRGB.R / 255.0, (double)RawRGB.G / 255.0, (double)RawRGB.B / 255.0).ToHSL();
            hsl.L = NewLuminance;
            return hsl.ToColor();
        }

        /// <summary>
        /// Change the lumination of the passed RGB color/color space and return a new RGB color/color space.
        /// </summary>
        /// <param name="Value">The value to convert.</param>
        /// <param name="NewLumination">The new lumination.</param>
        /// <returns>Adjusted RGB color/color space.</returns>
        public static RGB SetLumination (RGB Value, double NewLumination)
        {
            if (Value == null)
                throw new ArgumentNullException("Value");
            HSL hsl = Value.ToHSL();
            hsl.L = NewLumination;
            return hsl.ToRGB();
        }

        /// <summary>
        /// Try to parse a string that may contain HSL values in the format of H, S, L.
        /// </summary>
        /// <param name="Raw">The string that may contain HSL values.</param>
        /// <param name="Final">On success, will contain the RGB color value equiavalent of the converted string.</param>
        /// <returns>True on success, false on failure. If false is returned, <paramref name="Final"/> is undefined.</returns>
        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            HSL SHSL = new HSL();
            bool OK = TryParse(Raw, out SHSL);
            if (!OK)
                return false;
            Final = SHSL.ToRGBColor();
            return true;
        }

        /// <summary>
        /// Try to parse a string that may contains HSL values in the format of H, S, L.
        /// </summary>
        /// <param name="Raw">The string that may contain HSL values.</param>
        /// <param name="Final">On success, will contain a new HSL instantiation initialized with the HSL equivalent of the converted values.</param>
        /// <returns>True on success, false on failure. If false is returned, <paramref name="Final"/> is undefined.</returns>
        public bool TryParse (string Raw, out HSL Final)
        {
            Final = null;
            if (string.IsNullOrEmpty(Raw))
                return false;
            double NewH = 0.0;
            double NewS = 0.0;
            double NewL = 0.0;
            bool OK = IroColorSpace.TryParse3Double(Raw, out NewH, out NewS, out NewL);
            if (!OK)
                return false;
            Final = new HSL(NewH, NewS, NewL);
            return true;
        }

        /// <summary>
        /// Return the RGB color equivalent to the HSL color.
        /// </summary>
        /// <returns>Color struture.</returns>
        public Color ToRGBColor ()
        {
            RGB RGB = this.ToRGB();
            return RGB.ToRGBColor();
        }

        /// <summary>
        /// Parse the contents of <paramref name="Raw"/> and set this instance to that color.
        /// </summary>
        /// <param name="Raw">The string to parse.</param>
        /// <returns>True on success, false on failure. If false is returned the color of this instance is not changed.</returns>
        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            HSL LocalHSL = new HSL();
            bool OK = TryParse(Raw, out LocalHSL);
            if (OK)
            {
                this.H = LocalHSL.H;
                this.S = LocalHSL.S;
                this.L = LocalHSL.L;
            }
            return OK;
        }
    }
}
