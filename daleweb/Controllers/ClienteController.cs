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
    [Route("Cliente")]
    [ApiController]
    public class ClienteController : Controller
    {
        /// <summary>
        /// IConfiguration
        /// </summary>
        private IConfiguration _config;

        /// <summary>
        /// Acceso a Logger        
        /// </summary>
        private readonly ILogger<ClienteController> _logger;

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
        public ClienteController(IConfiguration config, ILogger<ClienteController> logger, IApiProxy apiProxy)
        {
            _config = config;
            _logger = logger;
            _ApiProxy = apiProxy;

            //Url del Backend
            urlBackend = config.GetConnectionString("BackendUrl");
        }

        /// <summary>
        /// GetAllClientes
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAllClientes()
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                urlBackend += "Cliente/GetAll";

                List<Dale.Model.Cliente> obRet = _ApiProxy.ProcessAPICall<List<Dale.Model.Cliente>, string>(HttpMethod.Get, urlBackend, null);
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

        [Route("AddCliente")]
        [HttpPost]
        public IActionResult AddCliente(Dale.Model.Cliente id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                urlBackend += "Cliente/AddCliente";

                Response obRet = _ApiProxy.ProcessAPICall<Response, Dale.Model.Cliente>(HttpMethod.Post, urlBackend, id);
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
