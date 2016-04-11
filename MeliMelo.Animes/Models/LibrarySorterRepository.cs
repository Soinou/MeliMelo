using MeliMelo.Common.Services.Repository;

namespace MeliMelo.Animes.Models
{
    public class LibrarySorterRepository : MemoryRepository<LibrarySorter>
    {
        public ILibrarySorterFactory Factory
        {
            get;
            set;
        }

        public LibraryRepository Libraries
        {
            get;
            set;
        }

        public LibrarySorter Add(string name, string input, string output)
        {
            var library = Libraries.Add(name, input, output);

            Libraries.Save();

            return Add(library);
        }

        public LibrarySorter Add(Library library)
        {
            var sorter = Factory.Create(library);

            Add(sorter);

            sorter.Start();

            return sorter;
        }

        protected override void OnFinalize()
        {
            foreach (var sorter in items_)
            {
                Factory.Release(sorter);
            }
        }
    }
}
