using Dominio;
using Dominio.Usuario;

namespace Api.Dtos
{
    public class UsuarioCrearModelo
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public List<Compra> Compras { get; set; }

        public Cliente ToEntity()
        {
            return new Cliente(this.CorreoElectronico, this.DireccionEntrega)
            {
                Id = this.Id,
                Compras = this.Compras
            };
        }
    }
}
