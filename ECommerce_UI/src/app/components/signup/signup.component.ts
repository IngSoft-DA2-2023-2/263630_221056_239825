import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';

import {
  FormControl,
  Validators,
  FormsModule,
  ReactiveFormsModule,
  Form,
} from '@angular/forms';
import { Usuario } from 'src/app/dominio/usuario.model';
import { Observable, catchError } from 'rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule,
    NgIf,
    ReactiveFormsModule,
  ],
})
export class SignupComponent {
  constructor(
    private auth: AuthService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {}
  email: FormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  password: FormControl = new FormControl('', [Validators.required]);
  direccion: FormControl = new FormControl('', [Validators.required]);
  esPaginaAdmin: Boolean = this.router.url.includes('/admin');

  getErrorMessage(): string {
    if (this.email.hasError('required')) {
      return 'Tiene que ingresar un valor';
    }
    if (this.password.hasError('required')) {
      return 'Tiene que ingresar un valor';
    }
    return this.email.hasError('email') ? 'Mail no valido' : '';
  }

  signup(): void {
    if (this.email.invalid || this.password.invalid || this.direccion.invalid) {
      this.openSnackBar('Error en el formulario', 'Cerrar');
      return;
    }
    const emailValue: string = this.email.value ?? '';
    const passwordValue: string = this.password.value ?? '';
    const direccionValue: string = this.direccion.value ?? '';
    if (emailValue == '' || passwordValue == '' || direccionValue == '') {
      this.openSnackBar('Error en el formulario', 'Cerrar');
      return;
    }
    if (passwordValue.length < 8) {
      this.openSnackBar(
        'La contraseña debe tener al menos 8 caracteres',
        'Cerrar'
      );
      return;
    }
    if (!this.esPaginaAdmin) {
      this.auth.signup(emailValue, passwordValue, direccionValue, 0)!
        .pipe(
          catchError((error : Error) => {
            this.openSnackBar('Error en el formulario: '+error.message , 'Cerrar');
            return [];
          })
        )
        .subscribe((response: any) => {
          console.log(response);
        });
    }
  }

  openSnackBar(message: string, action: string): void {
    this._snackBar.open(message, action);
  }
}
