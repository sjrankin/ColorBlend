using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorBlend
{
    public static class GenerativeTest
    {
        public static void CreateRandomColorImage (Image Destination)
        {
            double Width = Destination.ActualWidth;
            double Height = Destination.ActualHeight;
            double Stride = Width * 4;
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            byte[] Buffer = new byte[(int)Height * (int)Stride];
            CBI.RandomColoredBlock(ref Buffer, (int)Width, (int)Height, (int)Stride);
            Destination.Source = BitmapSource.Create((int)Width, (int)Height, 96.0, 96.0, PixelFormats.Bgra32,
              null, Buffer, (int)Stride);
        }

        public static void CreateRampingColorImage (Image Destination)
        {

        }

        public static void CreateGradientColorImage (Image Destination, bool IsHorizontal)
        {
            double Width = Destination.ActualWidth;
            double Height = Destination.ActualHeight;
            double Stride = Width * 4;
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            byte[] Buffer = new byte[(int)Height * (int)Stride];
            CBI.GradientGeneration(ref Buffer, Width, Height, Stride, IsHorizontal, Colors.Maroon, Colors.Red);
            Destination.Source = BitmapSource.Create((int)Width, (int)Height, 96.0, 96.0, PixelFormats.Bgra32,
               null, Buffer, (int)Stride);
        }

        public static void CreateGradientImage (Image Destination, bool IsHorizontal, Random Rand)
        {
            double Width = Destination.ActualWidth;
            double Height = Destination.ActualHeight;
            double Stride = Width * 4;
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            byte[] Buffer = new byte[(int)Height * (int)Stride];
            List<GradientStop> GSList = new List<GradientStop>();
#if true
            int Start = Rand.Next(2, 4);
            int End = Start + Rand.Next(2, 4);
//            int End = 4;// Start+ Rand.Next(2,4);
            for (int i = 0; i < End; i++)
            {
                GradientStop GS = new GradientStop();
                byte RR = (byte)Rand.Next(0xa0, 0xff);
                byte RG = (byte)Rand.Next(0xa0, 0xff);
                byte RB = (byte)Rand.Next(0xa0, 0xff);
                GS.Color = Color.FromArgb(0xff, RR, RG, RB);
                GS.Offset = Rand.NextNormal();
                GSList.Add(GS);
            }
#else
            GradientStop g0 = new GradientStop();
            g0.Offset = 0.0;
            g0.Color = Colors.Maroon;
            GSList.Add(g0);
            GradientStop g4 = new GradientStop();
            g4.Offset = 0.2;
            g4.Color = Colors.Purple;
            GSList.Add(g4);
            GradientStop g1 = new GradientStop();
            g1.Offset = 0.5;
            g1.Color = Colors.White;
            //GSList.Add(g1);
            GradientStop g3 = new GradientStop();
            g3.Offset = 0.75;
            g3.Color = Colors.Gold;
            GSList.Add(g3);
            GradientStop g2 = new GradientStop();
            g2.Offset = 1.0;
            g2.Color = Colors.Yellow;
            GSList.Add(g2);
#endif
            CBI.GradientGeneration2(ref Buffer, (int)Width, (int)Height, (int)Stride, IsHorizontal, GSList);
            Destination.Source = BitmapSource.Create((int)Width, (int)Height, 96.0, 96.0, PixelFormats.Bgra32,
                null, Buffer, (int)Stride);
        }

        public static void ImageChannelShift (Image Destination)
        {

        }
    }
}
