import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { ProductoComponent } from '../producto/producto.component';
import { NgFor } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css'],
  imports: [MatDividerModule, MatCardModule, ProductoComponent, NgFor],
})
export class CompraComponent {
  @Input() compra?: Compra;

  ngOnInit(): void {
    console.log(this.compra?.NombrePromo);
  }
}
