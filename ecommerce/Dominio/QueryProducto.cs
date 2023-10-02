using System.Linq.Expressions;

namespace Dominio;

public class QueryProducto
{
    public string? Nombre { get; set; }
    public int? Precio { get; set; }
    public int? MarcaId { get; set; }
    public int? CategoriaId { get; set; }
}
    