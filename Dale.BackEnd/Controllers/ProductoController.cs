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
    [Route("Producto")]
    [ApiController]
    public class ProductoController : ControllerBase
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
        private readonly IProductoService _productoService;

        /// <summary>
        /// Constructor del Controller
        /// </summary>
        /// <param name="config">DI configuracion</param>
        /// <param name="logger">DI Logger</param>
        public ProductoController(IConfiguration config, ILogger<ClienteController> logger, IProductoService productoService)
        {
            _config = config;
            _logger = logger;
            _productoService = productoService;
        }

        /// <summary>
        /// GetAllClientes
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(IList<Dale.Model.Producto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult GetAllProducto()
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                IList<Dale.Model.Producto> obRet = _productoService.GetAllProducto();
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
        [SwaggerResponse(StatusCodes.Status200OK, "GetUserByDocument", typeof(IList<Dale.Model.Response>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult AddProducto(Dale.Model.Producto id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                Response obResponse = new Response();
                obResponse.Mensaje = _productoService.AddProducto(id);
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
    }
}
