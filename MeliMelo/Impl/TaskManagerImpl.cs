using MeliMelo.Core.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeliMelo.Impl
{
    internal sealed class TaskManagerImpl : ITaskManager
    {
        /// <summary>
        /// Creates a new TaskManagerImpl
        /// </summary>
        public TaskManagerImpl()
        {
            tasks_ = new List<ITask>();
            auto_tasks_ = new List<IAutoTask>();
        }

        /// <summary>
        /// Gets the list of auto tasks
        /// </summary>
        public ReadOnlyCollection<IAutoTask> AutoTasks
        {
            get
            {
                return auto_tasks_.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the list of tasks
        /// </summary>
        public ReadOnlyCollection<ITask> Tasks
        {
            get
            {
                return tasks_.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds a new auto task to the manager
        /// </summary>
        /// <param name="task">Task to add</param>
        public void AddAutoTask(IAutoTask task)
        {
            auto_tasks_.Add(task);
        }

        /// <summary>
        /// Adds a new task to the manager
        /// </summary>
        /// <param name="task">Task to add</param>
        public void AddTask(ITask task)
        {
            tasks_.Add(task);
        }

        /// <summary>
        /// Starts all the tasks
        /// </summary>
        public void Start()
        {
            foreach (IAutoTask task in auto_tasks_)
                task.Start();
        }

        /// <summary>
        /// Stops all the tasks
        /// </summary>
        public void Stop()
        {
            foreach (IAutoTask task in auto_tasks_)
                task.Stop();
        }

        /// <summary>
        /// List of auto tasks
        /// </summary>
        private List<IAutoTask> auto_tasks_;

        /// <summary>
        /// List of manual tasks
        /// </summary>
        private List<ITask> tasks_;
    }
}
