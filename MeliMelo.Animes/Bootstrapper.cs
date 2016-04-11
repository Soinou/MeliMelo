using Castle.MicroKernel.Registration;
using MeliMelo.Animes.Models;
using MeliMelo.Common.Core;
using MeliMelo.Common.Utils;
using MeliMelo.ViewModels;

namespace MeliMelo.Animes
{
    internal class Bootstrapper : BootstrapperBase
    {
        protected override string MutexName
        {
            get
            {
                return "MeliMelo.Animes-a5af7fba-5de2-44de-b7ae-d4421cd6dcef";
            }
        }

        protected override void OnStart()
        {
            // Components
            Container.Register(Component.For<TrayIcon>()
                .DependsOn(Dependency.OnValue("text", "MeliMelo.Animes"))
                .DependsOn(Dependency.OnValue("icon", Properties.Resources.Icon))
                .LifeStyle.Singleton);

            Container.RegisterFactory<IAddLibraryViewModelFactory, AddLibraryViewModel>();
            Container.RegisterFactory<ISortingNodeViewModelFactory, SortingNodeViewModel>();
            Container.RegisterFactory<IMainViewModelFactory, MainViewModel>();
            Container.RegisterFactory<ILibrarySorterFactory, LibrarySorter>();
            Container.RegisterFactory<ILibrarySorterViewModelFactory, LibrarySorterViewModel>();

            Container.RegisterSingleton<LibrarySorterRepository>();
            Container.RegisterSingleton<LibraryRepository>();
            Container.RegisterSingleton<ShellViewModel>();
            Container.RegisterSingleton<Reader>();

            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
