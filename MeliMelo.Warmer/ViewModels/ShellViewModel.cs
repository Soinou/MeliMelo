using Caliburn.Micro;
using MeliMelo.Common.Services.Configuration;
using MeliMelo.Common.Utils;
using MeliMelo.Warmer.Models;
using System.Windows;

namespace MeliMelo.ViewModels
{
    internal class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            open_ = false;
        }

        public IConfiguration Configuration
        {
            get;
            set;
        }

        public IMainViewModelFactory MainFactory
        {
            get;
            set;
        }

        public WarmerTask Task
        {
            get;
            set;
        }

        public TrayIcon TrayIcon
        {
            get;
            set;
        }

        public IWindowManager WindowManager
        {
            get;
            set;
        }

        protected void Exit()
        {
            Task.Stop();
            TrayIcon.Hide();
            Application.Current.Shutdown(0);
        }

        protected override void OnInitialize()
        {
            Task.Start();

            TrayIcon.AddItem(kShow);
            TrayIcon.AddSeparator();
            TrayIcon.AddItem(kStart);
            TrayIcon.AddItem(kStop);
            TrayIcon.AddSeparator();
            TrayIcon.AddItem(kExit);

            TrayIcon.ItemClicked += TrayIconItemClicked;
            TrayIcon.DoubleClicked += TrayIconDoubleClicked;

            TrayIcon.Show();
        }

        protected void Show()
        {
            if (!open_)
            {
                open_ = true;

                var view = MainFactory.Create();

                WindowManager.ShowDialog(view);

                MainFactory.Release(view);

                open_ = false;
            }
        }

        protected void Start()
        {
            Task.Start();
        }

        protected void Stop()
        {
            Task.Stop();
        }

        protected void TrayIconDoubleClicked(object sender, System.EventArgs e)
        {
            Show();
        }

        protected void TrayIconItemClicked(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case kShow:
                    Show();
                    break;

                case kStart:
                    Start();
                    break;

                case kStop:
                    Stop();
                    break;

                case kExit:
                    Exit();
                    break;

                default:
                    break;
            }
        }

        protected const string kExit = "Exit";
        protected const string kShow = "Show";
        protected const string kStart = "Start";
        protected const string kStop = "Stop";
        protected bool open_;
    }
}
