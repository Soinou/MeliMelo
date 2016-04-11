using Caliburn.Micro;
using MeliMelo.MangaReader.Models;
using System.IO;
using System.Windows.Forms;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Settings flyout view model factory
    /// </summary>
    public interface ISettingsFlyoutViewModelFactory
    {
        /// <summary>
        /// Creates a new settings flyout
        /// </summary>
        /// <returns>A new settings flyout</returns>
        SettingsFlyoutViewModel Create();

        /// <summary>
        /// Releases a settings flyout
        /// </summary>
        /// <param name="flyout"></param>
        void Release(SettingsFlyoutViewModel flyout);
    }

    /// <summary>
    /// Represents the settings flyout view model
    /// </summary>
    public class SettingsFlyoutViewModel : FlyoutBaseViewModel
    {
        /// <summary>
        /// Manga selected event delegate
        /// </summary>
        /// <param name="manga">Newly selected manga</param>
        public delegate void MangaSelectedDelegate(Manga manga);

        /// <summary>
        /// Creates a new SettingsFlyoutViewModel
        /// </summary>
        public SettingsFlyoutViewModel()
        {
            Position = MahApps.Metro.Controls.Position.Right;
            Header = "Mangas";
            IsOpen = false;
            mangas_ = new BindableCollection<Manga>();
            selected_ = null;
        }

        /// <summary>
        /// Gets the list of available mangas
        /// </summary>
        public IObservableCollection<Manga> Mangas
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
                return selected_;
            }
            set
            {
                if (selected_ != value)
                {
                    selected_ = value;
                    NotifyOfPropertyChange(() => SelectedManga);
                    if (MangaSelected != null)
                    {
                        MangaSelected(selected_);
                    }
                }
            }
        }

        /// <summary>
        /// Called when the user presses the SetDirectory button
        /// </summary>
        public void SetDirectory()
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mangas_.Clear();

                foreach (var directory in Directory.GetDirectories(dialog.SelectedPath))
                {
                    mangas_.Add(new Manga(directory));
                }

                NotifyOfPropertyChange(() => Mangas);
            }
        }

        /// <summary>
        /// Event triggered when the selected manga changes
        /// </summary>
        public event MangaSelectedDelegate MangaSelected;

        /// <summary>
        /// List of available mangas
        /// </summary>
        private IObservableCollection<Manga> mangas_;

        /// <summary>
        /// Currently selected manga
        /// </summary>
        private Manga selected_;
    }
}
