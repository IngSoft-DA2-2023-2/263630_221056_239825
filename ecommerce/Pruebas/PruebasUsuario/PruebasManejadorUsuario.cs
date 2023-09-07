using Moq;
using Manejador;
using Dominio.Usuario;
using Dominio;
using Manejador.Interfaces;
using Repositorio;
using Repositorio.Interfaces;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasManejadorUsuario
    {
        private Mock<IRepositorioUsuario>? mock;
        private ManejadorUsuario? manejadorUsuario;
        private Administrador? cliente;
        private List<Usuario>? listaClientes;
        private Cliente? clienteSinDireccion;
        private Cliente? clienteSinMail;
        private Cliente? clienteNulo;
        private Compra? compra;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IRepositorioUsuario>(MockBehavior.Strict);
            manejadorUsuario = new ManejadorUsuario(mock.Object);
            cliente = new Administrador("martin@edelman.com.uy", "Zorrilla 124");
            cliente.Id = 1;
            listaClientes = new List<Usuario>();
            listaClientes.Add(cliente);
            clienteSinDireccion = new Cliente("Martín@Edelman", "");
            clienteSinMail = new Cliente("Martin Edelman", "Zorrilla 124");
            clienteNulo = null;
            compra = new Compra();
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            var resultado = manejadorUsuario!.RegistrarUsuario(cliente!);

            //Assert
            Assert.AreEqual(cliente, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioIncorrecto()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteSinMail!)).Returns(cliente!);
            manejadorUsuario!.RegistrarUsuario(clienteSinMail!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioSinDireccion()
        {
            mock!.Setup(x => x.AgregarUsuario(clienteSinDireccion!)).Returns(cliente!);
            manejadorUsuario!.RegistrarUsuario(clienteSinDireccion!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegistrarUsuarioNulo()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteNulo!)).Returns(cliente!);
            manejadorUsuario!.RegistrarUsuario(clienteNulo!);
        }

        [TestMethod]
        public void ObtenerUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            var resultado = manejadorUsuario!.ObtenerUsuario(1);

            //Assert
            Assert.AreEqual(cliente, resultado);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ObtenerUsuarioNoExiste()
        {
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            manejadorUsuario!.RegistrarUsuario(cliente!);
            var resultado = manejadorUsuario!.ObtenerUsuario(2);
        }
        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuarios()).Returns(listaClientes!);
            manejadorUsuario!.RegistrarUsuario(cliente!);
            var resultado = manejadorUsuario!.ObtenerUsuarios();

            // Assert
            Assert.AreEqual(listaClientes!.Count, resultado.Count);
            Assert.AreEqual(listaClientes[0], resultado[0]);
        }
        [TestMethod]
        public void ActualizarUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            mock!.Setup(x => x.ActualizarUsuario(1, "Julio Cesar 1247"));
            manejadorUsuario!.RegistrarUsuario(cliente!);
            manejadorUsuario!.ActualizarUsuario(1, "Julio Cesar 1247");
            cliente!.DireccionEntrega = "Julio Cesar 1247";
            var resultado = manejadorUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.DireccionEntrega, resultado.DireccionEntrega);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActualizarUsuarioNoExiste()
        {
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            mock!.Setup(x => x.ActualizarUsuario(2, "Julio Cesar 1247"));
            manejadorUsuario!.RegistrarUsuario(cliente!);
            manejadorUsuario!.ActualizarUsuario(2, "Julio Cesar 1247");
        }

        [TestMethod]
        public void AgregarCompraAlUsuarioOk()
        {
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compra!));
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            manejadorUsuario!.AgregarCompraAlUsuario(1, compra!);
            cliente!.Compras.Add(compra!);
            var resultado = manejadorUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.Compras, resultado.Compras);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgregarCompraAlUsuarioNoExiste()
        {
            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
            mock!.Setup(x => x.AgregarCompraAlUsuario(It.IsAny<int>(), It.IsAny<Compra>()));
            manejadorUsuario!.RegistrarUsuario(cliente!);
            manejadorUsuario!.AgregarCompraAlUsuario(2, compra!);
        }

        [TestMethod]
        public void ObtenerComprasDelUsuarioOk()
        {
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compra!));
            mock!.Setup(x => x.ObtenerComprasDelUsuario(1));
            manejadorUsuario!.AgregarCompraAlUsuario(1, compra!);
            var resultado = manejadorUsuario!.ObtenerComprasDelUsuario(1);
            cliente!.Compras.Add(compra!);

            // Assert
            Assert.AreEqual(cliente!.Compras, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ObtenerComprasDelUsuarioNoExiste()
        {
            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compra!));
            mock!.Setup(x => x.ObtenerComprasDelUsuario(2));
            manejadorUsuario!.RegistrarUsuario(cliente!);
            manejadorUsuario!.AgregarCompraAlUsuario(1, compra!);
            var resultado = manejadorUsuario!.ObtenerComprasDelUsuario(2);
        }
    }
}