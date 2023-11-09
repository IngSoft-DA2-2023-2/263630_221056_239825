import { Component, OnInit } from '@angular/core';
import { Producto } from 'src/app/dominio/producto.model';
import { NgFor, NgIf } from '@angular/common';
// import {MatRadioModule} from '@angular/material/radio';

@Component({
  // standalone: true,
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css'],
  // import: [ngFor]
  // imports: [MatRadioModule]
})
export class CarritoComponent implements OnInit {
  hayProductosEnCarrito: boolean = false;
  mostrarOpcionesPago: boolean = false;
  mostrarProductos: boolean = true;

  productosEnCarrito: Producto[] = [];

  ngOnInit() {
    const carrito = localStorage.getItem('carrito');
    this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
    this.hayProductosEnCarrito = this.productosEnCarrito.length > 0;
  }

  borrarCarrito() {
    this.productosEnCarrito = [];

    localStorage.removeItem('carrito');
  }

  pagar() {
    this.mostrarOpcionesPago = true;
    this.mostrarProductos = false;
  }
}
