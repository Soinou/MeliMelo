using Caliburn.Micro;
using System;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents a manga adding window
    /// </summary>
    public class AddMangaViewModel : Screen
    {
        /// <summary>
        /// Creates a new AddMangaViewModel
        /// </summary>
        public AddMangaViewModel()
        {
            DisplayName = "MeliMelo - Add Manga";

            link_ = "";
            name_ = "";
        }

        /// <summary>
        /// Gets if the save button is active or not
        /// </summary>
        public bool CanSave
        {
            get
            {
                return !string.IsNullOrEmpty(name_)
                    && !string.IsNullOrEmpty(link_)
                    && Uri.IsWellFormedUriString(link_, UriKind.Absolute);
            }
        }

        /// <summary>
        /// Gets/sets the manga link
        /// </summary>
        public string MangaLink
        {
            get
            {
                return link_;
            }
            set
            {
                if (link_ != value)
                {
                    link_ = value;
                    NotifyOfPropertyChange(() => MangaLink);
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        /// <summary>
        /// Gets/sets the manga name
        /// </summary>
        public string MangaName
        {
            get
            {
                return name_;
            }
            set
            {
                if (name_ != value)
                {
                    name_ = value;
                    NotifyOfPropertyChange(() => MangaName);
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        /// <summary>
        /// Closes the window without saving the manga
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        /// <summary>
        /// Saves the manga and closes the window
        /// </summary>
        public void Save()
        {
            TryClose(true);
        }

        /// <summary>
        /// Link of the created manga
        /// </summary>
        protected string link_;

        /// <summary>
        /// Name of the created manga
        /// </summary>
        protected string name_;
    }
}
