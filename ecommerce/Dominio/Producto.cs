namespace Dominio
{
    public class Producto
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public List<string> Marcas { get; set; }
        public List<string> Categorias { get; set; }
        public List<string> Colores { get; set; }

        public Producto(string nombre, int precio, string descripcion, List<string> marcas, List<string> categorias, List<string> colores)
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