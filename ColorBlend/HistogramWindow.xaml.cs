using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for HistogramWindow.xaml
    /// </summary>
    public partial class HistogramWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HistogramWindow ()
        {
            InitializeComponent();
            FileNameOut.Text = "";
        }

        /// <summary>
        /// Handle window closing.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void CloseViewer (object Sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Open an image that will be the source of the displayed histogram.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void OpenImage (object Sender, RoutedEventArgs e)
        {
            FileNameOut.Text = "";
            OpenFileDialog OFD = new OpenFileDialog
            {
                Title = "Open Image",
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".jpg",
                DereferenceLinks = true,
                Filter = "Jpg files (.jpg)|*.jpg|Png files (.png)|*.png",
                InitialDirectory = Environment.CurrentDirectory,
                Multiselect = false,
                ShowReadOnly = true
            };
            bool? Result = OFD.ShowDialog();
            if (!Result.HasValue)
                return;
            if (!Result.Value)
                return;
            BitmapImage BI = new BitmapImage();
            BI.BeginInit();
            BI.StreamSource = OFD.OpenFile();
            BI.EndInit();
            BitmapSource ImageSource = BI as BitmapSource;
            WB = new WriteableBitmap(ImageSource);
            FileNameOut.Text = System.IO.Path.GetFileName(OFD.FileName);
            DrawHistogram(WB);
        }

        /// <summary>
        /// Reset channel data.
        /// </summary>
        private void ClearChannelData ()
        {
            RedPercent = null;
            GreenPercent = null;
            BluePercent = null;
        }

        WriteableBitmap WB = null;
        int BinSize = 16;

        /// <summary>
        /// Draw a histogram using the image in <paramref name="Img"/>.
        /// </summary>
        /// <param name="Img">The image used for source data for the histogram.</param>
        private void DrawHistogram (WriteableBitmap Img)
        {
            if (Img == null)
                return;
            ClearChannelData();
            DrawHistogram2(Img);
        }

        double[] Combined = null;
        double[] RedPercent = null;
        double[] GreenPercent = null;
        double[] BluePercent = null;
        UInt32[] RawRed = null;
        UInt32[] RawGreen = null;
        UInt32[] RawBlue = null;

        /// <summary>
        /// Generate a histogram from <paramref name="Img"/> and then draw the histogram.
        /// </summary>
        /// <param name="Img">The image whose histogram will be generated/displayed.</param>
        private void DrawHistogram2 (WriteableBitmap Img)
        {
            if (Img == null)
                return;
            //ClearChannelData();
            int ImageWidth = Img.PixelWidth;
            int ImageHeight = Img.PixelHeight;
            int ImageStride = Img.BackBufferStride;
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            RedPercent = new double[BinSize];
            RawRed = new UInt32[BinSize];
            RedSum = 0;
            GreenPercent = new double[BinSize];
            RawGreen = new UInt32[BinSize];
            GreenSum = 0;
            BluePercent = new double[BinSize];
            RawBlue = new UInt32[BinSize];
            BlueSum = 0;
            unsafe
            {
                unsafe
                {
                    CBI.MakeHistogram((byte*)Img.BackBuffer, ImageWidth, ImageHeight, ImageStride,
                      BinSize,
                      ref RawRed, ref RedPercent, out RedSum,
                      ref RawGreen, ref GreenPercent, out GreenSum,
                      ref RawBlue, ref BluePercent, out BlueSum);
                }
            }

            MaxRedPercent = 0.0;
            MaxGreenPercent = 0.0;
            MaxBluePercent = 0.0;
            for (int i = 0; i < BinSize; i++)
            {
                if (RedPercent[i] > MaxRedPercent)
                    MaxRedPercent = RedPercent[i];
                if (GreenPercent[i] > MaxGreenPercent)
                    MaxGreenPercent = GreenPercent[i];
                if (BluePercent[i] > MaxBluePercent)
                    MaxBluePercent = BluePercent[i];
            }
            DoDrawHistogram(RedPercent, GreenPercent, BluePercent);
        }

        double MaxRedPercent = 0.0;
        double MaxGreenPercent = 0.0;
        double MaxBluePercent = 0.0;
        double GreatestPercent = 0.0;

        /// <summary>
        /// Draw a histogram with the passed data.
        /// </summary>
        /// <param name="Red">Red channel data.</param>
        /// <param name="Green">Green channel data.</param>
        /// <param name="Blue">Blue channel data.</param>
        private void DoDrawHistogram (double[] Red, double[] Green, double[] Blue)
        {
            HistogramHolder.Children.Clear();
            if (BlueCheck == null || GreenCheck == null || RedCheck == null)
                return;
            bool DoBlue = BlueCheck.IsChecked.Value;
            bool DoGreen = GreenCheck.IsChecked.Value;
            bool DoRed = RedCheck.IsChecked.Value;

#if false
            GreatestPercent = Math.Max(MaxRedPercent, Math.Max(MaxGreenPercent, MaxBluePercent));
            if (DoRed)
                AddHistogramChannel(Red, Colors.Red, GreatestPercent);
            if (DoGreen)
                AddHistogramChannel(Green, Color.FromRgb(0x0, 0xff, 0x0), GreatestPercent);
            if (DoBlue)
                AddHistogramChannel(Blue, Colors.Blue, GreatestPercent);
#else
            if (AsCombined)
            {
                Combined = new double[BinSize];
                double MaxCombined = 0.0;
                GraySum = 0;
                for (int i = 0; i < BinSize; i++)
                {
                    GraySum += (RawBlue[i] + RawGreen[i] + RawRed[i]);
                    Combined[i] = Red[i] + Green[i] + Blue[i];
                    if (Combined[i] > MaxCombined)
                        MaxCombined = Combined[i];
                }
                AddHistogramChannel(Combined, Color.FromArgb(0xa0, 0x80, 0x80, 0x80), MaxCombined);
            }
            else
            {
                if (DoRed)
                    AddHistogramChannel(Red, Colors.Red, MaxRedPercent);
                if (DoGreen)
                    AddHistogramChannel(Green, Color.FromRgb(0x0, 0xff, 0x0), MaxGreenPercent);
                if (DoBlue)
                    AddHistogramChannel(Blue, Colors.Blue, MaxBluePercent);
            }
#endif
            TextBlock TB = new TextBlock
            {
                Text = "Red: " + MaxRedPercent.ToString("N5") + ", Green: " + MaxGreenPercent.ToString("N5") + ", Blue: " + MaxBluePercent.ToString("N5"),
                FontSize = 15,
                FontWeight = FontWeights.Bold
            };
            HistogramHolder.Children.Add(TB);
        }

        /// <summary>
        /// Draw a histogram channel.
        /// </summary>
        /// <param name="ChannelData">The channel data to draw.</param>
        /// <param name="DisplayColor">The background color of the histogram.</param>
        /// <param name="MaxPercent">Maximum percent for <paramref name="ChannelData"/>.</param>
        private void AddHistogramChannel (double[] ChannelData, Color DisplayColor, double MaxPercent)
        {
            if (SmoothCheck == null)
                return;
            if (!SmoothCheck.IsChecked.HasValue)
                return;
            if (SmoothCheck.IsChecked.Value)
                AddSmoothHistogramChannel(ChannelData, DisplayColor, MaxPercent);
            else
                AddDiscreteHistogramChannel(ChannelData, DisplayColor, MaxPercent);
        }

        UInt32 RedSum = 0;
        UInt32 GreenSum = 0;
        UInt32 BlueSum = 0;
        UInt32 GraySum = 0;

        /// <summary>
        /// Draw a channel's worth of histogram polygons.
        /// </summary>
        /// <param name="ChannelData">The channel data to draw.</param>
        /// <param name="ChannelColor">The color of the channel.</param>
        /// <param name="MaxPercent">Determines vertical extent.</param>
        private void AddSmoothHistogramChannel (double[] ChannelData, Color ChannelColor, double MaxPercent)
        {
            if (ChannelData == null)
                return;
            double ViewportHeight = HistogramHolder.ActualHeight;
            double ViewportWidth = HistogramHolder.ActualWidth;
            double HRatio = ViewportWidth / (double)BinSize;
            Polygon PG = new Polygon
            {
                Fill = new SolidColorBrush(Color.FromArgb(0x80, ChannelColor.R, ChannelColor.G, ChannelColor.B))
            };
            SolidColorBrush StrokeBrush = new SolidColorBrush(Color.FromArgb(0xd0, 0x0, 0x0, 0x0));
            PG.Stroke = StrokeBrush;
            PG.StrokeThickness = 2;
            PG.VerticalAlignment = VerticalAlignment.Bottom;
            PG.HorizontalAlignment = HorizontalAlignment.Left;
            PG.StrokeLineJoin = PenLineJoin.Round;
            double AdjustedViewportHeight = ViewportHeight - (PG.StrokeThickness * 2.0);

            PG.Points.Add(new Point(0, ViewportHeight));
            for (int i = 0; i < BinSize; i++)
            {
                double ChannelPercent = ChannelData[i] / MaxPercent;
                double FinalY = ViewportHeight - (ChannelPercent * AdjustedViewportHeight);
                double FinalX = (double)i * HRatio;
                PG.Points.Add(new Point(FinalX, FinalY));
            }
            PG.Points.Add(new Point(BinSize * HRatio, ViewportHeight));
            Grid.SetRow(PG, 0);
            Grid.SetColumn(PG, 0);
            HistogramHolder.Children.Add(PG);
        }

        /// <summary>
        /// Draw a channel's worth of histogram rectangles.
        /// </summary>
        /// <param name="ChannelData">The channel data to draw.</param>
        /// <param name="ChannelColor">The color of the channel.</param>
        /// <param name="MaxPercent">Determines vertical extent.</param>
        private void AddDiscreteHistogramChannel (double[] ChannelData, Color ChannelColor, double MaxMagnitude)
        {
            if (ChannelData == null)
                return;
            double ViewportHeight = HistogramHolder.ActualHeight;
            double ViewportWidth = HistogramHolder.ActualWidth;
            double HRatio = ViewportWidth / (double)BinSize;
            Canvas C = new Canvas
            {
                Height = ViewportHeight,
                Width = ViewportWidth,
                Background = Brushes.Transparent
            };
            for (int i = 0; i < BinSize; i++)
            {
                Rectangle R = new Rectangle
                {
                    StrokeThickness = 0.0,
                    Fill = new SolidColorBrush(Color.FromArgb(0x80, ChannelColor.R, ChannelColor.G, ChannelColor.B)),
                    RadiusX = 0.0,
                    RadiusY = 0.0,
                    Width = HRatio
                };
                double ChannelPercent = ChannelData[i] / MaxMagnitude;
                R.Height = ChannelPercent * ViewportHeight;
                Canvas.SetLeft(R, i * HRatio);
                Canvas.SetBottom(R, 0);
                C.Children.Add(R);
            }
            Grid.SetRow(C, 0);
            Grid.SetColumn(C, 0);
            HistogramHolder.Children.Add(C);
        }

        /// <summary>
        /// Handle hiding or displaying various histogram channels.
        /// </summary>
        /// <param name="Sender">The checkbox that was checked.</param>
        /// <param name="e">Not used.</param>
        private void ChannelCheck (object Sender, RoutedEventArgs e)
        {
            CheckBox CB = Sender as CheckBox;
            if (CB == null)
                return;
            DoDrawHistogram(RedPercent, GreenPercent, BluePercent);
        }

        /// <summary>
        /// Handle changes to the size of the histogram display control.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HistogramHolder_SizeChanged (object Sender, SizeChangedEventArgs e)
        {
            if (Sender == null)
                return;
            DoDrawHistogram(RedPercent, GreenPercent, BluePercent);
        }

        /// <summary>
        /// Handle mouse move events in the histogram by displaying information under the mouse.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Event data.</param>
        private void HistogramHolder_MouseMove (object Sender, MouseEventArgs e)
        {
            if (Sender == null)
                return;
            if (CursorRed == null || CursorGreen == null || CursorBlue == null)
                return;
            if (RedPercent == null || GreenPercent == null || BluePercent == null)
                return;
            double ViewportWidth = HistogramHolder.ActualWidth;
            double HRatio = ViewportWidth / (double)BinSize;
            Point mouse = e.GetPosition(HistogramHolder);
            int Index = (int)(mouse.X / HRatio);
            if (Index >= BinSize)
                return;
#if true
            if (RedCheck.IsChecked.Value)
            {
                if (RedSum == 0)
                {
                    CursorRed.Text = "";
                }
                else
                {
                    double R = (double)RawRed[Index] / (double)RedSum;
                    //R *= 100.0;
                    CursorRed.Text = R.ToString("N3") + "%";
                }
            }
            if (GreenCheck.IsChecked.Value)
            {
                if (GreenSum == 0)
                {
                    CursorGreen.Text = "";
                }
                else
                {
                    double G = (double)RawGreen[Index] / (double)GreenSum;
                    //G *= 100.0;
                    CursorGreen.Text = G.ToString("N3") + "%";
                }
            }
            if (BlueCheck.IsChecked.Value)
            {
                if (BlueSum == 0)
                {
                    CursorBlue.Text = "";
                }
                else
                {
                    double B = (double)RawBlue[Index] / (double)BlueSum;
                    //B *= 100.0;
                    CursorBlue.Text = B.ToString("N3") + "%";
                }
            }
            if (CombinedCheck.IsChecked.Value)
            {
                if (GraySum==0)
                {
                    CursorGray.Text = "";
                }
                else
                {
                    double G = (double)Combined[Index] / (double)GraySum;
                    //G *= 100.0;
                    CursorGray.Text = G.ToString("N3") + "%";
                }
            }

#else
            CursorRed.Text = ((int)(RedPercent[Index] * (double)RedSum)).ToString();
            CursorGreen.Text = ((int)(GreenPercent[Index] * (double)GreenSum)).ToString();
            CursorBlue.Text = ((int)(BluePercent[Index] * (double)BlueSum)).ToString();
#endif
            MouseIndexOut.Text = Index.ToString();
        }

        /// <summary>
        /// Toggle smooth histogram display.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void DoSmoothCheck (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            DoDrawHistogram(RedPercent, GreenPercent, BluePercent);
        }

        /// <summary>
        /// Map between the user interface bin size and the actual bin size.
        /// </summary>
        Dictionary<string, int> BinMap = new Dictionary<string, int>()
        {
            {"Eight", 8},
            {"Ten", 10 },
            {"Sixteen", 16 },
            {"ThirtyTwo", 32},
            {"Fifty", 50},
            {"SixtyFour", 64},
            {"OneHundred", 100},
            {"OneTwentyEight", 128},
            {"TwoHundred", 200},
            {"TwoFiftySix", 256}
        };

        /// <summary>
        /// Handle histogram bin size changes.
        /// </summary>
        /// <param name="Sender">The combo box with the bin sizes.</param>
        /// <param name="e">Not used.</param>
        private void BinSizeChanged (object Sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = Sender as ComboBox;
            if (CB == null)
                return;
            ComboBoxItem CBI = (ComboBoxItem)CB.SelectedItem;
            if (CBI == null)
                return;
            if (!BinMap.ContainsKey(CBI.Name))
                BinSize = 256;
            else
            {
                int NewBinSize = BinMap[CBI.Name];
                if (NewBinSize == BinSize)
                    return;
                BinSize = NewBinSize;
            }
            //Need to get a new set of histogram data - can't use the old set because the bin size is different.
            DrawHistogram2(WB);
        }

        private bool AsCombined = false;

        /// <summary>
        /// Toggle color or gray histogram display. Gray is the combination of the red, green, and blue histograms.
        /// </summary>
        /// <param name="Sender">The checkbox that triggered this event.</param>
        /// <param name="e">Not used.</param>
        private void DoCombinedCheck (object Sender, RoutedEventArgs e)
        {
            CheckBox CB = Sender as CheckBox;
            if (CB == null)
                return;
            if (!CB.IsChecked.HasValue)
                return;
            if (CB.IsChecked.Value == AsCombined)
                return;
            if (CB.IsChecked.Value)
            {
                RedCheck.IsChecked = false;
                GreenCheck.IsChecked = false;
                BlueCheck.IsChecked = false;
                RedCursor.IsEnabled = false;
                GreenCursor.IsEnabled = false;
                BlueCursor.IsEnabled = false;
                GrayCursor.IsEnabled = true;
                CursorRed.Text = "";
                CursorGreen.Text = "";
                CursorBlue.Text = "";
            }
            else
            {
                RedCheck.IsChecked = true;
                GreenCheck.IsChecked = true;
                BlueCheck.IsChecked = true;
                RedCursor.IsEnabled = true;
                GreenCursor.IsEnabled = true;
                BlueCursor.IsEnabled = true;
                GrayCursor.IsEnabled = false;
                CursorGray.Text = "";
            }
            AsCombined = CB.IsChecked.Value;
            DoDrawHistogram(RedPercent, GreenPercent, BluePercent);
        }
    }
}
