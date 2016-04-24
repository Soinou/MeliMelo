using Caliburn.Micro;
using MeliMelo.Common.Services.Configuration;
using MeliMelo.Common.Services.Configuration.Values;
using MeliMelo.Common.Utils;
using MeliMelo.MangaReader.Models;
using System.IO;
using System.Linq;

namespace MeliMelo.ViewModels
{
    public class ReaderViewModel : Conductor<MangaViewModel>
    {
        public ReaderViewModel(IConfiguration configuration, IMangaViewModelFactory factory)
        {
            factory_ = factory;
            mangas_ = new BindableCollection<MangaViewModel>();
            selected_manga_ = null;

            directory_ = configuration.GetPath("Directory");
            item_height_ = configuration.GetInteger("ItemHeight");
            scroll_speed_ = configuration.GetDecimal("ScrollSpeed");

            directory_.ValueChanged += OnDirectoryValueChange;
            item_height_.ValueChanged += OnItemHeightValueChanged;
            scroll_speed_.ValueChanged += OnScrollSpeedValueChanged;

            Update(directory_.Value);
        }

        public IObservableCollection<MangaViewModel> Mangas
        {
            get
            {
                return mangas_;
            }
        }

        public MangaViewModel SelectedManga
        {
            get
            {
                return selected_manga_;
            }
            set
            {
                selected_manga_ = value;
                NotifyOfPropertyChange(() => SelectedManga);
                ActivateItem(selected_manga_);

                if (MangaChanged != null)
                {
                    MangaChanged(selected_manga_);
                }
            }
        }

        public event DataEventHandler<MangaViewModel> MangaChanged;

        private void OnDirectoryValueChange(string directory)
        {
            Update(directory);
        }

        private void OnItemHeightValueChanged(int item_height)
        {
            foreach (var manga in mangas_)
            {
                manga.ItemHeight = item_height;
            }
        }

        private void OnScrollSpeedValueChanged(decimal scroll_speed)
        {
            foreach (var manga in mangas_)
            {
                manga.ScrollSpeed = scroll_speed;
            }
        }

        private void Update(string directory)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                foreach (var manga in mangas_)
                {
                    factory_.Release(manga);
                }

                mangas_.Clear();

                foreach (var subdirectory in Directory.GetDirectories(directory))
                {
                    mangas_.Add(factory_.Create(new Manga(subdirectory), item_height_.Value, scroll_speed_.Value));
                }

                SelectedManga = mangas_.FirstOrDefault();

                mangas_.Refresh();
            }
        }

        private PathValue directory_;
        private IMangaViewModelFactory factory_;
        private IntegerValue item_height_;
        private IObservableCollection<MangaViewModel> mangas_;
        private DecimalValue scroll_speed_;
        private MangaViewModel selected_manga_;
    }
}
