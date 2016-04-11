using Castle.Core;
using MeliMelo.Common.Helpers;
using MeliMelo.Common.Services.Logging;
using MeliMelo.Common.Utils;
using MeliMelo.Mangas.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MeliMelo.Mangas.Models
{
    /// <summary>
    /// Mangas task
    /// </summary>
    public class MangasTask : TimedTask, IInitializable
    {
        /// <summary>
        /// Creates a new MangaUpdaterImpl
        /// </summary>
        public MangasTask()
        {
            count_ = 0;
            Interval = kNotificationUpdateInterval;
            parser_ = new MangaParser();
        }

        public ILogManager LogManager
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of manga
        /// </summary>
        public ICollection<Manga> Mangas
        {
            get
            {
                return Repository.Items;
            }
        }

        /// <summary>
        /// Manga repository
        /// </summary>
        public MangaRepository Repository
        {
            get;
            set;
        }

        /// <summary>
        /// Adds the given manga to the list
        /// </summary>
        /// <param name="manga">Manga to add</param>
        public void Add(Manga manga)
        {
            Repository.Add(manga);
            Save();
        }

        /// <inheritdoc />
        public void Initialize()
        {
            log_ = LogManager.Get("MeliMelo.Mangas");
        }

        /// <summary>
        /// Removes the given manga from the list
        /// </summary>
        /// <param name="manga">Manga to remove</param>
        public void Remove(Manga manga)
        {
            Repository.Remove(manga);
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
            Repository.Save();
        }

        /// <summary>
        /// Updates the manga list
        /// </summary>
        public void Update()
        {
            UpdateAll(true);
        }

        /// <summary>
        /// Triggered when at least one manga has received an update
        /// </summary>
        public event EventHandler<DataEventArgs<uint>> MangaUpdated;

        /// <inheritdoc />
        protected override void OnStart()
        {
            UpdateAll(true);
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
        }

        /// <summary>
        /// Updates all the mangas
        /// </summary>
        /// <param name="force">If we should force to update everything</param>
        protected async void UpdateAll(bool force = false)
        {
            uint new_chapters = 0;
            count_ -= kNotificationUpdateInterval;

            foreach (Manga manga in Repository.Items)
            {
                if (count_ <= 0 || force)
                    await UpdateManga(manga);

                new_chapters += (uint)manga.Chapters.Count(chapter => !chapter.IsRead);
            }

            if (new_chapters > 0 && MangaUpdated != null)
                MangaUpdated(this, new DataEventArgs<uint>(new_chapters));

            if (count_ <= 0)
                count_ = kFeedUpdateInterval;

            Repository.Save();
        }

        /// <summary>
        /// Updates the given manga
        /// </summary>
        /// <param name="manga">Manga to update</param>
        /// <returns>If the manga was updated or not</returns>
        protected async Task UpdateManga(Manga manga)
        {
            try
            {
                string data = await HttpHelper.Get(manga.Link);

                if (!string.IsNullOrEmpty(data))
                {
                    var document = XDocument.Parse(data);

                    var items = document.Root.Element("channel").Elements("item");

                    foreach (var item in items)
                    {
                        string description_item = item.Element("description").Value;
                        string link_item = item.Element("link").Value;
                        string title_item = item.Element("title").Value;

                        var description = parser_.ParseDescription(description_item);
                        float? number = parser_.ParseChapterNumber(title_item);

                        // We assume the link never changes (Since we don't have an uuid or
                        // something like that. Thanks MangaFox)
                        var chapter = manga.Chapters.FirstOrDefault(c => c.Link == link_item);

                        // Create a new chapter
                        if (chapter == null)
                        {
                            chapter = new Chapter();

                            chapter.Description = description.Item1;
                            chapter.Team = description.Item2;
                            chapter.Link = link_item;
                            chapter.Number = number;
                            chapter.Title = title_item;

                            manga.Add(chapter);
                        }
                        // Update data
                        else
                        {
                            chapter.Description = description.Item1;
                            chapter.Team = description.Item2;
                            chapter.Number = number;
                            chapter.Title = title_item;
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
        /// Interval between two feed updates (15 minutes)
        /// </summary>
        protected const int kFeedUpdateInterval = 900000;

        /// <summary>
        /// Interval between two notification updates (5 minutes)
        /// </summary>
        protected const int kNotificationUpdateInterval = 300000;

        /// <summary>
        /// Count before the next feed update
        /// </summary>
        protected uint count_;

        /// <summary>
        /// Logger
        /// </summary>
        protected ILog log_;

        /// <summary>
        /// Manga parser (Only parses chapter numbers for now)
        /// </summary>
        protected MangaParser parser_;
    }
}
