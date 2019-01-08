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
    /// Background transactions.
    /// </summary>
    public class BackgroundTransaction : RenderTransaction, IEnumerable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundTransaction ()
            : base(TransactionTypes.DrawBackground)
        {
        }

        /// <summary>
        /// Constructor. Allows for creation of initial background.
        /// </summary>
        /// <param name="BGColor">Background color.</param>
        /// <param name="DrawGrid">Draw a grid flag.</param>
        /// <param name="GridColor">Grid color.</param>
        /// <param name="GridCellWidth">Grid horizontal repeat size.</param>
        /// <param name="GridCellHeight">Grid vertical repeat size.</param>
        public BackgroundTransaction (Color BGColor, bool DrawGrid, Color GridColor, int GridCellWidth, int GridCellHeight)
            : base(TransactionTypes.DrawBackground)
        {
            AddInstruction(BGColor, DrawGrid, GridColor, GridCellWidth, GridCellHeight);
        }

        /// <summary>
        /// Add a background instruction.
        /// </summary>
        /// <param name="Instruction">Definition of the background to draw.</param>
        public void AddInstruction (BackgroundDefinition Instruction)
        {
            Instructions.Add(Instruction);
        }

        /// <summary>
        /// Add a background based on the passed parameters.
        /// </summary>
        /// <param name="BGColor">Background color.</param>
        /// <param name="DrawGrid">Draw a grid flag.</param>
        /// <param name="GridColor">Grid color.</param>
        /// <param name="GridCellWidth">Grid horizontal repeat size.</param>
        /// <param name="GridCellHeight">Grid vertical repeat size.</param>
        public void AddInstruction (Color BGColor, bool DrawGrid, Color GridColor, int GridCellWidth, int GridCellHeight)
        {
            BackgroundDefinition Instruction = new BackgroundDefinition
            {
                BGColor = BGColor,
                DrawGrid = DrawGrid,
                GridColor = GridColor,
                GridCellWidth = GridCellWidth,
                GridCellHeight = GridCellHeight
            };
            AddInstruction(Instruction);
        }

        /// <summary>
        /// Add a background instruction based on the passed parameter.
        /// </summary>
        /// <param name="BGColor">Background color.</param>
        public void AddInstruction (Color BGColor)
        {
            AddInstruction(BGColor, false, Colors.Transparent, int.MaxValue, int.MaxValue);
        }

        /// <summary>
        /// Enumerate through background instructions.
        /// </summary>
        /// <returns>Background instruction.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (object Instruction in Instructions)
                yield return Instruction as BackgroundDefinition;
        }
    }

    /// <summary>
    /// Defines a background.
    /// </summary>
    public class BackgroundDefinition
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundDefinition ()
        {
            BGColor = Colors.White;
            DrawGrid = false;
            GridColor = Colors.Black;
            GridCellWidth = 32;
            GridCellHeight = 32;
        }

        /// <summary>
        /// Color of the background.
        /// </summary>
        public Color BGColor { get; set; }

        /// <summary>
        /// Draw grid flag.
        /// </summary>
        public bool DrawGrid { get; set; }

        /// <summary>
        /// Grid color.
        /// </summary>
        public Color GridColor { get; set; }

        /// <summary>
        /// Horizontal grid frequency.
        /// </summary>
        public int GridCellWidth { get; set; }

        /// <summary>
        /// Vertical grid frequency.
        /// </summary>
        public int GridCellHeight { get; set; }
    }
}
