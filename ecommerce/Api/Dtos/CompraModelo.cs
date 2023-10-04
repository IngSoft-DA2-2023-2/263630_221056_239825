using Dominio;

namespace Api.Dtos
{
    public class CompraModelo
    {
        public int Id { get; set; }
        public int[] idProductos { get; set; }
    }
}
