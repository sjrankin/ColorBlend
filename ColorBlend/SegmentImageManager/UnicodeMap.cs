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
using System.Collections;
using System.IO;

namespace SegmentImageManager
{
    public enum GeneralCategories
    {
        Unknown,
        //Normative categories.
        Lu,
        Ll,
        Lt,
        Mn,
        Mc,
        Me,
        Nd,
        Nl,
        No,
        Zs,
        Zl,
        Zp,
        Cc,
        Cf,
        Cs,
        Co,
        Cn,
        //Informative categories.
        Lm,
        Lo,
        Pc,
        Pd,
        Ps,
        Pe,
        Pi,
        Pf,
        Po,
        Sm,
        Sc,
        Sk,
        So
    }

    public enum BidirectionalCategorys
    {
        Unknown,
        L,
        LRE,
        LRO,
        R,
        AL,
        RLE,
        RLO,
        PDF,
        EN,
        ES,
        ET,
        AN,
        CS,
        NSM,
        BN,
        B,
        S,
        WS,
        ON
    }

    public enum DecompositionMappings
    {
        Unknown,
        Font,
        NoBreak,
        Initial,
        Medial,
        Final,
        Isolated,
        Circle,
        Super,
        Sub,
        Vertical,
        Wide,
        Narrow,
        Small,
        Square,
        Fraction,
        Compat
    }

    public class UnicodeEntry
    {
        public uint Code { get; internal set; }                                             //0
        public string Name { get; internal set; }                                           //1
        public GeneralCategories GeneralCategory { get; internal set; }                     //2
        public int CanonicalCombiningClass { get; internal set; }                           //3
        public BidirectionalCategorys BidirectionalCategory { get; internal set; }          //4
        public DecompositionMappings DecompositionMapping { get; internal set; }            //5
        public int DecimalDigit { get; internal set; }                                      //6
        public string Digit { get; internal set; }                                          //7
        public bool Mirrored { get; internal set; }                                         //8
        public string U1Name { get; internal set; }                                         //9
        public string Comment { get; internal set; }                                        //10
        public string UppercaseMapping { get; internal set; }                               //11
        public string LowercaseMapping { get; internal set; }                               //12
        public string TitlecaseMapping { get; internal set; }                               //13
    }

    public  class UnicodeMap 
    {
        public  UnicodeMap ()
        {
            _UnicodeMap = new List<UnicodeEntry>();
        }

        private   List<UnicodeEntry> _UnicodeMap;

        public  void GenerateMap (string RawMapFileName)
        {
            string Contents = "";
            string StreamName = "ColorBlend.SegmentImageManager." + RawMapFileName;
            using (Stream ResourceStream = this.GetType().Assembly.GetManifestResourceStream(StreamName))
            {
                using (StreamReader ResourceReader = new StreamReader(ResourceStream))
                {
                    string Line = ResourceReader.ReadLine();
                    string[] Parts = Line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    UnicodeEntry Entry = new UnicodeEntry();
                    Entry.Code = GetUInt(Parts[0]);
                    Entry.Name = Parts[1];
                    Entry.GeneralCategory = GetGeneralCategory(Parts[2]);
                    Entry.CanonicalCombiningClass = GetInt(Parts[3]);
                    Entry.BidirectionalCategory = GetBidirectionalCategory(Parts[4]);
                    Entry.DecompositionMapping = GetDecompositionalMapping(Parts[5]);
                    Entry.DecimalDigit = GetInt(Parts[6]);
                    Entry.Digit = Parts[7];
                    Entry.Mirrored = GetBool(Parts[8]);
                    Entry.U1Name = Parts[9];
                    Entry.Comment = Parts[10];
                    Entry.UppercaseMapping = Parts[11];
                    Entry.LowercaseMapping = Parts[12];
                    Entry.TitlecaseMapping = Parts[13];
                    _UnicodeMap.Add(Entry);
                }
            }

            if (string.IsNullOrEmpty(Contents))
                throw new ArgumentNullException("Contents");
        }

        private  GeneralCategories GetGeneralCategory (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                throw new ArgumentNullException("Raw");
            switch (Raw.ToUpper())
            {
                case "LU":
                    return GeneralCategories.Lu;

                case "LL":
                    return GeneralCategories.Ll;

                case "LT":
                    return GeneralCategories.Lt;

                case "MN":
                    return GeneralCategories.Mn;

                case "MC":
                    return GeneralCategories.Mc;

                case "ME":
                    return GeneralCategories.Me;

                case "ND":
                    return GeneralCategories.Nd;

                case "NL":
                    return GeneralCategories.Nl;

                case "NO":
                    return GeneralCategories.No;

                case "ZS":
                    return GeneralCategories.Zs;

                case "ZL":
                    return GeneralCategories.Zl;

                case "ZP":
                    return GeneralCategories.Zp;

                case "CC":
                    return GeneralCategories.Cc;

                case "CF":
                    return GeneralCategories.Cf;

                case "CS":
                    return GeneralCategories.Cs;

                case "CO":
                    return GeneralCategories.Co;

                case "CN":
                    return GeneralCategories.Cn;

                case "LM":
                    return GeneralCategories.Lm;

                case "LO":
                    return GeneralCategories.Lo;

                case "PC":
                    return GeneralCategories.Pc;

                case "PD":
                    return GeneralCategories.Pd;

                case "PS":
                    return GeneralCategories.Ps;

                case "PE":
                    return GeneralCategories.Pe;

                case "PI":
                    return GeneralCategories.Pi;

                case "PF":
                    return GeneralCategories.Pf;

                case "PO":
                    return GeneralCategories.Po;

                case "SM":
                    return GeneralCategories.Sm;

                case "SC":
                    return GeneralCategories.Sc;

                case "SK":
                    return GeneralCategories.Sk;

                case "SO":
                    return GeneralCategories.So;

                default:
                    break;
            }
            return GeneralCategories.Unknown;
        }

        private  BidirectionalCategorys GetBidirectionalCategory (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                throw new ArgumentNullException("Raw");
            switch (Raw.ToUpper())
            {
                case "L":
                    return BidirectionalCategorys.L;

                case "LRE":
                    return BidirectionalCategorys.LRE;

                case "LRO":
                    return BidirectionalCategorys.LRO;

                case "R":
                    return BidirectionalCategorys.R;

                case "AL":
                    return BidirectionalCategorys.AL;

                case "RLE":
                    return BidirectionalCategorys.RLE;

                case "RLO":
                    return BidirectionalCategorys.RLO;

                case "PDF":
                    return BidirectionalCategorys.PDF;

                case "EN":
                    return BidirectionalCategorys.EN;

                case "ES":
                    return BidirectionalCategorys.ES;

                case "ET":
                    return BidirectionalCategorys.ET;

                case "AN":
                    return BidirectionalCategorys.AN;

                case "CS":
                    return BidirectionalCategorys.CS;

                case "NSM":
                    return BidirectionalCategorys.NSM;

                case "BN":
                    return BidirectionalCategorys.BN;

                case "B":
                    return BidirectionalCategorys.B;

                case "S":
                    return BidirectionalCategorys.S;

                case "WS":
                    return BidirectionalCategorys.WS;

                case "ON":
                    return BidirectionalCategorys.ON;

                default:
                    break;
            }

            return BidirectionalCategorys.Unknown;
        }

        private  DecompositionMappings GetDecompositionalMapping (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                throw new ArgumentNullException("Raw");
            switch (Raw.ToUpper())
            {
                case "<FONT>":
                    return DecompositionMappings.Font;

                case "<NOBREAK>":
                    return DecompositionMappings.NoBreak;

                case "<INITIAL>":
                    return DecompositionMappings.Initial;

                case "<MEDIAL>":
                    return DecompositionMappings.Medial;

                case "<FINAL>":
                    return DecompositionMappings.Final;

                case "<ISOLATED>":
                    return DecompositionMappings.Isolated;

                case "<CIRCLE>":
                    return DecompositionMappings.Circle;

                case "<SUPER>":
                    return DecompositionMappings.Super;

                case "<VERTICAL>":
                    return DecompositionMappings.Vertical;

                case "<WIDE>":
                    return DecompositionMappings.Wide;

                case "<NARROW>":
                    return DecompositionMappings.Narrow;

                case "<SMALL>":
                    return DecompositionMappings.Small;

                case "<SQUARE>":
                    return DecompositionMappings.Square;

                case "<FRACTION>":
                    return DecompositionMappings.Fraction;

                case "<COMPAT>":
                    return DecompositionMappings.Compat;

                default:
                    break;
            }
            return DecompositionMappings.Unknown;
        }

        private  uint GetUInt (string Raw)
        {
            uint u = 0;
            if (!uint.TryParse(Raw, out u))
                return uint.MaxValue;
            return u;
        }

        private  int GetInt (string Raw)
        {
            int u = 0;
            if (!int.TryParse(Raw, out u))
                return int.MaxValue;
            return u;
        }

        private  bool GetBool (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                return false;
            if (Raw.ToUpper() == "Y")
                return true;
            return false;
        }
    }
}
