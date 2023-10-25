import { Producto } from "./producto.model";

export interface Compra {
    Id: number,
    Productos: Producto[],
    Fecha: Date,
    Precio: number,
    NombrePromocion: string,
}