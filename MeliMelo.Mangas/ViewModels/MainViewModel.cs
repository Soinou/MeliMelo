using Caliburn.Micro;
using Castle.Core;
using MeliMelo.Mangas.Models;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public interface IMainViewModelFactory
    {
        MainViewModel Create();

        void Release(MainViewModel view_model);
    }

    public class MainViewModel : Screen, IInitializable
    {
        /// <summary>
        /// Creates a new MainViewModel
        /// </summary>
        public MainViewModel()
        {
            DisplayName = "MeliMelo - Mangas";

            mangas_ = new BindableCollection<MangaViewModel>();
            selected_manga_ = null;
        }

        /// <summary>
        /// Gets/sets the AddMangaViewModel factory
        /// </summary>
        public IAddMangaViewModelFactory AddMangaFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the DeleteMangaViewModel factory
        /// </summary>
        public IDeleteMangaViewModelFactory DeleteMangaFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the MangaViewModel factory
        /// </summary>
        public IMangaViewModelFactory MangaFactory
        {
            get;
            set;
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
        /// Gets/sets the MangasTask
        /// </summary>
        public MangasTask Task
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the WindowManager
        /// </summary>
        public IWindowManager WindowManager
        {
            get;
            set;
        }

        /// <summary>
        /// Adds a new manga to the list of mangas
        /// </summary>
        public void Add()
        {
            var dialog = AddMangaFactory.Create();

            var result = WindowManager.ShowDialog(dialog);

            if (result.HasValue && result.Value)
            {
                Manga manga = new Manga();
                manga.Name = dialog.MangaName;
                manga.Link = dialog.MangaLink;
                Task.Add(manga);
                var view_model = MangaFactory.Create(manga);
                mangas_.Add(view_model);
                selected_manga_ = view_model;
                NotifyOfPropertyChange(() => SelectedManga);
            }

            AddMangaFactory.Release(dialog);
        }

        /// <summary>
        /// Deletes the currently selected manga
        /// </summary>
        public void Delete()
        {
            if (selected_manga_ != null)
            {
                var dialog = DeleteMangaFactory.Create(selected_manga_.Manga);

                var result = WindowManager.ShowDialog(dialog);

                if (result.HasValue && result.Value)
                {
                    Task.Remove(selected_manga_.Manga);
                    mangas_.Remove(selected_manga_);
                    MangaFactory.Release(selected_manga_);
                    if (Task.Mangas.Count > 0)
                    {
                        selected_manga_ = mangas_.First();
                    }
                    else
                    {
                        selected_manga_ = null;
                    }
                    NotifyOfPropertyChange(() => SelectedManga);
                }

                DeleteMangaFactory.Release(dialog);
            }
        }

        /// <summary>
        /// Called when the component is initialized
        /// </summary>
        public void Initialize()
        {
            foreach (Manga manga in Task.Mangas)
                mangas_.Add(MangaFactory.Create(manga));
        }

        /// <summary>
        /// Forces an update of the manga list
        /// </summary>
        public void Update()
        {
            Task.Update();
        }

        /// <summary>
        /// List of manga view models
        /// </summary>
        protected IObservableCollection<MangaViewModel> mangas_;

        /// <summary>
        /// Currently selected manga
        /// </summary>
        protected MangaViewModel selected_manga_;
    }
}
