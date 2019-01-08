using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Iro3.Data.Image;

namespace ColorBlend
{
    /// <summary>
    /// Implements a restrictive list of images.
    /// </summary>
    public class ImageList2 : IEnumerable
    {
        /// <summary>
        /// Default constructor. Sets maximum count to 1.
        /// </summary>
        public ImageList2()
        {
            Images = new List<ImageData>();
            _MaximumCount = 1;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="MaximumCount">Maximum allowable number of images.</param>
        public ImageList2(int MaximumCount)
        {
            if (MaximumCount < 1)
                throw new ArgumentOutOfRangeException("MaximumCount too small.");
            Images = new List<ImageData>();
            _MaximumCount = MaximumCount;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="MaximumCount">Maximum allowable number of images.</param>
        /// <param name="Images">List of images to add.</param>
        public ImageList2 (int MaximumCount, List<ImageData> Images)
        {
            if (MaximumCount < 1)
                throw new ArgumentOutOfRangeException("MaximumCount too small.");
            if (Images == null)
                throw new ArgumentNullException("Images");
            this.Images = new List<ImageData>();
            _MaximumCount = MaximumCount;
            foreach (ImageData WB in Images)
                this.Images.Add(WB);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="MaximumCount">Maximum allowable number of images.</param>
        /// <param name="Images">List of images to add.</param>
        public ImageList2 (int MaximumCount, ImageList2 Images)
        {
            if (MaximumCount < 1)
                throw new ArgumentOutOfRangeException("MaximumCount too small.");
            if (Images == null)
                throw new ArgumentNullException("Images");
            this.Images = new List<ImageData>();
            _MaximumCount = MaximumCount;
            foreach (ImageData WB in Images)
                this.Images.Add(WB);
        }

        /// <summary>
        /// Get or set the first item in the image list. If there are no items in the list, null is returned or the value
        /// is added to the empty list.
        /// </summary>
        public ImageData First
        {
            get
            {
                if (Images.Count < 1)
                    return null;
                return Images[0];
            }
            set
            {
                if (Images.Count < 1)
                    Add(value);
                else
                    Images[0] = value;
            }
        }

        /// <summary>
        /// Internal list of images.
        /// </summary>
        private List<ImageData> Images = null;

        private int _MaximumCount = 1;
        /// <summary>
        /// Get or set the maximum number of images that can be stored in the list. If the new value is less than the old
        /// value, images past the new value are removed.
        /// </summary>
        public int MaximumCount
        {
            get
            {
                return _MaximumCount;
            }
            set
            {
                if (value < _MaximumCount)
                    RemoveToEnd(value);
                _MaximumCount = value;
                if (_MaximumCount < 1)
                    throw new ArgumentOutOfRangeException("Maximum count must be at least 1.");
            }
        }

        private bool _NoMaximumLimit = false;
        /// <summary>
        /// Get or set the no maximum limit flag. If setting to false, the maximum limit is reset to 1. (Setting to true
        /// internally sets the maximum count to int.MaxValue.)
        /// </summary>
        public bool NoMaximumLimit
        {
            get
            {
                return _NoMaximumLimit;
            }
            set
            {
                if (value)
                    MaximumCount = int.MaxValue;
                else
                    MaximumCount = 1;
            }
        }

        /// <summary>
        /// Remove images from the list based on the specified range.
        /// </summary>
        /// <param name="StartingIndex">Starting index of the images to remove.</param>
        /// <param name="ItemCount">Number of images to remove.</param>
        public void RemoveRange (int StartingIndex, int ItemCount)
        {
            Images.RemoveRange(StartingIndex, ItemCount);
            if (ListChangeEvent!=null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.RemoveRange
                };
                for (int i = StartingIndex; i < StartingIndex + ItemCount; i++)
                    args.ChangedIndices.Add(i);
                ListChangeEvent(this, args);
            }
        }

        /// <summary>
        /// Remove images from the list starting at <paramref name="StartingIndex"/> to the end of the list.
        /// </summary>
        /// <param name="StartingIndex">Starting index of the images to remove.</param>
        public void RemoveToEnd (int StartingIndex)
        {
            int ItemCount = Images.Count - StartingIndex;
            RemoveRange(StartingIndex, ItemCount);
            if (ListChangeEvent != null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.RemoveRange
                };
                for (int i = StartingIndex; i < StartingIndex + ItemCount; i++)
                    args.ChangedIndices.Add(i);
                ListChangeEvent(this, args);
            }
        }

        /// <summary>
        /// Add a range of images to the image list. If more images are added than are allowable, <seealso cref="Add"/> will
        /// throw an exception.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        public void AddRange (List<ImageData> Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            Images.AddRange(Range);
            if (ListChangeEvent != null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.AddRange
                };
                for (int i = Images.Count-Range.Count;i<Images.Count;i++)
                    args.ChangedIndices.Add(i);
                ListChangeEvent(this, args);
            }
        }

        /// <summary>
        /// Try to add a range of images to the list.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        /// <returns>True on success, false on failure (too many items to add).</returns>
        public bool TryAddRange(List<ImageData> Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            foreach(ImageData WB in Range)
            {
                bool AddedOK = TryAdd(WB);
                if (!AddedOK)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Add a range of images to the image list. If more images are added than are allowable, <seealso cref="Add"/> will
        /// throw an exception.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        public void AddRange (ImageList2 Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            foreach (ImageData WB in Range)
            {
                Images.Add(WB);
                if (ListChangeEvent != null)
                {
                    ListChangeEventArgs args = new ListChangeEventArgs
                    {
                        ChangeEvent = ListChangeEvents.AddOne
                    };
                    args.ChangedIndices.Add(Images.Count - 1);
                    ListChangeEvent(this, args);
                }
            }
        }

        /// <summary>
        /// Try to add a range of images to the list.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        /// <returns>True on success, false on failure (too many items to add).</returns>
        public bool TryAddRange (ImageList2 Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            foreach (ImageData WB in Range)
            {
                bool AddedOK = TryAdd(WB);
                if (!AddedOK)
                    return false;
                if (ListChangeEvent != null)
                {
                    ListChangeEventArgs args = new ListChangeEventArgs
                    {
                        ChangeEvent = ListChangeEvents.AddOne
                    };
                    args.ChangedIndices.Add(Images.Count - 1);
                    ListChangeEvent(this, args);
                }
            }
            return true;
        }

        /// <summary>
        /// Return the number of items in the image list.
        /// </summary>
        public int Count
        {
            get
            {
                return Images.Count;
            }
        }

        /// <summary>
        /// Add an image to the image list.
        /// </summary>
        /// <param name="NewBitmap">The image to add.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there is no room to add the new image (e.g., the list is already at the maximum count).
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="NewBitmap"/> is null.
        /// </exception>
        public void Add (ImageData NewBitmap)
        {
            if (Count == MaximumCount)
                throw new InvalidOperationException("List at maximum capacity.");
            if (NewBitmap == null)
                throw new ArgumentNullException("NewBitmap");
            Images.Add(NewBitmap);
            if (ListChangeEvent != null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.AddOne
                };
                args.ChangedIndices.Add(Images.Count - 1);
                ListChangeEvent(this, args);
            }
        }

        /// <summary>
        /// Attempt to add the image <paramref name="NewBitmap"/> to the image list.
        /// </summary>
        /// <param name="NewBitmap">The image to add.</param>
        /// <returns>True on success, false if the image list is full (count is at <paramref name="MaximumCount"/>.</returns>
        public bool TryAdd(ImageData NewBitmap)
        {
            if (NewBitmap == null)
                throw new ArgumentNullException("NewBitmap");
            if (Count == MaximumCount)
                return false;
            Images.Add(NewBitmap);
            if (ListChangeEvent != null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.AddOne
                };
                args.ChangedIndices.Add(Images.Count - 1);
                ListChangeEvent(this, args);
            }
            return true;
        }

        /// <summary>
        /// Remove the image <paramref name="OldBitmap"/> from the image list.
        /// </summary>
        /// <param name="OldBitmap">The image to remove.</param>
        /// <returns>True on success, false on error.</returns>
        public bool Remove(ImageData OldBitmap)
        {
            if (OldBitmap == null)
                throw new ArgumentNullException("OldBitmap");
            int OldBitmapIndex = Images.IndexOf(OldBitmap);
            if (OldBitmapIndex < 0)
                return false;
            bool Result= Images.Remove(OldBitmap);
            if (ListChangeEvent != null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.RemoveOne
                };
                args.ChangedIndices.Add(OldBitmapIndex);
                ListChangeEvent(this, args);
            }
            return Result;
        }

        /// <summary>
        /// Clear the image list.
        /// </summary>
        public void Clear()
        {
            Images.Clear();
            if (ListChangeEvent != null)
            {
                ListChangeEventArgs args = new ListChangeEventArgs
                {
                    ChangeEvent = ListChangeEvents.Clear
                };
                ListChangeEvent(this, args);
            }
        }

        /// <summary>
        /// Implements this.
        /// </summary>
        /// <param name="Index">Index of the item to access.</param>
        /// <returns>The image at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="Index"/> is out of valid range.
        /// </exception>
        public ImageData this[int Index]
        {
            get
            {
                if (Index < 1)
                    throw new ArgumentOutOfRangeException("Index too small.");
                if (Index > Count - 1)
                    throw new ArgumentOutOfRangeException("Index too big.");
                return Images[Index];
            }
            set
            {
                if (Index < 1)
                    throw new ArgumentOutOfRangeException("Index too small.");
                if (Index > Count - 1)
                    throw new ArgumentOutOfRangeException("Index too big.");
                if (Index > MaximumCount - 1)
                    throw new ArgumentOutOfRangeException("Index greater than maximum allowable count.");
                Images[Index] = value;
            }
        }

        /// <summary>
        /// Get the enumerator for the image list.
        /// </summary>
        /// <returns>Image list enumerator.</returns>
        public IEnumerator GetEnumerator ()
        {
            foreach (ImageData ID in Images)
                yield return ID;
        }

        /// <summary>
        /// Return the contents of the image list as an array of images.
        /// </summary>
        /// <returns>Array of images. If no items are in the image list, null is returned.</returns>
        public ImageData[] ToArray()
        {
            if (Count < 1)
                return null;
            ImageData[] Ary = new ImageData[Count];
            for (int i = 0; i < Count; i++)
                Ary[i] = Images[i];
            return Ary;
        }

        /// <summary>
        /// Event handler definition for list change events.
        /// </summary>
        /// <param name="Sender">Instance of the image list where the change occurred.</param>
        /// <param name="e">Change data.</param>
        public delegate void HandleListChangeEvent (object Sender, ListChangeEventArgs e);
        /// <summary>
        /// Triggered when images are added or removed from the list.
        /// </summary>
        public event HandleListChangeEvent ListChangeEvent;
    }

    /// <summary>
    /// Change events for images in the image list.
    /// </summary>
    public enum ListChangeEvents
    {
        /// <summary>
        /// No change.
        /// </summary>
        NoChange,
        /// <summary>
        /// One image added.
        /// </summary>
        AddOne,
        /// <summary>
        /// One image removed.
        /// </summary>
        RemoveOne,
        /// <summary>
        /// Range of images added.
        /// </summary>
        AddRange,
        /// <summary>
        /// Range of images removed.
        /// </summary>
        RemoveRange,
        /// <summary>
        /// All images removed.
        /// </summary>
        Clear
    }

    /// <summary>
    /// Event data for image list changes.
    /// </summary>
    public class ListChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ListChangeEventArgs () : base()
        {
            ChangeEvent = ListChangeEvents.NoChange;
            ChangedIndices = new List<int>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ChangedIndex">The index where the change occurred.</param>
        /// <param name="ChangeEvent">The type of change.</param>
        public ListChangeEventArgs(int ChangedIndex, ListChangeEvents ChangeEvent) :base()
        {
            this.ChangeEvent = ChangeEvent;
            ChangedIndices = new List<int>();
            ChangedIndices.Add(ChangedIndex);
        }

        /// <summary>
        /// Get a list of all locations in the image list that changed. If the list was cleared, this will be empty.
        /// </summary>
        public List<int> ChangedIndices { get; internal set; }

        /// <summary>
        /// The change that occurred.
        /// </summary>
        public ListChangeEvents ChangeEvent { get; internal set; }
    }
}
