import { Component, Input } from '@angular/core';
import { Producto } from '../../dominio/producto.model';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  standalone: true,
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css'],
  imports: [MatButtonModule, MatCardModule, MatDividerModule],
})
export class ProductoComponent {
  @Input() producto: Producto = {
    id: 0,
    nombre: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    categoria: '',
    colores: '',
    marca: '',
  };
  @Input() seMuestraBoton: boolean = true;
}
