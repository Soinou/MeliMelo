using System.Collections.Generic;
using System.IO;

namespace MeliMelo.MangaReader.Models
{
    /// <summary>
    /// Represents a manga
    /// </summary>
    public class Manga
    {
        /// <summary>
        /// Creates a new Manga
        /// </summary>
        /// <param name="directory_path">Manga directory path</param>
        public Manga(string directory_path)
        {
            directory_path_ = directory_path;
            slides_ = new List<Slide>();
            foreach (var file in Directory.GetFiles(directory_path_))
            {
                slides_.Add(new Slide(file));
            }
        }

        /// <summary>
        /// Gets the manga directory path
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return directory_path_;
            }
        }

        /// <summary>
        /// Gets the manga name
        /// </summary>
        public string Name
        {
            get
            {
                return Path.GetFileName(directory_path_);
            }
        }

        /// <summary>
        /// Gets the manga slides
        /// </summary>
        public IEnumerable<Slide> Slides
        {
            get
            {
                return slides_;
            }
        }

        /// <summary>
        /// Manga directory path
        /// </summary>
        private string directory_path_;

        /// <summary>
        /// Manga slides
        /// </summary>
        private ICollection<Slide> slides_;
    }
}
