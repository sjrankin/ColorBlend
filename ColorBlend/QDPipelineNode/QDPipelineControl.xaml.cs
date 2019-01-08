using System;
using System.Collections.Generic;
using System.Collections;
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
    /// Interaction logic for QDPipelineControl.xaml
    /// </summary>
    public partial class QDPipelineControl : UserControl, IEnumerable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public QDPipelineControl ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add a pipeline stage to the list.
        /// </summary>
        /// <param name="PipelineStage">The stage to add.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool AddStage (StageBase PipelineStage)
        {
            if (PipelineStage == null)
                return false;
            if (ContainsStage(PipelineStage.StageID))
                return false;
            QDPipelineNode QNode = new QDPipelineNode();
            QNode.QDNodeSelectionChangedEvent += StageNodeSelected;
            QNode.QDNodeCheckChanged += StageNodeEnableChanged;
            QNode.SetText(PipelineStage.ShortName);
            QNode.SetBitmap(PipelineStage.Output.First);
            Pipeline.Items.Add(QNode);
            return true;
        }

        /// <summary>
        /// Handle enable check change events in stages.
        /// </summary>
        /// <param name="Sender">The stage whose enable check changed.</param>
        /// <param name="e">Event data.</param>
        private void StageNodeEnableChanged (object Sender, QDNodeCheckChangedEventArgs e)
        {
            QDPipelineNode QNode = Sender as QDPipelineNode;
            if (QNode == null)
                return;
            int NewIndex = StageIndex(QNode.NodeID);
            if (NewIndex < 0)
                return;
            if (StageUIChangeEvents == null)
                return;
            HandleStageUIChangeEventArgs args = new HandleStageUIChangeEventArgs
            {
                StageID = QNode.NodeID,
                EnableStateChanged = true,
                NewEnableState = e.NewCheckState
            };
            StageUIChangeEvents(this, args);
        }


        /// <summary>
        /// Remove the stage with the ID of <paramref name="StageID"/> from the pipeline.
        /// </summary>
        /// <param name="StageID">ID of the stage to remove.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool RemoveStage (Guid StageID)
        {
            int Index = StageIndex(StageID);
            if (Index < 0)
                return false;
            if (((QDPipelineNode)Pipeline.Items[Index]).IsSelected)
                _SelectedNode = Guid.Empty;
            Pipeline.Items.RemoveAt(Index);
            return true;
        }

        /// <summary>
        /// Return the index of the stage whose ID is <paramref name="StageID"/>.
        /// </summary>
        /// <param name="StageID">ID of the stage whose index will be returned.</param>
        /// <returns>Index of the specified stage. -1 if not found.</returns>
        public int StageIndex (Guid StageID)
        {
            for (int i = 0; i < Pipeline.Items.Count; i++)
                if (((QDPipelineNode)Pipeline.Items[i]).NodeID == StageID)
                    return i;
            return -1;
        }

        /// <summary>
        /// Handle user-selection events (mouse clicks on stage nodes).
        /// </summary>
        /// <remarks>
        /// Handles events from stage nodes.
        /// </remarks>
        /// <param name="Sender">The stage node that was selected.</param>
        /// <param name="e">Event data.</param>
        private void StageNodeSelected (object Sender, QDNodeSelectionChangedEventArgs e)
        {
            QDPipelineNode QNode = Sender as QDPipelineNode;
            if (QNode == null)
                return;
            DeselectNode(_SelectedNode);
            SelectNode(QNode.NodeID);
        }

        /// <summary>
        /// Add a list of stages to the stage UI.
        /// </summary>
        /// <param name="PipelineStages">List of stages to add.</param>
        /// <param name="DoClear">If true, existing stages are removed before new stages added.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool BatchAdd (List<StageBase> PipelineStages, bool DoClear)
        {
            if (PipelineStages == null)
                return false;
            if (PipelineStages.Count < 1)
                return false;
            if (DoClear)
                Clear();
            foreach (StageBase Stage in PipelineStages)
                AddStage(Stage);
            return true;
        }

        /// <summary>
        /// Remove all stages from the pipeline list.
        /// </summary>
        public void Clear ()
        {
            Pipeline.Items.Clear();
        }

        /// <summary>
        /// Determines if a stage with the ID of <paramref name="StageID"/> is in the pipeline list.
        /// </summary>
        /// <param name="StageID"></param>
        /// <returns>True if a stage with the ID of <paramref name="StageID"/> exists, false if not.</returns>
        public bool ContainsStage (Guid StageID)
        {
            return StageIndex(StageID) < 0 ? false : true;
        }

        private Guid _SelectedNode = Guid.Empty;
        /// <summary>
        /// Get or set the selected stage node.
        /// </summary>
        public Guid SelectedNode
        {
            get
            {
                return _SelectedNode;
            }
            set
            {
                QDPipelineNode QNode = this[value];
                if (QNode == null)
                    return;
                QDPipelineNode OldNode = this[_SelectedNode];
                if (OldNode != null)
                    OldNode.DeselectNode();
                QNode.SelectNode();
                _SelectedNode = value;
            }
        }

        /// <summary>
        /// Select the stage node whose ID is <paramref name="NodeID"/>. Deselects previously selected node.
        /// </summary>
        /// <param name="NodeID">ID of the stage node to select.</param>
        public void SelectNode (Guid NodeID)
        {
            foreach (QDPipelineNode QNode in Pipeline.Items)
            {
                if (QNode.NodeID == NodeID)
                {
                    QNode.SelectNode();
                    QDPipelineNode OldNode = this[_SelectedNode];
                    if (OldNode != null)
                        OldNode.DeselectNode();
                    _SelectedNode = NodeID;
                    return;
                }
            }
        }

        /// <summary>
        /// Deselect the node whose ID is <paramref name="NodeID"/>.
        /// </summary>
        /// <param name="NodeID">ID of the node to deselect.</param>
        public void DeselectNode (Guid NodeID)
        {
            foreach (QDPipelineNode QNode in Pipeline.Items)
            {
                if (QNode.NodeID == NodeID)
                {
                    QNode.DeselectNode();
                    _SelectedNode = Guid.Empty;
                    return;
                }
            }
        }

        /// <summary>
        /// Deselect all items.
        /// </summary>
        public void ClearSelections ()
        {
            DeselectNode(_SelectedNode);
        }

        /// <summary>
        /// Handle selection events in the stage node ListBox.
        /// </summary>
        /// <remarks>
        /// Handles events from the ListBox.
        /// </remarks>
        /// <param name="Sender">Node where the click occurred.</param>
        /// <param name="e">Event data.</param>
        private void PipelineSelectionChanged (object Sender, SelectionChangedEventArgs e)
        {
            ListBox LB = Sender as ListBox;
            if (LB == null)
                return;
            QDPipelineNode QNode = LB.SelectedItem as QDPipelineNode;
            if (QNode == null)
                return;
            DeselectNode(_SelectedNode);
            SelectNode(QNode.NodeID);
        }

        /// <summary>
        /// Implements this.
        /// </summary>
        /// <param name="StageID">ID of the stage to return.</param>
        /// <returns>Stage node whose ID is <paramref name="StageID"/> on success, null if not found.</returns>
        public QDPipelineNode this[Guid StageID]
        {
            get
            {
                foreach (QDPipelineNode QNode in Pipeline.Items)
                    if (QNode.NodeID == StageID)
                        return QNode;
                return null;
            }
        }

        /// <summary>
        /// Enumerate through the stage nodes.
        /// </summary>
        /// <returns>Stage node.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (QDPipelineNode QNode in Pipeline.Items)
                yield return QNode;
        }

        /// <summary>
        /// Get or set the background of the stage node list.
        /// </summary>
        public new Brush Background
        {
            get
            {
                return Pipeline.Background;
            }
            set
            {
                Pipeline.Background = value;
            }
        }

        /// <summary>
        /// Event handler definition for pipeline stage UI change events.
        /// </summary>
        /// <param name="Sender">The pipeline list control that contains the changed state.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleStageUIChangeEvents (object Sender, HandleStageUIChangeEventArgs e);
        /// <summary>
        /// Triggered when a UI element changes state in a pipeline stage.
        /// </summary>
        public event HandleStageUIChangeEvents StageUIChangeEvents;
    }

    /// <summary>
    /// Event data for UI element state changes for pipeline stages.
    /// </summary>
    public class HandleStageUIChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HandleStageUIChangeEventArgs () : base()
        {
            StageID = Guid.Empty;
            EnableStateChanged = false;
            NewEnableState = false;
        }

        /// <summary>
        /// ID of the pipeline stage (and UI control) that changed.
        /// </summary>
        public Guid StageID { get; internal set; }

        /// <summary>
        /// Determines if the enable state changed.
        /// </summary>
        public bool EnableStateChanged { get; internal set; }

        /// <summary>
        /// The new enable state. Valid only if <seealso cref="EnableStateChanged"/> is true.
        /// </summary>
        public bool NewEnableState { get; internal set; }
    }
}
