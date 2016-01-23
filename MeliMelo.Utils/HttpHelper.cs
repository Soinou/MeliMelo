using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeliMelo.Utils
{
    /// <summary>
    /// Represents an HTTP helper
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Retrieves a string from the given url
        /// </summary>
        /// <param name="url">Url to retrieve from</param>
        /// <returns>A string</returns>
        public static async Task<string> Get(string url)
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }))
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
