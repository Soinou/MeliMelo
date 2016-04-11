using System.IO;
using System.Windows.Forms;

namespace MeliMelo.ViewModels
{
    public interface IAddLibraryViewModelFactory
    {
        AddLibraryViewModel Create();

        void Release(AddLibraryViewModel view_model);
    }

    public class AddLibraryViewModel : Caliburn.Micro.Screen
    {
        public AddLibraryViewModel()
        {
            DisplayName = "MeliMelo - Add Library";

            input_ = "";
            name_ = "";
        }

        public bool CanAdd
        {
            get
            {
                return !(string.IsNullOrEmpty(name_)
                    || string.IsNullOrEmpty(input_)
                    || string.IsNullOrEmpty(output_));
            }
        }

        public string Input
        {
            get
            {
                return input_;
            }
        }

        public string LibraryInput
        {
            get
            {
                if (string.IsNullOrEmpty(input_))
                {
                    return "Input (No directory set)";
                }
                else
                {
                    return "Input (" + input_ + ")";
                }
            }
            set
            {
                if (input_ != value)
                {
                    input_ = value;
                    NotifyOfPropertyChange(() => LibraryInput);
                    NotifyOfPropertyChange(() => CanAdd);
                }
            }
        }

        public string LibraryName
        {
            get
            {
                return name_;
            }
            set
            {
                if (name_ != value)
                {
                    name_ = value;
                    NotifyOfPropertyChange(() => LibraryName);
                    NotifyOfPropertyChange(() => CanAdd);
                }
            }
        }

        public string LibraryOutput
        {
            get
            {
                if (string.IsNullOrEmpty(output_))
                {
                    return "Output (No directory set)";
                }
                else
                {
                    return "Output (" + output_ + ")";
                }
            }
            set
            {
                if (output_ != value)
                {
                    output_ = value;
                    NotifyOfPropertyChange(() => LibraryOutput);
                    NotifyOfPropertyChange(() => CanAdd);
                }
            }
        }

        public string Name
        {
            get
            {
                return name_;
            }
        }

        public string Output
        {
            get
            {
                return output_;
            }
        }

        public void Add()
        {
            TryClose(true);
        }

        public void BrowseInput()
        {
            var dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK && Directory.Exists(dialog.SelectedPath))
            {
                LibraryInput = dialog.SelectedPath;
            }
        }

        public void BrowseOutput()
        {
            var dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK && Directory.Exists(dialog.SelectedPath))
            {
                LibraryOutput = dialog.SelectedPath;
            }
        }

        public void Cancel()
        {
            TryClose(false);
        }

        protected string input_;

        protected string name_;

        protected string output_;
    }
}
