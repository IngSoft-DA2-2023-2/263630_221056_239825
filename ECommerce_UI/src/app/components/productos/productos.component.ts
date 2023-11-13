import { Component, OnInit } from '@angular/core';
import { ProductoComponent } from '../producto/producto.component';
import { NgForOf, NgFor, NgIf, NumberFormatStyle } from '@angular/common';
import { Producto } from 'src/app/dominio/producto.model';
import { ProductsService } from 'src/app/services/productos.services';
import { catchError, of, take } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { CategoriaDTO } from 'src/app/dominio/categoria-dto.model';
import { MarcaDTO } from 'src/app/dominio/marca-dto.model';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
interface AplicaPromo {
  nombre: string;
  valor: boolean;
}

@Component({
  standalone: true,
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
  imports: [
    NgForOf,
    ProductoComponent,
    NgFor,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatFormFieldModule,
    NgIf,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
  ],
})
export class ProductosComponent implements OnInit {
  filterForm: FormGroup;
  ArrayProductos: Producto[] = [];
  listaCategorias: CategoriaDTO[] = [];
  selectedValueCategoria!: string;
  listaMarcas: MarcaDTO[] = [];
  selectedValueMarca!: string;

  precioMin: number | undefined;
  precioMax: number | undefined;
  porPrecio: number | undefined;
  porCategoria: string | undefined;
  porMarca: string | undefined;
  porNombre: string | undefined;
  filtroActivo: string | undefined;

  promocion = new FormControl('');
  aplicaPromo: AplicaPromo[] = [
    { nombre: 'Aplica promocion', valor: true },
    { nombre: 'No aplica promocion', valor: false },
  ];

  toggleFiltro(filtro: string) {
    this.filtroActivo = this.filtroActivo === filtro ? undefined : filtro;
    this.aplicarFiltros();
  }

  constructor(private productsServices: ProductsService) {
    this.filterForm = new FormBuilder().group({
      porNombre: [''],
      porPrecio: [''],
      precioRango: [''],
      precioMax: [''],
      precioMin: [''],
    });
  }

  ngOnInit() {
    const params = new HttpParams();
    this.ArrayProductos = this.productsServices.getProducts(params);
    this.productsServices
      .getCategorias()
      .subscribe((categorias: CategoriaDTO[]) => {
        this.listaCategorias = categorias;
      });
    this.productsServices.getMarcas().subscribe((marcas: MarcaDTO[]) => {
      this.listaMarcas = marcas;
    });
    // this.ArrayProductos.push(this.producto1, this.producto2);
  }

  aplicarFiltros() {
    this.actualizarProductos();
  }

  private actualizarProductos() {
    const nombre = this.filterForm.get('porNombre')?.value?.toLowerCase();
    const precio = this.filterForm.get('porPrecio')?.value;
    const precioRange = this.filterForm.get('precioRango')?.value;
    // const categoria = this.filterForm.get('porCategoria')?.value;
    // const marca = this.filterForm.get('porMarca')?.value;
    const promo = this.promocion.value;

    console.log(promo);

    const params = new HttpParams()
      .set('PrecioEspecifico', this.traerPrecioFijoConRango()[0] || '')
      .set('RangoPrecio', this.traerPrecioFijoConRango()[1] || '')
      .set('Nombre', nombre || '')
      .set('TienePromocion', promo || '')
      .set('CategoriaId', Number.parseInt(this.selectedValueCategoria) || '')
      .set('MarcaId', Number.parseInt(this.selectedValueMarca) || '');
    this.ArrayProductos = this.productsServices.getProducts(params);
  }

  traerPrecioFijoConRango(): number[] {
    const minimo = this.filterForm.get('precioMin')?.value;
    const maximo = this.filterForm.get('precioMax')?.value;
    const mediana = (minimo + maximo) / 2;
    const medianaSinDecimales = mediana.toFixed(0);
    const distancia = (maximo - mediana).toFixed(0);
    return [Number(medianaSinDecimales), Number(distancia)];
  }

  // private producto1: Producto = {
  //   id: 1,
  //   nombre: 'Cafe',
  //   descripcion: 'Molido',
  //   precio: 500,
  //   stock: 20,
  //   categoria: 'Bebida',
  //   marca: "Nescafe",
  //   colores: "Negro"
  // };

  // private producto2: Producto = {
  //   id: 1,
  //   nombre: 'helado',
  //   descripcion: 'Molido',
  //   precio: 5000,
  //   stock: 20,
  //   categoria: 'Bebida',
  //   marca: "Nescafe",
  //   colores: "Negro"
  // };
}
