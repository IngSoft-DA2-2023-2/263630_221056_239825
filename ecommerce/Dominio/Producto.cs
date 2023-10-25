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
        public int ColorId { get; set; }
        public int CategoriaId { get; set; }
        public int Stock { get; set; }
        public bool AplicaParaPromociones { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public Color Color { get; set; }
        public List<Compra> Compras { get; set; }

        public Producto()
        {
            
        }

        public Producto(string nombre, int precio, string descripcion, int marcaId, int categoriaId, int stock, bool promociones, int colorId)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            MarcaId = marcaId;
            CategoriaId = categoriaId;
            ColorId = colorId;
            Stock = stock;
            AplicaParaPromociones = promociones;

        }
    }
}