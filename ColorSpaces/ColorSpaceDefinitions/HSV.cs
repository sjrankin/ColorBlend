using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    [DebuggerDisplay("{NiceHSV}")]
    public class HSV : IEquatable<HSV>, IColorSpace
    {
        public HSV ()
        {
            CommonInitialization(0, 0, 0);
        }

        public HSV (double H, double S, double V)
        {
            CommonInitialization(H, S, V);
        }

        private void CommonInitialization (double H, double S, double V)
        {
            ID = Guid.NewGuid();
            Name = "";
            ColorSpace = ColorSpaces.HSV;
            this.H = H;
            this.S = S;
            this.V = V;
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
            v.Add(H);
            v.Add(S);
            v.Add(V);
            return v;
        }

        public string FormatDescription ()
        {
            return "";
        }

        double _H = 0.0;
        public double H
        {
            get
            {
                return _H;
            }
            set
            {
                _H = value;
            }
        }

        public string ColorLabel
        {
            get
            {
                return StaticColorLabel;
            }
        }

        internal static string StaticColorLabel
        {
            get
            {
                return "HSV";
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
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _S = value;
            }
        }

        double _V = 0.0;
        public double V
        {
            get
            {
                return _V;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _V = value;
            }
        }

        public string NiceHSV
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(H.ToString("n2"));
            sb.Append(", ");
            sb.Append(S.ToString("n2"));
            sb.Append(", ");
            sb.Append(V.ToString("n2"));
            return sb.ToString();
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            HSV LocalHSV = new HSV();
            bool OK = TryParse(Raw, out LocalHSV);
            if (OK)
                Final = LocalHSV.ToRGBColor();
            return false;
        }

        public bool TryParse (string Raw, out HSV Final)
        {
            Final = null;
            double LH, LS, LV;
            if (!IroColorSpace.TryParse3Double(Raw, out LH, out LS, out LV))
                return false;
            Final = new HSV(LH, LS, LV);
            return true;
        }

        public string ToInputString ()
        {
            return ToString();
        }

        public bool Equals (HSV Other)
        {
            if (Other == null)
                return false;
            return (Other.H == this.H && Other.S == this.S && Other.V == this.V);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as HSV);
        }

        public Color ToRGBColor ()
        {
            return ToRGB().ToRGBColor();
        }

        public RGB ToRGB ()
        {
            int i;
            double f, p, q, t;
            double _h = H;
            double _s = S;
            double _v = V;
            int r, g, b;
            if (_s == 0)
            {
                // achromatic (grey)
                return new RGB(0xff, V, V, V);
            }
            _h /= 60;            // sector 0 to 5
            i = (int)Math.Floor(_h);
            f = _h - i;          // factorial part of h
            p = _v * (1 - _s);
            q = _v * (1 - _s * f);
            t = _v * (1 - _s * (1 - f));
            switch (i)
            {
                case 0:
                    r = (byte)_v;
                    g = (byte)t;
                    b = (byte)p;
                    break;
                case 1:
                    r = (byte)q;
                    g = (byte)_v;
                    b = (byte)p;
                    break;
                case 2:
                    r = (byte)p;
                    g = (byte)_v;
                    b = (byte)t;
                    break;
                case 3:
                    r = (byte)p;
                    g = (byte)q;
                    b = (byte)_v;
                    break;
                case 4:
                    r = (byte)t;
                    g = (byte)p;
                    b = (byte)_v;
                    break;
                default:        // case 5:
                    r = (byte)_v;
                    g = (byte)p;
                    b = (byte)q;
                    break;
            }
            return new RGB(0xff, r, g, b);
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            HSV LocalHSV = new HSV();
            bool OK = TryParse(Raw, out LocalHSV);
            if (OK)
            {
                this.H = LocalHSV.H;
                this.S = LocalHSV.S;
                this.V = LocalHSV.V;
            }
            return OK;
        }
    }
}
