using Dominio;

namespace Api.Dtos;

public class CompraModelo
{
    public int Id { get; set; }
    public List<int> Productos { get; set; }
    public int Precio { get; set; }
    public string NombrePromo { get; set; }
    public DateTime FechaCompra { get; set; } 
    public int UsuarioId { get; set; }

    public CompraModelo(Compra compra)
    {
        Id = compra.Id;
        Precio = compra.Precio;
        NombrePromo = compra.NombrePromo;
        FechaCompra = compra.FechaCompra;
        UsuarioId = compra.UsuarioId;
        Productos = compra.Productos.Select(p => p.Id).ToList();
    }
}