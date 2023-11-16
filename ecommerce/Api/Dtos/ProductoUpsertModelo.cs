using Dominio;

namespace Api.Dtos;

public class ProductoUpsertModelo
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public string Descripcion { get; set; }

    public int MarcaId { get; set; }
    
    public int CategoriaId { get; set; }
    public int Stock { get; set; }
    public bool AplicaParaPromociones { get; set; }
    
    public int ColorId { get; set; }
    
    public Producto AEntidad()
    {
        return new Producto(Nombre, Precio, Descripcion, MarcaId, CategoriaId, Stock, AplicaParaPromociones, ColorId);
    }

}