using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MeliMelo.Common.Utils
{
    /// <summary>
    /// Base class of json converters
    /// </summary>
    /// <typeparam name="T">Type targeted by the converter</typeparam>
    public abstract class JsonConverterBase<T> : JsonConverter
    {
        /// <summary>
        /// If the converter can convert the given type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>If the converter can convert this type</returns>
        public override bool CanConvert(Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }

        /// <summary>
        /// Reads some json and transforms it as an object
        /// </summary>
        /// <param name="reader">Json reader</param>
        /// <param name="type">Type</param>
        /// <param name="value">Value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>A new object</returns>
        public override object ReadJson(JsonReader reader, Type type, object value,
                                        JsonSerializer serializer)
        {
            JObject json = JObject.Load(reader);

            T target = Create(json);

            serializer.Populate(json.CreateReader(), target);

            return target;
        }

        /// <summary>
        /// Writes the given object as a json object
        /// </summary>
        /// <param name="writer">Json writer</param>
        /// <param name="value">Value to write</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Convert(value).WriteTo(writer);
        }

        /// <summary>
        /// Converts the given object into a json object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Json object</returns>
        protected abstract JObject Convert(object value);

        /// <summary>
        /// Creates a new object using the given json object
        /// </summary>
        /// <param name="json">Json object</param>
        /// <returns>A new object</returns>
        protected abstract T Create(JObject json);
    }
}
