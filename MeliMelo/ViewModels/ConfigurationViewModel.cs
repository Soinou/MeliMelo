using Caliburn.Micro;
using MeliMelo.Core.Configuration;
using MeliMelo.Core.Configuration.Values;
using System.Collections.Generic;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents a wrapper around a configuration file
    /// </summary>
    internal class ConfigurationViewModel : Caliburn.Micro.Screen
    {
        /// <summary>
        /// Creates a new ConfigurationViewModel
        /// </summary>
        /// <param name="configuration_manager">Configuration manager</param>
        public ConfigurationViewModel(IConfigurationManager configuration_manager)
        {
            DisplayName = "MeliMelo - Configuration";

            configuration_ = configuration_manager;
            integer_values_ = new List<IntegerValueViewModel>();
            string_values_ = new List<StringValueViewModel>();
            path_values_ = new List<PathValueViewModel>();

            foreach (var pair in configuration_)
            {
                IValue value = pair.Value;
                IntegerValue integer_value = value as IntegerValue;
                StringValue string_value = value as StringValue;
                PathValue path_value = value as PathValue;

                if (integer_value != null)
                    integer_values_.Add(new IntegerValueViewModel(pair.Key, integer_value));
                else if (string_value != null)
                    string_values_.Add(new StringValueViewModel(pair.Key, string_value));
                else if (path_value != null)
                    path_values_.Add(new PathValueViewModel(pair.Key, path_value));
            }
        }

        /// <summary>
        /// Gets the integer values list
        /// </summary>
        public IObservableCollection<IntegerValueViewModel> IntegerValues
        {
            get
            {
                return new BindableCollection<IntegerValueViewModel>(integer_values_);
            }
        }

        /// <summary>
        /// Gets the path values list
        /// </summary>
        public IObservableCollection<PathValueViewModel> PathValues
        {
            get
            {
                return new BindableCollection<PathValueViewModel>(path_values_);
            }
        }

        /// <summary>
        /// Gets the string values list
        /// </summary>
        public IObservableCollection<StringValueViewModel> StringValues
        {
            get
            {
                return new BindableCollection<StringValueViewModel>(string_values_);
            }
        }

        /// <summary>
        /// Saves the configuration to a file
        /// </summary>
        public void Save()
        {
            configuration_.Save();
        }

        /// <summary>
        /// Configuration file
        /// </summary>
        protected IConfigurationManager configuration_;

        /// <summary>
        /// List of the integers
        /// </summary>
        protected List<IntegerValueViewModel> integer_values_;

        /// <summary>
        /// List of the path values
        /// </summary>
        protected List<PathValueViewModel> path_values_;

        /// <summary>
        /// List of the string values
        /// </summary>
        protected List<StringValueViewModel> string_values_;
    }
}
