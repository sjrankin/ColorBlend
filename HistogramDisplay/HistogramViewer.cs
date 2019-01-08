using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace HistogramDisplay
{
    /// <summary>
    /// Control that displays histogram data (but doesn't generate it).
    /// </summary>
    public partial class HistogramViewer : ContentControl
    {
        public HistogramViewer () : base()
        {
            CreateData();
            CreateUI();
            DrawUI();
        }

        /// <summary>
        /// Color channel names.
        /// </summary>
        private enum ChannelNames : int
        {
            /// <summary>
            /// Red.
            /// </summary>
            Red = 2,
            /// <summary>
            /// Green.
            /// </summary>
            Green = 1,
            /// <summary>
            /// Blue.
            /// </summary>
            Blue = 0
        }

        private Grid HistogramHolder;
        private Border Infrastructure;

        /// <summary>
        /// Create the user interface.
        /// </summary>
        private void CreateUI ()
        {
            Infrastructure = new Border();
            Infrastructure.BorderBrush = Brushes.Black;
            Infrastructure.BorderThickness = new Thickness(1);
            Infrastructure.Background = Brushes.Transparent;
            this.Content = Infrastructure;

            HistogramHolder = new Grid();
            Infrastructure.Child = HistogramHolder;
            HistogramHolder.VerticalAlignment = VerticalAlignment.Stretch;
            HistogramHolder.HorizontalAlignment = HorizontalAlignment.Stretch;
            HistogramHolder.Background = Brushes.WhiteSmoke;
            HistogramHolder.MouseMove += MouseMovingInHistogram;

            this.SizeChanged += HandleDisplaySizeChanged;
        }

        /// <summary>
        /// Handle mouse moves over the histogram.
        /// </summary>
        /// <param name="Sender">The histogram display sub-control.</param>
        /// <param name="e">Event data.</param>
        private void MouseMovingInHistogram (object Sender, MouseEventArgs e)
        {
            if (Histogram.Count < 1)
                return;
            if (Debug)
            {
                Grid HG = Sender as Grid;
                if (HG == null)
                    return;
                Point MPoint = e.GetPosition(HistogramHolder);
                double HWidth = HG.ActualWidth;
                double Percent = MPoint.X / HWidth;
                int BinIndex = (int)(Percent * (double)Histogram.Count);
                BinIndex = Math.Max(BinIndex, 255);
                BinIndex = Math.Min(BinIndex, 0);
                StringBuilder sb = new StringBuilder();
                sb.Append("Index: ");
                sb.Append(BinIndex);
                sb.Append(Environment.NewLine);
                sb.Append("Red: ");
                sb.Append(Histogram[BinIndex].RawRed);
                sb.Append(", ");
                sb.Append(Histogram[BinIndex].RedPercent);
                sb.Append("%");
                sb.Append(Environment.NewLine);
                sb.Append("Green: ");
                sb.Append(Histogram[BinIndex].RawGreen);
                sb.Append(", ");
                sb.Append(Histogram[BinIndex].GreenPercent);
                sb.Append("%");
                sb.Append(Environment.NewLine);
                sb.Append("Blue: ");
                sb.Append(Histogram[BinIndex].RawBlue);
                sb.Append(", ");
                sb.Append(Histogram[BinIndex].BluePercent);
                sb.Append("%");
                HG.ToolTip = sb.ToString();
            }
        }

        /// <summary>
        /// Clear histogram data.
        /// </summary>
        public void Clear ()
        {
            Histogram.Clear();
            HistogramHolder.Children.Clear();
        }

        /// <summary>
        /// Handle changes to the size of the histogram window.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleDisplaySizeChanged (object Sender, SizeChangedEventArgs e)
        {
            DrawUI();
        }

        /// <summary>
        /// Draw the user interface.
        /// </summary>
        private void DrawUI ()
        {
            if (HistogramHolder == null)
                return;
            HistogramHolder.Children.Clear();
            if (ShowCheckerboardBackground)
            {
                HistogramHolder.Background = CheckerboardPatternBrush(CheckerboardBackgroundSize, CheckerboardBackgroundSize,
                    (SolidColorBrush)CheckerboardBackgroundBrush0, (SolidColorBrush)CheckerboardBackgroundBrush1);
            }
            else
                HistogramHolder.Background = HistogramBackground;
            Infrastructure.BorderBrush = BorderBrush;
            Infrastructure.BorderThickness = BorderThickness;
            Infrastructure.CornerRadius = CornerRadius;
            if (ShowGray)
            {
                List<double> GrayData = new List<double>();
                List<int> RawGrayData = new List<int>();
                double MaxGrayPercent = double.MinValue;
                int MaxGrayValue = 0;
                foreach (HistogramTriplet Triplet in Histogram)
                {
                    double GrayValue = Triplet.Sum;
                    GrayData.Add(GrayValue);
                    RawGrayData.Add((int)Triplet.RawSum);
                    if (GrayValue > MaxGrayPercent)
                    {
                        MaxGrayPercent = GrayValue;
                        MaxGrayValue = (int)Triplet.RawSum;
                    }
                }
                //DoDrawHistogramChannel(GrayData, RawGrayData, GrayBrush, MaxGrayPercent, MaxGrayValue);
                DoDrawHistogramChannel(GrayData, RawGrayData, GetFinalBrush(HistogramChannels.Gray), MaxGrayPercent, MaxGrayValue);
            }
            else
            {
                if (ShowRed)
                {
                    double RedMaxPercent = 0.0;
                    int MaxRedValue = 0;
                    List<double> RedData = GetChannel(Histogram, ChannelNames.Red, out RedMaxPercent, out MaxRedValue);
                    List<int> RawRedData = GetRawChannel(Histogram, ChannelNames.Red, out MaxRedValue);
                    //DoDrawHistogramChannel(RedData, RawRedData, RedBrush, RedMaxPercent, MaxRedValue);
                    DoDrawHistogramChannel(RedData, RawRedData, GetFinalBrush(HistogramChannels.Red), RedMaxPercent, MaxRedValue);
                }
                if (ShowGreen)
                {
                    double GreenMaxPercent = 0.0;
                    int MaxGreenValue = 0;
                    List<double> GreenData = GetChannel(Histogram, ChannelNames.Green, out GreenMaxPercent, out MaxGreenValue);
                    List<int> RawGreenData = GetRawChannel(Histogram, ChannelNames.Green, out MaxGreenValue);
                    //DoDrawHistogramChannel(GreenData, RawGreenData, GreenBrush, GreenMaxPercent, MaxGreenValue);
                    DoDrawHistogramChannel(GreenData, RawGreenData, GetFinalBrush(HistogramChannels.Green), GreenMaxPercent, MaxGreenValue);
                }
                if (ShowBlue)
                {
                    double BlueMaxPercent = 0.0;
                    int MaxBlueValue = 0;
                    List<double> BlueData = GetChannel(Histogram, ChannelNames.Blue, out BlueMaxPercent, out MaxBlueValue);
                    List<int> RawBlueData = GetRawChannel(Histogram, ChannelNames.Blue, out MaxBlueValue);
                    //DoDrawHistogramChannel(BlueData, RawBlueData, BlueBrush, BlueMaxPercent, MaxBlueValue);
                    DoDrawHistogramChannel(BlueData, RawBlueData, GetFinalBrush(HistogramChannels.Blue), BlueMaxPercent, MaxBlueValue);
                }
            }
        }

        private SolidColorBrush GetFinalBrush (HistogramChannels Channel)
        {
            byte A = 0xff;
            byte R = 0xff;
            byte G = 0xff;
            byte B = 0xff;
            switch (Channel)
            {
                case HistogramChannels.Red:
                    if (ChannelDisplayAlphaLevelOverride.HasValue)
                    {
                        A = (byte)(ChannelDisplayAlphaLevelOverride.Value * 255.0);
                        R = ((SolidColorBrush)RedBrush).Color.R;
                        G = ((SolidColorBrush)RedBrush).Color.G;
                        B = ((SolidColorBrush)RedBrush).Color.B;
                    }
                    else
                        return (SolidColorBrush)RedBrush;
                    break;

                case HistogramChannels.Green:
                    if (ChannelDisplayAlphaLevelOverride.HasValue)
                    {
                        A = (byte)(ChannelDisplayAlphaLevelOverride.Value * 255.0);
                        R = ((SolidColorBrush)GreenBrush).Color.R;
                        G = ((SolidColorBrush)GreenBrush).Color.G;
                        B = ((SolidColorBrush)GreenBrush).Color.B;
                    }
                    else
                        return (SolidColorBrush)GreenBrush;
                    break;

                case HistogramChannels.Blue:
                    if (ChannelDisplayAlphaLevelOverride.HasValue)
                    {
                        A = (byte)(ChannelDisplayAlphaLevelOverride.Value * 255.0);
                        R = ((SolidColorBrush)BlueBrush).Color.R;
                        G = ((SolidColorBrush)BlueBrush).Color.G;
                        B = ((SolidColorBrush)BlueBrush).Color.B;
                    }
                    else
                        return (SolidColorBrush)GreenBrush;
                    break;

                case HistogramChannels.Gray:
                    if (ChannelDisplayAlphaLevelOverride.HasValue)
                    {
                        A = (byte)(ChannelDisplayAlphaLevelOverride.Value * 255.0);
                        R = ((SolidColorBrush)GrayBrush).Color.R;
                        G = ((SolidColorBrush)GrayBrush).Color.G;
                        B = ((SolidColorBrush)GrayBrush).Color.B;
                    }
                    else
                        return (SolidColorBrush)GreenBrush;
                    break;
            }

            return new SolidColorBrush(Color.FromArgb(A, R, G, B));
        }

        private void DoDrawHistogramChannel (List<double> ChannelData, List<int> RawChannelData, Brush ChannelBrush, double MaxPercent, int MaxValue)
        {
            if (AbsolutePercent)
                DrawHistogramChannel(ChannelData, ChannelBrush, MaxPercent);
            else
                DrawHistogramChannel(RawChannelData, ChannelBrush, MaxValue);
        }

        /// <summary>
        /// Draw a single histogram channel.
        /// </summary>
        /// <param name="ChannelData">The histogram data for the channel to draw.</param>
        /// <param name="ChannelBrush">The brush to use to draw the channel.</param>
        /// <param name="MaxPercent">Maximum percent for the channel data.</param>
        private void DrawHistogramChannel (List<double> ChannelData, Brush ChannelBrush, double MaxPercent)
        {
            if (ChannelData == null)
                return;
            if (ChannelData.Count < 1)
                return;

            double ViewportHeight = HistogramHolder.ActualHeight;
            double ViewportWidth = HistogramHolder.ActualWidth;
            double HRatio = ViewportWidth / (double)ChannelData.Count;
            Polygon PG = new Polygon();
            PG.Fill = ChannelBrush;
            if (IsOutlined)
            {
                PG.StrokeThickness = OutlineThickness;
                PG.Stroke = (SolidColorBrush)OutlineBrush;
                PG.StrokeLineJoin = PenLineJoin.Round;
            }
            else
            {
                PG.StrokeThickness = 0.0;
                PG.Stroke = Brushes.Transparent;
            }
            PG.VerticalAlignment = VerticalAlignment.Bottom;
            PG.HorizontalAlignment = HorizontalAlignment.Left;
            double AdjustedViewportHeight = ViewportHeight - (PG.StrokeThickness * 2.0);

            PG.Points.Add(new Point(0, ViewportHeight));
            for (int i = 0; i < ChannelData.Count; i++)
            {
                double ChannelPercent = 0.0;
                if (AbsolutePercent)
                    ChannelPercent = ChannelData[i] / MaxPercent;
                else
                    ChannelPercent = ChannelData[i] / AdjustedViewportHeight;
                double FinalY = ViewportHeight - (ChannelPercent * AdjustedViewportHeight);
                double FinalX = (double)i * HRatio;
                PG.Points.Add(new Point(FinalX, FinalY));
            }
            PG.Points.Add(new Point(ChannelData.Count * HRatio, ViewportHeight));
            Grid.SetRow(PG, 0);
            Grid.SetColumn(PG, 0);
            HistogramHolder.Children.Add(PG);
        }

        /// <summary>
        /// Draw a single histogram channel.
        /// </summary>
        /// <param name="ChannelData">The histogram data for the channel to draw.</param>
        /// <param name="ChannelBrush">The brush to use to draw the channel.</param>
        /// <param name="MaxValue">Maximum value for the channel data.</param>
        private void DrawHistogramChannel (List<int> ChannelData, Brush ChannelBrush, int MaxValue)
        {
            if (ChannelData == null)
                return;
            if (ChannelData.Count < 1)
                return;

            double ViewportHeight = HistogramHolder.ActualHeight;
            double ViewportWidth = HistogramHolder.ActualWidth;
            double HRatio = ViewportWidth / (double)ChannelData.Count;
            Polygon PG = new Polygon();
            PG.Fill = ChannelBrush;
            if (IsOutlined)
            {
                PG.StrokeThickness = OutlineThickness;
                PG.Stroke = (SolidColorBrush)OutlineBrush;
                PG.StrokeLineJoin = PenLineJoin.Round;
            }
            else
            {
                PG.StrokeThickness = 0.0;
                PG.Stroke = Brushes.Transparent;
            }
            PG.VerticalAlignment = VerticalAlignment.Bottom;
            PG.HorizontalAlignment = HorizontalAlignment.Left;
            double AdjustedViewportHeight = ViewportHeight - (PG.StrokeThickness * 2.0);

            PG.Points.Add(new Point(0, ViewportHeight));
            for (int i = 0; i < ChannelData.Count; i++)
            {
                double ChannelPercent = (double)((double)ChannelData[i] / (double)MaxValue);
                double FinalY = ViewportHeight - (ChannelPercent * AdjustedViewportHeight);
                double FinalX = (double)i * HRatio;
                PG.Points.Add(new Point(FinalX, FinalY));
            }
            PG.Points.Add(new Point(ChannelData.Count * HRatio, ViewportHeight));
            Grid.SetRow(PG, 0);
            Grid.SetColumn(PG, 0);
            HistogramHolder.Children.Add(PG);
        }

        /// <summary>
        /// Return a checkerboard brush.
        /// </summary>
        /// <param name="Width">Overall width of the brush.</param>
        /// <param name="Height">Overall height of the brush.</param>
        /// <param name="Color0">First brush.</param>
        /// <param name="Color1">Second brush.</param>
        /// <returns>Drawing brush.</returns>
        public DrawingBrush CheckerboardPatternBrush (double Width, double Height, SolidColorBrush Color0, SolidColorBrush Color1)
        {
            double HalfWidth = Width / 2;
            double HalfHeight = Height / 2;
            DrawingBrush TheBrush = new DrawingBrush();
            TheBrush.AlignmentX = AlignmentX.Left;
            TheBrush.AlignmentY = AlignmentY.Top;
            TheBrush.Stretch = Stretch.None;
            TheBrush.ViewportUnits = BrushMappingMode.Absolute;
            TheBrush.TileMode = TileMode.Tile;
            TheBrush.Viewport = new Rect(new Point(0, 0), new Point(Width, Height));
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing();
            Overall.Brush = Color0;
            RectangleGeometry RG0 = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = RG0;
            DG.Children.Add(Overall);

            GeometryDrawing Other = new GeometryDrawing();
            Other.Brush = Color1;
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
