import { Component, Input } from '@angular/core';
import { CompraService } from 'src/app/services/compra.service';
import { Compra } from 'src/app/dominio/compra.model';
import { Producto } from 'src/app/dominio/producto.model';
import { Usuario } from 'src/app/dominio/usuario.model';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {
  constructor(private compraService : CompraService){}

  @Input() usuario?: Usuario; 
  compras?: Compra[];

  private producto1: Producto = {
    id: 1,
    nombre: 'Cafe',
    descripcion: 'Molido',
    precio: 500,
    stock: 20,
    categoria: 'Bebida',
    marca: "Nescafe",
    colores: "Negro"
  };

  private producto2: Producto = {
    id: 3,
    nombre: 'Sushi',
    descripcion: 'Delicioso',
    precio: 1000,
    stock: 10,
    categoria: 'Comida',
    marca: "Fabric",
    colores: "Azul"
  };

  private producto3: Producto = {
    id: 2,
    nombre: 'Pan',
    descripcion: 'Gluten free',
    precio: 700,
    stock: 5,
    categoria: 'Sin gluten',
    marca: 'Bimbo',
    colores: 'Azul'
  };

  private producto4: Producto = {
    id: 7,
    nombre: 'Coca cola',
    descripcion: 'Sin azucar',
    precio: 700,
    stock: 5,
    categoria: 'Bebida',
    marca: 'Coca',
    colores: 'Roja'
  };

  ngOnInit(): void {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    let listaDeCompras : Compra[] = this.compraService.getCompraDelUsuario()
    const compra1 : Compra = {
      Id: 1,
      Productos: [this.producto1, this.producto2, this.producto3],
      FechaCompra: new Date(),
      Precio: 100,
      NombrePromo: 'Promo 1',
      UsuarioId: 1
    };
    const compra2 : Compra = {
      Id: 2,
      Productos: [this.producto3],
      FechaCompra: new Date(),
      Precio: 200,
      NombrePromo: '',
      UsuarioId: 1
    };
    // listaDeCompras = [compra1, compra2];
    this.compras = listaDeCompras;
  }
}
