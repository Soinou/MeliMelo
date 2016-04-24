using Caliburn.Micro;
using MeliMelo.MangaReader.Models;
using System;
using System.Windows.Media.Imaging;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Slide view model factory
    /// </summary>
    public interface ISlideViewModelFactory
    {
        /// <summary>
        /// Creates a new SlideViewModel
        /// </summary>
        /// <param name="slide">Slide</param>
        /// <param name="height"></param>
        /// <returns>Slide view model</returns>
        SlideViewModel Create(Slide slide, int height);

        /// <summary>
        /// Releases a slide view model
        /// </summary>
        /// <param name="slide">Slide view model to release</param>
        void Release(SlideViewModel slide);
    }

    /// <summary>
    /// Represents a slide view model
    /// </summary>
    public class SlideViewModel : PropertyChangedBase, IVirtualizable
    {
        /// <summary>
        /// Creates a new SlideViewModel
        /// </summary>
        /// <param name="slide">Slide</param>
        /// <param name="height"></param>
        public SlideViewModel(Slide slide, int height)
        {
            slide_ = slide;
            image_ = null;
            height_ = height;
        }

        public int Height
        {
            get
            {
                return height_;
            }
            set
            {
                height_ = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        /// <summary>
        /// Slide source
        /// </summary>
        public BitmapImage Source
        {
            get
            {
                return image_;
            }
            set
            {
                image_ = value;
                NotifyOfPropertyChange(() => Source);
            }
        }

        /// <summary>
        /// Loads the slide image
        /// </summary>
        public void Load()
        {
            if (image_ == null)
            {
                var bitmap = new BitmapImage(new Uri(slide_.FilePath));
                bitmap.Freeze();
                Source = bitmap;
            }
        }

        /// <summary>
        /// Unloads the slide image
        /// </summary>
        public void Unload()
        {
            if (image_ != null)
            {
                Source = null;
            }
        }

        private int height_;

        /// <summary>
        /// Slide image
        /// </summary>
        private BitmapImage image_;

        /// <summary>
        /// Slide
        /// </summary>
        private Slide slide_;
    }
}
