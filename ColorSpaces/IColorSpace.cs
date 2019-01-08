using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Iro3.Data.ColorSpaces
{
    public interface IColorSpace
    {
        bool IsSame (IColorSpace Other);

        ColorSpaces ColorSpace { get;  }

        Guid ID { get; set; }

        string Name { get; set; }

        string ColorLabel { get;  }

        HashSet<ColorSpaces> CanConvertTo { get; }

        string ToInputString ();

        bool TryParse (string Raw, out Color Final);

        bool ParseSet (string Raw);

        List<double> GetValues ();

        string FormatDescription ();

        Color ToRGBColor ();

        RGB ToRGB ();
    }
}
