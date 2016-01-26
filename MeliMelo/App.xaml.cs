using System.Windows;

namespace MeliMelo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets if the application is in debug mode
        /// </summary>
        public static bool Debug
        {
            get
            {
                return debug_;
            }
        }

        /// <summary>
        /// Called when the application is started
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0 && e.Args[0] == "--debug")
            {
                debug_ = true;
            }

            base.OnStartup(e);
        }

        /// <summary>
        /// If the application is in debug mode
        /// </summary>
        protected static bool debug_;
    }
}
