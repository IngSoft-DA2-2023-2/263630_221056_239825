import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Producto } from '../dominio/producto.model';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ProductoModelo } from '../dominio/productoModelo.model';
import { ColorDTO } from '../dominio/color-dto.model';
import { MarcaDTO } from '../dominio/marca-dto.model';
import { CategoriaDTO } from '../dominio/categoria-dto.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private urlGeneral: string = 'https://merely-loved-gibbon.ngrok-free.app/api/v1';
  private url: string = this.urlGeneral + '/productos';
  private _productosBehavior: BehaviorSubject<Producto[] | undefined>;

  constructor(private http: HttpClient) {
    this._productosBehavior = new BehaviorSubject<Producto[] | undefined>(undefined);
  }

  public get characters$(): Observable<Producto[] | undefined> {
    return this._productosBehavior.asObservable();
  }

  getProducts(params: HttpParams): Producto[] {
    let productos: Producto[] = [];
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    this.http
      .get<Producto[]>(this.url, { headers, params })
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
      aplicaParaPromociones: element.aplicaParaPromociones,
    };
    return producto;
  }

  getProduct(id: number): Observable<ProductoModelo>  {
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    return this.http.get<ProductoModelo>(this.url + '/' + id, { headers });
  }

  getColores() : Observable<ColorDTO[]>{
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    return this.http.get<ColorDTO[]>(this.url + '/colores', { headers });
  }

  getMarcas() : Observable<MarcaDTO[]>{
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    return this.http.get<MarcaDTO[]>(this.url + '/marcas', { headers });
  }

  getCategorias() : Observable<CategoriaDTO[]> {
    const headers: HttpHeaders = new HttpHeaders().set(
      'ngrok-skip-browser-warning',
      'placeHolderValue'
    );
    return this.http.get<CategoriaDTO[]>(this.url + '/categorias', { headers });
  }
}
