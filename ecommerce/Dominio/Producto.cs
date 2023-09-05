namespace Dominio
{
    public class Producto
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string Color { get; set; }

        public Producto(string nombre, int precio, string descripcion, string marca, string categoria, string color)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            Marca = marca;
            Categoria = categoria;
            Color = color;
        }
    }
}