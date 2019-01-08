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
    /// Interaction logic for RGBtoHSV.xaml
    /// </summary>
    public partial class RGBtoHSV : Page
    {
        public RGBtoHSV ()
        {
            InitializeComponent();
            CBI = new ColorBlenderInterface();
        }

        ColorBlenderInterface CBI = null;

        private byte GetByte (TextBox TB)
        {
            if (TB == null)
                throw new ArgumentNullException("TB");
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
            {
                Raw = "0";
                TB.Text = Raw;
            }
            Raw = Raw.Trim();
            bool IsHexSource = false;
            if (Raw[0] == '#')
            {
                IsHexSource = true;
                Raw = Raw.Substring(1);
                if (string.IsNullOrEmpty(Raw))
                    Raw = "0";
            }
            if (Raw.Length >= 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    IsHexSource = true;
                    Raw = Raw.Substring(2);
                    if (string.IsNullOrEmpty(Raw))
                        Raw = "0";
                }
            }
            byte Final = 0;
            if (IsHexSource)
            {
                Final = Convert.ToByte(Raw, 16);
                return Final;
            }
            bool ParsedOK = byte.TryParse(Raw, out Final);
            if (!ParsedOK)
            {
                Final = 0;
                TB.Text = "0";
            }
            return Final;
        }

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
            byte bR = 0;
            byte bG = 0;
            byte bB = 0;
            bR = GetByte(RedByte);
            bG = GetByte(GreenByte);
            bB = GetByte(BlueByte);

            double Hue = 0.0;
            double Saturation = 0.0;
            double Value = 0.0;
            CBI.ConvertRGBtoHSV(bR, bG, bB, out Hue, out Saturation, out Value);

            StringBuilder sb = new StringBuilder();
            sb.Append(Hue);
            sb.Append("°");
            sb.Append(", ");
            sb.Append(Saturation.ToString("n2"));
            sb.Append(", ");
            sb.Append(Value.ToString("n2"));
            HSVResult.Text = sb.ToString();
        }
    }
}
