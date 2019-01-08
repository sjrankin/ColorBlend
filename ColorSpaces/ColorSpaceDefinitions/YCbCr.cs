using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    public class YCbCr : IEquatable<YCbCr>, IColorSpace
    {
        public YCbCr ()
        {
            CommonInitialization(0.0, 0.0, 0.0);
        }

        public YCbCr (double Y, double Cb, double Cr)
        {
            CommonInitialization(Y, Cb, Cr);
        }

        private void CommonInitialization (double Y, double Cb, double Cr)
        {
            this.Y = Y;
            this.Cb = Cb;
            this.Cr = Cr;
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
            v.Add(Cb);
            v.Add(Cr);
            return v;
        }

        public bool Equals (YCbCr Other)
        {
            if (Other == null)
                return false;
            return (Other.Y == this.Y && Other.Cb == this.Cb && Other.Cr == this.Cr);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as YCbCr);
        }

        public string ToInputString ()
        {
            return "";
        }

        public string FormatDescription ()
        {
            StringBuilder sb = new StringBuilder("YCbCr");
            sb.Append(Environment.NewLine);
            sb.Append("Doubles: y cb cr (separators can be spaces or commas)");
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
                return "YCbCr";
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

        double _Cb = 0.0;
        public double Cb
        {
            get
            {
                return _Cb;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _Cb = value;
            }
        }

        double _Cr = 0.0;
        public double Cr
        {
            get
            {
                return _Cr;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _Cr = value;
            }
        }

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            YCbCr LocalYCbCr = new YCbCr();
            bool OK = TryParse(Raw, out LocalYCbCr);
            if (OK)
                Final = LocalYCbCr.ToRGBColor();
            return OK;
        }

        public bool TryParse (string Raw, out YCbCr Final)
        {
            Final = new YCbCr();
            double LY, LCb, LCr;
            if (!IroColorSpace.TryParse3Double(Raw, out LY, out LCb, out LCr))
                return false;
            Final = new YCbCr(LY,LCb,LCr);
            return true;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            YCbCr LocalYCbCr = new YCbCr();
            bool OK = TryParse(Raw, out LocalYCbCr);
            if (OK)
            {
                this.Y = LocalYCbCr.Y;
                this.Cb = LocalYCbCr.Cb;
                this.Cr = LocalYCbCr.Cr;
            }
            return OK;
        }
    }
}
