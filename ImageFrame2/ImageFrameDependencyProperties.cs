using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Iro3.Controls.Images
{
    public partial class ImageFrame
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(WriteableBitmap), typeof(ImageFrame),
                new UIPropertyMetadata(null, new PropertyChangedCallback(HandleSourceChange)));

        private static void HandleSourceChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            ((ImageFrame)Sender).Source = (WriteableBitmap)e.NewValue;
            ((ImageFrame)Sender).DrawUI();
        }

        public WriteableBitmap Source
        {
            get
            {
                return (WriteableBitmap)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
    }
}
