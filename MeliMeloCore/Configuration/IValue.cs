using Newtonsoft.Json.Linq;

namespace MeliMelo.Core.Configuration
{
    /// <summary>
    /// Represents a configuration value
    /// </summary>
    public interface IValue
    {
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
