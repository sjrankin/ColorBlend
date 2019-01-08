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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for HSLtoRGB.xaml
    /// </summary>
    public partial class HSLtoRGB : Page
    {
        public HSLtoRGB ()
        {
            InitializeComponent();
            CBI = new ColorBlenderInterface();
        }

        ColorBlenderInterface CBI = null;
       
        private double GetDouble (TextBox TB)
        {
            if (TB == null)
                throw new ArgumentNullException("TB");
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
            {
                Raw = "0.0";
                TB.Text = Raw;
            }
            double Final = 0.0;
            bool ParsedOK = double.TryParse(Raw, out Final);
            if (!ParsedOK)
            {
                Final = 0.0;
                TB.Text = "0.0";
            }
            return Final;
        }

        private void HandleConvertClick (object Sender, RoutedEventArgs e)
        {
            double Hue = GetDouble(HueInput);
            double Saturation = GetDouble(SaturationInput);
            double Luminance = GetDouble(LuminanceInput);

            double sR = 0.0;
            double sG = 0.0;
            double sB = 0.0;
            CBI.ConvertHSLToRGBDouble(Hue, Saturation, Luminance, out sR, out sG, out sB);

            byte bR = 0;
            byte bG = 0;
            byte bB = 0;
            CBI.ConvertHSLToRGBByte(Hue, Saturation, Luminance, out bR, out bG, out bB);

            StringBuilder dr = new StringBuilder();
            dr.Append(sR.ToString("n3"));
            dr.Append(", ");
            dr.Append(sG.ToString("n3"));
            dr.Append(", ");
            dr.Append(sB.ToString("n3"));
            RGBDoubleResult.Text = dr.ToString();

            StringBuilder br = new StringBuilder();
            br.Append("0x");
            br.Append(bR.ToString("x2"));
            br.Append(", ");
            br.Append("0x");
            br.Append(bG.ToString("x2"));
            br.Append(", ");
            br.Append("0x");
            br.Append(bB.ToString("x2"));
            RGBByteResult.Text = br.ToString();

            StringBuilder dcr = new StringBuilder();
            dcr.Append(bR.ToString());
            dcr.Append(", ");
            dcr.Append(bG.ToString());
            dcr.Append(", ");
            dcr.Append(bB.ToString());
            RGBDecResult.Text = dcr.ToString();
        }
    }
}
