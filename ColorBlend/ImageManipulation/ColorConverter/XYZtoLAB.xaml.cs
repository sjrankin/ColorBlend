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
    /// Interaction logic for XYZtoLAB.xaml
    /// </summary>
    public partial class XYZtoLAB : Page
    {
        public XYZtoLAB ()
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
            double X = GetDouble(XInput);
            double Y = GetDouble(YInput);
            double Z = GetDouble(ZInput);

            double L = 0.0;
            double A = 0.0;
            double B = 0.0;
            CBI.ConvertXYZtoCIELAB(X, Y, Z, out L, out A, out B);

            StringBuilder dr = new StringBuilder();
            dr.Append(L.ToString("n3"));
            dr.Append(", ");
            dr.Append(A.ToString("n3"));
            dr.Append(", ");
            dr.Append(B.ToString("n3"));
            LABResult.Text = dr.ToString();
        }
    }
}
