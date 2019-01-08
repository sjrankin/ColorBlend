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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ColorConversions.xaml
    /// </summary>
    public partial class ColorConversions : Window
    {
        public ColorConversions ()
        {
            InitializeComponent();
            FromRGB.IsChecked = true;
            ToRGB.IsChecked = true;
        }

        Page GetPage (string From, string To)
        {
            if (string.IsNullOrEmpty(From))
                return null;
            if (string.IsNullOrEmpty(To))
                return null;
            From = From.ToLower();
            To = To.ToLower();
            Tuple<string, string> Key = new Tuple<string, string>(From, To);
            if (!ConversionMap.ContainsKey(Key))
                return null;
            return ConversionMap[Key];
        }

        private Dictionary<Tuple<string, string>, Page> ConversionMap = new Dictionary<Tuple<string, string>, Page>()
        {
            {new Tuple<string, string>("rgb","hsl"), new RGBtoHSL() },
            {new Tuple<string, string>("hsl","rgb"), new HSLtoRGB() },
            {new Tuple<string, string>("rgb","yuv"), new RGBtoYUV() },
            {new Tuple<string, string>("yuv","rgb"), new YUVtoRGB() },
            {new Tuple<string, string>("rgb","ycbcr"), new YCbCrtoRGB() },
            {new Tuple<string, string>("ycbcr","rgb"), new RGBtoYCbCr() },
            {new Tuple<string, string>("rgb","yiq"), new RGBtoYIQ() },
            {new Tuple<string, string>("yiq","rgb"), new YIQtoRGB() },
            {new Tuple<string, string>("rgb","hsv"), new RGBtoHSV() },
            {new Tuple<string, string>("hsv","rgb"), new HSVtoRGB() },
            {new Tuple<string, string>("rgb","cmy"), new RGBtoCMY() },
            {new Tuple<string, string>("cmy","rgb"), new CMYtoRGB() },
            {new Tuple<string, string>("rgb","cmyk"), new RGBtoCMYK() },
            {new Tuple<string, string>("cmyk","rgb"), new CMYKtoRGB() },
            {new Tuple<string, string>("rgb","xyz"), new RGBtoXYZ() },
            {new Tuple<string, string>("xyz","rgb"), new RGBtoXYZ() },
            {new Tuple<string, string>("rgb","cielab"), new RGBtoLAB() },
            {new Tuple<string, string>("cielab","rgb"), new LABtoRGB() },
            {new Tuple<string, string>("xyz","cielab"), new XYZtoLAB() },
            {new Tuple<string, string>("cielab","xyz"), new LABtoXYZ() },
            {new Tuple<string, string>("rgb","tsl"), new RGBtoTSL() },
            {new Tuple<string, string>("tsl","rgb"), new TSLtoRGB() },
            {new Tuple<string, string>("rgb","ryb"), new RGBtoRYB() },
            {new Tuple<string, string>("ryb","rgb"), new RYBtoRGB() },
            {new Tuple<string, string>("rgb","ycgco"), null },
            {new Tuple<string, string>("ycgco","rgb"), null },
            {new Tuple<string, string>("rgb","ydbdr"), null },
            {new Tuple<string, string>("ydbdr","rgb"), null },
        };

        private string GetColorSpace (StackPanel SP)
        {
            if (SP == null)
                return "";
            if (SP.Children.Count < 1)
                return "";
            foreach (object SPObject in SP.Children)
            {
                RadioButton RB = SPObject as RadioButton;
                if (RB == null)
                    continue;
                if (RB.IsChecked.Value)
                {
                    return RB.Content as string;
                }
            }
            return "";
        }

        private void SetConversionPage ()
        {
            string From = GetColorSpace(FromPanel);
            if (string.IsNullOrEmpty(From))
                return;
            string To = GetColorSpace(ToPanel);
            if (string.IsNullOrEmpty(To))
                return;
            Page ConversionPage = GetPage(From, To);
            if (ConversionPage == null)
            {
                ConverterCalcPage.Navigate(new NoConversion());
                return;
            }
            ConverterCalcPage.Navigate(ConversionPage);
        }

        private void FromColorSpaceClick (object Sender, RoutedEventArgs e)
        {
            SetConversionPage();
        }

        private void ToColorSpaceClick (object Sender, RoutedEventArgs e)
        {
            SetConversionPage();
        }
    }
}
