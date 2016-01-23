using MeliMelo.Core.Plugins;
using MeliMelo.ViewModels;

namespace MeliMelo.Mangas
{
    /// <summary>
    /// Mangas plugin
    /// </summary>
    public class MangasPlugin : PluginBase
    {
        /// <summary>
        /// Creates a new MangasPlugin
        /// </summary>
        public MangasPlugin()
        {
            open_ = false;
            task_ = null;
        }

        /// <summary>
        /// Gets the name of the mangas plugin
        /// </summary>
        public override string Name
        {
            get
            {
                return "Mangas";
            }
        }

        /// <summary>
        /// Loads the mangas plugin
        /// </summary>
        public override void Load()
        {
            task_ = new MangasTask();

            Tasks.AddAutoTask(task_);

            TrayIcon.AddItem(kMangas);

            TrayIcon.ItemClicked += TrayIconItemClicked;
        }

        protected void TrayIconItemClicked(object sender, Utils.DataEventArgs<string> e)
        {
            if (e.Data == kMangas && !open_)
            {
                open_ = true;
                Windows.ShowDialog(new MangasViewModel(Windows, task_));
                open_ = false;
            }
        }

        protected const string kMangas = "Mangas";
        protected bool open_;
        protected MangasTask task_;
    }
}
