import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { TarjetaComponent } from "../tarjeta/tarjeta.component";
import { NgIf, NgFor } from '@angular/common';

@Component({
    selector: 'app-opciones-pago',
    templateUrl: './opciones-pago.component.html',
    styleUrls: ['./opciones-pago.component.css'],
    standalone: true,
    imports: [MatButtonModule, TarjetaComponent, NgIf, NgFor]
})
export class OpcionesPagoComponent {
  mostrarTarjeta: boolean = false;
  mostrarCuentaBancaria: boolean = false;
  mostrarPaganza: boolean = false;
  qrImageUrl: string = '/assets/img/QR.png';

  mostrarComponenteTarjeta() {
    this.mostrarTarjeta = true;
    this.mostrarCuentaBancaria = false;
    this.mostrarPaganza = false;
  }

  cuentasBancarias: { banco: string; numero: string }[] = [
    { banco: 'Santander', numero: '1234-5678-9012-3456' },
    { banco: 'ITAU', numero: '9876-5432-1098-7654' },
    { banco: 'Banco BBVA', numero: '1111-2222-3333-4444' },
  ];

  mostrarInfoCuentaBancaria() {
    this.mostrarCuentaBancaria = true;
    this.mostrarTarjeta = false;
    this.mostrarPaganza = false;
  }

  mostrarInfoPaganza() {
    this.mostrarPaganza = true;
    this.mostrarTarjeta = false;
    this.mostrarCuentaBancaria = false;
  }

}
