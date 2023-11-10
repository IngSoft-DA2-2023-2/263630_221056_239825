// import { Component, OnInit, ViewChild } from '@angular/core';
// import { Producto } from 'src/app/dominio/producto.model';
// import { NgFor, NgIf } from '@angular/common';
// import { FormBuilder, Validators } from '@angular/forms';
// // import {MatRadioModule} from '@angular/material/radio';
// import { MatStepper } from '@angular/material/stepper';

// @Component({
//   // standalone: true,
//   selector: 'app-carrito',
//   templateUrl: './carrito.component.html',
//   styleUrls: ['./carrito.component.css'],
//   // import: [ngFor]
//   // imports: [MatRadioModule]
// })
// export class CarritoComponent implements OnInit {
//   hayProductosEnCarrito: boolean = false;
//   mostrarOpcionesPago: boolean = false;
//   mostrarProductos: boolean = true;
//   firstFormGroup = this._formBuilder.group({
//     firstCtrl: ['', Validators.required],
//   });
//   secondFormGroup = this._formBuilder.group({
//     secondCtrl: '',
//   });

//   @ViewChild(MatStepper)
//   stepper!: MatStepper;

//   constructor(private _formBuilder: FormBuilder) { }


//   productosEnCarrito: Producto[] = [];

//   ngOnInit() {
//     const carrito = localStorage.getItem('carrito');
//     this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
//     this.hayProductosEnCarrito = this.productosEnCarrito.length > 0;
//   }

//   borrarCarrito() {
//     this.productosEnCarrito = [];
//     localStorage.removeItem('carrito');
//   }

//   pagar() {
//     if (this.productosEnCarrito.length === 0) {
//       alert("El carrito está vacío. Agregue productos para continuar con el pago.");
//     } else if (this.firstFormGroup.valid) {
//       this.mostrarOpcionesPago = true;
//       this.mostrarProductos = false;
//       this.stepper.next();
//     }
//   }
// }
import { Component, OnInit, ViewChild } from '@angular/core';
import { Producto } from 'src/app/dominio/producto.model';
import { FormBuilder } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css'],
})
export class CarritoComponent implements OnInit {
  hayProductosEnCarrito: boolean = false;
  mostrarOpcionesPago: boolean = false;
  mostrarProductos: boolean = true;

  productos = this._formBuilder.group({
    firstCtrl: [],
  });

  @ViewChild('stepper') stepper!: MatStepper;

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

  pagar() {
    if (this.hayProductosEnCarrito) {
      this.mostrarOpcionesPago = true;
      this.mostrarProductos = false;
      this.stepper.next();
    } else {
      alert("El carrito está vacío. Agregue productos para continuar con el pago.");
      return;
    }
    
  }
}


