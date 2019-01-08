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
    /// Interaction logic for Crumb.xaml
    /// </summary>
    public partial class Crumb : UserControl
    {
        public Crumb ()
        {
            InitializeComponent();
            CrumbData = new CrumbMetaData();
            SubCrumbList = new ObservableCollection<CrumbMetaData>();
            SubCrumbList.CollectionChanged += HandleSubCrumbListChanged;
        }

        private void HandleSubCrumbListChanged (object Sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void HandleMouseEnterCrumb (object Sender, MouseEventArgs e)
        {

        }

        private void HandleMouseExitCrumb (object Sender, MouseEventArgs e)
        {

        }

        private void HandleLeftButtonUp (object Sender, MouseButtonEventArgs e)
        {

        }

        public CrumbMetaData CrumbData { get; private set; }

        public ObservableCollection<CrumbMetaData> SubCrumbList { get; private set; }
    }
}
