import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductoService } from 'src/app/services/ProductoService';
import { Producto } from 'src/app/Types/Producto';

@Component({
  selector: 'app-add-producto',
  templateUrl: './add-producto.component.html',
  styleUrls: ['./add-producto.component.css']
})
export class AddProductoComponent implements OnInit {

  public producto : Producto;
  public loaded  = false;
  public end  = false;

  constructor(private productoService: ProductoService,
              private router: Router) { }

  ngOnInit() {
    this.producto = {nombre:"", valor:0, id:"" };
    this.loaded = true;
  }


   /* Submit */
   onSubmit() 
   {
     this.loaded = false;
 
     /* Realizo el envio de parametros */
    this.productoService.addProducto(this.producto).subscribe(result => 
        {
          this.loaded    = true;
          this.router.navigate(['/producto-home'])
      });
   }  

}
