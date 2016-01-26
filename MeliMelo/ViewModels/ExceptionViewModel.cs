using System;
using System.Text;

namespace MeliMelo.ViewModels
{
    internal class ExceptionViewModel : Caliburn.Micro.Screen
    {
        public ExceptionViewModel(Exception exception)
        {
            DisplayName = "MeliMelo - Whoops!";

            exception_ = exception;
        }

        public string ExceptionStackTrace
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine("Error message: " + exception_.Message);
                builder.Append(exception_.StackTrace);

                return builder.ToString();
            }
        }

        public string Message
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine("Seems like an error has occured in the application.");
                builder.AppendLine("Please contact the application author and send him the"
                    + " content of the text area below");

                return builder.ToString();
            }
        }

        /// <summary>
        /// Called when the user presses the close button
        /// </summary>
        public void Shutdown()
        {
            TryClose(true);
        }

        protected Exception exception_;
    }
}
