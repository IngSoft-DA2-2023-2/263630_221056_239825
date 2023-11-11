import { Component } from '@angular/core';
import { ProductoComponent } from '../producto/producto.component';
import { NgForOf, NgFor} from '@angular/common';
import { Producto } from 'src/app/dominio/producto.model';
import { ProductsService } from 'src/app/services/productos.services';
import { catchError, of, take } from 'rxjs';
// import {MatGridListModule} from '@angular/material/grid-list';

@Component({
  standalone: true,
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
  imports: [NgForOf, ProductoComponent, NgFor, /*MatGridListModule*/]
})
export class ProductosComponent {
  constructor(private productsServices : ProductsService){ }
  protected ArrayProductos: Producto[] = [];
  
  ngOnInit(){
    this.ArrayProductos = this.productsServices.getProducts();
    // this.ArrayProductos.push(this.producto1);
  }

  // private producto1: Producto = {
  //   id: 1,
  //   nombre: 'Cafe',
  //   descripcion: 'Molido',
  //   precio: 500,
  //   stock: 20,
  //   categoria: 'Bebida',
  //   marca: "Nescafe",
  //   colores: "Negro"
  // };
}


