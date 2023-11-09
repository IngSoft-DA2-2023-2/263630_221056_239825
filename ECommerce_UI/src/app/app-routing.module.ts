import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { ProductosComponent } from './components/productos/productos.component';
import { CarritoComponent } from './components/carrito/carrito.component';

import { TarjetaComponent } from './components/tarjeta/tarjeta.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { AdminComponent } from './components/admin/admin.component';
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'productos', component: ProductosComponent },
  // { path: 'carrito', component: TarjetaComponent }, 
  { path: 'carrito', component: CarritoComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'perfil', component: PerfilComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'admin/signup', component: SignupComponent },
]; 

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
