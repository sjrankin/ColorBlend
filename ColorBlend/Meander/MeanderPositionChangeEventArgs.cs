using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Event data used when updating a color point's position.
    /// </summary>
    public class MeanderPositionChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MeanderPositionChangeEventArgs ()
            : base()
        {
            PointText = "";
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="PointText">Status text.</param>
        public MeanderPositionChangeEventArgs (string PointText)
            : base()
        {
            this.PointText = PointText;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="X">X coordinate.</param>
        /// <param name="Y">Y coordinate.</param>
        /// <param name="PointIndex">Point being moved.</param>
        /// <param name="PointText">Status text.</param>
        public MeanderPositionChangeEventArgs (int X, int Y, int PointIndex, string PointText)
            : base()
        {
            this.X = X;
            this.Y = Y;
            this.PointIndex = PointIndex;
            this.PointText = PointText;
        }

        /// <summary>
        /// Status text.
        /// </summary>
        public string PointText { get; internal set; }

        /// <summary>
        /// The index of the point being moved.
        /// </summary>
        public int PointIndex { get; internal set; }

        /// <summary>
        /// New X coordinate.
        /// </summary>
        public int X { get; internal set; }

        /// <summary>
        /// New Y coordinate.
        /// </summary>
        public int Y { get; internal set; }
    }
}
