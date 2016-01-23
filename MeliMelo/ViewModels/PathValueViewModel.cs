using Caliburn.Micro;
using MeliMelo.Core.Configuration.Values;
using System.Windows.Forms;

namespace MeliMelo.ViewModels
{
    internal class PathValueViewModel : PropertyChangedBase
    {
        public PathValueViewModel(string name, PathValue path_value)
        {
            name_ = name;
            path_value_ = path_value;
        }

        public string Name
        {
            get
            {
                return name_ + " (" + path_value_.Value + ")";
            }
        }

        public string Path
        {
            get
            {
                return path_value_.Value;
            }
            set
            {
                path_value_.Value = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void Search()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Path = dialog.SelectedPath;
            }
        }

        protected string name_;

        protected PathValue path_value_;
    }
}
