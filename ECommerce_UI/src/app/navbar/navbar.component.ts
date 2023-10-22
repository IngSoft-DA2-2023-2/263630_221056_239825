import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';

import { AuthService } from '../auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, NgIf, RouterModule],
  providers: [AuthService],
})
export class NavbarComponent {
  constructor(private authService: AuthService, private router: Router) {}
  protected isLoggedIn: boolean = this.authService.UserIsLoggedIn();

  ngOnInit(): void {
    this.router.events.subscribe((val : any) => {
      if(val.url){
        this.isLoggedIn = this.authService.UserIsLoggedIn();
        console.log(this.isLoggedIn);
      }
    }
    )
  }

  logout(): void {
    this.authService.logout();
    this.isLoggedIn = false;
    this.router.navigate(['/']);
  }
}
