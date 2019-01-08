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
using HistogramDisplay;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for QDPipelineNode.xaml
    /// </summary>
    public partial class QDPipelineNode : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public QDPipelineNode ()
        {
            InitializeComponent();
            NodeID = Guid.NewGuid();
            ImageContainer.Background = CheckerboardPatternBrush(8.0, 8.0, Brushes.LightGray, Brushes.DarkGray);
            ShowingHistogram = false;
        }

        /// <summary>
        /// Used for histogram display (if needed).
        /// </summary>
        private ColorBlenderInterface CBI = null;

        /// <summary>
        /// Get or set the ID of the node.
        /// </summary>
        public Guid NodeID { get; set; }

        /// <summary>
        /// Handle enable check change.
        /// </summary>
        /// <param name="Sender">The check box that changed.</param>
        /// <param name="e">Not used.</param>
        private void NodeCheckChanged (object Sender, RoutedEventArgs e)
        {
            CheckBox CB = Sender as CheckBox;
            if (CB == null)
                return;
            if (QDNodeCheckChanged != null)
            {
                QDNodeCheckChangedEventArgs args = new QDNodeCheckChangedEventArgs(CB.IsChecked.Value);
                QDNodeCheckChanged(this, args);
            }
        }

        /// <summary>
        /// Set the bitmap image.
        /// </summary>
        /// <param name="BitmapImage">The image output from the pipeline stage.</param>
        public void SetBitmap (WriteableBitmap BitmapImage)
        {
            SampleImage.Source = BitmapImage;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="NodeText">The text to display.</param>
        public void SetText (string NodeText)
        {
            NodeData.Text = NodeText;
            NodeData.ToolTip = NodeText;
        }

        /// <summary>
        /// Sets the tool tip.
        /// </summary>
        /// <param name="TipText">Text for the tool tip.</param>
        public void SetToolTipText (string TipText)
        {
            MainControl.ToolTip = TipText;
        }

        /// <summary>
        /// Sets the tool stip.
        /// </summary>
        /// <param name="TipObject">Object for the tool tip.</param>
        public void SetToolTipObject (object TipObject)
        {
            MainControl.ToolTip = TipObject;
        }

        /// <summary>
        /// Sets the enable check.
        /// </summary>
        /// <param name="IsEnabled">How to set the check.</param>
        public void SetEnableCheck (bool IsEnabled)
        {
            EnableCheck.IsChecked = IsEnabled;
        }

        /// <summary>
        /// Shows or hides the histogram display.
        /// </summary>
        /// <param name="Show">Determines if the histogram is displayed or hidden.</param>
        public void ShowHistogram(bool Show)
        {
            if (SampleImage.Source==null)
            {
                HDisplay.Opacity = 0.0;
                SampleImage.Opacity = 1.0;
                ShowingHistogram = false;
                return;
            }
            ShowingHistogram = Show;
            if (Show)
            {
                if (CBI == null)
                    CBI = new ColorBlenderInterface();
                WriteableBitmap WB = SampleImage.Source as WriteableBitmap;
                if (WB == null)
                {
                    HDisplay.Opacity = 0.0;
                    SampleImage.Opacity = 1.0;
                    return;
                }
                HDisplay.Opacity = 1.0;
                SampleImage.Opacity = 0.0;
                int ImageWidth = WB.PixelWidth;
                int ImageHeight = WB.PixelHeight;
                int ImageStride = WB.BackBufferStride;
                double[] RedPercent = new double[256];
                UInt32[] RawRed = new UInt32[256];
                uint RedSum = 0;
                double[] GreenPercent = new double[256];
                UInt32[] RawGreen = new UInt32[256];
                uint GreenSum = 0;
                double[] BluePercent = new double[256];
                UInt32[] RawBlue = new UInt32[256];
                uint BlueSum = 0;
                unsafe
                {
                    unsafe
                    {
                        CBI.MakeHistogram((byte*)WB.BackBuffer, ImageWidth, ImageHeight, ImageStride,
                          256,
                          ref RawRed, ref RedPercent, out RedSum,
                          ref RawGreen, ref GreenPercent, out GreenSum,
                          ref RawBlue, ref BluePercent, out BlueSum);
                    }
                }
                List<HistogramTriplet> Triplets = new List<HistogramTriplet>();
                for (int i = 0; i < 256; i++)
                    Triplets.Add(new HistogramTriplet(RedPercent[i], GreenPercent[i], BluePercent[i]));
                HDisplay.BatchAdd(Triplets);
            }
            else
            {
                HDisplay.Opacity = 0.0;
                SampleImage.Opacity = 1.0;
            }
        }

        /// <summary>
        /// If true, the histogram is being displayed.
        /// </summary>
        public bool ShowingHistogram { get; internal set; }

        /// <summary>
        /// Handle mouse enter events.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void MainControl_MouseEnter (object Sender, MouseEventArgs e)
        {
            MainControl.BorderBrush = Brushes.Gold;
        }

        /// <summary>
        /// Handle mouse left events.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void MainControl_MouseLeave (object Sender, MouseEventArgs e)
        {
            MainControl.BorderBrush = Brushes.DarkGray;
        }

        /// <summary>
        /// Handle context menu clicks.
        /// </summary>
        /// <param name="Sender">The menu item that was clicked.</param>
        /// <param name="e">Not used.</param>
        private void ContextMenuClick (object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            if (QDMenuCommandEvent == null)
                return;
            QDNodeMouseCommandEventArgs args = new QDNodeMouseCommandEventArgs();
            string Command = MI.Name as string;
            if (string.IsNullOrEmpty(Command))
                return;
            switch (Command.ToLower())
            {
                case "remove":
                    args.RemoveItem = true;
                    break;

                case "histogram":
                    MI.IsChecked = !MI.IsChecked;
                    ShowHistogram(MI.IsChecked);
                    break;

                default:
                    break;
            }
            QDMenuCommandEvent(this, args);
        }

        private bool _IsSelected = false;
        /// <summary>
        /// Get or set the selected state of the node.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (QDNodeSelectionChangedEvent != null)
                {
                    QDNodeSelectionChangedEventArgs args = new QDNodeSelectionChangedEventArgs(value);
                    QDNodeSelectionChangedEvent(this, args);
                    if (args.CancelSelection)
                        return;
                }
                _IsSelected = value;
                if (_IsSelected)
                    SelectNode();
                else
                    DeselectNode();
            }
        }

        /// <summary>
        /// Programmatically select the node.
        /// </summary>
        public void SelectNode()
        {
            MainControl.Background = Brushes.Yellow;
            _IsSelected = true;
        }

        /// <summary>
        /// Programmatically deselect the node.
        /// </summary>
        public void DeselectNode()
        {
            MainControl.Background = Brushes.WhiteSmoke;
            _IsSelected = false;
        }

        /// <summary>
        /// Handle mouse down events. Changes selection state.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void MainControl_MouseDown (object Sender, MouseButtonEventArgs e)
        {
            IsSelected = !IsSelected;
        }

        /// <summary>
        /// Return a checkerboard brush.
        /// </summary>
        /// <param name="Width">Overall width of the brush.</param>
        /// <param name="Height">Overall height of the brush.</param>
        /// <param name="Color0">First brush.</param>
        /// <param name="Color1">Second brush.</param>
        /// <returns>Drawing brush.</returns>
        public DrawingBrush CheckerboardPatternBrush (double Width, double Height,
            SolidColorBrush Color0, SolidColorBrush Color1)
        {
            double HalfWidth = Width / 2;
            double HalfHeight = Height / 2;
            DrawingBrush TheBrush = new DrawingBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Stretch = Stretch.None,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Viewport = new Rect(new Point(0, 0), new Point(Width, Height))
            };
            DrawingGroup DG = new DrawingGroup();
            TheBrush.Drawing = DG;

            GeometryDrawing Overall = new GeometryDrawing
            {
                Brush = Color0
            };
            RectangleGeometry RG0 = new RectangleGeometry(new Rect(0, 0, Width, Height));
            Overall.Geometry = RG0;
            DG.Children.Add(Overall);

            GeometryDrawing Other = new GeometryDrawing
            {
                Brush = Color1
            };
            RectangleGeometry RG1 = new RectangleGeometry(new Rect(0, 0, HalfWidth, HalfHeight));
            RectangleGeometry RG2 = new RectangleGeometry(new Rect(HalfWidth, HalfHeight, HalfWidth, HalfHeight));
            GeometryGroup GG = new GeometryGroup();
            GG.Children.Add(RG1);
            GG.Children.Add(RG2);
            Other.Geometry = GG;
            DG.Children.Add(Other);

            return TheBrush;
        }

        /// <summary>
        /// Event handler definition for check state change events.
        /// </summary>
        /// <param name="Sender">The node where the change occurred.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleQDNodeCheckChangedEvents (object Sender, QDNodeCheckChangedEventArgs e);
        /// <summary>
        /// Triggered when the node's check box state is changed.
        /// </summary>
        public event HandleQDNodeCheckChangedEvents QDNodeCheckChanged;

        /// <summary>
        /// Event handler definition for menu command events.
        /// </summary>
        /// <param name="Sender">The node where the event occurred.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleQDMenuCommandEvents (object Sender, QDNodeMouseCommandEventArgs e);
        /// <summary>
        /// Triggered when a menu command is encountered.
        /// </summary>
        public event HandleQDMenuCommandEvents QDMenuCommandEvent;

        /// <summary>
        /// Event handler definition for selection state change events.
        /// </summary>
        /// <param name="Sender">The node whose selection state changed.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleQDNodeSelectionChangeEvents (object Sender, QDNodeSelectionChangedEventArgs e);
        /// <summary>
        /// Triggered when the selection state changes.
        /// </summary>
        public event HandleQDNodeSelectionChangeEvents QDNodeSelectionChangedEvent;
    }

    /// <summary>
    /// Event data for check state change events.
    /// </summary>
    public class QDNodeCheckChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public QDNodeCheckChangedEventArgs () : base()
        {
            NewCheckState = true;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NewCheckState">New check state.</param>
        public QDNodeCheckChangedEventArgs (bool NewCheckState) : base()
        {
            this.NewCheckState = NewCheckState;
        }

        /// <summary>
        /// Get the new check state.
        /// </summary>
        public bool NewCheckState { get; internal set; }
    }

    /// <summary>
    /// Event data for menu command events.
    /// </summary>
    public class QDNodeMouseCommandEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public QDNodeMouseCommandEventArgs () : base()
        {
            RemoveItem = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RemoveItem">Sets the remove item flag.</param>
        public QDNodeMouseCommandEventArgs (bool RemoveItem) : base()
        {
            this.RemoveItem = RemoveItem;
        }

        /// <summary>
        /// Get the remove item flag.
        /// </summary>
        public bool RemoveItem { get; internal set; }
    }

    /// <summary>
    /// Event data for selection state change events.
    /// </summary>
    public class QDNodeSelectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public QDNodeSelectionChangedEventArgs () : base()
        {
            IsSelected = false;
            CancelSelection = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="IsSelected">New selection state.</param>
        public QDNodeSelectionChangedEventArgs (bool IsSelected) : base()
        {
            this.IsSelected = IsSelected;
            CancelSelection = false;
        }

        /// <summary>
        /// The new selection state.
        /// </summary>
        public bool IsSelected { get; internal set; }

        /// <summary>
        /// Allows the selection to be canceled.
        /// </summary>
        public bool CancelSelection { get; set; }
    }
}
