using System;
using System.Timers;

namespace MeliMelo.Core.Tasks
{
    /// <summary>
    /// Implementation of the IAutoTask using a Timer
    /// </summary>
    public abstract class TimedTask : IAutoTask, IDisposable
    {
        /// <summary>
        /// Creates a new TimedTask
        /// </summary>
        public TimedTask()
        {
            timer_ = new Timer();
            disposed_ = false;

            timer_.Interval = 1000;
            timer_.Elapsed += OnElapsed;
        }

        /// <summary>
        /// get/set the interval between calls to the Run method (In milliseconds)
        /// </summary>
        public double Interval
        {
            get
            {
                return timer_.Interval;
            }
            set
            {
                timer_.Interval = value;
            }
        }

        /// <summary>
        /// Gets the name of the task
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// Gets the state of the task
        /// </summary>
        public bool Running
        {
            get
            {
                return timer_.Enabled;
            }
        }

        /// <summary>
        /// Disposes of the task
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Called when the task is triggered and needs to do actual work
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            if (!timer_.Enabled)
            {
                OnStart();
                timer_.Start();
            }
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            if (timer_.Enabled)
            {
                OnStop();
                timer_.Stop();
            }
        }

        /// <summary>
        /// Disposes of the task
        /// </summary>
        /// <param name="disposing">If the GC is finalizing the object</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed_)
            {
                if (disposing)
                {
                    timer_.Dispose();
                }

                disposed_ = true;
            }
        }

        /// <summary>
        /// Called when the task is started
        /// </summary>
        protected virtual void OnStart()
        { }

        /// <summary>
        /// Called when the task is stopped
        /// </summary>
        protected virtual void OnStop()
        { }

        /// <summary>
        /// Called by the timer each Interval milliseconds
        /// </summary>
        /// <param name="sender">Sender (Ignored)</param>
        /// <param name="e">Arguments (Ignored)</param>
        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            Run();
        }

        /// <summary>
        /// If the object is disposed
        /// </summary>
        private bool disposed_;

        /// <summary>
        /// Internal timer
        /// </summary>
        private Timer timer_;
    }
}
