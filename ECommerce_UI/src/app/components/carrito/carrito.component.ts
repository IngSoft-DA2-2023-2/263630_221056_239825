import { Component, OnInit, ViewChild } from '@angular/core';
import { Producto } from 'src/app/dominio/producto.model';
import { FormBuilder } from '@angular/forms';
import { MatStepperModule, MatStepper } from '@angular/material/stepper';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ProductoComponent } from '../producto/producto.component';
import { OpcionesPagoComponent } from '../opciones-pago/opciones-pago.component';
import { NgIf, NgFor } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css'],
  imports: [MatButtonModule, MatStepperModule, MatCardModule, ProductoComponent, OpcionesPagoComponent, NgIf, NgFor],
})
export class CarritoComponent implements OnInit {
  hayProductosEnCarrito: boolean = false;

  productos = this._formBuilder.group({
    firstCtrl: [''],
  });

  haPagado = this._formBuilder.group({
    firstCtrl: [''],
  });

  @ViewChild('stepper') stepper!: MatStepperModule;

  constructor(private _formBuilder: FormBuilder) {}

  productosEnCarrito: Producto[] = [];

  ngOnInit() {
    this.cargarCarrito();
  }

  cargarCarrito() {
    const carrito = localStorage.getItem('carrito');
    this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
    this.hayProductosEnCarrito = this.productosEnCarrito.length > 0;
  }

  borrarCarrito() {
    this.productosEnCarrito = [];
    localStorage.removeItem('carrito');
    this.cargarCarrito();
  }

  eliminarProductoDelCarrito(index: number) {
    // Elimina el producto del carrito al recibir el evento del componente hijo
    this.productosEnCarrito.splice(index, 1);
    localStorage.setItem('carrito', JSON.stringify(this.productosEnCarrito));
  }
}


