using Caliburn.Micro;
using MeliMelo.Mangas;
using MeliMelo.Mangas.Core;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace MeliMelo.ViewModels
{
    public class MangasViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new MangasViewModel
        /// </summary>
        /// <param name="window_manager">Window manager</param>
        /// <param name="task">Mangas task</param>
        public MangasViewModel(IWindowManager window_manager, MangasTask task)
        {
            task_ = task;
            mangas_ = new BindableCollection<MangaViewModel>();
            selected_manga_ = null;
            window_manager_ = window_manager;

            foreach (Manga manga in task_.Mangas)
                mangas_.Add(new MangaViewModel(task_, manga));
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
            AddMangaViewModel dialog = new AddMangaViewModel();

            var result = window_manager_.ShowDialog(dialog);

            if (result.HasValue && result.Value)
            {
                Manga manga = new Manga();
                manga.Name = dialog.MangaName;
                manga.Link = dialog.MangaLink;
                task_.Add(manga);
                var view_model = new MangaViewModel(task_, manga);
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
            if (selected_manga_ != null)
            {
                DeleteMangaViewModel dialog = new DeleteMangaViewModel(selected_manga_.Manga);

                var result = window_manager_.ShowDialog(dialog);

                if (result.HasValue && result.Value)
                {
                    task_.Remove(selected_manga_.Manga);
                    mangas_.Remove(selected_manga_);
                    if (task_.Mangas.Count > 0)
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
        /// Manga updater
        /// </summary>
        protected MangasTask task_;

        /// <summary>
        /// Window manager
        /// </summary>
        protected IWindowManager window_manager_;
    }
}
