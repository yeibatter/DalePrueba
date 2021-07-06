import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../Types/Cliente';


const ClienteGetAll                 = 'Cliente/GetAll';
const ClienteGetByDocument          = 'Cliente/GetByDocument';
const ClienteUpdate                 = 'Cliente/UpdateCliente';
const ClienteAdd                    = 'Cliente/AddCliente';
const ClienteDelete                 = 'Cliente/DeleteClient';

@Injectable()
export class ClienteService 
{
    urlBase: string;

    /* Constructor */
    constructor(@Inject('BASE_URL') baseUrl: string,
                private http: HttpClient) {
        this.urlBase =  baseUrl;
    }

    /* GetAll */
    getAll(): Observable<Cliente[]> {
        const sUrl = this.urlBase + ClienteGetAll;
        console.log(sUrl);
        return this.http.get<Cliente[]>(sUrl);
    }

    /* getByDocument */
    getByDocument(document : string): Observable<Cliente[]> {
        const sUrl = this.urlBase + ClienteGetByDocument + "/" + document;
        console.log(sUrl);
        return this.http.get<Cliente[]>(sUrl);
    }

    /* addCliente */
    addCliente(itemc: Cliente): Observable<Response> {
        const sUrl = this.urlBase + ClienteAdd;
        console.log(sUrl);
        return this.http.post<Response>(sUrl, itemc);
    }

    /* updateCliente */
    updateCliente(itemc: Cliente): Observable<Response> {
        const sUrl = this.urlBase + ClienteUpdate;
        console.log(sUrl);
        return this.http.post<Response>(sUrl, itemc);
    }

    /* deleteCliente */
    deleteCliente(obParametros: Cliente): Observable<Response> {
        const sUrl = this.urlBase + ClienteDelete;
        console.log(sUrl);
        return this.http.post<Response>(sUrl, obParametros);
    }
}
