using System;

namespace MeliMelo.Mangas.Core
{
    /// <summary>
    /// Represents a manga chapter
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// Creates a new Chapter
        /// </summary>
        public Chapter()
        {
            title_ = "";
            link_ = "";
            read_ = false;
        }

        /// <summary>
        /// Gets/sets if the chapter was read or not
        /// </summary>
        public bool IsRead
        {
            get
            {
                return read_;
            }
            set
            {
                read_ = value;

                if (read_ && Read != null)
                    Read(this, new EventArgs());
            }
        }

        /// <summary>
        /// Gets the chapter description
        /// </summary>
        public string Link
        {
            get
            {
                return link_;
            }
            set
            {
                link_ = value;
            }
        }

        /// <summary>
        /// Gets the chapter title
        /// </summary>
        public string Title
        {
            get
            {
                return title_;
            }
            set
            {
                title_ = value;
            }
        }

        /// <summary>
        /// Triggered when the chapter has been read
        /// </summary>
        public event EventHandler Read;

        /// <summary>
        /// Chapter link
        /// </summary>
        protected string link_;

        /// <summary>
        /// If this chapter was read or not
        /// </summary>
        protected bool read_;

        /// <summary>
        /// Chapter title
        /// </summary>
        protected string title_;
    }
}
