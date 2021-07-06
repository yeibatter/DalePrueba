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
    [Route("Venta")]
    [ApiController]
    public class VentaController : ControllerBase
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
        private readonly IVentaService _ventaService;

        /// <summary>
        /// Constructor del Controller
        /// </summary>
        /// <param name="config">DI configuracion</param>
        /// <param name="logger">DI Logger</param>
        public VentaController(IConfiguration config, ILogger<ClienteController> logger, IVentaService ventaService)
        {
            _config = config;
            _logger = logger;
            _ventaService = ventaService;

        }


        /// <summary>
        /// Add Venta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("AddVenta")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "AddVenta", typeof(IList<Dale.Model.Response>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(string))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "NotAcceptable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound", typeof(string))]
        public IActionResult AddVenta(Dale.Model.Venta id)
        {
            ObjectResult oResult = StatusCode(StatusCodes.Status406NotAcceptable, "NotAcceptable");

            try
            {
                Response obResponse = _ventaService.CrearVenta(id);
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
