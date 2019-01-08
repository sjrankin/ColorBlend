using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Buttons
{
    /// <summary>
    /// Interaction logic for DropDownButton.xaml
    /// </summary>
    public partial class DropDownButton : UserControl
    {
        public DropDownButton ()
        {
            InitializeComponent();
            DropState = false;
            ButtonLabel = "Drop Button";
            ItemsContainer = new Popup();
            ItemList = new ListBox();
            ItemsContainer.Child = ItemList;
            ItemList.Width = double.NaN;
            ItemList.Height = double.NaN;
            ItemList.SelectionChanged += DropDownItemSelectionChanged;
            Items = new ObservableCollection<object>();
            Items.CollectionChanged += Items_CollectionChanged;
        }

        void DropDownItemSelectionChanged (object Sender, SelectionChangedEventArgs e)
        {
            ListBox LB = Sender as ListBox;
            if (LB == null)
                return;
            if (DropDownItemSelected != null)
            {
                DropDownItemSelectionEventArgs args = new DropDownItemSelectionEventArgs(LB.SelectedIndex, LB.SelectedItem);
                DropDownItemSelected(this, args);
                if (args.KeepOpen)
                    return;
                ClosePopup();
            }
        }

        void Items_CollectionChanged (object Sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<object> Objects = Sender as ObservableCollection<object>;
            if (Objects == null)
                return;
            UpdateItemDisplay(Objects);
        }

        private void UpdateItemDisplay (ObservableCollection<object> Objects)
        {
            ItemList.Items.Clear();
            foreach (object Something in Objects)
                ItemList.Items.Add(Something);
        }

        private void ClosePopup()
        {
            ItemsContainer.IsOpen = false;
            ItemsContainer.StaysOpen = false;
            DropState = false;
            DropButton.Content = "▼";
        }

        public double ButtonPartWidth
        {
            get
            {
                return ButtonPart.ActualWidth;
            }
            set
            {
                ButtonPart.Width = value;
            }
        }

        public double ButtonPartHeight
        {
            get
            {
                return ButtonPart.ActualHeight;
            }
            set
            {
                PartsGrid.Height = value;
                ItemsContainer.Height = DropDownHeightMultiplier * value;
            }
        }

        private double _DropDownHeightMultiplier = 5.0;
        public double DropDownHeightMultiplier
        {
            get
            {
                return _DropDownHeightMultiplier;
            }
            set
            {
                _DropDownHeightMultiplier = value;
                ItemsContainer.Height = this.Height * value;
            }
        }

        public object ButtonLabel
        {
            get
            {
                return ButtonPart.Content;
            }
            set
            {
                ButtonPart.Content = value;
            }
        }

        private void ButtonPartClicked (object Sender, RoutedEventArgs e)
        {
            if (ButtonClicked != null)
                ButtonClicked(this, e);
        }

        public delegate void HandleButtonPartClick (object Sender, RoutedEventArgs e);
        public event HandleButtonPartClick ButtonClicked;

        private void DropControlButtonClicked (object Sender, RoutedEventArgs e)
        {
            DropState = !DropState;
            DropButton.Content = DropState ? "▲" : "▼";
            if (DropButtonClicked != null)
                DropButtonClicked(this, new DropButtonClickEventArgs(DropState));
            if (DropState)
            {
                ItemsContainer.StaysOpen = true;
                ItemsContainer.IsOpen = true;
            }
            else
            {
                ClosePopup();
            }
        }

        private Popup ItemsContainer;
        private ListBox ItemList;

        public bool DropState { get; private set; }

        public delegate void HandleDropButtonClick (object Sender, DropButtonClickEventArgs e);
        public event HandleDropButtonClick DropButtonClicked;

        public ObservableCollection<object> Items { get; internal set; }

        private void ControlSizeChanged (object Sender, SizeChangedEventArgs e)
        {
            ItemsContainer.Width = this.ActualWidth;
            ItemsContainer.Height = DropDownHeightMultiplier * this.ActualHeight;
        }

        public delegate void HandleDropDownItemSelectedEvent (object Sender, DropDownItemSelectionEventArgs e);
        public event HandleDropDownItemSelectedEvent DropDownItemSelected;
    }

    public class DropButtonClickEventArgs : EventArgs
    {
        public DropButtonClickEventArgs ()
            : base()
        {
            this.DroppedDown = false;
        }

        public DropButtonClickEventArgs (bool DroppedDown)
            : base()
        {
            this.DroppedDown = DroppedDown;
        }

        public bool DroppedDown { get; internal set; }
    }

    public class DropDownItemSelectionEventArgs : EventArgs
    {
        public DropDownItemSelectionEventArgs ()
            : base()
        {
            SelectedObject = null;
            SelectedItemIndex = -1;
            KeepOpen = false;
        }

        public DropDownItemSelectionEventArgs (int SelectedItemIndex, object SelectedObject)
        {
            this.SelectedItemIndex = SelectedItemIndex;
            this.SelectedObject = SelectedObject;
            KeepOpen = false;
        }

        public object SelectedObject { get; internal set; }

        public int SelectedItemIndex { get; internal set; }

        public bool KeepOpen { get; set; }
    }
}
