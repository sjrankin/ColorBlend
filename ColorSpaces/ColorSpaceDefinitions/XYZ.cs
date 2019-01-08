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
    //http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
    public class XYZ : IEquatable<XYZ>, IColorSpace
    {
        public XYZ ()
        {
            CommonInitialization(0.0, 0.0, 0.0);
        }

        public XYZ (double X, double Y, double Z)
        {
            CommonInitialization(X, Y, Z);
        }

        private void CommonInitialization (double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            CanConvertTo = new HashSet<ColorSpaces>();
            CanConvertTo.Add(ColorSpaces.CIELab);
            CanConvertTo.Add(ColorSpaces.RGB);
        }

        public HashSet<ColorSpaces> CanConvertTo { get; internal set; }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public ColorSpaces ColorSpace { get; internal set; }

        public List<double> GetValues ()
        {
            List<double> v = new List<double>();
            v.Add(X);
            v.Add(Y);
            v.Add(Z);
            return v;
        }

        public bool Equals (XYZ Other)
        {
            if (Other == null)
                return false;
            return (Other.X == this.X && Other.Y == this.Y && Other.Z == this.Z);
        }

        public bool IsSame (IColorSpace Other)
        {
            if (Other == null)
                return false;
            if (Other.ColorSpace != this.ColorSpace)
                return false;
            return Equals(Other as XYZ);
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
            double R = (X * 3.240479) + (Y * -1.537150) + (Z * -0.498535);
            double G = (X * -0.969256) + (Y * 1.875992) + (Z * 0.041556);
            double B = (X * 0.055648) + (Y * -0.204043) + (Z * 1.057311);
            return new RGB(R, G, B);
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
                return "XYZ";
            }
        }

        double _X = 0.0;
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _X = value;
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

        double _Z = 0.0;
        public double Z
        {
            get
            {
                return _Z;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 1.0)
                    value = 1.0;
                _Z = value;
            }
        }

        public CIELab ToCIELab ()
        {
            double Xp = X / RefX;
            double Yp = Y / RefY;
            double Zp = Z / RefZ;

            if (Xp < 0.008856)
                Xp = Math.Pow(Xp, (double)(1 / 3));
            else
                Xp = (7.787 * Xp) + (16 / 116);
            if (Yp < 0.008856)
                Yp = Math.Pow(Yp, (double)(1 / 3));
            else
                Yp = (7.787 * Yp) + (16 / 116);
            if (Zp < 0.008856)
                Zp = Math.Pow(Zp, (double)(1 / 3));
            else
                Zp = (7.787 * Zp) + (16 / 116);
            double L = (116 * Yp) - 16;
            double A = 500 * (Xp - Yp);
            double B = 500 * (Yp - Zp);
            return new CIELab(L, A, B);
        }

        const double RefX = 95.047;
        const double RefY = 100.00;
        const double RefZ = 100.00;

        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            XYZ LocalXYZ = new XYZ();
            bool OK = TryParse(Raw, out LocalXYZ);
            if (OK)
                Final = LocalXYZ.ToRGBColor();
            return false;
        }

        public bool TryParse (string Raw, out XYZ Final)
        {
            Final = null;
            double LX, LY, LZ;
            if (!IroColorSpace.TryParse3Double(Raw, out LX, out LY, out LZ))
                return false;
            Final = new XYZ(LX, LY, LZ);
            return false;
        }

        public bool ParseSet (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            XYZ LocalXYZ = new XYZ();
            bool OK = TryParse(Raw, out LocalXYZ);
            if (OK)
            {
                this.X = LocalXYZ.X;
                this.Y = LocalXYZ.Y;
                this.Z = LocalXYZ.Z;
            }
            return OK;
        }
    }
}
