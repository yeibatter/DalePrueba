using System.Net.Http;

namespace daleweb.Interfaces
{
    public interface IApiProxy
    {
        /// <summary>
        /// Procesa el api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="method"></param>
        /// <param name="apiEndPoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        T ProcessAPICall<T, V>(HttpMethod method, string apiEndPoint, V request) where T : new();


    }
}
