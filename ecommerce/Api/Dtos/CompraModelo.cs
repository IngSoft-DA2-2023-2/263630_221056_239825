using Dominio;

namespace Api.Dtos
{
    public class CompraModelo
    {
        public int Id { get; set; }
        public List<Producto> Productos { get; set; }
        public Compra ToEntity()
        {
            return new Compra()
            {
                Id = this.Id,
                Productos = this.Productos,
            };
        }
    }
}
