using MeliMelo.Common.Services.Configuration;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents the settings view model
    /// </summary>
    public class SettingsViewModel : Caliburn.Micro.Screen
    {
        /// <summary>
        /// Creates a new SettingsViewModel
        /// </summary>
        public SettingsViewModel(IConfiguration configuration,
            IPathValueViewModelFactory path_factory,
            IIntegerValueViewModelFactory integer_factory,
            IDecimalValueViewModelFactory double_factory)
        {
            configuration_ = configuration;

            Directory = path_factory.Create("Directory", configuration.GetPath("Directory"));
            ItemHeight = integer_factory.Create("Item Height", configuration.GetInteger("ItemHeight"));
            ScrollSpeed = double_factory.Create("Scroll Speed", configuration.GetDecimal("ScrollSpeed"));
        }

        /// <summary>
        /// Gets the directory
        /// </summary>
        public PathValueViewModel Directory
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the item height
        /// </summary>
        public IntegerValueViewModel ItemHeight
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the scroll speed
        /// </summary>
        public DecimalValueViewModel ScrollSpeed
        {
            get;
            private set;
        }

        public void Save()
        {
            configuration_.Save();
        }

        private IConfiguration configuration_;
    }
}
