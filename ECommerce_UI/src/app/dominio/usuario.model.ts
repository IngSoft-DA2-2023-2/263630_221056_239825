import { Compra } from "./compra.model";

export interface Usuario {
  id: number;
  correoElectronico: string;
  direccionEntrega: string;
  compras: Compra[];
  rol: number;
}