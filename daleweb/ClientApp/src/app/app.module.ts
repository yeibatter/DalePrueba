import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ClienteService } from './services/ClienteService';
import { AddClienteComponent } from './cliente/add-cliente/add-cliente.component';
import { EditClienteComponent } from './cliente/edit-cliente/edit-cliente.component';
import { ListClienteComponent } from './cliente/list-cliente/list-cliente.component';
import { AddProductoComponent } from './producto/add-producto/add-producto.component';
import { ListProductoComponent } from './producto/list-producto/list-producto.component';
import { ProductoService } from './services/ProductoService';
import { AddVentaComponent } from './venta/add-venta/add-venta.component';
import { VentaService } from './services/VentaService';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,

    AddClienteComponent,
    EditClienteComponent,
    ListClienteComponent,
    AddProductoComponent,
    ListProductoComponent,
    AddVentaComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'cliente-home', component: ListClienteComponent },
      { path: 'cliente-add', component: AddClienteComponent },
      { path: 'producto-home', component: ListProductoComponent },
      { path: 'producto-add', component: AddProductoComponent },
      { path: 'venta-add', component: AddVentaComponent },
    ])
  ],
  providers: [
    ClienteService,
    ProductoService,
    VentaService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
