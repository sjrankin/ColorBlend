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
    /// Encapsulates a point.
    /// </summary>
    [DebuggerDisplay("({IntX},{IntY})")]
    public class PointEx : IEquatable<PointEx>
    {
        /// <summary>
        /// Constructor. Sets the point to (0,0).
        /// </summary>
        public PointEx ()
        {
            X = 0.0;
            Y = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="X">Original horizontal coordinate.</param>
        /// <param name="Y">Original vertical coordinate.</param>
        public PointEx (double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="X">Original horizontal coordinate.</param>
        /// <param name="Y">Original vertical coordinate.</param>
        /// <param name="IsNormal">Determines if coordinates are normalized.</param>
        public PointEx (double X, double Y, bool IsNormal)
        {
            this.IsNormal = IsNormal;
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="X">Original horizontal coordinate.</param>
        /// <param name="Y">Original vertical coordinate.</param>
        /// <param name="IsNormal">Determines if coordinates are normalized.</param>
        /// <param name="LockNormalState">
        /// If true, the normal state (whatever the value of <paramref name="IsNormal"/> is locked and cannot be changed.
        /// </param>
        public PointEx (double X, double Y, bool IsNormal, bool LockNormalState)
        {
            this.IsNormal = IsNormal;
            this.NormalLocked = LockNormalState;
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="SourcePoint">
        /// Point that serves as the source for the coordinates for the newly-instantiated point.
        /// </param>
        public PointEx (PointEx SourcePoint)
        {
            if (SourcePoint == null)
                throw new ArgumentNullException("SourcePoint");
            this.X = SourcePoint.X;
            this.Y = SourcePoint.Y;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Rand">Random number generated used to create random values for X and Y.</param>
        /// <param name="Min">Minimum random number.</param>
        /// <param name="Max">Maximum random number.</param>
        public PointEx (Random Rand, int Min, int Max)
        {
            this.X = Rand.Next(Min, Max);
            this.Y = Rand.Next(Min, Max);
        }

        private double _X = 0.0;
        /// <summary>
        /// Get or set X.
        /// </summary>
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                if (PointChangeEvent != null)
                {
                    PointChangeEventArgs args = new PointChangeEventArgs(value, null);
                    PointChangeEvent(this, args);
                    if (args.CancelChange)
                        return;
                }
                if (IsNormal)
                    _X = Math.Min(Math.Max(value, 0.0), 1.0);
                else
                    _X = value;
            }
        }

        private double _Y = 0.0;
        /// <summary>
        /// Get or set Y.
        /// </summary>
        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                if (PointChangeEvent != null)
                {
                    PointChangeEventArgs args = new PointChangeEventArgs(null, value);
                    PointChangeEvent(this, args);
                    if (args.CancelChange)
                        return;
                }
                if (IsNormal)
                    _Y = Math.Min(Math.Max(value, 0.0), 1.0);
                else
                    _Y = value;
            }
        }

        public bool SetPoint (double X, double Y)
        {
            if (PointChangeEvent != null)
            {
                PointChangeEventArgs args = new PointChangeEventArgs(X, Y);
                PointChangeEvent(this, args);
                if (args.CancelChange)
                    return false;
            }
            this.X = X;
            this.Y = Y;
            return true;
        }

        public bool CopyPoint (PointEx OtherPoint)
        {
            if (OtherPoint == null)
                throw new ArgumentNullException("OtherPoint");
            if (PointChangeEvent != null)
            {
                PointChangeEventArgs args = new PointChangeEventArgs(OtherPoint.X, OtherPoint.Y);
                PointChangeEvent(this, args);
                if (args.CancelChange)
                    return false;
            }
            this.X = OtherPoint.X;
            this.Y = OtherPoint.Y;
            return true;
        }

        /// <summary>
        /// Get X as an integer.
        /// </summary>
        public int IntX
        {
            get
            {
                return (int)X;
            }
        }

        /// <summary>
        /// Get Y as an integer.
        /// </summary>
        public int IntY
        {
            get
            {
                return (int)Y;
            }
        }

        public bool HasNegative
        {
            get
            {
                if (X < 0)
                    return true;
                if (Y < 0)
                    return true;
                return false;
            }
        }

        public bool GreaterThan (double TestX, double TestY)
        {
            if ((X < TestX) && (Y < TestY))
                return true;
            return false;
        }

        public bool EitherGreaterThan (double TestX, double TestY)
        {
            if (X < TestX)
                return true;
            if (Y < TestY)
                return true;
            return false;
        }

        public bool LessThan (double TestX, double TestY)
        {
            if ((X > TestX) && (Y > TestY))
                return true;
            return false;
        }

        public bool EitherLessThan (double TestX, double TestY)
        {
            if (X > TestX)
                return true;
            if (Y > TestY)
                return true;
            return false;
        }

        /// <summary>
        /// Determines if X is equal to <paramref name="XValue"/> and Y is equal to <paramref name="YValue"/>.
        /// </summary>
        /// <param name="XValue">The value to compare against X.</param>
        /// <param name="YValue">The value to compare against Y.</param>
        /// <returns>
        /// True if X is equal to <paramref name="XValue"/> and Y is equal to <paramref name="YValue"/>,
        /// false otherwise.
        /// </returns>
        public bool IsSame (double XValue, double YValue)
        {
            return X == XValue && Y == YValue;
        }

        /// <summary>
        /// Determines if X is equal to <paramref name="XValue"/> and Y is equal to <paramref name="YValue"/>.
        /// </summary>
        /// <param name="XValue">The value to compare against X.</param>
        /// <param name="YValue">The value to compare against Y.</param>
        /// <returns>
        /// True if X is equal to <paramref name="XValue"/> and Y is equal to <paramref name="YValue"/>,
        /// false otherwise.
        /// </returns>
        public bool IsSame (int XValue, int YValue)
        {
            return IntX == XValue && IntY == YValue;
        }

        /// <summary>
        /// Determines if both X and Y are equal to <paramref name="Value"/>.
        /// </summary>
        /// <param name="Value">The value to compare against X and Y.</param>
        /// <returns>True if X and Y are both <paramref name="Value"/>, false otherwise.</returns>
        public bool BothAre (double Value)
        {
            return X == Value && Y == Value;
        }

        /// <summary>
        /// Determines if both X and Y are equal to <paramref name="Value"/>.
        /// </summary>
        /// <param name="Value">The value to compare against X and Y.</param>
        /// <returns>True if X and Y are both <paramref name="Value"/>, false otherwise.</returns>
        public bool BothAre (int Value)
        {
            return IntX == Value && IntY == Value;
        }

        /// <summary>
        /// Determines if neither X or Y is equal to <paramref name="Value"/>.
        /// </summary>
        /// <param name="Value">The value to compare against X and Y.</param>
        /// <returns>True if both X and Y are not <paramref name="Value"/>, false otherwise.</returns>
        public bool NeitherAre (double Value)
        {
            return X != Value && Y != Value;
        }

        /// <summary>
        /// Determines if neither X or Y is equal to <paramref name="Value"/>.
        /// </summary>
        /// <param name="Value">The value to compare against X and Y.</param>
        /// <returns>True if both X and Y are not <paramref name="Value"/>, false otherwise.</returns>
        public bool NeitherAre (int Value)
        {
            return IntX != Value && IntY != Value;
        }

        private bool _IsNormal = false;
        /// <summary>
        /// Determines if the point is normalized or not. If set to true, <paramref name="X"/> and <paramref name="Y"/> are
        /// allowed to range from 0.0 to 1.0 only. When set to true, existing values are clamped. If set to false, existing
        /// values are not changed.
        /// </summary>
        public bool IsNormal
        {
            get
            {
                return _IsNormal;
            }
            set
            {
                if (!_NormalLocked.HasValue)
                {
                    _IsNormal = value;
                    if (_IsNormal)
                    {
                        X = Math.Min(Math.Max(X, 0.0), 1.0);
                        Y = Math.Min(Math.Max(Y, 0.0), 1.0);
                    }
                }
            }
        }

        private Nullable<bool> _NormalLocked = null;
        /// <summary>
        /// Get or set the normal locked state. Once set, this property cannot be changed (changes are ignored). If set to true,
        /// current values are clamped to 0.0 to 1.0.
        /// </summary>
        public bool NormalLocked
        {
            get
            {
                return _NormalLocked.HasValue ? _NormalLocked.Value : false;
            }
            set
            {
                if (_NormalLocked.HasValue)
                    return;
                IsNormal = true;
                _NormalLocked = value;
            }
        }

        /// <summary>
        /// Rotates this point by <paramref name="Angle"/> degrees and returns the result in a new PointEx.
        /// </summary>
        /// <param name="Angle">Degrees to rotate the point.</param>
        /// <returns>Rotated point. Null if this point is normal.</returns>
        public PointEx RotatePoint(double Angle)
        {
            if (IsNormal)
                return null;
            PointEx RPoint = new PointEx
            {
                X = X * Math.Cos(Angle) + Y * Math.Sin(Angle),
                Y = Y * Math.Cos(Angle) - X * Math.Sin(Angle)
            };
            return RPoint;
        }

        /// <summary>
        /// Return a reasonable string representation of the value.
        /// </summary>
        /// <returns></returns>
        public override string ToString ()
        {
            return IntX.ToString() + "," + IntY.ToString();
        }

        public bool Equals (PointEx OtherPoint)
        {
            if (OtherPoint == null)
                return false;
            if (OtherPoint.X != X)
                return false;
            if (OtherPoint.Y != Y)
                return false;
            return true;
        }

        public PointEx Delta (PointEx OtherPoint)
        {
            if (OtherPoint == null)
                throw new ArgumentNullException("OtherPoint");
            PointEx DeltaValue = new PointEx
            {
                X = Math.Abs(X - OtherPoint.X),
                Y = Math.Abs(Y - OtherPoint.Y)
            };
            return DeltaValue;
        }

        public double Distance (PointEx OtherPoint)
        {
            if (OtherPoint == null)
                throw new ArgumentNullException("OtherPoint");
            return Math.Sqrt(Math.Pow(X - OtherPoint.X, 2) + Math.Pow(Y - OtherPoint.Y, 2));
        }

        public static PointEx operator -(PointEx Point1, PointEx Point2)
        {
            PointEx Result = new PointEx
            {
                X = Point1.X - Point2.X,
                Y = Point1.Y - Point2.Y
            };
            return Result;
        }

        public static PointEx operator +(PointEx Point1, PointEx Point2)
        {
            PointEx Result = new PointEx
            {
                X = Point1.X + Point2.X,
                Y = Point1.Y + Point2.Y
            };
            return Result;
        }

        public static PointEx operator *(PointEx Point1, PointEx Point2)
        {
            PointEx Result = new PointEx
            {
                X = Point1.X * Point2.X,
                Y = Point1.Y * Point2.Y
            };
            return Result;
        }

        public static PointEx operator /(PointEx Point1, PointEx Point2)
        {
            if ((Point2.X == 0) || (Point2.Y == 0))
                throw new DivideByZeroException("Second operand contains a zero value.");
            PointEx Result = new PointEx
            {
                X = Point1.X / Point2.X,
                Y = Point1.Y / Point2.Y
            };
            return Result;
        }

        public static PointEx operator %(PointEx Point1, PointEx Point2)
        {
            if ((Point2.X == 0) || (Point2.Y == 0))
                throw new DivideByZeroException("Second operand contains a zero value.");
            PointEx Result = new PointEx
            {
                X = Point1.X % Point2.X,
                Y = Point1.Y % Point2.Y
            };
            return Result;
        }

        public static bool operator <(PointEx Point1, PointEx Point2)
        {
            double P1Distance = Math.Sqrt(Math.Pow(Point1.X, 2) + Math.Pow(Point1.Y, 2));
            double P2Distance = Math.Sqrt(Math.Pow(Point2.X, 2) + Math.Pow(Point2.Y, 2));
            return P1Distance < P2Distance;
        }

        public static bool operator <=(PointEx Point1, PointEx Point2)
        {
            double P1Distance = Math.Sqrt(Math.Pow(Point1.X, 2) + Math.Pow(Point1.Y, 2));
            double P2Distance = Math.Sqrt(Math.Pow(Point2.X, 2) + Math.Pow(Point2.Y, 2));
            return P1Distance <= P2Distance;
        }

        public static bool operator >(PointEx Point1, PointEx Point2)
        {
            double P1Distance = Math.Sqrt(Math.Pow(Point1.X, 2) + Math.Pow(Point1.Y, 2));
            double P2Distance = Math.Sqrt(Math.Pow(Point2.X, 2) + Math.Pow(Point2.Y, 2));
            return P1Distance > P2Distance;
        }

        public static bool operator >=(PointEx Point1, PointEx Point2)
        {
            double P1Distance = Math.Sqrt(Math.Pow(Point1.X, 2) + Math.Pow(Point1.Y, 2));
            double P2Distance = Math.Sqrt(Math.Pow(Point2.X, 2) + Math.Pow(Point2.Y, 2));
            return P1Distance >= P2Distance;
        }

        public PointEx Add (double Vector)
        {
            return new PointEx(X + Vector, Y + Vector);
        }

        public PointEx Subtract (double Vector)
        {
            return new PointEx(X - Vector, Y - Vector);
        }

        public PointEx Multiply (double Vector)
        {
            return new PointEx(X * Vector, Y * Vector);
        }

        public PointEx Divide (double Vector)
        {
            if (Vector == 0)
                throw new DivideByZeroException("Vector is 0.");
            return new PointEx(X / Vector, Y / Vector);
        }

        public PointEx Modulus (double Vector)
        {
            if (Vector == 0)
                throw new DivideByZeroException("Vector is 0.");
            return new PointEx(X % Vector, Y % Vector);
        }

        public PointEx SwapXY ()
        {
            return new PointEx(Y, X);
        }

        /// <summary>
        /// Create a new point with both X and Y clamped to <paramref name="Minimum"/>:<paramref name="Maximum"/>.
        /// </summary>
        /// <param name="Minimum"></param>
        /// <param name="Maximum"></param>
        /// <returns></returns>
        public PointEx Clamp (double Minimum, double Maximum)
        {
            PointEx Result = new PointEx(X, Y);
            if (Result.X < Minimum)
                Result.X = Minimum;
            if (Result.X > Maximum)
                Result.X = Maximum;
            if (Result.Y < Minimum)
                Result.Y = Minimum;
            if (Result.Y > Maximum)
                Result.Y = Maximum;
            return Result;
        }

        public PointEx ClampX (double Minimum, double Maximum)
        {
            PointEx Result = new PointEx(X, Y);
            if (Result.X < Minimum)
                Result.X = Minimum;
            if (Result.X > Maximum)
                Result.X = Maximum;
            return Result;
        }

        public PointEx ClampY (double Minimum, double Maximum)
        {
            PointEx Result = new PointEx(X, Y);
            if (Result.Y < Minimum)
                Result.Y = Minimum;
            if (Result.Y > Maximum)
                Result.Y = Maximum;
            return Result;
        }

        public PointEx ClampMinimum (double Minimum)
        {
            PointEx Result = new PointEx(X, Y);
            if (Result.X < Minimum)
                Result.X = Minimum;
            if (Result.Y < Minimum)
                Result.Y = Minimum;
            return Result;
        }

        public PointEx ClampMaximum (double ClampMaximum)
        {
            PointEx Result = new PointEx(X, Y);
            if (Result.X > ClampMaximum)
                Result.X = ClampMaximum;
            if (Result.Y > ClampMaximum)
                Result.Y = ClampMaximum;
            return Result;
        }

        public PointEx AbsoluteValue ()
        {
            return new PointEx(Math.Abs(X), Math.Abs(Y));
        }

        public static PointEx RandomPoint (Random Rand, int Minimum, int Maximum)
        {
            return RandomPoint(Rand, Minimum, Maximum, Minimum, Maximum);
        }

        public static PointEx RandomPoint (Random Rand, int MinX, int MaxX, int MinY, int MaxY)
        {
            PointEx NewPoint = new PointEx
            {
                X = Rand.Next(MinX, MaxX),
                Y = Rand.Next(MinY, MaxY)
            };
            return NewPoint;
        }

        public static double SmallestX (PointEx Point1, PointEx Point2)
        {
            return Point1.X < Point2.X ? Point1.X : Point2.X;
        }

        public static double LargestX (PointEx Point1, PointEx Point2)
        {
            return Point1.X > Point2.X ? Point1.X : Point2.X;
        }

        public static double SmallestY (PointEx Point1, PointEx Point2)
        {
            return Point1.Y < Point2.Y ? Point1.Y : Point2.Y;
        }

        public static double LargestY (PointEx Point1, PointEx Point2)
        {
            return Point1.Y > Point2.Y ? Point1.Y : Point2.Y;
        }

        public static bool CloseEnough (PointEx Point1, PointEx Point2, int Variance)
        {
            if (Point1.X > Point2.X + Variance)
                return false;
            if (Point1.X < Point2.X - Variance)
                return false;
            if (Point1.Y > Point2.Y + Variance)
                return false;
            if (Point1.Y < Point2.Y - Variance)
                return false;
            return true;
        }

        public double Largest ()
        {
            if (X > Y)
                return X;
            else
                return Y;
        }

        public double Smallest ()
        {
            if (X < Y)
                return X;
            else
                return Y;
        }

        public bool ValuesEqual ()
        {
            return X == Y;
        }

        /// <summary>
        /// Event handler definition for point change events.
        /// </summary>
        /// <param name="Sender">The point that is changing.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandlePointChangeEvents (object Sender, PointChangeEventArgs e);
        /// <summary>
        /// Triggered when a point (either X or Y or both) is changing.
        /// </summary>
        public event HandlePointChangeEvents PointChangeEvent;
    }

    /// <summary>
    /// Event data for point change events.
    /// </summary>
    public class PointChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PointChangeEventArgs () : base()
        {
            NewX = null;
            NewY = null;
            CancelChange = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NewX">New X value.</param>
        /// <param name="NewY">New Y value.</param>
        public PointChangeEventArgs (Nullable<double> NewX, Nullable<double> NewY) : base()
        {
            this.NewX = NewX;
            this.NewY = NewY;
            CancelChange = false;
        }

        /// <summary>
        /// Get the new X value. If null, X hasn't changed.
        /// </summary>
        public Nullable<double> NewX { get; internal set; }

        /// <summary>
        /// Get the new Y value. If null, Y hasn't changed.
        /// </summary>
        public Nullable<double> NewY { get; internal set; }

        /// <summary>
        /// If the recepient sets this property to false, PointEx will not change the point's value.
        /// </summary>
        public bool CancelChange { get; set; }
    }
}
