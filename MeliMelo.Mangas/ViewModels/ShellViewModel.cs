using Caliburn.Micro;
using MeliMelo.Common.Utils;
using MeliMelo.Mangas.Models;
using System;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Shell view model
    /// </summary>
    public class ShellViewModel : Screen
    {
        /// <summary>
        /// Creates a new ShellViewModel
        /// </summary>
        /// <param name="main">Main view model</param>
        /// <param name="manager">Window manager</param>
        /// <param name="task">Mangas task</param>
        /// <param name="tray">Tray icon</param>
        public ShellViewModel(MainViewModel main, IWindowManager manager, MangasTask task,
            TrayIcon tray)
        {
            main_ = main;
            manager_ = manager;
            open_ = false;
            task_ = task;
            tray_ = tray;

            task_.MangaUpdated += TaskMangaUpdated;

            tray_.ItemClicked += IconItemClicked;
            tray_.DoubleClicked += IconDoubleClicked;
            tray_.NotificationClicked += IconNotificationClicked;

            tray_.AddItem(kShow);
            tray_.AddSeparator();
            tray_.AddItem(kStart);
            tray_.AddItem(kStop);
            tray_.AddSeparator();
            tray_.AddItem(kExit);

            tray_.Show();

            task_.Start();
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        private void Exit()
        {
            task_.Stop();
            tray_.Hide();
            TryClose();
        }

        /// <summary>
        /// Called when the tray icon has been double clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void IconDoubleClicked(object sender, EventArgs e)
        {
            Show();
        }

        /// <summary>
        /// Called when an item of the tray icon menu has been clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void IconItemClicked(object sender, DataEventArgs<string> e)
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

        /// <summary>
        /// Called when a tray icon notification has been clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void IconNotificationClicked(object sender, EventArgs e)
        {
            Show();
        }

        /// <summary>
        /// Shows the main view
        /// </summary>
        private void Show()
        {
            if (!open_)
            {
                open_ = true;

                manager_.ShowDialog(main_);

                open_ = false;
            }
        }

        /// <summary>
        /// Starts the task
        /// </summary>
        private void Start()
        {
            task_.Start();
        }

        /// <summary>
        /// Stops the task
        /// </summary>
        private void Stop()
        {
            task_.Stop();
        }

        /// <summary>
        /// Called when the manga task has new chapters available
        /// </summary>
        /// <param name="count">Count of new available chapters</param>
        private void TaskMangaUpdated(uint count)
        {
            tray_.Notify("MeliMelo.Mangas", count + " new chapters available");
        }

        /// <summary>
        /// Exit constant
        /// </summary>
        private const string kExit = "Exit";

        /// <summary>
        /// Show constant
        /// </summary>
        private const string kShow = "Show";

        /// <summary>
        /// Start constant
        /// </summary>
        private const string kStart = "Start";

        /// <summary>
        /// Stop constant
        /// </summary>
        private const string kStop = "Stop";

        /// <summary>
        /// Main view model
        /// </summary>
        private MainViewModel main_;

        /// <summary>
        /// Window manager
        /// </summary>
        private IWindowManager manager_;

        private bool open_;

        /// <summary>
        /// Mangas task
        /// </summary>
        private MangasTask task_;

        /// <summary>
        /// Tray icon
        /// </summary>
        private TrayIcon tray_;
    }
}
