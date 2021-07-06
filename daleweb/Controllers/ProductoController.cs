using Dale.Model;
using daleweb.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;

namespace daleweb.Controllers
{
    [Route("Producto")]
    [ApiController]
    public class ProductoController : Controller
    {
        /// <summary>
        /// IConfiguration
        /// </summary>
        private IConfiguration _config;

        /// <summary>
        /// Acceso a Logger        
        /// </summary>
        private readonly ILogger<ProductoController> _logger;

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
        public ProductoController(IConfiguration config, ILogger<ProductoController> logger, IApiProxy apiProxy)
        {
            _config = config;
            _logger = logger;
            _ApiProxy = apiProxy;

            //Url del Backend
            urlBackend = config.GetConnectionString("BackendUrl");
        }


        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                urlBackend += "Producto/GetAll";

                List<Dale.Model.Producto> obRet = _ApiProxy.ProcessAPICall<List<Dale.Model.Producto>, string>(HttpMethod.Get, urlBackend, null);
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

        /// <summary>
        /// AddProducto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("AddProducto")]
        [HttpPost]
        public IActionResult AddProducto(Dale.Model.Producto id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                urlBackend += "Producto/AddProducto";

                Response obRet = _ApiProxy.ProcessAPICall<Response, Dale.Model.Producto>(HttpMethod.Post, urlBackend, id);
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
