using Caliburn.Micro;
using MeliMelo.Common.Services.Configuration.Values;

namespace MeliMelo.ViewModels
{
    public interface IIntegerValueViewModelFactory
    {
        IntegerValueViewModel Create(string name, IntegerValue value);

        void Release(IntegerValueViewModel view_model);
    }

    /// <summary>
    /// Represents a view model wrapping an integer value
    /// </summary>
    public class IntegerValueViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Creates a new IntegerViewModel
        /// </summary>
        /// <param name="name">Name of the value wrapped</param>
        /// <param name="value">Value wrapped</param>
        public IntegerValueViewModel(string name, IntegerValue value)
        {
            name_ = name;
            value_ = value;
        }

        /// <summary>
        /// Gets the maximum value of the integer
        /// </summary>
        public int Maximum
        {
            get
            {
                return value_.Maximum;
            }
        }

        /// <summary>
        /// Gets the minimum value of the integer
        /// </summary>
        public int Minimum
        {
            get
            {
                return value_.Minimum;
            }
        }

        /// <summary>
        /// Gets the name of the value wrapped
        /// </summary>
        public string Name
        {
            get
            {
                return name_ + " (" + value_.Value + value_.Unit + ")";
            }
        }

        /// <summary>
        /// Gets the step between the integer values
        /// </summary>
        public int Step
        {
            get
            {
                return value_.Step;
            }
        }

        /// <summary>
        /// Gets/sets the value wrapped by this view model
        /// </summary>
        public int Value
        {
            get
            {
                return value_.Value;
            }
            set
            {
                if (value_.Value != value)
                {
                    value_.Value = value;
                    NotifyOfPropertyChange(() => Value);
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }

        /// <summary>
        /// Name of the wrapped value
        /// </summary>
        protected string name_;

        /// <summary>
        /// Value wrapped
        /// </summary>
        protected IntegerValue value_;
    }
}
