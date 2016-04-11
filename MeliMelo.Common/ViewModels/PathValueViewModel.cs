using Caliburn.Micro;
using MeliMelo.Common.Services.Configuration.Values;
using System.Windows.Forms;

namespace MeliMelo.ViewModels
{
    public interface IPathValueViewModelFactory
    {
        PathValueViewModel Create(string name, PathValue value);

        void Release(PathValueViewModel view_model);
    }

    public class PathValueViewModel : PropertyChangedBase
    {
        public PathValueViewModel(string name, PathValue value)
        {
            name_ = name;
            value_ = value;
        }

        public string Name
        {
            get
            {
                return name_ + " (" + value_.Value + ")";
            }
        }

        public string Path
        {
            get
            {
                return value_.Value;
            }
            set
            {
                value_.Value = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void Browse()
        {
            var dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Path = dialog.SelectedPath;
            }
        }

        protected string name_;

        protected PathValue value_;
    }
}
