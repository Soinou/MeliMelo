using Caliburn.Micro;
using MeliMelo.MangaReader.Models;
using MeliMelo.MangaReader.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Manga view model factory
    /// </summary>
    public interface IMangaViewModelFactory
    {
        /// <summary>
        /// Creates a new MangaViewModel
        /// </summary>
        /// <param name="manga">Manga</param>
        /// <param name="item_height"></param>
        /// <param name="speed"></param>
        /// <returns>View model</returns>
        MangaViewModel Create(Manga manga, int item_height, decimal speed);

        /// <summary>
        /// Releases a manga view model
        /// </summary>
        /// <param name="manga">Manga view model to release</param>
        void Release(MangaViewModel manga);
    }

    /// <summary>
    /// Represents a manga view model
    /// </summary>
    public class MangaViewModel : Screen
    {
        /// <summary>
        /// Creates a new MangaViewModel
        /// </summary>
        /// <param name="manga">Manga</param>
        /// <param name="item_height"></param>
        /// <param name="speed"></param>
        public MangaViewModel(Manga manga, int item_height, decimal speed)
        {
            item_height_ = item_height;
            speed_ = speed;
            manga_ = manga;
            slides_ = new PagedList<SlideViewModel>();

            int item = 0;
            foreach (var slide in manga_.Slides)
            {
                int page = item / kItemsPerPage;
                item++;
                slides_.Add(page, new SlideViewModel(slide, item_height_));
            }

            slides_.Update(0);
        }

        public int ItemHeight
        {
            get
            {
                return item_height_;
            }
            set
            {
                item_height_ = value;
                foreach (var slide in Slides)
                {
                    slide.Height = item_height_;
                }
            }
        }

        public string MangaName
        {
            get
            {
                return manga_.Name;
            }
        }

        /// <summary>
        /// Gets the scroll viewer speed
        /// </summary>
        public decimal ScrollSpeed
        {
            get
            {
                return speed_;
            }
            set
            {
                speed_ = value;
                NotifyOfPropertyChange(() => ScrollSpeed);
            }
        }

        /// <summary>
        /// Gets the manga slides
        /// </summary>
        public IEnumerable<SlideViewModel> Slides
        {
            get
            {
                return slides_.Items.Values.SelectMany(i => i);
            }
        }

        /// <summary>
        /// Called when the current scroll position has changed
        /// </summary>
        /// <param name="e">Scroll event</param>
        public void ScrollChanged(ScrollChangedEventArgs e)
        {
            int index = (int)(e.VerticalOffset / (item_height_ * kItemsPerPage));

            slides_.Update(index);
        }

        protected override void OnActivate()
        {
            // Load the first pages
            slides_.Update(0);

            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            // Unload everything
            slides_.Update(-1);

            base.OnDeactivate(close);
        }

        /// <summary>
        /// Number of items per page
        /// </summary>
        private const int kItemsPerPage = 2;

        private int item_height_;

        /// <summary>
        /// Manga
        /// </summary>
        private Manga manga_;

        /// <summary>
        /// List of slides
        /// </summary>
        private PagedList<SlideViewModel> slides_;

        private decimal speed_;
    }
}
