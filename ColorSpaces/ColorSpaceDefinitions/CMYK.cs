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
    [DebuggerDisplay("{NiceCMYK}")]
    public class CMYK : IEquatable<CMYK>, IColorSpace
    {
        public CMYK ()
        {
            C = 0.0;
            M = 0.0;
            Y = 0.0;
            K = 0.0;
        }

        public CMYK (double C, double M, double Y, double K)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            this.K = K;
        }

        private void CommonInitialization (double C, double M, double Y, double K)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            this.K = K;
            CanConvertTo = new HashSet<ColorSpaces>();
            CanConvertTo.Add(ColorSpaces.RGB);
            CanConvertTo.Add(ColorSpaces.CMY);
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

        public bool Equals (CMYK Other)
        {
            if (Other == null)
                return false;
            return (Other.C == this.C && Other.M == this.M && Other.Y == this.Y && Other.K == this.K);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as CMYK);
        }

        public string ToInputString ()
        {
            return ToString();
        }

        public string FormatDescription ()
        {
            return "";
        }

        public CMY ToCMY ()
        {
            CMY NewCMY = new CMY();
            NewCMY.Cyan = (Cyan * (1.0 - K)) + K;
            NewCMY.Magenta = (Magenta * (1.0 - K)) + K;
            NewCMY.Yellow = (Yellow * (1.0 - K)) + K;
            return NewCMY;
        }

        public RGB ToRGB ()
        {
            return ToCMY().ToRGB();
        }

        public Color ToRGBColor ()
        {
            return ToRGB().ToRGBColor();
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
                return "CMYK";
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

        double _K = 0.0;
        public double K
        {
            get
            {
                return _K;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _K = value;
            }
        }

        public double Black
        {
            get
            {
                return K;
            }
            set
            {
                K = value;
            }
        }

        public double Key
        {
            get
            {
                return K;
            }
            set
            {
                K = value;
            }
        }

        public string NiceCMYK
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
            sb.Append(", ");
            sb.Append(K.ToString("n2"));
            return sb.ToString();
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            CMYK LocalCMYK = new CMYK();
            bool OK = TryParse(Raw, out LocalCMYK);
            if (OK)
                Final = LocalCMYK.ToRGBColor();
            return false;
        }

        public bool TryParse (string Raw, out CMYK Final)
        {
            Final = null;
            double LC, LM, LY, LK;
            if (!IroColorSpace.TryParse4Double(Raw, out LC, out LM, out LY, out LK))
                return false;
            Final = new CMYK(LC, LM, LY, LK);
            return true;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            CMYK LocalCMYK = new CMYK();
            bool OK = TryParse(Raw, out LocalCMYK);
            if (OK)
            {
                this.C = LocalCMYK.C;
                this.M = LocalCMYK.M;
                this.Y = LocalCMYK.Y;
                this.K = LocalCMYK.K;
            }
            return OK;
        }
    }
}
