using Caliburn.Micro;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Shell view model
    /// </summary>
    public class ShellViewModel : Conductor<Screen>
    {
        /// <summary>
        /// Creates a new ShellViewModel
        /// </summary>
        public ShellViewModel(ReaderViewModel reader, SettingsViewModel settings)
        {
            DisplayName = "MeliMelo.MangaReader";

            reader_ = reader;
            reader_.MangaChanged += OnReaderMangaChanged;
            settings_ = settings;

            ActivateItem(reader_);

            if (reader_.SelectedManga != null)
            {
                DisplayName = reader_.SelectedManga.MangaName + " - MeliMelo.MangaReader";
            }
        }

        public void Reader()
        {
            ActivateItem(reader_);
        }

        public void Settings()
        {
            ActivateItem(settings_);
        }

        private void OnReaderMangaChanged(MangaViewModel manga)
        {
            if (manga == null)
            {
                DisplayName = "MeliMelo.MangaReader";
            }
            else
            {
                DisplayName = manga.MangaName + " - MeliMelo.MangaReader";
            }
        }

        /// <summary>
        /// Reader view model
        /// </summary>
        private ReaderViewModel reader_;

        /// <summary>
        /// Settings view model
        /// </summary>
        private SettingsViewModel settings_;
    }
}
