import { Component } from '@angular/core';
import {
  FormControl,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule,
    NgIf,
  ],
})
export class LoginComponent {
  constructor(private auth: AuthService, private _snackBar: MatSnackBar) {}
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
          const loggedIn : boolean = this.auth.login(emailValue, passwordValue);
          if (!loggedIn) {
            this.openSnackBar('Mail o contraseña invalida', 'OK');
          }
        }
      } else {
        this.openSnackBar('Mail o contraseña invalida', 'OK');
      }
  }
  
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
