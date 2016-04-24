using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeliMelo.Ui.Controls
{
    public class FastScrollViewer : ScrollViewer
    {
        static FastScrollViewer()
        {
            ScrollSpeedProperty = DependencyProperty.Register("ScrollSpeed", typeof(double), typeof(FastScrollViewer));
        }

        public FastScrollViewer()
        {
            SetValue(IsDeferredScrollingEnabledProperty, true);
        }

        public double ScrollSpeed
        {
            get
            {
                return (double)GetValue(ScrollSpeedProperty);
            }
            set
            {
                SetValue(ScrollSpeedProperty, value);
            }
        }

        public static DependencyProperty ScrollSpeedProperty;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            ScrollToVerticalOffset(VerticalOffset - (e.Delta * (double)GetValue(ScrollSpeedProperty)));
        }
    }
}
