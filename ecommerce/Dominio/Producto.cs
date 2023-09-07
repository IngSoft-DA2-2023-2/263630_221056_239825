namespace Dominio
{
    public class Producto
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public Marca Marcas { get; set; }
        public Categoria Categorias { get; set; }
        public Color Colores { get; set; }

        public Producto(string nombre, int precio, string descripcion, Marca marcas, Categoria categorias, Color colores)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            Marcas = marcas;
            Categorias = categorias;
            Colores = colores;
        }
    }
}