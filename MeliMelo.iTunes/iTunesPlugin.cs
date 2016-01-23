using iTunesLib;
using MeliMelo.Core.Plugins;
using System;

namespace MeliMelo.iTunes
{
    public class iTunesPlugin : PluginBase
    {
        public iTunesPlugin()
        {
            app_ = new iTunesApp();
        }

        public override string Name
        {
            get
            {
                return "iTunes";
            }
        }

        public override void Load()
        {
            iTunesTask task = new iTunesTask(Keyboard);

            task.OnNext += TaskOnNext;
            task.OnPrevious += TaskOnPrevious;
            task.OnPlayPause += TaskOnPlayPause;

            Tasks.AddAutoTask(task);
        }

        protected void TaskOnNext(object sender, EventArgs e)
        {
            app_.NextTrack();
        }

        protected void TaskOnPlayPause(object sender, EventArgs e)
        {
            if (app_.PlayerState == ITPlayerState.ITPlayerStatePlaying)
                app_.Pause();
            else
                app_.Play();
        }

        protected void TaskOnPrevious(object sender, EventArgs e)
        {
            app_.PreviousTrack();
        }

        protected iTunesApp app_;
    }
}
