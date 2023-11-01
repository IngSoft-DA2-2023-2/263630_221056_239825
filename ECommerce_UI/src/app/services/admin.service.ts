import { Injectable } from '@angular/core';
import { Producto } from '../dominio/producto.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from '../dominio/usuario.model';
import { Compra } from '../dominio/compra.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private urlGeneral: string =
    'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private urlProductos: string = this.urlGeneral + '/productos';

  constructor(private http : HttpClient) { }

  createProduct(producto: Producto): Producto {
    throw new ErrorEvent('Not implemented');
  }

  updateProduct(producto: Producto): Producto {
    throw new ErrorEvent('Not implemented');
  }

  deleteProduct(id: number): void {
    const token = sessionStorage.getItem('token')!;
    const headers: HttpHeaders = new HttpHeaders().set('Authorization', token);
    this.http.delete(this.urlProductos+'/'+id, {headers})
  }

  getUsuarios() : Usuario{
    throw new ErrorEvent("Not Implemented")
  }

  getUsuario(id : Number) : Usuario{
    throw new ErrorEvent("Not Implemented")
  }

  getCompras() : Compra[]{
    throw new ErrorEvent("Not Implemented")
  }
}
