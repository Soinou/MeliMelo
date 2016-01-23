using MeliMelo.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace MeliMelo.Core.Configuration.Values
{
    /// <summary>
    /// Represents a string value
    /// </summary>
    public class StringValue : IValue
    {
        /// <summary>
        /// Creates a new StringValue
        /// </summary>
        public StringValue()
            : this("")
        { }

        /// <summary>
        /// Creates a new StringValue
        /// </summary>
        /// <param name="value">Value of the path</param>
        public StringValue(string value)
        {
            value_ = value;
        }

        /// <summary>
        /// Gets/sets the value of this string
        /// </summary>
        public string Value
        {
            get
            {
                return value_;
            }
            set
            {
                if (value_ != value)
                {
                    value_ = value;
                    if (ValueChange != null)
                        ValueChange(this, new DataEventArgs<string>(value_));
                }
            }
        }

        /// <summary>
        /// Deserializes the string value from the given Json object
        /// </summary>
        /// <param name="json">Json object to deserialize from</param>
        /// <returns>The value</returns>
        public IValue Deserialize(JObject json)
        {
            value_ = json["String"].Value<string>();
            return this;
        }

        /// <summary>
        /// Serializes the string value to the given Json object
        /// </summary>
        /// <param name="json">Json object to serialize to</param>
        public void Serialize(JObject json)
        {
            json["String"] = value_;
        }

        /// <summary>
        /// Triggered when the value is changed
        /// </summary>
        public event EventHandler<DataEventArgs<string>> ValueChange;

        /// <summary>
        /// Value of this string
        /// </summary>
        protected string value_;
    }
}
