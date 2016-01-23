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

        protected override void OnStart()
        {
            Temperature.Set(temperature_.Value);
        }

        protected override void OnStop()
        {
            Temperature.Reset();
        }

        protected IntegerValue interval_;
        protected IntegerValue temperature_;
    }
}
