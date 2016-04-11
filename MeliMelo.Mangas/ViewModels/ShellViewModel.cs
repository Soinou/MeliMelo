using Caliburn.Micro;
using Castle.Core;
using MeliMelo.Common.Utils;
using MeliMelo.Mangas.Models;
using System;

namespace MeliMelo.ViewModels
{
    public class ShellViewModel : Screen, IInitializable
    {
        public ShellViewModel()
        {
            open_ = false;
        }

        public TrayIcon Icon
        {
            get;
            set;
        }

        public IMainViewModelFactory MainFactory
        {
            get;
            set;
        }

        public MangasTask Task
        {
            get;
            set;
        }

        public IWindowManager WindowManager
        {
            get;
            set;
        }

        public void Initialize()
        {
            Task.MangaUpdated += TaskMangaUpdated;

            Icon.ItemClicked += IconItemClicked;
            Icon.DoubleClicked += IconDoubleClicked;
            Icon.NotificationClicked += IconNotificationClicked;

            Icon.AddItem(kShow);
            Icon.AddSeparator();
            Icon.AddItem(kStart);
            Icon.AddItem(kStop);
            Icon.AddSeparator();
            Icon.AddItem(kExit);

            Icon.Show();

            Task.Start();
        }

        protected void Exit()
        {
            Task.Stop();
            Icon.Hide();
            TryClose();
        }

        protected void IconDoubleClicked(object sender, EventArgs e)
        {
            Show();
        }

        protected void IconItemClicked(object sender, DataEventArgs<string> e)
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

        protected void IconNotificationClicked(object sender, EventArgs e)
        {
            Show();
        }

        protected void Show()
        {
            if (!open_)
            {
                open_ = true;

                var window = MainFactory.Create();

                WindowManager.ShowDialog(window);

                MainFactory.Release(window);

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

        protected void TaskMangaUpdated(object sender, DataEventArgs<uint> e)
        {
            Icon.Notify("MeliMelo.Mangas", e.Data + " new chapters available");
        }

        protected const string kExit = "Exit";
        protected const string kShow = "Show";
        protected const string kStart = "Start";
        protected const string kStop = "Stop";
        protected bool open_;
    }
}
