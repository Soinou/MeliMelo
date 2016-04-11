using MeliMelo.Common.Services.Repository;

namespace MeliMelo.Animes.Models
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

            library.Name = name;
            library.Input = input;
            library.Output = output;

            Add(library);

            return library;
        }
    }
}
