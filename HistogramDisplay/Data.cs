using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace HistogramDisplay
{
    public partial class HistogramViewer
    {
        /// <summary>
        /// Create the data structure needed to hold histogram data.
        /// </summary>
        private void CreateData ()
        {
            Histogram = new ObservableCollection<HistogramTriplet>();
            Histogram.CollectionChanged += HistogramDataChanged;
        }

        private bool IgnoreChanges = false;

        /// <summary>
        /// Handle changes to the histogram data.
        /// </summary>
        /// <param name="Sender">The histogram data that changed.</param>
        /// <param name="e">Event information.</param>
        private void HistogramDataChanged (object Sender, NotifyCollectionChangedEventArgs e)
        {
            if (Sender == null)
                return;
            if (IgnoreChanges)
                return;
            DrawUI();
        }

        /// <summary>
        /// Get a channel of data from histogram data.
        /// </summary>
        /// <param name="Data">Source histogram data.</param>
        /// <param name="Channel">The channel to extract from <paramref name="Data"/>.</param>
        /// <param name="MaxPercent">The maximum percent found in the extracted channel.</param>
        /// <returns>List of histogram data for the extracted channel.</returns>
        private List<double> GetChannel (ObservableCollection<HistogramTriplet> Data, ChannelNames Channel, out double MaxPercent,
            out int MaxValue)
        {
            List<double> ChannelData = new List<double>();
            MaxPercent = double.MinValue;
            MaxValue = 0;
            foreach (HistogramTriplet Triplet in Data)
            {
                double ChannelValue = 0.0;
                switch (Channel)
                {
                    case ChannelNames.Red:
                        ChannelValue = Triplet.Red;
                        if (Triplet.RawRed > MaxValue)
                            MaxValue = (int)Triplet.RawRed;
                        break;

                    case ChannelNames.Green:
                        ChannelValue = Triplet.Green;
                        if (Triplet.RawGreen > MaxValue)
                            MaxValue = (int)Triplet.RawGreen;
                        break;

                    case ChannelNames.Blue:
                        ChannelValue = Triplet.Blue;
                        if (Triplet.RawBlue > MaxValue)
                            MaxValue = (int)Triplet.RawBlue;
                        break;
                }
                if (ChannelValue > MaxPercent)
                    MaxPercent = ChannelValue;
                ChannelData.Add(ChannelValue);
            }
            return ChannelData;
        }

        /// <summary>
        /// Get a channel of raw data from histogram data.
        /// </summary>
        /// <param name="Data">Source histogram data.</param>
        /// <param name="Channel">The channel to extract from <paramref name="Data"/>.</param>
        /// <param name="MaxPercent">The maximum percent found in the extracted channel.</param>
        /// <returns>List of histogram data for the extracted channel.</returns>
        private List<int> GetRawChannel (ObservableCollection<HistogramTriplet> Data, ChannelNames Channel, out int MaxValue)
        {
            List<int> ChannelData = new List<int>();
            MaxValue = 0;
            foreach (HistogramTriplet Triplet in Data)
            {
                int ChannelValue = 0;
                switch (Channel)
                {
                    case ChannelNames.Red:
                        ChannelValue = (int)Triplet.RawRed;
                        if (Triplet.RawRed > MaxValue)
                            MaxValue = (int)Triplet.RawRed;
                        break;

                    case ChannelNames.Green:
                        ChannelValue = (int)Triplet.RawGreen;
                        if (Triplet.RawGreen > MaxValue)
                            MaxValue = (int)Triplet.RawGreen;
                        break;

                    case ChannelNames.Blue:
                        ChannelValue = (int)Triplet.RawBlue;
                        if (Triplet.RawBlue > MaxValue)
                            MaxValue = (int)Triplet.RawBlue;
                        break;
                }
                ChannelData.Add(ChannelValue);
            }
            return ChannelData;
        }

        /// <summary>
        /// Add a range of data to the histogram.
        /// </summary>
        /// <param name="HData">Histogram triplets to add.</param>
        public void AddRange(List<HistogramTriplet> HData)
        {
            if (HData == null)
                return;
            Histogram.Clear();
            foreach (HistogramTriplet Triplet in HData)
                Histogram.Add(Triplet);
        }

        /// <summary>
        /// Add data to the histogram. Updating is disabled until all data has been added. The control is updated once
        /// all data has been added.
        /// </summary>
        /// <param name="HData">Histogram triplets to add.</param>
        public void BatchAdd(List<HistogramTriplet> HData)
        {
            IgnoreChanges = true;
            AddRange(HData);
            IgnoreChanges = false;
            DrawUI();
        }
    }
}
