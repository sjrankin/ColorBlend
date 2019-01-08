using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Windows.Threading;
using System.Diagnostics;

namespace ColorBlend
{
    /// <summary>
    /// Base class for render transactions.
    /// </summary>
    public class RenderTransaction
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RenderTransaction ()
        {
            InitializeTransaction(TransactionTypes.NOP);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="TransactionType">The transaction type.</param>
        public RenderTransaction (TransactionTypes TransactionType)
        {
            InitializeTransaction(TransactionType);
        }

        /// <summary>
        /// Initialize the transaction.
        /// </summary>
        /// <param name="TransactionType">The transaction type.</param>
        private void InitializeTransaction (TransactionTypes TransactionType)
        {
            LayerIndex = 0;
            Instructions = new ObservableCollection<object>();
            Instructions.CollectionChanged += InstructionCollectionChanged;
            RenderCount = 1;
            this.TransactionType = TransactionType;
            RenderStart = DateTime.Now;
            RenderEnd = RenderStart;
        }

        /// <summary>
        /// Handle changes to the collection of instructions.
        /// </summary>
        /// <param name="Sender">The instruction that changed.</param>
        /// <param name="e">Event data.</param>
        void InstructionCollectionChanged (object Sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<object> Instructions = Sender as ObservableCollection<object>;
            if (Instructions == null)
            {
                RenderCount = 0;
                return;
            }
            RenderCount = Instructions.Count;
        }

        /// <summary>
        /// Add an instruction.
        /// </summary>
        /// <param name="Instruction">The instruction to add.</param>
        internal void AddInstruction (object Instruction)
        {
            Instructions.Add(Instruction);
        }

        /// <summary>
        /// Add the contents of <paramref name="OtherCollection"/> to this collection.
        /// </summary>
        /// <param name="OtherCollection">The other collection whose contents will be added to this collection.</param>
        public void AddInstructionCollection (ObservableCollection<object> OtherCollection)
        {
            foreach (object OtherObject in OtherCollection)
                AddInstruction(OtherObject);
        }

        /// <summary>
        /// Get or set the transaction type.
        /// </summary>
        public TransactionTypes TransactionType { get; set; }

        /// <summary>
        /// Get or set the render start time.
        /// </summary>
        public DateTime RenderStart { get; set; }

        /// <summary>
        /// Get or set the render end time.
        /// </summary>
        public DateTime RenderEnd { get; set; }

        /// <summary>
        /// Get the duration of the rendering.
        /// </summary>
        public Duration RenderDuration
        {
            get
            {
                if (RenderEnd < RenderStart)
                    return new Duration(new TimeSpan(0, 0, 0));
                return RenderEnd - RenderStart;
            }
        }

        /// <summary>
        /// Number of items to render.
        /// </summary>
        public int RenderCount { get; set; }

        /// <summary>
        /// Collection of instructions.
        /// </summary>
        public ObservableCollection<object> Instructions { get; internal set; }

        /// <summary>
        /// The layer index.
        /// </summary>
        public int LayerIndex { get; set; }
    }

    /// <summary>
    /// Render transaction types.
    /// </summary>
    public enum TransactionTypes
    {
        /// <summary>
        /// No operation.
        /// </summary>
        NOP,
        /// <summary>
        /// Line render transactions.
        /// </summary>
        DrawLine,
        /// <summary>
        /// Color block render transactions.
        /// </summary>
        DrawBlock,
        /// <summary>
        /// Color blob render transactions.
        /// </summary>
        DrawColorBlob,
        /// <summary>
        /// Background render transactions.
        /// </summary>
        DrawBackground
    }
}
