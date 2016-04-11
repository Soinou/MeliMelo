using MeliMelo.Common.Helpers;
using System;
using System.Collections.Concurrent;

namespace MeliMelo.Common.Services.Logging
{
    /// <summary>
    /// Implementation of the ILog interface
    /// </summary>
    public class LogImpl : ILog
    {
        /// <summary>
        /// Creates a new LogImpl
        /// </summary>
        /// <param name="name">Name of the logger</param>
        public LogImpl(string name)
        {
            name_ = name;
            messages_ = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="where"></param>
        /// <param name="what"></param>
        public void Error(string where, string what)
        {
            Output("[Error](" + where + "): " + what);
        }

        /// <summary>
        /// Flushes the write operations of the logger
        /// </summary>
        public void Flush()
        {
            if (!writing_)
            {
                writing_ = true;

                if (messages_.Count > 0)
                {
                    DateTime date = DateTime.Now;
                    string short_date = date.ToString("yyyy-MM-dd");
                    string long_date = date.ToString("yyyy-MM-ddTHH:mm:ss");
                    string file_name = @"logs\" + name_ + "_" + short_date + ".log";
                    string path = IoHelper.GetDataPath(file_name);

                    while (messages_.Count > 0)
                    {
                        string message = null;

                        if (messages_.TryDequeue(out message))
                        {
                            string long_message = "[" + long_date + "]" + message;

                            IoHelper.Append(path, long_message + Environment.NewLine);
#if DEBUG
                            Console.WriteLine(long_message);
#endif
                        }
                    }
                }

                writing_ = false;
            }
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="where"></param>
        /// <param name="what"></param>
        public void Info(string where, string what)
        {
            Output("[Info](" + where + "): " + what);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="where"></param>
        /// <param name="what"></param>
        public void Warning(string where, string what)
        {
            Output("[Warning](" + where + "): " + what);
        }

        /// <summary>
        /// Outputs the given message to the log file
        /// </summary>
        /// <param name="message">Message</param>
        protected void Output(string message)
        {
            messages_.Enqueue(message);
        }

        /// <summary>
        /// Messages to write
        /// </summary>
        protected ConcurrentQueue<string> messages_;

        /// <summary>
        /// Logger name
        /// </summary>
        protected string name_;

        /// <summary>
        /// If the logger is currently writing
        /// </summary>
        protected bool writing_;
    }
}
