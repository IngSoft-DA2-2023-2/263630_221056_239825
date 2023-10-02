using Dominio;

namespace Api.Dtos;

public class CategoriaModelo
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public CategoriaModelo(Categoria categoria)
    {
        Id = categoria.Id;
        Nombre = categoria.Nombre;
    }
}