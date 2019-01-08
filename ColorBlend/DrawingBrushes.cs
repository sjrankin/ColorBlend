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
using System.Diagnostics;

namespace ColorBlend
{
    public partial class MainWindow
    {
        public enum GridBrushTypes
        {
            Blueprint,
            GridPaper,
            LinedPaper
        }

        /// <summary>
        /// Return a brush with a regular, grid pattern of some type.
        /// </summary>
        /// <param name="Which">Determines the pattern type of the returned brush.</param>
        /// <param name="Width">Width of the pattern.</param>
        /// <param name="Height">Height of the pattern.</param>
        /// <returns>Drawing brush.</returns>
        public DrawingBrush GridBrush (GridBrushTypes Which, double Width, double Height)
        {
            switch (Which)
            {
                case GridBrushTypes.Blueprint:
                    return BluePrintBrush(Width, Height);

                case GridBrushTypes.GridPaper:
                    return GridBrush(Width, Height);

                case GridBrushTypes.LinedPaper:
                    return LinedPaperBrush(Width, Height);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Return a brush that forms a blueprint-like background.
        /// </summary>
        /// <param name="Width">Width of the rectangle.</param>
        /// <param name="Height">Height of the rectangle.</param>
        /// <returns>Drawing brush that looks like a blueprint background.</returns>
        public DrawingBrush BluePrintBrush (double Width, double Height)
        {
            DrawingBrush TheBrush = new DrawingBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Stretch = Stretch.None,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Viewport = new Rect(new Point(0, 0), new Point(Width, Height))
            };
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing
            {
                Brush = Brushes.Navy
            };
            RectangleGeometry OverallGeometry = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = OverallGeometry;
            DG.Children.Add(Overall);

            GeometryDrawing BGLines = new GeometryDrawing
            {
                Brush = Brushes.DarkSlateBlue
            };
            RectangleGeometry Line1 = new RectangleGeometry(new Rect(0, 0, Width, 1));
            RectangleGeometry Line2 = new RectangleGeometry(new Rect(0, 0, 1, Height));
            GeometryGroup LineGroup = new GeometryGroup();
            LineGroup.Children.Add(Line1);
            LineGroup.Children.Add(Line2);
            BGLines.Geometry = LineGroup;
            DG.Children.Add(BGLines);

            return TheBrush;
        }

        /// <summary>
        /// Return a brush that forms a grid background.
        /// </summary>
        /// <param name="Width">Width of the rectangle.</param>
        /// <param name="Height">Height of the rectangle.</param>
        /// <returns>Drawing brush that looks like grid paper.</returns>
        public DrawingBrush GridBrush (double Width, double Height)
        {
            DrawingBrush TheBrush = new DrawingBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Stretch = Stretch.None,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Viewport = new Rect(new Point(0, 0), new Point(Width, Height))
            };
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing
            {
                Brush = Brushes.WhiteSmoke
            };
            RectangleGeometry OverallGeometry = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = OverallGeometry;
            DG.Children.Add(Overall);

            GeometryDrawing BGLines = new GeometryDrawing
            {
                Brush = Brushes.LightGray
            };
            RectangleGeometry Line1 = new RectangleGeometry(new Rect(0, 0, Width, 1));
            RectangleGeometry Line2 = new RectangleGeometry(new Rect(0, 0, 1, Height));
            GeometryGroup LineGroup = new GeometryGroup();
            LineGroup.Children.Add(Line1);
            LineGroup.Children.Add(Line2);
            BGLines.Geometry = LineGroup;
            DG.Children.Add(BGLines);

            return TheBrush;
        }

        /// <summary>
        /// Return a brush that forms a lined paper-like background.
        /// </summary>
        /// <param name="Width">Width of the rectangle.</param>
        /// <param name="Height">Height of the rectangle.</param>
        /// <returns>Drawing brush that looks like lined paper.</returns>
        public DrawingBrush LinedPaperBrush (double Width, double Height)
        {
            DrawingBrush TheBrush = new DrawingBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Stretch = Stretch.None,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Viewport = new Rect(new Point(0, 0), new Point(Width, Height))
            };
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing
            {
                Brush = Brushes.White
            };
            RectangleGeometry OverallGeometry = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = OverallGeometry;
            DG.Children.Add(Overall);

            GeometryDrawing BGLines = new GeometryDrawing
            {
                Brush = Brushes.Blue
            };
            RectangleGeometry Line1 = new RectangleGeometry(new Rect(0, Height - 1, Width, 1));
            GeometryGroup LineGroup = new GeometryGroup();
            LineGroup.Children.Add(Line1);
            BGLines.Geometry = LineGroup;
            DG.Children.Add(BGLines);

            return TheBrush;
        }

        /// <summary>
        /// Return a checkerboard brush.
        /// </summary>
        /// <param name="Width">Overall width of the brush.</param>
        /// <param name="Height">Overall height of the brush.</param>
        /// <param name="Color0">First brush.</param>
        /// <param name="Color1">Second brush.</param>
        /// <returns>Drawing brush.</returns>
        public DrawingBrush CheckerboardPatternBrush (double Width, double Height,
            SolidColorBrush Color0, SolidColorBrush Color1)
        {
            double HalfWidth = Width / 2;
            double HalfHeight = Height / 2;
            DrawingBrush TheBrush = new DrawingBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Stretch = Stretch.None,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Viewport = new Rect(new Point(0, 0), new Point(Width, Height))
            };
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing
            {
                Brush = Color0
            };
            RectangleGeometry RG0 = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = RG0;
            DG.Children.Add(Overall);

            GeometryDrawing Other = new GeometryDrawing
            {
                Brush = Color1
            };
            RectangleGeometry RG1 = new RectangleGeometry(new Rect(0, 0, HalfWidth, HalfHeight));
            RectangleGeometry RG2 = new RectangleGeometry(new Rect(HalfWidth, HalfHeight, HalfWidth, HalfHeight));
            GeometryGroup GG = new GeometryGroup();
            GG.Children.Add(RG1);
            GG.Children.Add(RG2);
            Other.Geometry = GG;
            DG.Children.Add(Other);

            return TheBrush;
        }
    }
}