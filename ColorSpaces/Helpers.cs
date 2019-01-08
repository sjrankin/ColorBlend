using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    //http://www.easyrgb.com/index.php?X=MATH&H=14#text14
    public partial class IroColorSpace
    {
        /// <summary>
        /// Parse a string that may have three double values.
        /// </summary>
        /// <remarks>
        /// Values may be separated by spaces, commas, semi-colons, slashes, and degree signs.
        /// </remarks>
        /// <param name="Raw">The raw string.</param>
        /// <param name="D1">First double.</param>
        /// <param name="D2">Second double.</param>
        /// <param name="D3">Third double.</param>
        /// <returns>
        /// True on success, false on failure. On failure, <paramref name="D1"/>, <paramref name="D2"/>, and 
        /// <paramref name="D3"/> are undefined.
        /// </returns>
        internal static bool TryParse3Double (string Raw, out double D1, out double D2, out double D3)
        {
            D1 = 0.0;
            D2 = 0.0;
            D3 = 0.0;

            if (string.IsNullOrEmpty(Raw))
                return false;
            Raw = Raw.Trim();
            string[] parts = Raw.Split(new char[] { ' ', ',', ';', '/', '°' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                return false;
            if (!double.TryParse(parts[0], out D1))
                return false;
            if (!double.TryParse(parts[1], out D2))
                return false;
            if (!double.TryParse(parts[2], out D3))
                return false;

            return true;
        }

        /// <summary>
        /// Parse a string that may have four double values.
        /// </summary>
        /// <remarks>
        /// Values may be separated by spaces, commas, semi-colons, slashes, and degree signs.
        /// </remarks>
        /// <param name="Raw">The raw string.</param>
        /// <param name="D1">First double.</param>
        /// <param name="D2">Second double.</param>
        /// <param name="D3">Third double.</param>
        /// <param name="D4">Fourth double.</param>
        /// <returns>
        /// True on success, false on failure. On failure, <paramref name="D1"/>, <paramref name="D2"/>,  
        /// <paramref name="D3"/>, or <paramref name="D4"/> are undefined.
        /// </returns>
        internal static bool TryParse4Double (string Raw, out double D1, out double D2, out double D3, out double D4)
        {
            D1 = 0.0;
            D2 = 0.0;
            D3 = 0.0;
            D4 = 0.0;

            if (string.IsNullOrEmpty(Raw))
                return false;
            Raw = Raw.Trim();
            string[] parts = Raw.Split(new char[] { ' ', ',', ';', '/', '°' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4)
                return false;
            if (!double.TryParse(parts[0], out D1))
                return false;
            if (!double.TryParse(parts[1], out D2))
                return false;
            if (!double.TryParse(parts[2], out D3))
                return false;
            if (!double.TryParse(parts[3], out D4))
                return false;

            return true;
        }

        /// <summary>
        /// Parse a string that may have three integer values.
        /// </summary>
        /// <remarks>
        /// Values may be separated by spaces, commas, semi-colons, and slashes
        /// </remarks>
        /// <param name="Raw">The raw string.</param>
        /// <param name="I1">First integer.</param>
        /// <param name="I2">Second integer.</param>
        /// <param name="I3">Third integer.</param>
        /// <returns>
        /// True on success, false on failure. On failure, <paramref name="I1"/>, <paramref name="I2"/>, and 
        /// <paramref name="I3"/> are undefined.
        /// </returns>
        internal static bool TryParse3Int (string Raw, out int I1, out int I2, out int I3)
        {
            I1 = 0;
            I2 = 0;
            I3 = 0;

            if (string.IsNullOrEmpty(Raw))
                return false;
            Raw = Raw.Trim();
            string[] parts = Raw.Split(new char[] { ' ', ',', ';', '/' });
            if (parts.Length != 3)
                return false;
            if (!int.TryParse(parts[0], out I1))
                return false;
            if (!int.TryParse(parts[1], out I2))
                return false;
            if (!int.TryParse(parts[2], out I3))
                return false;

            return true;
        }

        /// <summary>
        /// Parse a string that may have three byte values.
        /// </summary>
        /// <remarks>
        /// Values may be separated by spaces, commas, semi-colons, and slashes
        /// </remarks>
        /// <param name="Raw">The raw string.</param>
        /// <param name="B1">First byte.</param>
        /// <param name="B2">Second byte.</param>
        /// <param name="B3">Third byte.</param>
        /// <param name="Base">The expected base.</param>
        /// <returns>
        /// True on success, false on failure. On failure, <paramref name="B1"/>, <paramref name="B2"/>, and 
        /// <paramref name="B3"/> are undefined.
        /// </returns>
        internal static bool TryParse3Byte (string Raw, out byte B1, out byte B2, out byte B3, int Base = 10)
        {
            B1 = 0;
            B2 = 0;
            B3 = 0;

            if (string.IsNullOrEmpty(Raw))
                return false;
            if (Base != 10 || Base != 16)
                Base = 10;
            Raw = Raw.Trim();
            string[] parts = Raw.Split(new char[] { ' ', ',', ';', '/' });
            if (parts.Length != 3)
                return false;

            B1 = Convert.ToByte(parts[0], Base);
            B2 = Convert.ToByte(parts[1], Base);
            B3 = Convert.ToByte(parts[2], Base);

            return true;
        }

        /// <summary>
        /// Parse a string that may have three integer values.
        /// </summary>
        /// <remarks>
        /// Values may be separated by spaces, commas, semi-colons, and slashes
        /// </remarks>
        /// <param name="Raw">The raw string.</param>
        /// <param name="I1">First integer.</param>
        /// <param name="I2">Second integer.</param>
        /// <param name="I3">Third integer.</param>
        /// <param name="I4">Fourth integer.</param>
        /// <returns>
        /// True on success, false on failure. On failure, <paramref name="I1"/>, <paramref name="I2"/>, and 
        /// <paramref name="I3"/> are undefined.
        /// </returns>
        internal static bool TryParse4Int (string Raw, out int I1, out int I2, out int I3, out int I4)
        {
            I1 = 0;
            I2 = 0;
            I3 = 0;
            I4 = 0;

            if (string.IsNullOrEmpty(Raw))
                return false;
            Raw = Raw.Trim();
            string[] parts = Raw.Split(new char[] { ' ', ',', ';', '/' });
            if (parts.Length != 4)
                return false;
            if (!int.TryParse(parts[0], out I1))
                return false;
            if (!int.TryParse(parts[1], out I2))
                return false;
            if (!int.TryParse(parts[2], out I3))
                return false;
            if (!int.TryParse(parts[3], out I4))
                return false;

            return true;
        }

        /// <summary>
        /// Try to parse <paramref name="Raw"/> as a possible hex value. If the value can't be interpreted as a hex value,
        /// it will be parsed as a decimal value.
        /// </summary>
        /// <param name="Raw">The string to parse.</param>
        /// <param name="Final">the result of the parsing, if true is returned.</param>
        /// <returns>True on success, false on failure. If false is returned <paramref name="Final"/> is undefined.</returns>
        internal static bool TryParsePossibleHexValue (string Raw, out UInt32 Final)
        {
            Final = 0;
            Raw = Raw.Trim();
            if (string.IsNullOrEmpty(Raw))
                return false;
            if (Raw.Length >= 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    Raw = Raw.Substring(2);
                    Raw = "#" + Raw;
                }
            }
            if (Raw[0] != '#')
            {
                if (UInt32.TryParse(Raw, out Final))
                    return true;
                return false;
            }
            Raw = Raw.Substring(1);
            if (string.IsNullOrEmpty(Raw))
                return false;
            try
            {
                Final = Convert.ToUInt32(Raw, 16);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Given a color space, return its name.
        /// </summary>
        /// <param name="TheColorSpace">The color space whose name will be returned.</param>
        /// <returns>the name of <paramref name="TheColorSpace"/> on success, empty string if not found.</returns>
        public static string GetColorSpaceName (ColorSpaces TheColorSpace)
        {
            if (!ColorSpaceNameMap.ContainsKey(TheColorSpace))
                return "";
            return ColorSpaceNameMap[TheColorSpace];
        }

        /// <summary>
        /// Maps color spaces to their names.
        /// </summary>
        internal static Dictionary<ColorSpaces, string> ColorSpaceNameMap = new Dictionary<ColorSpaces, string>()
        {
            {ColorSpaces.RGB, RGB.StaticColorLabel },
            {ColorSpaces.HSL, HSL.StaticColorLabel },
            {ColorSpaces.HSV, HSV.StaticColorLabel },
            {ColorSpaces.YUV, YUV.StaticColorLabel },
            {ColorSpaces.YIQ, YIQ.StaticColorLabel },
            {ColorSpaces.YCbCr, YCbCr.StaticColorLabel },
            {ColorSpaces.CIELab, CIELab.StaticColorLabel },
            {ColorSpaces.XYZ, XYZ.StaticColorLabel },
            {ColorSpaces.CMY, CMY.StaticColorLabel },
            {ColorSpaces.CMYK, CMYK.StaticColorLabel },
            {ColorSpaces.TSL, TSL.StaticColorLabel }
        };

        /// <summary>
        /// Given a color space name, return the color space enumeration.
        /// </summary>
        /// <param name="RawName">Name of the color space to return.</param>
        /// <returns>Color space associated with <paramref name="RawName"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thown if <paramref name="RawName"/> is empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="RawName"/> cannot be found.
        /// </exception>
        public static ColorSpaces GetSpaceFromName(string RawName)
        {
            if (string.IsNullOrEmpty(RawName))
                throw new ArgumentNullException("RawName");
            foreach(KeyValuePair<ColorSpaces,string> KVP in ColorSpaceNameMap)
            {
                if (KVP.Value.ToLower() == RawName.ToLower())
                    return KVP.Key;
            }
            throw new InvalidOperationException("Color space name not found.");
        }

        /// <summary>
        /// Parse the contents of <paramref name="Raw"/> using the color space <paramref name="ColorSpace"/> and return the color
        /// in <paramref name="ParsedColor"/>.
        /// </summary>
        /// <param name="Raw">String to parse.</param>
        /// <param name="ColorSpace">Where to parse the string.</param>
        /// <param name="ParsedColor">Results of the parse.</param>
        /// <returns>True on success, false on failure. If false is returned, <paramref name="ParsedColor"/> is undefined.</returns>
        public static bool ParseColorInput (string Raw, ColorSpaces ColorSpace, out Color ParsedColor)
        {
            IColorSpace Result = null;
            bool OK = ParseColorInput(Raw, ColorSpace, out Result);
            if (OK)
                ParsedColor = Result.ToRGBColor();
            else
                ParsedColor = Colors.Transparent;
            return OK;
        }

        /// <summary>
        /// Parse the contents of <paramref name="Raw"/> using the color space <paramref name="ColorSpace"/> and return the color
        /// in <paramref name="NewColor"/>.
        /// </summary>
        /// <param name="Raw">String to parse.</param>
        /// <param name="ColorSpace">Where to parse the string.</param>
        /// <param name="NewColor">Results of the parse.</param>
        /// <returns>True on success, false on failure. If false is returned, <paramref name="NewColor"/> is undefined.</returns>
        public static bool ParseColorInput (string Raw, ColorSpaces ColorSpace, out IColorSpace NewColor)
        {
            NewColor = null;
            bool OK = false;

            switch (ColorSpace)
            {
                case ColorSpaces.RGB:
                    RGB iRGB = new RGB();
                    OK = iRGB.TryParse(Raw, out iRGB);
                    if (OK)
                    {
                        NewColor = iRGB;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.HSL:
                    HSL iHSL = new HSL();
                    OK = iHSL.TryParse(Raw, out iHSL);
                    if (OK)
                    {
                        NewColor = iHSL;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.HSV:
                    HSV iHSV = new HSV();
                    OK = iHSV.TryParse(Raw, out iHSV);
                    if (OK)
                    {
                        NewColor = iHSV;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.YUV:
                    YUV iYUV = new YUV();
                    OK = iYUV.TryParse(Raw, out iYUV);
                    if (OK)
                    {
                        NewColor = iYUV;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.YIQ:
                    YIQ iYIQ = new YIQ();
                    OK = iYIQ.TryParse(Raw, out iYIQ);
                    if (OK)
                    {
                        NewColor = iYIQ;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.YCbCr:
                    YCbCr iYCbCr = new YCbCr();
                    OK = iYCbCr.TryParse(Raw, out iYCbCr);
                    if (OK)
                    {
                        NewColor = iYCbCr;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.CMY:
                    CMY iCMY = new CMY();
                    OK = iCMY.TryParse(Raw, out iCMY);
                    if (OK)
                    {
                        NewColor = iCMY;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.CMYK:
                    CMYK iCMYK = new CMYK();
                    OK = iCMYK.TryParse(Raw, out iCMYK);
                    if (OK)
                    {
                        NewColor = iCMYK;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.XYZ:
                    XYZ iXYZ = new XYZ();
                    OK = iXYZ.TryParse(Raw, out iXYZ);
                    if (OK)
                    {
                        NewColor = iXYZ;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.CIELab:
                    CIELab iCIELab = new CIELab();
                    OK = iCIELab.TryParse(Raw, out iCIELab);
                    if (OK)
                    {
                        NewColor = iCIELab;
                        return true;
                    }
                    NewColor = null;
                    return false;

                case ColorSpaces.TSL:
                    TSL iTSL = new TSL();
                    OK = iTSL.TryParse(Raw, out iTSL);
                    if (OK)
                    {
                        NewColor = iTSL;
                        return true;
                    }
                    NewColor = null;
                    return false;

                default:
                    NewColor = null;
                    return false;
            }
        }

        /// <summary>
        /// Create a new (and uncolored) color space.
        /// </summary>
        /// <param name="ColorSpace">The type of color space to return.</param>
        /// <returns>New color space based on <paramref name="Colorspace"/>.</returns>
        public static IColorSpace MakeColor(ColorSpaces ColorSpace)
        {
            switch(ColorSpace)
            {
                case ColorSpaces.RGB:
                    return new RGB();

                case ColorSpaces.HSL:
                    return new HSL();

                case ColorSpaces.HSV:
                    return new HSV();

                case ColorSpaces.CMY:
                    return new CMY();

                case ColorSpaces.CMYK:
                    return new CMYK();

                case ColorSpaces.YCbCr:
                    return new YCbCr();

                case ColorSpaces.YIQ:
                    return new YIQ();

                case ColorSpaces.YUV:
                    return new YUV();

                case ColorSpaces.XYZ:
                    return new XYZ();

                case ColorSpaces.CIELab:
                    return new CIELab();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Convert the color in <paramref name="OldColorSpace"/> to the color space indicated by <paramref name="NewColorSpace"/>.
        /// </summary>
        /// <param name="NewColorSpace">The new color space.</param>
        /// <param name="OldColorSpace">The old color space.</param>
        /// <returns>
        /// New color space with converted color. If the old and new color spaces are the same, the old color space is returned
        /// unchanged.
        /// </returns>
        public static IColorSpace ConvertColorSpace (ColorSpaces NewColorSpace, IColorSpace OldColorSpace)
        {
            if (OldColorSpace == null)
                throw new ArgumentNullException("OldColorSpace");
            if (OldColorSpace.ColorSpace == NewColorSpace)
                return OldColorSpace;
            RGB IntCS = OldColorSpace.ToRGB();
            if (IntCS == null)
                throw new InvalidOperationException("Error converting color space to RGB.");
            return IntCS.ToOther(NewColorSpace);
        }
    }
}
