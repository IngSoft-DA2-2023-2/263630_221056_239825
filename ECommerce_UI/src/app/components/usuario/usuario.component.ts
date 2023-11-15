import { Component, Input } from '@angular/core';
import { Usuario } from 'src/app/dominio/usuario.model';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';
import { AdminService } from 'src/app/services/admin.service';
import { TokenUserService } from 'src/app/services/token-user.service';
import { UsuariosComponent } from '../usuarios/usuarios.component';
import { NotificationComponent } from '../notification/notification.component';
import { MatDialog } from '@angular/material/dialog';
import { catchError } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css'],
  standalone: true,
  imports: [
    MatCardModule,
    MatDividerModule,
    NgIf,
    MatButtonModule,
    NotificationComponent,
  ],
})
export class UsuarioComponent {
  @Input() usuario!: Usuario;
  protected comprasDelUsuario! : number;

  constructor(
    private tokenUserService: TokenUserService,
    private usuariosComponent: UsuariosComponent,
    private router : Router,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.comprasDelUsuario = this.usuario.compras.length;
    console.log(this.usuario.compras);
  }

  eliminarUsuario(): void {
    this.tokenUserService
      .deleteUsuario(this.usuario.id).pipe(catchError((error: Error) => {
        this.openNotification('No se pudo eliminar el usuario');
        return [];
      }))
      .subscribe((response: any) => {
        this.openNotification('Usuario eliminado exitosamente');
        this.usuariosComponent.ngOnInit();
      });
  }

  modificarUsuario() : void {
    this.router.navigate(['/admin/editar/usuario/', this.usuario.id]);
  }

  openNotification(mensaje: string): void {
    const dialogRef = this.dialog.open(NotificationComponent, {
      data: { mensaje: mensaje },
    });
  }
}
