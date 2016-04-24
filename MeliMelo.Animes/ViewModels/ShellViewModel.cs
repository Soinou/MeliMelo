using Caliburn.Micro;
using MeliMelo.Common.Utils;
using System;
using System.Threading.Tasks;

namespace MeliMelo.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(MainViewModel main, IWindowManager manager, TrayIcon tray)
        {
            main_ = main;
            manager_ = manager;
            open_ = false;
            tray_ = tray;

            tray_.ItemClicked += IconItemClicked;
            tray_.DoubleClicked += IconDoubleClicked;
            tray_.NotificationClicked += IconNotificationClicked;

            tray_.AddItem(kShow);
            tray_.AddSeparator();
            tray_.AddItem(kExit);

            tray_.Show();
        }

        protected async void Exit()
        {
            await Task.Run(() =>
            {
                tray_.Hide();
                TryClose();
            });
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

                manager_.ShowDialog(main_);

                open_ = false;
            }
        }

        private const string kExit = "Exit";
        private const string kShow = "Show";
        private MainViewModel main_;
        private IWindowManager manager_;
        private bool open_;
        private TrayIcon tray_;
    }
}
