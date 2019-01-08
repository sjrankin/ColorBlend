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

namespace ColorBlend
{
    /// <summary>
    /// Implements a restrictive list of images.
    /// </summary>
    public class ImageList : IEnumerable
    {
        /// <summary>
        /// Default constructor. Sets maximum count to 1.
        /// </summary>
        public ImageList()
        {
            Images = new List<WriteableBitmap>();
            _MaximumCount = 1;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="MaximumCount">Maximum allowable number of images.</param>
        public ImageList(int MaximumCount)
        {
            if (MaximumCount < 1)
                throw new ArgumentOutOfRangeException("MaximumCount too small.");
            Images = new List<WriteableBitmap>();
            _MaximumCount = MaximumCount;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="MaximumCount">Maximum allowable number of images.</param>
        /// <param name="Images">List of images to add.</param>
        public ImageList (int MaximumCount, List<WriteableBitmap> Images)
        {
            if (MaximumCount < 1)
                throw new ArgumentOutOfRangeException("MaximumCount too small.");
            if (Images == null)
                throw new ArgumentNullException("Images");
            this.Images = new List<WriteableBitmap>();
            _MaximumCount = MaximumCount;
            foreach (WriteableBitmap WB in Images)
                this.Images.Add(WB);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="MaximumCount">Maximum allowable number of images.</param>
        /// <param name="Images">List of images to add.</param>
        public ImageList (int MaximumCount, ImageList Images)
        {
            if (MaximumCount < 1)
                throw new ArgumentOutOfRangeException("MaximumCount too small.");
            if (Images == null)
                throw new ArgumentNullException("Images");
            this.Images = new List<WriteableBitmap>();
            _MaximumCount = MaximumCount;
            foreach (WriteableBitmap WB in Images)
                this.Images.Add(WB);
        }

        /// <summary>
        /// Get or set the first item in the image list. If there are no items in the list, null is returned or the value
        /// is added to the empty list.
        /// </summary>
        public WriteableBitmap First
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
        private List<WriteableBitmap> Images = null;

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
        }

        /// <summary>
        /// Remove images from the list starting at <paramref name="StartingIndex"/> to the end of the list.
        /// </summary>
        /// <param name="StartingIndex">Starting index of the images to remove.</param>
        public void RemoveToEnd (int StartingIndex)
        {
            int ItemCount = Images.Count - StartingIndex;
            RemoveRange(StartingIndex, ItemCount);
        }

        /// <summary>
        /// Add a range of images to the image list. If more images are added than are allowable, <seealso cref="Add"/> will
        /// throw an exception.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        public void AddRange (List<WriteableBitmap> Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            Images.AddRange(Range);
        }

        /// <summary>
        /// Try to add a range of images to the list.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        /// <returns>True on success, false on failure (too many items to add).</returns>
        public bool TryAddRange(List<WriteableBitmap> Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            foreach(WriteableBitmap WB in Range)
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
        public void AddRange (ImageList Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            foreach (WriteableBitmap WB in Range)
                Images.Add(WB);
        }

        /// <summary>
        /// Try to add a range of images to the list.
        /// </summary>
        /// <param name="Range">List of images to add.</param>
        /// <returns>True on success, false on failure (too many items to add).</returns>
        public bool TryAddRange (ImageList Range)
        {
            if (Range == null)
                throw new ArgumentNullException("Range");
            foreach (WriteableBitmap WB in Range)
            {
                bool AddedOK = TryAdd(WB);
                if (!AddedOK)
                    return false;
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
        public void Add (WriteableBitmap NewBitmap)
        {
            if (Count == MaximumCount)
                throw new InvalidOperationException("List at maximum capacity.");
            if (NewBitmap == null)
                throw new ArgumentNullException("NewBitmap");
            Images.Add(NewBitmap);
        }

        /// <summary>
        /// Attempt to add the image <paramref name="NewBitmap"/> to the image list.
        /// </summary>
        /// <param name="NewBitmap">The image to add.</param>
        /// <returns>True on success, false if the image list is full (count is at <paramref name="MaximumCount"/>.</returns>
        public bool TryAdd(WriteableBitmap NewBitmap)
        {
            if (NewBitmap == null)
                throw new ArgumentNullException("NewBitmap");
            if (Count == MaximumCount)
                return false;
            Images.Add(NewBitmap);
            return true;
        }

        /// <summary>
        /// Remove the image <paramref name="OldBitmap"/> from the image list.
        /// </summary>
        /// <param name="OldBitmap">The image to remove.</param>
        /// <returns>True on success, false on error.</returns>
        public bool Remove(WriteableBitmap OldBitmap)
        {
            if (OldBitmap == null)
                throw new ArgumentNullException("OldBitmap");
            return Images.Remove(OldBitmap);
        }

        /// <summary>
        /// Clear the image list.
        /// </summary>
        public void Clear()
        {
            Images.Clear();
        }

        /// <summary>
        /// Implements this.
        /// </summary>
        /// <param name="Index">Index of the item to access.</param>
        /// <returns>The image at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="Index"/> is out of valid range.
        /// </exception>
        public WriteableBitmap this[int Index]
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
            foreach (WriteableBitmap WB in Images)
                yield return WB;
        }

        /// <summary>
        /// Return the contents of the image list as an array of images.
        /// </summary>
        /// <returns>Array of images. If no items are in the image list, null is returned.</returns>
        public WriteableBitmap[] ToArray()
        {
            if (Count < 1)
                return null;
            WriteableBitmap[] Ary = new WriteableBitmap[Count];
            for (int i = 0; i < Count; i++)
                Ary[i] = Images[i];
            return Ary;
        }
    }
}
