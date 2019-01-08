using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace ColorBlend
{
    public static class ExMethods
    {
        /// <summary>
        /// Clamp the value <paramref name="Raw"/> to a range specified by <paramref name="Low"/> and <paramref name="High"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value being clamped. Must be comparable.</typeparam>
        /// <param name="Raw">The value to clamp.</param>
        /// <param name="Low">Low end of the clamp range, inclusive.</param>
        /// <param name="High">High end of the clamp range, inclusive.</param>
        /// <returns>Original value or clamped value if necessary.</returns>
        public static T Clamp<T>(this T Raw, T Low, T High) where T : IComparable
        {
            if (Raw.CompareTo(Low) < 0)
                return Low;
            if (Raw.CompareTo(High) > 0)
                return High;
            return Raw;
        }

        public static int IntValue(this TextBox TB, Nullable<int> DefaultValue = 0)
        {
            if (TB == null)
                throw new InvalidOperationException("TextBox must not be null.");
            string Raw = TB.Text;
            if (DefaultValue == null && string.IsNullOrEmpty(Raw))
                throw new InvalidOperationException("No value to convert.");
            if (string.IsNullOrEmpty(Raw))
                return DefaultValue.Value;
            if (int.TryParse(Raw, out int ival))
                return ival;
            if (DefaultValue != null)
                return DefaultValue.Value;
            throw new InvalidOperationException("Unable to parse value in TextBox.");
        }

        public static double DoubleValue(this TextBox TB, Nullable<double> DefaultValue = 0.0)
        {
            if (TB == null)
                throw new InvalidOperationException("TextBox must not be null.");
            string Raw = TB.Text;
            if (DefaultValue == null && string.IsNullOrEmpty(Raw))
                throw new InvalidOperationException("No value to convert.");
            if (string.IsNullOrEmpty(Raw))
                return DefaultValue.Value;
            if (double.TryParse(Raw, out double dval))
                return dval;
            if (DefaultValue != null)
                return DefaultValue.Value;
            throw new InvalidOperationException("Unable to parse value in TextBox.");
        }

        public static double DoubleValue(this TextBox TB, double DefaultValue,double ClampLow, double ClampHigh)
        {
            if (TB == null)
                throw new InvalidOperationException("TextBox must not be null.");
            string Raw = TB.Text;
            if (string.IsNullOrEmpty(Raw))
                return DefaultValue;
            if (!double.TryParse(Raw, out double dval))
                dval = DefaultValue;
            if (dval < ClampLow)
                dval = ClampLow;
            if (dval > ClampHigh)
                dval = ClampHigh;
            return dval;
        }

        public static int NextEven(this Random Rand)
        {
            return Rand.Next() & 0xfffe;
        }

        public static int NextEven(this Random Rand, int Low, int High)
        {
            return Rand.Next(Low, High) & 0xfffe;
        }

        public static int NextOdd(this Random Rand)
        {
            return Rand.Next() & 1;
        }

        public static int NextOdd(this Random Rand, int Low, int High)
        {
            return Rand.Next(Low, High) & 1;
        }

        public static int AtLeast(this Random Rand, int Minimum)
        {
            while (true)
            {
                int RandVal = Rand.Next();
                if (RandVal >= Minimum)
                    return RandVal;
            }
        }

        public static double NextNormal(this Random Rand)
        {
            return (double)Rand.Next() / (double)Int32.MaxValue;
        }

        public static List<int> AddList(this List<int> Source, List<int> Other)
        {
            if (Source.Count != Other.Count)
                throw new InvalidOperationException("Count mismatch.");
            List<int> Result = new List<int>();
            for (int i = 0; i < Source.Count; i++)
                Result.Add(Source[i] + Other[i]);
            return Result;
        }

        public static List<int> MeanList(this List<int> Source, int Count)
        {
            if (Count == 0)
                throw new DivideByZeroException("Count");
            if (Count == 1)
                return Source;
            List<int> Result = new List<int>();
            for (int i = 0; i < Source.Count; i++)
                Result.Add(Source[i] / Count);
            return Result;
        }

        public static void PixelSet(this byte[] PixelPlane, int Index, Color ColorValue)
        {
            if (Index >= PixelPlane.Length)
                return;
            PixelPlane[Index + 0] = ColorValue.B;
            PixelPlane[Index + 1] = ColorValue.G;
            PixelPlane[Index + 2] = ColorValue.R;
            PixelPlane[Index + 3] = ColorValue.A;
        }

        public static Color PixelGet(this byte[] PixelPlane, int Index)
        {
            if (Index >= PixelPlane.Length)
                return Colors.Transparent;
            byte b = PixelPlane[Index + 0];
            byte g = PixelPlane[Index + 1];
            byte r = PixelPlane[Index + 2];
            byte a = PixelPlane[Index + 3];
            return Color.FromArgb(a, r, g, b);
        }

        public static UInt32 ToBGRA(this Color Source)
        {
            UInt32 BGRA = (UInt32)(Source.B << 24);
            BGRA |= (UInt32)(Source.G << 16);
            BGRA |= (UInt32)(Source.R << 8);
            BGRA |= (UInt32)(Source.A);
            return BGRA;
        }

        public static UInt32 ToBGRA(this UInt32 FromARGB)
        {
            byte A = (byte)((FromARGB & 0xff000000) >> 24);
            byte R = (byte)((FromARGB & 0x00ff0000) >> 16);
            byte G = (byte)((FromARGB & 0x0000ff00) >> 8);
            byte B = (byte)((FromARGB & 0x000000ff) >> 0);
            UInt32 BGRA = (UInt32)((B << 24) + (G << 16) + (R << 8) + (A << 0));
            return BGRA;
        }

        public static UInt32 ToARGB(this Color Source)
        {
            UInt32 ARGB = (UInt32)(Source.A << 24);
            ARGB |= (UInt32)(Source.R << 16);
            ARGB |= (UInt32)(Source.G << 8);
            ARGB |= (UInt32)(Source.B);
            return ARGB;
        }

        public static UInt32 ToARGB(this UInt32 FromBGRA)
        {
            byte B = (byte)((FromBGRA & 0xff000000) >> 24);
            byte G = (byte)((FromBGRA & 0x00ff0000) >> 16);
            byte R = (byte)((FromBGRA & 0x0000ff00) >> 8);
            byte A = (byte)((FromBGRA & 0x000000ff) >> 0);
            UInt32 ARGB = (UInt32)((A << 24) + (R << 16) + (G << 8) + (B << 0));
            return ARGB;
        }

        public static Color FromBGRA(this UInt32 Source)
        {
            Color Final = Colors.Transparent;
            Final.A = (byte)(Source & 0x000000ff);
            Final.R = (byte)((Source & 0x0000ff00) >> 8);
            Final.G = (byte)((Source & 0x00ff0000) >> 16);
            Final.B = (byte)((Source & 0xff000000) >> 24);
            return Final;
        }

        public static Color FromARGB(this UInt32 Source)
        {
            Color Final = Colors.Transparent;
            Final.B = (byte)(Source & 0x000000ff);
            Final.G = (byte)((Source & 0x0000ff00) >> 8);
            Final.R = (byte)((Source & 0x00ff0000) >> 16);
            Final.A = (byte)((Source & 0xff000000) >> 24);
            return Final;
        }

        public static string ToHexColor(this Color Source, bool IncludeAlpha = true)
        {
            string Final = "#";
            if (IncludeAlpha)
                Final += Source.A.ToString("x2");
            Final += Source.R.ToString("x2");
            Final += Source.G.ToString("x2");
            Final += Source.B.ToString("x2");
            return Final;
        }

        public static Color Percentage(this Color Source, double Percent)
        {
            Percent = Math.Abs(Percent);
            if (Percent > 1.0)
                Percent = 1.0;
            Color NewColor = Color.FromRgb((byte)((double)Source.R * Percent),
                                           (byte)((double)Source.G * Percent),
                                           (byte)((double)Source.B * Percent));
            return NewColor;
        }

        public static bool PercentGreaterThan(this Random Rand, double Percent)
        {
            const int MaxRand = 100000;
            int R = Rand.Next(0, MaxRand);
            if (((double)MaxRand * Percent) > R)
                return true;
            return false;
        }

        public static double NormalizedNext(this Random Rand)
        {
            const int MaxRand = 100000;
            int R = Rand.Next(0, MaxRand);
            return (double)R / (double)MaxRand;
        }

        public static bool ContainsValidDouble(this string Value)
        {
            return double.TryParse(Value, out double NotUsed);
        }

        public static bool ContainsValidInteger(this string Value)
        {
            return int.TryParse(Value, out int NotUsed);
        }

        public static bool AllSameValue(this double Value, params double[] OtherValues)
        {
            if (OtherValues == null)
                return false;
            if (OtherValues.Length < 1)
                return false;
            foreach (double D in OtherValues)
                if (D != Value)
                    return false;
            return true;
        }
    }
}
