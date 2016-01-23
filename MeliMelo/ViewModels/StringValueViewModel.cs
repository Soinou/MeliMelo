using Caliburn.Micro;
using MeliMelo.Core.Configuration.Values;

namespace MeliMelo.ViewModels
{
    internal class StringValueViewModel : PropertyChangedBase
    {
        public StringValueViewModel(string name, StringValue string_value)
        {
            name_ = name;
            string_value_ = string_value;
        }

        public string Name
        {
            get
            {
                return name_;
            }
        }

        public string Value
        {
            get
            {
                return string_value_.Value;
            }
            set
            {
                if (string_value_.Value != value)
                {
                    string_value_.Value = value;
                    NotifyOfPropertyChange(() => Value);
                }
            }
        }

        protected string name_;

        protected StringValue string_value_;
    }
}
