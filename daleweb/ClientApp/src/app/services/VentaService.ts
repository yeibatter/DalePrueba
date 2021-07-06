import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Venta } from '../Types/Venta';
 
const VentaAdd                   = 'Venta/AddVenta';


@Injectable()
export class VentaService 
{
    urlBase: string;

    /* Constructor */
    constructor(@Inject('BASE_URL') baseUrl: string,
                private http: HttpClient) {
        this.urlBase =  baseUrl;
    }


    /* add */
    addVenta(itemp: Venta): Observable<Response> {
        const sUrl = this.urlBase + VentaAdd;
        console.log(sUrl);
        return this.http.post<Response>(sUrl, itemp);
    }

    
}
