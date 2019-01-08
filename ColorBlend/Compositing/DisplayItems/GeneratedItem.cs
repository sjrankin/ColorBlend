using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ColorBlend.Compositing
{
    public class GeneratedItem :DisplayItemBase
    {
        public GeneratedItem (GeneratedTypes GeneratedType) : base(ItemTypes.Generated)
        {
            GeneratedColor = new ColorEx();
            HorizontalGridLineColor = new ColorEx();
            VerticalGridLineColor = new ColorEx();
            this.GeneratedType = GeneratedType;
        }

        public GeneratedTypes GeneratedType { get; private set; }

        public ColorEx GeneratedColor { get; internal set; }

        public ColorEx HorizontalGridLineColor { get; internal set; }

        public ColorEx VerticalGridLineColor { get;internal set; }
    }

    public enum GeneratedTypes
    {
        ColorBlock,
        Grid,
    }
}
