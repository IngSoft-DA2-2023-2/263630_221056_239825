import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Compra } from '../dominio/compra.model';
import { compraCreateModelo } from '../dominio/compraCreateModelo.model';
import { Usuario } from '../dominio/usuario.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TokenUserService {
  private urlGeneral: string =
  'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private urlUsuarios: string = this.urlGeneral + '/usuarios';

  constructor(private http : HttpClient) { }
  getCompraDelUsuario() : Observable<Compra[]> {
    const id: string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const newUrl: string = this.urlUsuarios + '/' + id + '/compras';
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      Authorization: token,
      'Content-Type': 'application/json',
    });
    return this.http.get<Compra[]>(newUrl, {headers});
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

  putUsuario(usuario : Usuario){
    const id : string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    this.http.put(this.urlUsuarios + '/' + id, usuario, { headers }).subscribe((response: any) => {
      const usuarioCambiado: Usuario = response;
      sessionStorage.setItem('usuario', JSON.stringify(usuarioCambiado));
    });
  }

  deleteUsuario(id : number) : Observable<any>{
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    return this.http.delete(this.urlUsuarios + '/' + id, { headers });
  }
}