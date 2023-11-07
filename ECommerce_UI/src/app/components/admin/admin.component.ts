import { Component } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import { ProductosComponent } from '../productos/productos.component';
import { UsuariosComponent } from '../usuarios/usuarios.component';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [MatTabsModule, ProductosComponent, UsuariosComponent]
})
export class AdminComponent {

}
