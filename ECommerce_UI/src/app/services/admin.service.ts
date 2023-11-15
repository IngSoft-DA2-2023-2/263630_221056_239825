import { Injectable } from '@angular/core';
import { Producto } from '../dominio/producto.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from '../dominio/usuario.model';
import { Compra } from '../dominio/compra.model';
import { Observable, catchError } from 'rxjs';
import { ProductoDTO } from '../dominio/producto-dto.model';

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

  createProduct(producto: ProductoDTO): Observable<ProductoDTO> {
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    return this.http.post<ProductoDTO>(this.urlProductos, producto, { headers });
  }

  updateProduct(producto: Producto): Producto {
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    this.http.put(this.urlProductos + '/' + producto.id, producto, { headers });
    return producto;
  }

  deleteProduct(id: number): Observable<any> {
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    return this.http.delete(this.urlProductos + '/' + id, { headers });
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
      id: response.id,
      compras: response.compras,
      correoElectronico: response.correoElectronico,
      direccionEntrega: response.direccionEntrega,
      rol: response.rol,
    };
    return usuario;
  }

  getUsuario(id: Number): Observable<Usuario> {
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    return this.http.get<Usuario>(this.urlUsuarios + '/' + id, { headers });
  }

  getCompras(): Observable<Compra[]> {
    const token: string = sessionStorage.getItem('token')!;
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'placeHolderValue',
      'Content-Type': 'application/json',
      Authorization: token,
    });
    return this.http.get<Compra[]>(this.urlCompras, { headers })
  }
}
