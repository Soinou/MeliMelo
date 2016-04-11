using Newtonsoft.Json.Linq;

namespace MeliMelo.Common.Services.Configuration.Values
{
    /// <summary>
    /// Represents a configuration value
    /// </summary>
    public interface IValue
    {
        /// <summary>
        /// Gets if the value is registered or not
        /// </summary>
        bool IsRegistered
        {
            get;
            set;
        }

        /// <summary>
        /// Deserialize the IValue from the given Json object
        /// </summary>
        /// <param name="json">Json object to read</param>
        /// <returns>The value</returns>
        IValue Deserialize(JObject json);

        /// <summary>
        /// Serializes the IValue in the given Json object
        /// </summary>
        /// <param name="json">Json object</param>
        void Serialize(JObject json);
    }
}
