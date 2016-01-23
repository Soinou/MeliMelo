using System;
using System.IO;

namespace MeliMelo.Animes
{
    public class SortNode
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anime">the anime</param>
        /// <param name="source">the source</param>
        /// <param name="destination">the destination</param>
        public SortNode(Anime anime, string source, string destination)
        {
            this.anime = anime;
            this.source = source;
            this.destination = destination;
        }

        /// <summary>
        /// Destination property
        /// </summary>
        public string Destination
        {
            get
            {
                return destination;
            }
        }

        /// <summary>
        /// Anime episode property
        /// </summary>
        public string Episode
        {
            get
            {
                return anime.EpisodeNumber();
            }
        }

        /// <summary>
        /// Source property
        /// </summary>
        public string Source
        {
            get
            {
                return source;
            }
        }

        /// <summary>
        /// Anime title property
        /// </summary>
        public string Title
        {
            get
            {
                return anime.AnimeTitle();
            }
        }

        /// <summary>
        /// Moves the file
        /// </summary>
        public bool Move()
        {
            try
            {
                File.Copy(source, Path.Combine(destination));
                File.Delete(source);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The recognized anime
        /// </summary>
        private Anime anime;

        /// <summary>
        /// The node destination
        /// </summary>
        private string destination;

        /// <summary>
        /// The node source
        /// </summary>
        private string source;
    }
}
