import { Component, Input } from '@angular/core';
import { Compra } from 'src/app/dominio/compra.model';
import { Producto } from 'src/app/dominio/producto.model';
import { Usuario } from 'src/app/dominio/usuario.model';
import { AdminService } from 'src/app/services/admin.service';
import { TokenUserService } from 'src/app/services/token-user.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {
  constructor(private compraService : TokenUserService, private adminService : AdminService){}

  @Input() usuario?: Usuario; 
  compras?: Compra[];

  ngOnInit(): void {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    let listaDeCompras : Compra[] = this.compraService.getCompraDelUsuario()
    let listaDeUsuarios : Usuario[] = this.adminService.getUsuarios();
    this.compras = listaDeCompras;
  }
}
