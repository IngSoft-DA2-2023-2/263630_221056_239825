import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Producto } from '../dominio/producto.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
    url:string = 'https://localhost:7061/api/v1/productos';

    constructor(private http: HttpClient) {}

    getProducts() : Producto[] {
        throw new ErrorEvent("Not implemented");
    }

    getProduct(id: number) : Producto {
        throw new ErrorEvent("Not implemented");
    }

    createProduct(producto: Producto) : Producto {
        throw new ErrorEvent("Not implemented");
    }

    updateProduct(producto: Producto) : Producto {
        throw new ErrorEvent("Not implemented");
    }

    deleteProduct(id: number) : void {
        throw new ErrorEvent("Not implemented");
    }
}
