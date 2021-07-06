namespace Dale.BackEnd.Interfaces
{
    public interface IVentaService
    {
        /// <summary>
        /// CrearVenta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Model.Response CrearVenta(Dale.Model.Venta item);
    }
}
