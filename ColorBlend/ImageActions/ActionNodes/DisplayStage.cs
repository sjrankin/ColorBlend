using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace ColorBlend
{
    public class DisplayStage : StageBase
    {
        public delegate void ImageDisplay (WriteableBitmap ImageToDisplay);

        public DisplayStage (ImageDisplay DisplayFunction) : base("Display", "Displays the node's input.", ImageActions.Display)
        {
            base.ChangeDelegate = StateChange;
            base.UserVisible = false;
            this.DisplayFunction = DisplayFunction;
        }

        public void SetImageDisplay(ImageDisplay DisplayFunction)
        {
            this.DisplayFunction = DisplayFunction;
        }

        private ImageDisplay DisplayFunction;

        public void StateChange (StageAction State)
        {
            switch(State.State)
            {
                case StageActions.Refresh:
                    if (DisplayFunction != null)
                        DisplayFunction(Input.First);
                    break;
            }
        }
    }
}
