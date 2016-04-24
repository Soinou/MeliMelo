using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeliMelo.Core.Helpers
{
    /// <summary>
    /// Represents an HTTP helper
    /// </summary>
    public static class Http
    {
        /// <summary>
        /// Retrieves a string from the given url
        /// </summary>
        /// <param name="url">Url to retrieve from</param>
        /// <returns>A string</returns>
        public static async Task<string> Get(string url)
        {
            var methods = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var handler = new HttpClientHandler { AutomaticDecompression = methods })
            using (var client = new HttpClient(handler))
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
