import { Producto } from "./producto.model";

export interface Compra {
    Id: number,
    Productos: Producto[],
    FechaCompra: Date,
    Precio: number,
    NombrePromo: string,
    UsuarioId: number
}