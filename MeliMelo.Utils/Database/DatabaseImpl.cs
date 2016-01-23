using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MeliMelo.Utils
{
    public class DatabaseImpl<T> : IDatabase<T> where T : class
    {
        public DatabaseImpl(string file_name)
        {
            file_name_ = file_name;
            items_ = null;

            if (!File.Exists(file_name_))
                File.Create(file_name_);

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
            File.WriteAllText(file_name_, JsonConvert.SerializeObject(items_));
        }

        protected void Load()
        {
            items_ = JsonConvert.DeserializeObject<ICollection<T>>(File.ReadAllText(file_name_));

            if (items_ == null)
                items_ = new LinkedList<T>();
        }

        protected string file_name_;

        protected ICollection<T> items_;
    }
}
