using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Defines an action state communicated from the base to the child class.
    /// </summary>
    public class StageAction
    {
        /// <summary>
        /// Constructor. Sets action state to NOP.
        /// </summary>
        public StageAction ()
        {
            State = StageActions.NOP;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="State">Action state value.</param>
        public StageAction(StageActions State)
        {
            this.State = State;
        }

        /// <summary>
        /// Get or set the state of the action.
        /// </summary>
        public StageActions State { get; set; }
    }

    /// <summary>
    /// States communicated by the base class to the child class.
    /// </summary>
    public enum StageActions
    {
        /// <summary>
        /// No operation/action.
        /// </summary>
        NOP,
        /// <summary>
        /// Enable state changed.
        /// </summary>
        EnableChange,
        /// <summary>
        /// Image size changed.
        /// </summary>
        SizeChange,
        /// <summary>
        /// Refresh request.
        /// </summary>
        Refresh,
        /// <summary>
        /// Sink state changed.
        /// </summary>
        SinkChange,
        /// <summary>
        /// Input changed.
        /// </summary>
        InputChanged,
    }
}
