using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    public class OptionalValue
    {
        public OptionalValue ()
        {
            Name = null;
            Value = null;
            ValueType = typeof(object);
        }

        public OptionalValue(string Name, string Value, Type ValueType)
        {
            this.Name = Name;
            this.Value = Value;
            this.ValueType = ValueType;
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public Type ValueType { get; set; }
    }
}
