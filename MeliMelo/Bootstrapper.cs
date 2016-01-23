using Caliburn.Micro;
using MeliMelo.Animes;
using MeliMelo.Core.Configuration;
using MeliMelo.Core.Plugins;
using MeliMelo.Core.Shortcuts;
using MeliMelo.Core.Tasks;
using MeliMelo.Core.Tray;
using MeliMelo.Impl;
using MeliMelo.iTunes;
using MeliMelo.Mangas;
using MeliMelo.Screen;
using MeliMelo.ViewModels;

namespace MeliMelo
{
    /// <summary>
    /// Application bootstrapper
    /// </summary>
    internal class Bootstrapper : BootstrapperImpl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Bootstrapper()
        {
            configuration_ = null;
            keyboard_ = null;
            plugins_ = null;
            tasks_ = null;
            tray_icon_ = null;
            windows_ = null;
        }

        /// <summary>
        /// Called when the application is started
        /// </summary>
        protected override void OnStart()
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected void Register<T>(string name, T manager)
        {
            container_.RegisterInstance(typeof(T), name, manager);
            plugins_.Register<T>(manager);
        }

        /// <summary>
        /// Register all the service managers in the IoC container
        /// </summary>
        protected override void RegisterServices()
        {
            // Create managers
            configuration_ = new ConfigurationManagerImpl("MeliMelo.db");
            keyboard_ = new KeyboardManagerImpl();
            plugins_ = new PluginManagerImpl();
            tasks_ = new TaskManagerImpl();
            tray_icon_ = new TrayIconManagerImpl("MeliMelo", Properties.Resources.Icon);
            windows_ = new WindowManager();

            // Add plugins to the plugin manager
            plugins_.Add(new iTunesPlugin());
            plugins_.Add(new ScreenPlugin());
            plugins_.Add(new MangasPlugin());
            plugins_.Add(new AnimesPlugin());

            // Register managers in the container
            Register("ConfigurationManager", configuration_);
            Register("KeyboardManager", keyboard_);
            Register("PluginManager", plugins_);
            Register("TaskManager", tasks_);
            Register("TrayIconManager", tray_icon_);
            Register("WindowManager", windows_);

            // Register ShellViewModel in the container
            container_.PerRequest<ShellViewModel, ShellViewModel>();
        }

        /// <summary>
        /// configuration manager
        /// </summary>
        protected IConfigurationManager configuration_;

        /// <summary>
        /// Keyboard manager
        /// </summary>
        protected IKeyboardManager keyboard_;

        /// <summary>
        /// Plugin manager
        /// </summary>
        protected IPluginManager plugins_;

        /// <summary>
        /// Task manager
        /// </summary>
        protected ITaskManager tasks_;

        /// <summary>
        /// Tray icon manager
        /// </summary>
        protected ITrayIconManager tray_icon_;

        /// <summary>
        /// Window manager
        /// </summary>
        protected IWindowManager windows_;
    }
}
