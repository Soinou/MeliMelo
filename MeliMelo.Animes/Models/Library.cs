namespace MeliMelo.Animes.Models
{
    /// <summary>
    /// Represents an anime library
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Gets/sets the library input
        /// </summary>
        public string Input
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the monitoring state of the library
        /// </summary>
        public bool Monitoring
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the library name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the library output
        /// </summary>
        public string Output
        {
            get;
            set;
        }
    }
}
