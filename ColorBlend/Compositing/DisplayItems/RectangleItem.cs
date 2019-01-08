using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ColorBlend.Compositing
{
    public class RectangleItem : DisplayItemBase
    {
        public RectangleItem () : base(ItemTypes.Rectangle)
        {
            FillColor = new ColorEx();
            UseGradients = false;
            GradientColors = new List<ColorEx>();
        }

        public ColorEx FillColor { get; set; }

        public bool UseGradients { get; set; }

        public List<ColorEx> GradientColors { get; internal set; }
    }
}
