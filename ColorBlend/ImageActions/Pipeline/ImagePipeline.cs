using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Image processing pipeline.
    /// </summary>
    public class ImagePipeline : IEnumerable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImagePipeline ()
        {
            Stages = new List<StageBase>();
        }

        /// <summary>
        /// Pipeline container.
        /// </summary>
        private List<StageBase> Stages;

        /// <summary>
        /// Get the number of nodes in the pipeline.
        /// </summary>
        public int Count
        {
            get
            {
                return Stages.Count;
            }
        }

        /// <summary>
        /// Clear the pipeline.
        /// </summary>
        public void Clear ()
        {
            Stages.Clear();
        }

        /// <summary>
        /// Add <paramref name="Node"/> to the pipeline.
        /// </summary>
        /// <param name="Node">Action node to add.</param>
        public void Add (StageBase Node)
        {
            Node.Input.Add(Node.Output.First);
            Stages.Add(Node);
            NotifyPipelineChanged(Stages.Count - 1);
        }

        /// <summary>
        /// Add a range of nodes to the pipeline.
        /// </summary>
        /// <param name="Nodes">List of nodes to add.</param>
        public void AddRange (List<StageBase> Nodes)
        {
            foreach (StageBase AB in Nodes)
                Add(AB);
        }

        /// <summary>
        /// Add the nodes from another pipeline to this pipeline.
        /// </summary>
        /// <param name="Nodes">Pipeline whose nodes will be added to this pipeline.</param>
        public void AddRange (ImagePipeline Nodes)
        {
            foreach (StageBase AB in Nodes)
                Add(AB);
        }

        /// <summary>
        /// Insert the node <paramref name="Node"/> into the pipeline at <paramref name="Index"/>.
        /// </summary>
        /// <param name="Index">Where to insert the node.</param>
        /// <param name="Node">The node to add.</param>
        public void Insert (int Index, StageBase Node)
        {
            StageBase Previous = null;
            if (Index > 0)
            {
                Previous = Stages[Index - 1];
                Node.Input.Add(Previous.Output.First);
            }
            StageBase Next = null;
            if (Index < Count - 1)
                Next = Last();
            if (Next != null)
            {
                Next.Input.Add(Node.Output.First);
            }
            Stages.Insert(Index, Node);
            NotifyPipelineChanged(Index);
        }

        /// <summary>
        /// Moves the stage at <paramref name="OldIndex"/> to <paramref name="NewIndex"/>.
        /// </summary>
        /// <param name="OldIndex">The location of the stage to move.</param>
        /// <param name="NewIndex">The new location of the stage on success.</param>
        public void Move(int OldIndex, int NewIndex)
        {
            if (Stages.Count < 1)
                return;
            if (OldIndex < 0 || OldIndex > Stages.Count - 1)
                throw new ArgumentOutOfRangeException("OldIndex out of range.");
            if (NewIndex < 0 || NewIndex > Stages.Count - 1)
                throw new ArgumentOutOfRangeException("NewIndex out of range.");
            Insert(NewIndex, Stages[OldIndex]);
            RemoveAt(OldIndex);
            NotifyPipelineChanged(NewIndex);
        }

        /// <summary>
        /// Remove the node at the pipeline index <paramref name="Index"/>.
        /// </summary>
        /// <param name="Index">The index of the node to remove.</param>
        public void RemoveAt (int Index)
        {
            if (Index < 0)
                throw new ArgumentOutOfRangeException("Index too small.");
            if (Index > Count - 1)
                throw new ArgumentOutOfRangeException("Index too big.");
            if (Index == 0)
            {
                Stages[1].Input.Clear();
                Stages.RemoveAt(0);
                NotifyPipelineChanged(-1);
                return;
            }
            if (Index < Count - 1)
            {
                Stages[Index + 1].Input.Clear();
                Stages[Index + 1].Input.Add(Stages[Index - 1].Output.First);
                Stages.RemoveAt(Index);
                NotifyPipelineChanged(-1);
                return;
            }
        }

        /// <summary>
        /// Returns the index of <paramref name="Node"/>.
        /// </summary>
        /// <param name="Node">The node whose index will be returned.</param>
        /// <returns>Index of <paramref name="Node"/> on success, -1 if not found.</returns>
        public int IndexOf (StageBase Node)
        {
            return Stages.IndexOf(Node);
        }

        /// <summary>
        /// Return the first node in the pipeline (regardless of visibility).
        /// </summary>
        /// <returns>The first node in the pipeline.</returns>
        public StageBase First()
        {
            if (Stages == null)
                return null;
            if (Stages.Count < 1)
                return null;
            return Stages.First();
        }

        /// <summary>
        /// Return the last node in the pipeline (regardless of visibility).
        /// </summary>
        /// <returns>The last node in the pipeline.</returns>
        public StageBase Last ()
        {
            if (Stages == null)
                return null;
            if (Stages.Count < 1)
                return null;
            return Stages.Last();
        }

        /// <summary>
        /// Return the last visible node in the pipeline.
        /// </summary>
        /// <returns>Last visible node in the pipeline.</returns>
        public StageBase LastVisible ()
        {
            if (Stages == null)
                return null;
            if (Stages.Count < 1)
                return null;
            foreach (StageBase AB in Stages)
            {
                if (AB.IsSink)
                    return AB;
            }
            return Last();
        }

        /// <summary>
        /// Execute the pipeline starting at <paramref name="StartingIndex"/> for <paramref name="ExecuteCount"/> nodes. Will stop
        /// early if a sink node is encountered.
        /// </summary>
        /// <param name="StartingIndex">Index of the first node to execute.</param>
        /// <param name="ExecuteCount">Number of nodes to execute.</param>
        /// <returns>Number of nodes executed.</returns>
        public int Execute (int StartingIndex, int ExecuteCount)
        {
            int NodesExecuted = 0;
            for (int i = StartingIndex; i < StartingIndex + ExecuteCount; i++)
            {
                NodesExecuted++;
                Stages[i].Refresh();
                if (Stages[i].IsSink)
                    return NodesExecuted;
            }
            return NodesExecuted;
        }

        /// <summary>
        /// Execute the pipeline starting at the node whose index is <paramref name="StartingIndex"/> to the end of the pipeline or
        /// until a sink node is encountered.
        /// </summary>
        /// <param name="StartingIndex">Index of the first node to execute.</param>
        /// <returns>Number of nodes executed.</returns>
        public int Execute (int StartingIndex)
        {
            return Execute(StartingIndex, Stages.Count - StartingIndex);
        }

        /// <summary>
        /// Execute the entire pipeline until the end is reached or a sink node is encountered.
        /// </summary>
        /// <returns>Number of nodes executed.</returns>
        public int Execute ()
        {
            return Execute(0);
        }

        /// <summary>
        /// Implements this.
        /// </summary>
        /// <param name="Index">Index of the node in the pipeline to access.</param>
        /// <returns>The node at <paramref name="Index"/>.</returns>
        public StageBase this[int Index]
        {
            get
            {
                if (Index < 0)
                    throw new ArgumentOutOfRangeException("Index too small.");
                if (Index > Count - 1)
                    throw new ArgumentOutOfRangeException("Index too big.");
                return Stages[Index];
            }
            set
            {
                if (Index < 0)
                    throw new ArgumentOutOfRangeException("Index too small.");
                if (Index > Count - 1)
                    throw new ArgumentOutOfRangeException("Index too big.");
                NotifyPipelineChanged(Index);
                Stages[Index] = value;
            }
        }

        /// <summary>
        /// Enumerate through the nodes in the pipeline.
        /// </summary>
        /// <returns>A pipeline node.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (StageBase AB in Stages)
                yield return AB;
        }

        /// <summary>
        /// Notifies any subscribers that a pipeline stage has been changed (added, removed, inserted...).
        /// </summary>
        /// <param name="ChangeIndex">The index of the changed pipeline stage.</param>
        private void NotifyPipelineChanged (int ChangeIndex)
        {
            if (PipelineStageChanged == null)
                return;
            PipelineStageChangeEventArgs args = new PipelineStageChangeEventArgs(Stages.Count, ChangeIndex);
            PipelineStageChanged(this, args);
        }

        /// <summary>
        /// Delegate definition for pipeline stage change events.
        /// </summary>
        /// <param name="Sender">The pipeline where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public delegate void HandlePipelineStageChangeEvents (object Sender, PipelineStageChangeEventArgs e);
        /// <summary>
        /// Triggered when a stage in the pipeline changes.
        /// </summary>
        public event HandlePipelineStageChangeEvents PipelineStageChanged;
    }

    /// <summary>
    /// Event data for pipeline stage change events.
    /// </summary>
    public class PipelineStageChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PipelineStageChangeEventArgs () : base()
        {
            NewStageCount = 0;
            ChangeIndex = -1;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NewStageCount">New number of stages in the pipeline.</param>
        /// <param name="ChangeIndex">The index of the changed stage.</param>
        public PipelineStageChangeEventArgs (int NewStageCount, int ChangeIndex) : base()
        {
            this.NewStageCount = NewStageCount;
            this.ChangeIndex = ChangeIndex;
        }

        /// <summary>
        /// Get the number of items in the pipeline after the stage change.
        /// </summary>
        public int NewStageCount { get; internal set; }

        /// <summary>
        /// Index of the changed stage.
        /// </summary>
        public int ChangeIndex { get; internal set; }
    }
}
