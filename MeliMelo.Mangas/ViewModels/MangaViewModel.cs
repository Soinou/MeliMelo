using Caliburn.Micro;
using Castle.Core;
using MeliMelo.Mangas.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public interface IMangaViewModelFactory
    {
        MangaViewModel Create(Manga manga);

        void Release(MangaViewModel view_model);
    }

    public class MangaViewModel : Screen, IInitializable
    {
        /// <summary>
        /// Creates a new MangaViewModel
        /// </summary>
        /// <param name="manga">Manga to wrap</param>
        public MangaViewModel(Manga manga)
        {
            DisplayName = manga.Name;

            chapters_ = new List<ChapterViewModel>();
            manga_ = manga;
            manga_.NewChapter += OnMangaNewChapter;
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

        public IChapterViewModelFactory ChapterFactory
        {
            get;
            set;
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
        /// Manga task
        /// </summary>
        public MangasTask Task
        {
            get;
            set;
        }

        public void Initialize()
        {
            foreach (var chapter in manga_.Chapters)
                chapters_.Add(ChapterFactory.Create(manga_, chapter));
        }

        /// <summary>
        /// Reads all the chapters currently not read
        /// </summary>
        public async void ReadAll()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                foreach (var chapter in manga_.Chapters)
                    chapter.IsRead = true;

                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => Chapters);

                Task.Save();
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
            chapters_.Add(ChapterFactory.Create(manga_, chapter));
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
    }
}
