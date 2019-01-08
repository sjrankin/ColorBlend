using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistogramDisplay
{
    /// <summary>
    /// Encapsulates one RGB triplet for histogram usage.
    /// </summary>
    public class HistogramTriplet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HistogramTriplet()
        {
            Red = 0.0;
            Green = 0.0;
            Blue = 0.0;
            RawRed = 0;
            RawGreen = 0;
            RawBlue = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Red">Red value.</param>
        /// <param name="Green">Green value.</param>
        /// <param name="Blue">Blue value.</param>
        public HistogramTriplet(double Red, double Green, double Blue)
        {
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
            RawRed = 0;
            RawGreen = 0;
            RawBlue = 0;
        }

        /// <summary>
        /// Get or set the red value.
        /// </summary>
        public double Red { get; set; }

        /// <summary>
        /// Get the string representation of the red percent.
        /// </summary>
        public string RedPercent
        {
            get
            {
                return (Red * 100).ToString("n4");
            }
        }

        /// <summary>
        /// Get or set the raw red value.
        /// </summary>
        public UInt32 RawRed { get; set; }

        /// <summary>
        /// Get or set the green value.
        /// </summary>
        public double Green { get; set; }

        /// <summary>
        /// Get the string representation of the green percent.
        /// </summary>
        public string GreenPercent
        {
            get
            {
                return (Green * 100).ToString("n4");
            }
        }

        /// <summary>
        /// Get or set the raw green value.
        /// </summary>
        public UInt32 RawGreen { get; set; }

        /// <summary>
        /// Get or set the blue value.
        /// </summary>
        public double Blue { get; set; }

        /// <summary>
        /// Get the string representation of the blue percent.
        /// </summary>
        public string BluePercent
        {
            get
            {
                return (Blue * 100).ToString("n4");
            }
        }

        /// <summary>
        /// Get or set the raw blue value.
        /// </summary>
        public UInt32 RawBlue { get; set; }

        /// <summary>
        /// Return the sum of <seealso cref="Red"/>, <seealso cref="Green"/>, and <seealso cref="Blue"/>.
        /// </summary>
        public double Sum
        {
            get
            {
                return Red + Green + Blue;
            }
        }

        /// <summary>
        /// Return the sum of <seealso cref="RawRed"/>, <seealso cref="RawGreen"/>, and <seealso cref="RawBlue"/>.
        /// </summary>
        public UInt32 RawSum
        {
            get
            {
                return RawRed + RawGreen + RawBlue;
            }
        }

        /// <summary>
        /// Return the specified channel information.
        /// </summary>
        /// <param name="WhichChannel">Determines the channel to return.</param>
        /// <returns>Channel data. NAN if unknown channel.</returns>
        public double GetChannel(HistogramChannels WhichChannel)
        {
            switch(WhichChannel)
            {
                case HistogramChannels.Red:
                    return Red;

                case HistogramChannels.Green:
                    return Green;

                case HistogramChannels.Blue:
                    return Blue;

                case HistogramChannels.Gray:
                    return Sum;

                default:
                    return double.NaN;
            }
        }
    }

    /// <summary>
    /// Histogram channels.
    /// </summary>
    public enum HistogramChannels
    {
        /// <summary>
        /// Red channel.
        /// </summary>
        Red,
        /// <summary>
        /// Green channel.
        /// </summary>
        Green,
        /// <summary>
        /// Blue channel.
        /// </summary>
        Blue,
        /// <summary>
        /// Combined red, green, and blue.
        /// </summary>
        Gray
    }
}
