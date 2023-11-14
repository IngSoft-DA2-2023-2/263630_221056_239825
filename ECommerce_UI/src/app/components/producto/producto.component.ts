import { Component, Input } from '@angular/core';
import { Producto } from '../../dominio/producto.model';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { ProductsService } from 'src/app/services/productos.services';
import { ActivatedRoute, Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { Output, EventEmitter } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { catchError } from 'rxjs';
import { ProductosComponent } from '../productos/productos.component';
import { MatDialog } from '@angular/material/dialog';
import { NotificationComponent } from '../notification/notification.component';

@Component({
  standalone: true,
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css'],
  imports: [MatButtonModule, MatCardModule, MatDividerModule, NgFor],
})
export class ProductoComponent {
  @Output() eliminarProductoClick: EventEmitter<void> = new EventEmitter<void>();
  constructor(
    private activatedRoute: ActivatedRoute,
    private adminService: AdminService,
    // private productosComponent: ProductosComponent,
    private router: Router, 
    private dialog: MatDialog
  ) {}
  @Input() producto!: Producto;
  @Input() seMuestraBoton : boolean = true;

  getBotones(): { texto: string; accion: string }[] {
    const url = this.activatedRoute.snapshot.url.join('/');
    if (url === 'admin') {
      return [
        { texto: 'Modificar Producto', accion: 'Modificar' },
        { texto: 'Eliminar Producto', accion: 'Eliminar' },
      ];
    } else if (url === 'carrito') {
      return [{ texto: 'Eliminar del Carrito', accion: 'Eliminar' }];
    } else {
      return [{ texto: 'Agregar al Carrito', accion: 'Agregar' }];
    }
  }

  realizarAccion(accion: string) {
    if (accion === 'Modificar') {
      this.router.navigate(['/admin/editar/producto', this.producto.id]);
    } else if (accion === 'Eliminar') {
      this.eliminarProducto();
    } else if (accion === 'Agregar') {
      this.agregarAlCarrito();
    }
  }

  eliminarProducto(): void {
    this.eliminarProductoClick.emit();
  
    this.adminService
      .deleteProduct(this.producto.id)
      .pipe(
        catchError((error: Error) => {
          this.openNotification('No se pudo eliminar el producto');
          return [];
        })
      )
      .subscribe((response: any) => {
        this.openNotification('Producto eliminado exitosamente');
        // this.productosComponent.ngOnInit();
      });
  }

  openNotification(mensaje: string): void {
    const dialogRef = this.dialog.open(NotificationComponent, {
      data: { mensaje: mensaje },
    });
  }

  agregarAlCarrito() {
    if(this.producto.stock !=0){
      const carrito = localStorage.getItem('carrito');
      let carritoArray: Producto[] = carrito ? JSON.parse(carrito) : [];
      carritoArray.push(this.producto);
      localStorage.setItem('carrito', JSON.stringify(carritoArray));
      this.openNotification('Producto agregado al carrito');
      this.producto.stock--;
    }else{
      this.openNotification('No hay stock disponible');
    }
    
  }
}
