using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MapNodeItem
{
    public partial class MapNode
    {
        public static readonly DependencyProperty OutputCountProperty =
            DependencyProperty.Register("OutputCount", typeof(int), typeof(MapNode),
                new UIPropertyMetadata(1, new PropertyChangedCallback(HandleOutputCountChange)));

        public static void HandleOutputCountChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            MapNode MNode = Sender as MapNode;
            if (MNode == null)
                return;
            MNode.OutputCount = (int)e.NewValue;
            MNode.DrawUI();
        }

        public int OutputCount
        {
            get
            {
                return (int)GetValue(OutputCountProperty);
            }
            set
            {
                SetValue(OutputCountProperty, value);
            }
        }
    }
}
