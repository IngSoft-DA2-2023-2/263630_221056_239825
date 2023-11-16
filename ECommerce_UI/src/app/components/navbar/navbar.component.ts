import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';

import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { Usuario } from 'src/app/dominio/usuario.model';
import {MatSidenavModule} from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, NgIf, RouterModule, MatSidenavModule,MatIconModule],
  providers: [AuthService],
})
export class NavbarComponent {
  constructor(private authService: AuthService, private router: Router) {}
  protected isLoggedIn: boolean = this.authService.UserIsLoggedIn();
  protected isAdmin : boolean = false;
  protected isMobile: boolean = false;
  protected showMobileMenu: boolean = false;

  ngOnInit(): void {
    this.router.events.subscribe((val : any) => {
      if(val.url){
        this.isLoggedIn = this.authService.UserIsLoggedIn();
        if(sessionStorage.getItem('usuario') != null){
          const usuario : Usuario = JSON.parse(sessionStorage.getItem('usuario')!);
          if(usuario.rol == 1 || usuario.rol == 2){
            this.isAdmin = true;
          } else {
            this.isAdmin = false;
          }
        }
      }
    }
    )
    this.checkScreenSize();
  }

  checkScreenSize() {
    this.isMobile = window.innerWidth < 500;
  }

  toggleMobileMenu() {
    // Muestra u oculta el menú en dispositivos móviles
    this.showMobileMenu = !this.showMobileMenu;
  }

  logout(): void {
    this.authService.logout();
    this.isLoggedIn = false;
    this.router.navigate(['/']);
  }
}
