using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Iro3.Data.ColorSpaces
{
public partial class IroColorSpace
    {
        public IroColorSpace()
        {
        }
    }

    public enum ColorSpaces
    {
        RGB,
        YUV,
        YIQ,
        YCbCr,
        HSV,
        HSL,
        CMY,
        CMYK,
        CIELab,
        XYZ,
        TSL
    }
}
