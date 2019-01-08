using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    public class NOPStage : StageBase
    {
        public NOPStage () : base("NOP", "No action - passive pass-through.", ImageActions.NOP)
        {
            base.ChangeDelegate = StateChange;
        }

        public void StateChange (StageAction State)
        {

        }
    }
}
