using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    //http://en.wikipedia.org/wiki/TSL_color_space
    //http://en.wikipedia.org/wiki/List_of_color_spaces_and_their_uses
    public class TSL : IEquatable<TSL>, IColorSpace
    {
        public TSL ()
        {
            CommonInitialization(0.0, 0.0, 0.0);
        }

        public TSL (double T, double S, double L)
        {
            CommonInitialization(T, S, L);
        }

        private void CommonInitialization (double T, double S, double L)
        {
            this.T = T;
            this.S = S;
            this.L = L;
            CanConvertTo = new HashSet<ColorSpaces>();
            CanConvertTo.Add(ColorSpaces.RGB);
        }

        public HashSet<ColorSpaces> CanConvertTo { get; internal set; }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public ColorSpaces ColorSpace { get; internal set; }

        public List<double> GetValues ()
        {
            List<double> v = new List<double>();
            v.Add(T);
            v.Add(S);
            v.Add(L);
            return v;
        }

        public bool Equals (TSL Other)
        {
            if (Other == null)
                return false;
            return (Other.T == this.T && Other.S == this.S && Other.L == this.L);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as TSL);
        }

        public string ToInputString ()
        {
            return ToString();
        }

        public string FormatDescription ()
        {
            return "";
        }

        public RGB ToRGB ()
        {
            double x = (1.0 / Math.Tan(2.0 * Math.PI * T));
            double r = 0.0;
            double g = 0.0;
            if (T == 0.0)
                r = (Math.Sqrt(5.0) / 3.0) * S;
            else
                r = (x * g) + (1.0 / 3.0);
            if (T > 0.5)
                g = -(Math.Sqrt(5.0 / (9.0 * ((x * x) + 1.0))) * S);
            else
                if (T < 0.5)
                g = (Math.Sqrt(5.0 / (9.0 * ((x * x) + 1.0))) * S);
            else
                g = 0.0;
            double k = L / ((r * 0.185) + (g * 0.473) + 0.114);
            return new RGB(r * k, g * k, k * (1 - r - g));
        }

        public Color ToRGBColor ()
        {
            return Colors.Transparent;
        }

        public string ColorLabel
        {
            get
            {
                return StaticColorLabel;
            }
        }

        public static string StaticColorLabel
        {
            get
            {
                return "TSL";
            }
        }

        double _T = 0.0;
        public double T
        {
            get
            {
                return _T;
            }
            set
            {
                _T = value;
            }
        }

        double _S = 0.0;
        public double S
        {
            get
            {
                return _S;
            }
            set
            {
                _S = value;
            }
        }

        double _L = 0.0;
        public double L
        {
            get
            {
                return _L;
            }
            set
            {
                _L = value;
            }
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            TSL LocalTSL = new TSL();
            bool OK = TryParse(Raw, out LocalTSL);
            if (OK)
                Final = LocalTSL.ToRGBColor();
            return OK;
        }

        public bool TryParse (string Raw, out TSL Final)
        {
            Final = null;
            double LT, LS, LL;
            if (!IroColorSpace.TryParse3Double(Raw, out LT, out LS, out LL))
                return false;
            Final = new TSL(LT, LS, LL);
            return true;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            TSL LocalTSL = new TSL();
            bool OK = TryParse(Raw, out LocalTSL);
            if (OK)
            {
                this.T = LocalTSL.T;
                this.S = LocalTSL.S;
                this.L = LocalTSL.L;
            }
            return OK;
        }
    }
}
