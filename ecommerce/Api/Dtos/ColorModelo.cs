using Dominio;

namespace Api.Dtos;

public class ColorModelo
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public ColorModelo(Color color)
    {
        Id = color.Id;
        Nombre = color.Nombre;
    }
}