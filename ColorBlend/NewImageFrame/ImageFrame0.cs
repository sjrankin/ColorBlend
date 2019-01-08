using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace ColorBlend
{
    /// <summary>
    /// Control that displays an image with ancillary infrastructure.
    /// </summary>
    public partial class ImageFrame0 : ContentControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageFrame0() : base()
        {
            CreateUI();
            DrawUI();
        }

        /// <summary>
        /// Create the user interface.
        /// </summary>
        private void CreateUI()
        {
            Container = new Grid();
            base.Content = Container;
            ContainerBorder = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = Brushes.Transparent
            };
            Container.Children.Add(ContainerBorder);
            SV = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
            };
            ContainerBorder.Child = SV;
            ImageGrid = new Grid();
            SV.Content = ImageGrid;
            BackgroundBorder = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.White
            };
            ImageGrid.Children.Add(BackgroundBorder);
            ImageOut = new Image
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            BackgroundBorder.Child = ImageOut;
        }

        #region Component controls.
        private Grid Container = null;
        private Image ImageOut = null;
        private ScrollViewer SV = null;
        private Grid ImageGrid = null;
        private Border ContainerBorder = null;
        private Border BackgroundBorder = null;
        #endregion

        /// <summary>
        /// Draw the user interface.
        /// </summary>
        private void DrawUI()
        {
            if (ShowCheckerboardBackground)
            {
                BackgroundBorder.Background = Utility.CheckerboardPatternBrush(CheckerboardColorSquareSize, CheckerboardColorSquareSize,
                    CheckerboardFirstColor, CheckerboardSecondColor);
            }
            else
            {
                BackgroundBorder.Background = BackgroundBrush;
            }
            if (
                (GridSpacing == 0.0) ||
                (GridBrush == Brushes.Transparent) ||
                (GridThickness == new Thickness(0.0))
                )
            {
                //Do nothing.
            }
            else
            {
                //Draw the grid.
            }
            Stretch ImageStretch = FitImageToFrame ? Stretch.Uniform : Stretch.None;
            ImageOut.Stretch = ImageStretch;
            if (DisplayImage == null)
                ImageOut.Source = null;
            else
                ImageOut.Source = DisplayImage.AsImageSource();
        }

        /// <summary>
        /// Get the width of the image (not necessarily the display width).
        /// </summary>
        public Nullable<int> ImageWidth
        {
            get
            {
                if (DisplayImage == null)
                    return null;
                return DisplayImage.Width;
            }
        }

        /// <summary>
        /// Get the height of the image (not necessarily the display height).
        /// </summary>
        public Nullable<int> ImageHeight
        {
            get
            {
                if (DisplayImage == null)
                    return null;
                return DisplayImage.Height;
            }
        }

        /// <summary>
        /// Get the stride of the image.
        /// </summary>
        public Nullable<int> ImageStride
        {
            get
            {
                if (DisplayImage == null)
                    return null;
                return DisplayImage.Stride;
            }
        }

        /// <summary>
        /// Get the color of the image at absoluate (<paramref name="X"/>,<paramref name="Y"/>).
        /// </summary>
        /// <remarks>
        /// Exceptions are thrown by DisplayImage on bad coordinates or no image.
        /// </remarks>
        /// <param name="X">Horizontal coordinate.</param>
        /// <param name="Y">Vertical coordinate.</param>
        /// <returns>The color at (<paramref name="X"/>,<paramref name="Y"/>).</returns>
        public Color GetColorAt(int X, int Y)
        {
            if (DisplayImage == null)
                return Colors.Transparent;
            if (!DisplayImage.Loaded)
                return Colors.Transparent;
            return DisplayImage[X, Y];
        }

        /// <summary>
        /// Try to get the color at the absolute coordinate (<paramref name="X"/>,<paramref name="Y"/>).
        /// </summary>
        /// <param name="X">The horizontal coordinate.</param>
        /// <param name="Y">The vertical cooridnate.</param>
        /// <param name="TheColor">The color at (<paramref name="X"/>,<paramref name="Y"/>) on success, undefined on failure.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool TryGetColorAt(int X, int Y, out Color TheColor)
        {
            TheColor = Colors.Transparent;
            if (DisplayImage == null)
                return false;
            if (!DisplayImage.Loaded)
                return false;
            return DisplayImage.TryGetColor(X, Y, out TheColor);
        }
    }
}
