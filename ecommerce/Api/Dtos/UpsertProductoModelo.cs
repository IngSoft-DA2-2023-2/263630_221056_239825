using Dominio;

namespace Api.Dtos;

public class UpsertProductoModelo
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public string Descripcion { get; set; }

    public int MarcaId { get; set; }
    
    public int CategoriaId { get; set; }
    
    public List<int> Colores { get; set; }
    
    public Producto AEntidad()
    {
        return new Producto(Nombre, Precio, Descripcion, MarcaId, CategoriaId, Colores.Select(c => new Color {Id = c}).ToList());
    }

}