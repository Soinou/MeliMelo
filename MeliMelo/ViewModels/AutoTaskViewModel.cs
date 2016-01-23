using Caliburn.Micro;
using MeliMelo.Core.Tasks;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents a wrapper around an automatic task
    /// </summary>
    internal class AutoTaskViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new AutomaticTaskViewModel
        /// </summary>
        /// <param name="task">Task to wrap</param>
        public AutoTaskViewModel(IAutoTask task)
        {
            task_ = task;
        }

        /// <summary>
        /// Gets if the task can be started
        /// </summary>
        public bool CanStart
        {
            get
            {
                return !task_.Running;
            }
        }

        /// <summary>
        /// Gets if the task can be stopped
        /// </summary>
        public bool CanStop
        {
            get
            {
                return task_.Running;
            }
        }

        /// <summary>
        /// Gets the name of the task
        /// </summary>
        public string Name
        {
            get
            {
                return task_.Name;
            }
        }

        /// <summary>
        /// Starts the task
        /// </summary>
        public void Start()
        {
            task_.Start();
            NotifyOfPropertyChange(() => CanStart);
            NotifyOfPropertyChange(() => CanStop);
        }

        /// <summary>
        /// Stops the task
        /// </summary>
        public void Stop()
        {
            task_.Stop();
            NotifyOfPropertyChange(() => CanStart);
            NotifyOfPropertyChange(() => CanStop);
        }

        /// <summary>
        /// Wrapped task
        /// </summary>
        protected IAutoTask task_;
    }
}
