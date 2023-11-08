import { Compra } from "./compra.model";

export interface Usuario {
  correoElectronico: string;
  direccionEntrega: string;
  compras: Compra[];
  rol: number;
}