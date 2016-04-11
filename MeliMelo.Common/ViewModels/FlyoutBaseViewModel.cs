using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace MeliMelo.ViewModels
{
    public abstract class FlyoutBaseViewModel : PropertyChangedBase
    {
        public string Header
        {
            get
            {
                return header_;
            }

            set
            {
                if (value != header_)
                {
                    header_ = value;
                    NotifyOfPropertyChange(() => Header);
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return is_open_;
            }

            set
            {
                if (!value.Equals(is_open_))
                {
                    is_open_ = value;
                    NotifyOfPropertyChange(() => IsOpen);
                }
            }
        }

        public Position Position
        {
            get
            {
                return position_;
            }

            set
            {
                if (value != position_)
                {
                    position_ = value;
                    NotifyOfPropertyChange(() => Position);
                }
            }
        }

        private string header_;

        private bool is_open_;

        private Position position_;
    }
}
