using Caliburn.Micro;
using MeliMelo.MangaReader.Models;
using System.Collections.Generic;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Shell view model
    /// </summary>
    public class ShellViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new ShellViewModel
        /// </summary>
        public ShellViewModel(ISettingsFlyoutViewModelFactory settings_factory,
            IMangaViewModelFactory manga_factory)
        {
            flyout_ = settings_factory.Create();
            flyout_.MangaSelected += OnFlyoutMangaSelected;
            manga_ = null;
            manga_factory_ = manga_factory;
        }

        /// <summary>
        /// Gets the list of flyouts
        /// </summary>
        public IEnumerable<FlyoutBaseViewModel> Flyouts
        {
            get
            {
                return new SettingsFlyoutViewModel[] { flyout_ };
            }
        }

        /// <summary>
        /// Gets the selected manga view model
        /// </summary>
        public MangaViewModel Manga
        {
            get
            {
                return manga_;
            }
        }

        /// <summary>
        /// Opens/closes the settings flyout
        /// </summary>
        public void Settings()
        {
            flyout_.IsOpen = !flyout_.IsOpen;
        }

        /// <summary>
        /// Triggered when a new manga was selected in the flyout
        /// </summary>
        /// <param name="manga">Newly selected manga</param>
        private void OnFlyoutMangaSelected(Manga manga)
        {
            if (manga_ != null)
            {
                manga_factory_.Release(manga_);
            }

            manga_ = manga_factory_.Create(manga);
            NotifyOfPropertyChange(() => Manga);
        }

        /// <summary>
        /// Settings flyout
        /// </summary>
        private SettingsFlyoutViewModel flyout_;

        /// <summary>
        /// Currently selected manga view model
        /// </summary>
        private MangaViewModel manga_;

        /// <summary>
        /// Manga view model factory
        /// </summary>
        private IMangaViewModelFactory manga_factory_;
    }
}
