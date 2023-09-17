using System.Drawing;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public Marca Marcas { get; set; }
        public Categoria Categorias { get; set; }
        public List<Color> Colores { get; set; }

        public List<ColorPorProducto> ColoresDelProducto { get; set; }

        public Producto(string nombre, int precio, string descripcion, Marca marcas, Categoria categorias, List<Color> colores)
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