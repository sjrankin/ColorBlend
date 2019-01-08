using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ColorBlend
{
    /// <summary>
    /// Random walker class. Supplies points that appear to be a random walk.
    /// </summary>
    /// <remarks>
    /// http://www.coderanch.com/t/480970/Game-Development/java/plot-curve-points
    /// http://en.wikipedia.org/wiki/B%C3%A9zier_curve
    /// </remarks>
    public class RandomWalker
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Width">Width of the walking plane.</param>
        /// <param name="Height">Height of the walking plane.</param>
        public RandomWalker (int Width, int Height)
        {
            Point1 = null;
            Point2 = null;
            Bezier = null;
            Rand = new Random();
            this.Width = Width;
            this.Height = Height;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="GlobalRand">Random number generator to use.</param>
        /// <param name="Width">Width of the walking plane.</param>
        /// <param name="Height">Height of the walking plane.</param>
        public RandomWalker (Random GlobalRand, int Width, int Height)
        {
            if (GlobalRand == null)
                throw new ArgumentNullException("GlobalRand");
            Point1 = null;
            Point2 = null;
            Bezier = null;
            Rand = GlobalRand;
            this.Width = Width;
            this.Height = Height;
        }

        /// <summary>
        /// Get or set the width of the motion plane.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Get or set the height of the motion plane.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The increment between Bezier points.
        /// </summary>
        private double Increment { get; set; }

        /// <summary>
        /// Start walking. New points will be returned via <seealso cref="NewPointAvailableEvent"/>.
        /// </summary>
        /// <param name="InitialPoint">Initial line segment's starting point.</param>
        /// <param name="FinalPoint">Initial line segment's ending point.</param>
        /// <param name="Frequency">Frequency of point generation. Value is in milliseconds.</param>
        /// <param name="Increment">Increment used to move the point. Normalized (e.g., must be between 0.0 and 1.0.</param>
        public void StartWalking (PointEx InitialPoint, PointEx FinalPoint, int Frequency, double Increment)
        {
            if (Clock != null)
            {
                Clock.Stop();
                Clock.IsEnabled = false;
                Clock = null;
            }
            this.Increment = Increment;
            SetPoints(InitialPoint, FinalPoint);
            Clock = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(Frequency)
            };
            Clock.Tick += ClockTick;
            Clock.IsEnabled = true;
            Clock.Start();
        }

        /// <summary>
        /// Stop's the random walk. Resets class.
        /// </summary>
        /// <returns>The last point on the Bezier curve that was calculated.</returns>
        public PointEx StopWalking ()
        {
            if (Clock == null)
                return null;
            Clock.Stop();
            Clock = null;
            Point1 = null;
            Point2 = null;
            Bezier = null;
            _Incrementor = 0.0;
            return LastPoint;
        }

        /// <summary>
        /// Sets the end-points of the current segment and generates a random Bezier point between them.
        /// </summary>
        /// <param name="Point1">Starting point of the current segment.</param>
        /// <param name="Point2">Ending point of the current segment.</param>
        private void SetPoints (PointEx Point1, PointEx Point2)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
#if false
            this.Bezier = CreateBezierPoint(Point1, Point2);
#else
            this.Bezier = MakeBezierSeedPoint(Point1, Point2);
#endif
        }

        private PointEx MakeBezierSeedPoint (PointEx Point1, PointEx Point2)
        {
            PointEx BSeed = new PointEx();
            return BSeed;
        }

        double _Incrementor = 0.0;

        /// <summary>
        /// Generate a new point along the Bezier curve.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ClockTick (object Sender, EventArgs e)
        {
            PointEx NewBezier = BezierPoint(_Incrementor);
            _Incrementor += Increment;
            LastPoint = new PointEx(NewBezier.X, NewBezier.Y);
            bool NewSegmentStarted = false;
            if (NewPointAvailableEvent != null)
            {
                NewPointAvailableEventArgs args = new NewPointAvailableEventArgs(NewBezier.X,NewBezier.Y, NewSegmentStarted);
                NewPointAvailableEvent(this, args);
            }
            if (PointEx.CloseEnough(NewBezier, Point2, 1))
            {
                //We're at (or near enough) to the line's ending point. Start a new line segment with the old
                //line segment's ending point as the starting point for the new segment.
                SetPoints(Point2, PointEx.RandomPoint(Rand, 0, Width, 0, Height));
                NewSegmentStarted = true;
                _Incrementor = 0.0;
            }
        }

        internal PointEx LastPoint = null;
        private DispatcherTimer Clock = null;
        internal Random Rand;
        internal PointEx Point1 = null;
        internal PointEx Point2 = null;
        internal PointEx Bezier = null;

        /// <summary>
        /// Create a point on the Bezier curve defined by the values <seealso cref="Point1"/>, <seealso cref="Point2"/>,
        /// and <seealso cref="Bezier"/>.
        /// </summary>
        /// <param name="Increment">Determines where along the curve to generate the point. Must be normalized.</param>
        /// <returns>New point on the Bezier curve.</returns>
        public PointEx BezierPoint (double Increment)
        {
            if (Point1 == null || Point2 == null || Bezier == null)
                return null;

            double x = ((1 - Increment) * (1 - Increment) * Point1.IntX + 
                2 * (1 - Increment) * Increment * Bezier.IntX + 
                Increment * Increment * Point2.IntX);
            double y = ((1 - Increment) * (1 - Increment) * Point1.IntY + 
                2 * (1 - Increment) * Increment * Bezier.IntY + 
                Increment * Increment * Point2.IntY);

            return new PointEx(x, y);
        }

        /// <summary>
        /// Creates a random point in the plane defined by (Point1.X,Point1.Y),(Point2.X,Point2.Y).
        /// </summary>
        /// <param name="Point1">Plane's first point.</param>
        /// <param name="Point2">Plane's second point.</param>
        /// <returns>Random point somewhere in the plane.</returns>
        internal PointEx CreateBezierPoint (PointEx Point1, PointEx Point2)
        {
            PointEx BP = new PointEx();
            int SmallestX = (int)PointEx.SmallestX(Point1, Point2);
            int LargestX = (int)PointEx.LargestX(Point1, Point2);
            int SmallestY = (int)PointEx.SmallestY(Point1, Point2);
            int LargestY = (int)PointEx.LargestY(Point1, Point2);
            BP = PointEx.RandomPoint(Rand, SmallestX, LargestX, SmallestY, LargestY);
            return BP;
        }

        /// <summary>
        /// Delegate definition for new Bezier point available events.
        /// </summary>
        /// <param name="Sender">The <seealso cref="RandomWalker"/> class that has the new point.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleNewPointAvailableEvents (object Sender, NewPointAvailableEventArgs e);
        /// <summary>
        /// Triggered when a new point is available on the defined Bezier curve.
        /// </summary>
        public event HandleNewPointAvailableEvents NewPointAvailableEvent;
    }

    /// <summary>
    /// Event data for new Bezier point available events.
    /// </summary>
    public class NewPointAvailableEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NewPointAvailableEventArgs () : base()
        {
            NewPoint = new PointEx(0, 0);
            NewSegment = false;
        }

        public NewPointAvailableEventArgs (double X, double Y,bool NewSegment) : base()
        {
            this.NewPoint = new PointEx(X, Y);
            this.NewSegment = NewSegment;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NewPoint">The new Bezier point.</param>
        /// <param name="NewSegment">Flag indicating a new segment has started.</param>
        public NewPointAvailableEventArgs (PointEx NewPoint, bool NewSegment) :base()
        {
            this.NewPoint = new PointEx(NewPoint);
            this.NewSegment = NewSegment;
        }

        /// <summary>
        /// Get the new Bezier point.
        /// </summary>
        public PointEx NewPoint { get; internal set; }

        /// <summary>
        /// If true, a new line segment has been started.
        /// </summary>
        public bool NewSegment { get; internal set; }
    }
}
