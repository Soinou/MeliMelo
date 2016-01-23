namespace MeliMelo.Core.Tasks
{
    /// <summary>
    /// Represents an automatic task
    /// </summary>
    public interface IAutoTask : ITask
    {
        /// <summary>
        /// Gets if the task is currently running
        /// </summary>
        bool Running
        {
            get;
        }

        /// <summary>
        /// Starts the task
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the task
        /// </summary>
        void Stop();
    }
}
