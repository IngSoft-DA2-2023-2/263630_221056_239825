import { Component } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import { ProductosComponent } from '../productos/productos.component';
import { UsuariosComponent } from '../usuarios/usuarios.component';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [MatTabsModule, ProductosComponent, UsuariosComponent, MatButtonModule]
})
export class AdminComponent {
  protected selected = 0;
  constructor (private route : Router){}

  agregarProducto() {
    this.route.navigate(['admin/agregar/producto']);
  }
}
