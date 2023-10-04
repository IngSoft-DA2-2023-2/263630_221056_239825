using Dominio;

namespace Api.Dtos;

public class MarcaModelo
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public MarcaModelo(Marca marca)
    {
        Id = marca.Id;
        Nombre = marca.Nombre;
    }
}