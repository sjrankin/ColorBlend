using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    public class MeanderNewVectorEventArgs : EventArgs
    {
        public MeanderNewVectorEventArgs ()
            : base()
        {
            StartVector = new PointEx(0, 0);
            EndVector = new PointEx(0, 0);
        }

        public MeanderNewVectorEventArgs (PointEx StartVector, PointEx EndVector)
            : base()
        {
            this.StartVector = StartVector;
            this.EndVector = EndVector;
        }

        public PointEx StartVector { get; internal set; }
        public PointEx EndVector { get; internal set; }
    }
}
