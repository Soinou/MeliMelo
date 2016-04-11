using Caliburn.Micro;
using MeliMelo.Mangas.Models;
using System.Diagnostics;

namespace MeliMelo.ViewModels
{
    public interface IChapterViewModelFactory
    {
        ChapterViewModel Create(Manga manga, Chapter chapter);

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
        public ChapterViewModel(Manga manga, Chapter chapter)
        {
            chapter_ = chapter;
            chapter_.Read += OnChapterRead;
            manga_ = manga;
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
        /// Manga task
        /// </summary>
        public MangasTask Task
        {
            get;
            set;
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
            Task.Save();
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
    }
}
