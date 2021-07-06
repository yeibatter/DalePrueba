import { Component, OnInit } from '@angular/core';
import { ClienteService } from 'src/app/services/ClienteService';
import { Cliente } from 'src/app/Types/Cliente';
import {Router} from "@angular/router"


@Component({
  selector: 'app-add-cliente',
  templateUrl: './add-cliente.component.html',
  styleUrls: ['./add-cliente.component.css']
})
export class AddClienteComponent implements OnInit 
{
  public cliente : Cliente;
  public loaded  = false;
  public end  = false;


  constructor(private clienteService: ClienteService,
             private router: Router) { }

  ngOnInit() 
  {
    this.cliente = {apellidos:"", documento:"", id:"", nombres:"", numeroTelefono:""};
    this.loaded = true;
  }

   /* Submit */
   onSubmit() 
   {
     this.loaded = false;
 
     /* Realizo el envio de parametros */
    this.clienteService.addCliente(this.cliente).subscribe(result => 
        {
          this.loaded    = true;
          this.router.navigate(['/cliente-home'])
      });
   }  

}
