import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { ProductoComponent } from '../producto/producto.component';
import { NgFor } from '@angular/common';
import { CommonModule } from '@angular/common';
import { ProductsService } from 'src/app/services/productos.services';
import { Producto } from 'src/app/dominio/producto.model';
import { ProductoModelo } from 'src/app/dominio/productoModelo.model';

@Component({
  standalone: true,
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css'],
  imports: [MatDividerModule, MatCardModule, ProductoComponent, NgFor, CommonModule],
})
export class CompraComponent {
  @Input() compra?: Compra;
  protected productosDeLaCompra : ProductoModelo[] = [];
  constructor(private productosService : ProductsService) {}

  ngOnInit(): void {
    this.compra?.productos.forEach(id => {
      this.productosService.getProduct(id).subscribe((producto) => {
        this.productosDeLaCompra.push(producto);
      });
    });
  }
}
