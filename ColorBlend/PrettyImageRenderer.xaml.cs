using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for PrettyImageRenderer.xaml
    /// </summary>
    public partial class PrettyImageRenderer : Window
    {
        public PrettyImageRenderer ()
        {
            throw new InvalidOperationException("Don't use this constructor.");
        }

        public PrettyImageRenderer (MainWindow Main, Random Rand)
        {
            InitializeComponent();
            if (Main == null || Rand == null)
                throw new ArgumentNullException("Null parameters in constructor.");
            this.Main = Main;
            this.Rand = Rand;
        }

        private MainWindow Main;

        private double GetImageWidth()
        {
            string Raw = ImageWidthInput.Text;
            if (string.IsNullOrEmpty(Raw))
                Raw = "800";
            double dtemp = 0.0;
            if (double.TryParse(Raw, out dtemp))
                return dtemp;
            return 800.0;
        }

        private double GetImageHeight ()
        {
            string Raw = ImageHeightInput.Text;
            if (string.IsNullOrEmpty(Raw))
                Raw = "1200";
            double dtemp = 0.0;
            if (double.TryParse(Raw, out dtemp))
                return dtemp;
            return 1200.0;
        }

        private double GetImageDPI ()
        {
            string Raw = ImageDPIInput.Text;
            if (string.IsNullOrEmpty(Raw))
                Raw = "300";
            double dtemp = 0.0;
            if (double.TryParse(Raw, out dtemp))
                return dtemp;
            return 300.0;
        }

        private Image MakeDrawingImage ()
        {
            Image NewImage = new Image
            {
                Width = GetImageWidth(),
                Height = GetImageHeight()
            };
            return NewImage;
        }

        private Random Rand;

        private List<ColorPoint> MakeColorBlobs (double ImageWidth, double ImageHeight)
        {
            List<ColorPoint> Points = new List<ColorPoint>();
            int BlobCount = 1;
            if (RandomBlobCountCheck.IsChecked.Value)
                BlobCount = Rand.Next(25, 200);
            else
                BlobCount = BlobCountInput.IntValue(500);
            for (int i = 0; i < BlobCount; i++)
            {
                ColorPoint CP = new ColorPoint
                {
                    Name = "Point " + i.ToString(),
                    AlphaStart = 1.0,
                    AlphaEnd = 0.0,
                    UseAlpha = true,
                    UseAbsoluteOnly = true,
                    PointColor = Main.RandomColor(Rand, 0x0a0, 0xff, false),
                    Width = Rand.NextEven(64, 128)
                };
                CP.Height = CP.Width;
                int Left = Rand.Next(0, (int)ImageWidth);
                int Top = Rand.Next(0, (int)ImageHeight);
                CP.AbsolutePoint = new PointEx(Left, Top);
                Points.Add(CP);
            }
            return Points;
        }

        private void HandleOKClick (object Sender, RoutedEventArgs e)
        {
            Image Surface = MakeDrawingImage();
            Main.PurePoints = MakeColorBlobs(Surface.ActualWidth,Surface.ActualHeight);
            this.Close();
        }

        private void HandleCancelClick (object Sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
