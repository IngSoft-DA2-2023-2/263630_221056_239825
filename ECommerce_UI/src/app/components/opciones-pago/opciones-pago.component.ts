import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { TarjetaComponent } from "../tarjeta/tarjeta.component";
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-opciones-pago',
    templateUrl: './opciones-pago.component.html',
    styleUrls: ['./opciones-pago.component.css'],
    standalone: true,
    imports: [MatButtonModule, TarjetaComponent, NgIf]
})
export class OpcionesPagoComponent {
  mostrarTarjeta: boolean = false;

  mostrarComponenteTarjeta() {
    this.mostrarTarjeta = true;
  }
}
