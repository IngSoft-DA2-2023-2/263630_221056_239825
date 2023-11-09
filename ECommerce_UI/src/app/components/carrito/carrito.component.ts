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
  tieneProductosEnCarrito: boolean = false;

  productosEnCarrito: Producto[] = [];

  ngOnInit() {
    const carrito = localStorage.getItem('carrito');
    this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
    this.tieneProductosEnCarrito = this.productosEnCarrito.length > 0;
  }

  borrarCarrito() {
    this.productosEnCarrito = [];
    
    localStorage.removeItem('carrito');
  }
}
