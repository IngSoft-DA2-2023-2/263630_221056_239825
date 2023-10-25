using System.Linq.Expressions;

namespace Dominio;

public class QueryProducto
{
    public string? Nombre { get; set; }
    public int? PrecioEspecifico { get; set; }
    public int? MarcaId { get; set; }
    public int? CategoriaId { get; set; }
    public bool? TienePromociones { get; set; }
    public int? RangoPrecio { get; set; }
}
    