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
            window_manager_ = window_manager;
            manga_updater_ = task;
            mangas_ = CollectionViewSource.GetDefaultView(manga_updater_.Mangas);
            mangas_.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            selected_manga_ = null;
            manga_ = null;
        }

        /// <summary>
        /// Gets the shown manga view model
        /// </summary>
        public MangaViewModel Manga
        {
            get
            {
                return manga_;
            }
        }

        /// <summary>
        /// Gets the list of mangas
        /// </summary>
        public ICollectionView Mangas
        {
            get
            {
                return mangas_;
            }
        }

        /// <summary>
        /// Gets/sets the currently selected manga
        /// </summary>
        public Manga SelectedManga
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
                    if (selected_manga_ == null)
                        manga_ = null;
                    else
                        manga_ = new MangaViewModel(manga_updater_, selected_manga_);
                    NotifyOfPropertyChange(() => SelectedManga);
                    NotifyOfPropertyChange(() => Manga);
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
                manga_updater_.Add(manga);
                mangas_.Refresh();
                selected_manga_ = manga;
                manga_ = new MangaViewModel(manga_updater_, selected_manga_);
                NotifyOfPropertyChange(() => SelectedManga);
                NotifyOfPropertyChange(() => Manga);
            }
        }

        /// <summary>
        /// Deletes the currently selected manga
        /// </summary>
        public void Delete()
        {
            if (selected_manga_ != null)
            {
                DeleteMangaViewModel dialog = new DeleteMangaViewModel(selected_manga_);

                var result = window_manager_.ShowDialog(dialog);

                if (result.HasValue && result.Value)
                {
                    manga_updater_.Remove(selected_manga_);
                    if (manga_updater_.Mangas.Count > 0)
                    {
                        selected_manga_ = manga_updater_.Mangas.First();
                        manga_ = new MangaViewModel(manga_updater_, selected_manga_);
                    }
                    else
                    {
                        selected_manga_ = null;
                        manga_ = null;
                    }
                    NotifyOfPropertyChange(() => SelectedManga);
                    NotifyOfPropertyChange(() => Manga);
                }
            }
        }

        /// <summary>
        /// Forces an update of the manga list
        /// </summary>
        public void Update()
        {
            manga_updater_.Update();
        }

        /// <summary>
        /// Current manga view model
        /// </summary>
        protected MangaViewModel manga_;

        /// <summary>
        /// Manga updater
        /// </summary>
        protected MangasTask manga_updater_;

        /// <summary>
        /// Observable manga list
        /// </summary>
        protected ICollectionView mangas_;

        /// <summary>
        /// Currently selected manga
        /// </summary>
        protected Manga selected_manga_;

        /// <summary>
        /// Window manager
        /// </summary>
        protected IWindowManager window_manager_;
    }
}
