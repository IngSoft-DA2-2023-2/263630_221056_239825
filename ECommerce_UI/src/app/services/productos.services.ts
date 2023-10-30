import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Producto } from '../dominio/producto.model';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ProductoModelo } from '../dominio/productoModelo.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private urlGeneral: string = 'https://localhost:7061/api/v1';
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
    this.http.get<Producto[]>(this.url).subscribe((response: any) => {
      response.forEach((element: ProductoModelo) => {
        productos.push(this.createSingleProduct(element));
      });
      productos = response;
      console.log(response);
    });

    return productos;
  }

  createSingleProduct(element : ProductoModelo) : Producto {
    let producto : Producto = {
        id: element.id,
        descripcion: element.descripcion,
        precio: element.precio,
        stock: element.stock,
        nombre: element.nombre,
        marca: element.marca.nombre,
        categoria: element.categoria.nombre,
        colores: element.color.nombre
    }
    return producto;
  }

  getProduct(id: number): Producto {
    throw new ErrorEvent('Not implemented');
  }

  createProduct(producto: Producto): Producto {
    throw new ErrorEvent('Not implemented');
  }

  updateProduct(producto: Producto): Producto {
    throw new ErrorEvent('Not implemented');
  }

  deleteProduct(id: number): void {
    throw new ErrorEvent('Not implemented');
  }
}
