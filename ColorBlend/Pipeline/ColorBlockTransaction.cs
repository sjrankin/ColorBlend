using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ColorBlend
{
    /// <summary>
    /// Color block transactions.
    /// </summary>
    public class ColorBlockTransaction : RenderTransaction, IEnumerable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorBlockTransaction ()
            : base(TransactionTypes.DrawBlock)
        {
        }

        /// <summary>
        /// Constructor. Allows for the creation of the initial color block.
        /// </summary>
        /// <param name="Left">Left side of the block.</param>
        /// <param name="Top">Top side of the block.</param>
        /// <param name="Width">Width of the block.</param>
        /// <param name="Height">Height of the block.</param>
        /// <param name="BlockColor">Color of the block.</param>
        public ColorBlockTransaction (int Left, int Top, int Width, int Height, Color BlockColor)
            : base(TransactionTypes.DrawBlock)
        {
            AddInstruction(Left, Top, Width, Height, BlockColor);
        }

        /// <summary>
        /// Add a color block instruction/definition.
        /// </summary>
        /// <param name="Definition">Definition of a color block.</param>
        public void AddInstruction (ColorBlockDefinition Definition)
        {
            if (Definition == null)
                return;
            Instructions.Add(Definition);
        }

        /// <summary>
        /// Create and add a color definition block.
        /// </summary>
        /// <param name="Left">Left side of the block.</param>
        /// <param name="Top">Top side of the block.</param>
        /// <param name="Width">Width of the block.</param>
        /// <param name="Height">Height of the block.</param>
        /// <param name="BlockColor">Color of the block.</param>
        public void AddInstruction (int Left, int Top, int Width, int Height, Color BlockColor)
        {
            ColorBlockDefinition Definition = new ColorBlockDefinition
            {
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                BlockColor = BlockColor
            };
            AddInstruction(Definition);
        }

        /// <summary>
        /// Enumerate through the color block instructions.
        /// </summary>
        /// <returns>A color block instruction.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (object Instruction in Instructions)
                yield return Instruction as ColorBlockDefinition;
        }

        public List<ColorBlenderInterface.ColorBlock> ColorBlockList ()
        {
            List<ColorBlenderInterface.ColorBlock> ColorBlocks = new List<ColorBlenderInterface.ColorBlock>();
            foreach (ColorBlockDefinition CBD in Instructions)
            {
                ColorBlenderInterface.ColorBlock NewBlock = new ColorBlenderInterface.ColorBlock
                {
                    Left = CBD.Left,
                    Top = CBD.Top,
                    Width = CBD.Width,
                    Height = CBD.Height,
                    A = CBD.BlockColor.A,
                    R = CBD.BlockColor.R,
                    G = CBD.BlockColor.G,
                    B = CBD.BlockColor.B
                };
                ColorBlocks.Add(NewBlock);
            }
            return ColorBlocks;
        }
    }

    /// <summary>
    /// Defines a color block.
    /// </summary>
    public class ColorBlockDefinition
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorBlockDefinition ()
        {
            Left = 0;
            Top = 0;
            Width = 0;
            Height = 0;
            BlockColor = Colors.Transparent;
        }

        /// <summary>
        /// Left side of the block.
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Top side of the block.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Width of the block.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the block.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Color of the block.
        /// </summary>
        public Color BlockColor { get; set; }

        /// <summary>
        /// Return a fixable structure.
        /// </summary>
        /// <returns>Fixable structure with the contents of this class.</returns>
        public ColorBlockStructure ToFixable ()
        {
            ColorBlockStructure CBS = new ColorBlockStructure
            {
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                BlockColor = (UInt32)((BlockColor.B << 24) + (BlockColor.G << 16) + (BlockColor.R << 8) + (BlockColor.A))
            };
            return CBS;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ColorBlockStructure
    {
        public Int32 Left;
        public Int32 Top;
        public Int32 Width;
        public Int32 Height;
        public UInt32 BlockColor;
    }
}
