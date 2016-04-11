using MeliMelo.Common.Helpers;
using MeliMelo.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeliMelo.Common.Services.Repository
{
    /// <summary>
    /// Implementation of IRepository using a Json formatted file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonRepository<T> : DisposableBase, IRepository<T> where T : class
    {
        public JsonRepository(string path)
        {
            path_ = path;
            items_ = null;

            Load();
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
                if (query(item))
                    return item;

            return null;
        }

        public void Remove(T item)
        {
            items_.Remove(item);
        }

        public void Save()
        {
            IoHelper.Write(path_, JsonConvert.SerializeObject(items_));
        }

        protected void Load()
        {
            items_ = JsonConvert.DeserializeObject<ICollection<T>>(IoHelper.Read(path_));

            if (items_ == null)
                items_ = new LinkedList<T>();
        }

        protected ICollection<T> items_;
        protected string path_;
    }
}
