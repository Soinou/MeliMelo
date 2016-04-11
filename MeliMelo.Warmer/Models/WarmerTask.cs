using MeliMelo.Common.Services.Configuration;
using MeliMelo.Common.Services.Configuration.Values;
using MeliMelo.Common.Utils;

namespace MeliMelo.Warmer.Models
{
    public class WarmerTask : TimedTask
    {
        public WarmerTask(IConfiguration configuration)
        {
            temperature_ = configuration.GetInteger("Temperature");
            interval_ = configuration.GetInteger("Interval");

            temperature_.ValueChanged += TemperatureValueChanged;
            interval_.ValueChanged += IntervalValueChanged;

            Interval = interval_.Value;
        }

        public override void Run()
        {
            Temperature.Set(temperature_.Value);
        }

        protected void IntervalValueChanged(object sender, DataEventArgs<int> e)
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

        protected void TemperatureValueChanged(object sender, DataEventArgs<int> e)
        {
            Temperature.Set(e.Data);
        }

        protected IntegerValue interval_;

        protected IntegerValue temperature_;
    }
}
