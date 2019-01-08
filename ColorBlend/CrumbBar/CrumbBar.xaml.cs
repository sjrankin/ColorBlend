using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace ColorBlend.CrumbBar
{
    /// <summary>
    /// Interaction logic for CrumbBar.xaml
    /// </summary>
    public partial class CrumbBar : UserControl
    {
        public CrumbBar ()
        {
            InitializeComponent();
            BarContents = new ObservableCollection<Crumb>();
            BarContents.CollectionChanged += HandleBarContentsChanged;
        }

        private void HandleBarContentsChanged (object Sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private double _BarHeight = 30.0;
        public double BarHeight
        {
            get
            {
                return _BarHeight;
            }
            set
            {
                _BarHeight = value;
            }
        }

        private double _BarWidth = 100.0;
        public double BarWidth
        {
            get
            {
                return _BarWidth;
            }
            set
            {
                _BarWidth = value;
            }
        }

        private HorizontalAlignment _HorizontalAlignment = HorizontalAlignment.Stretch;
        public new HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return _HorizontalAlignment;
            }
            set
            {
                _HorizontalAlignment = value;
            }
        }

        private VerticalAlignment _VerticalAlignment = VerticalAlignment.Center;
        public new VerticalAlignment VerticalAlignment
        {
            get
            {
                return _VerticalAlignment;
            }
            set
            {
                _VerticalAlignment = value;
            }
        }

        public ObservableCollection<Crumb> BarContents { get; private set; }
    }
}
