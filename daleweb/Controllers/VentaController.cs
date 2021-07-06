using Dale.Model;
using daleweb.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;


namespace daleweb.Controllers
{
    [Route("Venta")]
    [ApiController]
    public class VentaController : Controller
    {
        /// <summary>
        /// IConfiguration
        /// </summary>
        private IConfiguration _config;

        /// <summary>
        /// Acceso a Logger        
        /// </summary>
        private readonly ILogger<VentaController> _logger;

        /// <summary>
        /// Abis Negocio
        /// </summary>
        private readonly IApiProxy _ApiProxy;

        /// <summary>
        /// Backend
        /// </summary>
        private string urlBackend;

        /// <summary>
        /// Constructor del Controller
        /// </summary>
        /// <param name="config">DI configuracion</param>
        /// <param name="logger">DI Logger</param>
        public VentaController(IConfiguration config, ILogger<VentaController> logger, IApiProxy apiProxy)
        {
            _config = config;
            _logger = logger;
            _ApiProxy = apiProxy;

            //Url del Backend
            urlBackend = config.GetConnectionString("BackendUrl");

        }

        /// <summary>
        /// AddProducto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("AddVenta")]
        [HttpPost]
        public IActionResult AddVenta(Dale.Model.Venta id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                urlBackend += "Venta/AddVenta";

                Response obRet = _ApiProxy.ProcessAPICall<Response, Dale.Model.Venta>(HttpMethod.Post, urlBackend, id);
                oResult = StatusCode(StatusCodes.Status200OK, obRet);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                oResult = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            //Retorno el Token
            return oResult;

        }
    }
}
