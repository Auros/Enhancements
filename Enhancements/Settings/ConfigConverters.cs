using IPA.Config.Data;
using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancements.Settings
{
    public class ConfigConverters
    {
        public sealed class EnumConverter<T> : ValueConverter<T>
        where T : Enum
        {
            public override T FromValue(Value value, object parent)
                => value is Text t
                    ? (T)Enum.Parse(typeof(T), t.Value)
                    : throw new ArgumentException("Value not a string", nameof(value));

            public override Value ToValue(T obj, object parent)
                => Value.Text(obj.ToString());
        }
    }
}
