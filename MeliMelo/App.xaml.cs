using MeliMelo.Properties;
using MeliMelo.Utils.Log;
using Squirrel;
using System;
using System.Text;
using System.Threading;
using System.Windows;

namespace MeliMelo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            mutex_ = null;
            disposed_ = false;

            InitializeComponent();
        }

        /// <summary>
        /// Gets if the application is in debug mode
        /// </summary>
        public static bool Debug
        {
            get
            {
                return debug_;
            }
        }

        /// <summary>
        /// Disposes of the application resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes of the application resources
        /// </summary>
        /// <param name="disposing">If we need to dispose of the managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed_)
            {
                if (disposing)
                {
                    if (mutex_ != null)
                    {
                        LogManager.Instance.Stop();
                        mutex_.ReleaseMutex();
                        mutex_.Close();
                        mutex_ = null;
                    }
                }

                disposed_ = true;
            }
        }

        /// <summary>
        /// Called when the application is exited
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnExit(ExitEventArgs e)
        {
            Dispose();
            base.OnExit(e);
        }

        /// <summary>
        /// Called when the application is started
        /// </summary>
        /// <param name="e">Event args</param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (e.Args.Length > 0 && e.Args[0] == "--debug")
                    debug_ = true;

                if (!debug_)
                {
                    string url = Settings.Default.kProjectURL;

                    using (var manager = await UpdateManager.GitHubUpdateManager(url))
                    {
                        SquirrelAwareApp.HandleEvents(onFirstRun: async () =>
                        {
                            manager.CreateShortcutsForExecutable("MeliMelo.exe",
                                ShortcutLocation.Desktop | ShortcutLocation.Startup, false);

                            await manager.CreateUninstallerRegistryEntry();
                        });
                    }
                }

                string mutex_name = "MeliMelo-2218c0a3-7447-46c7-ad37-87bc73e36bef";

                bool mutex_created = false;
                mutex_ = new Mutex(true, mutex_name, out mutex_created);

                if (!mutex_created)
                {
                    mutex_ = null;
                    Current.Shutdown();
                }
                else
                {
                    LogManager.Instance.Start();

                    base.OnStartup(e);
                }
            }
            catch (Exception ex)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("Seems like an error has occured: ");
                builder.AppendLine(ex.Message);
                builder.AppendLine("Stack Trace:");
                builder.AppendLine(ex.StackTrace);

                MessageBox.Show(builder.ToString(), "Whoops!");
            }
        }

        /// <summary>
        /// If the application is in debug mode
        /// </summary>
        protected static bool debug_;

        /// <summary>
        /// If the resources are disposed or not
        /// </summary>
        protected bool disposed_;

        /// <summary>
        /// Mutex used to check for existing instance
        /// </summary>
        protected Mutex mutex_;
    }
}
