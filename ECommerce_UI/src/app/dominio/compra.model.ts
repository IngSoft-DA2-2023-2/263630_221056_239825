import { Producto } from "./producto.model";

export interface Compra {
    id: number,
    productos: number[],
    fechaCompra: Date,
    precio: number,
    nombrePromo: string,
    usuarioId: number
}