import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Producto } from 'src/app/dominio/producto.model';
import {
  FormControl,
  ReactiveFormsModule,
  Validators,
  FormsModule,
} from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';
import { ProductsService } from 'src/app/services/productos.services';
import { catchError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationComponent } from '../notification/notification.component';
import { MarcaDTO } from 'src/app/dominio/marca-dto.model';
import { ColorDTO } from 'src/app/dominio/color-dto.model';
import { CategoriaDTO } from 'src/app/dominio/categoria-dto.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-modificar-producto',
  templateUrl: './modificar-producto.component.html',
  styleUrls: ['./modificar-producto.component.css'],
  standalone: true,
  imports: [
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    NgFor,
    MatFormFieldModule,
    MatSelectModule,
    NgIf,
    FormsModule,
  ],
})
export class ModificarProductoComponent {
  nombre: FormControl = new FormControl('');
  descripcion: FormControl = new FormControl('');
  precio: FormControl = new FormControl('', [Validators.min(0)]);
  stock: FormControl = new FormControl('', [Validators.min(0)]);
  marca: FormControl = new FormControl('');
  colores: FormControl = new FormControl('');
  categoria: FormControl = new FormControl('');
  listaMarcas: MarcaDTO[] = [];
  listaColores: ColorDTO[] = [];
  listaFormColores: FormControl = new FormControl([]);
  selectedValue!: string;
  listaCategorias: CategoriaDTO[] = [];
  editarProducto: Boolean = false;

  constructor(
    private router: Router,
    private adminService: AdminService,
    private productoService: ProductsService,
    private route: ActivatedRoute
  ) {}

  producto: Producto = {
    id: 0,
    nombre: 'Un producto',
    descripcion: 'Una descripción',
    precio: 0,
    stock: 0,
    marca: '',
    colores: '',
    categoria: '',
  };

  ngOnInit(): void {
    if (this.router.url.includes('editar')) {
      this.route.params.subscribe((params) => {
        const idDeLaUrl = params['id'];
        if (idDeLaUrl) {
          this.productoService
            .getProduct(Number(idDeLaUrl))!
            .pipe(
              catchError((error: HttpErrorResponse) => {
                if (error.status == 404) {
                  this.openNotification('No se encontró el producto');
                } else {
                  this.openNotification(error.error.message);
                }
                this.router.navigate(['/admin']);
                return [];
              })
            )
            .subscribe((producto: Producto) => {
              this.producto = producto;
            });
        }
      });
    }
    this.productoService.getColores().subscribe((colores: ColorDTO[]) => {
      this.listaColores = colores;
    });
    this.productoService
      .getCategorias()
      .subscribe((categorias: CategoriaDTO[]) => {
        this.listaCategorias = categorias;
      });
    this.productoService.getMarcas().subscribe((marcas: MarcaDTO[]) => {
      this.listaMarcas = marcas;
    });
  }

  openNotification(arg0: string) {
    throw new Error('Method not implemented.');
  }

  getErrorMessage(): string {
    if (this.nombre.hasError('email')) {
      return 'Debe ingresar un nombre valido';
    }
    return this.descripcion.hasError('minLength')
      ? ''
      : 'La contraseña debe tener 8 caracteres';
  }

  accionAceptar(): void {}

  cancelar(): void {
    this.router.navigate(['/admin']);
  }
}
