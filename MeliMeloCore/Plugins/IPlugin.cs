namespace MeliMelo.Core.Plugins
{
    /// <summary>
    /// Represents a plugin extending the application features
    /// </summary>
    public partial interface IPlugin
    {
        /// <summary>
        /// Gets the plugin name
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Loads the plugin
        /// </summary>
        void Load();

        /// <summary>
        /// Registers a manager into the plugin
        /// </summary>
        /// <typeparam name="T">Type of the manager</typeparam>
        /// <param name="manager">Manager</param>
        void Register<T>(object manager);
    }
}
