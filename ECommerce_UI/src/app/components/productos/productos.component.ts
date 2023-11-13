import { Component, OnInit } from '@angular/core';
import { ProductoComponent } from '../producto/producto.component';
import { NgForOf, NgFor, NgIf } from '@angular/common';
import { Producto } from 'src/app/dominio/producto.model';
import { ProductsService } from 'src/app/services/productos.services';
import { catchError, of, take } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

interface AplicaPromo{
  nombre: string;
  valor: boolean;
}


@Component({
  standalone: true,
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
  imports: [NgForOf, ProductoComponent, NgFor, MatInputModule, MatButtonModule, ReactiveFormsModule, MatSelectModule, MatFormFieldModule, NgIf]
})
export class ProductosComponent implements OnInit {
  filterForm: FormGroup;
  ArrayProductos: Producto[] = [];

  precioRango: 'mayor' | 'menor' | undefined;
  porPrecio: number | undefined;
  porCategoria: string | undefined;
  porMarca: string | undefined;
  porNombre: string | undefined;
  filtroActivo: string | undefined;
  
  promocion = new FormControl('');
  aplicaPromo: AplicaPromo[] = [{nombre: 'Aplica promocion', valor: true}, {nombre: 'No aplica promocion', valor: false}];

  toggleFiltro(filtro: string) {
    this.filtroActivo = this.filtroActivo === filtro ? undefined : filtro;
    this.aplicarFiltros();
  }

  constructor(private productsServices: ProductsService) {
    this.filterForm = new FormBuilder().group({
      porNombre: [''],
      porPrecio: [''],
      precioRango: [''],
      // porCategoria: [''],
      // porMarca: ['']
    });
  }

  ngOnInit() {
    this.ArrayProductos = this.productsServices.getProducts();
    this.actualizarProductos();
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
      .set('PrecioEspecifico', precio || '')
      // .set('Categoria', categoria || '')
      // .set('MarcaId', marca || '')
      .set('RangoPrecio', precioRange || '')
      .set('Nombre', nombre || '')
      .set('TienePromocion', promo || '');
    this.productsServices.getProductosPorFiltro(params).subscribe((productos) => {
      this.ArrayProductos = productos;
    });
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


