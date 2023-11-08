import { Component, OnInit } from '@angular/core';
import { Producto } from 'src/app/dominio/producto.model';
import { NgFor, NgIf } from '@angular/common';

@Component({
  // standalone: true,
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css']
  // import: [ngFor]
})
export class CarritoComponent implements OnInit {
  productosEnCarrito: Producto[] = [];

  ngOnInit() {
    const carrito = localStorage.getItem('carrito');
    this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
  }
  borrarCarrito() {
    this.productosEnCarrito = [];

    localStorage.removeItem('carrito');
  }
}
