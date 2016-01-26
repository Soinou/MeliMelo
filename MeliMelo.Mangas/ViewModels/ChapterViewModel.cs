using Caliburn.Micro;
using MeliMelo.Mangas;
using MeliMelo.Mangas.Core;
using System;
using System.Diagnostics;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// </summary>
    public class ChapterViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new ChapterViewModel
        /// </summary>
        /// <param name="task">Mangas task</param>
        /// <param name="manga">Manga</param>
        /// <param name="chapter">Chapter to wrap</param>
        public ChapterViewModel(MangasTask task, Manga manga, Chapter chapter)
        {
            chapter_ = chapter;
            chapter_.Read += OnChapterRead;
            manga_ = manga;
            task_ = task;
        }

        /// <summary>
        /// Returns if the chapter is read or not
        /// </summary>
        public string Read
        {
            get
            {
                return chapter_.IsRead ? "Read" : "Not Read";
            }
        }

        /// <summary>
        /// Gets the chapter title
        /// </summary>
        public string Title
        {
            get
            {
                return chapter_.Title;
            }
        }

        /// <summary>
        /// Opens the chapter link in the browser
        /// </summary>
        public void Open()
        {
            Process.Start(chapter_.Link);
            chapter_.IsRead = true;
            NotifyOfPropertyChange(() => Read);
            task_.Save();
        }

        /// <summary>
        /// Called when the chapter has been read
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnChapterRead(object sender, EventArgs e)
        {
            NotifyOfPropertyChange(() => Read);
        }

        /// <summary>
        /// Wrapper chapter
        /// </summary>
        protected Chapter chapter_;

        /// <summary>
        /// Manga
        /// </summary>
        protected Manga manga_;

        /// <summary>
        /// Mangas task
        /// </summary>
        protected MangasTask task_;
    }
}
