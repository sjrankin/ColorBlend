using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Input.MatrixInput
{
    public static class InputBoxFactory
    {
        public static MatrixTextBox MakeTextBox(int X, int Y, double InitialValue)
        {
            MatrixTextBox MTB = new MatrixTextBox(X, Y, InitialValue);
            Grid.SetColumn(MTB, X);
            Grid.SetRow(MTB, Y);
            MTB.MoveToNextInputEvent += HandleTextBoxMove;
            return MTB;
        }

        private static void HandleTextBoxMove (object Sender, NextInputEventArgs e)
        {
        }
    }
}
