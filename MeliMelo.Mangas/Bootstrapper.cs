using MeliMelo.Common.Core;
using MeliMelo.Mangas.Models;
using MeliMelo.Mangas.Utils;
using MeliMelo.ViewModels;

namespace MeliMelo.Mangas
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
                return "MeliMelo.Mangas-ace8e3b6-39fa-4071-9a29-c136cfaf2349";
            }
        }

        protected override void OnStart()
        {
            // Tray Icon
            Container.RegisterTrayIcon("MeliMelo.Mangas", Properties.Resources.Icon);

            // Components
            Container.RegisterFactory<IAddMangaViewModelFactory, AddMangaViewModel>();
            Container.RegisterFactory<IDeleteMangaViewModelFactory, DeleteMangaViewModel>();
            Container.RegisterFactory<IChapterViewModelFactory, ChapterViewModel>();
            Container.RegisterFactory<IMangaViewModelFactory, MangaViewModel>();

            // Utils
            Container.RegisterSingleton<DialogManager>();
            Container.RegisterSingleton<MangaParser>();

            // Repository
            Container.RegisterSingleton<MangaRepository>();

            // Task
            Container.RegisterSingleton<MangasTask>();

            // Shell
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<ShellViewModel>();

            // Display shell view
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
