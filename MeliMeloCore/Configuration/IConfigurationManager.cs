using MeliMelo.Core.Configuration.Values;
using System.Collections.Generic;

namespace MeliMelo.Core.Configuration
{
    /// <summary>
    /// Represents a configuration file
    /// </summary>
    public interface IConfigurationManager : IEnumerable<KeyValuePair<string, IValue>>
    {
        /// <summary>
        /// Retrieves an integer from the configuration file
        /// 
        /// If the integer does not exists, creates it with the given values
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="unit">Unit</param>
        /// <param name="default_value">Default value</param>
        /// <param name="minimum">Minimum</param>
        /// <param name="maximum">Maximum</param>
        /// <param name="step">Step</param>
        /// <returns>The integer</returns>
        IntegerValue GetInteger(string key, string unit, int default_value, int minimum,
            int maximum, int step);

        /// <summary>
        /// Retrieves a path from the configuration file
        /// 
        /// If the path does not exists, creates it with the given value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="default_value">Default value</param>
        /// <returns>Path</returns>
        PathValue GetPath(string key, string default_value);

        /// <summary>
        /// Retrieves a string from the configuration file
        /// 
        /// If the string does not exists, creates it with the given value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="default_value">Default value</param>
        /// <returns>String</returns>
        StringValue GetString(string key, string default_value);

        /// <summary>
        /// Saves the configuration file
        /// </summary>
        void Save();
    }
}
