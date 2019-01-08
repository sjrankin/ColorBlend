using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace Iro3.Data.ColorSpaces
{
    [DebuggerDisplay("{NiceYUV}")]
    public class YUV : IEquatable<YUV>, IColorSpace
    {
        public YUV ()
        {
            Y = 0.0;
            U = 0.0;
            V = 0.0;
        }

        public YUV (double Y, double U, double V)
        {
            this.Y = Y;
            this.U = U;
            this.V = V;
        }

        private void CommonInitialization (double Y, double U, double V)
        {
            this.Y = Y;
            this.U = U;
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
            v.Add(Y);
            v.Add(U);
            v.Add(V);
            return v;
        }

        public bool Equals (YUV Other)
        {
            if (Other == null)
                return false;
            return (Other.Y == this.Y && Other.U == this.U && Other.V == this.V);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as YUV);
        }

        public string ToInputString ()
        {
            return "";
        }

        public string FormatDescription ()
        {
            StringBuilder sb = new StringBuilder("YUV");
            sb.Append(Environment.NewLine);
            sb.Append("Doubles: y u v (separators can be spaces or commas)");
            sb.Append(Environment.NewLine);
            sb.Append("Example: 0.5,0.2,0.3");
            return sb.ToString();
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
                return "YUV";
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
                _Y = value;
            }
        }

        double _U = 0.0;
        public double U
        {
            get
            {
                return _U;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _U = value;
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

        public string NiceYUV
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Y.ToString("n2"));
            sb.Append(", ");
            sb.Append(U.ToString("n2"));
            sb.Append(", ");
            sb.Append(V.ToString("n2"));
            return sb.ToString();
        }

        public bool TryParse (string Raw, out double Y, out double U, out double V)
        {
            Y = 0.0;
            U = 0.0;
            V = 0.0;

            return IroColorSpace.TryParse3Double(Raw, out Y, out U, out V);
        }

        public bool TryParse (string Raw, out YUV Final)
        {
            Final = null;
            double LY, LU, LV;
            if (!IroColorSpace.TryParse3Double(Raw, out LY, out LU, out LV))
                return false;
            Final = new YUV(LY, LU, LV);

            return true;
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            YUV LocalYUV = new YUV();
            bool OK = TryParse(Raw, out LocalYUV);
            if (OK)
                Final = LocalYUV.ToRGBColor();
            return OK;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            YUV LocalYUV = new YUV();
            bool OK = TryParse(Raw, out LocalYUV);
            if (OK)
            {
                this.Y = LocalYUV.Y;
                this.U = LocalYUV.U;
                this.V = LocalYUV.V;
            }
            return OK;
        }
    }
}
