import { Component } from '@angular/core';
import { ProductoComponent } from '../producto/producto.component';
import { NgForOf, NgFor } from '@angular/common';
import { Producto } from 'src/app/dominio/producto.model';

@Component({
  standalone: true,
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
  imports: [NgForOf, ProductoComponent, NgFor]
})
export class ProductosComponent {
  private producto1: Producto = {
    id: 1,
    nombre: 'Cafe',
    descripcion: 'Molido',
    precio: 500,
    stock: 20,
    categoria: 'Bebida'
  };

  private producto2: Producto = {
    id: 3,
    nombre: 'Sushi',
    descripcion: 'Delicioso',
    precio: 1000,
    stock: 10,
    categoria: 'Comida'
  };

  protected ArrayProductos: Producto[] = [this.producto1, this.producto2];
  
}
