using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend.Compositing
{
    public class DisplayItem
    {
        public DisplayItem ()
        {
            ID = Guid.NewGuid();
            Description = "empty";
            Item = null;
        }

        public Guid ID { get; set; }

        public string Description { get; set; }

        public DisplayItemBase Item { get; set; }
    }

    public enum ItemTypes
    {
        Unknown,
        Rectangle,
        Ellipse,
        Buffer,
        Line,
        Generated
    }
}
