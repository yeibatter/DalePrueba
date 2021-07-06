using System.Collections.Generic;

namespace Dale.BackEnd.Interfaces
{
    public interface IClienteService
    {
        /// <summary>
        /// Get All Clientes
        /// </summary>
        /// <returns></returns>
        IList<Dale.Model.Cliente> GetAllClientes();

        /// <summary>
        /// Retorna un Cliente dado el Documento
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Dale.Model.Cliente GetByDocument(string document);

        /// <summary>
        /// Add Cliente
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string AddCliente(Dale.Model.Cliente item);


        /// <summary>
        /// Update Cliente
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string UpdateCliente(Dale.Model.Cliente item);

        /// <summary>
        /// Delete Cliente
        /// </summary>
        /// <param name="item"></param>
        void DeleteClient(Dale.Model.Cliente item);
    }
}
