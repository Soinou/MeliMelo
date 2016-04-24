using MeliMelo.Common.Core;
using MeliMelo.ViewModels;

namespace MeliMelo.MangaReader
{
    internal class Bootstrapper : BootstrapperBase
    {
        protected override string ConfigurationFileName
        {
            get
            {
                return "MeliMelo.MangaReader.Configuration.json";
            }
        }

        protected override string MutexName
        {
            get
            {
                return "MeliMelo.MangaReader-05874d14-6fee-4418-8d32-544647d3c9f6";
            }
        }

        protected override void OnStart()
        {
            // Configuration
            Configuration.RegisterPath("Directory", "");
            Configuration.RegisterInteger("ItemHeight", "px", 1200, 500, 2000, 100);
            Configuration.RegisterDouble("ScrollSpeed", "x", 1, 0.5M, 5, 0.1M);

            // Components
            Container.RegisterFactory<IMangaViewModelFactory, MangaViewModel>();
            Container.RegisterFactory<ISlideViewModelFactory, SlideViewModel>();

            // Pages
            Container.RegisterSingleton<ReaderViewModel>();
            Container.RegisterSingleton<SettingsViewModel>();

            // Shell
            Container.RegisterSingleton<ShellViewModel>();

            // Display shell
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
