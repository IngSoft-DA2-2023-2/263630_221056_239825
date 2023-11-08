import { Component, Input } from '@angular/core';
import { Producto } from '../../dominio/producto.model';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { ProductsService } from 'src/app/services/productos.services';

@Component({
  standalone: true,
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css'],
  imports: [MatButtonModule, MatCardModule, MatDividerModule],
})
export class ProductoComponent {
  constructor(private productsServices : ProductsService){ }
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

  agregarAlCarrito() {
    const productoNuevo: Producto = this.productsServices.getProduct(this.producto.id)!;
    if(productoNuevo.stock > 0){
      if (localStorage.getItem('productosEnCarrito') == null) {
        let carrito: Producto[] = [];
        carrito.push(productoNuevo);
        localStorage.setItem('productosEnCarrito', JSON.stringify(carrito));
      } else {
        let carrito: Producto[] = JSON.parse(localStorage.getItem('productosEnCarrito')!);
        carrito.push(productoNuevo);
        localStorage.setItem('productosEnCarrito', JSON.stringify(carrito));
      }
    }
    else{
      alert("no hay stock")
    }
  }
}

