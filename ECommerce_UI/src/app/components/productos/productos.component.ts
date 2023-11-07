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
  
  private producto1: Producto = {
     id: 1,
     nombre: 'Cafe',
     descripcion: 'Molido',
     precio: 500,
     stock: 20,
     marca: "Nescafe",
     categoria: 'Bebida',
     colores: "Negro"
  };

  // private producto2: Producto = {
  //   id: 3,
  //   nombre: 'Sushi',
  //   descripcion: 'Delicioso',
  //   precio: 1000,
  //   stock: 10,
  //   categoria: ['Comida'],
  //   marca: ["Fabric"],
  //   colores: ["Azul"]
  // };

  // private producto3: Producto = {
  //   id: 2,
  //   nombre: 'Pan',
  //   descripcion: 'Gluten free',
  //   precio: 700,
  //   stock: 5,
  //   categoria: ['Sin gluten'],
  //   marca: ['Bimbo'],
  //   colores: ['Azul']
  // };

  // private producto4: Producto = {
  //   id: 7,
  //   nombre: 'Coca cola',
  //   descripcion: 'Sin azucar',
  //   precio: 700,
  //   stock: 5,
  //   categoria: ['Bebida'],
  //   marca: ['Coca'],
  //   colores: ['Roja']
  // };

  ngOnInit(){
    // this.ArrayProductos = this.productsServices.getProducts();
    this.ArrayProductos.push(this.producto1);
  }
}

// addToCart(producto: Producto): void {
//   if (producto.stock > 0) {
//     console.log(`Agregado al carrito: ${producto.nombre}`);
//     producto.stock--;
//   } else {
//     console.log('No hay suficiente stock disponible para ' + producto.nombre);
//   }
// }
