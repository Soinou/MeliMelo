using MeliMelo.Ui;
using MeliMelo.ViewModels;

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

        protected override void OnExit()
        {
        }

        protected override void OnStart()
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
