import { Component, Input } from '@angular/core';
import { Producto } from '../../dominio/producto.model';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { ProductsService } from 'src/app/services/productos.services';
import { ActivatedRoute, Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { Output, EventEmitter } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css'],
  imports: [MatButtonModule, MatCardModule, MatDividerModule, NgFor],
})

export class ProductoComponent {
  @Output() eliminarProductoClick: EventEmitter<void> = new EventEmitter<void>();

  constructor(private activatedRoute: ActivatedRoute, private productsServices : ProductsService, private router : Router){ }
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
      this.router.navigate(['/admin/editar/producto', this.producto.id]);
    } else if (accion === 'Eliminar') {
      this.eliminarProductoClick.emit();
    } else if (accion === 'Agregar') {
      this.agregarAlCarrito();
    }
  }

  agregarAlCarrito() {
    const carrito = localStorage.getItem('carrito');
    let carritoArray: Producto[] = carrito ? JSON.parse(carrito) : [];
    carritoArray.push(this.producto);
    localStorage.setItem('carrito', JSON.stringify(carritoArray));
  }
}
  

