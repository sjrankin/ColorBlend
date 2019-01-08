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
    /// Line render transactions.
    /// </summary>
    public class LineTransaction : RenderTransaction, IEnumerable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LineTransaction ()
            : base(TransactionTypes.DrawLine)
        {
        }

        /// <summary>
        /// Constructor. Allows creation of initial line.
        /// </summary>
        /// <param name="Point1">First point.</param>
        /// <param name="Point2">Second point.</param>
        /// <param name="LineColor">Color of the line.</param>
        /// <param name="LineThickness">Thickness of the line.</param>
        /// <param name="AntiAlias">Anti-alias flag.</param>
        public LineTransaction (PointEx Point1, PointEx Point2, Color LineColor, int LineThickness = 1, bool AntiAlias = false)
            : base(TransactionTypes.DrawLine)
        {
            AddInstruction(Point1, Point2, LineColor, LineThickness, AntiAlias);
        }

        /// <summary>
        /// Add a new line definition.
        /// </summary>
        /// <param name="Instruction">Line defintion.</param>
        public void AddInstruction (LineDefinition2 Instruction)
        {
            Instructions.Add(Instruction);
        }

        /// <summary>
        /// Add data for a new line.
        /// </summary>
        /// <param name="Point1">First point.</param>
        /// <param name="Point2">Second point.</param>
        /// <param name="LineColor">Color of the line.</param>
        /// <param name="LineThickness">Thickness of the line.</param>
        /// <param name="AntiAlias">Anti-alias flag.</param>
        public void AddInstruction (PointEx Point1, PointEx Point2, Color LineColor, int LineThickness = 1, bool AntiAlias = false)
        {
            LineDefinition2 Instruction = new LineDefinition2
            {
                FirstPoint = Point1,
                SecondPoint = Point2,
                LineColor = LineColor,
                LineThickness = LineThickness,
                AntiAlias = AntiAlias
            };
            AddInstruction(Instruction);
        }

        /// <summary>
        /// Enumerate through the line drawing instructions.
        /// </summary>
        /// <returns>A line definition.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (object Instruction in Instructions)
                yield return Instruction as LineDefinition2;
        }
    }

    /// <summary>
    /// Defines a line.
    /// </summary>
    public class LineDefinition2
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LineDefinition2 ()
        {
            FirstPoint = new PointEx(0, 0);
            SecondPoint = new PointEx(0, 0);
            LineColor = Colors.White;
            AntiAlias = false;
            LineThickness = 1;
        }

        /// <summary>
        /// First endpoint.
        /// </summary>
        public PointEx FirstPoint { get; internal set; }

        /// <summary>
        /// Second endpoint.
        /// </summary>
        public PointEx SecondPoint { get; internal set; }

        /// <summary>
        /// The color of the line.
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Anti-alias flag.
        /// </summary>
        public bool AntiAlias { get; set; }

        /// <summary>
        /// Line thickness.
        /// </summary>
        public int LineThickness { get; set; }

        /// <summary>
        /// Convert data held by the instance into a fixable structure.
        /// </summary>
        /// <returns>Fixable structure.</returns>
        public LineDefinitionStructure ToFixable ()
        {
            LineDefinitionStructure LDS = new LineDefinitionStructure
            {
                X1 = FirstPoint.IntX,
                Y1 = FirstPoint.IntY,
                X2 = SecondPoint.IntX,
                Y2 = SecondPoint.IntY,
                AntiAlias = AntiAlias,
                LineThickness = LineThickness,
                LineColor = LineColor.ToARGB()
            };
            return LDS;
        }
    }

    /// <summary>
    /// Defines a line.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LineDefinitionStructure
    {
        /// <summary>
        /// Horizontal point 1.
        /// </summary>
        public Int32 X1;
        /// <summary>
        /// Vertical point 1.
        /// </summary>
        public Int32 Y1;
        /// <summary>
        /// Horizontal point 2.
        /// </summary>
        public Int32 X2;
        /// <summary>
        /// Vertical point 2.
        /// </summary>
        public Int32 Y2;
        /// <summary>
        /// Line color.
        /// </summary>
        public UInt32 LineColor;
        /// <summary>
        /// Line thickness.
        /// </summary>
        public Int32 LineThickness;
        /// <summary>
        /// Anti-alias flag.
        /// </summary>
        public bool AntiAlias;
    }
}
