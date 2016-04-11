using MeliMelo.Common.Helpers;
using MeliMelo.Common.Services.Configuration.Json;
using MeliMelo.Common.Services.Configuration.Values;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MeliMelo.Common.Services.Configuration
{
    /// <summary>
    /// Implementation of the IConfiguration interface
    /// </summary>
    public class ConfigurationImpl : IConfiguration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path to the configuration file</param>
        public ConfigurationImpl(string path)
        {
            path_ = path;
            Load();
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, IValue>> GetEnumerator()
        {
            return values_.GetEnumerator();
        }

        /// <inheritdoc />
        public IntegerValue GetInteger(string key)
        {
            return Get<IntegerValue>(key);
        }

        /// <inheritdoc />
        public PathValue GetPath(string key)
        {
            return Get<PathValue>(key);
        }

        /// <inheritdoc />
        public StringValue GetString(string key)
        {
            return Get<StringValue>(key);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return values_.GetEnumerator();
        }

        /// <inheritdoc />
        public void RegisterInteger(string key, string unit, int default_value, int minimum,
                                            int maximum, int step)
        {
            Register(key, () =>
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

        /// <inheritdoc />
        public void RegisterPath(string key, string default_value)
        {
            Register(key, () =>
            {
                PathValue path_value = new PathValue();
                path_value.Value = default_value;
                return path_value;
            });
        }

        /// <inheritdoc />
        public void RegisterString(string key, string default_value)
        {
            Register(key, () =>
            {
                StringValue string_value = new StringValue();
                string_value.Value = default_value;
                return string_value;
            });
        }

        /// <inheritdoc />
        public void Save()
        {
            IoHelper.Write(path_, JsonConvert.SerializeObject(values_,
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
        protected T Get<T>(string key) where T : class, IValue
        {
            IValue value = default(T);

            if (!values_.TryGetValue(key, out value))
                throw new ValueNotFoundException(key);

            T new_value = value as T;

            if (new_value == default(T))
                throw new IncorrectValueTypeException(key, typeof(T), value.GetType());

            if (!new_value.IsRegistered)
                throw new ValueNotRegisteredException(key);

            return new_value;
        }

        /// <summary>
        /// Loads the configuration from the file
        /// </summary>
        protected void Load()
        {
            if (File.Exists(path_))
            {
                string data = IoHelper.Read(path_);

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
        /// Registers a value
        /// 
        /// Will create it if it doesn't exist, and will mark the value as registered
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="create">How to create the key</param>
        protected void Register<T>(string key, Func<T> create) where T : class, IValue
        {
            IValue value = default(T);
            T new_value = default(T);

            if (!values_.TryGetValue(key, out value) || (new_value = value as T) == default(T))
                values_.Add(key, new_value = create());

            new_value.IsRegistered = true;
        }

        /// <summary>
        /// Path to the configuration file
        /// </summary>
        protected string path_;

        /// <summary>
        /// Values dictionary
        /// </summary>
        protected Dictionary<string, IValue> values_;
    }

    /// <summary>
    /// Thrown when a value has incorrect type
    /// </summary>
    public class IncorrectValueTypeException : Exception
    {
        /// <summary>
        /// Creates a new IncorrectValueTypeException
        /// </summary>
        /// <param name="key">Value key</param>
        /// <param name="asked">Asked type</param>
        /// <param name="got">Type got</param>
        public IncorrectValueTypeException(string key, Type asked, Type got)
            : base("Value with key \"" + key + "\" has incorrect type. Asked for \"" + asked.Name
                  + "\". Got \"" + got.Name + "\"")
        { }
    }

    /// <summary>
    /// Thrown when a value is not found
    /// </summary>
    public class ValueNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new ValueNotFoundException
        /// </summary>
        /// <param name="key">Value key</param>
        public ValueNotFoundException(string key)
            : base("Value with key \"" + key + "\" was not found in the configuration file")
        { }
    }

    /// <summary>
    /// Thrown when a value is not registered yet
    /// </summary>
    public class ValueNotRegisteredException : Exception
    {
        /// <summary>
        /// Creates a new ValueNotRegisteredException
        /// </summary>
        /// <param name="message">Value key</param>
        public ValueNotRegisteredException(string key)
            : base("Value with key \"" + key + "\" was not registered before using it")
        { }
    }
}
