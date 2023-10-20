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
  private isLoggedIn: boolean; // Variable para rastrear el estado de inicio de sesión
  private url : string = 'https://localhost:7061/api/v1/authentication';

  constructor(private http: HttpClient, private router: Router) {
    this.isLoggedIn = false;
    this.checkLocalStorage();
  }
  
  response = 'a response';

  checkLocalStorage() : void {
    if (localStorage.getItem('token') != null) {
      this.isLoggedIn = true;
    }
  }

  login(mail:string, password:string) :void {
    const credenciales = {
      "correoElectronico": mail,
      "contrasena": password,
    };
    // const postData = JSON.stringify(credenciales);	
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http
      .post(this.url, credenciales, {
        headers,
      })
      .subscribe(
        (response: any) => {
          console.log('POST Request Successful:', response);
          var token : string = response.token;
          localStorage.setItem('token', 'Bearer '+token);
          this.isLoggedIn = true;
          this.router.navigate(['/']);
        },
        (error) => {
          console.error('POST Request Error:', error);
          if(error.status == 404){
            // TODO: Manejar el error
            console.log("Error 404")
          }
        }
      );
  }

  // Método para establecer el estado de inicio de sesión a false
  logout() : void {
    localStorage.removeItem('token');
    this.isLoggedIn = false;
  }

  // Método para verificar si el usuario está logueado o no
  UserIsLoggedIn(): boolean {
    return this.isLoggedIn;
  }
}
