export interface ProductoDTO {
  nombre: string;
  precio: number;
  descripcion: string;
  marcaId: number;
  categoriaId: number;
  stock: number;
  aplicaParaPromociones: boolean;
  colorId: number;
}
