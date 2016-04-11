namespace MeliMelo.MangaReader.Models
{
    /// <summary>
    /// Represents a manga slide
    /// </summary>
    public class Slide
    {
        /// <summary>
        /// Creates a new slide
        /// </summary>
        /// <param name="file_path">Slide file path</param>
        public Slide(string file_path)
        {
            file_path_ = file_path;
        }

        /// <summary>
        /// Gets the slide file path
        /// </summary>
        public string FilePath
        {
            get
            {
                return file_path_;
            }
        }

        /// <summary>
        /// Slide file path
        /// </summary>
        private string file_path_;
    }
}
