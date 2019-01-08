using System;
using System.Collections.Generic;
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
    /// Implements a pipeline of render operations.
    /// </summary>
    public class RenderPipeline
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RenderPipeline ()
        {
            Pipeline = new List<RenderTransaction>();
            CBI = new ColorBlenderInterface();
            RenderDuration = new TimeSpan(0);
        }

        /// <summary>
        /// Add a render transaction. Transactions will be executed in FILO order. In other words, the last transaction added
        /// will be executed first.
        /// </summary>
        /// <param name="NewTransaction">A render transaction.</param>
        public void AddTransaction (RenderTransaction NewTransaction)
        {
            NewTransaction.LayerIndex = LayerIndex++;
            Pipeline.Add(NewTransaction);
        }

        /// <summary>
        /// Layer index counter. Reset after each pipeline execution.
        /// </summary>
        private int LayerIndex = 0;

        /// <summary>
        /// Combined adjacent transaction into single transactions with multiple instructions.
        /// </summary>
        /// <param name="SourcePipeline">The pipeline to consolidate.</param>
        /// <returns>Consolidated pipeline.</returns>
        private List<RenderTransaction> ConsolidatePipeline (List<RenderTransaction> SourcePipeline)
        {
            if (SourcePipeline == null)
                throw new ArgumentNullException("SourcePipeline");
            if (SourcePipeline.Count < 1)
                return SourcePipeline;

            List<RenderTransaction> Consolidated = new List<RenderTransaction>();
            TransactionTypes CurrentType = SourcePipeline[0].TransactionType;
            //Need to prime the consolidated list.
            Consolidated.Add(SourcePipeline[0]);
            for (int i = 1; i < SourcePipeline.Count; i++)
            {
                RenderTransaction Transaction = SourcePipeline[i];
                if (Transaction.TransactionType == CurrentType)
                {
                    Consolidated.Last().AddInstructionCollection(Transaction.Instructions);
                }
                else
                {
                    CurrentType = Transaction.TransactionType;
                    Consolidated.Add(Transaction);
                }
            }
            return Consolidated;
        }

        /// <summary>
        /// Execute the pipeline.
        /// </summary>
        /// <param name="Target">Information about the buffer where rendering will occur.</param>
        /// <param name="ExecutionCount">Will contain the number of consolidated transactions executed.</param>
        /// <param name="FailIndex">
        /// On failure, will indicate which consolidated transaction caused the failure. The actual transaction that failed
        /// will be in the property <seealso cref="FailedTransaction"/>.
        /// </param>
        /// <returns>True on success, false if a transaction failed.</returns>
        public bool ExecutePipeline (RenderTarget Target, ref int ExecutionCount, ref int FailIndex)
        {
            if (Target == null)
                throw new ArgumentNullException("Target");
            Pipeline.Reverse();
            ExecutionCount = 0;
            FailedTransaction = null;
            RenderDuration = new TimeSpan(0);
            List<RenderTransaction> ConsolidatedPipeline = ConsolidatePipeline(Pipeline);
            foreach (RenderTransaction Transaction in ConsolidatedPipeline)
            {
                bool RenderResult = false;
                switch (Transaction.TransactionType)
                {
                    case TransactionTypes.NOP:
                        break;

                    case TransactionTypes.DrawBackground:
                        if (Transaction.Instructions.Count < 1)
                            throw new InvalidOperationException("No transaction instructions for DrawBackground.");
                        BackgroundDefinition BGClear = (BackgroundDefinition)((BackgroundTransaction)Transaction).Instructions[0];
                        byte[] local = Target.Bits;
                        CBI.DoClearBuffer(ref local, Target.Width, Target.Height, Target.Stride,
                            BGClear.BGColor, 
                            BGClear.DrawGrid, BGClear.GridColor, BGClear.GridCellWidth, BGClear.GridCellHeight,
                            Colors.Transparent);
                        Target.Bits = local;
                        break;

                    case TransactionTypes.DrawBlock:
                        CBI.DrawColorBlocks(ref Target,((ColorBlockTransaction)(Transaction)).ColorBlockList(),Colors.Transparent);
                        break;

                    case TransactionTypes.DrawColorBlob:
                        for (int DrawColorBlobIndex = 0; DrawColorBlobIndex < Transaction.RenderCount; DrawColorBlobIndex++)
                        {
                        }
                        break;

                    case TransactionTypes.DrawLine:
                        for (int DrawLineIndex = 0; DrawLineIndex < Transaction.RenderCount; DrawLineIndex++)
                        {
                        }
                        break;
                }
                if (!RenderResult)
                {
                    FailedTransaction = Transaction;
                    Clear();
                    FailIndex = ExecutionCount;
                    return false;
                }
                ExecutionCount++;
            }
            Clear();
            return true;
        }

        /// <summary>
        /// The duration of the render.
        /// </summary>
        public Duration RenderDuration { get; internal set; }

        /// <summary>
        /// If a failure was reported, this is the transaction that failed. If no failure was encountered, this is null.
        /// </summary>
        public RenderTransaction FailedTransaction { get; internal set; }

        /// <summary>
        /// Clear the pipeline.
        /// </summary>
        public void Clear ()
        {
            Pipeline.Clear();
            LayerIndex = 0;
        }

        /// <summary>
        /// Get the number of transactions.
        /// </summary>
        public int TransactionCount
        {
            get
            {
                return Pipeline.Count;
            }
        }

        /// <summary>
        /// The pipeline.
        /// </summary>
        private List<RenderTransaction> Pipeline { get; set; }

        /// <summary>
        /// Where the work is done.
        /// </summary>
        private ColorBlenderInterface CBI { get; set; }
    }
}
