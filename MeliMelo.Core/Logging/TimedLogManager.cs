using MeliMelo.Core.Helpers;
using System.Collections.Generic;
using System.Timers;

namespace MeliMelo.Core.Logging
{
    /// <summary>
    /// Represents a log manager
    /// </summary>
    public class TimedLogManager : DisposableBase, ILogManager
    {
        /// <summary>
        /// Creates a new LogManager
        /// </summary>
        public TimedLogManager()
        {
            loggers_ = new Dictionary<string, FileLog>();
            timer_ = new Timer();

            timer_.Interval = 1000;
            timer_.Elapsed += OnTimerElapsed;
            timer_.Start();
        }

        /// <summary>
        /// Gets the logger with the given name
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>The logger</returns>
        public ILog Get(string name)
        {
            FileLog log = null;

            if (!loggers_.TryGetValue(name, out log))
            {
                log = new FileLog(name);
                loggers_.Add(name, log);
            }

            return log;
        }

        /// <inheritdoc />
        protected override void OnFinalize()
        {
            timer_.Stop();

            foreach (var log in loggers_.Values)
            {
                log.Flush();
            }

            timer_.Dispose();
        }

        /// <summary>
        /// Called when the timer has elapsed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var log in loggers_.Values)
            {
                log.Flush();
            }
        }

        /// <summary>
        /// Dictionary of loggers
        /// </summary>
        protected Dictionary<string, FileLog> loggers_;

        /// <summary>
        /// Write timer
        /// </summary>
        protected Timer timer_;
    }
}
