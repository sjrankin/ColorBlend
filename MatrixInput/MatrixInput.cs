using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;

namespace Input.MatrixInput
{
    public partial class MatrixInput : ContentControl
    {
        public MatrixInput () :base()
        {
            CreateUI();
        }

        private void CreateUI()
        {
            MainContainer = new Border();
            MainContainer.Background = Brushes.WhiteSmoke;
            MainContainer.BorderBrush = Brushes.Black;
            this.Content = MainContainer;

            MatrixContainer = new ScrollViewer();
            MatrixContainer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            MatrixContainer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MainContainer.Child = MatrixContainer;

            Matrix = new Grid();
            MatrixContainer.Content = Matrix;
            DrawUI();
        }

        private void DrawUI()
        {

        }

        private Border MainContainer;
        private ScrollViewer MatrixContainer;
        private Grid Matrix;
    }
}
