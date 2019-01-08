using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorBlend
{
    class SolarizeStage : StageBase
    {
        public SolarizeStage () : base("Solarize", "Solarize conversion", ImageActions.Solarize)
        {
            base.ChangeDelegate = StateChange;
            Threshold = 0.5;
            InvertThreshold = false;
        }

        public void StateChange (StageAction State)
        {
            switch (State.State)
            {
                case StageActions.Refresh:
                case StageActions.SizeChange:
                case StageActions.InputChanged:
                    Execute();
                    break;

                case StageActions.EnableChange:
                    if (!IsEnabled)
                    {
                        //If not enabled, hook the output to the input.
                        Output.Clear();
                        if (Input.Count > 0)
                            Output.Add(Input.First);
                    }
                    else
                        Execute();
                    break;
            }
        }

        public new bool Execute ()
        {
            if (Input.Count < 1)
                return false;
            WriteableBitmap DB = new WriteableBitmap(Input.First.PixelWidth, Input.First.PixelHeight, Input.First.DpiX, Input.First.DpiY,
                PixelFormats.Bgra32, null);

            bool OK = false;
            OK = CBI.Solarize(Input.First, DB, Input.First.PixelWidth, Input.First.PixelHeight, Input.First.BackBufferStride, Threshold, InvertThreshold);

            if (OK)
            {
                Output.Clear();
                Output.Add(DB);
            }
            NotifyCompleted(OK ? ColorBlenderInterface.ReturnCode.Success : ColorBlenderInterface.ReturnCode.Error);
            return OK;
        }

        public double Threshold { get; set; }

        public bool InvertThreshold { get; set; }
    }
}
