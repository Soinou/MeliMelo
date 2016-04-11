using Castle.Core;
using MeliMelo.Animes.Models;
using MeliMelo.Common.Services.Configuration;

namespace MeliMelo.ViewModels
{
    public interface ISettingsFlyoutViewModelFactory
    {
        SettingsFlyoutViewModel Create();

        void Release(SettingsFlyoutViewModel view_model);
    }

    public class SettingsFlyoutViewModel : FlyoutBaseViewModel, IInitializable
    {
        public SettingsFlyoutViewModel()
        {
            Header = "Settings";
            Position = MahApps.Metro.Controls.Position.Right;
            IsOpen = false;
        }

        public IConfiguration Configuration
        {
            get;
            set;
        }

        public PathValueViewModel Input
        {
            get;
            protected set;
        }

        public PathValueViewModel Output
        {
            get;
            protected set;
        }

        public IPathValueViewModelFactory PathFactory
        {
            get;
            set;
        }

        public AnimesTask Task
        {
            get;
            set;
        }

        public void Initialize()
        {
            Input = PathFactory.Create("Input", Configuration.GetPath("Input"));
            Output = PathFactory.Create("Output", Configuration.GetPath("Output"));

            NotifyOfPropertyChange(() => Input);
            NotifyOfPropertyChange(() => Output);
        }

        public void Save()
        {
            Configuration.Save();
            // Restart the task, since paths have changed
            Task.Stop();
            Task.Start();
        }
    }
}
