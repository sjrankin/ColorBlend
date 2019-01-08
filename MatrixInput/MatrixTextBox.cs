using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace Input.MatrixInput
{
    public class MatrixTextBox : TextBox
    {
        public MatrixTextBox () : base()
        {
            CommonInitialization(-1, -1, "");
        }

        public MatrixTextBox (int X, int Y, double Initial) : base()
        {
            CommonInitialization(X, Y, Initial.ToString());
        }

        private void CommonInitialization (int X, int Y, string InitialValue)
        {
            Location = new GridLocation(X, Y);
            this.Text = InitialValue;
            this.KeyDown += HandleKeyPress;
            this.LostFocus += HandleLostFocus;
        }

        private void HandleLostFocus (object Sender, RoutedEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            string BoxContents = TB.Text;
            double BoxValue = 0.0;
            bool OK = double.TryParse(BoxContents, out BoxValue);
            if (!OK)
                TB.Text = "0.0";
        }

        private void HandleKeyPress (object Sender, KeyEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            if (e.Key == Key.Return || e.Key == Key.Return)
            {
                string BoxContents = TB.Text;
                double BoxValue = 0.0;
                bool OK = double.TryParse(BoxContents, out BoxValue);
                if (!OK)
                    TB.Text = "0.0";
                e.Handled = true;
                if (MoveToNextInputEvent!=null)
                {
                    NextInputEventArgs args = new NextInputEventArgs(Location.X,Location.Y);
                    MoveToNextInputEvent(this, args);
                }
            }
        }

        public GridLocation Location { get; private set; }

        public delegate void HandleMoveToNextInputEvent (object Sender, NextInputEventArgs e);
        public event HandleMoveToNextInputEvent MoveToNextInputEvent;
    }

    public class NextInputEventArgs : EventArgs
    {
        public NextInputEventArgs () : base()
        {
            OldLocation = new GridLocation();
        }

        public NextInputEventArgs (int X, int Y) : base()

        {
            OldLocation = new GridLocation(X, Y);
        }

        public GridLocation OldLocation { get; private set; }
    }

    public class GridLocation
    {
        public GridLocation ()
        {
            X = -1;
            Y = -1;
        }

        public GridLocation (int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
