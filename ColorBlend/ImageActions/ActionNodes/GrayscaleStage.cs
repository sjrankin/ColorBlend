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
    class GrayscaleStage : StageBase
    {
        public GrayscaleStage () : base("Grayscale", "Grayscale conversion", ImageActions.Grayscale)
        {
            base.ChangeDelegate = StateChange;
            GrayscaleType = ColorBlenderInterface.GrayscaleTypes.Grayscale_Mean;
            Levels = 256;
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
            if (GrayscaleType == ColorBlenderInterface.GrayscaleTypes.Grayscale_GrayLevels)
                OK = CBI.ImageGrayscaleLevels(Input.First, DB, Input.First.PixelWidth, Input.First.PixelHeight, Input.First.BackBufferStride, Levels);
            else
                OK = CBI.ImageGrayscale(Input.First, DB, Input.First.PixelWidth, Input.First.PixelHeight, Input.First.BackBufferStride, (int)GrayscaleType);

            if (OK)
            {
                Output.Clear();
                Output.Add(DB);
            }
            NotifyCompleted(OK ? ColorBlenderInterface.ReturnCode.Success : ColorBlenderInterface.ReturnCode.Error);
            return OK;
        }

        public ColorBlenderInterface.GrayscaleTypes GrayscaleType { get; set; }

        public int Levels { get; set; }
    }
}
