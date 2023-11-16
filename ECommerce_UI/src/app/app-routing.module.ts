import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { ProductosComponent } from './components/productos/productos.component';
import { CarritoComponent } from './components/carrito/carrito.component';

import { PerfilComponent } from './components/perfil/perfil.component';
import { AdminComponent } from './components/admin/admin.component';
import { TarjetaComponent } from './components/tarjeta/tarjeta.component';
import { OpcionesPagoComponent } from './components/opciones-pago/opciones-pago.component';
import { ModificarUsuarioComponent } from './components/modificar-usuario/modificar-usuario.component';
import { ModificarProductoComponent } from './components/modificar-producto/modificar-producto.component';
import { HomepageComponent } from './components/homepage/homepage.component';
import { AuthGuard } from './auth.guard';
const routes: Routes = [
  { path: '', component: HomepageComponent},
  { path: 'login', component: LoginComponent },
  { path: 'productos', component: ProductosComponent },
  { path: 'carrito', component: CarritoComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard] },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard] },
  { path: 'admin/signup', component: SignupComponent, canActivate: [AuthGuard] },
  { path: 'admin/editar/usuario/:id', component: ModificarUsuarioComponent, canActivate: [AuthGuard] },
  { path: 'admin/editar/producto/:id', component: ModificarProductoComponent, canActivate: [AuthGuard] },
  { path: 'admin/agregar/producto', component: ModificarProductoComponent, canActivate: [AuthGuard] },
  { path: 'perfil/editar', component: ModificarUsuarioComponent, canActivate: [AuthGuard] },
  { path: 'opcionesPago', component: OpcionesPagoComponent}
]; 

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
