import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Usuario } from '../dominio/usuario.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedIn: boolean;
  private urlGeneral : string = 'https://localhost:7061/api/v1';
  private urlAuthentication: string = this.urlGeneral + '/authentication';
  private urlUsuario: string = this.urlGeneral + '/usuarios';

  constructor(private http: HttpClient, private router: Router) {
    this.isLoggedIn = false;
    this.checkLocalStorage();
  }

  response = 'a response';

  checkLocalStorage(): void {
    if (sessionStorage.getItem('token') != null) {
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
          var id : string = response.id;
          sessionStorage.setItem('token', 'Bearer ' + token);
          sessionStorage.setItem('idUsuario', id);
          this.isLoggedIn = true;
          const usuario = this.createUser(response);
          sessionStorage.setItem('usuario', JSON.stringify(usuario));
          this.router.navigate(['/']);
          return true;
        },
        (error) => {
          alert(error.message);
          return false;
        }
      );
      
    return false;
  }

  createUser(response : any){
    const usuario : Usuario = {
      correoElectronico: response.correoElectronico,
      direccionEntrega: response.direccionEntrega,
      rol: response.rol,
      compras: response.compras,
    }
    return usuario;
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
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('usuario');
    this.isLoggedIn = false;
  }

  UserIsLoggedIn(): boolean {
    this.checkLocalStorage();
    return this.isLoggedIn;
  }
}
