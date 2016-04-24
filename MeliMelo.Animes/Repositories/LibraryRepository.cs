using MeliMelo.Animes.Models;
using MeliMelo.Common.Services.Repository;

namespace MeliMelo.Animes.Repositories
{
    /// <summary>
    /// Represents a repository of anime libraries
    /// </summary>
    public class LibraryRepository : JsonRepository<Library>
    {
        /// <summary>
        /// Creates a new LibraryRepository
        /// </summary>
        public LibraryRepository()
            : base(@"data\MeliMelo.Libraries.db")
        { }

        public Library Add(string name, string input, string output)
        {
            Library library = new Library();

            library.name = name;
            library.input = input;
            library.output = output;

            Add(library);

            return library;
        }
    }
}
