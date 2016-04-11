using Castle.MicroKernel.Registration;
using MeliMelo.Common.Core;
using MeliMelo.Common.Utils;
using MeliMelo.ViewModels;
using MeliMelo.Warmer.Models;

namespace MeliMelo.Warmer
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override string MutexName
        {
            get
            {
                return "MeliMelo.Warmer-e76116e8-a4d2-4b02-9f1d-c37215674ef6";
            }
        }

        protected override void OnStart()
        {
            Configuration.RegisterInteger("Interval", "ms", 1000, 100, 10000, 100);
            Configuration.RegisterInteger("Temperature", "K", 2300, 1100, 25100, 100);

            // Components
            Container.Register(Component.For<TrayIcon>()
                .DependsOn(Dependency.OnValue("text", "MeliMelo.Warmer"))
                .DependsOn(Dependency.OnValue("icon", Properties.Resources.Icon))
                .LifeStyle.Singleton);

            // Singletons
            Container.RegisterSingleton<WarmerTask>();
            Container.RegisterSingleton<ShellViewModel>();

            // Factories
            Container.RegisterFactory<IMainViewModelFactory, MainViewModel>();

            // Display shell view
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
