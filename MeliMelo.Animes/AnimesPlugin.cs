using MeliMelo.Core.Configuration.Values;
using MeliMelo.Core.Plugins;

namespace MeliMelo.Animes
{
    /// <summary>
    /// Represents the anime plugin
    /// </summary>
    public class AnimesPlugin : PluginBase
    {
        /// <summary>
        /// Gets the name of the plugin
        /// </summary>
        public override string Name
        {
            get
            {
                return "Animes";
            }
        }

        /// <summary>
        /// Loads the plugin
        /// </summary>
        public override void Load()
        {
            PathValue input = Configuration.GetPath("Animes.Input", "");
            PathValue output = Configuration.GetPath("Animes.Output", "");

            Tasks.AddAutoTask(new AnimesTask(input, output));
        }
    }
}
