using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ColorBlend.Compositing
{
    public class LineItem :DisplayItemBase
    {
        public LineItem () : base(ItemTypes.Line)
        {
            LineColor = new ColorEx();
            StartPoint = new PointEx(0, 0);
            EndPoint = new PointEx(0, 0);
        }

        public ColorEx LineColor { get; internal set; }

        public PointEx StartPoint { get; internal set; }

        public PointEx EndPoint { get; internal set; }
    }
}
