using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    public class SplitterStage : StageBase
    {
        public SplitterStage () : base("Splitter", "Image splitter to send the same image to multiple actions.", ImageActions.Splitter)
        {
            base.ChangeDelegate = StateChange;
            Output.NoMaximumLimit = true;
        }

        public void StateChange (StageAction State)
        {

        }

        public void SplitCount(int Count)
        {
            Output.Clear();
            for (int i = 0; i < Count; i++)
                Output.Add(Input.First);
        }
    }
}
