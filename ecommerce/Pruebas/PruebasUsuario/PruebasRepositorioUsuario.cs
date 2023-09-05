using Moq;
using Repositorio.Interfaces;
using Repositorio;
using Dominio.Usuario;
using Dominio;
using System.Linq.Expressions;

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
            mock = new Mock<IRepositorioUsuario>();
            repositorioUsuario = new RepositorioUsuario(mock.Object);
            cliente = new Cliente("martin@edelman.com.uy", "Zorrilla 124");
            cliente.Id = 1;
            clienteIncorrecto = new Cliente("Martín Edelman","");
            clienteNulo = null;
            compra = new Compra();
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>()));
            mock!.Object.AgregarUsuario(new Cliente("martin@edelman.com.uy", "Zorrilla 124"));
            
            // Assert
            mock.Verify(x => x.AgregarUsuario(It.Is<Usuario>(v => v.CorreoElectronico.Equals(cliente!.CorreoElectronico))), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioIncorrecto()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteIncorrecto!));
            repositorioUsuario!.AgregarUsuario(clienteIncorrecto!);
            //Assert
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
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
            mock!.Setup(x => x.ObtenerUsuario(It.IsAny<int>()));
            mock!.Object.AgregarUsuario(cliente!);
            mock!.Object.ObtenerUsuario(1);

            // Assert
            mock.Verify(x => x.ObtenerUsuario(It.Is<int>(v => v.Equals(cliente!.Id))), Times.AtLeastOnce);
        }
        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            mock!.Setup(x => x.ObtenerUsuarios());
            mock!.Object.AgregarUsuario(cliente!);
            mock!.Object.ObtenerUsuarios();

            // Assert
            mock.Verify(x => x.ObtenerUsuarios());
        }

        [TestMethod]
        public void ActualizarUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.ActualizarUsuario(It.IsAny<int>(), It.IsAny<string>()));
            mock!.Object.AgregarUsuario(cliente!);
            mock!.Object.ActualizarUsuario(1, "Julio Cesar 1247");
            cliente!.DireccionEntrega = "Julio Cesar 1247";

            // Assert
            mock.Verify(x => x.ActualizarUsuario(It.Is<int>(v => v.Equals(cliente!.Id)), It.Is<string>(v => v.Equals(cliente!.DireccionEntrega))), Times.AtLeastOnce);
        }

        [TestMethod]
        public void AgregarCompraAlUsuarioOk()
        {
            mock!.Setup(x => x.AgregarCompraAlUsuario(It.IsAny<int>(), It.IsAny<Compra>()));
            mock!.Object.AgregarUsuario(cliente!);
            mock!.Object.AgregarCompraAlUsuario(1, compra!);
            cliente!.Compras.Add(compra!);

            // Assert
            mock.Verify(x => x.AgregarCompraAlUsuario(It.Is<int>(v => v.Equals(cliente!.Id)), It.Is<Compra>(v => v.Equals(cliente!.Compras[0]))), Times.AtLeastOnce);
        }

        [TestMethod]
        public void ObtenerComprasDelUsuarioOk()
        {
            mock!.Setup(x => x.ObtenerComprasDelUsuario(It.IsAny<int>()));
            mock!.Object.AgregarUsuario(cliente!);
            mock!.Object.AgregarCompraAlUsuario(1, compra!);
            mock!.Object.ObtenerComprasDelUsuario(1);
            cliente!.Compras.Add(compra!);

            // Assert
            mock!.VerifyAll();
        }
    }
}
