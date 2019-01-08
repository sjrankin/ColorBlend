using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Iro3.Selectors
{
    public partial class Rotational
    {
        #region Control Event Handling
        private void HandleMouseMoveInRotator (object Sender, MouseEventArgs e)
        {
            Ellipse ERot = Sender as Ellipse;
            if (ERot == null)
                return;
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            Point RMouseLocation = e.GetPosition(ERot);
            double NewAngle = GetPointAngle(RMouseLocation);
            RotateControl(NewAngle);
            UpdateValue(NewAngle);
            Value = NewAngle;
        }

        private double GetPointAngle(Point Spot)
        {
            double Angle = Math.Atan2(Spot.Y - (Height / 2.0), Spot.X - (Width / 2.0));
            return Angle;
        }

        private void HandleMouseExitedRotator (object Sender, MouseEventArgs e)
        {
            if (ShowValue)
            {
            }
        }

        private void HandleMouseEnteredRotator (object Sender, MouseEventArgs e)
        {
            if (ShowValue)
            {
            }
        }
        #endregion

        #region Event messaging
        private void UpdateValue (double NewValue)
        {
            if (RotationalValueChangedEvent == null)
                return;
            RotationalValueChangedEventArgs args = new RotationalValueChangedEventArgs(NewValue);
            RotationalValueChangedEvent(this, args);
        }

        public delegate void HandleRotationalValueChanged (object Sender, RotationalValueChangedEventArgs e);
        public event HandleRotationalValueChanged RotationalValueChangedEvent;
        #endregion
    }

    public class RotationalValueChangedEventArgs : EventArgs
    {
        public RotationalValueChangedEventArgs () : base()
        {
            NewValue = 0.0;
            CancelChange = false;
        }

        public RotationalValueChangedEventArgs (double NewValue) : base()
        {
            this.NewValue = NewValue;
            CancelChange = false;
        }

        public double NewValue { get; internal set; }

        public bool CancelChange { get; set; }
    }
}
