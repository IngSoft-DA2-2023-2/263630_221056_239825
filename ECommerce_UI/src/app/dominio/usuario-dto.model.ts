import { Compra } from "./compra.model";

export interface UsuarioDTO {
    id: number;
    correoElectronico: string;
    direccionEntrega: string;
    compras: Compra[];
    contrasena: string;
    rol: number;
  }