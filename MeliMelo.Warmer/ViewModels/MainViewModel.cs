using Caliburn.Micro;
using Castle.Core;
using MeliMelo.Common.Services.Configuration;
using MeliMelo.Warmer.Models;

namespace MeliMelo.ViewModels
{
    public interface IMainViewModelFactory
    {
        MainViewModel Create();

        void Release(MainViewModel view_model);
    }

    public class MainViewModel : Screen, IInitializable
    {
        public MainViewModel()
        {
            DisplayName = "MeliMelo - Warmer";
        }

        public IConfiguration Configuration
        {
            get;
            set;
        }

        public IIntegerValueViewModelFactory IntegerFactory
        {
            get;
            set;
        }

        public IntegerValueViewModel Interval
        {
            get;
            protected set;
        }

        public WarmerTask Task
        {
            get;
            set;
        }

        public IntegerValueViewModel Temperature
        {
            get;
            protected set;
        }

        public void Initialize()
        {
            Interval = IntegerFactory.Create("Interval",
                Configuration.GetInteger("Interval"));
            Temperature = IntegerFactory.Create("Temperature",
                Configuration.GetInteger("Temperature"));

            NotifyOfPropertyChange(() => Interval);
            NotifyOfPropertyChange(() => Temperature);
        }

        public void Save()
        {
            Configuration.Save();
        }

        public void Start()
        {
            Task.Start();
        }

        public void Stop()
        {
            Task.Stop();
        }
    }
}
