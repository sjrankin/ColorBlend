using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ColorBlend.Compositing
{
    public class BufferItem : DisplayItemBase
    {
        public BufferItem () : base(ItemTypes.Buffer)
        {
            BufferColor = new ColorEx();
            ImagePointer = IntPtr.Zero;
            BufferData = null;
            UseBufferData = true;
            DrawGrid = false;
            HorizontalGridLineColor = new ColorEx();
            VerticalGridLineColor = new ColorEx();
        }

        public ColorEx BufferColor { get; internal set; }

        public IntPtr ImagePointer { get; set; }

        public byte[] BufferData { get; set; }

        public bool UseBufferData { get; set; }

        public bool DrawGrid { get; set; }

        public ColorEx HorizontalGridLineColor { get; internal set; }

        public ColorEx VerticalGridLineColor { get; internal set; }
    }
}
