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
    /// Interaction logic for SmallColor.xaml
    /// </summary>
    public partial class SmallColor : Window
    {
        public SmallColor ()
        {
            InitializeComponent();
            TheColor = Colors.White;
            SetColor();
            OKClicked = false;
            ClearAllPoints = false;
        }

        public Color TheColor { get; set; }

        public bool OKClicked { get; internal set; }

        public bool ClearAllPoints { get; internal set; }

        private void SetColor ()
        {
            ColorValueInput.Text = MakeColorString(TheColor);
            ColorSample.Background = new SolidColorBrush(TheColor);
        }

        private string MakeColorString (Color SomeColor)
        {
            string Final = "#";
            Final += SomeColor.A.ToString("x2");
            Final += SomeColor.R.ToString("x2");
            Final += SomeColor.G.ToString("x2");
            Final += SomeColor.B.ToString("x2");
            return Final;
        }

        private void GetColorText ()
        {
            string Raw = ColorValueInput.Text;
            if (string.IsNullOrEmpty(Raw))
            {
                Raw = "#ffffffff";
                ColorValueInput.Text = Raw;
            }
            if (Raw[0] == '#')
                Raw = Raw.Substring(1);
            List<byte> Parts = ColorParts(Raw);
            Color NewColor = Color.FromArgb(Parts[0], Parts[1], Parts[2], Parts[3]);
            TheColor = NewColor;
            SetColor();
        }

        private List<byte> ColorParts (string RawValue)
        {
            byte A = 0xff;
            byte R = 0x0;
            byte G = 0x0;
            byte B = 0x0;
            List<byte> Parts = new List<byte>() { 0xff, 0x0, 0x0, 0x0 };
            UInt32 FullRaw = Convert.ToUInt32(RawValue, 16);
            A = (byte)((FullRaw & 0xff000000) >> 24);
            R = (byte)((FullRaw & 0x00ff0000) >> 16);
            G = (byte)((FullRaw & 0x0000ff00) >> 8);
            B = (byte)((FullRaw & 0x000000ff) >> 0);
            Parts.Clear();
            Parts.Add(A);
            Parts.Add(R);
            Parts.Add(G);
            Parts.Add(B);
            return Parts;
        }

        private void ColorValueInput_KeyDown (object sender, KeyEventArgs e)
        {
            TextBox TB = sender as TextBox;
            if (TB == null)
                return;
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                GetColorText();
            }
        }

        private void ColorValueInput_LostFocus (object sender, RoutedEventArgs e)
        {
            TextBox TB = sender as TextBox;
            if (TB == null)
                return;
            GetColorText();
        }

        private void SetColorButtonClicked (object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
            if (B == null)
                return;
            GetColorText();
        }

        private void OKButtonClicked (object Sender, RoutedEventArgs e)
        {
            OKClicked = true;
            this.Close();
        }

        private void CancelButtonClicked (object Sender, RoutedEventArgs e)
        {
            ClearAllPoints = false;
            OKClicked = false;
            this.Close();
        }

        private void ResetEverything(object Sender, RoutedEventArgs e)
        {
            ClearAllPoints = true;
        }
    }
}
