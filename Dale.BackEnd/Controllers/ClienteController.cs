using Dale.BackEnd.Interfaces;
using Dale.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace Dale.BackEnd.Controllers
{
    [Route("Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
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
        /// Negocio
        /// </summary>
        private readonly IClienteService _clienteService;

        /// <summary>
        /// Constructor del Controller
        /// </summary>
        /// <param name="config">DI configuracion</param>
        /// <param name="logger">DI Logger</param>
        public ClienteController(IConfiguration config, ILogger<ClienteController> logger, IClienteService clienteService)
        {
            _config = config;
            _logger = logger;
            _clienteService = clienteService;
        }

        /// <summary>
        /// GetAllClientes
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(IList<Dale.Model.Cliente>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult GetAllClientes()
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                IList<Dale.Model.Cliente> obRet = _clienteService.GetAllClientes();
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
        /// Get by Document
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Route("GetByDocument/{documentNumber}")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(Dale.Model.Cliente))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult GetByDocument(string documentNumber)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                Dale.Model.Cliente obRet = _clienteService.GetByDocument(documentNumber);
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
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("UpdateCliente")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(IList<Dale.Model.Cliente>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult UpdateCliente(Dale.Model.Cliente id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                string sItem = _clienteService.UpdateCliente(id);
                oResult = StatusCode(StatusCodes.Status200OK, sItem);
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
        /// Add Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("AddCliente")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(IList<Dale.Model.Response>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult AddCliente(Dale.Model.Cliente id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                Response obResponse = new Response();
                obResponse.Mensaje = _clienteService.AddCliente(id);
                obResponse.Status = true;

                oResult = StatusCode(StatusCodes.Status200OK, obResponse);
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
        /// delete Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteClient")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(IList<Dale.Model.Cliente>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult DeleteClient(Dale.Model.Cliente id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                 _clienteService.DeleteClient(id);

                string sItem = "OK";
                oResult = StatusCode(StatusCodes.Status200OK, sItem);
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
