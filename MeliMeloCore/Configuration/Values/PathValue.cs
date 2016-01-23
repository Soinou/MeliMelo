using MeliMelo.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace MeliMelo.Core.Configuration.Values
{
    /// <summary>
    /// Represents a system path value
    /// </summary>
    public class PathValue : IValue
    {
        /// <summary>
        /// Creates a new PathValue
        /// </summary>
        public PathValue()
            : this("")
        { }

        /// <summary>
        /// Creates a new PathValue
        /// </summary>
        /// <param name="value">Value of the path</param>
        public PathValue(string value)
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
        /// Deserializes the path value from the given Json object
        /// </summary>
        /// <param name="json">Json object to deserialize from</param>
        /// <returns>The value</returns>
        public IValue Deserialize(JObject json)
        {
            value_ = json["Path"].Value<string>();
            return this;
        }

        /// <summary>
        /// Serializes the integer value to the given Json object
        /// </summary>
        /// <param name="json">Json object to serialize to</param>
        public void Serialize(JObject json)
        {
            json["Path"] = value_;
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
