import { Component, Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';
import { Usuario } from 'src/app/dominio/usuario.model';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {
  @Input() usuario?: Usuario; 
  compras?: Compra[];

  ngOnInit(): void {
    this.usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    const compra1 : Compra = {
      Id: 1,
      Productos: [],
      Fecha: new Date(),
      Precio: 100,
      NombrePromocion: 'Promo 1',
    };
    const compra2 : Compra = {
      Id: 2,
      Productos: [],
      Fecha: new Date(),
      Precio: 200,
      NombrePromocion: '',
    };
    const listaDeCompras : Compra[] = [compra1, compra2];
    this.compras = listaDeCompras;
  }
}
