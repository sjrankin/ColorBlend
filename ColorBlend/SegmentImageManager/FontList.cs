using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Xml;
using System.Windows.Markup;

namespace SegmentImageManager
{
    //http://www.fileformat.info/info/unicode/category/index.htm
    public class FontList
    {
        public FontList(string FontName, double FontSize, FontWeight FontWeight)
        {
            Initialize(FontName, FontSize, FontWeight, null, null);
        }

        public FontList (string FontName, double FontSize, FontWeight FontWeight,
            int RangeLow, int RangeHigh)
        {
            Initialize(FontName, FontSize, FontWeight, RangeLow, RangeHigh);
        }

        private void Initialize(string FontName, double FontSize, FontWeight FontWeight,
            Nullable<int> RangeLow, Nullable<int> RangeHigh)
        {
            _CharList = new List<WriteableBitmap>();
            this.FontName = FontName;
            this.FontSize = FontSize;
            this.FontWeight = FontWeight;
            FirstCharacterIndex = RangeLow.HasValue ? RangeLow.Value : 0;
            LastCharacterIndex = RangeHigh.HasValue ? RangeHigh.Value : 65535;
        }

        public int FirstCharacterIndex { get; private set; }

        public int LastCharacterIndex { get; private set; }

        public string FontName { get; private set; }

        public double FontSize { get; private set; }

        public FontWeight FontWeight { get; private set; }

        private List<WriteableBitmap> _CharList = null;
        public List<WriteableBitmap> CharacterList
        {
            get
            {
                return _CharList;
            }
        }

        public bool Generate ()
        {
            FontFamily Family = new FontFamily(FontName);
            for(int i=FirstCharacterIndex;i<=LastCharacterIndex;i++)
            {

            }
            return false;
        }

        //http://stackoverflow.com/questions/5604855/how-to-determine-which-fonts-contain-a-specific-character
        private static void PrintFamiliesSupportingChar (char characterToCheck)
        {
            int count = 0;
            ICollection<FontFamily> fontFamilies = Fonts.GetFontFamilies(@"C:\Windows\Fonts\");
            ushort glyphIndex;
            int unicodeValue = Convert.ToUInt16(characterToCheck);
            GlyphTypeface glyph;
            string familyName;

            foreach (FontFamily family in fontFamilies)
            {
                var typefaces = family.GetTypefaces();
                foreach (Typeface typeface in typefaces)
                {
                    typeface.TryGetGlyphTypeface(out glyph);
                    if (glyph != null && glyph.CharacterToGlyphMap.TryGetValue(unicodeValue, out glyphIndex))
                    {
                        family.FamilyNames.TryGetValue(XmlLanguage.GetLanguage("en-us"), out familyName);
                        Console.WriteLine(familyName + " Supports ");
                        count++;
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Total {0} fonts support {1}", count, characterToCheck);
        }
    }
}
