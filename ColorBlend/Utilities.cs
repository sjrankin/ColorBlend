using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using HistogramDisplay;

namespace ColorBlend
{
    public static class Utility
    {
        /// <summary>
        /// Return a checkerboard brush.
        /// </summary>
        /// <param name="Width">Overall width of the brush.</param>
        /// <param name="Height">Overall height of the brush.</param>
        /// <param name="Color0">First brush.</param>
        /// <param name="Color1">Second brush.</param>
        /// <returns>Drawing brush.</returns>
        public static DrawingBrush CheckerboardPatternBrush (double Width, double Height,
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

        /// <summary>
        /// Use pre-existing histogram data to populate a histogram viewer.
        /// </summary>
        /// <param name="Triplets">Histogram data.</param>
        /// <param name="HDisplay">Histogram viewer.</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool ViewHistogram (List<HistogramTriplet> Triplets, HistogramViewer HDisplay)
        {
            if (Triplets == null || HDisplay == null)
                return false;
            HDisplay.Clear();
            HDisplay.BatchAdd(Triplets);
            return true;
        }

        /// <summary>
        /// Create a histogram from <paramref name="WB"/> and send it to the histogram viewer.
        /// </summary>
        /// <param name="WB">Image from which a histogram will be generated.</param>
        /// <param name="HDisplay">The histogram viewer.</param>
        /// <param name="BinSize">Number of bins in the histogram.</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool GenerateHistogram (WriteableBitmap WB, HistogramViewer HDisplay, int BinSize)
        {
            if (WB == null || HDisplay == null)
            {
                return false;
            }
            List<HistogramTriplet> Triplets = GenerateHistogram(WB, BinSize);
            return ViewHistogram(Triplets, HDisplay);
        }

        /// <summary>
        /// Generate a histogram from <paramref name="WB"/>.
        /// </summary>
        /// <param name="WB">The image from which the histogram will be created.</param>
        /// <param name="BinSize">Bins in the histogram.</param>
        /// <returns>List of triples from the histogram.</returns>
        public static List<HistogramTriplet> GenerateHistogram (WriteableBitmap WB, int BinSize)
        {
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            if (WB == null)
            {
                return new List<HistogramTriplet>();
            }
            int ImageWidth = WB.PixelWidth;
            int ImageHeight = WB.PixelHeight;
            int ImageStride = WB.BackBufferStride;
            double[] RedPercent = new double[BinSize];
            UInt32[] RawRed = new UInt32[BinSize];
            uint RedSum = 0;
            double[] GreenPercent = new double[BinSize];
            UInt32[] RawGreen = new UInt32[BinSize];
            uint GreenSum = 0;
            double[] BluePercent = new double[BinSize];
            UInt32[] RawBlue = new UInt32[BinSize];
            uint BlueSum = 0;
            unsafe
            {
                unsafe
                {
                    CBI.MakeHistogram((byte*)WB.BackBuffer, ImageWidth, ImageHeight, ImageStride,
                      BinSize,
                      ref RawRed, ref RedPercent, out RedSum,
                      ref RawGreen, ref GreenPercent, out GreenSum,
                      ref RawBlue, ref BluePercent, out BlueSum);
                }
            }
            List<HistogramTriplet> Triplets = new List<HistogramTriplet>();
            for (int i = 0; i < BinSize; i++)
            {
                HistogramTriplet Triplet = new HistogramTriplet(RedPercent[i], GreenPercent[i], BluePercent[i])
                {
                    RawRed = RawRed[i],
                    RawGreen = RawGreen[i],
                    RawBlue = RawBlue[i]
                };
                Triplets.Add(Triplet);
            }
            return Triplets;
        }

        /// <summary>
        /// Given a list of histogram triplets, return one channel's worth of data.
        /// </summary>
        /// <param name="Triplets">List of triplets.</param>
        /// <param name="WhichChannel">The channel to return.</param>
        /// <returns>List of channel data.</returns>
        public static List<double> HistogramTripletSplitter (List<HistogramTriplet> Triplets, HistogramChannels WhichChannel)
        {
            if (Triplets == null)
                return new List<double>();
            List<double> OneChannel = new List<double>();
            foreach (HistogramTriplet Triple in Triplets)
                OneChannel.Add(Triple.GetChannel(WhichChannel));
            return OneChannel;
        }

        /// <summary>
        /// Split the list of histogram triplets into three separate lists, one for each channel
        /// </summary>
        /// <param name="Triplets">List of histogram triplets to split.</param>
        /// <returns>Empty list on error, list of lists (in RGB order) of histogram data.</returns>
        public static List<List<double>> HistogramTripletSplitter (List<HistogramTriplet> Triplets)
        {
            if (Triplets == null)
                return new List<List<double>>();
            List<double> Red = new List<double>();
            List<double> Green = new List<double>();
            List<double> Blue = new List<double>();
            List<List<double>> Channels = new List<List<double>>();
            Channels.AddRange(new List<List<double>>()
            {
                HistogramTripletSplitter(Triplets,HistogramChannels.Red),
                HistogramTripletSplitter(Triplets,HistogramChannels.Green),
                HistogramTripletSplitter(Triplets,HistogramChannels.Blue)
            });
            return Channels;
        }

        /// <summary>
        /// Split the list of histogram triplets (for raw data) into three separate lists, one for each channel
        /// </summary>
        /// <param name="Triplets">List of histogram triplets to split.</param>
        /// <returns>Empty list on error, list of lists (in RGB order) of raw histogram data.</returns>
        public static List<List<UInt32>> RawHistogramTripletSplitter (List<HistogramTriplet> Triplets)
        {
            if (Triplets == null)
                return new List<List<UInt32>>();
            List<UInt32> Red = new List<UInt32>();
            List<UInt32> Green = new List<UInt32>();
            List<UInt32> Blue = new List<UInt32>();
            List<List<UInt32>> Channels = new List<List<UInt32>>();
            Channels.AddRange(new List<List<UInt32>>() { Red, Green, Blue });
            for(int i = 0; i<Triplets.Count;i++)
            {
                Red.Add(Triplets[i].RawRed);
                Red.Add(Triplets[i].RawGreen);
                Red.Add(Triplets[i].RawBlue);
            }
            return Channels;
        }
    }


}
