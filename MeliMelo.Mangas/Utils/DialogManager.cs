using Caliburn.Micro;
using MeliMelo.Mangas.Models;
using MeliMelo.ViewModels;

namespace MeliMelo.Mangas.Utils
{
    /// <summary>
    /// Dialog manager
    /// </summary>
    public class DialogManager
    {
        /// <summary>
        /// Creates a new DialogManager
        /// </summary>
        /// <param name="add_manga">Add manga factory</param>
        /// <param name="delete_manga">Delete manga factory</param>
        /// <param name="manager">Window manager</param>
        public DialogManager(IAddMangaViewModelFactory add_manga,
            IDeleteMangaViewModelFactory delete_manga,
            IWindowManager manager)
        {
            manager_ = manager;
            add_manga_ = add_manga;
            delete_manga_ = delete_manga;
        }

        /// <summary>
        /// Displays the add manga dialog
        /// </summary>
        /// <returns>Created manga, or null</returns>
        public Manga AddManga()
        {
            Manga manga = null;

            var dialog = add_manga_.Create();
            var result = manager_.ShowDialog(dialog);

            if (result.HasValue && result.Value)
            {
                manga = new Manga();
                manga.Name = dialog.MangaName;
                manga.Link = dialog.MangaLink;
            }

            add_manga_.Release(dialog);

            return manga;
        }

        /// <summary>
        /// Displays the delete confirmation dialog for the given manga
        /// </summary>
        /// <param name="manga">Manga</param>
        /// <returns>If the user pressed yes</returns>
        public bool DeleteManga(Manga manga)
        {
            bool confirm = false;
            var dialog = delete_manga_.Create(manga);
            var result = manager_.ShowDialog(dialog);

            if (result.HasValue && result.Value)
            {
                confirm = true;
            }

            delete_manga_.Release(dialog);

            return confirm;
        }

        /// <summary>
        /// Add manga view model factory
        /// </summary>
        private IAddMangaViewModelFactory add_manga_;

        /// <summary>
        /// Delete manga view model factory
        /// </summary>
        private IDeleteMangaViewModelFactory delete_manga_;

        /// <summary>
        /// Window manager
        /// </summary>
        private IWindowManager manager_;
    }
}
