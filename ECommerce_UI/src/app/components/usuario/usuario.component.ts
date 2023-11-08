import { Component, Input } from '@angular/core';
import { Usuario } from 'src/app/dominio/usuario.model';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css'],
  standalone: true,
  imports: [MatCardModule, MatDividerModule, NgIf, MatButtonModule],
})
export class UsuarioComponent {
  @Input() usuario!: Usuario;

  constructor() {}
}
