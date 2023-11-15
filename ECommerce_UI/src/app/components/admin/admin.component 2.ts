import { Component } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import { ProductosComponent } from '../productos/productos.component';
import { UsuariosComponent } from '../usuarios/usuarios.component';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { Compra } from 'src/app/dominio/compra.model';
import { AdminService } from 'src/app/services/admin.service';
import { NumberInput } from '@angular/cdk/coercion';
import { CompraComponent } from '../compra/compra.component';
import { NgFor } from '@angular/common';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [MatTabsModule, ProductosComponent, UsuariosComponent, MatButtonModule, CompraComponent, NgFor]
})
export class AdminComponent {
  protected selected : NumberInput = 0;
  protected compras : Compra[] = []
  constructor (private route : Router, private adminService : AdminService){}

  agregarProducto() {
    this.route.navigate(['admin/agregar/producto']);
  }

  ngOnInit(){
    this.adminService.getCompras().subscribe((data : Compra[]) => {
      this.compras = data;
    })
  }
}
