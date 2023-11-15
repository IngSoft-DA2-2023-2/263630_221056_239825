import { Producto } from "./producto.model";

export interface Compra {
    id: number,
    productos: Producto[],
    fechaCompra: Date,
    precio: number,
    nombrePromo: string,
    usuarioId: number
}