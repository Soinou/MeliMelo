using System;
using System.Collections.Generic;

namespace MeliMelo.Common.Services.Repository
{
    /// <summary>
    /// Represents a repository of items
    /// </summary>
    /// <typeparam name="T">Wrapped item</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets the list of items in the database
        /// </summary>
        ICollection<T> Items
        {
            get;
        }

        /// <summary>
        /// Adds an item to the database
        /// </summary>
        /// <param name="item">Item to add</param>
        void Add(T item);

        /// <summary>
        /// Finds an item in the database
        /// </summary>
        /// <param name="query">Query to execute on each</param>
        /// <returns></returns>
        T Find(Func<T, bool> query);

        /// <summary>
        /// Removes an item from the database
        /// </summary>
        /// <param name="item">Item to remove</param>
        void Remove(T item);

        /// <summary>
        /// Saves any changes to the disk
        /// </summary>
        void Save();
    }
}
