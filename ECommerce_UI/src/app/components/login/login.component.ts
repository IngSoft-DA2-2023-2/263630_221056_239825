import { Component } from '@angular/core';
import {
  FormControl,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';

@Component({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule,
    NgIf,
    ReactiveFormsModule
  ],
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
})
export class LoginComponent {
  constructor(private router : Router, private auth: AuthService, private _snackBar: MatSnackBar) {}
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]); 

  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'Tiene que ingresar un valor';
    }
    if (this.password.hasError('required')) {
      return 'Tiene que ingresar un valor';
    }
    return this.email.hasError('email') ? 'Mail no valido' : '';
  }

  login() : void {
      if (!this.email.hasError('email')) {
        const emailValue : string = this.email.value ?? '';
        const passwordValue : string = this.password.value ?? '';
        if (emailValue == '' || passwordValue == '') {
          this.openSnackBar('Mail o contraseña invalida', 'OK');
        } else {
          this.auth.login(emailValue, passwordValue).pipe(
            catchError((error : Error) => {
              this.openSnackBar('Error en el formulario: '+error.message , 'Cerrar');
              return [];
            })
          ).subscribe(
            (response: any) => {
              var token: string = response.token;
              var id: string = response.id;
              sessionStorage.setItem('token', 'Bearer ' + token);
              sessionStorage.setItem('idUsuario', id);
              sessionStorage.setItem('usuario', JSON.stringify(response));
              this.router.navigate(['/']);
            }
          );
        }
      } else {
        this.openSnackBar('Mail o contraseña invalida', 'OK');
      }
  }
  
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
