import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SignupComponent } from './signup/signup.component';
import { ProductosComponent } from './components/productos/productos.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'productos', component: ProductosComponent }, // TO DO: Falta agregarle el componente
  { path: 'carrito', component: NavbarComponent }, // TODO: Falta agregarle el componente
  { path: 'signup', component: SignupComponent },
  { path: 'perfil', component: NavbarComponent }, // TODO: Falta agregarle el componente
]; 

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
