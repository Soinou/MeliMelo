using Caliburn.Micro;
using MeliMelo.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MeliMelo.Ui
{
    /// <summary>
    /// Reimplementation of the BootstrapperBase of Caliburn Micro to add some features
    ///
    /// Adds a WindsorContainer where all components of this assembly are added as well as a mutex to
    /// check for already started instances of the resulting program
    /// </summary>
    public abstract class BootstrapperBase : Caliburn.Micro.BootstrapperBase, IDisposable
    {
        /// <summary>
        /// Creates a new BootstrapperBase
        /// </summary>
        public BootstrapperBase()
        {
            Initialize();

            Application.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Mutex name
        /// </summary>
        protected abstract string MutexName
        {
            get;
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
        /// Exits the application
        /// </summary>
        /// <param name="exit_code"></param>
        protected void Exit(int exit_code = 0)
        {
            OnExit();
            Application.Shutdown(exit_code);
        }

        /// <summary>
        /// Called once the application has to exit
        /// </summary>
        protected abstract void OnExit();

        /// <summary>
        /// Called once the bootstrapper is started and all the components are installed
        /// </summary>
        protected abstract void OnStart();

        /// <inheritdoc />
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            bool mutex_created = false;
            mutex_ = new Mutex(true, MutexName, out mutex_created);

            if (!mutex_created)
            {
                mutex_ = null;
                Exit(1);
            }
            else
            {
                OnStart();
            }
        }

        /// <summary>
        /// If the resources have been freed already
        /// </summary>
        protected bool disposed_;

        /// <summary>
        /// Mutex
        /// </summary>
        protected Mutex mutex_;

        /// <summary>
        /// Called when the application dispatcher has an unhandled exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnDispatcherUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            ShowException(e.Exception);
            e.Handled = true;
        }

        /// <summary>
        /// Called when an unhandled exception is thrown
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowException((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Called when the task scheduler has an unhandled exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ShowException(e.Exception);
            e.SetObserved();
        }

        /// <summary>
        /// Shows an exception message to the user
        /// </summary>
        /// <param name="exception">Exception message</param>
        private void ShowException(Exception exception)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            var manager = new WindowManager();

            manager.ShowDialog(new ExceptionViewModel(exception));

            Exit(1);
        }
    }
}
