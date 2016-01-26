using Caliburn.Micro;
using MeliMelo.Animes;
using MeliMelo.Core;
using MeliMelo.Core.Configuration;
using MeliMelo.Core.Plugins;
using MeliMelo.Core.Shortcuts;
using MeliMelo.Core.Tasks;
using MeliMelo.Core.Tray;
using MeliMelo.Impl;
using MeliMelo.iTunes;
using MeliMelo.Mangas;
using MeliMelo.Screen;
using MeliMelo.ViewModels;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MeliMelo
{
    /// <summary>
    /// Application bootstrapper
    /// </summary>
    internal class Bootstrapper : BootstrapperImpl, IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Bootstrapper()
        {
            Application.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            configuration_ = null;
            keyboard_ = null;
            plugins_ = null;
            tasks_ = null;
            tray_icon_ = null;
            windows_ = null;
        }

        /// <summary>
        /// Cleans resources used by this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Cleans resources used by this object
        /// </summary>
        /// <param name="disposing">If the GC is finalizing this object</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed_)
            {
                if (disposing)
                {
                    Utils.Log.LogManager.Instance.Stop();

                    if (mutex_ != null)
                    {
                        mutex_.ReleaseMutex();
                        mutex_.Close();
                        mutex_ = null;
                    }
                }

                disposed_ = true;
            }
        }

        /// <summary>
        /// Called when the application dispatcher has an unhandled exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnDispatcherUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            ShowException(e.Exception);
            e.Handled = true;
        }

        /// <summary>
        /// Called when the application is initialized
        /// </summary>
        protected override void OnInitialize()
        {
            Utils.Log.LogManager.Instance.Start();

            string mutex_name = "MeliMelo-2218c0a3-7447-46c7-ad37-87bc73e36bef";

            bool mutex_created = false;
            mutex_ = new Mutex(true, mutex_name, out mutex_created);

            if (!mutex_created)
            {
                mutex_ = null;
                System.Windows.Application.Current.Shutdown(1);
            }
            else if (!App.Debug)
            {
                Updater.CreateShortcuts();
            }
        }

        /// <summary>
        /// Called when the application is started
        /// </summary>
        protected override void OnStart()
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        /// <summary>
        /// Called when an unhandled exception is thrown
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowException((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Called when the task scheduler has an unhandled exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ShowException(e.Exception);
            e.SetObserved();
        }

        /// <summary>
        /// Registers a service manager in the bootstrapper
        /// </summary>
        /// <typeparam name="T">Type of the manager</typeparam>
        /// <param name="name">Name of the manager</param>
        /// <param name="manager">Manager</param>
        protected void Register<T>(string name, T manager)
        {
            container_.RegisterInstance(typeof(T), name, manager);
            plugins_.Register<T>(manager);
        }

        /// <summary>
        /// Register all the service managers in the IoC container
        /// </summary>
        protected override void RegisterServices()
        {
            // Create managers
            configuration_ = new ConfigurationManagerImpl(@"..\data\MeliMelo.db");
            keyboard_ = new KeyboardManagerImpl();
            plugins_ = new PluginManagerImpl();
            tasks_ = new TaskManagerImpl();
            tray_icon_ = new TrayIconManagerImpl("MeliMelo", Properties.Resources.Icon);
            windows_ = new WindowManager();

            // Add plugins to the plugin manager
            plugins_.Add(new iTunesPlugin());
            plugins_.Add(new ScreenPlugin());
            plugins_.Add(new MangasPlugin());
            plugins_.Add(new AnimesPlugin());

            // Register managers in the container
            Register("ConfigurationManager", configuration_);
            Register("KeyboardManager", keyboard_);
            Register("PluginManager", plugins_);
            Register("TaskManager", tasks_);
            Register("TrayIconManager", tray_icon_);
            Register("WindowManager", windows_);

            // Register ShellViewModel in the container
            container_.PerRequest<ShellViewModel, ShellViewModel>();
        }

        /// <summary>
        /// Shows an exception message to the user
        /// </summary>
        /// <param name="exception">Exception message</param>
        protected void ShowException(Exception exception)
        {
            if (windows_ != null)
                windows_.ShowDialog(new ExceptionViewModel(exception));
            else
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine("A fatal error has occured.");
                builder.AppendLine("Error: " + exception.Message);
                builder.Append(exception.StackTrace);

                MessageBox.Show(builder.ToString(), "Whoops!");
            }

            if (tray_icon_ != null)
                tray_icon_.Hide();

            if (tasks_ != null)
                tasks_.Stop();

            Application.Shutdown(1);
        }

        /// <summary>
        /// configuration manager
        /// </summary>
        protected IConfigurationManager configuration_;

        /// <summary>
        /// If the object was already disposed of
        /// </summary>
        protected bool disposed_;

        /// <summary>
        /// Keyboard manager
        /// </summary>
        protected IKeyboardManager keyboard_;

        /// <summary>
        /// Mutex used to check for existing instance
        /// </summary>
        protected Mutex mutex_;

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
