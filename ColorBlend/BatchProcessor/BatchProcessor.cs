using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Batches objects for processing.
    /// </summary>
    public class BatchProcessor : IEnumerable
    {
        public BatchProcessor ()
        {
            CommonInitialization();
        }

        public BatchProcessor (List<object> Original)
        {
            if (Original == null)
                throw new ArgumentNullException("Original");
            CommonInitialization();
            PopulateWith(Original);
        }

        private void CommonInitialization ()
        {
            Data = new Queue<object>();
        }

        public void Clear ()
        {
            Data.Clear();
        }

        public void Add (object NewObject)
        {
            Data.Enqueue(NewObject);
        }

        public void Remove (object OldObject)
        {
            bool Replaced = false;
            List<object> scratch = null;
            foreach (object Something in Data)
            {
                if (Something == OldObject)
                {
                    scratch = ToList();
                    scratch.Remove(OldObject);
                    Replaced = true;
                    break;
                }
            }
            if (Replaced)
                PopulateWith(scratch);
        }

        public void RemoveAt (int Index)
        {
            if (Index < 0 || Index > Data.Count - 1)
                throw new IndexOutOfRangeException("Index");
            List<object> scratch = ToList();
            scratch.RemoveAt(Index);
            PopulateWith(scratch);
        }

        public bool Contains (object ThisObject)
        {
            return Data.Contains(ThisObject);
        }

        public object First
        {
            get
            {
                if (Data.Count < 1)
                    throw new InvalidOperationException("Not populated.");
                return Data.First();
            }
            set
            {
                if (Data.Count < 1)
                    Add(value);
                else
                {
                    this[0] = value;
                }
            }
        }

        public object Last
        {
            get
            {
                return Data.Last();
            }
            set
            {
                this[Data.Count - 1] = value;
            }
        }

        public object Next
        {
            get
            {
                return Dequeue();
            }
        }

        public bool TryGetNext (out object NextObject)
        {
            NextObject = null;
            if (Data.Count < 1)
                return false;
            NextObject = Data.Dequeue();
            return true;
        }

        public object Dequeue ()
        {

            if (Data.Count < 1)
                throw new InvalidOperationException("No data to dequeue.");
            return Data.Dequeue();
        }

        public bool TryDequeue (out object Dequeued)
        {
            Dequeued = null;
            if (Data.Count < 1)
                return false;
            Dequeued = Data.Dequeue();
            return true;
        }

        public void Enqueue (object NewObject)
        {
            Data.Enqueue(NewObject);
        }



        public int Count
        {
            get
            {
                return Data.Count;
            }
        }

        public object this[int Index]
        {
            get
            {
                if (Index < 0 || Index > Count - 1)
                    throw new IndexOutOfRangeException("Index");
                List<object> scratch = ToList();
                return scratch[Index];
            }
            set
            {
                if (Index < 0 || Index > Count - 1)
                    throw new IndexOutOfRangeException("Index");
                List<object> scratch = ToList();
                scratch[Index] = value;
                PopulateWith(scratch);
            }
        }

        private Queue<object> Data = null;

        public IEnumerator GetEnumerator ()
        {
            foreach (object Something in Data)
                yield return Something;
        }

        public List<object> ToList ()
        {
            List<object> TheList = new List<object>();
            foreach (object Something in Data)
                TheList.Add(Something);
            return TheList;
        }

        public void PopulateWith (List<object> TheList)
        {
            Data.Clear();
            foreach (object Something in TheList)
                Data.Enqueue(Something);
        }
    }
}
