namespace Dominio.Usuario
{
    public abstract class Usuario
    {
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public List<Compra> Compras { get; set; }

        public Usuario(string correoElectronico, string direccionEntrega)
        {
            CorreoElectronico = correoElectronico;
            DireccionEntrega = direccionEntrega;
            Compras = new List<Compra>();
        }
    }
}
