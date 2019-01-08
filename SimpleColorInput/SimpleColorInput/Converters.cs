using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iro3.Data.ColorSpaces;

namespace Iro3.Controls.ColorInput
{
    public static class Converters
    {
        public static List<ColorSpaces> KnownColorSpaces
        {
            get
            {
                List<ColorSpaces> SpaceList = new List<ColorSpaces>();
                SpaceList.Add(ColorSpaces.RGB);
                SpaceList.Add(ColorSpaces.HSL);
                SpaceList.Add(ColorSpaces.HSV);
                SpaceList.Add(ColorSpaces.CMY);
                SpaceList.Add(ColorSpaces.CMYK);
                SpaceList.Add(ColorSpaces.YUV);
                SpaceList.Add(ColorSpaces.YIQ);
                SpaceList.Add(ColorSpaces.YCbCr);
                SpaceList.Add(ColorSpaces.XYZ);
                SpaceList.Add(ColorSpaces.CIELab);
                SpaceList.Add(ColorSpaces.TSL);
                return SpaceList;
            }
        }
    }
}
