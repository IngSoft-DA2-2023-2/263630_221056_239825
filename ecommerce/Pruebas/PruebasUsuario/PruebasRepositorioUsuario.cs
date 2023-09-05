using Moq;
using Repositorio.Interfaces;
using Repositorio;
using Dominio.Usuario;
using Dominio;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasRepositorioUsuario
    {
        private Mock<IRepositorioUsuario>? mock;
        private RepositorioUsuario? repositorioUsuario;
        private Cliente? cliente;
        private Cliente? clienteIncorrecto;
        private Cliente? clienteNulo;
        private Compra? compra;
        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IRepositorioUsuario>(MockBehavior.Strict);
            repositorioUsuario = new RepositorioUsuario(mock.Object);
            cliente = new Cliente("martin@edlman.com.uy", "Zorrilla 124");
            cliente.Id = 1;
            clienteIncorrecto = new Cliente("Martín Edelman","");
            clienteNulo = null;
            compra = new Compra();
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!));
            repositorioUsuario!.AgregarUsuario(cliente!);
            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        public void RegistrarUsuarioIncorrecto()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteIncorrecto!));
            repositorioUsuario!.AgregarUsuario(clienteIncorrecto!);
            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        public void RegistrarUsuarioNulo()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteNulo!));
            repositorioUsuario!.AgregarUsuario(clienteNulo!);
            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        public void ObtenerUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.ObtenerUsuario(cliente!.Id));
            repositorioUsuario!.ObtenerUsuario(cliente!.Id);
            //Assert
            mock.VerifyAll();
        }
        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.ObtenerUsuarios());
            repositorioUsuario!.ObtenerUsuarios();
            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        public void ActualizarUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.ActualizarUsuario(1, "Julio Cesar 1247"));
            repositorioUsuario!.ActualizarUsuario(1, "Julio Cesar 1247");

            mock.VerifyAll();
        }

        [TestMethod]
        public void AgregarCompraAlUsuarioOk()
        {
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compra!));
            repositorioUsuario!.AgregarCompraAlUsuario(1, compra!);

            mock.VerifyAll();
        }

        [TestMethod]
        public void ObtenerComprasDelUsuarioOk()
        {
            mock!.Setup(x => x.ObtenerComprasDelUsuario(1));
            repositorioUsuario!.ObtenerComprasDelUsuario(1);

            mock.VerifyAll();
        }
    }
}
