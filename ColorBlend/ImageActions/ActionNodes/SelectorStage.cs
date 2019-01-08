using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    public class SelectorStage : StageBase
    {
        public SelectorStage () : base("Selector", "Selects an image from the set of input images.", ImageActions.Selector)
        {
            base.ChangeDelegate = StateChange;
            Input.NoMaximumLimit = true;
        }

        public void StateChange (StageAction State)
        {

        }
    }
}
