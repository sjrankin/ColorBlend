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
using System.Diagnostics;

namespace ColorBlend
{
    /// <summary>
    /// Color blob transactions.
    /// </summary>
    public class ColorBlobTransaction : RenderTransaction, IEnumerable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorBlobTransaction ()
            : base(TransactionTypes.DrawColorBlob)
        {
        }

        /// <summary>
        /// Constructor. Allows creation of initial color blob.
        /// </summary>
        /// <param name="ColorBlob">The color blob bits.</param>
        /// <param name="UpperLeft">Upper-left point of the color blob.</param>
        public ColorBlobTransaction (byte[] ColorBlob, PointEx UpperLeft)
            : base(TransactionTypes.DrawColorBlob)
        {
            AddInstruction(ColorBlob, UpperLeft);
        }

        /// <summary>
        /// Add a color blob insruction.
        /// </summary>
        /// <param name="Instruction">Definition of the color blob.</param>
        public void AddInstruction (ColorBlockDefinition Instruction)
        {
            Instructions.Add(Instruction);
        }

        /// <summary>
        /// Add a color blob defined by the parameters.
        /// </summary>
        /// <param name="ColorBlob">The color blob bits.</param>
        /// <param name="UpperLeft">Upper-left point of the color blob.</param>
        public void AddInstruction (byte[] ColorBlob, PointEx UpperLeft)
        {
            ColorBlobDefinition Instruction = new ColorBlobDefinition
            {
                ColorBlob = ColorBlob,
                UpperLeft = UpperLeft
            };
            AddInstruction(Instruction);
        }

        /// <summary>
        /// Enumerate through the color blob instructions.
        /// </summary>
        /// <returns>Color blob instruction.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (object Instruction in Instructions)
                yield return Instruction as ColorBlobDefinition;
        }
    }

    /// <summary>
    /// Defines a color blob.
    /// </summary>
    public class ColorBlobDefinition
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorBlobDefinition ()
        {
            ColorBlob = null;
            UpperLeft = new PointEx(0, 0);
        }

        /// <summary>
        /// Color blob bits.
        /// </summary>
        public byte[] ColorBlob { get; set; }

        /// <summary>
        /// Upper-left point of the color blob.
        /// </summary>
        public PointEx UpperLeft { get; internal set; }
    }
}
