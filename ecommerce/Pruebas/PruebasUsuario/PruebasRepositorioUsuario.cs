/*using Moq;
using DataAccess.Interfaces;
using DataAccess;
using Dominio.Usuario;
using Dominio;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasRepositorioUsuario
    {
        private Usuario? unCliente;
        private List<Usuario>? usuarios;
        IRepositorioUsuario? repositorioUsuario;
        [TestInitialize]
        public void InitTest()
        {
            unCliente = new Usuario("martin@edelman.com.uy", "Zorrilla 124", "12345678") {
                Rol = CategoriaRol.Cliente
            };
            usuarios = new List<Usuario> { unCliente };
            repositorioUsuario = new RepositorioUsuario(_dbContext);
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            repositorioUsuario!.AgregarUsuario(unCliente!);

            // Assert
            List<Usuario> usuariosObtenidos = repositorioUsuario.ObtenerUsuarios();
            repositorioUsuario!.EliminarUsuario(unCliente!);
            Assert.AreEqual(usuarios!.Count(), usuariosObtenidos.Count());
            Assert.AreEqual(unCliente!.CorreoElectronico, usuariosObtenidos.Last().CorreoElectronico);
        }

        [TestMethod]
        public void ObtenerUsuarioPorCorreoOk()
        {
            repositorioUsuario!.AgregarUsuario(unCliente!);
            Usuario usuarioObtenido = repositorioUsuario!.ObtenerUsuario(u => u.CorreoElectronico == unCliente!.CorreoElectronico);
            repositorioUsuario!.EliminarUsuario(unCliente!);

            Assert.IsNotNull(usuarioObtenido);
            Assert.AreEqual(unCliente!.CorreoElectronico, usuarioObtenido.CorreoElectronico);
        }

        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            repositorioUsuario!.AgregarUsuario(unCliente!);
            List<Usuario> usuariosObtenidos = repositorioUsuario!.ObtenerUsuarios();
            repositorioUsuario!.EliminarUsuario(unCliente!);

            Assert.AreEqual(usuariosObtenidos.Count(), usuarios!.Count());
            Assert.AreEqual(usuariosObtenidos[0].CorreoElectronico, usuarios![0].CorreoElectronico);
        }

        [TestMethod]
        public void ActualizarUsuarioOk()
        {
            repositorioUsuario!.AgregarUsuario(unCliente!);
            string nuevaDireccion = "Nueva Dirección 123";
            unCliente!.DireccionEntrega = nuevaDireccion;
            repositorioUsuario!.ActualizarUsuario(unCliente);
            Usuario usuarioActualizado = repositorioUsuario.ObtenerUsuario(u => u.CorreoElectronico == unCliente.CorreoElectronico);
            repositorioUsuario.EliminarUsuario(unCliente!);

            Assert.IsNotNull(usuarioActualizado);
            Assert.AreEqual(nuevaDireccion, usuarioActualizado.DireccionEntrega);
        }

        [TestMethod]
        public void EliminarUsuarioOk()
        {
            repositorioUsuario!.AgregarUsuario(unCliente!);
            repositorioUsuario!.EliminarUsuario(unCliente!);
            List<Usuario> usuariosObtenidos = repositorioUsuario.ObtenerUsuarios();
            Assert.AreEqual(usuariosObtenidos.Count, 0);
        }
    }
}
*/