using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorBlend.Compositing
{
    public class DisplayItemBase
    {
        public DisplayItemBase(ItemTypes DisplayItemType)
        {
            ItemType = DisplayItemType;
            Width = 0.0;
            Height = 0.0;
            Location = new PointEx(0, 0);
            Marked = false;
            RemoveWhenExecuted = false;
            BaseColor = new ColorEx();
        }

        public ColorEx BaseColor { get; internal set; }

        public ItemTypes ItemType { get; private set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public PointEx Location { get; internal set; }

        internal bool Marked { get; set; }

        public bool RemoveWhenExecuted { get; set; }
    }
}
