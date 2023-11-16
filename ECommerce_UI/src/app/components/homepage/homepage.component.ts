import { Component } from '@angular/core';
import { ProductoComponent } from '../producto/producto.component';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css'],
  standalone: true,
  imports: [ProductoComponent]
})
export class HomepageComponent {

}
