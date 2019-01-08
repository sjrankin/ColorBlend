using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ColorPointItem.xaml
    /// </summary>
    public partial class ColorPointItem : UserControl
    {
        public ColorPointItem ()
        {
            InitializeComponent();
            ItemID = Guid.Empty;
        }

        public Guid ItemID { get; set; }

        public bool ItemNameNormal
        {
            get
            {
                return PointNameBlock.FontStyle == FontStyles.Italic ? false : true;
            }
            set
            {
                PointNameBlock.FontStyle = value ? FontStyles.Normal : FontStyles.Italic;
                PointNameBlock.Foreground = value ? Brushes.Black : Brushes.DimGray;
            }
        }
    }
}
