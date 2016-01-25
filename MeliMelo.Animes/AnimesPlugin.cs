using MeliMelo.Core.Configuration.Values;
using MeliMelo.Core.Plugins;
using MeliMelo.ViewModels;

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

            task_ = new AnimesTask(input, output);

            Tasks.AddAutoTask(task_);

            TrayIcon.AddItem(kAnimes);

            TrayIcon.ItemClicked += TrayIconItemClicked;
        }

        protected void TrayIconItemClicked(object sender, Utils.DataEventArgs<string> e)
        {
            if (e.Data == kAnimes & !open_)
            {
                open_ = true;
                Windows.ShowDialog(new SortingQueueViewModel(task_.Queue));
                open_ = false;
            }
        }

        protected const string kAnimes = "Animes";

        protected bool open_;

        protected AnimesTask task_;
    }
}
