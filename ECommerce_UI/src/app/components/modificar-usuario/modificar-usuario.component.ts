import { Component, Input } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { NgIf } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import {
  FormControl,
  Validators,
  FormsModule,
  ReactiveFormsModule,
  Form,
  FormBuilder,
} from '@angular/forms';
import { Observable, catchError } from 'rxjs';
import { Usuario } from 'src/app/dominio/usuario.model';
import { AdminService } from 'src/app/services/admin.service';
import { TokenUserService } from 'src/app/services/token-user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { NotificationComponent } from '../notification/notification.component';

@Component({
  selector: 'app-modificar-usuario',
  templateUrl: './modificar-usuario.component.html',
  styleUrls: ['./modificar-usuario.component.css'],
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule,
    NgIf,
    ReactiveFormsModule,
    MatCheckboxModule,
  ],
})
export class ModificarUsuarioComponent {
  usuario : Usuario = {
    id: 0,
    correoElectronico: 'unCorreo',
    compras: [],
    direccionEntrega: '',
    rol: 0,
  };

  constructor(
    private auth: AuthService,
    private adminService : AdminService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private dialog: MatDialog
  ) {}
  email: FormControl = new FormControl('', [
    Validators.email,
  ]);
  password: FormControl = new FormControl('', [Validators.minLength(8)]);
  direccion: FormControl = new FormControl('');
  esPaginaAdmin: Boolean = this.router.url.includes('/admin');
  roles = this._formBuilder.group({
    cliente: false,
    admin: false,
  });

  ngOnInit(): void {
    if (this.esPaginaAdmin) {
      this.route.params.subscribe(params => {
        const idDeLaUrl = params['id']; 
        if (idDeLaUrl) {
          this.adminService.getUsuario(Number(idDeLaUrl)).pipe(
            catchError((error: HttpErrorResponse) => {
              this.openNotification(error.error.message);
              this.router.navigate(['/admin']);
              return [];
            })
          ).subscribe((usuario: Usuario) => { this.usuario = usuario; });
        }
      });
    } else {
      this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    }
  }

  getErrorMessage(): string {
    if (this.email.hasError('email')) {
      return 'Debe ingresar un mail valido';
    }
    return this.password.hasError('minLength') ? '' : 'La contraseña debe tener 8 caracteres';
  }

  signup(): void {
    if (this.email.invalid || this.password.invalid || this.direccion.invalid) {
      this.openSnackBar('Error en el formulario', 'Cerrar');
      return;
    }
    const emailValue: string = this.email.value ?? this.usuario.correoElectronico;
    const passwordValue: string = this.password.value ?? '';
    const direccionValue: string = this.direccion.value ?? this.usuario.direccionEntrega;
    if (passwordValue.length < 8 && passwordValue.length > 0) {
      this.openSnackBar(
        'La contraseña debe tener al menos 8 caracteres',
        'Cerrar'
      );
      return;
    }
    if (!this.esPaginaAdmin) {
      this.auth
        .signup(emailValue, passwordValue, direccionValue, 0)!
        .pipe(
          catchError((error: Error) => {
            this.openSnackBar(
              'Error en el formulario: ' + error.message,
              'Cerrar'
            );
            return [];
          })
        )
        .subscribe((response: any) => {
          this.router.navigate(['/login']);
        });
    } else {
      let rol: number = -1;
      if (this.roles.value.admin && this.roles.value.cliente) {
        rol = 2;
      } else if (this.roles.value.admin) {
        rol = 1;
      } else if (this.roles.value.cliente) {
        rol = 0;
      }
      this.auth
        .signup(emailValue, passwordValue, direccionValue, rol)!
        .pipe(
          catchError((error: Error) => {
            this.openSnackBar(error.message, 'Cerrar');
            return [];
          })
        )
        .subscribe((response: any) => {
          this.openSnackBar('Usuario creado con éxito', 'Cerrar');
        });
    }
  }

  cancelar(): void {
    if (this.esPaginaAdmin) {
      this.router.navigate(['/admin']);
    } else {
      this.router.navigate(['/perfil']);
    }
  }

  openSnackBar(message: string, action: string): void {
    this._snackBar.open(message, action);
  }

  openNotification(mensaje: string): void {
    const dialogRef = this.dialog.open(NotificationComponent, {
      data: { mensaje: mensaje },
    });
  }
}
function ngOnInit(): (target: ModificarUsuarioComponent, propertyKey: "") => void {
  throw new Error('Function not implemented.');
}

