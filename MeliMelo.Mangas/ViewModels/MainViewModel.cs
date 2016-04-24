using Caliburn.Micro;
using MeliMelo.Mangas.Models;
using MeliMelo.Mangas.Utils;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public class MainViewModel : Screen
    {
        /// <summary>
        /// Creates a new MainViewModel
        /// </summary>
        public MainViewModel(IChapterViewModelFactory chapter_factory, DialogManager dialog, MangasTask task,
            IMangaViewModelFactory manga_factory)
        {
            DisplayName = "MeliMelo - Mangas";

            chapter_factory_ = chapter_factory;
            dialog_ = dialog;
            manga_factory_ = manga_factory;
            mangas_ = new BindableCollection<MangaViewModel>();
            selected_manga_ = null;
            task_ = task;
        }

        /// <summary>
        /// Gets the list of mangas
        /// </summary>
        public ICollectionView Mangas
        {
            get
            {
                var mangas = CollectionViewSource.GetDefaultView(mangas_);

                mangas.SortDescriptions.Add(new SortDescription("Name",
                    ListSortDirection.Ascending));

                return mangas;
            }
        }

        /// <summary>
        /// Gets/sets the currently selected manga
        /// </summary>
        public MangaViewModel SelectedManga
        {
            get
            {
                return selected_manga_;
            }
            set
            {
                if (selected_manga_ != value)
                {
                    selected_manga_ = value;
                    NotifyOfPropertyChange(() => SelectedManga);
                }
            }
        }

        /// <summary>
        /// Adds a new manga to the list of mangas
        /// </summary>
        public void Add()
        {
            var manga = dialog_.AddManga();

            if (manga != null)
            {
                task_.Add(manga);
                var view_model = manga_factory_.Create(chapter_factory_, manga, task_);
                mangas_.Add(view_model);
                selected_manga_ = view_model;
                NotifyOfPropertyChange(() => SelectedManga);
            }
        }

        /// <summary>
        /// Deletes the currently selected manga
        /// </summary>
        public void Delete()
        {
            if (selected_manga_ != null && dialog_.DeleteManga(selected_manga_.Manga))
            {
                task_.Remove(selected_manga_.Manga);
                mangas_.Remove(selected_manga_);
                manga_factory_.Release(selected_manga_);
                if (task_.Mangas.Count() > 0)
                {
                    selected_manga_ = mangas_.First();
                }
                else
                {
                    selected_manga_ = null;
                }
                NotifyOfPropertyChange(() => SelectedManga);
            }
        }

        /// <summary>
        /// Called when the component is initialized
        /// </summary>
        public void Initialize()
        {
            foreach (Manga manga in task_.Mangas)
                mangas_.Add(manga_factory_.Create(chapter_factory_, manga, task_));
        }

        /// <summary>
        /// Forces an update of the manga list
        /// </summary>
        public void Update()
        {
            task_.Update();
        }

        /// <summary>
        /// List of manga view models
        /// </summary>
        protected IObservableCollection<MangaViewModel> mangas_;

        /// <summary>
        /// Currently selected manga
        /// </summary>
        protected MangaViewModel selected_manga_;

        /// <summary>
        /// Chapter factory
        /// </summary>
        private IChapterViewModelFactory chapter_factory_;

        /// <summary>
        /// Dialog manager
        /// </summary>
        private DialogManager dialog_;

        /// <summary>
        /// Manga view model factory
        /// </summary>
        private IMangaViewModelFactory manga_factory_;

        /// <summary>
        /// Manga updater task
        /// </summary>
        private MangasTask task_;
    }
}
