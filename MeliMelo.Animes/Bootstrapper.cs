using MeliMelo.Animes.Models;
using MeliMelo.Animes.Repositories;
using MeliMelo.Animes.Utils;
using MeliMelo.Common.Core;
using MeliMelo.ViewModels;

namespace MeliMelo.Animes
{
    internal class Bootstrapper : BootstrapperBase
    {
        protected override string ConfigurationFileName
        {
            get
            {
                return null;
            }
        }

        protected override string MutexName
        {
            get
            {
                return "MeliMelo.Animes-a5af7fba-5de2-44de-b7ae-d4421cd6dcef";
            }
        }

        protected override void OnStart()
        {
            // Tray icon
            Container.RegisterTrayIcon("MeliMelo.Animes", Properties.Resources.Icon);

            // Components
            Container.RegisterFactory<IAddLibraryViewModelFactory, AddLibraryViewModel>();
            Container.RegisterFactory<ISortingNodeViewModelFactory, SortingNodeViewModel>();
            Container.RegisterFactory<ILibrarySorterFactory, LibrarySorter>();
            Container.RegisterFactory<ILibrarySorterViewModelFactory, LibrarySorterViewModel>();

            // Repositories
            Container.RegisterSingleton<LibrarySorterRepository>();
            Container.RegisterSingleton<LibraryRepository>();

            // Views
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<ShellViewModel>();

            // Utils
            Container.RegisterSingleton<DialogManager>();

            // Parser
            Container.RegisterSingleton<AnimeParser>();

            // Display shell view
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
