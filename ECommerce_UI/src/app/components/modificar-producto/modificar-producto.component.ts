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
import { ProductoDTO } from 'src/app/dominio/producto-dto.model';
interface AplicaPromo {
  nombre: string;
  valor: boolean;
}
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
    FormsModule,
    NgFor,
    MatFormFieldModule,
    MatSelectModule,
    NgIf,
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
  listaColores: ColorDTO[] = [];
  listaFormColores: FormControl = new FormControl('');
  listaAplicaPromo: AplicaPromo[] = [
    {
      nombre: 'No aplica para promociones',
      valor: false,
    },
    {
      nombre: 'Aplica para promociones',
      valor: true,
    },
  ];
  selectedValueAplicaPromo!: boolean;
  listaCategorias: CategoriaDTO[] = [];
  selectedValueCategoria!: string;
  listaMarcas: MarcaDTO[] = [];
  selectedValueMarca!: string;
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
      this.editarProducto = true;
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
    alert(arg0);
  }

  getErrorMessage(): string {
    if (this.nombre.hasError('email')) {
      return 'Debe ingresar un nombre valido';
    }
    return this.descripcion.hasError('minLength')
      ? ''
      : 'La contraseña debe tener 8 caracteres';
  }

  accionAceptar(): void {
    if (this.editarProducto) {
      this.modificarProducto();
    } else {
      this.crearProducto();
    }
  }

  modificarProducto(): void {
    if (
      this.nombre.value ||
      this.descripcion.value ||
      this.precio.value !== null ||
      this.stock.value !== null ||
      this.listaFormColores.value ||
      this.selectedValueCategoria ||
      this.selectedValueMarca ||
      this.selectedValueAplicaPromo !== null
    ) {
      this.openNotification('Se modificó el producto');
    } else {
      this.openNotification('Debe completar todos los campos');
    }
  }

  crearProducto(): void {
    if (
      this.nombre.value &&
      this.descripcion.value &&
      this.precio.value !== null &&
      this.stock.value !== null &&
      this.listaFormColores.value &&
      this.selectedValueCategoria &&
      this.selectedValueMarca &&
      this.selectedValueAplicaPromo !== null
    ) {
      this.adminService
        .createProduct(
          this.crearProductoDTO(
            this.nombre.value,
            this.descripcion.value,
            this.precio.value,
            this.stock.value,
            this.getIdsFromColorNames(this.listaFormColores.value),
            Number.parseInt(this.selectedValueCategoria),
            Number.parseInt(this.selectedValueMarca),
            this.selectedValueAplicaPromo
          )
        )
        .subscribe((producto: ProductoDTO) => {
          this.openNotification('Se creó el producto');
        });
    } else {
      this.openNotification('Debe completar todos los campos');
    }
  }

  getIdsFromColorNames(colorNames: string[]): number {
    const ids: number[] = [];

    colorNames.forEach((colorName: string) => {
      const colorDTO = this.listaColores.find(
        (color: ColorDTO) => color.nombre === colorName
      );

      if (colorDTO) {
        ids.push(colorDTO.id);
      }
    });

    return ids[0];
  }

  crearProductoDTO(
    nombre: string,
    descripcion: string,
    precio: number,
    stock: number,
    color: number,
    categoria: number,
    marca: number,
    aplicaPromo: boolean
  ): ProductoDTO {
    return {
      nombre: nombre,
      descripcion: descripcion,
      precio: precio,
      stock: stock,
      colorId: color,
      categoriaId: categoria,
      marcaId: marca,
      aplicaParaPromociones: aplicaPromo,
    };
  }

  cancelar(): void {
    this.router.navigate(['/admin']);
  }
}
