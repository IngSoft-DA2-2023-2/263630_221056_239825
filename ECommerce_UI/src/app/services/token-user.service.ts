import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Compra } from '../dominio/compra.model';
import { compraCreateModelo } from '../dominio/compraCreateModelo.model';
import { Usuario } from '../dominio/usuario.model';
import { Observable } from 'rxjs';
import { UsuarioDTO } from '../dominio/usuario-dto.model';

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

  postCompraDelUsuario(compraPorHacer : compraCreateModelo) : Observable<compraCreateModelo> {
    const id: string = sessionStorage.getItem('idUsuario')!;
    const token: string = sessionStorage.getItem('token')!;
    const newUrl: string = this.urlUsuarios + '/' + id + '/compras';
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      'Authorization': token,
    });
    return this.http.post<compraCreateModelo>(newUrl, compraPorHacer, {headers});
  }

  putUsuario(usuario : UsuarioDTO) : Observable<Usuario> {
    const id : string = usuario.id.toString();
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    return this.http.put<Usuario>(this.urlUsuarios + '/' + id, usuario, { headers });
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