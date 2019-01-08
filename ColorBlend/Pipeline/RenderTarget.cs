using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorBlend
{
    /// <summary>
    /// Describes the buffer where things are rendered.
    /// </summary>
    public class RenderTarget
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RenderTarget ()
        {
            Bits = null;
            Width = 0;
            Height = 0;
            Stride = 0;
            PixelSize = 0;
            DefaultColor = Colors.Transparent;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Bits">The target's bits.</param>
        /// <param name="Width">Width of the target in pixels.</param>
        /// <param name="Height">Height of the target in scan lines.</param>
        /// <param name="Stride">Stride of the target.</param>
        /// <param name="DefaultColor">Default color.</param>
        /// <param name="PixelSize">Width of one pixel.</param>
        public RenderTarget (byte[] Bits, int Width, int Height, int Stride, Color DefaultColor, int PixelSize = 4)
        {
            this.Bits = Bits;
            this.Width = Width;
            this.Height = Height;
            this.Stride = Stride;
            this.PixelSize = PixelSize;
            this.DefaultColor = DefaultColor;
        }

        /// <summary>
        /// The target's bits.
        /// </summary>
        public byte[] Bits { get; set; }

        /// <summary>
        /// Width of the target in pixels.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the target in scan lines.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Stride of the target.
        /// </summary>
        public int Stride { get; set; }

        /// <summary>
        /// Width of one pixel.
        /// </summary>
        public int PixelSize { get; set; }

        /// <summary>
        /// The default background color (color used unless something else indicates otherwise).
        /// </summary>
        public Color DefaultColor { get; set; }
    }
}
