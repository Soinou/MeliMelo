using Caliburn.Micro;
using MeliMelo.Core.Configuration;
using MeliMelo.Core.Plugins;
using MeliMelo.Core.Shortcuts;
using MeliMelo.Core.Tasks;
using MeliMelo.Core.Tray;
using MeliMelo.Utils;
using System;
using System.Windows;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents the shell view model
    /// </summary>
    internal class ShellViewModel
    {
        /// <summary>
        /// Creates a new MainViewModel
        /// </summary>
        /// <param name="configuration">Configuration manager</param>
        /// <param name="keyboard">Keyboard manager</param>
        /// <param name="plugins">Plugin manager</param>
        /// <param name="tasks">Task manager</param>
        /// <param name="tray_icon">Tray icon manager</param>
        /// <param name="windows">Window manager</param>
        /// <param name="manga_updater">Manga updater</param>
        public ShellViewModel(IConfigurationManager configuration, IKeyboardManager keyboard,
            IPluginManager plugins, ITaskManager tasks, ITrayIconManager tray_icon,
            IWindowManager windows)
        {
            configuration_ = configuration;
            keyboard_ = keyboard;
            plugins_ = plugins;
            tasks_ = tasks;
            tray_icon_ = tray_icon;
            windows_ = windows;

            InitializeComponents();
        }

        /// <summary>
        /// Stops the application
        /// </summary>
        protected void Exit()
        {
            tasks_.Stop();
            tray_icon_.Hide();
            Application.Current.Shutdown(0);
        }

        /// <summary>
        /// Initializes the ViewModel components
        /// </summary>
        protected void InitializeComponents()
        {
            tray_icon_.ItemClicked += TrayIconManagerItemClicked;
            tray_icon_.DoubleClicked += TrayIconManagerDoubleClicked;
            tray_icon_.NotificationClicked += TrayIconManagerNotificationClicked;

            tray_icon_.AddItem(kShow);
            tray_icon_.AddSeparator();

            plugins_.Load();

            if (tray_icon_.ItemCount > 2)
            {
                tray_icon_.AddSeparator();
            }

            tray_icon_.AddItem(kExit);

            tray_icon_.Show();

            tasks_.Start();
        }

        /// <summary>
        /// Shows the manga list window
        /// </summary>
        protected void Show()
        {
            if (!open_)
            {
                open_ = true;

                windows_.ShowDialog(new MainViewModel(configuration_, tasks_, windows_));

                open_ = false;
            }
        }

        /// <summary>
        /// Called when the tray icon is double clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void TrayIconManagerDoubleClicked(object sender, EventArgs e)
        {
            Show();
        }

        /// <summary>
        /// Called when an item of the tray icon menu is clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void TrayIconManagerItemClicked(object sender, DataEventArgs<string> e)
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

        /// <summary>
        /// Called when a tray icon notification is clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void TrayIconManagerNotificationClicked(object sender, EventArgs e)
        {
            Show();
        }

        /// <summary>
        /// Exit item constant
        /// </summary>
        protected const string kExit = "Exit";

        /// <summary>
        /// Show item constant
        /// </summary>
        protected const string kShow = "Show";

        /// <summary>
        /// Configuration manager
        /// </summary>
        protected IConfigurationManager configuration_;

        /// <summary>
        /// Keyboard manager
        /// </summary>
        protected IKeyboardManager keyboard_;

        /// <summary>
        /// If the mangas window is opened
        /// </summary>
        protected bool open_;

        /// <summary>
        /// Plugin manager
        /// </summary>
        protected IPluginManager plugins_;

        /// <summary>
        /// Task manager
        /// </summary>
        protected ITaskManager tasks_;

        /// <summary>
        /// Tray icon manager
        /// </summary>
        protected ITrayIconManager tray_icon_;

        /// <summary>
        /// Window manager
        /// </summary>
        protected IWindowManager windows_;
    }
}
