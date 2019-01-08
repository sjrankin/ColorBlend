using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iro3.Controls.ColorInput
{
    public static class Extensions
    {
        public static double Clamp(this double Raw, double Low, double High)
        {
            if (High < Low)
                return Raw;
            if (Raw < Low)
                return Low;
            if (Raw > High)
                return High;
            return Raw;
        }

        public static bool CloseTo(this double Value1, double Value2, double Percent)
        {
            Percent = Percent.Clamp(0.0, 1.0);
            double Biggest = Math.Max(Value1, Value2);
            double Range = Biggest * Percent;
            if ((Value2 >= (Biggest - Range)) && (Value2 <= (Biggest + Range)))
                return true;
            return false;
        }
    }
}
