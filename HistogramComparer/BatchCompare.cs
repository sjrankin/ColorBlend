using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram
{
    public class BatchCompare
    {
        public BatchCompare ()
        {
        }

        /// <summary>
        /// Return the index of the image in <paramref name="ImageList"/> that is closest
        /// to <paramref name="Target"/>.
        /// </summary>
        /// <param name="Target">The image that will be compared to each image in <paramref name="ImageList"/>.</param>
        /// <param name="ImageList">List of images that will be compared to <paramref name="Target"/>.</param>
        /// <returns>
        /// Index of the image in <paramref name="ImageList"/> that is closest to <paramref name="Target"/> on
        /// success, exception on errors. If more than one image in <paramref name="ImageList"/> has the same
        /// comparison value, the first image in <paramref name="ImageList"/> with the same value will be returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="Target"/> or <paramref name="ImageList"/> is null.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="ImageList"/> is empty.
        /// </exception>
        public int Nearest(RawHistogram Target, List<RawHistogram> ImageList)
        {
            if (Target == null)
                throw new ArgumentNullException("Target");
            if (ImageList == null)
                throw new ArgumentNullException("ImageList");
            if (ImageList.Count < 1)
                throw new IndexOutOfRangeException("No images in image list.");
            if (ImageList.Count == 1)
                return 0;
            return 0;
        }
    }
}
