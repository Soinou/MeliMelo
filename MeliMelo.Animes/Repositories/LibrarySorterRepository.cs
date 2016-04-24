using MeliMelo.Animes.Models;
using MeliMelo.Common.Services.Repository;

namespace MeliMelo.Animes.Repositories
{
    public class LibrarySorterRepository : MemoryRepository<LibrarySorter>
    {
        public LibrarySorterRepository(ILibrarySorterFactory factory, AnimeParser parser,
            LibraryRepository repository)
        {
            factory_ = factory;
            parser_ = parser;
            repository_ = repository;

            foreach (var library in repository_.Items)
            {
                Add(library);
            }
        }

        public LibrarySorter Add(string name, string input, string output)
        {
            var library = repository_.Add(name, input, output);

            repository_.Save();

            return Add(library);
        }

        public LibrarySorter Add(Library library)
        {
            var sorter = factory_.Create(library, parser_);

            Add(sorter);

            return sorter;
        }

        protected override void OnFinalize()
        {
            foreach (var sorter in items_)
            {
                factory_.Release(sorter);
            }
        }

        private ILibrarySorterFactory factory_;
        private AnimeParser parser_;
        private LibraryRepository repository_;
    }
}
