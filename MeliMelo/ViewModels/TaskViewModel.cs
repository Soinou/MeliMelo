using Caliburn.Micro;
using MeliMelo.Core.Tasks;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents a wrapper a round a task
    /// </summary>
    internal class TaskViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new TaskViewModel
        /// </summary>
        /// <param name="task">Task to wrap</param>
        public TaskViewModel(ITask task)
        {
            task_ = task;
        }

        /// <summary>
        /// Gets the task name
        /// </summary>
        public string Name
        {
            get
            {
                return task_.Name;
            }
        }

        /// <summary>
        /// Runs the task
        /// </summary>
        public void Run()
        {
            task_.Run();
        }

        /// <summary>
        /// Wrapped task
        /// </summary>
        protected ITask task_;
    }
}
