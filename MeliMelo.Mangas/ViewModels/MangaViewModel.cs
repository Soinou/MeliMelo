using Caliburn.Micro;
using MeliMelo.Mangas;
using MeliMelo.Mangas.Core;
using MeliMelo.Utils;
using System.Threading.Tasks;

namespace MeliMelo.ViewModels
{
    public class MangaViewModel : Screen
    {
        /// <summary>
        /// Creates a new MangaViewModel
        /// </summary>
        /// <param name="task">Mangas task</param>
        /// <param name="manga">Manga to wrap</param>
        public MangaViewModel(MangasTask task, Manga manga)
        {
            task_ = task;
            manga_ = manga;
            manga_.NewChapter += MangaNewChapter;
            chapters_ = new BindableCollection<ChapterViewModel>();
            foreach (Chapter chapter in manga_.Chapters)
                chapters_.Add(new ChapterViewModel(task_, manga_, chapter));

            DisplayName = manga_.Name;
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

        /// <summary>
        /// Gets the chapter list
        /// </summary>
        public IObservableCollection<ChapterViewModel> Chapters
        {
            get
            {
                return chapters_;
            }
        }

        /// <summary>
        /// Reads all the chapters currently not read
        /// </summary>
        public async void ReadAll()
        {
            await Task.Run(() =>
            {
                foreach (Chapter chapter in manga_.Chapters)
                    chapter.IsRead = true;

                chapters_.Refresh();
                NotifyOfPropertyChange(() => Chapters);

                task_.Save();
            });
        }

        /// <summary>
        /// Chapter list
        /// </summary>
        protected IObservableCollection<ChapterViewModel> chapters_;

        /// <summary>
        /// Wrapped manga
        /// </summary>
        protected Manga manga_;

        /// <summary>
        /// Manga updater
        /// </summary>
        protected MangasTask task_;

        /// <summary>
        /// Called when a chapter has been added to the manga
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void MangaNewChapter(object sender, DataEventArgs<Chapter> e)
        {
            chapters_.Insert(0, new ChapterViewModel(task_, manga_, e.Data));
            chapters_.Refresh();
            NotifyOfPropertyChange(() => Chapters);
            NotifyOfPropertyChange(() => CanReadAll);
        }
    }
}
