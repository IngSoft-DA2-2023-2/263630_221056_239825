import { Component } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button'; 

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['../app.component.scss'],
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule]
})
export class NavbarComponent {
  
}
