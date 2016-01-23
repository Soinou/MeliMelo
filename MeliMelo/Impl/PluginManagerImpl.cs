using MeliMelo.Core.Plugins;
using System.Collections;
using System.Collections.Generic;

namespace MeliMelo.Impl
{
    /// <summary>
    /// Implementation of the IPluginManager
    /// </summary>
    internal sealed class PluginManagerImpl : IPluginManager
    {
        /// <summary>
        /// Creates a new PluginManagerImpl
        /// </summary>
        public PluginManagerImpl()
        {
            plugins_ = new List<IPlugin>();
        }

        /// <summary>
        /// Adds a plugin to the manager
        /// </summary>
        /// <param name="plugin">Plugin to add</param>
        public void Add(IPlugin plugin)
        {
            plugins_.Add(plugin);
        }

        /// <summary>
        /// Gets the plugin manager enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<IPlugin> GetEnumerator()
        {
            return plugins_.GetEnumerator();
        }

        /// <summary>
        /// Gets the plugin manager enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return plugins_.GetEnumerator();
        }

        /// <summary>
        /// Loads all the plugins
        /// </summary>
        public void Load()
        {
            foreach (IPlugin plugin in plugins_)
            {
                plugin.Load();
            }
        }

        /// <summary>
        /// Registers the given manager into all the plugins of this manager
        /// </summary>
        /// <typeparam name="T">Type of the manager</typeparam>
        /// <param name="manager">Manager</param>
        public void Register<T>(object manager)
        {
            foreach (IPlugin plugin in plugins_)
            {
                plugin.Register<T>(manager);
            }
        }

        /// <summary>
        /// Plugin list
        /// </summary>
        private ICollection<IPlugin> plugins_;
    }
}
