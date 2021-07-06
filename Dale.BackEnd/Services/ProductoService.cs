using Dale.BackEnd.Context;
using Dale.BackEnd.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Dale.BackEnd.Services
{
    public class ProductoService : IProductoService
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
        public ProductoService(IConfiguration config, IServiceCollection services)
        {
            this._config = config;

            //Service Provider
            var serviceProvider = services.BuildServiceProvider();

            //Contexto DB
            // _context = new DaleContext(config.GetConnectionString("DaleContext"));

            _context = serviceProvider.GetService<DaleContext>();

        }

        /// <summary>
        /// GetAllProducto
        /// </summary>
        /// <returns></returns>
        public IList<Dale.Model.Producto> GetAllProducto()
        {

            //Retorna todos los clientes de la Bd   
            List<DBModel.Producto> lstDB = _context.Productos.ToList();
            return ConvertFromModel(lstDB);
        }

        /// <summary>
        /// GetByNombre
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Dale.Model.Producto GetByNombre(string producto)
        {
            //Retorna todos los clientes de la Bd   
            DBModel.Producto itemProd = _context.Productos.Where(b => b.PrdNombre == producto).FirstOrDefault();
            return ConvertFromModel(itemProd);
        }

        /// <summary>
        /// AddProducto
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddProducto(Dale.Model.Producto item)
        {
            string stRet = string.Empty;
            Dale.Model.Producto obCliente = GetByNombre(item.Nombre);

            //Si el Item entrante no es null
            //Si el item no existe en la bd
            if (obCliente?.Id == null)
            {
                System.Guid idDb = System.Guid.NewGuid();
                stRet = idDb.ToString();


                //Genera objeto tipo Db
                DBModel.Producto clienteDB = new DBModel.Producto()
                {
                    PrdId = idDb,
                    PrdNombre = item.Nombre,
                    PrdValor = item.Valor 
                };

                _context.Productos.Add(clienteDB);
                _context.SaveChanges();
            }

            //Retorna el ID
            return stRet;
        }


        /// <summary>
        /// ConvertFromModel
        /// </summary>
        /// <param name="dbItems"></param>
        /// <returns></returns>
        private IList<Dale.Model.Producto> ConvertFromModel(List<DBModel.Producto> dbItems)
        {
            List<Dale.Model.Producto> lstFaces = new List<Model.Producto>();

            if (dbItems != null)
            {
                foreach (DBModel.Producto dbItem in dbItems)
                {
                    lstFaces.Add(ConvertFromModel(dbItem));
                }
            }

            return lstFaces;
        }


        /// <summary>
        /// ConvertFromModel
        /// </summary>
        /// <param name="dbItem"></param>
        /// <returns></returns>
        private Dale.Model.Producto ConvertFromModel(DBModel.Producto dbItem)
        {
            Dale.Model.Producto itemRet = new Model.Producto();

            if (dbItem != null)
            {
                itemRet = new Model.Producto
                {
                    Id = dbItem.PrdId.ToString(),
                    Nombre = dbItem.PrdNombre,
                    Valor = dbItem.PrdValor 
                };
            }

            return itemRet;
        }
    }
}
