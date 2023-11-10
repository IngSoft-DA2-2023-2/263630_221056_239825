import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { LoginComponent } from './components/login/login.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { ProductosComponent } from './components/productos/productos.component';
import { SignupComponent } from './components/signup/signup.component';
import { ProductoComponent } from './components/producto/producto.component';
import { MatStepperModule } from '@angular/material/stepper';

import { TarjetaComponent } from './components/tarjeta/tarjeta.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { CompraComponent } from './components/compra/compra.component';
import { AdminComponent } from './components/admin/admin.component';
import { UsuarioComponent } from './components/usuario/usuario.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { CarritoComponent } from './components/carrito/carrito.component';
import { OpcionesPagoComponent } from './components/opciones-pago/opciones-pago.component';
@NgModule({
  declarations: [AppComponent, PerfilComponent, CarritoComponent, OpcionesPagoComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    HttpClientModule,
    NavbarComponent,
    LoginComponent,
    SignupComponent,
    TarjetaComponent,
    ProductoComponent,
    ProductosComponent,
    CompraComponent,
    AdminComponent,
    UsuarioComponent,
    UsuariosComponent,
    MatStepperModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
