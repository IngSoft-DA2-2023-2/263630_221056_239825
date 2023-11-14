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
import { NotificationComponent } from '../notification/notification.component';
import { MatDialog } from '@angular/material/dialog';

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
    private opcionesPago: OpcionesPagoComponent,
    public dialog: MatDialog
  ) { }

  productosEnCarrito: Producto[] = [];

  ngOnInit() {
    this.cargarCarrito();
    this.sumaPrecios();
    sessionStorage.removeItem('metodoDePago');
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
    const metodoDePago = sessionStorage.getItem('metodoDePago') || '';
    if (this.productosEnCarrito.length === 0) {
      this.openNotification("No hay productos en el carrito, por favor, agregue productos");
      return;
    } else if (metodoDePago === '') {
      this.openNotification("No se ha seleccionado un método de pago");
      return;
    } else {
      const idDeProductosDelCarrito: number[] = this.productosEnCarrito.map((producto) => producto.id);
      const compraModel: compraCreateModelo = {
        idProductos: idDeProductosDelCarrito,
        metodoDePago: metodoDePago,
      };

      this.tokenService
        .postCompraDelUsuario(compraModel)
        .pipe(
          catchError((err) => {
            // Error
            this.openNotification('Error al procesar la compra. Por favor, inténtalo nuevamente.');
            return [];
          })
        )
        .subscribe((data) => {
          // Compra exitosa
          this.openNotification(`¡Compra exitosa!`);
          this.borrarCarrito();
          sessionStorage.removeItem('metodoDePago');
        });
    }


  }

  openNotification(mensaje: string): void {
    const dialogRef = this.dialog.open(NotificationComponent, {
      data: { mensaje: mensaje },
    });
    dialogRef.componentInstance.showExitoso(mensaje);
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
