using Moq;
using Manejador;
using Dominio.Usuario;
using Dominio;
using Manejador.Interfaces;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasManejadorUsuario
    {
        private Mock<IManejadorUsuario>? mock;
        private ManejadorUsuario? manejadorUsuario;
        private Cliente? cliente;
        private Cliente? clienteIncorrecto;
        private Cliente? clienteNulo;
        private int idCliente;
        private Compra? compra;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IManejadorUsuario>(MockBehavior.Strict);
            manejadorUsuario = new ManejadorUsuario(mock.Object);
            cliente = new Cliente("martin@edelman.com.uy", "Zorrilla 124");
            cliente.Id = 1;
            clienteIncorrecto = new Cliente("", "");
            clienteNulo = null;
            compra = new Compra();
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.RegistrarUsuario(cliente!));
            manejadorUsuario!.RegistrarUsuario(cliente!);

            //Assert
            mock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void RegistrarUsuarioIncorrecto()
        {
            // Act
            mock!.Setup(x => x.RegistrarUsuario(clienteIncorrecto!));
            manejadorUsuario!.RegistrarUsuario(clienteIncorrecto!);

            //Assert
            mock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void RegistrarUsuarioNulo()
        {
            // Act
            mock!.Setup(x => x.RegistrarUsuario(clienteNulo!));
            manejadorUsuario!.RegistrarUsuario(clienteNulo!);
            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        public void ObtenerUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            manejadorUsuario!.ObtenerUsuario(1);

            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            mock!.Setup(x => x.ObtenerUsuarios());
            manejadorUsuario!.ObtenerUsuarios();

            mock.VerifyAll();
        }
        [TestMethod]
        public void ActualizarUsuarioOk()
        {
            mock!.Setup(x => x.ActualizarUsuario(cliente!.Id, "Julio Cesar 1247"));
            manejadorUsuario!.ActualizarUsuario(cliente!.Id, "Julio Cesar 1247");

            mock.VerifyAll();
        }
        [TestMethod]
        public void AgregarCompraAlUsuarioOk()
        {
            mock!.Setup(x => x.AgregarCompraAlUsuario(cliente!.Id, compra!));
            manejadorUsuario!.AgregarCompraAlUsuario(cliente!.Id, compra!);

            mock.VerifyAll();
        }
        [TestMethod]
        public void ObtenerComprasDelUsuarioOk()
        {
            mock!.Setup(x => x.ObtenerComprasDelUsuario(cliente!.Id));
            manejadorUsuario!.ObtenerComprasDelUsuario(cliente!.Id);

            mock.VerifyAll();
        }
    }
}