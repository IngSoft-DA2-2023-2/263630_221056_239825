import { Component, OnInit, ViewChild } from '@angular/core';
import { Producto } from 'src/app/dominio/producto.model';
import { FormBuilder } from '@angular/forms';
import { MatStepperModule, MatStepper } from '@angular/material/stepper';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ProductoComponent } from '../producto/producto.component';
import { OpcionesPagoComponent } from '../opciones-pago/opciones-pago.component';
import { NgIf, NgFor } from '@angular/common';
import { CommonModule } from '@angular/common';
import { ProductosComponent } from '../productos/productos.component';
import { TokenUserService } from 'src/app/services/token-user.service';
import { compraCreateModelo } from 'src/app/dominio/compraCreateModelo.model';
import { catchError, throwError } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css'],
  imports: [
    MatButtonModule,
    MatStepperModule,
    MatCardModule,
    ProductoComponent,
    OpcionesPagoComponent,
    NgIf,
    NgFor,
    CommonModule,
    ProductosComponent,
  ],
  providers: [OpcionesPagoComponent],
})
export class CarritoComponent implements OnInit {
  hayProductosEnCarrito: boolean = false;
  costoTotal: number = 0;

  productos = this._formBuilder.group({
    firstCtrl: [''],
  });

  haPagado = this._formBuilder.group({
    firstCtrl: [''],
  });

  @ViewChild('stepper') stepper!: MatStepperModule;

  constructor(
    private _formBuilder: FormBuilder,
    private tokenService: TokenUserService,
    private opcionesPago: OpcionesPagoComponent
  ) {}

  productosEnCarrito: Producto[] = [];

  ngOnInit() {
    this.cargarCarrito();
    this.sumaPrecios();
  }

  cargarCarrito() {
    const carrito = localStorage.getItem('carrito');
    this.productosEnCarrito = carrito ? JSON.parse(carrito) : [];
    this.hayProductosEnCarrito = this.productosEnCarrito.length > 0;
  }

  sumaPrecios() {
    this.costoTotal = this.productosEnCarrito.reduce(
      (total, producto) => total + producto.precio,
      0
    );
  }

  borrarCarrito() {
    this.productosEnCarrito = [];
    localStorage.removeItem('carrito');
    this.cargarCarrito();
  }

  pagar() {
    if (this.productosEnCarrito.length > 0) {
      const idDeProductosDelCarrito: number[] = this.productosEnCarrito.map(
        (producto) => producto.id
      );
      const metodoDePago = sessionStorage.getItem('metodoDePago') || '';

      const compraModel: compraCreateModelo = {
        idProductos: idDeProductosDelCarrito,
        metodoDePago: metodoDePago,
      };

      console.log(compraModel);
      this.tokenService
        .postCompraDelUsuario(compraModel)
        .pipe(
          catchError((err) => {
            alert(err.error.message);
            return throwError(err);
          })
        )
        .subscribe((data) => {
          console.log(data);
          this.borrarCarrito();
          sessionStorage.removeItem('metodoDePago');
        });
    }
  }

  eliminarProductoDelCarrito(index: number) {
    this.productosEnCarrito.splice(index, 1);
    localStorage.setItem('carrito', JSON.stringify(this.productosEnCarrito));
  }

  calcularTotalCarrito(): number {
    return this.productosEnCarrito.reduce(
      (total, producto) => total + producto.precio,
      0
    );
  }
}
