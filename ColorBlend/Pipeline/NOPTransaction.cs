using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// NOP transactions.
    /// </summary>
    public class NOPTransaction : RenderTransaction
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NOPTransaction ()
            : base(TransactionTypes.NOP)
        {
            RenderCount = 0;
        }
    }
}
