import { Injectable } from '@angular/core';
import { Producto } from '../dominio/producto.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from '../dominio/usuario.model';
import { Compra } from '../dominio/compra.model';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private urlGeneral: string =
    'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private urlProductos: string = this.urlGeneral + '/productos';
  private urlUsuarios: string = this.urlGeneral + '/usuarios';
  private urlCompras: string = this.urlGeneral + '/compras';

  constructor(private http: HttpClient) {}

  createProduct(producto: Producto): Producto {
    throw new ErrorEvent('Not implemented');
  }

  updateProduct(producto: Producto): Producto {
    throw new ErrorEvent('Not implemented');
  }

  deleteProduct(id: number): void {
    const token: string = sessionStorage.getItem('token')!;
    const headers: HttpHeaders = new HttpHeaders().set('Authorization', token);
    this.http.delete(this.urlProductos + '/' + id, { headers });
  }

  getUsuarios(): Usuario[] {
    let listaUsuarios: Usuario[] = [];
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    this.http
      .get<Usuario[]>(this.urlUsuarios, { headers })
      .subscribe((response: any) => {
        response.forEach((element: any) => {
          listaUsuarios.push(this.crearUsuario(element));
        });
      });
    return listaUsuarios;
  }

  private crearUsuario(response: Usuario): Usuario {
    let usuario: Usuario = {
      compras: response.compras,
      correoElectronico: response.correoElectronico,
      direccionEntrega: response.direccionEntrega,
      rol: response.rol,
    };
    return usuario;
  }

  getUsuario(id: Number): Usuario {
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    this.http
      .get<Usuario>(this.urlUsuarios, { headers })
      .subscribe((response: any) => {
        const usuario: Usuario = this.crearUsuario(response);
        return usuario;
      });
    throw new Error('Usuario no encontrado');
  }

  getCompras(): Compra[] {
    let listaCompras: Compra[] = [];
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    this.http
      .get<Usuario[]>(this.urlCompras, { headers })
      .subscribe((response: any) => {
        response.forEach((element: Compra) => {
          listaCompras.push(response);
        });
      });
    return listaCompras;
  }
}
