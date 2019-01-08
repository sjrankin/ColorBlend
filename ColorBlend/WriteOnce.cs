using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    /// <summary>
    /// Implements a container class that allows callers write a value only once but retrieve it as many times as desired.
    /// </summary>
    /// <typeparam name="T">Type of value stored in class.</typeparam>
    public class WriteOnce<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WriteOnce ()
        {
            Written = false;
            IgnoreSameValueWrites = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="IgnoreSameValueWrites">Sets the "can-write-same-value-without-error" flag.</param>
        public WriteOnce (bool IgnoreSameValueWrites)
        {
            Written = false;
            this.IgnoreSameValueWrites = IgnoreSameValueWrites;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="OnceValue">Value of the container.</param>
        /// <param name="IgnoreSameValueWrites">Sets the "can-write-same-value-without-error" flag.</param>
        public WriteOnce (T OnceValue, bool IgnoreSameValueWrites)
        {
            Written = false;
            this.IgnoreSameValueWrites = IgnoreSameValueWrites;
            Value = OnceValue;
        }

        /// <summary>
        /// Get the written once state.
        /// </summary>
        public bool Written { get; private set; }

        /// <summary>
        /// Set to true to allow identical values to be written without exception.
        /// </summary>
        public bool IgnoreSameValueWrites { get; set; }

        private T _Value = default(T);
        /// <summary>
        /// Get or set (only once) the value. Once set, attempting to set again will generate an exception.
        /// </summary>
        /// <exception cref="WriteOnceException{T}">
        /// Thrown if a value is set more than one time.
        /// </exception>
        public T Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (IgnoreSameValueWrites)
                {
                    if (_Value.Equals(value))
                        return;
                }
                if (Written)
                    throw new WriteOnceException<T>(_Value, value, "Attempted to write more than once.");
                _Value = value;
                Written = true;
            }
        }

        /// <summary>
        /// Attempts to set the value <paramref name="ValueToWrite"/>.
        /// </summary>
        /// <param name="ValueToWrite">Value to attempt to write.</param>
        /// <returns>true on success, false if already written.</returns>
        public bool TryWrite (T ValueToWrite)
        {
            if (IgnoreSameValueWrites)
                if (Value.Equals(ValueToWrite))
                    return true;
            if (Written)
                return false;
            Value = ValueToWrite;
            return true;
        }
    }

    /// <summary>
    /// Exception for WriteOnce already written exceptions.
    /// </summary>
    /// <typeparam name="T">The type of value contained in WriteOnce.</typeparam>
    public class WriteOnceException<T> : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WriteOnceException () : base()
        {
            WrittenValue = default(T);
            AttemptedValue = default(T);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Message">Error message.</param>
        /// <param name="Inner">Inner exception.</param>
        public WriteOnceException (string Message, Exception Inner) : base(Message, Inner)
        {
            WrittenValue = default(T);
            AttemptedValue = default(T);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="WrittenValue">Current value.</param>
        /// <param name="AttemptedValue">The value that was attempted to be written.</param>
        public WriteOnceException (T WrittenValue, T AttemptedValue) : base()
        {
            this.WrittenValue = WrittenValue;
            this.AttemptedValue = AttemptedValue;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="WrittenValue">Current value.</param>
        /// <param name="AttemptedValue">The value that was attempted to be written.</param>
        /// <param name="Message">Error message.</param>
        public WriteOnceException (T WrittenValue, T AttemptedValue, string Message) : base(Message)
        {
            this.WrittenValue = WrittenValue;
            this.AttemptedValue = AttemptedValue;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="WrittenValue">Current value.</param>
        /// <param name="AttemptedValue">The value that was attempted to be written.</param>
        /// <param name="Message">Error message.</param>
        /// <param name="Inner">Inner exception.</param>
        public WriteOnceException (T WrittenValue, T AttemptedValue, string Message, Exception Inner) : base(Message, Inner)
        {
            this.WrittenValue = WrittenValue;
            this.AttemptedValue = AttemptedValue;
        }

        /// <summary>
        /// The current, existing value in WriteOnce.
        /// </summary>
        public T WrittenValue { get; internal set; }

        /// <summary>
        /// The value that was attempted to be written.
        /// </summary>
        public T AttemptedValue { get; internal set; }
    }
}
