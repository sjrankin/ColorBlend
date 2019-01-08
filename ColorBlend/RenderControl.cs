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
        private void Renderer ()
        {
            DoRender3();
        }

        int FinalWidth = 0;
        int FinalHeight = 0;
        int Stride = 0;
        Color BoundryColor = Colors.Transparent;

        #region DoRender3
        /// <summary>
        /// Render the color blobs.
        /// </summary>
        private void DoRender3 ()
        {
            if (!ColorsInitialized)
                return;
            PointCount = GetPointCount();
            DateTime RenderStart = DateTime.Now;
            SetStatus("Rendering: " + PointCount.ToString() + " points");
            double SDWidth = SurfaceContainer.ActualWidth;
            FinalWidth = (int)SDWidth;
            double SDHeight = SurfaceContainer.ActualHeight;
            FinalHeight = (int)SDHeight;
            ColorBlenderInterface cbi = new ColorBlenderInterface();
            List<ColorBlenderInterface.PlaneSet> Planes = new List<ColorBlenderInterface.PlaneSet>();
            foreach (ColorPoint CP in PurePoints)
            {
                //if (!CP.UseAbsoluteOnly)
                //    CP.CreateAbsolutePoint(FinalWidth, FinalHeight);
                ColorBlenderInterface.PlaneSet Plane = new ColorBlenderInterface.PlaneSet
                {
                    Width = CP.IntWidth,
                    Height = CP.IntHeight,
                    Stride = CP.IntWidth * 4,
                    Left = CP.AbsolutePoint.IntX
                };
                Plane.Right = Plane.Left + (int)CP.Width - 1;
                Plane.Top = CP.AbsolutePoint.IntY;
                Plane.Bottom = Plane.Top + (int)CP.Height - 1;
                Plane.Radius = (int)CP.Radius;
                Plane.PlaneBits = new byte[(int)(Plane.Height * Plane.Stride)];
                byte CenterAlpha = (byte)(CP.AlphaStart * 255.0);
                byte EdgeAlpha = (byte)(CP.AlphaEnd * 255.0);
                //Render the blob ignoring position, e.g., it's positionless but not dimensionless.
                cbi.Blendy3(ref Plane.PlaneBits, Plane.Width, Plane.Height, Plane.Stride, CP.PointColor, CenterAlpha, EdgeAlpha,
                    BoundryColor);
                Planes.Add(Plane);
            }

            //Merge all of the color blobs (in the list of planes) to the final render surface.
            Tuple<PointEx, PointEx> MaxSurface = MaximumFinalExtent(FinalWidth, FinalHeight);
            int RenderSurfaceWidth = (int)SurfaceContainer.ActualWidth;
            int RenderSurfaceHeight = (int)SurfaceContainer.ActualHeight;
            int RenderSurfaceStride = RenderSurfaceWidth * 4;
            int FinalStride = FinalWidth * 4;
            GlobalPixelMapStride = FinalStride;
            byte[] MasterRenderedPlane = new byte[(int)(RenderSurfaceHeight * RenderSurfaceStride)];

            if (ShowPictureBackground)
            {
                if (BackgroundPicture == null)
                {
                    BackgroundPicture = new Image
                    {
                        //object z = FindResource("TestImage1");
                        Source = (ImageSource)FindResource("TestImage1")
                    };
                }
                BitmapSource TheImage = BackgroundPicture.Source as BitmapSource;
                WriteableBitmap WB = new WriteableBitmap(TheImage);
                unsafe
                {
                    cbi.DoFillBufferWithBuffer(ref MasterRenderedPlane, RenderSurfaceWidth, RenderSurfaceHeight, RenderSurfaceStride,
                      (IntPtr)WB.BackBuffer.ToPointer(), WB.PixelWidth, WB.PixelHeight, WB.BackBufferStride,
                      Colors.Yellow);
                }
            }
            else
            {
                cbi.DoClearBuffer(ref MasterRenderedPlane, RenderSurfaceWidth, RenderSurfaceHeight, RenderSurfaceStride,
                      Colors.Black, true, Colors.DarkGray, 25, 25,
                      Colors.Transparent);
                BackgroundPicture = null;
            }
            ColorBlenderInterface.ReturnCode Returned = ColorBlenderInterface.ReturnCode.Success;
            ColorBlenderInterface.DrawnObject[] DrawResults = null;
            cbi.MergeBlobs4(ref MasterRenderedPlane, RenderSurfaceWidth, RenderSurfaceHeight, RenderSurfaceStride,
                            Planes, ref Returned, ref DrawResults);

            //Crop the final render surface to the destination image surface, but only if we need to.
            GlobalPixelMap = MasterRenderedPlane;                  //Set GlobalPixelMap in case we don't need to crop.

            if (PointMover != null)
            {
                List<LineDefinition> Lines = new List<LineDefinition>();
                Lines.Add(new LineDefinition((int)PointMover.StartPoint.Y, Colors.Yellow, true));
                Lines.Add(new LineDefinition((int)PointMover.StartPoint.X, Colors.Yellow, false));
                Lines.Add(new LineDefinition((int)PointMover.DestinationPoint.Y, Colors.Magenta, true));
                Lines.Add(new LineDefinition((int)PointMover.DestinationPoint.X, Colors.Magenta, false));
                cbi.DrawLines(ref GlobalPixelMap, FinalWidth, FinalHeight, FinalStride, Lines);
            }

            //Copy the final surface to the image's data.
            SurfaceDestination.Source = BitmapSource.Create(FinalWidth, FinalHeight, 96.0, 96.0, PixelFormats.Bgra32,
                null, GlobalPixelMap, FinalStride);

            DateTime RenderEnd = DateTime.Now;
            Duration RenderDuration = RenderEnd - RenderStart;
            AppendStatus(", " + RenderDuration.ToString());
            AppendStatus(", " + FinalWidth.ToString() + "x" + FinalHeight.ToString());
        }

        byte[] GlobalPixelMap = null;
        int GlobalPixelMapStride = 0;
        #endregion

        public double CumulativeMS = 0;
        public int TotalBlockCount = 0;

        public void DrawGradientBlocks (int Count)
        {
            ColorBlenderInterface cbi = new ColorBlenderInterface();
            double SDWidth = SurfaceContainer.ActualWidth;
            FinalWidth = (int)SDWidth;
            double SDHeight = SurfaceContainer.ActualHeight;
            FinalHeight = (int)SDHeight;
            int FinalStride = FinalWidth * 4;
            GlobalPixelMapStride = FinalStride;
            GlobalPixelMap = new byte[FinalHeight * FinalStride];
            List<ColorBlenderInterface.GradientPoint> GradientPoints = new List<ColorBlenderInterface.GradientPoint>();

            for (int i = 0; i < Count; i++)
            {
                ColorBlenderInterface.GradientPoint GPoint = new ColorBlenderInterface.GradientPoint
                {
                    X = GlobalRand.Next(0, FinalWidth),
                    Y = GlobalRand.Next(0, FinalHeight),
                    Width = GlobalRand.Next(24, 512),
                    Height = GlobalRand.Next(24, 512)
                };
                GPoint.Stride = GPoint.X * 4;
                GPoint.Region = new byte[GPoint.Stride * GPoint.Width];
                List<ColorPoint> CL = new List<ColorPoint>();
                int ColorCount = GlobalRand.Next(2, 5);
                for(int k=0;k<ColorCount;k++)
                {
                    ColorPoint CP = new ColorPoint
                    {
                        PointColor = RandomColor(GlobalRand, 0x80, 0xff, false)
                    };
                    CL.Add(CP);
                }
            }

            cbi.DoClearBuffer(ref GlobalPixelMap, FinalWidth, FinalHeight, FinalStride,
                Colors.Black, true, Colors.DarkGray, 25, 25,
                Colors.Transparent);
        }

        public void DrawRandomBlocks (int Count, Random Rand)
        {
            DateTime RenderStart = DateTime.Now;
            SetStatus("Rendering: " + Count.ToString() + " color blocks.");
            ColorBlenderInterface cbi = new ColorBlenderInterface();
            double SDWidth = SurfaceContainer.ActualWidth;
            FinalWidth = (int)SDWidth;
            double SDHeight = SurfaceContainer.ActualHeight;
            FinalHeight = (int)SDHeight;
            int FinalStride = FinalWidth * 4;
            GlobalPixelMapStride = FinalStride;
            GlobalPixelMap = new byte[FinalHeight * FinalStride];
            List<UIElement> ClrDbg = new List<UIElement>();
            List<ColorBlenderInterface.ColorBlock> ColorBlocks = new List<ColorBlenderInterface.ColorBlock>();

            for (int i = 0; i < Count; i++)
            {
                Color RColor = RandomColor(Rand, 0x0, 0xff, true);
                RColor.A = (byte)Rand.Next(0xb0, 0xff);

                int Left = Rand.Next(0, FinalWidth - 2);
                int LeftRemainder = FinalWidth - Left;
                int Top = Rand.Next(0, FinalHeight - 2);
                int TopRemainder = FinalHeight - Top;
                int BWidth = Rand.Next(1, LeftRemainder);
                int BHeight = Rand.Next(1, TopRemainder);
                ColorBlenderInterface.ColorBlock CB = new ColorBlenderInterface.ColorBlock
                {
                    Left = Left,
                    Top = Top,
                    Width = BWidth,
                    Height = BHeight,
                    BlockColor = RColor.ToBGRA()
                };
                ColorBlocks.Add(CB);
                StackPanel sp = ColorDebugBlock(RColor);
                sp.ToolTip = "(" + Left.ToString() + "," + Top.ToString() + ") " + BWidth.ToString() + "x" + BHeight.ToString();
                ClrDbg.Add(sp);
            }

            cbi.DrawColorBlocks(ref GlobalPixelMap, FinalWidth, FinalHeight, FinalStride, ColorBlocks, Colors.Transparent);

            DateTime RenderEnd = DateTime.Now;
            Duration RenderDuration = RenderEnd - RenderStart;
            AppendStatus(", " + MakeNiceDuration(RenderDuration, "D4"));
            long mean = RenderDuration.TimeSpan.Ticks / Count;
            TimeSpan MeanSpan = TimeSpan.FromTicks(mean);
            string means = MakeNiceDuration(new Duration(MeanSpan), "D4");
            AppendStatus("  " + means);

            TotalBlockCount += Count;
            CumulativeMS += RenderDuration.TimeSpan.Ticks;
            double MeanMS = CumulativeMS / (double)TotalBlockCount;
            string PrettyT = ((int)MeanMS).ToString("D7");
            PrettyT = PrettyT.Substring(0, 3) + "_" + PrettyT.Substring(3);
            MeanMSOut.Text = PrettyT;

            ToDebugOutObjectList(ClrDbg, false);
            ToDebugOut("Render duration: " + RenderDuration.ToString(), false);
            TextBlock TB = new TextBlock
            {
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Text = "Blend Color Test Colors"
            };
            ToDebugOut(TB);

            //Copy the final surface to the image's data.
            SurfaceDestination.Source = BitmapSource.Create(FinalWidth, FinalHeight, 96.0, 96.0, PixelFormats.Bgra32,
                null, GlobalPixelMap, FinalStride);
        }

        public string MakeNiceDuration (Duration Dur, string fmt)
        {
            string Final = "";
            string sec = Dur.TimeSpan.Seconds.ToString();
            string fms = Dur.TimeSpan.Milliseconds.ToString(fmt);
            Final = sec + "." + fms;
            return Final;
        }

        private void TestBackgroundDrawing (Color BGColor, Color FGColor, int CellWidth, int CellHeight)
        {
            DateTime RenderStart = DateTime.Now;
            ColorBlenderInterface cbi = new ColorBlenderInterface();
            double SDWidth = SurfaceContainer.ActualWidth;
            FinalWidth = (int)SDWidth;
            double SDHeight = SurfaceContainer.ActualHeight;
            FinalHeight = (int)SDHeight;
            int FinalStride = FinalWidth * 4;
            GlobalPixelMap = new byte[FinalHeight * FinalStride];

            cbi.DoClearBuffer(ref GlobalPixelMap, FinalWidth, FinalHeight, FinalStride,
               FGColor, true, BGColor, CellWidth, CellHeight, Colors.Transparent);
            //Copy the final surface to the image's data.
            SurfaceDestination.Source = BitmapSource.Create(FinalWidth, FinalHeight, 96.0, 96.0, PixelFormats.Bgra32,
                null, GlobalPixelMap, FinalStride);

            DateTime RenderEnd = DateTime.Now;
            Duration RenderDuration = RenderEnd - RenderStart;
            SetStatus("Render duration: " + RenderDuration.ToString());
        }

        private StackPanel ColorDebugBlock (Color TheColor)
        {
            StackPanel SP = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(2)
            };
            TextBlock TB = new TextBlock
            {
                Margin = new Thickness(1, 1, 4, 1),
                FontSize = 12
            };
            Run R0 = new Run("Color block ");
            TB.Inlines.Add(R0);
            Run R1 = new Run(TheColor.ToString())
            {
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            TB.Inlines.Add(R1);
            Border B = new Border
            {
                Width = 16,
                Height = 18,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(TheColor),
                Margin = new Thickness(1, 2, 2, 2)
            };
            SP.Children.Add(TB);
            SP.Children.Add(B);
            return SP;
        }

        public void DrawRandomLines (ref byte[] Buffer, int FinalWidth, int FinalHeight, int FinalStride)
        {
            Random Rand = new Random();
            ColorBlenderInterface cbi = new ColorBlenderInterface();
            int HorizontalCount = Rand.Next(1, 20);
            int VerticalCount = Rand.Next(1, 20);
            for (int i = 0; i < HorizontalCount; i++)
            {
                Color RColor = RandomColor(Rand);
                int Y = Rand.Next(0, FinalHeight - 1);
                cbi.DrawHorizontalLine(ref Buffer, FinalWidth, FinalHeight, FinalStride, Y, RColor);
            }
            for (int i = 0; i < VerticalCount; i++)
            {
                Color RColor = RandomColor(Rand);
                int X = Rand.Next(0, FinalWidth - 1);
                cbi.DrawVerticalLine(ref Buffer, FinalWidth, FinalHeight, FinalStride, X, RColor);
            }
        }

        public void DrawLine2 (PointEx Point1, PointEx Point2, Color LineColor, bool AntiAlias, int LineThickness)
        {
            DateTime RenderStart = DateTime.Now;
            ColorBlenderInterface cbi = new ColorBlenderInterface();
            double SDWidth = SurfaceContainer.ActualWidth;
            FinalWidth = (int)SDWidth;
            double SDHeight = SurfaceContainer.ActualHeight;
            FinalHeight = (int)SDHeight;
            int FinalStride = FinalWidth * 4;
            GlobalPixelMap = new byte[FinalHeight * FinalStride];

            cbi.DrawALine(ref GlobalPixelMap, FinalWidth, FinalHeight, FinalStride,
                Point1, Point2, LineColor, AntiAlias, LineThickness);
            //Copy the final surface to the image's data.
            SurfaceDestination.Source = BitmapSource.Create(FinalWidth, FinalHeight, 96.0, 96.0, PixelFormats.Bgra32,
                null, GlobalPixelMap, FinalStride);

            DateTime RenderEnd = DateTime.Now;
            Duration RenderDuration = RenderEnd - RenderStart;
            SetStatus("Render duration: " + RenderDuration.ToString());
            StringBuilder sb = new StringBuilder(", (");
            sb.Append(Point1.IntX.ToString());
            sb.Append(",");
            sb.Append(Point1.IntY.ToString());
            sb.Append(") to (");
            sb.Append(Point2.IntX.ToString());
            sb.Append(",");
            sb.Append(Point2.IntY.ToString());
            sb.Append(")");
            AppendStatus(sb.ToString());
        }

        /// <summary>
        /// Determines and returns the upper-left and lower-right of the surface needed to allow all in-bounds color blobs
        /// to be fully drawn. Color blobs that are out of bounds are marked as such and not included in the extent calculations.
        /// </summary>
        /// <param name="FinalWidth">Window width.</param>
        /// <param name="FinalHeight">Window height.</param>
        /// <returns>
        /// The upper-left and lower-right corners of the surface that can hold all in-bound color blobs without cropping.
        /// </returns>
        private Tuple<PointEx, PointEx> MaximumFinalExtent (int FinalWidth, int FinalHeight)
        {
            int SmallestX = int.MaxValue;
            int SmallestY = int.MaxValue;
            int BiggestX = int.MinValue;
            int BiggestY = int.MinValue;
            foreach (ColorPoint CP in PurePoints)
            {
                //CP.CreateAbsolutePoint(FinalWidth, FinalHeight);
                if (OutOfBounds(CP, FinalWidth, FinalHeight))
                {
                    CP.OutOfBounds = true;
                    continue;
                }
                CP.OutOfBounds = false;
                if (CP.AbsolutePoint.X < SmallestX)
                    SmallestX = CP.AbsolutePoint.IntX;
                if (CP.AbsolutePoint.X + CP.Width > BiggestX)
                    BiggestX = CP.AbsolutePoint.IntX + CP.IntWidth;
                if (CP.AbsolutePoint.Y < SmallestY)
                    SmallestY = CP.AbsolutePoint.IntY;
                if (CP.AbsolutePoint.Y + CP.Height > BiggestY)
                    BiggestY = CP.AbsolutePoint.IntY + CP.IntHeight;
            }
            if (SmallestX > 0)
                SmallestX = 0;
            if (SmallestY > 0)
                SmallestY = 0;
            if (BiggestX < FinalWidth)
                BiggestX = FinalWidth;
            if (BiggestY < FinalHeight)
                BiggestY = FinalHeight;
            return new Tuple<PointEx, PointEx>(new PointEx(SmallestX, SmallestY), new PointEx(BiggestX, BiggestY));
        }

        /// <summary>
        /// Determines if <paramref name="SomePoint"/> is entirely out of bounds (e.g., can not be rendered in the final surface).
        /// </summary>
        /// <param name="SomePoint">The point to determine out-of-boundness.</param>
        /// <param name="Width">The width of the final window.</param>
        /// <param name="Height">The height of the final window.</param>
        /// <returns>True if <paramref name="SomePoint"/> is out of bounds, false if not.</returns>
        bool OutOfBounds (ColorPoint SomePoint, int Width, int Height)
        {
            if (SomePoint.AbsolutePoint.X + SomePoint.IntWidth < 0)
                return true;
            if (SomePoint.AbsolutePoint.Y + SomePoint.IntHeight < 0)
                return true;
            if (SomePoint.AbsolutePoint.X - SomePoint.IntWidth > Width)
                return true;
            if (SomePoint.AbsolutePoint.Y - SomePoint.IntHeight > Height)
                return true;
            return false;
        }
    }
}
