import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from 'src/app/services/ClienteService';
import { ProductoService } from 'src/app/services/ProductoService';
import { VentaService } from 'src/app/services/VentaService';
import { Cliente } from 'src/app/Types/Cliente';
import { DetalleVenta } from 'src/app/Types/DetalleVenta';
import { Producto } from 'src/app/Types/Producto';
import { Venta } from 'src/app/Types/Venta';

@Component({
  selector: 'app-add-venta',
  templateUrl: './add-venta.component.html',
  styleUrls: ['./add-venta.component.css']
})
export class AddVentaComponent implements OnInit {
  public selectedValue: string;
  public selectedName: string;

  public prodSelectedValue :string;
  public prodSelectedName :string;
  public prodCantidad :number;

  public ventaTotal :number;
  public idRow: number;
  public clientId : string;

  public loaded: boolean;
  public isSelectedClient: boolean;
  public clientes: Cliente[];
  public productos: Producto[];
  public detalleVenta : DetalleVenta[];

  constructor(private clienteService: ClienteService,
    private productoService: ProductoService,
    private ventaService: VentaService,
    private router: Router) { }

  ngOnInit() {
    this.detalleVenta = new Array();
    this. idRow++;
    this.clienteService.getAll().subscribe({
      next: (result: Cliente[]) => {
        if (result) {
          this.clientes = result;
          this.loaded = true;
        }
      },
      error: (error) => console.log(error)
    });

    this.productoService.getAll().subscribe({
      next: (result: Producto[]) => {
        if (result) {
          this.productos = result;
          this.loaded = true;
        }
      },
      error: (error) => console.log(error)
    });
  }

  //Selecciona el cliente
  public selectCliente() {
    if (this.selectedValue !== undefined) 
    {
      this.isSelectedClient = true;

      for (let index = 0; index < this.clientes.length; index++) {
        const element = this.clientes[index];

        if (element.id === this.selectedValue) {
          this.selectedName = element.nombres;
          this.clientId = element.id;
        }
      }
    }
  }

  //Adiciona Detalle
  public addDetalle() 
  {
    this. idRow++;
    let oTotal = 0;

      if (this.prodSelectedValue !== undefined) 
      {
        for (let index = 0; index < this.productos.length; index++) {
          const element = this.productos[index];
  
          if (element.id === this.prodSelectedValue) 
          {
            let vDetalleVenta : DetalleVenta;   
            vDetalleVenta = { valor: element.valor, 
              cantidad:this.prodCantidad, 
              total: (element.valor * this.prodCantidad),
              productId:element.id , 
              nombre: element.nombre, 
              id:this.idRow.toString(), 
              ventaId : this.idRow.toString() };
            this.detalleVenta.push(vDetalleVenta);
          }
        }

        for (let index = 0; index < this.detalleVenta.length; index++) {
          const element = this.detalleVenta[index];
          oTotal += ( element.cantidad * element.valor );   
        }

        this.ventaTotal = oTotal;
      }
    }



    /* Submit */
    onSubmit() 
    {
      this.loaded = false;

      let oVenta : Venta;
      oVenta = new Venta();

      oVenta.clienteId = this.clientId;
      oVenta.totalFactura = this.ventaTotal;
      oVenta.detalle = this.detalleVenta;
  
      /* Realizo el envio de parametros */
     this.ventaService.addVenta(oVenta).subscribe(result => 
         {
           this.loaded    = true;
           this.router.navigate(['/'])
       });
    }  




}
