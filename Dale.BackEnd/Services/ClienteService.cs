using Dale.BackEnd.Context;
using Dale.BackEnd.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dale.BackEnd.Services
{
    /// <summary>
    /// Cliente
    /// </summary>
    public class ClienteService : IClienteService
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
        public ClienteService(IConfiguration config, IServiceCollection services)
        {
            this._config = config;

            //Service Provider
            var serviceProvider = services.BuildServiceProvider();

            //Contexto DB
            // _context = new DaleContext(config.GetConnectionString("DaleContext"));

            _context = serviceProvider.GetService<DaleContext>();

        }

        /// <summary>
        /// Get All Clientes
        /// </summary>
        /// <returns></returns>
        public IList<Dale.Model.Cliente> GetAllClientes()
        {

            //Retorna todos los clientes de la Bd   
            List<DBModel.Cliente> lstClientesDB = _context.Clientes.ToList();
            return ConvertFromModel(lstClientesDB);
        }

        /// <summary>
        /// Retorna un Cliente dado el Documento
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Dale.Model.Cliente GetByDocument(string document)
        {
            //Retorna todos los clientes de la Bd   
            DBModel.Cliente itemCliente = _context.Clientes.Where(b => b.CliDocumento == document).FirstOrDefault();
            return ConvertFromModel(itemCliente);
        }

        /// <summary>
        /// Add Cliente
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddCliente(Dale.Model.Cliente item) 
        {
            string stRet = string.Empty;
            Dale.Model.Cliente obCliente = GetByDocument(item.Documento);

            //Si el Item entrante no es null
            //Si el item no existe en la bd
            if (obCliente?.Id == null)
            {
                System.Guid idDb = System.Guid.NewGuid();
                stRet = idDb.ToString();


                //Genera objeto tipo Db
                DBModel.Cliente clienteDB = new DBModel.Cliente()
                {
                    CliId = idDb,
                    CliApellidos = item.Apellidos,
                    CliDocumento = item.Documento,
                    CliNombres = item.Nombres,
                    CliNumeroTelefono = item.NumeroTelefono,
                };

                _context.Clientes.Add(clienteDB);
                _context.SaveChanges();
            }

            //Retorna el ID
            return stRet;
        }

        /// <summary>
        /// Update Cliente
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateCliente(Dale.Model.Cliente item)
        {
            string stRet = string.Empty;
            Dale.Model.Cliente obCliente = GetByDocument(item.Documento);

            //Si el Item entrante no es null
            //Si el item existe en la bd
            if (item != null && obCliente != null)
            {
               
                //Genera objeto tipo Db
                DBModel.Cliente clienteDB = new DBModel.Cliente()
                {
                    CliId = System.Guid.Parse( obCliente.Id),
                    CliApellidos = item.Apellidos,
                    CliDocumento = item.Documento,
                    CliNombres = item.Nombres,
                    CliNumeroTelefono = item.NumeroTelefono,
                };

                _context.Clientes.Update(clienteDB);
                _context.SaveChanges();
            }

            //Retorna el ID
            return stRet;
        }

        /// <summary>
        /// Delete Cliente
        /// </summary>
        /// <param name="item"></param>
        public void DeleteClient(Dale.Model.Cliente item)
        {
            DBModel.Cliente obCliente = _context.Clientes.Where(d => d.CliDocumento == item.Documento).First();

            if (obCliente != null)
            {
                _context.Clientes.Remove(obCliente);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// ConvertFromModel
        /// </summary>
        /// <param name="dbItems"></param>
        /// <returns></returns>
        private IList<Dale.Model.Cliente> ConvertFromModel(List<DBModel.Cliente> dbItems)
        {
            List<Dale.Model.Cliente> lstFaces = new List<Model.Cliente>();

            if (dbItems != null)
            {
                foreach (DBModel.Cliente dbItem in dbItems)
                {
                    lstFaces.Add(ConvertFromModel(dbItem));
                }
            }

            return lstFaces;
        }

        /// <summary>
        /// Convierte a Objeto Ciente
        /// </summary>
        /// <param name="dbItem"></param>
        /// <returns></returns>
        private Dale.Model.Cliente ConvertFromModel(DBModel.Cliente dbItem)
        {
            Dale.Model.Cliente itemRet = new  Model.Cliente();

            if (dbItem != null)
            {
                itemRet = new  Model.Cliente
                {
                    Id = dbItem.CliId.ToString(),
                    Apellidos = dbItem.CliApellidos,
                    Nombres = dbItem.CliNombres,
                    Documento = dbItem.CliDocumento,
                    NumeroTelefono = dbItem.CliNumeroTelefono
                };
            }

            return itemRet;
        }
    }
}
