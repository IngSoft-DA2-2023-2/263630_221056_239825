import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { ProductoComponent } from '../producto/producto.component';
import { NgFor } from '@angular/common';
import { TokenUserService } from 'src/app/services/token-user.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css'],
  imports: [MatDividerModule, MatCardModule, ProductoComponent, NgFor, CommonModule],
})
export class CompraComponent {
  @Input() compra?: Compra;

  ngOnInit(): void {
    // console.log('Compra en CompraComponent:', this.compra);
  }
}
