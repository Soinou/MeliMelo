namespace MeliMelo.Utils.Log
{
    /// <summary>
    /// Represents a logger interface
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="where"></param>
        /// <param name="what"></param>
        void Error(string where, string what);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="where"></param>
        /// <param name="what"></param>
        void Info(string where, string what);

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="where"></param>
        /// <param name="what"></param>
        void Warning(string where, string what);
    }
}
