using MeliMelo.Common.Services.Repository;

namespace MeliMelo.Mangas.Models
{
    /// <summary>
    /// Manga repository using the JsonRepository
    /// </summary>
    public class MangaRepository : JsonRepository<Manga>
    {
        /// <summary>
        /// Creates a new MangaRepository
        /// </summary>
        public MangaRepository()
            : base(@"data\MeliMelo.Mangas.db")
        { }
    }
}
