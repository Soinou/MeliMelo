using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeliMelo.Common.Controls
{
    public class FastScrollViewer : ScrollViewer
    {
        static FastScrollViewer()
        {
            SpeedFactorProperty = DependencyProperty.Register("SpeedFactor", typeof(double), typeof(FastScrollViewer));
        }

        public double SpeedFactor
        {
            get
            {
                return (double)GetValue(SpeedFactorProperty);
            }
            set
            {
                SetValue(SpeedFactorProperty, value);
            }
        }

        public static DependencyProperty SpeedFactorProperty;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            ScrollToVerticalOffset(VerticalOffset - (e.Delta * (double)GetValue(SpeedFactorProperty)));
        }
    }
}
