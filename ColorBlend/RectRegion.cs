using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Defines a rectangular region.
    /// </summary>
    public class RectRegion : IEquatable<RectRegion>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RectRegion ()
        {
            CommonInitialization();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="UpperLeft">Upper-left coordinate of the region.</param>
        /// <param name="LowerRight">Lower-right coordinate of the region.</param>
        public RectRegion (PointEx UpperLeft, PointEx LowerRight)
        {
            CommonInitialization();
            if (UpperLeft == null)
                throw new ArgumentNullException("UpperLeft");
            if (LowerRight == null)
                throw new ArgumentNullException("LowerRight");
            if (UpperLeft > LowerRight)
                throw new InvalidOperationException("Upper left is greater than lower right.");
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="UpperLeft">Upper-left coordinate of the region.</param>
        /// <param name="LowerRight">Lower-right coordinate of the region.</param>
        /// <param name="IsNormal">Determines if coordinates are normal values.</param>
        /// <param name="LockNormal">Determines if coordinates are locked as normal values.</param>
        public RectRegion (PointEx UpperLeft, PointEx LowerRight, bool IsNormal, bool LockNormal)
        {
            CommonInitialization(IsNormal, LockNormal);
            if (UpperLeft == null)
                throw new ArgumentNullException("UpperLeft");
            if (LowerRight == null)
                throw new ArgumentNullException("LowerRight");
            if (UpperLeft > LowerRight)
                throw new InvalidOperationException("Upper left is greater than lower right.");
        }

        /// <summary>
        /// Initialize the region.
        /// </summary>
        /// <param name="IsNormal">Coordinates are normals flag.</param>
        /// <param name="LockNormal">Lock as normal flag.</param>
        private void CommonInitialization (bool IsNormal = false, bool LockNormal = false)
        {
            _IsNormal = IsNormal;
            _UpperLeft = new PointEx(0, 0, IsNormal, LockNormal);
            _UpperLeft.PointChangeEvent += ValidateUpperLeftPoint;
            _LowerRight = new PointEx(0, 0, IsNormal, LockNormal);
            _LowerRight.PointChangeEvent += ValidateLowerRightPoint;
        }

        private bool _IsNormal = false;
        /// <summary>
        /// Get the normal flag - if true, coordinates are all normals. Must be set via a constructor.
        /// </summary>
        public bool IsNormal
        {
            get
            {
                return _IsNormal;
            }
        }

        /// <summary>
        /// Handle point change events for the lower-right coordinate.
        /// </summary>
        /// <param name="Sender">The lower-right point.</param>
        /// <param name="e">Event data.</param>
        private void ValidateLowerRightPoint (object Sender, PointChangeEventArgs e)
        {
            PointEx PE = Sender as PointEx;
            if (PE == null)
                return;
            double wX = e.NewX.HasValue ? e.NewX.Value : PE.X;
            double wY = e.NewY.HasValue ? e.NewY.Value : PE.Y;
            PointEx W = new PointEx(wX, wY);
            if (W < UpperLeft)
                e.CancelChange = true;
            else
                e.CancelChange = false;
        }

        /// <summary>
        /// Handle point change events for the upper-left coordinate.
        /// </summary>
        /// <param name="Sender">The upper-left point.</param>
        /// <param name="e">Event data.</param>
        private void ValidateUpperLeftPoint (object Sender, PointChangeEventArgs e)
        {
            PointEx PE = Sender as PointEx;
            if (PE == null)
                return;
            double wX = e.NewX.HasValue ? e.NewX.Value : PE.X;
            double wY = e.NewY.HasValue ? e.NewY.Value : PE.Y;
            PointEx W = new PointEx(wX, wY);
            if (W > LowerRight)
                e.CancelChange = true;
            else
                e.CancelChange = false;
        }

        private PointEx _UpperLeft;
        /// <summary>
        /// Get or set the upper-left point. 
        /// </summary>
        public PointEx UpperLeft
        {
            get
            {
                return _UpperLeft;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value > LowerRight)
                    throw new InvalidOperationException("Upper left may not be greater than lower right.");
                UpperLeft.X = value.X;
                UpperLeft.Y = value.Y;
            }
        }

        /// <summary>
        /// Try to set the upper-left point.
        /// </summary>
        /// <param name="NewPoint">The point whose value will be set.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool TrySetUpperLeft (PointEx NewPoint)
        {
            if (NewPoint == null)
                return false;
            if (NewPoint > LowerRight)
                return false;
            UpperLeft.X = NewPoint.X;
            UpperLeft.Y = NewPoint.Y;
            return true;
        }

        private PointEx _LowerRight;
        /// <summary>
        /// Get or set the lower-right point. 
        /// </summary>
        public PointEx LowerRight
        {
            get
            {
                return _LowerRight;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value < UpperLeft)
                    throw new InvalidOperationException("Lower right may not be less than upper left.");
                LowerRight.X = value.X;
                LowerRight.Y = value.Y;
            }
        }

        /// <summary>
        /// Try to set the lower-right point.
        /// </summary>
        /// <param name="NewPoint">The point whose value will be set.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool TrySetLowerRight (PointEx NewPoint)
        {
            if (NewPoint == null)
                return false;
            if (NewPoint < UpperLeft)
                return false;
            LowerRight.X = NewPoint.X;
            LowerRight.Y = NewPoint.Y;
            return true;
        }

        /// <summary>
        /// Determines if this region fully contains <paramref name="OtherRegion"/>.
        /// </summary>
        /// <param name="OtherRegion">The region to test.</param>
        /// <returns>True if this region contains <paramref name="OtherRegion"/>, false if not.</returns>
        public bool Contains (RectRegion OtherRegion)
        {
            if (OtherRegion == null)
                return false;
            if (OtherRegion.IsNormal && !this.IsNormal)
                return false;
            return (OtherRegion.UpperLeft > this.UpperLeft && OtherRegion.LowerRight < this.LowerRight);
        }

        /// <summary>
        /// Determines if this region is fully contained in <paramref name="OtherRegion"/>.
        /// </summary>
        /// <param name="OtherRegion">The region to test.</param>
        /// <returns>True if this region is contained in <paramref name="OtherRegion"/>, false if not.</returns>
        public bool ContainedIn (RectRegion OtherRegion)
        {
            if (OtherRegion == null)
                return false;
            if (OtherRegion.IsNormal && !this.IsNormal)
                return false;
            return (OtherRegion.UpperLeft < this.UpperLeft && OtherRegion.LowerRight > this.LowerRight);
        }

        /// <summary>
        /// Determines if this region has the same coordinate as <paramref name="OtherRegion"/>.
        /// </summary>
        /// <param name="OtherRegion">The other region to test.</param>
        /// <returns>True if the regions are equal, false if not.</returns>
        public bool Equals (RectRegion OtherRegion)
        {
            if (OtherRegion == null)
                return false;
            if (OtherRegion.IsNormal && !this.IsNormal)
                return false;
            return (OtherRegion.UpperLeft == this.UpperLeft && OtherRegion.LowerRight == this.LowerRight);
        }

        /// <summary>
        /// Determines if this region overlaps <paramref name="OtherRegion"/>.
        /// </summary>
        /// <param name="OtherRegion">The other region.</param>
        /// <returns>True if this region overlaps <paramref name="OtherRegion"/>, false if not.</returns>
        public bool Overlaps (RectRegion OtherRegion)
        {
            if (OtherRegion == null)
                return false;
            if (OtherRegion.IsNormal && !this.IsNormal)
                return false;
            return (this.UpperLeft.X < OtherRegion.UpperLeft.X && this.LowerRight.X > OtherRegion.LowerRight.X &&
                this.UpperLeft.Y < OtherRegion.UpperLeft.Y && this.LowerRight.Y > OtherRegion.LowerRight.Y);
        }

        /// <summary>
        /// Get the absolute height of the region.
        /// </summary>
        public double Height
        {
            get
            {
                return Math.Abs(LowerRight.Y - UpperLeft.Y);
            }
        }

        /// <summary>
        /// Get the absolute width of the region.
        /// </summary>
        public double Width
        {
            get
            {
                return Math.Abs(LowerRight.X - UpperLeft.X);
            }
        }

        /// <summary>
        /// Get the area of the region.
        /// </summary>
        public double Area
        {
            get
            {
                return Width * Height;
            }
        }

        /// <summary>
        /// Override subtraction.
        /// </summary>
        /// <param name="Rect1">First rectangle.</param>
        /// <param name="Rect2">Second rectangle.</param>
        /// <returns>New rectangle with coordinates subtracted.</returns>
        public static RectRegion operator -(RectRegion Rect1, RectRegion Rect2)
        {
            RectRegion Result = new RectRegion
            {
                UpperLeft = Rect1.UpperLeft - Rect2.UpperLeft
            };
            return Result;
        }

        /// <summary>
        /// Override addition.
        /// </summary>
        /// <param name="Rect1">First rectangle.</param>
        /// <param name="Rect2">Second rectangle.</param>
        /// <returns>New rectangle with coordinates added.</returns>
        public static RectRegion operator +(RectRegion Rect1, RectRegion Rect2)
        {
            RectRegion Result = new RectRegion
            {
                UpperLeft = Rect1.UpperLeft - Rect2.UpperLeft
            };
            return Result;
        }

        /// <summary>
        /// Override less-than comparison. Compares areas.
        /// </summary>
        /// <param name="Rect1">First rectangle.</param>
        /// <param name="Rect2">Second rectangle.</param>
        /// <returns>True if <paramref name="Point1"/> is less than <paramref name="Point2"/>, false if not.</returns>
        public static bool operator <(RectRegion Point1, RectRegion Point2)
        {
            return Point1.Area < Point2.Area;
        }

        /// <summary>
        /// Override less-than-or-equal comparison. Compares areas.
        /// </summary>
        /// <param name="Rect1">First rectangle.</param>
        /// <param name="Rect2">Second rectangle.</param>
        /// <returns>True if <paramref name="Point1"/> is less than or equal to <paramref name="Point2"/>, false if not.</returns>
        public static bool operator <=(RectRegion Point1, RectRegion Point2)
        {
            return Point1.Area <= Point2.Area;
        }

        /// <summary>
        /// Override greater-than comparison. Compares areas.
        /// </summary>
        /// <param name="Rect1">First rectangle.</param>
        /// <param name="Rect2">Second rectangle.</param>
        /// <returns>True if <paramref name="Point1"/> is greater than <paramref name="Point2"/>, false if not.</returns>
        public static bool operator >(RectRegion Point1, RectRegion Point2)
        {
            return Point1.Area > Point2.Area;
        }

        /// <summary>
        /// Override greater-than-or-equal comparison. Compares areas.
        /// </summary>
        /// <param name="Rect1">First rectangle.</param>
        /// <param name="Rect2">Second rectangle.</param>
        /// <returns>True if <paramref name="Point1"/> is greater than or equal to <paramref name="Point2"/>, false if not.</returns>
        public static bool operator >=(RectRegion Point1, RectRegion Point2)
        {
            return Point1.Area >= Point2.Area;
        }
    }
}
