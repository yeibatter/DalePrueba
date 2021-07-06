import { Component, OnInit } from '@angular/core';
import { ClienteService } from 'src/app/services/ClienteService';
import { Cliente } from 'src/app/Types/Cliente';

@Component({
  selector: 'app-list-cliente',
  templateUrl: './list-cliente.component.html',
  styleUrls: ['./list-cliente.component.css']
})
export class ListClienteComponent implements OnInit 
{

  public clientes : Cliente[];

  /* Constructor */
  constructor(private clienteService: ClienteService) { }

  //Init
  ngOnInit() : void 
  {
    this.clienteService.getAll().subscribe({
      next: (result: Cliente[]) => {
        if (result) {
          this.clientes = result;
        }
      },
      error: (error) => console.log(error)
    });
  }

}
