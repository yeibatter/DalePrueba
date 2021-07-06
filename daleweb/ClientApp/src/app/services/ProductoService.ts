import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto } from '../Types/Producto';


const ProductoGetAll                = 'Producto/GetAll';
const ProductoAdd                   = 'Producto/AddProducto';


@Injectable()
export class ProductoService 
{
    urlBase: string;

    /* Constructor */
    constructor(@Inject('BASE_URL') baseUrl: string,
                private http: HttpClient) {
        this.urlBase =  baseUrl;
    }

    /* GetAll */
    getAll(): Observable<Producto[]> {
        const sUrl = this.urlBase + ProductoGetAll;
        console.log(sUrl);
        return this.http.get<Producto[]>(sUrl);
    }


    /* add */
    addProducto(itemp: Producto): Observable<Response> {
        const sUrl = this.urlBase + ProductoAdd;
        console.log(sUrl);
        return this.http.post<Response>(sUrl, itemp);
    }

    
}
