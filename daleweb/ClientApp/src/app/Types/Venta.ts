import { DetalleVenta } from "./DetalleVenta";

export class Venta 
{
    id: string;
    clienteId : string;
    totalFactura: number;
    detalle: DetalleVenta[];
}