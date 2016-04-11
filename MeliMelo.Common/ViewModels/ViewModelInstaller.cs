using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MeliMelo.Common.Core;

namespace MeliMelo.ViewModels
{
    public class ViewModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterFactory<IExceptionViewModelFactory, ExceptionViewModel>();
            container.RegisterFactory<IIntegerValueViewModelFactory, IntegerValueViewModel>();
            container.RegisterFactory<IPathValueViewModelFactory, PathValueViewModel>();
            container.RegisterFactory<IStringValueViewModelFactory, StringValueViewModel>();
        }
    }
}
