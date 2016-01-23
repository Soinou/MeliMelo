namespace MeliMelo.Core.Tasks
{
    /// <summary>
    /// Represents a task
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Gets the name of the task
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Runs the task
        /// </summary>
        void Run();
    }
}
