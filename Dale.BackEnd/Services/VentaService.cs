using Dale.BackEnd.Context;
using Dale.BackEnd.Interfaces;
using Dale.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Dale.BackEnd.Services
{
    public class VentaService : IVentaService
    {
        /// <summary>
        /// Config
        /// </summary>
        private IConfiguration _config;

        /// <summary>
        /// Acceso a Loggin
        /// </summary>
        private ILogger _logger { get; }


        /// <summary>
        /// Service Context
        /// </summary>
        internal readonly DaleContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <returns></returns>
        public VentaService(IConfiguration config, IServiceCollection services)
        {
            this._config = config;

            //Service Provider
            var serviceProvider = services.BuildServiceProvider();

            //Contexto DB
            // _context = new DaleContext(config.GetConnectionString("DaleContext"));

            _context = serviceProvider.GetService<DaleContext>();

        }

        /// <summary>
        /// CrearVenta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Response CrearVenta(Dale.Model.Venta item)
        {
            Response stRet = new Response();

            System.Guid idDb = System.Guid.NewGuid();
            string stRetItem = idDb.ToString();
            System.Guid cliID = System.Guid.Parse(item.ClienteId);

            //Genera objeto tipo Db
            DBModel.Ventum clienteDB = new DBModel.Ventum()
            {
                VnId = idDb,
                CliId = cliID,
                VnFecha = DateTime.Now,
                VnTotalFactura = item.TotalFactura,
                DetalleVenta = null
            };

            _context.Venta.Add(clienteDB);


            List<DBModel.DetalleVentum> lstDetalle = new List<DBModel.DetalleVentum>();
            foreach (Dale.Model.DetalleVenta itemDetalle in item.Detalle)
            {
                DBModel.DetalleVentum dbItem = new DBModel.DetalleVentum()
                {
                    DvCantidad = itemDetalle.Cantidad,
                    PrdId = System.Guid.Parse(itemDetalle.ProductId),
                    DvId = System.Guid.NewGuid(),
                    VnId = idDb
                };

                _context.DetalleVenta.Add(dbItem);
            }

            _context.SaveChanges();
            //Retorna el ID
            return stRet;
        }
    }
}
