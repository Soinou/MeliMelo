using System.Collections.ObjectModel;

namespace MeliMelo.Core.Tasks
{
    /// <summary>
    /// Represents a task manager
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Gets the auto task list
        /// </summary>
        ReadOnlyCollection<IAutoTask> AutoTasks
        {
            get;
        }

        /// <summary>
        /// Gets the task list
        /// </summary>
        ReadOnlyCollection<ITask> Tasks
        {
            get;
        }

        /// <summary>
        /// Adds an auto task to the manager
        /// </summary>
        /// <param name="task">Auto task to add</param>
        void AddAutoTask(IAutoTask task);

        /// <summary>
        /// Adds a task to the manager
        /// </summary>
        /// <param name="task">Task to add</param>
        void AddTask(ITask task);

        /// <summary>
        /// Starts all the tasks
        /// </summary>
        void Start();

        /// <summary>
        /// Stops all the tasks
        /// </summary>
        void Stop();
    }
}
