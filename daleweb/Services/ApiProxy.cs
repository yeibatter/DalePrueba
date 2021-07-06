using daleweb.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace daleweb.Services
{
    public class ApiProxy : IApiProxy
    {

        /// <summary>
        /// Backend url
        /// </summary>
        private readonly string urlBackend;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <returns></returns>
        public ApiProxy(IConfiguration config)
        {
            //Url del Backend
            urlBackend = config.GetConnectionString("BackendUrl");
        }

        /// <summary>
        /// ProcessAPICall
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="method"></param>
        /// <param name="apiEndPoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public T ProcessAPICall<T, V>(HttpMethod method,
                                      string apiEndPoint,
                                      V request) where T : new()
        {
            //JsonContent
            string jsonContent = string.Empty;

            //Serializo el Objeto Si viene
            if (request != null)
            {
                jsonContent = SerializeNullValues(request);
            }

            return ProcessAPICall<T>(method, apiEndPoint, jsonContent);
        }

        /// <summary>
        /// ProcessAPICall
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="apiEndPoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private T ProcessAPICall<T>(HttpMethod method,
                                      string apiEndPoint,
                                      string request) where T : new()
        {
            //Resultado del llamado
            T obRetorno = new T();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return DateTime.Now.Ticks > 1; };
                //Cliente http
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    //Genero la URL dada la direccion plana
                    Uri webService = new Uri(apiEndPoint);

                    //Using para Generar el Cliente http
                    httpClient.BaseAddress = webService;

                    //TimeOut
                    TimeSpan oTimeOut = new TimeSpan(0, 0, 50);

#if DEBUG
                    oTimeOut = new TimeSpan(0, 5, 0);
#endif

                    //Asigno el Time Out de 2 min
                    httpClient.Timeout = oTimeOut;

                    //Forzar para usar TLS12
                    //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls

                    try
                    {
                        //Conenido JSON
                        string jsonContent = string.Empty;
                        string sMensaje = string.Empty;

                        //Genero el mensaje request
                        using (HttpRequestMessage requestMessage = new HttpRequestMessage(method, webService))
                        {
                          
                            //Normalizo
                            jsonContent = (string.IsNullOrEmpty(request) ? string.Empty : request);

                            //Mensaje de Envio                    
                            // fachada.Logger.LogInformation("Json Send ->" + jsonContent);

                            //Si no viene Vacio
                            if (!string.IsNullOrEmpty(jsonContent))
                            {
                                //Asigno el Mensaje si no viene null o Empty                    
                                requestMessage.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                            }

                            //Envio la solicitud al servidor y espero la tarea
                            using (Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, CancellationToken.None))
                            {
                                //Obtengo la respuesta!
                                using (HttpResponseMessage httpResponse = httpRequest.Result)
                                {
                                    //Contenido de Response
                                    using (HttpContent responseContent = httpResponse.Content)
                                    {
                                        //Si es un StatusCode 200 a 299 - OK!
                                        if (httpResponse.StatusCode == HttpStatusCode.OK ||
                                            httpResponse.StatusCode == HttpStatusCode.Created ||
                                            httpResponse.StatusCode == HttpStatusCode.Accepted)
                                        {
                                           
                                          
                                        }

                                        //Obtengo el Response Content
                                        using (Task<Stream> tResponse = responseContent.ReadAsStreamAsync())
                                        {
                                            //Obtengo el Resultado
                                            Stream sResponse = tResponse.Result;

                                            //Obtento el reader del Content
                                            using (StreamReader sr = new StreamReader(sResponse))
                                            {
                                                //Transforma el reader a String!
                                                sMensaje = sr.ReadToEnd();
                                            }
                                        }

                                        //Retorno el objeto
                                        obRetorno = JsonConvert.DeserializeObject<T>(sMensaje);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            //Retorno
            return obRetorno;
        }

        /// <summary>
        /// SerializeNullValues
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string SerializeNullValues(object o)
        {
            return JsonConvert.SerializeObject(o, Newtonsoft.Json.Formatting.Indented,
                             new JsonSerializerSettings
                             {
                                 NullValueHandling = NullValueHandling.Ignore
                             });
        }
    }
}
