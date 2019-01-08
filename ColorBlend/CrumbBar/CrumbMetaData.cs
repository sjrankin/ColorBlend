using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend.CrumbBar
{
    public class CrumbMetaData
    {
        public CrumbMetaData ()
        {
            CrumbVisualization = null;
            CrumbToolTip = "";
            CrumbTag = null;
            IsEnabled = true;
        }

        public CrumbMetaData (object CrumbVisualization, string CrumbToolTip, object CrumbTag, bool IsEnabled)
        {
            this.CrumbVisualization = CrumbVisualization;
            this.CrumbToolTip = CrumbToolTip;
            this.CrumbTag = CrumbTag;
            this.IsEnabled = IsEnabled;
        }

        public object CrumbVisualization { get; set; }

        public string CrumbToolTip { get; set; }

        public object CrumbTag { get; set; }

        public bool IsEnabled { get; set; }
    }
}
