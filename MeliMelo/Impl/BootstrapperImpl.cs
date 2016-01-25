using Caliburn.Micro;
using MeliMelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace MeliMelo.Impl
{
    /// <summary>
    /// Implementation of the BootstrapperBase with a built-in IoC container
    /// </summary>
    internal abstract class BootstrapperImpl : BootstrapperBase
    {
        /// <summary>
        /// Creates a new BootstrapperImpl
        /// </summary>
        public BootstrapperImpl()
        {
            Initialize();

            container_ = new SimpleContainer();
        }

        /// <summary>
        /// Buildups the container with the given instance
        /// </summary>
        /// <param name="instance">Instance</param>
        protected override void BuildUp(object instance)
        {
            container_.BuildUp(instance);
        }

        /// <summary>
        /// Gets all instances linked to the given type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>All the instances</returns>
        protected override IEnumerable<object> GetAllInstances(Type type)
        {
            return container_.GetAllInstances(type);
        }

        /// <summary>
        /// Gets the instance linked to the given type/key
        /// </summary>
        /// <param name="yype">Type</param>
        /// <param name="key">Key</param>
        /// <returns>Instance</returns>
        protected override object GetInstance(Type type, string key)
        {
            return container_.GetInstance(type, key);
        }

        /// <summary>
        /// Called when the application is started
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Called when the bootstrapper is started
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            RegisterServices();

            OnStart();
        }

        /// <summary>
        /// Register services in the IoC container
        /// </summary>
        protected abstract void RegisterServices();

        /// <summary>
        /// Select the correct assemblies
        /// </summary>
        /// <returns>Correct assemblies</returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();

            assemblies.Add(typeof(MangasViewModel).GetTypeInfo().Assembly);
            assemblies.Add(typeof(SortingQueueViewModel).GetTypeInfo().Assembly);

            return assemblies;
        }

        /// <summary>
        /// IoC container of the application
        /// </summary>
        protected SimpleContainer container_;
    }
}
