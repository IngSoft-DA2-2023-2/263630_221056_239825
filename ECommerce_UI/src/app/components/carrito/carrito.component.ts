import { Component } from '@angular/core';
import { Producto } from 'src/app/dominio/producto.model';
import { NgFor } from '@angular/common';

@Component({
  // standalone: true,
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css']
  // import: [ngFor]
})
export class CarritoComponent {
  productosEnCarrito: Producto[] = [];

  constructor() {
    const carrito = localStorage.getItem('carrito');
    this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
  }

  ngOnInit() {

  }
}
