using System.Collections.Generic;
using System.Timers;

namespace MeliMelo.Utils.Log
{
    /// <summary>
    /// Represents a log manager
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// Gets the instance of the log manager
        /// </summary>
        public static LogManager Instance
        {
            get
            {
                if (instance_ == null)
                    instance_ = new LogManager();

                return instance_;
            }
        }

        /// <summary>
        /// Gets the logger with the given name
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>The logger</returns>
        public ILog Get(string name)
        {
            LogImpl log = null;

            if (!loggers_.TryGetValue(name, out log))
            {
                log = new LogImpl(name);
                loggers_.Add(name, log);
            }

            return log;
        }

        /// <summary>
        /// Starts the logging operations
        /// </summary>
        public void Start()
        {
            if (!timer_.Enabled)
                timer_.Start();
        }

        /// <summary>
        /// Stops the logging operations
        /// </summary>
        public void Stop()
        {
            if (timer_.Enabled)
                timer_.Stop();

            foreach (LogImpl log in loggers_.Values)
                log.Flush();
        }

        /// <summary>
        /// Creates a new LogManager
        /// </summary>
        protected LogManager()
        {
            loggers_ = new Dictionary<string, LogImpl>();
            timer_ = new Timer();
            timer_.Interval = 1000;
            timer_.Elapsed += OnTimerElapsed;
        }

        /// <summary>
        /// Called when the timer has elapsed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        protected void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (LogImpl log in loggers_.Values)
                log.Flush();
        }

        /// <summary>
        /// Instance of the log manager
        /// </summary>
        protected static LogManager instance_;

        /// <summary>
        /// Dictionary of loggers
        /// </summary>
        protected Dictionary<string, LogImpl> loggers_;

        /// <summary>
        /// Write timer
        /// </summary>
        protected Timer timer_;
    }
}
