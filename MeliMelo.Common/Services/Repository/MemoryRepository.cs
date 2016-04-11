using MeliMelo.Common.Utils;
using System;
using System.Collections.Generic;

namespace MeliMelo.Common.Services.Repository
{
    public class MemoryRepository<T> : DisposableBase, IRepository<T> where T : class
    {
        public MemoryRepository()
        {
            items_ = new List<T>();
        }

        public ICollection<T> Items
        {
            get
            {
                return items_;
            }
        }

        public void Add(T item)
        {
            items_.Add(item);
        }

        public T Find(Func<T, bool> query)
        {
            foreach (T item in items_)
            {
                if (query(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        public void Remove(T item)
        {
            items_.Remove(item);
        }

        public void Save()
        {
            throw new InvalidOperationException("Cannot save a memory repository");
        }

        protected List<T> items_;
    }
}
