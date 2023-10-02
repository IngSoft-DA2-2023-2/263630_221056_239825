using System.Drawing;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public List<Color> Colores { get; set; }

        public Producto()
        {
            
        }

        public Producto(string nombre, int precio, string descripcion, int marcaId, int categoriaId, List<Color> colores)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            MarcaId = marcaId;
            CategoriaId = categoriaId;
            Colores = colores;
        }
    }
}