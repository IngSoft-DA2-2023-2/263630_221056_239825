import { Component } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import { ProductosComponent } from '../productos/productos.component';
import { UsuariosComponent } from '../usuarios/usuarios.component';
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [MatTabsModule, ProductosComponent, UsuariosComponent]
})
export class AdminComponent {
  protected selected = 0;
}
