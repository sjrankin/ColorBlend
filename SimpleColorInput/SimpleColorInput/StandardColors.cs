using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Iro3.Controls.ColorInput
{
    public partial  class SimpleColor
    {
        /// <summary>
        /// Given a color value, return its name, if it exists. Otherwise return its ToString() value.
        /// </summary>
        /// <param name="ColorValue">Value of the color whose name (if it exists) will be returned.</param>
        /// <returns>Name of the color if it exists, value of the color if it doesn't.</returns>
        internal string GetColorName(Color ColorValue)
        {
            foreach(KeyValuePair<string,Color> KVP in StandardColors)
            {
                if (KVP.Value == ColorValue)
                    return KVP.Key;
            }
            return ToString();
        }

        /// <summary>
        /// Dictionary of standard colors based on <seealso cref="StandardColors"/> but with all color names normalized.
        /// </summary>
        internal static Dictionary<string, Color> UStandardColors;
        /// <summary>
        /// Dicationary of standard colors.
        /// </summary>
        internal Dictionary<string, Color> StandardColors = new Dictionary<string, Color>()
        {
            {"AliceBlue", Colors.AliceBlue},
{"AntiqueWhite", Colors.AntiqueWhite},
{"Aqua", Colors.Aqua},
{"Aquamarine", Colors.Aquamarine},
{"Azure", Colors.Azure},
{"Beige", Colors.Beige},
{"Bisque", Colors.Bisque},
{"Black", Colors.Black},
{"BlanchedAlmond",Colors.BlanchedAlmond},
{"Blue", Colors.Blue},
{"BlueViolet", Colors.BlueViolet},
{"Brown", Colors.Brown},
{"BurlyWood", Colors.BurlyWood},
{"CadetBlue", Colors.CadetBlue},
{"Chartreuse", Colors.Chartreuse},
{"Chocolate", Colors.Chocolate},
{"Coral", Colors.Coral},
{"CornflowerBlue", Colors.CornflowerBlue},
{"Cornsilk", Colors.Cornsilk},
{"Crimson", Colors.Crimson},
{"Cyan", Colors.Cyan},
{"DarkBlue", Colors.DarkBlue},
{"DarkCyan", Colors.DarkCyan},
{"DarkGoldenrod", Colors.DarkGoldenrod},
{"DarkGray", Colors.DarkGray},
{"DarkGreen", Colors.DarkGreen},
{"DarkKhaki", Colors.DarkKhaki},
{"DarkMagenta", Colors.DarkMagenta},
{"DarkOliveGreen", Colors.DarkOliveGreen},
{"DarkOrange", Colors.DarkOrange},
{"DarkOrchid", Colors.DarkOrchid},
{"DarkRed", Colors.DarkRed},
{"DarkSalmon", Colors.DarkSalmon},
{"DarkSeaGreen", Colors.DarkSeaGreen},
{"DarkSlateBlue", Colors.DarkSlateBlue},
{"DarkSlateGray", Colors.DarkSlateGray},
{"DarkTurquoise", Colors.DarkTurquoise},
{"DarkViolet", Colors.DarkViolet},
{"DeepPink",Colors.DeepPink},
{"DeepSkyBlue", Colors.DeepSkyBlue},
{"DimGray", Colors.DimGray},
{"DodgerBlue", Colors.DodgerBlue},
{"Firebrick", Colors.Firebrick},
{"FloralWhite", Colors.FloralWhite},
{"ForestGreen", Colors.ForestGreen},
{"Fuchsia", Colors.Fuchsia},
{"Gainsboro", Colors.Gainsboro},
{"GhostWhite", Colors.GhostWhite},
{"Gold", Colors.Gold},
{"Goldenrod", Colors.Goldenrod},
{"Gray", Colors.Gray},
{"Green", Colors.Green},
{"GreenYellow",Colors.GreenYellow},
{"Honeydew", Colors.Honeydew},
{"HotPink", Colors.HotPink},
{"IndianRed", Colors.IndianRed},
{"Indigo",Colors.Indigo},
{"Ivory", Colors.Ivory},
{"Khaki", Colors.Khaki},
{"Lavender", Colors.Lavender},
{"LavenderBlush", Colors.LavenderBlush},
{"LawnGreen", Colors.LawnGreen},
{"LemonChiffon", Colors.LemonChiffon},
{"LightBlue", Colors.LightBlue},
{"LightCoral", Colors.LightCoral},
{"LightCyan", Colors.LightCyan},
{"LightGoldenrodYellow", Colors.LightGoldenrodYellow},
{"LightGray", Colors.LightGray},
{"LightGreen", Colors.LightGreen},
{"LightPink", Colors.LightPink},
{"LightSalmon", Colors.LightSalmon},
{"LightSeaGreen", Colors.LightSeaGreen},
{"LightSkyBlue", Colors.LightSkyBlue},
{"LightSlateGray", Colors.LightSlateGray},
{"LightSteelBlue",Colors.LightSteelBlue},
{"LightYellow", Colors.LightYellow},
{"Lime", Colors.Lime},
{"LimeGreen", Colors.LimeGreen},
{"Linen", Colors.Linen},
{"Magenta", Colors.Magenta},
{"Maroon", Colors.Maroon},
{"MediumAquamarine", Colors.MediumAquamarine},
{"MediumBlue", Colors.MediumBlue},
{"MediumOrchid", Colors.MediumOrchid},
{"MediumPurple",Colors.MediumPurple},
{"MediumSeaGreen", Colors.MediumSeaGreen},
{"MediumSlateBlue", Colors.MediumSlateBlue},
{"MediumSpringGreen", Colors.MediumSpringGreen},
{"MediumTurquoise", Colors.MediumTurquoise},
{"MediumVioletRed", Colors.MediumVioletRed},
{"MidnightBlue", Colors.MidnightBlue},
{"MintCream",Colors.MintCream},
{"MistyRose", Colors.MistyRose},
{"Moccasin", Colors.Moccasin},
{"NavajoWhite", Colors.NavajoWhite},
{"Navy", Colors.Navy},
{"OldLace", Colors.OldLace},
{"Olive", Colors.Olive},
{"OliveDrab", Colors.OliveDrab},
{"Orange", Colors.Orange},
{"OrangeRed", Colors.OrangeRed},
{"Orchid", Colors.Orchid},
{"PaleGoldenrod", Colors.PaleGoldenrod},
{"PaleGreen",Colors.PaleGreen},
{"PaleTurquoise", Colors.PaleTurquoise},
{"PaleVioletRed", Colors.PaleVioletRed},
{"PapayaWhip", Colors.PapayaWhip},
{"PeachPuff", Colors.PeachPuff},
{"Peru", Colors.Peru},
{"Pink", Colors.Pink},
{"Plum", Colors.Plum},
{"PowderBlue", Colors.PowderBlue},
{"Purple", Colors.Purple},
{"Red", Colors.Red},
{"RosyBrown", Colors.RosyBrown},
{"RoyalBlue", Colors.RoyalBlue},
{"SaddleBrown", Colors.SaddleBrown},
{"Salmon", Colors.Salmon},
{"SandyBrown", Colors.SandyBrown},
{"SeaGreen", Colors.SeaGreen},
{"SeaShell", Colors.SeaShell},
{"Sienna", Colors.Sienna},
{"Silver", Colors.Silver},
{"SkyBlue", Colors.SkyBlue},
{"SlateBlue",Colors.SlateBlue},
{"SlateGray", Colors.SlateGray},
{"Snow", Colors.Snow},
{"SpringGreen", Colors.SpringGreen},
{"SteelBlue", Colors.SteelBlue},
{"Tan",Colors.Tan},
{"Teal", Colors.Teal},
{"Thistle", Colors.Thistle},
{"Tomato", Colors.Tomato},
{"Transparent", Colors.Transparent},
{"Turquoise", Colors.Turquoise},
{"Violet", Colors.Violet},
{"Wheat", Colors.Wheat},
{"White", Colors.White},
{"WhiteSmoke", Colors.WhiteSmoke},
{"Yellow",Colors.Yellow},
{"YellowGreen", Colors.YellowGreen},

            {"ActiveBorderColor", SystemColors.ActiveBorderColor },
            {"ActiveCaptionColor", SystemColors.ActiveCaptionColor },
            {"ActiveCaptionTextColor", SystemColors.ActiveCaptionTextColor },
            {"AppWorkspaceColor", SystemColors.AppWorkspaceColor },
            {"ControlColor", SystemColors.ControlColor },
            {"ControlDarkColor", SystemColors.ControlDarkColor },
            {"ControlDarkDarkColor", SystemColors.ControlDarkDarkColor },
            {"ControlLightColor", SystemColors.ControlLightColor },
            {"ControlTextColor", SystemColors.ControlTextColor },
            {"DesktopColor", SystemColors.DesktopColor },
            {"GradientActiveCaptionColor", SystemColors.GradientActiveCaptionColor },
            {"GradientInactiveCaptionColor", SystemColors.GradientInactiveCaptionColor },
            {"GrayTextColor", SystemColors.GrayTextColor },
            {"HighlightColor", SystemColors.HighlightColor },
            {"HighlightTextColor", SystemColors.HighlightTextColor },
            {"HotTrackColor", SystemColors.HotTrackColor },
            {"InactiveBorderColor", SystemColors.InactiveBorderColor },
            {"InactiveCaptionColor", SystemColors.InactiveCaptionColor },
            {"InactiveSelectionHighlightColor", SystemColors.InactiveSelectionHighlightBrush.Color },
            {"InfoColor", SystemColors.InfoColor },
            {"InfoTextColor", SystemColors.InfoTextColor },
            {"MenuBarColor", SystemColors.MenuBarColor },
            {"MenuColor", SystemColors.MenuColor },
            {"MenuHighlightColor", SystemColors.MenuHighlightColor },
            {"MenuTextColor", SystemColors.MenuTextColor },
            {"ScrollBarColor", SystemColors.ScrollBarColor },
            {"WindowColor", SystemColors.WindowColor },
            {"WindowFrameColor", SystemColors.WindowFrameColor },
            {"WindowTextColor", SystemColors.WindowTextColor }
        };
    }
}

