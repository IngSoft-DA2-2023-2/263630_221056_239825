import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Producto } from 'src/app/dominio/producto.model';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';
import { ProductsService } from 'src/app/services/productos.services';
import { catchError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationComponent } from '../notification/notification.component';

@Component({
  selector: 'app-modificar-producto',
  templateUrl: './modificar-producto.component.html',
  styleUrls: ['./modificar-producto.component.css'],
  standalone: true,
  imports: [MatCardModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
})
export class ModificarProductoComponent {
  nombre: FormControl = new FormControl('');
  descripcion: FormControl = new FormControl('');
  precio: FormControl = new FormControl('', [Validators.min(0)]);
  stock: FormControl = new FormControl('', [Validators.min(0)]);
  marca: FormControl = new FormControl('');
  colores: FormControl = new FormControl('');
  categoria: FormControl = new FormControl('');

  constructor (private router : Router, private adminService : AdminService, private productoService : ProductsService, private route : ActivatedRoute) { }

  producto : Producto = {
    id: 0,
    nombre: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    marca: '',
    colores: '',
    categoria: '',
  };

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const idDeLaUrl = params['id']; 
      if (idDeLaUrl) {
        this.productoService.getProduct(Number(idDeLaUrl))!.pipe(
          catchError((error: HttpErrorResponse) => {
            if(error.status == 404) {
              this.openNotification('No se encontró el producto');
            } else {
              this.openNotification(error.error.message);
            }
            this.router.navigate(['/admin']);
            return [];
          })
        ).subscribe((producto: Producto) => { this.producto = producto; });
      }
    });
  }

  openNotification(arg0: string) {
    throw new Error('Method not implemented.');
  }

  getErrorMessage(): string {
    if (this.nombre.hasError('email')) {
      return 'Debe ingresar un nombre valido';
    }
    return this.descripcion.hasError('minLength') ? '' : 'La contraseña debe tener 8 caracteres';
  }

  actualizarProducto() : void {

  }

  cancelar() : void{
    this.router.navigate(['/admin']);
  }
}
