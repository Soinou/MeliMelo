using Caliburn.Micro;
using MeliMelo.Mangas.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public interface IMangaViewModelFactory
    {
        MangaViewModel Create(IChapterViewModelFactory factory, Manga manga, MangasTask task);

        void Release(MangaViewModel view_model);
    }

    public class MangaViewModel : Screen
    {
        /// <summary>
        /// Creates a new MangaViewModel
        /// </summary>
        /// <param name="factory">Chapter factory</param>
        /// <param name="manga">Manga to wrap</param>
        /// <param name="task">Mangas task</param>
        public MangaViewModel(IChapterViewModelFactory factory, Manga manga, MangasTask task)
        {
            DisplayName = manga.Name;

            chapters_ = new List<ChapterViewModel>();
            factory_ = factory;
            manga_ = manga;
            manga_.NewChapter += OnMangaNewChapter;
            task_ = task;

            foreach (var chapter in manga_.Chapters)
                chapters_.Add(factory_.Create(manga_, chapter, task_));
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
        public ICollectionView Chapters
        {
            get
            {
                var view = CollectionViewSource.GetDefaultView(chapters_);

                view.SortDescriptions.Add(new SortDescription("Number",
                    ListSortDirection.Descending));
                view.SortDescriptions.Add(new SortDescription("Title",
                    ListSortDirection.Descending));

                return view;
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
                foreach (var chapter in manga_.Chapters)
                    chapter.IsRead = true;

                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => Chapters);

                task_.Save();
            });
        }

        /// <summary>
        /// Called when a chapter is read
        /// </summary>
        protected void OnChapterRead()
        {
            NotifyOfPropertyChange(() => Name);
        }

        /// <summary>
        /// Called when a chapter has been added to the manga
        /// </summary>
        /// <param name="chapter">Added chapter</param>
        protected void OnMangaNewChapter(Chapter chapter)
        {
            chapter.Read += OnChapterRead;
            chapters_.Add(factory_.Create(manga_, chapter, task_));
            NotifyOfPropertyChange(() => Name);
            NotifyOfPropertyChange(() => Chapters);
            NotifyOfPropertyChange(() => CanReadAll);
        }

        /// <summary>
        /// Chapter list
        /// </summary>
        protected List<ChapterViewModel> chapters_;

        /// <summary>
        /// Wrapped manga
        /// </summary>
        protected Manga manga_;

        /// <summary>
        /// Chapter factory
        /// </summary>
        private IChapterViewModelFactory factory_;

        /// <summary>
        /// Mangas task
        /// </summary>
        private MangasTask task_;
    }
}
