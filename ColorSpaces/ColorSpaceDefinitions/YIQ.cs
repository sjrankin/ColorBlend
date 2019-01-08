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
    [DebuggerDisplay("{NiceYIQ}")]
    public class YIQ : IEquatable<YIQ>, IColorSpace
    {
        public YIQ ()
        {
            CommonInitialization(0.0, 0.0, 0.0);
        }

        public YIQ (double Y, double I, double Q)
        {
            CommonInitialization(Y, I, Q);
        }

        private void CommonInitialization (double Y, double I, double Q)
        {
            this.Y = Y;
            this.I = I;
            this.Q = Q;
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
            v.Add(I);
            v.Add(Q);
            return v;
        }

        public bool Equals (YIQ Other)
        {
            if (Other == null)
                return false;
            return (Other.Y == this.Y && Other.I == this.I && Other.Q == this.Q);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as YIQ);
        }

        public string ToInputString ()
        {
            return "";
        }

        public string FormatDescription ()
        {
            StringBuilder sb = new StringBuilder("YIQ");
            sb.Append(Environment.NewLine);
            sb.Append("Doubles: y i q (separators can be spaces or commas)");
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
                return "YIQ";
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

        double _I = 0.0;
        public double I
        {
            get
            {
                return _I;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _I = value;
            }
        }

        double _Q = 0.0;
        public double Q
        {
            get
            {
                return _Q;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _Q = value;
            }
        }

        public string NiceYIQ
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
            sb.Append(I.ToString("n2"));
            sb.Append(", ");
            sb.Append(Q.ToString("n2"));
            return sb.ToString();
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            YIQ LocalYIQ = new YIQ();
            bool OK = TryParse(Raw, out LocalYIQ);
            if (OK)
                Final = LocalYIQ.ToRGBColor();
            return OK;
        }

        public bool TryParse (string Raw, out YIQ Final)
        {
            Final = null;
            return false;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            YIQ LocalYIQ = new YIQ();
            bool OK = TryParse(Raw, out LocalYIQ);
            if (OK)
            {
                this.Y = LocalYIQ.Y;
                this.I = LocalYIQ.I;
                this.Q = LocalYIQ.Q;
            }
            return OK;
        }
    }
}
