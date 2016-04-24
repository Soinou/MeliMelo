using Caliburn.Micro;
using MeliMelo.Mangas.Models;
using System.Diagnostics;

namespace MeliMelo.ViewModels
{
    public interface IChapterViewModelFactory
    {
        ChapterViewModel Create(Manga manga, Chapter chapter, MangasTask task);

        void Release(ChapterViewModel view_model);
    }

    /// <summary>
    /// </summary>
    public class ChapterViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new ChapterViewModel
        /// </summary>
        /// <param name="manga">Manga</param>
        /// <param name="chapter">Chapter to wrap</param>
        /// <param name="task">Mangas task</param>
        public ChapterViewModel(Manga manga, Chapter chapter, MangasTask task)
        {
            chapter_ = chapter;
            chapter_.Read += OnChapterRead;
            manga_ = manga;
            task_ = task;
        }

        /// <summary>
        /// Gets the chapter number
        /// </summary>
        public float? Number
        {
            get
            {
                return chapter_.Number;
            }
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
                return chapter_.FormatTitle();
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
        protected void OnChapterRead()
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
        private MangasTask task_;
    }
}
