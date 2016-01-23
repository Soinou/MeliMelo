using MeliMelo.Core.Configuration;
using MeliMelo.Core.Configuration.Json;
using MeliMelo.Core.Configuration.Values;
using MeliMelo.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MeliMelo.Impl
{
    /// <summary>
    /// Implementation of the IConfiguration interface
    /// </summary>
    public class ConfigurationManagerImpl : IConfigurationManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file_name">Name of the file holding the configuration</param>
        public ConfigurationManagerImpl(string file_name)
        {
            file_name_ = file_name;
            Load();
        }

        /// <summary>
        /// Gets the enumerator of the configuration file
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<KeyValuePair<string, IValue>> GetEnumerator()
        {
            return values_.GetEnumerator();
        }

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
        public IntegerValue GetInteger(string key, string unit, int default_value, int minimum,
            int maximum, int step)
        {
            return Get(key, () =>
            {
                IntegerValue integer_value = new IntegerValue();
                integer_value.Unit = unit;
                integer_value.Minimum = minimum;
                integer_value.Maximum = maximum;
                integer_value.Step = step;
                integer_value.Value = default_value;
                return integer_value;
            });
        }

        /// <summary>
        /// Retrieves a path from the configuration file
        /// 
        /// If the path does not exists, creates it with the given value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="default_value">Default value</param>
        /// <returns>Path</returns>
        public PathValue GetPath(string key, string default_value)
        {
            return Get(key, () =>
            {
                PathValue path_value = new PathValue();
                path_value.Value = default_value;
                return path_value;
            });
        }

        /// <summary>
        /// Retrieves a string from the configuration file
        /// 
        /// If the string does not exists, creates it with the given value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="default_value">Default value</param>
        /// <returns>String</returns>
        public StringValue GetString(string key, string default_value)
        {
            return Get(key, () =>
            {
                StringValue string_value = new StringValue();
                string_value.Value = default_value;
                return string_value;
            });
        }

        /// <summary>
        /// Gets the enumerator of the configuration file
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return values_.GetEnumerator();
        }

        /// <summary>
        /// Saves the configuration to the file
        /// </summary>
        public void Save()
        {
            IoHelper.Write(file_name_, JsonConvert.SerializeObject(values_,
                new IValueConverter()));
        }

        /// <summary>
        /// Retrieves a value by its key
        /// 
        /// If the value does not exists, creates it using the given function
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="key">Key</param>
        /// <param name="create">Creation function</param>
        /// <returns>Value</returns>
        protected T Get<T>(string key, Func<T> create) where T : class, IValue
        {
            IValue value = default(T);
            T new_value = default(T);

            if (!values_.TryGetValue(key, out value) || (new_value = value as T) == default(T))
                values_.Add(key, new_value = create());

            return new_value;
        }

        /// <summary>
        /// Loads the configuration from the file
        /// </summary>
        protected void Load()
        {
            if (File.Exists(file_name_))
            {
                string data = IoHelper.Read(file_name_);

                values_ = JsonConvert.DeserializeObject<Dictionary<string, IValue>>(data,
                    new IValueConverter());
            }
            else
            {
                values_ = new Dictionary<string, IValue>();

                Save();
            }
        }

        /// <summary>
        /// Name of the file holding the configuration
        /// </summary>
        protected string file_name_;

        /// <summary>
        /// Values dictionary
        /// </summary>
        protected Dictionary<string, IValue> values_;
    }
}
