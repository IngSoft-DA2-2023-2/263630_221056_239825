import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { compraCreateModelo } from '../dominio/compraCreateModelo.model';

@Injectable({
  providedIn: 'root',
})
export class CompraService {
  private urlGeneral: string =
    'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private url: string = '/usuarios';
  constructor(private http: HttpClient) {}

  getCompraDelUsuario() : void {
    const id: string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const newUrl: string = this.urlGeneral + this.url + '/' + id + '/compras';
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': token,
    });
    this.http.get(newUrl, {headers}).subscribe((response : any)=> {
      console.log(response);
    })
  }

  postCompraDelUsuario(compraPorHacer : compraCreateModelo) : void {
    const id: string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const newUrl: string = this.urlGeneral + this.url + '/' + id + '/compras';
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': token,
    });
    this.http.post(newUrl, compraPorHacer, {headers}).subscribe((response: any) => {
      console.log(response);
    })
  }
}
