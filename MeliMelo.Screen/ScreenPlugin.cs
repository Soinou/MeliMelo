using MeliMelo.Core.Configuration.Values;
using MeliMelo.Core.Plugins;

namespace MeliMelo.Screen
{
    public class ScreenPlugin : PluginBase
    {
        public override string Name
        {
            get
            {
                return "Screen";
            }
        }

        public override void Load()
        {
            IntegerValue temperature = Configuration.GetInteger("Screen.Temperature", "K",
                2300, 1100, 25100, 100);
            IntegerValue interval = Configuration.GetInteger("Screen.Interval", "ms",
                1000, 100, 10000, 100);

            Tasks.AddAutoTask(new ScreenTask(temperature, interval));
        }
    }
}
