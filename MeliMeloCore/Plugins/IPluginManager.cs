using System.Collections.Generic;

namespace MeliMelo.Core.Plugins
{
    /// <summary>
    /// Represents a plugin manager
    /// </summary>
    public interface IPluginManager : IEnumerable<IPlugin>
    {
        /// <summary>
        /// Adds a plugin to the manager
        /// </summary>
        /// <param name="plugin">Plugin to add</param>
        void Add(IPlugin plugin);

        /// <summary>
        /// Load all plugins
        /// </summary>
        void Load();

        /// <summary>
        /// Registers a manager into all the plugins of this manager
        /// </summary>
        /// <typeparam name="T">Type of the manager</typeparam>
        /// <param name="manager">Manager</param>
        void Register<T>(object manager);
    }
}
