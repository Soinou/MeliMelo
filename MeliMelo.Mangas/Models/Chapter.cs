using MeliMelo.Common.Utils;

namespace MeliMelo.Mangas.Models
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
            read_ = false;
        }

        /// <summary>
        /// Gets/sets the chapter description
        /// </summary>
        public string Description
        {
            get;
            set;
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
                    Read();
            }
        }

        /// <summary>
        /// Gets the chapter description
        /// </summary>
        public string Link
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the chapter number
        /// 
        /// (Yeah it's a float, ask One to know why we need a fucking float)
        /// </summary>
        public float? Number
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the translation team
        /// </summary>
        public string Team
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the chapter title
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Formats the chapter title correctly
        /// </summary>
        /// <returns></returns>
        public string FormatTitle()
        {
            if (Number.HasValue)
            {
                if (!string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Team))
                {
                    return "#" + Number.Value + ": " + Description + " [" + Team + "]";
                }
                else
                {
                    return "#" + Number.Value;
                }
            }
            else
            {
                return Title;
            }
        }

        /// <summary>
        /// Triggered when the chapter has been read
        /// </summary>
        public event DataEventHandler Read;

        /// <summary>
        /// If this chapter was read or not
        /// </summary>
        protected bool read_;
    }
}
