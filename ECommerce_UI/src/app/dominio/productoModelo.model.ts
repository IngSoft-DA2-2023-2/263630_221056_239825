import { MarcaModelo } from "./marcaModelo.model";
import { CategoriaModelo } from "./categoriaModelo.model";
import { ColorModelo } from "./colorModelo.model";

export interface ProductoModelo {
    id: number;
    nombre: string;
    precio: number;
    descripcion: string;
    stock: number;
    marca: MarcaModelo;
    categoria: CategoriaModelo;
    color: ColorModelo;
}