import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Compra } from '../dominio/compra.model';
import { compraCreateModelo } from '../dominio/compraCreateModelo.model';

@Injectable({
  providedIn: 'root'
})
export class TokenUserService {
  private urlGeneral: string =
    'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private urlUsuarios: string = this.urlGeneral + '/usuarios';

  constructor(private http : HttpClient) { }
  getCompraDelUsuario() : Compra[] {
    const id: string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const newUrl: string = this.urlUsuarios + '/' + id + '/compras';
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      Authorization: token,
      'Content-Type': 'application/json',
    });
    let compras : Compra[] = []
    this.http.get(newUrl, {headers}).subscribe((response : any)=> {
      compras = response
    })
    return compras;
  }

  postCompraDelUsuario(compraPorHacer : compraCreateModelo) : void {
    const id: string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const newUrl: string = this.urlUsuarios + '/' + id + '/compras';
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      'Authorization': token,
    });
    this.http.post(newUrl, compraPorHacer, {headers}).subscribe((response: any) => {
      console.log(response);
    })
  }

}