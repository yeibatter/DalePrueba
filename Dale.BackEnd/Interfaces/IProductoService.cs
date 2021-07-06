namespace Dale.BackEnd.Interfaces
{
    public interface IProductoService
    {

        /// <summary>
        /// GetAllProducto
        /// </summary>
        /// <returns></returns>
        System.Collections.Generic.IList<Dale.Model.Producto> GetAllProducto();

        /// <summary>
        /// GetByNombre
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Dale.Model.Producto GetByNombre(string producto);


        /// <summary>
        /// AddProducto
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string AddProducto(Dale.Model.Producto item);
    }
}
