using MeliMelo.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace MeliMelo.Core.Configuration.Values
{
    /// <summary>
    /// Represents an integer present in a configuration file
    /// </summary>
    public class IntegerValue : IValue
    {
        /// <summary>
        /// Creates a new Integer
        /// </summary>
        public IntegerValue()
            : this("", 0, 0, 100, 1)
        { }

        /// <summary>
        /// Creates a new Integer
        /// </summary>
        /// <param name="unit">Unit</param>
        /// <param name="value">Value</param>
        /// <param name="minimum">Minimum</param>
        /// <param name="maximum">Maximum</param>
        /// <param name="step">Step</param>
        public IntegerValue(string unit, int value, int minimum, int maximum, int step)
        {
            unit_ = unit;
            value_ = value;
            minimum_ = minimum;
            maximum_ = maximum;
            step_ = step;
        }

        /// <summary>
        /// Gets/sets the maximum value of the integer
        /// 
        /// When setting, if the value is greater than the new maximum, value will be set to the new maximum
        /// </summary>
        public int Maximum
        {
            get
            {
                return maximum_;
            }
            set
            {
                maximum_ = value;
                if (value_ > maximum_)
                    value_ = maximum_;
            }
        }

        /// <summary>
        /// Gets/sets the minimum value of the integer
        /// 
        /// When setting, if the value is less than the new minimum, value will be set to the new minimum
        /// </summary>
        public int Minimum
        {
            get
            {
                return minimum_;
            }
            set
            {
                minimum_ = value;
                if (value_ < minimum_)
                    value_ = minimum_;
            }
        }

        /// <summary>
        /// Gets/sets the step between integer values
        /// 
        /// When setting, if the new step doesn't match the value, value will be set to the minimum
        /// </summary>
        public int Step
        {
            get
            {
                return step_;
            }
            set
            {
                step_ = value;
                if (value_ % step_ != 0)
                    value_ = minimum_;
            }
        }

        /// <summary>
        /// Gets/sets the unit of the integer
        /// </summary>
        public string Unit
        {
            get
            {
                return unit_;
            }
            set
            {
                unit_ = value;
            }
        }

        /// <summary>
        /// Gets/sets the internal value
        /// 
        /// When setting, if the value doesn't match with the parameters, nothing will happen
        /// </summary>
        public int Value
        {
            get
            {
                return value_;
            }
            set
            {
                if (value_ != value && value <= maximum_ && value >= minimum_
                    && value % step_ == 0)
                {
                    value_ = value;
                    if (ValueChanged != null)
                        ValueChanged(null, new DataEventArgs<int>(value_));
                }
            }
        }

        /// <summary>
        /// Deserializes the integer value from the given Json object
        /// </summary>
        /// <param name="json">Json object to deserialize from</param>
        /// <returns>The value</returns>
        public IValue Deserialize(JObject json)
        {
            unit_ = json["Unit"].Value<string>();
            value_ = json["Value"].Value<int>();
            minimum_ = json["Minimum"].Value<int>();
            maximum_ = json["Maximum"].Value<int>();
            step_ = json["Step"].Value<int>();
            return this;
        }

        /// <summary>
        /// Serializes the integer value to the given Json object
        /// </summary>
        /// <param name="json">Json object to serialize to</param>
        public void Serialize(JObject json)
        {
            json["Unit"] = unit_;
            json["Value"] = value_;
            json["Minimum"] = minimum_;
            json["Maximum"] = maximum_;
            json["Step"] = step_;
        }

        /// <summary>
        /// Triggered when the value is changed
        /// </summary>
        public event EventHandler<DataEventArgs<int>> ValueChanged;

        /// <summary>
        /// Maximum value
        /// </summary>
        protected int maximum_;

        /// <summary>
        /// Minimum value
        /// </summary>
        protected int minimum_;

        /// <summary>
        /// Steps between values
        /// </summary>
        protected int step_;

        /// <summary>
        /// Unit of the value
        /// </summary>
        protected string unit_;

        /// <summary>
        /// Internal value
        /// </summary>
        protected int value_;
    }
}
