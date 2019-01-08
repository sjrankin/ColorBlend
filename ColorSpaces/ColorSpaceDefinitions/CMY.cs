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
    [DebuggerDisplay("{NiceCMY}")]
    public class CMY : IEquatable<CMY>, IColorSpace
    {
        public CMY ()
        {
            CommonInitialization(0.0, 0.0, 0.0);
        }

        public CMY (double C, double M, double Y)
        {
            CommonInitialization(C, M, Y);
        }

        private void CommonInitialization (double C, double M, double Y)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            CanConvertTo = new HashSet<ColorSpaces>();
            CanConvertTo.Add(ColorSpaces.RGB);
            CanConvertTo.Add(ColorSpaces.CMYK);
        }

        public HashSet<ColorSpaces> CanConvertTo { get; internal set; }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public ColorSpaces ColorSpace { get; internal set; }

        public List<double> GetValues ()
        {
            List<double> v = new List<double>();
            v.Add(C);
            v.Add(M);
            v.Add(Y);
            return v;
        }

        public bool Equals (CMY Other)
        {
            if (Other == null)
                return false;
            return (Other.C == this.C && Other.M == this.M && Other.Y == this.Y);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as CMY);
        }

        public string ToInputString ()
        {
            return ToString();
        }

        public string FormatDescription ()
        {
            return "";
        }

        public CMYK ToCMYK()
        {
            CMYK NewCMYK = new CMYK();
            double k = 1.0;
            if (Cyan < k)
                k = Cyan;
            if (Magenta < k)
                k = Magenta;
            if (Yellow < k)
                k = Yellow;
            if (k==1.0)
            {
                NewCMYK.C = 0.0;
                NewCMYK.M = 0.0;
                NewCMYK.Y = 0.0;
                NewCMYK.K = k;
            }
            else
            {
                NewCMYK.C = (Cyan - k) / (1.0 - k);
                NewCMYK.M = (Magenta - k) / (1.0 - k);
                NewCMYK.Y = (Yellow - k) / (1.0 - k);
                NewCMYK.K = k;
            }
            return NewCMYK;
        }

        public RGB ToRGB ()
        {
            RGB NewRGB = new RGB();
            NewRGB.scA = 1.0;
            NewRGB.scR = 1.0 - Cyan;
            NewRGB.scG = 1.0 - Magenta;
            NewRGB.scB = 1.0 - Yellow;
            return NewRGB;
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
                return "CMY";
            }
        }

        double _Y = 0.0;
        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _Y = value;
            }
        }

        public double Yellow
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }

        double _M = 0.0;
        public double M
        {
            get
            {
                return _M;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _M = value;
            }
        }

        public double Magenta
        {
            get
            {
                return M;
            }
            set
            {
                M = value;
            }
        }

        double _C = 0.0;
        public double C
        {
            get
            {
                return _C;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _C = value;
            }
        }

        public double Cyan
        {
            get
            {
                return C;
            }
            set
            {
                C = value;
            }
        }

        public string NiceCMY
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(C.ToString("n2"));
            sb.Append(", ");
            sb.Append(M.ToString("n2"));
            sb.Append(", ");
            sb.Append(Y.ToString("n2"));
            return sb.ToString();
        }
#if false
        public CMYK ToCMYK ()
        {
            double tK = 1.0;
            if (C < tK)
                tK = C;
            if (M < tK)
                tK = M;
            if (Y < tK)
                tK = Y;
            if (tK == 0.0)
            {
                return new CMYK(0.0, 0.0, 0.0, 0.0);
            }
            double Cp = (C - tK) / (1.0 - tK);
            double Mp = (M - tK) / (1.0 - tK);
            double Yp = (Y - tK) / (1.0 - tK);
            double Kp = tK;
            return new CMYK(Cp, Mp, Yp, Kp);
        }
#endif

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            CMY LocalCMY = new CMY();
            bool OK = TryParse(Raw, out LocalCMY);
            if (OK)
                Final = LocalCMY.ToRGBColor();
            return OK;
        }

        public bool TryParse (string Raw, out CMY Final)
        {
            Final = null;
            double LC, LM, LY;
            if (!IroColorSpace.TryParse3Double(Raw, out LC, out LM, out LY))
                return false;
            Final = new CMY(LC, LM, LY);
            return true;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            CMY LocalCMY = new CMY();
            bool OK = TryParse(Raw, out LocalCMY);
            if (OK)
            {
                this.C = LocalCMY.C;
                this.M = LocalCMY.M;
                this.Y = LocalCMY.Y;
            }
            return OK;
        }
    }
}
