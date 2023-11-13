import { Component, Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';
import { MatButtonModule } from '@angular/material/button';
import { Usuario } from 'src/app/dominio/usuario.model';
import { AdminService } from 'src/app/services/admin.service';
import { TokenUserService } from 'src/app/services/token-user.service';
import { NgFor, NgIf } from '@angular/common';
import { CompraComponent } from '../compra/compra.component';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css'],
  standalone: true,
  imports: [MatButtonModule, CompraComponent, NgFor, NgIf],
})
export class PerfilComponent {
  constructor(private compraService : TokenUserService, private adminService : AdminService, private router : Router){}

  usuario?: Usuario; 
  compras?: Compra[] = [];

  ngOnInit(): void {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    this.compraService.getCompraDelUsuario().pipe(
      catchError((error: HttpErrorResponse) => {
        if(error.status == 404) {
          console.log('No se encontró el usuario');
        } else {
          console.log(error.message);
        }
        return [];
      })
    ).subscribe((compras : Compra[]) => {
      this.compras = compras;
    });
  }

  editarUsuario(){
    this.router.navigate(['/perfil/editar']);
  }

  eliminarUsuario(){
    this.compraService.deleteUsuario(this.usuario!.id).subscribe(() => {
      sessionStorage.clear();
      this.router.navigate(['/']);
    });
  }
}
