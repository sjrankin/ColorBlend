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

namespace ColorBlend
{
    /// <summary>
    /// Handles the movement of points on the surface.
    /// </summary>
    public class Meander
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="SurfaceContainer">The control that acts as the container of the surface where drawing takes place.</param>
        /// <param name="PurePoints">List of color points.</param>
        /// <param name="PointIndex">If not null, the index of the color point to move. If null, 0 is used as the index.</param>
        public Meander (Border SurfaceContainer, List<ColorPoint> PurePoints, Nullable<int> PointIndex = null, bool InStepMode = false)
        {
            this.SurfaceContainer = SurfaceContainer;
            this.PurePoints = PurePoints;
            if (PointIndex == null)
                this.PointIndex = 0;
            else
                this.PointIndex = PointIndex.Value;
            this.InStepMode = InStepMode;
        }

        private Border SurfaceContainer = null;
        private List<ColorPoint> PurePoints = null;
        private int PointIndex = 0;

        DispatcherTimer MotionTimer = null;

        public bool InStepMode { get; internal set; }

        /// <summary>
        /// Start moving the specified point.
        /// </summary>
        public void DoStartMotion ()
        {
            if (MotionTimer != null)
                return;
            PurePoints[PointIndex].CreateAbsolutePoint((int)SurfaceContainer.ActualWidth, (int)SurfaceContainer.ActualHeight);
            MV = new MotionVector((int)PurePoints[PointIndex].AbsolutePoint.X, (int)PurePoints[PointIndex].AbsolutePoint.Y)
            {
                MaxX = (int)SurfaceContainer.ActualWidth - 50,
                MaxY = (int)SurfaceContainer.ActualHeight - 50,
                MinX = 50,
                MinY = 50
            };
            MV.StartNewVector(25);
            if (!InStepMode)
            {
                MotionTimer = new DispatcherTimer();
                MotionTimer.Tick += MotionTimerTickHandler;
                MotionTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                MotionTimer.IsEnabled = true;
                MotionTimer.Start();
            }
            UpdateState(MotionEventStates.MotionStarted);
        }

        private MotionVector MV;

        /// <summary>
        /// Handle timer ticks - updates the point's position.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void MotionTimerTickHandler (object Sender, EventArgs e)
        {
            DoStep();
        }

        public void DoStep ()
        {
            bool StillRunning = MV.UpdateVector();
            if (MV.VectorComplete)
            {
                if (MeanderVectorChangeEvent != null)
                {
                    MeanderVectorChangeEvent(this, new MeanderNewVectorEventArgs(MV.Start, MV.Destination));
                }
                MV.StartNewVector(25, MV.DestinationX, MV.DestinationY);
                return;
            }
            PurePoints[PointIndex].AbsolutePoint.X = (double)MV.CurrentX;
            PurePoints[PointIndex].AbsolutePoint.Y = (double)MV.CurrentY;
            string NewPt = ((int)PurePoints[PointIndex].AbsolutePoint.X).ToString() + "x" + ((int)PurePoints[PointIndex].AbsolutePoint.X).ToString();
            NewPt = NewPt + " {" + MV.DestinationX.ToString() + "x" + MV.DestinationY.ToString() + "}";
            if (MeanderPositionChangeEvent != null)
                MeanderPositionChangeEvent(this, new MeanderPositionChangeEventArgs((int)PurePoints[PointIndex].AbsolutePoint.X,
                    (int)PurePoints[PointIndex].AbsolutePoint.Y, PointIndex, NewPt));
        }

        /// <summary>
        /// Stop moving the point.
        /// </summary>
        public void DoEndMotion ()
        {
            if (MotionTimer == null)
                return;
            MotionTimer.Stop();
            MotionTimer.IsEnabled = false;
            MotionTimer = null;
            UpdateState(MotionEventStates.MotionEnded);
        }

        public PointEx StartPoint
        {
            get
            {
                return MV.Start;
            }
        }

        public PointEx DestinationPoint
        {
            get
            {
                return MV.Destination;
            }
        }

        /// <summary>
        /// Trigger a motion state change event.
        /// </summary>
        /// <param name="State">The new state.</param>
        private void UpdateState (MotionEventStates State)
        {
            if (MeanderMotionStateChangeEvent != null)
                MeanderMotionStateChangeEvent(this, new MeanderMotionStateChangedEventArgs(State));
        }

        /// <summary>
        /// Event handler definition for point position update events.
        /// </summary>
        /// <param name="Sender">The instance of the Meander class where the event took place.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleMeanderPositionChangeEvents (object Sender, MeanderPositionChangeEventArgs e);
        /// <summary>
        /// Triggered when a new position is available.
        /// </summary>
        public event HandleMeanderPositionChangeEvents MeanderPositionChangeEvent;

        /// <summary>
        /// Event handler definition for motion state change events.
        /// </summary>
        /// <param name="Sender">The instance of the Meander class where the event took place.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleMeanderMotionStateChangeEvents (object Sender, MeanderMotionStateChangedEventArgs e);
        /// <summary>
        /// Triggered when the motion state changes.
        /// </summary>
        public event HandleMeanderMotionStateChangeEvents MeanderMotionStateChangeEvent;

        /// <summary>
        /// Event handler definition for vector change events.
        /// </summary>
        /// <param name="Sender">The instance of the Meander class where the event took place.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleMeanderVectorChangeEvents (object Sender, MeanderNewVectorEventArgs e);
        /// <summary>
        /// Triggered when the vector changes.
        /// </summary>
        public event HandleMeanderVectorChangeEvents MeanderVectorChangeEvent;
    }

    /// <summary>
    /// Valid motion states.
    /// </summary>
    public enum MotionEventStates
    {
        /// <summary>
        /// Motion stated.
        /// </summary>
        MotionStarted,
        /// <summary>
        /// Motion ended.
        /// </summary>
        MotionEnded,
        /// <summary>
        /// Current moving.
        /// </summary>
        InMotion,
        /// <summary>
        /// Currently not moving.
        /// </summary>
        NotMoving,
        /// <summary>
        /// The point should be removed.
        /// </summary>
        Removed
    }
}
