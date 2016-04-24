using Caliburn.Micro;
using MeliMelo.Warmer.Localization;
using System.Reflection;
using System.Text;

namespace MeliMelo.ViewModels
{
    /// <summary>
    /// Represents the about view model
    /// </summary>
    internal class AboutViewModel : Screen
    {
        /// <summary>
        /// Creates a new AboutViewModel
        /// </summary>
        /// <param name="locale">Locale</param>
        public AboutViewModel(ILocale locale)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var name = assembly.GetName();
            var builder = new StringBuilder();

            builder.AppendLine(name.Name);
            builder.AppendLine("Version " + name.Version.ToString());
            builder.AppendLine("Copyright © Abricot Soinou 2016");
            builder.AppendLine(locale["About.Contact"]);
            builder.AppendLine(locale["About.Bugs"]);

            About = builder.ToString();
        }

        /// <summary>
        /// Gets the about string
        /// </summary>
        public string About
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public override string DisplayName
        {
            get
            {
                return "MeliMelo.Warmer - About";
            }
            set
            {
            }
        }
    }
}
