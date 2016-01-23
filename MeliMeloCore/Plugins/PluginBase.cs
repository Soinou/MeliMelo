using Caliburn.Micro;
using MeliMelo.Core.Configuration;
using MeliMelo.Core.Shortcuts;
using MeliMelo.Core.Tasks;
using MeliMelo.Core.Tray;
using System;

namespace MeliMelo.Core.Plugins
{
    /// <summary>
    /// Base implementation of the IPlugin interface
    /// </summary>
    public abstract class PluginBase : IPlugin
    {
        /// <summary>
        /// Gets/sets the configuration manager
        /// </summary>
        public IConfigurationManager Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets/sets the keyboard manager
        /// </summary>
        public IKeyboardManager Keyboard
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the plugin name
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// Gets/sets the plugin manager
        /// </summary>
        public IPluginManager Plugins
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets/sets the task manager
        /// </summary>
        public ITaskManager Tasks
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets/sets the tray icon manager
        /// </summary>
        public ITrayIconManager TrayIcon
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets/sets the window manager
        /// </summary>
        public IWindowManager Windows
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads the plugin
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Registers a manager into the plugin
        /// </summary>
        /// <typeparam name="T">Type of the manager</typeparam>
        /// <param name="manager">Manager</param>
        public void Register<T>(object manager)
        {
            if (typeof(T) == typeof(IConfigurationManager))
                Configuration = manager as IConfigurationManager;
            else if (typeof(T) == typeof(IKeyboardManager))
                Keyboard = manager as IKeyboardManager;
            else if (typeof(T) == typeof(IPluginManager))
                Plugins = manager as IPluginManager;
            else if (typeof(T) == typeof(ITaskManager))
                Tasks = manager as ITaskManager;
            else if (typeof(T) == typeof(ITrayIconManager))
                TrayIcon = manager as ITrayIconManager;
            else if (typeof(T) == typeof(IWindowManager))
                Windows = manager as IWindowManager;
            else
                throw new InvalidOperationException("Unrecognized type \"" + typeof(T).FullName
                    + "\"");
        }
    }
}
