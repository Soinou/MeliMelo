using Caliburn.Micro;
using MeliMelo.Core.Tasks;
using System.Collections.Generic;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents the window displaying all the available tasks
    /// </summary>
    internal class TasksViewModel : Caliburn.Micro.Screen
    {
        /// <summary>
        /// Creates a new TasksViewModel
        /// </summary>
        public TasksViewModel(ITaskManager task_manager)
        {
            DisplayName = "MeliMelo - Tasks";

            tasks_ = new List<TaskViewModel>();
            auto_tasks_ = new List<AutoTaskViewModel>();

            foreach (var task in task_manager.Tasks)
                tasks_.Add(new TaskViewModel(task));

            foreach (var auto_task in task_manager.AutoTasks)
                auto_tasks_.Add(new AutoTaskViewModel(auto_task));
        }

        /// <summary>
        /// Gets the auto tasks list
        /// </summary>
        public IObservableCollection<AutoTaskViewModel> AutoTasks
        {
            get
            {
                return new BindableCollection<AutoTaskViewModel>(auto_tasks_);
            }
        }

        /// <summary>
        /// Gets the task list
        /// </summary>
        public IObservableCollection<TaskViewModel> Tasks
        {
            get
            {
                return new BindableCollection<TaskViewModel>(tasks_);
            }
        }

        /// <summary>
        /// Auto task list
        /// </summary>
        protected List<AutoTaskViewModel> auto_tasks_;

        /// <summary>
        /// Task list
        /// </summary>
        protected List<TaskViewModel> tasks_;
    }
}
