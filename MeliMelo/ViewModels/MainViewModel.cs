using Caliburn.Micro;
using MahApps.Metro.Controls;
using MeliMelo.Core.Configuration;
using MeliMelo.Core.Tasks;
using MeliMelo.Properties;
using MeliMelo.Utils;
using Squirrel;
using System;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents the main view model
    /// </summary>
    internal class MainViewModel : Conductor<Caliburn.Micro.Screen>
    {
        /// <summary>
        /// Creates a new MainViewModel
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="tasks">Task manager</param>
        /// <param name="windows">Window manager</param>
        public MainViewModel(IConfigurationManager configuration, ITaskManager tasks,
            IWindowManager windows)
        {
            DisplayName = "MeliMelo";

            configuration_ = configuration;
            tasks_ = tasks;
            windows_ = windows;
            transition_ = TransitionType.Right;
            showing_tasks_ = false;

            ShowTasks();
        }

        /// <summary>
        /// Gets the transition type
        /// </summary>
        public TransitionType Transition
        {
            get
            {
                return transition_;
            }
            set
            {
                transition_ = value;
                NotifyOfPropertyChange(() => Transition);
            }
        }

        /// <summary>
        /// Updates the application
        /// </summary>
        public async void ApplicationUpdate()
        {
            if (!App.Debug)
            {
                bool restart = false;

                string url = Settings.Default.kProjectURL;

                using (var manager = await UpdateManager.GitHubUpdateManager(url))
                {
                    var update_info = await manager.CheckForUpdate();
                    if (update_info != null && update_info.ReleasesToApply.Count > 0)
                    {
                        var result = windows_.ShowDialog(new ApplicationUpdateViewModel(true,
                            update_info));

                        if (result.HasValue && result.Value)
                        {
                            await manager.DownloadReleases(update_info.ReleasesToApply);
                            await manager.ApplyReleases(update_info);
                            restart = true;
                        }
                    }
                    else
                        windows_.ShowDialog(new ApplicationUpdateViewModel(false, update_info));
                }

                if (restart)
                    UpdateManager.RestartApp();
            }
        }

        /// <summary>
        /// Shows the configuration screen
        /// </summary>
        public void ShowConfiguration()
        {
            if (showing_tasks_)
            {
                Transition = TransitionType.Left;
                ActivateItem(new ConfigurationViewModel(configuration_));
                showing_tasks_ = false;
            }
        }

        /// <summary>
        /// Shows the tasks screen
        /// </summary>
        public void ShowTasks()
        {
            if (!showing_tasks_)
            {
                Transition = TransitionType.Right;
                ActivateItem(new TasksViewModel(tasks_));
                showing_tasks_ = true;
            }
        }

        /// <summary>
        /// Triggered when the window is closed
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> Close;

        /// <summary>
        /// Called when the window is deactivated
        /// </summary>
        /// <param name="close">If the window is closing or just deactivating</param>
        protected override void OnDeactivate(bool close)
        {
            if (Close != null)
                Close(this, new DataEventArgs<bool>(close));

            base.OnDeactivate(close);
        }

        /// <summary>
        /// Configuration manager
        /// </summary>
        protected IConfigurationManager configuration_;

        /// <summary>
        /// If we are showing the tasks page or the configuration page
        /// </summary>
        protected bool showing_tasks_;

        /// <summary>
        /// Task manager
        /// </summary>
        protected ITaskManager tasks_;

        /// <summary>
        /// Transition type
        /// </summary>
        protected TransitionType transition_;

        /// <summary>
        /// Window manager
        /// </summary>
        protected IWindowManager windows_;
    }
}
