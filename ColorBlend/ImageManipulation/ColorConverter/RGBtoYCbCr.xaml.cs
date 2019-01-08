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
    /// Interaction logic for RGBtoYCbCr.xaml
    /// </summary>
    public partial class RGBtoYCbCr : Page
    {
        public RGBtoYCbCr ()
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

        private void HandleConvertClick (object Sender, RoutedEventArgs e)
        {
            byte Red = GetByte(RedInput);
            byte Green = GetByte(GreenInput);
            byte Blue = GetByte(BlueInput);

            double Y = 0.0;
            double U = 0.0;
            double V = 0.0;
            CBI.ConvertRGBtoYCbCr(Red, Green, Blue, out Y, out U, out V);

            StringBuilder dr = new StringBuilder();
            dr.Append(Y.ToString("n3"));
            dr.Append(", ");
            dr.Append(U.ToString("n3"));
            dr.Append(", ");
            dr.Append(V.ToString("n3"));
            YUVResult.Text = dr.ToString();
        }
    }
}
