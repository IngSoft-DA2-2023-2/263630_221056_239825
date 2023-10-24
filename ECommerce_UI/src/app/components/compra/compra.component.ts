import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';

@Component({
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css'],
})
export class CompraComponent {
  @Input() compra?: Compra;
}