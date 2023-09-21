using System.Drawing;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public List<Color> Colores { get; set; }

        public Producto(string nombre, int precio, string descripcion)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
        }
        
        
    }
}