using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MapNodeItem;

namespace NodeMap
{
    public partial class NodeMap
    {
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<MapNode>), typeof(NodeMap),
                new UIPropertyMetadata(new List<MapNode>(), new PropertyChangedCallback(HandleItemsChange)));

        public static void HandleItemsChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            NodeMap NMap = Sender as NodeMap;
            if (NMap == null)
                return;
            NMap.Items = (List<MapNode>)e.NewValue;
            NMap.UpdateUI();
        }

        public List<MapNode> Items
        {
            get
            {
                return (List<MapNode>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public static readonly DependencyProperty InputConnectorColorProperty =
            DependencyProperty.Register("InputConnectorColor", typeof(Color), typeof(NodeMap),
                new UIPropertyMetadata(Colors.Black, new PropertyChangedCallback(HandleInputConnectColorChange)));

        public static void HandleInputConnectColorChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            NodeMap NMap = Sender as NodeMap;
            if (NMap == null)
                return;
            NMap.InputConnectColor = (Color)e.NewValue;
            NMap.UpdateUI();
        }

        public Color InputConnectColor
        {
            get
            {
                return (Color)GetValue(InputConnectorColorProperty);
            }
            set
            {
                SetValue(InputConnectorColorProperty, value);
            }
        }

        public static readonly DependencyProperty InputConnectorThicknessProperty =
            DependencyProperty.Register("InputConnectorThickness", typeof(double), typeof(NodeMap),
                new UIPropertyMetadata(2.0, new PropertyChangedCallback(HandleInputConnectorThicknessChange)));

        public static void HandleInputConnectorThicknessChange(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            NodeMap NMap = Sender as NodeMap;
            if (NMap == null)
                return;
            NMap.InputConnectorThickness = (double)e.NewValue;
            NMap.UpdateUI();
        }

        public double InputConnectorThickness
        {
            get
            {
                return (double)GetValue(InputConnectorThicknessProperty);
            }
            set
            {
                SetValue(InputConnectorThicknessProperty, value);
            }
        }

        public static readonly DependencyProperty OutputConnectorColorProperty =
           DependencyProperty.Register("OutputConnectorColor", typeof(Color), typeof(NodeMap),
            new UIPropertyMetadata(Colors.Blue, new PropertyChangedCallback(HandleOutputConnectColorChange)));

        public static void HandleOutputConnectColorChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            NodeMap NMap = Sender as NodeMap;
            if (NMap == null)
                return;
            NMap.OutputConnectColor = (Color)e.NewValue;
            NMap.UpdateUI();
        }

        public Color OutputConnectColor
        {
            get
            {
                return (Color)GetValue(OutputConnectorColorProperty);
            }
            set
            {
                SetValue(OutputConnectorColorProperty, value);
            }
        }

        public static readonly DependencyProperty OutputConnectorThicknessProperty =
          DependencyProperty.Register("OutputConnectorThickness", typeof(double), typeof(NodeMap),
           new UIPropertyMetadata(2.0, new PropertyChangedCallback(HandleOutputConnectorThicknessChange)));

        public static void HandleOutputConnectorThicknessChange (DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            NodeMap NMap = Sender as NodeMap;
            if (NMap == null)
                return;
            NMap.OutputConnectorThickness = (double)e.NewValue;
            NMap.UpdateUI();
        }

        public double OutputConnectorThickness
        {
            get
            {
                return (double)GetValue(OutputConnectorThicknessProperty);
            }
            set
            {
                SetValue(OutputConnectorThicknessProperty, value);
            }
        }
    }
}
