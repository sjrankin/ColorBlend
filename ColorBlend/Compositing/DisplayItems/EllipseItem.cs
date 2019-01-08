using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ColorBlend.Compositing
{
    public class EllipseItem : DisplayItemBase
    {
        public EllipseItem () : base(ItemTypes.Ellipse)
        {
            EllipseColor = new ColorEx();
            Radius = 0.0;
            CenterAlpha = 0.0;
            EdgeAlpha = 0.0;
        }

        public ColorEx EllipseColor { get; internal set; }

        public double Radius { get; set; }

        public double CenterAlpha { get; set; }

        public double EdgeAlpha { get; set; }
    }
}
