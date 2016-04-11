using MeliMelo.Common.Utils;
using System.Collections.Generic;

namespace MeliMelo.Mangas.Models
{
    /// <summary>
    /// Represents a manga RSS feed
    /// </summary>
    public class Manga
    {
        /// <summary>
        /// Creates a new Manga
        /// </summary>
        public Manga()
        {
            chapters_ = new LinkedList<Chapter>();
        }

        /// <summary>
        /// Gets/sets the manga RSS feed item count
        /// </summary>
        public ICollection<Chapter> Chapters
        {
            get
            {
                return chapters_;
            }
            set
            {
                chapters_ = new LinkedList<Chapter>(value);
            }
        }

        /// <summary>
        /// Gets if the manga has at least one unread chapter
        /// </summary>
        public bool HasUnread
        {
            get
            {
                foreach (var chapter in chapters_)
                    if (!chapter.IsRead)
                        return true;

                return false;
            }
        }

        /// <summary>
        /// Gets/sets the manga RSS link
        /// </summary>
        public string Link
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the manga name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Adds a chapter to the manga
        /// </summary>
        /// <param name="chapter"></param>
        public void Add(Chapter chapter)
        {
            chapters_.AddFirst(chapter);
            if (NewChapter != null)
                NewChapter(chapter);
        }

        /// <summary>
        /// Triggered when a new chapter is added
        /// </summary>
        public event DataEventHandler<Chapter> NewChapter;

        /// <summary>
        /// List of manga chapters
        /// </summary>
        protected LinkedList<Chapter> chapters_;
    }
}
