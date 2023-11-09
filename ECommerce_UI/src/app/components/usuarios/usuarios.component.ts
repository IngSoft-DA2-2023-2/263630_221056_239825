import { Component, NgModule } from '@angular/core';
import { Usuario } from 'src/app/dominio/usuario.model';
import { AdminService } from 'src/app/services/admin.service';
import { NgFor, NgIf } from '@angular/common';
import { UsuarioComponent } from '../usuario/usuario.component';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css'],
  standalone: true,
  imports: [
    UsuarioComponent,
    NgFor,
    MatButtonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    NgIf,
  ],
})
export class UsuariosComponent {
  filterForm: FormGroup;
  filtros = new FormControl([]);
  listaDeFiltros: string[] = [
    'Administrador',
    'Cliente',
    'Sin Compras',   
    'Con Compras',
  ];
  constructor(private adminService: AdminService) {
    this.filterForm = new FormBuilder().group({
      correoElectronico: [''],
    });
  }
  protected usuarios: Usuario[] = [];
  ngOnInit(): void {
    this.usuarios = this.adminService.getUsuarios();
  }

  applyFilter() {
    const correo = this.filterForm
      .get('correoElectronico')
      ?.value?.toLowerCase();
    const esAdmin = this.filtros?.value!.some((rol) => rol === 'Administrador');
    const noEsAdmin = this.filtros?.value?.some((rol) => rol === 'Cliente');
    const tieneCompras = this.filtros?.value?.some((rol) => rol === 'Con Compras');
    const noTieneCompras = this.filtros?.value?.some((rol) => rol === 'Sin Compras');
    if (
      !esAdmin &&
      !noEsAdmin &&
      !tieneCompras &&
      !noTieneCompras &&
      correo == ''
    ) {
      this.usuarios = this.adminService.getUsuarios();
      return;
    }
    this.usuarios = this.usuarios.filter((usuario) => {
      let matchesUsername = true;
      let matchesAdminStatus = true;
      let matchesCompras = true;
      if (!esAdmin || !noEsAdmin){
        if (esAdmin) {
          matchesAdminStatus = usuario.rol == 1 || usuario.rol == 2;
        }
        if (noEsAdmin) {
          matchesAdminStatus = usuario.rol == 0;
        }
      }
      if (!tieneCompras || !noTieneCompras){
        if (tieneCompras) {
          matchesCompras = usuario.compras.length > 0;
        }
        if (noTieneCompras) {
          matchesCompras = usuario.compras.length == 0;
        }
      }
      if (correo != '') {
        matchesUsername = usuario.correoElectronico
          .toLowerCase()
          .includes(correo);
      }
      return matchesUsername && matchesAdminStatus && matchesCompras;
    });
  }
}
