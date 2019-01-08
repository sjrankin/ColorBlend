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
    public class MotionVector
    {
        public MotionVector (int InitialX, int InitialY)
        {
            CurrentX = InitialX;
            CurrentY = InitialY;
            StartX = InitialX;
            StartY = InitialY;
            Increment = 0.01;
            MinX = 0;
            MinY = 0;
            MaxX = InitialX + 100;
            MaxY = InitialY + 100;
            VectorComplete = false;
        }

        public PointEx Start
        {
            get
            {
                return new PointEx(StartX, StartY);
            }
        }

        public PointEx Destination
        {
            get
            {
                return new PointEx(DestinationX, DestinationY);
            }
        }

        public PointEx Current
        {
            get
            {
                return new PointEx(CurrentX, CurrentY);
            }
        }

        public double Increment { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int StartX { get; internal set; }
        public int StartY { get; internal set; }
        public int DestinationX { get; internal set; }
        public int DestinationY { get; internal set; }
        public double HorizontalDistance { get; internal set; }
        public double VerticalDistance { get; internal set; }

        private Random Rand = new Random();
        public void StartNewVector (int Steps, Nullable<int> NewStartX = null, Nullable<int> NewStartY = null, Nullable<int> Increment = null)
        {
            this.Milliseconds = Milliseconds;
            if (NewStartX.HasValue)
                StartX = NewStartX.Value;
            else
                StartX = CurrentX;
            if (NewStartY.HasValue)
                StartY = NewStartY.Value;
            else
                StartY = CurrentY;
            if (Increment.HasValue)
                this.Increment = Increment.Value;
            DestinationX = Rand.Next(MinX, MaxX);
            DestinationY = Rand.Next(MinY, MaxY);
            VectorDistance = Math.Sqrt(Math.Pow(StartX - DestinationX, 2) + Math.Pow(StartY - DestinationY, 2));
            HorizontalDistance = (int)Math.Abs(StartX - DestinationX);
            VerticalDistance = (int)Math.Abs(StartY - DestinationY);
            this.Steps = Steps;
            StepCount = 0;
            VectorComplete = false;
        }
        public int Steps { get; internal set; }
        public int StepCount { get; internal set; }
        public int Milliseconds { get; set; }

        public double VectorDistance { get; internal set; }

        public bool UpdateVector ()
        {
            if (VectorComplete)
                return false;
            XDirection = StartX < DestinationX ? 1 : -1;
            YDirection = StartY < DestinationX ? 1 : -1;
            double DistancePercentage = (double)StepCount / (double)Steps;
            CurrentX = (int)(StartX + (XDirection * HorizontalDistance * DistancePercentage));
            CurrentY = (int)(StartY + (YDirection * VerticalDistance * DistancePercentage));
            if (StepCount >= Steps)
            {
                VectorComplete = true;
                return false;
            }
            StepCount++;
            return true;
        }

        public int XDirection { get; internal set; }

        public int YDirection { get; internal set; }

        public void StopVector ()
        {
            VectorComplete = true;
        }

        public bool VectorComplete { get; internal set; }

        public int CurrentX { get; internal set; }

        public int CurrentY { get; internal set; }
    }
}
