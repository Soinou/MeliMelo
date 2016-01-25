using MeliMelo.Core.Tasks;
using MeliMelo.Mangas.Core;
using MeliMelo.Utils;
using MeliMelo.Utils.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MeliMelo.Mangas
{
    /// <summary>
    /// Mangas task
    /// </summary>
    public class MangasTask : TimedTask, IDisposable
    {
        /// <summary>
        /// Creates a new MangaUpdaterImpl
        /// </summary>
        public MangasTask()
        {
            log_ = LogManager.Instance.Get("MeliMelo.Mangas");

            database_ = new DatabaseImpl<Manga>(@"..\data\Mangas.db");

            Interval = 3600000;

            disposed_ = false;
        }

        /// <summary>
        /// Gets the list of manga
        /// </summary>
        public ICollection<Manga> Mangas
        {
            get
            {
                return database_.Items;
            }
        }

        /// <summary>
        /// Gets the name of the task
        /// </summary>
        public override string Name
        {
            get
            {
                return "Mangas";
            }
        }

        /// <summary>
        /// Adds the given manga to the list
        /// </summary>
        /// <param name="manga">Manga to add</param>
        public void Add(Manga manga)
        {
            database_.Add(manga);
            Save();
        }

        /// <summary>
        /// Removes the given manga from the list
        /// </summary>
        /// <param name="manga">Manga to remove</param>
        public void Remove(Manga manga)
        {
            database_.Remove(manga);
            Save();
        }

        public override void Run()
        {
            UpdateAll();
        }

        /// <summary>
        /// Saves any changes
        /// </summary>
        public void Save()
        {
            database_.Save();
        }

        /// <summary>
        /// Updates the manga list
        /// </summary>
        public void Update()
        {
            UpdateAll();
        }

        /// <summary>
        /// Triggered when at least one manga has received an update
        /// </summary>
        public event EventHandler<DataEventArgs<uint>> MangaUpdated;

        /// <summary>
        /// Disposes of the resource
        /// </summary>
        /// <param name="disposing">If we should dispose of the managed resources</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposed_)
            {
                if (disposing)
                {
                    database_.Save();
                }

                disposed_ = true;
            }
        }

        protected override void OnStart()
        {
            UpdateAll();
        }

        /// <summary>
        /// Updates the given manga
        /// </summary>
        /// <param name="manga">Manga to update</param>
        /// <returns>If the manga was updated or not</returns>
        protected async Task Update(Manga manga)
        {
            try
            {
                string data = await HttpHelper.Get(manga.Link);

                if (!string.IsNullOrEmpty(data))
                {
                    var document = XDocument.Parse(data);

                    var items = document.Root.Element("channel").Elements("item");

                    int diff = items.Count() - manga.Chapters.Count;

                    if (diff > 0)
                    {
                        foreach (var item in items.Take(diff).Reverse())
                        {
                            string title = item.Element("title").Value;
                            string link = item.Element("link").Value;
                            string description = item.Element("description").Value;

                            Chapter chapter = new Chapter();

                            chapter.Title = title;
                            chapter.Link = link;

                            manga.Add(chapter);
                        }
                    }
                }

                log_.Info("Update", "Successfully updated the manga \"" + manga.Name + "\"");
            }
            catch (Exception e)
            {
                log_.Error("Update", "Could not update the manga \"" + manga.Name + "\": "
                    + e.Message);
            }
        }

        /// <summary>
        /// Updates all the mangas
        /// </summary>
        protected async void UpdateAll()
        {
            uint count = 0;

            foreach (Manga manga in database_.Items)
            {
                await Update(manga);

                count += (uint)manga.Chapters.Count(chapter => !chapter.IsRead);
            }

            if (count > 0 && MangaUpdated != null)
                MangaUpdated(this, new DataEventArgs<uint>(count));

            database_.Save();
        }

        /// <summary>
        /// Database
        /// </summary>
        protected IDatabase<Manga> database_;

        /// <summary>
        /// If the resource has already been disposed of
        /// </summary>
        protected bool disposed_;

        /// <summary>
        /// Logger
        /// </summary>
        protected ILog log_;
    }
}
