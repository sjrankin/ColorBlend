using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace ColorBlend
{
    /// <summary>
    /// Defines a color point.
    /// </summary>
    [DebuggerDisplay("({AbsolutePoint.X},{AbsolutePoint.Y}) {PointColor}")]
    public class ColorPoint
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorPoint ()
        {
            CommonInitialization();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorPoint (string Name)
        {
            CommonInitialization();
            this.Name = Name;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RelativeX">The color's relative horizontal location.</param>
        /// <param name="RelativeY">The color's relative vertical location.</param>
        /// <param name="SourceColor">The color at the relative location.</param>
        public ColorPoint (double RelativeX, double RelativeY, Color SourceColor)
        {
            CommonInitialization();
            this.RelativePoint = new PointEx(RelativeX, RelativeY);
            this.PointColor = SourceColor;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Name">Name of the point.</param>
        /// <param name="RelativeX">The color's relative horizontal location.</param>
        /// <param name="RelativeY">The color's relative vertical location.</param>
        /// <param name="SourceColor">The color at the relative location.</param>
        public ColorPoint (string Name, double RelativeX, double RelativeY, Color SourceColor)
        {
            CommonInitialization();
            this.Name = Name;
            this.RelativePoint = new PointEx(RelativeX, RelativeY);
            this.PointColor = SourceColor;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RelativeX">The color's relative horizontal location.</param>
        /// <param name="RelativeY">The color's relative vertical location.</param>
        /// <param name="SourceColor">The color at the relative location.</param>
        /// <param name="Radius">
        /// Optional value for the <seealso cref="Radius"/>. If <paramref name="Hypotenuse"/> is also specified, the Radius
        /// will be ignored.
        /// </param>
        /// <param name="Hypotenuse">Optional value for the <seealso cref="Hypotenuse"/>.</param>
        public ColorPoint (double RelativeX, double RelativeY, Color SourceColor, Nullable<double> Radius = null,
            Nullable<double> Hypotenuse = null)
        {
            CommonInitialization();
            this.RelativePoint = new PointEx(RelativeX, RelativeY);
            this.PointColor = SourceColor;
            if (Radius.HasValue)
            {
                this.UseRadius = true;
                this.Radius = Radius.Value;
            }
            else
                this.Radius = 100.0;
            if (Hypotenuse.HasValue)
            {
                this.UseRadius = false;
                this.Hypotenuse = Hypotenuse.Value;
            }
            else
                this.Hypotenuse = 1.0;
        }

        /// <summary>
        /// Get the pixel size.
        /// </summary>
        public int PixelSize
        {
            get
            {
                if (!PixelFormatSizes.ContainsKey(PointFormat))
                    throw new InvalidOperationException("Unknown pixel format: " + PointFormat.ToString());
                return PixelFormatSizes[PointFormat];
            }
        }

        /// <summary>
        /// Get or set the supported pixel point format.
        /// </summary>
        public SupportedPixelFormats PointFormat { get; set; }

        /// <summary>
        /// Get the supported pixel format.
        /// </summary>
        public enum SupportedPixelFormats
        {
            BGRA32,
        }

        /// <summary>
        /// Size of pixel formats.
        /// </summary>
        private Dictionary<SupportedPixelFormats, int> PixelFormatSizes = new Dictionary<SupportedPixelFormats, int>()
        {
            {SupportedPixelFormats.BGRA32, 4}
        };

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="AbsoluteX">The point's upper-left X position.</param>
        /// <param name="AbsoluteY">The point's upper-left Y position.</param>
        /// <param name="Width">The width of the point.</param>
        /// <param name="Height">The height of the point.</param>
        /// <param name="SourceColor">The point's color.</param>
        public ColorPoint (string Name, int AbsoluteX, int AbsoluteY, int Width, int Height, Color SourceColor)
        {
            CommonInitialization();
            this.Name = Name;
            this.AbsolutePoint = new PointEx(AbsoluteX, AbsoluteY);
            this.Width = Width;
            this.Height = Height;
            this.PointColor = SourceColor;
        }

        /// <summary>
        /// Initialization common to all constructors. Ensure properties are properly set.
        /// </summary>
        private void CommonInitialization ()
        {
            ColorPointID = Guid.NewGuid();
            PointColor = Color.FromArgb(0, 0, 0, 0);
            RelativePoint = new PointEx(0, 0);
            AbsolutePoint = new PointEx(0, 0);
            Radius = 100.0;
            Width = 100.0;
            Height = 100.0;
            Top = 0.0;
            Left = 0.0;
            Hypotenuse = 1.0;
            UseRadius = false;
            UseAlpha = false;
            AlphaEnd = 0.0;
            AlphaStart = 1.0;
            DrawHorizontalIndicators = false;
            DrawVerticalIndicators = false;
            DrawPointIndicator = false;
            Name = "";
            Enabled = true;
            UseAbsoluteOnly = false;
            OutOfBounds = true;
            PointFormat = SupportedPixelFormats.BGRA32;
        }

        /// <summary>
        /// Global random number generator.
        /// </summary>
        private Random Rand = new Random();

        /// <summary>
        /// Get or set the point name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the radius. Ignored unless <seealso cref="UseRadius"/> is true.
        /// </summary>
        private double _Radius = 100.0;
        public double Radius
        {
            get
            {
                return _Radius;
            }
            set
            {
                _Radius = value;
                Extent = (_Radius * 2) + (_Radius % 2 == 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// Get the extent (the width or height, e.g., the diameter) of the point.
        /// </summary>
        public double Extent { get; internal set; }

        /// <summary>
        /// Get or set the upper coordinate of the upper-left point.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Get or set the left coordinate of the upper-left point.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Get or set the height of the blob.
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Get or set the height of the blob as an integer.
        /// </summary>
        public int IntHeight
        {
            get
            {
                return (int)Height;
            }
            set
            {
                Height = (double)value;
            }
        }

        /// <summary>
        /// Get or set the width of the blob.
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Get or set the width of the blob as an integer.
        /// </summary>
        public int IntWidth
        {
            get
            {
                return (int)Width;
            }
            set
            {
                Width = (double)value;
            }
        }

        /// <summary>
        /// Used by parents of this class as an out-of-bounds flag. Other than initializing this property to true, this
        /// class doesn't set this property.
        /// </summary>
        public bool OutOfBounds { get; set; }

        /// <summary>
        /// Determines if the Radius or Hypotenuse is used for percent calculations.
        /// </summary>
        public bool UseRadius { get; set; }

        /// <summary>
        /// Determines if alpha calculations are made.
        /// </summary>
        public bool UseAlpha { get; set; }

        /// <summary>
        /// Alpha value at this pure point.
        /// </summary>
        public double AlphaStart { get; set; }

        /// <summary>
        /// Alpha value at either Radius or Hypotenuse, depending on the value of <seealso cref="UseRadius"/>.
        /// </summary>
        public double AlphaEnd { get; set; }

        /// <summary>
        /// Get or set the hypotenuse. Ignored if <seealso cref="UseRadius"/> is true.
        /// </summary>
        public double Hypotenuse { get; set; }

        /// <summary>
        /// Get or set the color at the point.
        /// </summary>
        public Color PointColor { get; set; }

        /// <summary>
        /// Get the relative location in the buffer for the point. This is the upper-left corner of the blob.
        /// </summary>
        public PointEx RelativePoint { get; internal set; }

        /// <summary>
        /// Determines if the color point is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Get the absolute location in the buffer for the point. Returns 0,0 unless <seealso cref="CreateAbsolutePoint"/> has been called.
        /// </summary>
        public PointEx AbsolutePoint { get; internal set; }

        /// <summary>
        /// Get or set the draw horizontal indicators flag.
        /// </summary>
        public bool DrawHorizontalIndicators { get; set; }

        /// <summary>
        /// Get or set the draw vertical indicators flag.
        /// </summary>
        public bool DrawVerticalIndicators { get; set; }

        /// <summary>
        /// Get or set the draw point indicator flag.
        /// </summary>
        public bool DrawPointIndicator { get; set; }

        /// <summary>
        /// Get the color point ID.
        /// </summary>
        public Guid ColorPointID { get; internal set; }

        /// <summary>
        /// Determines if cropping is needed based on the <seealso cref="Radius"/>, <seealso cref="AbsolutePoint"/>,
        /// and the target size.
        /// </summary>
        /// <remarks>
        /// <seealso cref="CreateAbsolutePoint"/> is called to collapse the relative points. Assumes <seealso cref="Radius"/>
        /// has been set as appropriate.
        /// </remarks>
        /// <param name="TargetWidth">Logical width of the target buffer.</param>
        /// <param name="TargetHeight">Logical height of the target buffer.</param>
        /// <returns>True if cropping is needed, false if not.</returns>
        public bool NeedToCropOld (int TargetWidth, int TargetHeight)
        {
            if (!UseAbsoluteOnly)
                CreateAbsolutePoint(TargetWidth, TargetHeight);
            if (AbsolutePoint.X - Radius < 0)
                return true;
            if (AbsolutePoint.X + Radius > TargetWidth - 1)
                return true;
            if (AbsolutePoint.Y - Radius < 0)
                return true;
            if (AbsolutePoint.Y + Radius > TargetHeight - 1)
                return true;
            return false;
        }

        /// <summary>
        /// Determines if cropping is needed based on the <seealso cref="Radius"/>, <seealso cref="AbsolutePoint"/>,
        /// and the target size.
        /// </summary>
        /// <remarks>
        /// <seealso cref="CreateAbsolutePoint"/> is called to collapse the relative points. Assumes <seealso cref="Radius"/>
        /// has been set as appropriate.
        /// </remarks>
        /// <param name="TargetWidth">Logical width of the target buffer.</param>
        /// <param name="TargetHeight">Logical height of the target buffer.</param>
        /// <returns>True if cropping is needed, false if not.</returns>
        public bool NeedToCrop (int TargetWidth, int TargetHeight)
        {
            if (!UseAbsoluteOnly)
                CreateAbsolutePoint(TargetWidth, TargetHeight);
            if (AbsolutePoint.X + Width < 0)
                return true;
            if (AbsolutePoint.X > TargetWidth)
                return true;
            if (AbsolutePoint.Y + Height < 0)
                return true;
            if (AbsolutePoint.Y > TargetHeight)
                return true;
            return false;
        }

        /// <summary>
        /// Returns valid coordinates regardless of the origin of the point. It's entirely possible that the final coordinates
        /// are not visible in the view port.
        /// </summary>
        /// <param name="TargetWidth">Logical width of the target buffer.</param>
        /// <param name="TargetHeight">Logical height of the target buffer.</param>
        /// <param name="UpperLeft">Will contain a valid upper-left coordinate.</param>
        /// <param name="LowerRight">Will contain a valid lower-right coordinate.</param>
        /// <returns>True if the point is fully within the specified buffer, false if not.</returns>
        public bool FinalCoordinates (int TargetWidth, int TargetHeight, ref PointEx UpperLeft, ref PointEx LowerRight)
        {
            if (NeedToCrop(TargetWidth, TargetHeight))
            {
                int NotUsed = 0;
                CroppedCoordinates(TargetWidth, TargetHeight, ref UpperLeft, ref LowerRight, ref NotUsed);
                return false;
            }
            UpperLeft = new PointEx(AbsolutePoint.X - Radius, AbsolutePoint.Y - Radius);
            LowerRight = new PointEx(AbsolutePoint.X + Radius, AbsolutePoint.Y + Radius);
            return true;
        }

        /// <summary>
        /// Return cropped coordinates for the color blob. If no cropping is needed, the coordinates will be for the entire
        /// blob. If the entire blob is not visible, the returned area will be 0.
        /// </summary>
        /// <param name="TargetWidth">The width of the target buffer.</param>
        /// <param name="TargetHeight">The height of the target buffer.</param>
        /// <param name="UpperLeft">Upper-left corner coordinates.</param>
        /// <param name="LowerRight">Lower-right corner coordinates.</param>
        /// <param name="Area">The area of the cropped region. Will be zero if not visible.</param>
        public void CroppedCoordinatesOld (int TargetWidth, int TargetHeight, ref PointEx UpperLeft, ref PointEx LowerRight,
            ref int Area)
        {
            LeftCropped = false;
            TopCropped = false;
            RightCropped = false;
            BottomCropped = false;
            double upperLEFT = AbsolutePoint.X - Radius;
            if (upperLEFT < 0.0)
            {
                upperLEFT = 0.0;
                LeftCropped = true;
            }
            double UPPERleft = AbsolutePoint.Y - Radius;
            if (UPPERleft < 0.0)
            {
                UPPERleft = 0.0;
                TopCropped = true;
            }
            UpperLeft = new PointEx(upperLEFT, UPPERleft);
            double lowerRIGHT = AbsolutePoint.X + Radius;
            if (lowerRIGHT > TargetWidth - 1)
            {
                lowerRIGHT = TargetWidth - 1;
                RightCropped = true;
            }
            double LOWERright = AbsolutePoint.Y + Radius;
            if (LOWERright > TargetHeight - 1)
            {
                LOWERright = TargetHeight - 1;
                BottomCropped = true;
            }
            LowerRight = new PointEx(lowerRIGHT, LOWERright);
            Area = (LowerRight.IntX - UpperLeft.IntX + 1) * (LowerRight.IntY - UpperLeft.IntY + 1);
        }

        /// <summary>
        /// Return cropped coordinates for the color blob. If no cropping is needed, the coordinates will be for the entire
        /// blob. If the entire blob is not visible, the returned area will be 0.
        /// </summary>
        /// <param name="TargetWidth">The width of the target buffer.</param>
        /// <param name="TargetHeight">The height of the target buffer.</param>
        /// <param name="UpperLeft">Upper-left corner coordinates.</param>
        /// <param name="LowerRight">Lower-right corner coordinates.</param>
        /// <param name="Area">The area of the cropped region. Will be zero if not visible.</param>
        public void CroppedCoordinates (int TargetWidth, int TargetHeight, ref PointEx UpperLeft, ref PointEx LowerRight,
            ref int Area)
        {
            LeftCropped = false;
            TopCropped = false;
            RightCropped = false;
            BottomCropped = false;
            double upperLEFT = AbsolutePoint.X;
            if (upperLEFT < 0)
            {
                upperLEFT = 0;
                LeftCropped = true;
            }
            double UPPERleft = AbsolutePoint.Y;
            if (UPPERleft < 0)
            {
                UPPERleft = 0;
                TopCropped = true;
            }
            UpperLeft = new PointEx(upperLEFT, UPPERleft);
            double lowerRIGHT = AbsolutePoint.X + Width;
            if (lowerRIGHT > TargetWidth)
            {
                //lowerRIGHT =
                //RightCropped = true;
            }
        }

        /// <summary>
        /// Get the left cropped flag.
        /// </summary>
        public bool LeftCropped { get; internal set; }

        /// <summary>
        /// Get the top cropped flag.
        /// </summary>
        public bool TopCropped { get; internal set; }

        /// <summary>
        /// Get the right cropped flag.
        /// </summary>
        public bool RightCropped { get; internal set; }

        /// <summary>
        /// Get the bottom cropped flag.
        /// </summary>
        public bool BottomCropped { get; internal set; }

        /// <summary>
        /// Determines if <paramref name="UpperLeft"/> is "greater" than <paramref name="LowerRight"/>.
        /// </summary>
        /// <param name="UpperLeft">Upper left point.</param>
        /// <param name="LowerRight">Lower right point.</param>
        /// <returns>True if <paramref name="UpperLeft"/> is "less" than <paramref name="LowerRight"/>, false if not.</returns>
        public bool PointsInOrder (PointEx UpperLeft, PointEx LowerRight)
        {
            if (UpperLeft.X > LowerRight.X)
                return false;
            if (UpperLeft.Y > LowerRight.Y)
                return false;
            return true;
        }

        /// <summary>
        /// Returns provisional coordinates (which may be outside of the valid buffer) depending on the absolute coordinates
        /// of the point.
        /// </summary>
        /// <param name="TargetWidth">Logical width of the target buffer.</param>
        /// <param name="TargetHeight">Logical height of the target buffer.</param>
        /// <param name="UpperLeft">Will contain a upper-left coordinate.</param>
        /// <param name="LowerRight">Will contain a lower-right coordinate.</param>
        /// <returns>True if the point is fully within the specified buffer, false if not.</returns>
        public bool ProvisionalCoordinates (int TargetWidth, int TargetHeight, ref Point UpperLeft, ref Point LowerRight)
        {
            bool DoCrop = NeedToCrop(TargetWidth, TargetHeight);
            UpperLeft = new Point(AbsolutePoint.X - Radius, AbsolutePoint.Y - Radius);
            LowerRight = new Point(AbsolutePoint.X + Radius, AbsolutePoint.Y + Radius);
            return !DoCrop;
        }

        /// <summary>
        /// "Move" the point by setting the absolute coordinates. If <seealso cref="UseAbsoluteOnly"/> is not true, setting
        /// coordinates here will have no effect and will be overwritten at the next <seealso cref="CreateAbsolutePoint"/>
        /// method call.
        /// </summary>
        /// <param name="NewX">The new absolute horizontal position.</param>
        /// <param name="NewY">The new absolute vertical position.</param>
        public void AbsoluteMove (int NewX, int NewY, Nullable<bool> OnlyAbsolute = null)
        {
            if (OnlyAbsolute.HasValue)
                UseAbsoluteOnly = OnlyAbsolute.Value;
            AbsolutePoint.X = (double)NewX;
            AbsolutePoint.Y = (double)NewY;
        }

        /// <summary>
        /// Determines if the point should be positioned only via the absolute point, not the relative point.
        /// </summary>
        public bool UseAbsoluteOnly { get; set; }

        /// <summary>
        /// Collapse the relative point into an absolute point given the buffer's dimensions.
        /// </summary>
        /// <param name="Width">Logical width of the buffer.</param>
        /// <param name="Height">Logical height of the buffer.</param>
        public void CreateAbsolutePoint (int Width, int Height)
        {
            AbsolutePoint.X = Width * RelativePoint.X;
            AbsolutePoint.Y = Height * RelativePoint.Y;
        }

        /// <summary>
        /// Given another coodinate (defined by <paramref name="OtherX"/>,<paramref name="OtherY"/>), return the distance from
        /// the current point.
        /// </summary>
        /// <param name="OtherX">Other horizontal coordinate.</param>
        /// <param name="OtherY">Other vertical coordinate.</param>
        /// <returns>Distance from this point to (<paramref name="OtherX"/>,<paramref name="OtherY"/>).</returns>
        public double DistanceFrom (double OtherX, double OtherY)
        {
            return Math.Sqrt(Math.Pow(OtherX - AbsolutePoint.X, 2) + Math.Pow(OtherY - AbsolutePoint.Y, 2));
        }

        /// <summary>
        /// Return the percent out from the point <paramref name="OtherX"/>,<paramref name="OtherY"/> is in relation
        /// to the Radius (if <paramref name="UseRadius"/> is true) or the Hypotenuse.
        /// </summary>
        /// <param name="OtherX">The other point's absolute horizontal location.</param>
        /// <param name="OtherY">The other point's absolute vertical location.</param>
        /// <returns>The percent distance from the point.</returns>
        public double Percent (double OtherX, double OtherY)
        {
            if (UseRadius)
                return RadiusPercent(OtherX, OtherY);
            else
                return HypotenusePercent(OtherX, OtherY);
        }

        /// <summary>
        /// Return the percent out from the point <paramref name="OtherX"/>,<paramref name="OtherY"/> is in relation to the
        /// <seealso cref="Hypotenuse"/>.
        /// </summary>
        /// <param name="OtherX">The other point's absolute horizontal location.</param>
        /// <param name="OtherY">The other point's absolute vertical location.</param>
        /// <returns>The percent distance from the point along the Hypotenuse.</returns>
        public double HypotenusePercent (double OtherX, double OtherY)
        {
            if (Hypotenuse == 0.0)
                throw new DivideByZeroException("No hypotenuse specified.");
            double Distance = DistanceFrom(OtherX, OtherY);
            return Distance / Hypotenuse;
        }

        /// <summary>
        /// Return the percent out from the point <paramref name="OtherX"/>,<paramref name="OtherY"/> is in relation to the
        /// <seealso cref="Radius"/>.
        /// </summary>
        /// <param name="OtherX">The other point's absolute horizontal location.</param>
        /// <param name="OtherY">The other point's absolute vertical location.</param>
        /// <returns>The percent distance from the point along the Radius.</returns>
        public double RadiusPercent (double OtherX, double OtherY)
        {
            if (Radius == 0.0)
                throw new DivideByZeroException("No radius specified.");
            double Distance = DistanceFrom(OtherX, OtherY);
            return Distance / Radius;
        }

        /// <summary>
        /// Return the color at <paramref name="OtherX"/>,<paramref name="OtherY"/> in terms of the location of this color
        /// and the color at this location.
        /// </summary>
        /// <param name="OtherX">Other point's horizontal coordinate.</param>
        /// <param name="OtherY">Other point's vertical coordinate.</param>
        /// <returns>The calculated color at <paramref name="OtherX"/>,<paramref name="OtherY"/>.</returns>
        public Color ColorAt (double OtherX, double OtherY)
        {
            double ColorPercent = Percent(OtherX, OtherY);
            Color NewColor = Color.FromRgb((byte)(PointColor.R * ColorPercent),
                                           (byte)(PointColor.G * ColorPercent),
                                           (byte)(PointColor.B * ColorPercent));
            return NewColor;
        }

        /// <summary>
        /// Convert the color into a hex color string.
        /// </summary>
        /// <returns>Hex color string.</returns>
        public override string ToString ()
        {
            string sval = "#";
            sval += PointColor.A.ToString("x2");
            sval += PointColor.R.ToString("x2");
            sval += PointColor.G.ToString("x2");
            sval += PointColor.B.ToString("x2");
            return sval;
        }

        /// <summary>
        /// Convert a string value containing a hex color value into an integer. Assign the color from the value.
        /// </summary>
        /// <param name="RawValue">The string representation of the hex value of the color.</param>
        /// <returns>The actual numeric value of <paramref name="RawValue"/>.</returns>
        public UInt32 FromString (string RawValue)
        {
            if (string.IsNullOrEmpty(RawValue))
            {
                PointColor = Color.FromArgb(0, 0, 0, 0);
                return 0;
            }
            if (RawValue[0] == '#')
                RawValue = RawValue.Substring(1);
            UInt32 ActualValue = 0x0;
            List<byte> parts = ColorParts(RawValue, ref ActualValue);
            PointColor = Color.FromArgb(parts[0], parts[1], parts[2], parts[3]);
            return ActualValue;
        }

        /// <summary>
        /// Convert <paramref name="RawValue"/> into a list of bytes representing color channels.
        /// </summary>
        /// <param name="RawValue">The string to convert.</param>
        /// <param name="ActualValue">Will contain the integer value of <paramref name="RawValue"/> on exit.</param>
        /// <returns>List of color channel values in ARGB order.</returns>
        private List<byte> ColorParts (string RawValue, ref UInt32 ActualValue)
        {
            byte A = 0xff;
            byte R = 0x0;
            byte G = 0x0;
            byte B = 0x0;
            List<byte> Parts = new List<byte>() { 0xff, 0x0, 0x0, 0x0 };
            UInt32 FullRaw = Convert.ToUInt32(RawValue, 16);
            ActualValue = FullRaw;
            A = (byte)((FullRaw & 0xff000000) >> 24);
            R = (byte)((FullRaw & 0x00ff0000) >> 16);
            G = (byte)((FullRaw & 0x0000ff00) >> 8);
            B = (byte)((FullRaw & 0x000000ff) >> 0);
            Parts.Clear();
            Parts.Add(A);
            Parts.Add(R);
            Parts.Add(G);
            Parts.Add(B);
            return Parts;
        }

        /// <summary>
        /// Return a structure with all of the needed information to be passed to the native function.
        /// </summary>
        /// <returns>Structure with pure color information.</returns>
        public ColorBlenderInterface.PureColorType ToStructure ()
        {
            ColorBlenderInterface.PureColorType Final = new ColorBlenderInterface.PureColorType
            {
                X = (int)AbsolutePoint.X,
                Y = (int)AbsolutePoint.Y,
                Alpha = PointColor.A,
                Red = PointColor.R,
                Green = PointColor.G,
                Blue = PointColor.B,
                Hypotenuse = Hypotenuse,
                Radius = Radius,
                AlphaStart = AlphaStart,
                AlphaEnd = AlphaEnd,
                UseRadius = UseRadius,
                UseAlpha = UseAlpha,
                Top = (int)Top,
                Left = (int)Left,
                Width = IntWidth,
                Height = IntHeight,
                DrawHorizontalIndicators = DrawHorizontalIndicators,
                DrawVerticalIndicators = DrawVerticalIndicators,
                DrawPointIndicator = DrawPointIndicator
            };
            return Final;
        }

        /// <summary>
        /// Return a value between 0.0 and 1.0.
        /// </summary>
        /// <returns>Random normal value.</returns>
        private double RandomNormal ()
        {
            int intRand = Rand.Next(0, 1000);
            return (double)intRand / 1000.0;
        }

        /// <summary>
        /// Alternative random normal method.
        /// </summary>
        /// <param name="Rand">Random number generator to use.</param>
        /// <returns>Random normal value.</returns>
        private double RandomNormal (Random Rand)
        {
            int intRand = Rand.Next(0, 1000);
            return (double)intRand / 1000.0;
        }

        /// <summary>
        /// Return a random boolean value.
        /// </summary>
        /// <returns>Random boolean value.</returns>
        private bool RandomBoolean ()
        {
            return Rand.Next(10000) > 5000 ? true : false;
        }

        /// <summary>
        /// Alternative random boolean value generator.
        /// </summary>
        /// <param name="Rand">Random number generator to use.</param>
        /// <returns>Random boolean value.</returns>
        private bool RandomBoolean (Random Rand)
        {
            return Rand.Next(10000) > 5000 ? true : false;
        }

        /// <summary>
        /// Randomize the color point.
        /// </summary>
        public void DoRandomize ()
        {
            PointColor = RandomColor(0xa0, 0xff);
            RelativePoint.X = RandomNormal();
            RelativePoint.Y = RandomNormal();
            AlphaStart = RandomNormal();
            AlphaEnd = RandomNormal();
            UseAlpha = RandomBoolean();
            UseRadius = RandomBoolean();
            Radius = (double)Rand.Next(20, 200);
        }

        /// <summary>
        /// Alternative color point randomization method.
        /// </summary>
        /// <param name="Rand">Random number generator to use.</param>
        public void DoRandomize (Random Rand)
        {
            PointColor = RandomColor(0xa0, 0xff, Rand);
            RelativePoint.X = RandomNormal(Rand);
            RelativePoint.Y = RandomNormal(Rand);
            AlphaStart = RandomNormal(Rand);
            AlphaEnd = RandomNormal(Rand);
            UseAlpha = RandomBoolean(Rand);
            UseRadius = RandomBoolean(Rand);
            Radius = (double)Rand.Next(20, 200);
        }

        /// <summary>
        /// Return a randomly constructed color.
        /// </summary>
        /// <param name="LowValue">Lowest legal value for any channel.</param>
        /// <param name="HighValue">Highest legal value for any channel.</param>
        /// <param name="IgnoreAlpha">Determines if the alpha channel is ignored. If so, the alpha value will be 0xff.</param>
        /// <returns>Color constructed with random values.</returns>
        private Color RandomColor (byte LowValue, byte HighValue, bool IgnoreAlpha = true)
        {
            if (LowValue > HighValue)
                throw new InvalidOperationException("Low and High values mixed up.");
            List<Byte> Channels = new List<byte>();
            for (int i = 0; i < 4; i++)
                Channels.Add((byte)Rand.Next((int)LowValue, (int)HighValue));
            if (IgnoreAlpha)
                Channels[0] = 0xff;
            Color Final = Color.FromArgb(Channels[0], Channels[1], Channels[2], Channels[3]);
            return Final;
        }

        /// <summary>
        /// Alternative random color generation method.
        /// </summary>
        /// <param name="LowValue">Lowest legal value for any channel.</param>
        /// <param name="HighValue">Highest legal value for any channel.</param>
        /// <param name="Rand">Random number generator to use.</param>
        /// <param name="IgnoreAlpha">Determines if the alpha channel is ignored. If so, the alpha value will be 0xff.</param>
        /// <returns>Color constructed with random values.</returns>
        private Color RandomColor (byte LowValue, byte HighValue, Random Rand, bool IgnoreAlpha = true)
        {
            if (LowValue > HighValue)
                throw new InvalidOperationException("Low and High values mixed up.");
            List<Byte> Channels = new List<byte>();
            for (int i = 0; i < 4; i++)
                Channels.Add((byte)Rand.Next((int)LowValue, (int)HighValue));
            if (IgnoreAlpha)
                Channels[0] = 0xff;
            Color Final = Color.FromArgb(Channels[0], Channels[1], Channels[2], Channels[3]);
            return Final;
        }
    }
}
