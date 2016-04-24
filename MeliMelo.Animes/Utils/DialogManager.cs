using Caliburn.Micro;
using MeliMelo.Animes.Models;
using MeliMelo.Animes.Repositories;
using MeliMelo.ViewModels;

namespace MeliMelo.Animes.Utils
{
    public class DialogManager
    {
        public DialogManager(IAddLibraryViewModelFactory add_library_factory,
            IWindowManager manager, LibrarySorterRepository repository)
        {
            add_library_factory_ = add_library_factory;
            manager_ = manager;
            repository_ = repository;
        }

        public LibrarySorter AddLibrary()
        {
            LibrarySorter sorter = null;

            var dialog = add_library_factory_.Create();
            var result = manager_.ShowDialog(dialog);

            if (result.HasValue && result.Value)
            {
                sorter = repository_.Add(dialog.Name, dialog.Input, dialog.Output);
            }

            add_library_factory_.Release(dialog);

            return sorter;
        }

        private IAddLibraryViewModelFactory add_library_factory_;
        private IWindowManager manager_;
        private LibrarySorterRepository repository_;
    }
}
