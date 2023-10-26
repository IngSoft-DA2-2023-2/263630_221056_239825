using Dominio;

namespace Api.Dtos;

public class ProductoModelo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public string Descripcion { get; set; }

    public MarcaModelo Marca { get; set; }
    
    public CategoriaModelo Categoria { get; set; }
    
    public ColorModelo Color { get; set; }

    public ProductoModelo(Producto producto)
    {
        Id = producto.Id;
        Nombre = producto.Nombre;
        Precio = producto.Precio;
        Descripcion = producto.Descripcion;
        Marca = new MarcaModelo(producto.Marca);
        Categoria = new CategoriaModelo(producto.Categoria);
        Color = new ColorModelo(producto.Color);
    }

}