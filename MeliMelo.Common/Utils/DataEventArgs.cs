using System;

namespace MeliMelo.Common.Utils
{
    /// <summary>
    /// Represents an event args porting some kind of data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Data of the event args</param>
        public DataEventArgs(T data)
        {
            data_ = data;
        }

        /// <summary>
        /// Gets/sets the data of the event args
        /// </summary>
        public T Data
        {
            get
            {
                return data_;
            }
        }

        /// <summary>
        /// Data of the event args
        /// </summary>
        protected T data_;
    }
}
