using Caliburn.Micro;
using MeliMelo.Mangas;
using MeliMelo.Mangas.Core;
using MeliMelo.Utils;
using System;
using System.Threading.Tasks;

namespace MeliMelo.ViewModels
{
    public class MangaViewModel : Screen
    {
        /// <summary>
        /// Creates a new MangaViewModel
        /// </summary>
        /// <param name="task">Mangas task</param>
        /// <param name="manga">Manga to wrap</param>
        public MangaViewModel(MangasTask task, Manga manga)
        {
            DisplayName = manga.Name;

            chapters_ = new BindableCollection<ChapterViewModel>();
            manga_ = manga;
            manga_.NewChapter += OnMangaNewChapter;
            task_ = task;

            foreach (Chapter chapter in manga_.Chapters)
                chapters_.Add(new ChapterViewModel(task_, manga_, chapter));
        }

        /// <summary>
        /// Gets if the read all button is active
        /// </summary>
        public bool CanReadAll
        {
            get
            {
                return manga_.Chapters.Count > 0;
            }
        }

        /// <summary>
        /// Gets the chapter list
        /// </summary>
        public IObservableCollection<ChapterViewModel> Chapters
        {
            get
            {
                return chapters_;
            }
        }

        /// <summary>
        /// Gets the view model manga
        /// </summary>
        public Manga Manga
        {
            get
            {
                return manga_;
            }
        }

        /// <summary>
        /// Gets the manga name
        /// </summary>
        public string Name
        {
            get
            {
                return manga_.Name + (manga_.HasUnread ? " (New Chapters)" : "");
            }
        }

        /// <summary>
        /// Reads all the chapters currently not read
        /// </summary>
        public async void ReadAll()
        {
            await Task.Run(() =>
            {
                foreach (Chapter chapter in manga_.Chapters)
                    chapter.IsRead = true;

                chapters_.Refresh();
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => Chapters);

                task_.Save();
            });
        }

        /// <summary>
        /// Called when a chapter is read
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnChapterRead(object sender, EventArgs e)
        {
            NotifyOfPropertyChange(() => Name);
        }

        /// <summary>
        /// Called when a chapter has been added to the manga
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnMangaNewChapter(object sender, DataEventArgs<Chapter> e)
        {
            e.Data.Read += OnChapterRead;
            chapters_.Insert(0, new ChapterViewModel(task_, manga_, e.Data));
            chapters_.Refresh();
            NotifyOfPropertyChange(() => Name);
            NotifyOfPropertyChange(() => Chapters);
            NotifyOfPropertyChange(() => CanReadAll);
        }

        /// <summary>
        /// Chapter list
        /// </summary>
        protected IObservableCollection<ChapterViewModel> chapters_;

        /// <summary>
        /// Wrapped manga
        /// </summary>
        protected Manga manga_;

        /// <summary>
        /// Manga updater
        /// </summary>
        protected MangasTask task_;
    }
}
