using MeliMelo.Core.Configuration.Values;
using MeliMelo.Core.Tasks;

namespace MeliMelo.Screen
{
    public class ScreenTask : TimedTask
    {
        public ScreenTask(IntegerValue temperature, IntegerValue interval)
        {
            temperature_ = temperature;
            interval_ = interval;

            temperature_.ValueChanged += TemperatureValueChanged;
            interval_.ValueChanged += IntervalValueChanged;

            Interval = interval_.Value;
        }

        public override string Name
        {
            get
            {
                return "Screen";
            }
        }

        public override void Run()
        {
            Temperature.Set(temperature_.Value);
        }

        protected void IntervalValueChanged(object sender, Utils.DataEventArgs<int> e)
        {
            Interval = e.Data;
        }

        protected override void OnStart()
        {
            Temperature.Set(temperature_.Value);
        }

        protected override void OnStop()
        {
            Temperature.Reset();
        }

        protected void TemperatureValueChanged(object sender, Utils.DataEventArgs<int> e)
        {
            Temperature.Set(e.Data);
        }

        protected IntegerValue interval_;

        protected IntegerValue temperature_;
    }
}
