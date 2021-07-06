import { Component, OnInit } from '@angular/core';
import { ProductoService } from 'src/app/services/ProductoService';
import { Producto } from 'src/app/Types/Producto';

@Component({
  selector: 'app-list-producto',
  templateUrl: './list-producto.component.html',
  styleUrls: ['./list-producto.component.css']
})
export class ListProductoComponent implements OnInit {

  public productos : Producto[];

  /* Constructor */
  constructor(private productoService: ProductoService) { }

  //Init
  ngOnInit() : void 
  {
    this.productoService.getAll().subscribe({
      next: (result: Producto[]) => {
        if (result) {
          this.productos = result;
        }
      },
      error: (error) => console.log(error)
    });
  }


}
