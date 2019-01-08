using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Event data for motion state changes.
    /// </summary>
    public class MeanderMotionStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor. Sets <seealso cref="State"/> to NotMoving.
        /// </summary>
        public MeanderMotionStateChangedEventArgs ()
            : base()
        {
            this.State = MotionEventStates.NotMoving;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="State">The state value.</param>
        public MeanderMotionStateChangedEventArgs (MotionEventStates State)
            : base()
        {
            this.State = State;
        }

        /// <summary>
        /// Get or set the motion state value.
        /// </summary>
        public MotionEventStates State { get; internal set; }
    }
}
