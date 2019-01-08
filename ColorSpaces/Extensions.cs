using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    public static class Extensions
    {
        /// <summary>
        /// Clamp a double to the specified range.
        /// </summary>
        /// <param name="Raw">The double to clamp.</param>
        /// <param name="Low">Low range.</param>
        /// <param name="High">High range.</param>
        /// <returns>Clamped value.</returns>
        public static double Clamp (this double Raw, double Low, double High)
        {
            if (High < Low)
                return Raw;
            if (Raw < Low)
                return Low;
            if (Raw > High)
                return High;
            return Raw;
        }

        /// <summary>
        /// Determine if <paramref name="Value1"/> is "close" to <paramref name="Value2"/>.
        /// </summary>
        /// <param name="Value1">First value.</param>
        /// <param name="Value2">Second value.</param>
        /// <param name="Percent">Determines closeness.</param>
        /// <returns>True if <paramref name="Value1"/> is close to <paramref name="Value2"/>, false if not.</returns>
        public static bool CloseTo (this double Value1, double Value2, double Percent)
        {
            Percent = Percent.Clamp(0.0, 1.0);
            double Biggest = Math.Max(Value1, Value2);
            double Range = Biggest * Percent;
            if ((Value2 >= (Biggest - Range)) && (Value2 <= (Biggest + Range)))
                return true;
            return false;
        }

        /// <summary>
        /// Convert a color space color to a WPF solid color brush.
        /// </summary>
        /// <param name="TheColorSpace">The color space color to convert.</param>
        /// <returns>WPF solid color brush.</returns>
        public static SolidColorBrush ToBrush (this IColorSpace TheColorSpace, bool ErrorFail = true)
        {
            if (TheColorSpace == null)
            {
                if (ErrorFail)
                    throw new ArgumentNullException("TheColorSpace");
                else
                    return Brushes.Transparent;
            }
            return new SolidColorBrush(TheColorSpace.ToRGBColor());
        }
    }
}
