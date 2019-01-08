using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace Iro3.Data.ColorSpaces
{
    /// <summary>
    /// Encapsulates an RGB color space color.
    /// </summary>
    [DebuggerDisplay("{NiceRGB}")]
    public class RGB : IEquatable<RGB>, IColorSpace
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RGB ()
        {
            CommonInitialization(0, 0, 0, 0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Raw">32-bit value used to initialize the channels.</param>
        public RGB (UInt32 Raw)
        {
            A = (byte)((Raw & 0xff000000) >> 24);
            R = (byte)((Raw & 0x00ff0000) >> 16);
            G = (byte)((Raw & 0x0000ff00) >> 8);
            B = (byte)((Raw & 0x000000ff) >> 0);
            CommonInitialization(A, R, G, B);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="OtherRGB">Another ARGB instantiation whose data will be used to initialize this instantiation.</param>
        public RGB (RGB OtherRGB)
        {
            if (OtherRGB == null)
                throw new ArgumentNullException("OtherRGB");
            CommonInitialization(OtherRGB.A, OtherRGB.R, OtherRGB.G, OtherRGB.B);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="A">Initial alpha value.</param>
        /// <param name="R">Initial red value.</param>
        /// <param name="G">Initial green value.</param>
        /// <param name="B">Initial blue value.</param>
        public RGB (byte A, byte R, byte G, byte B)
        {
            CommonInitialization(A, R, G, B);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        /// The alpha channel is set to 0xff.
        /// </remarks>
        /// <param name="R">Initial red value.</param>
        /// <param name="G">Initial green value.</param>
        /// <param name="B">Initial blue value.</param>
        public RGB (byte R, byte G, byte B)
        {
            CommonInitialization(0xff, R, G, B);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        /// Using this constructor and the double versions of the channel values results in better colors.
        /// </remarks>
        /// <param name="scA">Initial alpha value, clamped to 0.0 to 1.0.</param>
        /// <param name="scR">Initial red value, clamped to 0.0 to 1.0.</param>
        /// <param name="scG">Initial green value, clamped to 0.0 to 1.0.</param>
        /// <param name="scB">Initial blue value, clamped to 0.0 to 1.0.</param>
        public RGB (double scA, double scR, double scG, double scB)
        {
            byte iA = (byte)(scA.Clamp(0.0, 1.0) * 255.0);
            byte iR = (byte)(scR.Clamp(0.0, 1.0) * 255.0);
            byte iG = (byte)(scG.Clamp(0.0, 1.0) * 255.0);
            byte iB = (byte)(scB.Clamp(0.0, 1.0) * 255.0);
            CommonInitialization(iA, iR, iG, iB);
            //Reassign the channels but to the double versions.
            this.scA = scA;
            this.scR = scR;
            this.scG = scG;
            this.scB = scB;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        /// Using this constructor and the double versions of the channel values results in better colors.
        /// The alpha channel is set to 1.0;
        /// </remarks>
        /// <param name="scR">Initial red value, clamped to 0.0 to 1.0.</param>
        /// <param name="scG">Initial green value, clamped to 0.0 to 1.0.</param>
        /// <param name="scB">Initial blue value, clamped to 0.0 to 1.0.</param>
        public RGB (double scR, double scG, double scB)
        {
            byte iA = 0xff;
            byte iR = (byte)(scR.Clamp(0.0, 1.0) * 255.0);
            byte iG = (byte)(scG.Clamp(0.0, 1.0) * 255.0);
            byte iB = (byte)(scB.Clamp(0.0, 1.0) * 255.0);
            CommonInitialization(iA, iR, iG, iB);
            //Reassign the channels but to the double versions.
            this.scA = 1.0;
            this.scR = scR;
            this.scG = scG;
            this.scB = scB;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Source">Color struct that will be used to initialize this instance.</param>
        public RGB (Color Source)
        {
            CommonInitialization(Source.A, Source.R, Source.G, Source.B);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Other">Other color space color that is the source for this instance.</param>
        public RGB (IColorSpace Other)
        {
            if (Other == null)
                throw new ArgumentNullException("Other");
            RGB Scratch = Other.ToRGB();
            CommonInitialization(0, 0, 0, 0);
            scA = Scratch.scA;
            scR = Scratch.scR;
            scG = Scratch.scG;
            scB = Scratch.scB;
        }

        /// <summary>
        /// Initialization common to all constructors.
        /// </summary>
        /// <param name="InitialA">Initial alpha value.</param>
        /// <param name="InitialR">Initial red value.</param>
        /// <param name="InitialG">Initial green value.</param>
        /// <param name="InitialB">Initial blue value.</param>
        private void CommonInitialization (byte InitialA, byte InitialR, byte InitialG, byte InitialB)
        {
            ColorSpace = ColorSpaces.RGB;
            ID = Guid.NewGuid();
            Name = "";
            A = InitialA;
            R = InitialR;
            G = InitialG;
            B = InitialB;
            CanConvertTo = new HashSet<ColorSpaces>();
            CanConvertTo.Add(ColorSpaces.CIELab);
            CanConvertTo.Add(ColorSpaces.CMY);
            CanConvertTo.Add(ColorSpaces.CMYK);
            CanConvertTo.Add(ColorSpaces.HSL);
            CanConvertTo.Add(ColorSpaces.HSV);
            CanConvertTo.Add(ColorSpaces.XYZ);
            CanConvertTo.Add(ColorSpaces.YCbCr);
            CanConvertTo.Add(ColorSpaces.YIQ);
            CanConvertTo.Add(ColorSpaces.YUV);
            CanConvertTo.Add(ColorSpaces.TSL);
        }

        /// <summary>
        /// Get the color space.
        /// </summary>
        public ColorSpaces ColorSpace { get; internal set; }

        /// <summary>
        /// Get or set the ID of the color.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Get or set the name of the color.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get a set of color spaces that this class can convert to.
        /// </summary>
        public HashSet<ColorSpaces> CanConvertTo { get; internal set; }

        /// <summary>
        /// Determines equality between this instance and another instance of the RGB class. Compares the byte color channels.
        /// </summary>
        /// <param name="OtherRGB">The other instance to compare to this instance.</param>
        /// <returns>True if all byte channels are the same between the two instances, false if not.</returns>
        public bool Equals (RGB OtherRGB)
        {
            if (OtherRGB == null)
                return false;
            return (OtherRGB.A == this.A && OtherRGB.R == this.R && OtherRGB.G == this.G && OtherRGB.B == this.B);
        }

        /// <summary>
        /// Determines equality between the other color space and this instance.
        /// </summary>
        /// <param name="Other">The other color space to compare to this color space.</param>
        /// <returns>True if the color space values are equal, false if not.</returns>
        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as RGB);
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
        /// Get the color space label.
        /// </summary>
        internal static string StaticColorLabel
        {
            get
            {
                return "RGB";
            }
        }

        private byte _A = 0;
        /// <summary>
        /// Get or set the alpha byte channel.
        /// </summary>
        public byte A
        {
            get
            {
                return _A;
            }
            set
            {
                _A = value;
                _scA = (value / 255.0).Clamp(0.0, 1.0);
            }
        }

        /// <summary>
        /// Alias for <seealso cref="A"/>.
        /// </summary>
        public byte Alpha
        {
            get
            {
                return A;
            }
            set
            {
                A = value;
            }
        }

        private byte _R = 0;
        /// <summary>
        /// Get or set the red byte channel.
        /// </summary>
        public byte R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
                _scR = (value / 255.0).Clamp(0.0, 1.0);
            }
        }

        /// <summary>
        /// Alias for <seealso cref="R"/>.
        /// </summary>
        public byte Red
        {
            get
            {
                return R;
            }
            set
            {
                R = value;
            }
        }

        private byte _G = 0;
        /// <summary>
        /// Get or set the green byte channel.
        /// </summary>
        public byte G
        {
            get
            {
                return _G;
            }
            set
            {
                _G = value;
                _scG = (value / 255.0).Clamp(0.0, 1.0);
            }
        }

        /// <summary>
        /// Alias for <seealso cref="G"/>.
        /// </summary>
        public byte Green
        {
            get
            {
                return G;
            }
            set
            {
                G = value;
            }
        }

        private byte _B = 0;
        /// <summary>
        /// Get or set the blue byte channel level.
        /// </summary>
        public byte B
        {
            get
            {
                return _B;
            }
            set
            {
                _B = value;
                _scB = (value / 255.0).Clamp(0.0, 1.0);
            }
        }

        /// <summary>
        /// Alias for <seealso cref="B"/>.
        /// </summary>
        public byte Blue
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }

        private double _scA = 0.0;
        /// <summary>
        /// Get or set the alpha double channel.
        /// </summary>
        public double scA
        {
            get
            {
                return _scA;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _scA = value;
                A = (byte)(255.0 * value);
            }
        }

        private double _scR = 0.0;
        /// <summary>
        /// Get or set the red double channel.
        /// </summary>
        public double scR
        {
            get
            {
                return _scR;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _scR = value;
                R = (byte)(255.0 * value);
            }
        }

        private double _scG = 0.0;
        /// <summary>
        /// Get or set the green double channel.
        /// </summary>
        public double scG
        {
            get
            {
                return _scG;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _scG = value;
                G = (byte)(255.0 * value);
            }
        }

        private double _scB = 0.0;
        /// <summary>
        /// Get or set the blue double channel.
        /// </summary>
        public double scB
        {
            get
            {
                return _scB;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _scB = value;
                B = (byte)(255.0 * value);
            }
        }

        /// <summary>
        /// Return a string representation of the contained value in the form a, r, g, b where each channel is a decimal number.
        /// </summary>
        /// <returns>String representation of the contained value.</returns>
        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(A.ToString());
            sb.Append(", ");
            sb.Append(R.ToString());
            sb.Append(", ");
            sb.Append(G.ToString());
            sb.Append(", ");
            sb.Append(B.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Return a string representation of the contained value in the specified format.
        /// </summary>
        /// <param name="IsHex">If true, hex values will be returned. Hex values are preceeded by '#'.</param>
        /// <param name="ByChannels">If true, channels are returned individually, if false, a 32-bit number is returned.</param>
        /// <returns>String representation of the contained value formatted as directed.</returns>
        public string ToString (bool IsHex, bool ByChannels)
        {
            if (!IsHex && ByChannels)
                return ToString();
            StringBuilder sb = new StringBuilder();
            if (ByChannels)
            {
                sb.Append("#");
                sb.Append(A.ToString("x2"));
                sb.Append(", ");
                sb.Append("#");
                sb.Append(R.ToString("x2"));
                sb.Append(", ");
                sb.Append("#");
                sb.Append(G.ToString("x2"));
                sb.Append(", ");
                sb.Append("#");
                sb.Append(B.ToString("x2"));
            }
            else
            {
                UInt32 v = (UInt32)((A << 24) | (R << 16) | (G << 8) | (B << 0));
                sb.Append("#");
                sb.Append(v.ToString("x8"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Used for debugging.
        /// </summary>
        public string NiceRGB
        {
            get
            {
                return ToString(true, false);
            }
        }

        /// <summary>
        /// Returns a string that can be used for input.
        /// </summary>
        /// <returns></returns>
        public string ToInputString ()
        {
            return ToString(true, false);
        }

        /// <summary>
        /// Return the HSL equivalent to the RGB color space value in this instance.
        /// </summary>
        /// <returns>HSL equivalent to the current RGB value.</returns>
        public HSL ToHSL ()
        {
#if true
            double H = 0;
            double S = 0;
            double L = 0;
            double Max = Math.Max(R, Math.Max(G, B));
            double Min = Math.Min(R, Math.Min(G, B));
            double Delta = Max - Min;
            if (Delta == 0)
            {
                H = 0;
            }
            else
            {
                if (R == Max)
                {
                    if (G >= B)
                        H = ((G - B) / Delta);
                }
                if (G == Max)
                {
                    H = ((B - R) / Delta) + 2;
                }
                if (B == Max)
                {
                    H = ((R - G) / Delta) + 4;
                }
            }
            H *= 60.0;
            L = (Max + Min) / 2;
            if (Delta == 0.0)
            {
                S = 0.0;
            }
            else
            {
                S = Delta / (1 - Math.Abs(2 * L - 1));
            }
            return new HSL(H, S, L);
#else
            double Max = Math.Max(scR, Math.Max(scG, scB));
            double Min = Math.Min(scR, Math.Min(scG, scB));
            double Delta = Max - Min;
            double H, S, L;

            if (Max == R)
            {
                H = 60.0 * (double)((int)((G - B) / Delta) % 6);
            }
            else
                if (Max == G)
            {
                H = 60.0 * (((B - R) / Delta) + 2);
            }
            else
            {
                H = 60.0 * (((R - G) / Delta) + 4);
            }
            L = (Max + Min) / 2;
            if (Delta == 0.0)
            {
                S = 0.0;
            }
            else
            {
                S = Delta / (1 - Math.Abs(2 * L - 1));
            }
            return new HSL(H, S, L);
#endif
        }

        /// <summary>
        /// Return the HSV equivalent of the contain value.
        /// </summary>
        /// <returns>HSV equivalent of the contained value.</returns>
        public HSV ToHSV ()
        {
            double Min = Math.Min(R, Math.Min(G, B));
            double Max = Math.Max(R, Math.Max(G, B));
            double V = Max;
            double H, S;
            double Delta = Max - Min;
            if (Max != 0.0)
                S = Delta / Max;
            else
            {
                //Black
                S = 0.0;
                H = -1;
                return new HSV(H, S, V);
            }
            if (R == Max)
                H = (G - B) / Delta;
            else
                if (G == Max)
                H = 2 + ((B - R) / Delta);
            else
                H = 4 + ((R - G) / Delta);
            H *= 60.0;
            if (H < 0.0)
                H += 360.0;
            return new HSV(H, S, V);
        }

        /// <summary>
        /// Return the YUV equivalent of the contain value.
        /// </summary>
        /// <returns>YUV equivalent of the contained value.</returns>
        public YUV ToYUV ()
        {
            double Y = (scR * 0.299) + (scG * 0.587) + (scB * 0.114);
            double U = (scR * -0.14713) + (scG * -0.28886) + (scB * 0.436);
            double V = (scR * 0.615) + (scG * -0.51499) + (scB * -0.10001);
            return new YUV(Y, U, V);
        }

        /// <summary>
        /// Return the YIQ equivalent of the contain value.
        /// </summary>
        /// <returns>YIQ equivalent of the contained value.</returns>
        public YIQ ToYIQ ()
        {
            double Y = (0.299 * scR) + (0.587 * scG) + (0.114 * scB);
            double I = (0.596 * scR) - (0.275 * scG) - (0.321 * scB);
            double Q = (0.212 * scR) - (0.523 * scG) + (0.311 * scB);
            return new YIQ(Y, I, Q);
        }

        /// <summary>
        /// Return the YCbCr equivalent of the contain value.
        /// </summary>
        /// <returns>YCbCr equivalent of the contained value.</returns>
        public YCbCr ToYCbCr ()
        {
            double Y = (double)(scR * (77 / 255)) + (scG * (150 / 255)) + (scB * (29 / 255));
            double Cb = (double)(scR * (44 / 255)) - (scG * (87 / 255)) + (scB * (131 / 255)) + 128;
            double Cr = (double)(scR * (131 / 255)) - (scG * (110 / 255)) - (scB * (21 / 255)) + 128;
            return new YCbCr(Y, Cb, Cr);
        }

        /// <summary>
        /// Return the XYZ equivalent of the contain value.
        /// </summary>
        /// <returns>XYZ equivalent of the contained value.</returns>
        public XYZ ToXYZ ()
        {
            double X = (scR * 0.412456) + (scG * 0.357580) + (scB * 0.180423);
            double Y = (scR * 0.212671) + (scG * 0.715160) + (scB * 0.072169);
            double Z = (scR * 0.019334) + (scG * 0.119193) + (scB * 0.950227);
            return new XYZ(X, Y, Z);
        }

        /// <summary>
        /// Return the CMY equivalent of the contain value.
        /// </summary>
        /// <returns>CMY equivalent of the contained value.</returns>
        public CMY ToCMY ()
        {
            double C = 1.0 - scR;
            double M = 1.0 - scG;
            double Y = 1.0 - scB;
            return new CMY(C, M, Y);
        }

        /// <summary>
        /// Return the CMYK equivalent of the contain value.
        /// </summary>
        /// <returns>CMKY equivalent of the contained value.</returns>
        public CMYK ToCMYK ()
        {
            CMY IsCMY = ToCMY();
            return IsCMY.ToCMYK();
        }

        /// <summary>
        /// Return the CIE Lab equivalent of the contain value.
        /// </summary>
        /// <returns>CIE Lab equivalent of the contained value.</returns>
        public CIELab ToCIELab ()
        {
            XYZ IsXYZ = ToXYZ();
            return IsXYZ.ToCIELab();
        }

        /// <summary>
        /// Return the TSL equivalent of the contain value.
        /// </summary>
        /// <returns>TSL equivalent of the contained value.</returns>
        public TSL ToTSL ()
        {
            TSL IsTSL = new TSL();

            double rp = scR - (1.0 / 3.0);
            double gp = scG - (1.0 / 3.0);
            double r = scR / (scR + scG + scB);
            double g = scG / (scR + scG + scB);

            if (gp > 0)
            {
                IsTSL.T = (1.0 / (2.0 * Math.PI)) * Math.Atan((rp / gp) + (1.0 / 4.0));
            }
            else
            {
                if (gp < 0)
                {
                    IsTSL.T = (1.0 / (2.0 * Math.PI)) * Math.Atan((rp / gp) + (3.0 / 4.0));
                }
                else
                if (gp == 0.0)
                    IsTSL.T = 0.0;
            }
            IsTSL.S = Math.Sqrt((9.0 / 5.0) * ((rp * rp) + (gp * gp)));
            IsTSL.L = (scR * 0.299) + (scG * 0.587) + (scB * 0.114);

            return IsTSL;
        }

        /// <summary>
        /// Converts the color in this instance to the color space indicated by <paramref name="OtherSpace"/>.
        /// </summary>
        /// <param name="OtherSpace">The color space to convert this color to.</param>
        /// <returns>New color space with the equivalent color of this color space.</returns>
        public IColorSpace ToOther (ColorSpaces OtherSpace)
        {
            switch (OtherSpace)
            {
                case ColorSpaces.CIELab:
                    return ToCIELab();

                case ColorSpaces.CMY:
                    return ToCMY();

                case ColorSpaces.CMYK:
                    return ToCMYK();

                case ColorSpaces.HSL:
                    return ToHSL();

                case ColorSpaces.HSV:
                    return ToHSV();

                case ColorSpaces.RGB:
                    return this;

                case ColorSpaces.TSL:
                    return ToTSL();

                case ColorSpaces.XYZ:
                    return ToXYZ();

                case ColorSpaces.YCbCr:
                    return ToYCbCr();

                case ColorSpaces.YIQ:
                    return ToYIQ();

                case ColorSpaces.YUV:
                    return ToYUV();

                default:
                    throw new InvalidOperationException("Invalid color space.");
            }
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

            if (Raw.Length >= 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    Raw = "#" + Raw.Substring(2);
                }
            }

            if (Raw[0] != '#')
            {
                string RawName = Raw.ToUpper();
                RawName = RawName.Replace(" ", "");
                if (IroColorSpace.UColorNameExists(RawName))
                {
                    Final = IroColorSpace.GetUColorValue(RawName);
                    return true;
                }
                return false;
            }

            bool OK = false;
            UInt32 IVal = 0;
            //Check for the presence of commas or other separators
            if (Raw.Contains(",") || Raw.Contains(" ") || Raw.Contains("/"))
            {
                string[] channels = Raw.Split(new char[] { ',', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (channels.Length < 3 || channels.Length > 4)
                    return false;
                if (channels.Length == 3)
                {
                    Final.A = 0xff;
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[0], out IVal);
                    if (!OK)
                        return false;
                    Final.R = (byte)IVal;
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[1], out IVal);
                    if (!OK)
                        return false;
                    Final.G = (byte)IVal;
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[2], out IVal);
                    if (!OK)
                        return false;
                    Final.B = (byte)IVal;
                    return true;
                }
                else
                {
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[0], out IVal);
                    if (!OK)
                        return false;
                    Final.A = (byte)IVal;
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[1], out IVal);
                    if (!OK)
                        return false;
                    Final.R = (byte)IVal;
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[2], out IVal);
                    if (!OK)
                        return false;
                    Final.G = (byte)IVal;
                    OK = IroColorSpace.TryParsePossibleHexValue(channels[3], out IVal);
                    if (!OK)
                        return false;
                    Final.B = (byte)IVal;
                    return true;
                }
            }

            OK = IroColorSpace.TryParsePossibleHexValue(Raw, out IVal);
            if (!OK)
                return false;
            Final.A = (byte)((IVal & 0xff000000) >> 24);
            Final.R = (byte)((IVal & 0x00ff0000) >> 16);
            Final.G = (byte)((IVal & 0x0000ff00) >> 8);
            Final.B = (byte)((IVal & 0x000000ff) >> 0);

            return true;
        }

        /// <summary>
        /// Attempt to parse the string in <paramref name="Raw"/> and return the result in <paramref name="Final"/>.
        /// </summary>
        /// <param name="Raw">The string to parse.</param>
        /// <param name="Final">On success, will contain the RGB value based on the contents of <paramref name="Raw"/>.</param>
        /// <returns>True on success, false on failure. If false is returned, <paramref name="Final"/> is undefined.</returns>
        public bool TryParse (string Raw, out RGB Final)
        {
            Final = null;
            Color ParsedColor = Colors.Transparent;
            bool ParsedOK = TryParse(Raw, out ParsedColor);
            if (!ParsedOK)
                return false;
            Final = new RGB(ParsedColor);
            return true;
        }

        /// <summary>
        /// Return a list of raw values in A, R, G, B order. The values are all clamped from 0.0 to 1.0.
        /// </summary>
        /// <returns>List of channel data.</returns>
        public List<double> GetValues ()
        {
            List<double> v = new List<double>();
            v.Add(scA);
            v.Add(scR);
            v.Add(scG);
            v.Add(scB);
            return v;
        }

        /// <summary>
        /// Return a description of the understood formats.
        /// </summary>
        /// <returns>Understood formats.</returns>
        public string FormatDescription ()
        {
            StringBuilder sb = new StringBuilder("RGB");
            sb.Append(Environment.NewLine);
            sb.Append("Hex value: '#rrggbb' or '#aarrggbb'");
            sb.Append(Environment.NewLine);
            sb.Append("Name value: Standard color name (case insensitive, no internal spaces)");
            sb.Append(Environment.NewLine);
            sb.Append("Example with name: SkyBlue");
            sb.Append(Environment.NewLine);
            sb.Append("Example with alpha: #ff00ff00");
            sb.Append(Environment.NewLine);
            sb.Append("Example no alpha: #ff80e0");
            sb.Append(Environment.NewLine);
            sb.Append("Example by channels: 100, 255, 25");
            sb.Append(Environment.NewLine);
            sb.Append("Example by channels with alpha: 255, 88, 200, 192");
            sb.Append(Environment.NewLine);
            sb.Append("Example by hex channels: #d0, #52, #ff");
            sb.Append(Environment.NewLine);
            sb.Append("Example by hex channels with alpha: #de #ad #be #ef");
            return sb.ToString();
        }

        /// <summary>
        /// Return a Color structure from the current values.
        /// </summary>
        /// <returns>Color structure set to the current values.</returns>
        public Color ToRGBColor ()
        {
            return Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        /// Return a new copy of this class, shallowly copied.
        /// </summary>
        /// <returns></returns>
        public RGB ToRGB ()
        {
            return new RGB(this);
        }

        /// <summary>
        /// Parse a raw string and set the channel values if valid.
        /// </summary>
        /// <param name="Raw">The string to parse.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            RGB LocalRGB = new RGB();
            bool OK = TryParse(Raw, out LocalRGB);
            if (OK)
            {
                scA = LocalRGB.scA;
                scR = LocalRGB.scR;
                scG = LocalRGB.scG;
                scB = LocalRGB.scB;
            }
            return OK;
        }
    }
}
