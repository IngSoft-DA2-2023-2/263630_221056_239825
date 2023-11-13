import { Component, OnInit } from '@angular/core';
import { ProductoComponent } from '../producto/producto.component';
import { NgForOf, NgFor} from '@angular/common';
import { Producto } from 'src/app/dominio/producto.model';
import { ProductsService } from 'src/app/services/productos.services';
import { catchError, of, take } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import {MatInputModule} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
  imports: [NgForOf, ProductoComponent, NgFor, MatInputModule, MatButtonModule, ReactiveFormsModule]
})
export class ProductosComponent implements OnInit{
  filterForm: FormGroup;
  ArrayProductos: Producto[] = [];

  // precioPorOrden: 'mayor' | 'menor' | undefined;
  porPrecio: number | undefined;
  porCategoria: string | undefined;
  porMarca: string | undefined;
  porNombre: string | undefined;
  filtroActivo: string | undefined;

toggleFiltro(filtro: string) {
  this.filtroActivo = this.filtroActivo === filtro ? undefined : filtro;
  this.aplicarFiltros();
}

  constructor(private productsServices : ProductsService){
    this.filterForm = new FormBuilder().group({
      porNombre: [''], 
      // porPrecio: [''],
      // porCategoria: [''],
      // porMarca: ['']
    });
   }
  
  ngOnInit(){
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
    const categoria = this.filterForm.get('porCategoria')?.value;
    const marca = this.filterForm.get('porMarca')?.value;

    const params = new HttpParams()
      // .set('Precio', precio || '')
      // .set('Categoria', categoria || '')
      // .set('MarcaId', marca || '')
      .set('Nombre', nombre || '');
    this.productsServices.getProductosPorFiltro(params).subscribe((productos) => {
      this.ArrayProductos = productos;
    });
    // console.log(this.ArrayProductos);
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


