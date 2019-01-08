using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Markup;

namespace Iro3.Controls.ColorInput
{
    public partial class SimpleColor
    {
        /// <summary>
        /// Attempt to parse <paramref name="Raw"/> as either a hex color value or a standard color name. If the string begins with
        /// the '#' character then this method treats the string as a numeric value. Otherwise, the string is treated as a color name.
        /// </summary>
        /// <param name="Raw">The string to convert.</param>
        /// <param name="Final">The final, converted color.</param>
        /// <returns>True if <paramref name="Raw"/> was successfully parsed, false if not.</returns>
        public bool TryParse (string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            if (string.IsNullOrEmpty(Raw))
                return false;

            Raw = Raw.Trim(new char[] { ' ' });
            if (string.IsNullOrEmpty(Raw))
                return false;
            //Change "0x" (or "0X") to "#"
            if (Raw.Length >= 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    Raw = Raw.Substring(2);
                    Raw = "#" + Raw;
                }
            }
            if (Raw[0] != '#')
            {
                string RawName = Raw.ToUpper();
                RawName = RawName.Replace(" ", "");
                if (UStandardColors.ContainsKey(RawName))
                {
                    Final = UStandardColors[RawName];
                    return true;
                }
                return false;
            }
            Raw = Raw.Substring(1);

            string A = "ff";
            string R = "";
            string G = "";
            string B = "";
            if (Raw.Length == 6)
            {
                R = Raw.Substring(0, 2);
                G = Raw.Substring(2, 2);
                B = Raw.Substring(4, 2);
            }
            else
                if (Raw.Length == 8)
            {
                A = Raw.Substring(0, 2);
                R = Raw.Substring(2, 2);
                G = Raw.Substring(4, 2);
                B = Raw.Substring(6, 2);
            }
            else
                return false;

            byte av = Convert.ToByte(A, 16);
            byte rv = Convert.ToByte(R, 16);
            byte gv = Convert.ToByte(G, 16);
            byte bv = Convert.ToByte(B, 16);

            Final = Color.FromArgb(av, rv, gv, bv);

            return true;
        }

        private bool TryHSLParse(string Raw, out Color Final)
        {
            Final = Colors.Transparent;
            return false;
        }
    }
}
