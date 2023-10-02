using Moq;
using DataAccess.Interfaces;
using DataAccess;
using Dominio.Usuario;
using Dominio;
using System.Linq.Expressions;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasRepositorioUsuario
    {
        private Mock<ECommerceContext>? mock;
        private RepositorioUsuario? repositorioUsuario;
        private Usuario? cliente;
        private List<Usuario>? listaClientes;
        private Usuario? clienteSinDireccion;
        private Usuario? clienteSinMail;
        private Usuario? clienteNulo;
        private Compra? compra;
        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ECommerceContext>();
            repositorioUsuario = new RepositorioUsuario(mock.Object);
            cliente = new Usuario("martin@edelman.com.uy", "Zorrilla 124", "Password123");
            cliente.Id = 1;
            listaClientes = new List<Usuario>();
            listaClientes.Add(cliente);
            clienteSinDireccion = new Usuario("Martín@Edelman", "", "Password123");
            clienteSinMail = new Usuario("Martin Edelman", "Zorrilla 124", "Password123");
            clienteNulo = null;
            compra = new Compra();
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.Set<Usuario>());
            mock!.Setup(x => x.SaveChanges());
            var resultado = repositorioUsuario!.AgregarUsuario(cliente!);

            // Assert
            Assert.AreEqual(cliente, resultado);
        }

        [TestMethod]
        public void ObtenerUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.AgregarUsuario(cliente!);
            var resultado = repositorioUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente, resultado);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ObtenerUsuarioNoExiste()
        {
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.AgregarUsuario(cliente!);
            var resultado = repositorioUsuario!.ObtenerUsuario(2);
        }
        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.AgregarUsuario(cliente!);
            var resultado = repositorioUsuario!.ObtenerUsuarios();

            // Assert
            Assert.AreEqual(listaClientes!.Count, resultado.Count);
            Assert.AreEqual(listaClientes[0], resultado[0]);
        }

        [TestMethod]
        public void ActualizarUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.AgregarUsuario(cliente!);
            cliente!.DireccionEntrega = "Julio Cesar 1247";
            repositorioUsuario!.ActualizarUsuario(cliente!);
            var resultado = repositorioUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.DireccionEntrega, resultado.DireccionEntrega);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActualizarUsuarioNoExiste()
        {
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.AgregarUsuario(cliente!);
            repositorioUsuario!.ActualizarUsuario(clienteNulo!);
        }

        [TestMethod]
        public void EliminarUsuarioCorrecto()
        {
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.AgregarUsuario(cliente!);
            repositorioUsuario!.EliminarUsuario(cliente!);
            List<Usuario> usuarios = repositorioUsuario!.ObtenerUsuarios();

            Assert.IsTrue(usuarios.Count() == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EliminarUsuarioIncorrecto()
        {
            Usuario nuevoCliente = new Usuario("martin@edelman.com.uy", "Zorrilla 142", "Password123")
            {
                Id = 100,
                Rol = new List<CategoriaRol>() {
                    CategoriaRol.Cliente
                }
            };
            mock!.Setup(x => x.Set<Usuario>());
            repositorioUsuario!.EliminarUsuario(nuevoCliente!);
        }
    }
}
