import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
} from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedIn: boolean;
  private urlAuthentication: string =
    'https://localhost:7061/api/v1/authentication';
  private urlUsuario: string = 'https://localhost:7061/api/v1/usuarios';

  constructor(private http: HttpClient, private router: Router) {
    this.isLoggedIn = false;
    this.checkLocalStorage();
  }

  response = 'a response';

  checkLocalStorage(): void {
    if (localStorage.getItem('token') != null) {
      this.isLoggedIn = true;
    }
  }

  login(mail: string, password: string): boolean {
    const credenciales = {
      correoElectronico: mail,
      contrasena: password,
    };
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http
      .post(this.urlAuthentication, credenciales, {
        headers,
      })
      .subscribe(
        (response: any) => {
          var token: string = response.token;
          localStorage.setItem('token', 'Bearer ' + token);
          this.isLoggedIn = true;
          this.router.navigate(['/']);
          return true;
        },
        (error) => {
          return false;
        }
      );
    return false;
  }

  signup(mail: string, password: string, direccion: string): boolean {
    const credenciales : object = {
      correoElectronico: mail,
      direccionEntrega: direccion,
      rol: 0,
      contrasena: password,
    };
    const headers : HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    this.http
      .post(this.urlUsuario, credenciales, {
        headers,
      })
      .subscribe(
        (response: any) => {
          this.router.navigate(['/login']);
          return true;
        },
        (error) => {
          return false;
        }
      );
    return false;
  }

  logout(): void {
    localStorage.removeItem('token');
    this.isLoggedIn = false;
  }

  UserIsLoggedIn(): boolean {
    this.checkLocalStorage();
    return this.isLoggedIn;
  }
}