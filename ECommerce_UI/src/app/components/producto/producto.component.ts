import { Component, Input } from '@angular/core';
import { Producto } from '../../dominio/producto.model';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { ProductsService } from 'src/app/services/productos.services';
import { ActivatedRoute } from '@angular/router';
import { NgFor } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css'],
  imports: [MatButtonModule, MatCardModule, MatDividerModule, NgFor],
})

export class ProductoComponent {
  constructor(private activatedRoute: ActivatedRoute, private productsServices : ProductsService){ }
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

  getBotones(): { texto: string; accion: string }[] {
    const url = this.activatedRoute.snapshot.url.join('/');
    if (url === 'admin') {
      return [
        { texto: 'Modificar Producto', accion: 'Modificar' },
        { texto: 'Eliminar Producto', accion: 'Eliminar' }
      ]; 
    } else if (url === 'carrito') {
      return [{ texto: 'Eliminar del Carrito', accion: 'Eliminar' }]; 
    }else{
      return [{ texto: 'Agregar al Carrito', accion: 'Agregar' }];
    }
  }

  realizarAccion(accion: string) {
    if (accion === 'Modificar') {
      alert('Modificar');
    } else if (accion === 'Eliminar') {
      alert('Eliminar');
    } else if (accion === 'Agregar') {
      this.agregarAlCarrito();
    }
  }

  agregarAlCarrito() {
    // const productoNuevo: Producto = this.productsServices.getProduct(this.producto.id)!;
    // if(productoNuevo.stock > 0){
    //   if (localStorage.getItem('productosEnCarrito') == null) {
    //     let carrito: Producto[] = [];
    //     carrito.push(productoNuevo);
    //     localStorage.setItem('productosEnCarrito', JSON.stringify(carrito));
    //   } else {
    //     let carrito: Producto[] = JSON.parse(localStorage.getItem('productosEnCarrito')!);
    //     carrito.push(productoNuevo);
    //     localStorage.setItem('productosEnCarrito', JSON.stringify(carrito));
    //   }
    // }
    // else{
    //   alert("no hay stock")
    // }
    const carrito = localStorage.getItem('carrito');
    let carritoArray: Producto[] = carrito ? JSON.parse(carrito) : [];
    carritoArray.push(this.producto);
    localStorage.setItem('carrito', JSON.stringify(carritoArray));
  }
}
  

