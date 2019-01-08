using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Implements a normal value (0.0 to 1.0).
    /// </summary>
    public class Normal
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Normal ()
        {
            RawValue = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RawValue">Initial raw value.</param>
        /// <param name="MaxValue">Maximum raw value.</param>
        public Normal (double RawValue, double MaxValue)
        {
            if (MaxValue < Min)
                throw new ArgumentOutOfRangeException("MaxValue");
            Max = MaxValue;
            this.RawValue = RawValue;
        }

        /// <summary>
        /// Return the minimum normal value.
        /// </summary>
        public double Min
        {
            get
            {
                return 0;
            }
        }

        WriteOnce<double> _Max = new WriteOnce<double>();
        /// <summary>
        /// Get or set the maximum raw value. Can be set only once.
        /// </summary>
        public Nullable<double> Max
        {
            get
            {
                if (!_Max.Written)
                    return null;
                return _Max.Value;
            }
            set
            {
                if (!value.HasValue)
                    return;
                if (_Max.Written)
                    return;
                _Max.Value = value.Value;
            }
        }

        private double _RawValue = 0.0;
        /// <summary>
        /// Get or set the raw value.
        /// </summary>
        public double RawValue
        {
            get
            {
                return _RawValue;
            }
            set
            {
                if (value < Min)
                    throw new ArgumentOutOfRangeException("value less than Min");
                if (value > Max)
                    throw new ArgumentOutOfRangeException("value larger than Max");
                _RawValue = value;
            }
        }

        /// <summary>
        /// Try to set the raw value.
        /// </summary>
        /// <param name="Raw">Raw value.</param>
        /// <returns>True on success, false on error.</returns>
        public bool TrySetRawValue (double Raw)
        {
            if (Raw < Min)
                return false;
            if (Raw > Max)
                return false;
            RawValue = Raw;
            return true;
        }

        /// <summary>
        /// Try to set the normal value. If set without error, <seealso cref="RawValue"/> is updated as well.
        /// </summary>
        /// <param name="RawNormal">Normal value.</param>
        /// <returns>true on success, false on error.</returns>
        public bool TrySetNormal (double RawNormal)
        {
            if (!ValidNormal(RawNormal))
                return false;
            if (!Max.HasValue)
                return false;
            RawValue = Max.Value * RawNormal;
            return true;
        }

        /// <summary>
        /// Get the normalized value.
        /// </summary>
        public double Normalized
        {
            get
            {
                if (!Max.HasValue)
                    return 0.0;
                return (double)RawValue / Max.Value;
            }
        }

        /// <summary>
        /// Determines if <paramref name="RawNormal"/> is a valid normal value (0.0 to 1.0).
        /// </summary>
        /// <param name="RawNormal">Value to test.</param>
        /// <returns>True if <paramref name="RawNormal"/> is valid, false if not.</returns>
        public static bool ValidNormal (double RawNormal)
        {
            if (RawNormal < 0.0)
                return false;
            if (RawNormal > 1.0)
                return false;
            return true;
        }
    }
}
