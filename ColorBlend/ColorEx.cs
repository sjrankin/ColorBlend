using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.InteropServices;

namespace ColorBlend
{
    /// <summary>
    /// Extended color.
    /// </summary>
    public class ColorEx : IEquatable<ColorEx>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorEx ()
        {
            TheColor = Colors.Transparent;
            Location = new PointEx(0, 0);
            Radius = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="SourceColor">Original color.</param>
        public ColorEx (Color SourceColor)
        {
            TheColor = SourceColor;
            Location = new PointEx(0, 0);
            Radius = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="R">Initial Red channel value.</param>
        /// <param name="G">Initial Green channel value.</param>
        /// <param name="B">Initial Blue channel value.</param>
        public ColorEx (byte R, byte G, byte B)
        {
            TheColor = Color.FromArgb(0xff, R, G, B);
            Location = new PointEx(0, 0);
            Radius = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="A">Initial Alpha channel value.</param>
        /// <param name="R">Initial Red channel value.</param>
        /// <param name="G">Initial Green channel value.</param>
        /// <param name="B">Initial Blue channel value.</param>
        public ColorEx(byte A, byte R, byte G, byte B)
        {
            TheColor = Color.FromArgb(A, R, G, B);
            Location = new PointEx(0, 0);
            Radius = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="SourceColor">Original color.</param>
        /// <param name="X">Horizontal coordinate.</param>
        /// <param name="Y">Vertical coordinate.</param>
        public ColorEx (Color SourceColor, int X, int Y)
        {
            TheColor = SourceColor;
            Location = new PointEx(X, Y);
            Radius = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="SourceColor">Original color.</param>
        /// <param name="SourceLocation">Original coordinates.</param>
        public ColorEx(Color SourceColor, PointEx SourceLocation)
        {
            TheColor = SourceColor;
            Location = new PointEx(SourceLocation);
            Radius = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="OtherColor">Other ColorEx from which data will be used to populate the newly instantiated ColorEx.</param>
        public ColorEx (ColorEx OtherColor)
        {
            TheColor = OtherColor.AsColor;
            Location = OtherColor.Location;
            Radius = OtherColor.Radius;
        }

        /// <summary>
        /// Determines if this instantiated ColorEx has the same values as <paramref name="OtherColor"/>.
        /// </summary>
        /// <param name="OtherColor">Other color to compare to this color.</param>
        /// <returns>True if colors (and location and radius) are equal, false if not.</returns>
        public bool Equals (ColorEx OtherColor)
        {
            if (OtherColor == null)
                return false;
            bool ColorValuesEqual = (
                                     (OtherColor.TheColor.A == TheColor.A) &&
                                     (OtherColor.TheColor.R == TheColor.R) &&
                                     (OtherColor.TheColor.G == TheColor.G) &&
                                     (OtherColor.TheColor.B == TheColor.B)
                                    );
            bool LocationsEqual = OtherColor.Location.Equals(Location);
            bool RadiiEqual = OtherColor.Radius == Radius;
            return ColorValuesEqual && LocationsEqual && RadiiEqual;
        }

        /// <summary>
        /// Change the color to a random color.
        /// </summary>
        /// <param name="Rand">If specified the random number generator to use. If null a new one will be used.</param>
        /// <param name="IncludeAlpha">If true, the Alpha channel will be randomized. If not, Alpha will be set to 0xff.</param>
        public void Randomize (Random Rand = null, bool IncludeAlpha = false)
        {
            if (Rand == null)
                Rand = new Random();
            if (IncludeAlpha)
                TheColor.A = (byte)Rand.Next(0, 0xff);
            else
                TheColor.A = 0xff;
            TheColor.R = (byte)Rand.Next(0, 0xff);
            TheColor.G = (byte)Rand.Next(0, 0xff);
            TheColor.B = (byte)Rand.Next(0, 0xff);
        }

        /// <summary>
        /// Change the color to a random color.
        /// </summary>
        /// <param name="Min">Minimum channel value.</param>
        /// <param name="Max">Maximum channel value.</param>
        /// <param name="Rand">If specified the random number generator to use. If null a new one will be used.</param>
        /// <param name="IncludeAlpha">If true, the Alpha channel will be randomized. If not, Alpha will be set to 0xff.</param>
        public void Randomize (byte Min, byte Max, Random Rand = null, bool IncludeAlpha = false)
        {
            if (Rand == null)
                Rand = new Random();
            if (IncludeAlpha)
                TheColor.A = (byte)Rand.Next(0, 0xff);
            else
                TheColor.A = 0xff;
            TheColor.R = (byte)Rand.Next(Min, Max);
            TheColor.G = (byte)Rand.Next(Min, Max);
            TheColor.B = (byte)Rand.Next(Min, Max);
        }

        /// <summary>
        /// Invert the color.
        /// </summary>
        /// <param name="IncludeAlpha">If true, alpha will be inverted as well.</param>
        public void Invert (bool IncludeAlpha = false)
        {
            if (IncludeAlpha)
                TheColor.A = (byte)(0xff - TheColor.A);
            TheColor.R = (byte)(0xff - TheColor.R);
            TheColor.G = (byte)(0xff - TheColor.G);
            TheColor.B = (byte)(0xff - TheColor.B);
        }

        /// <summary>
        /// Clamp the color's channels to the specified values.
        /// </summary>
        /// <param name="MinChannelValue">Minimum channel value.</param>
        /// <param name="MaxChannelValue">Maximum channel value.</param>
        /// <param name="IncludeAlpha">If true, Alpha will be clamped as well.</param>
        public void Clamp (byte MinChannelValue, byte MaxChannelValue, bool IncludeAlpha = false)
        {
            if (IncludeAlpha)
            {
                if (TheColor.A < MinChannelValue)
                    TheColor.A = MinChannelValue;
                if (TheColor.A > MaxChannelValue)
                    TheColor.A = MaxChannelValue;
            }
            if (TheColor.R < MinChannelValue)
                TheColor.R = MinChannelValue;
            if (TheColor.R > MaxChannelValue)
                TheColor.R = MaxChannelValue;
            if (TheColor.G < MinChannelValue)
                TheColor.G = MinChannelValue;
            if (TheColor.G > MaxChannelValue)
                TheColor.G = MaxChannelValue;
            if (TheColor.B < MinChannelValue)
                TheColor.B = MinChannelValue;
            if (TheColor.B > MaxChannelValue)
                TheColor.B = MaxChannelValue;
        }

        /// <summary>
        /// Holds the color value.
        /// </summary>
        internal Color TheColor;
        /// <summary>
        /// Get the .Net color equivalent to this color.
        /// </summary>
        public Color AsColor
        {
            get
            {
                return TheColor;
            }
        }

        /// <summary>
        /// Get or set the radius of the color.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Get or set the location of the color.
        /// </summary>
        public PointEx Location { get; internal set; }

        /// <summary>
        /// Return a structure with the color's values.
        /// </summary>
        /// <returns>Populated ColorExStruct.</returns>
        public ColorExStruct GetColorStructure ()
        {
            ColorExStruct CES = new ColorExStruct
            {
                PackedARGBColor = TheColor.ToARGB(),
                X = Location.IntX,
                Y = Location.IntY,
                Radius = Radius
            };
            return CES;
        }
    }

    /// <summary>
    /// Structure that contains the values of a ColorEx instantiation. Used for interop.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorExStruct
    {
        /// <summary>
        /// Packed ARGB color.
        /// </summary>
        public UInt32 PackedARGBColor;
        /// <summary>
        /// Horizontal location.
        /// </summary>
        public int X;
        /// <summary>
        /// Vertical location.
        /// </summary>
        public int Y;
        /// <summary>
        /// Radius of the color.
        /// </summary>
        public double Radius;
    }
}
