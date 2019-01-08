using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    public class CIELab : IEquatable<CIELab>, IColorSpace
    {
        public CIELab ()
        {
            CommonInitialization(0.0, 0.0, 0.0);
        }

        public CIELab (double L, double A, double B)
        {
            CommonInitialization(L, A, B);
        }

        private void CommonInitialization (double L, double A, double B)
        {
            this.L = L;
            this.A = A;
            this.B = B;
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
            v.Add(L);
            v.Add(A);
            v.Add(B);
            return v;
        }

        public bool Equals (CIELab Other)
        {
            if (Other == null)
                return false;
            return (Other.L == this.L && Other.A == this.A && Other.B == this.B);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as CIELab);
        }

        public string ToInputString ()
        {
            return "";
        }

        public string FormatDescription ()
        {
            return "";
        }

        public RGB ToRGB ()
        {
            return null;
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
                return "CIE";
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

        double _A = 0.0;
        public double A
        {
            get
            {
                return _A;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _A = value;
            }
        }

        double _B = 0.0;
        public double B
        {
            get
            {
                return _B;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _B = value;
            }
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            CIELab LocalLab = new CIELab();
            bool OK = TryParse(Raw, out LocalLab);
            if (OK)
                Final= LocalLab.ToRGBColor();
            return OK;
        }

        public bool TryParse (string Raw, out CIELab Final)
        {
            Final = null;
            double LL, LA, LB;
            if (!IroColorSpace.TryParse3Double(Raw, out LL, out LA, out LB))
                return false;
            Final = new CIELab(LL, LA, LB);
            return false;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            CIELab LocalCIELab = new CIELab();
            bool OK = TryParse(Raw, out LocalCIELab);
            if (OK)
            {
                this.L = LocalCIELab.L;
                this.A = LocalCIELab.A;
                this.B = LocalCIELab.B;
            }
            return OK;
        }
    }
}
