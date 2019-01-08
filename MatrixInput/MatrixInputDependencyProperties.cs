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
    public partial class MatrixInput
    {
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<double>), typeof(MatrixInput),
                new UIPropertyMetadata(new List<double>(), new PropertyChangedCallback(HandleItemsChange)));

        public static void HandleItemsChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            MatrixInput That = Sender as MatrixInput;
            if (That == null)
                return;
            That.Items = (List<double>)e.NewValue;
            That.DrawUI();
        }

        public List<double> Items
        {
            get
            {
                return (List<double>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }
    }
}
