import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Producto } from '../dominio/producto.model';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ProductoModelo } from '../dominio/productoModelo.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private urlGeneral: string =
    'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private url: string = this.urlGeneral + '/productos';
  private _productosBehavior: BehaviorSubject<Producto[] | undefined>;

  constructor(private http: HttpClient) {
    this._productosBehavior = new BehaviorSubject<Producto[] | undefined>(
      undefined
    );
  }

  public get characters$(): Observable<Producto[] | undefined> {
    return this._productosBehavior.asObservable();
  }

  getProducts(): Producto[] {
    let productos: Producto[] = [];
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    this.http
      .get<Producto[]>(this.url, { headers })
      .subscribe((response: any) => {
        response.forEach((element: ProductoModelo) => {
          productos.push(this.createSingleProduct(element));
        });
        productos = response;
      });
    return productos;
  }

  private createSingleProduct(element: ProductoModelo): Producto {
    let producto: Producto = {
      id: element.id,
      descripcion: element.descripcion,
      precio: element.precio,
      stock: element.stock,
      nombre: element.nombre,
      marca: element.marca.nombre,
      categoria: element.categoria.nombre,
      colores: element.color.nombre,
    };
    return producto;
  }

  getProduct(id: number): Producto | null {
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    this.http
      .get<Producto[]>(this.url + '/' + id, { headers })
      .subscribe((response: any) => {
        return this.createSingleProduct(response);
      });
    return null;
  }
}
