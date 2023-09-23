﻿using Dominio;
using Dominio.Usuario;
using DataAccess.Interfaces;
using Servicios.Interfaces;

namespace Servicios
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        public ManejadorUsuario(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        public Usuario RegistrarUsuario(Usuario usuario)
        {
            if (ValidarUsuario(usuario)) {
                usuario = repositorioUsuario.AgregarUsuario((Cliente) usuario);
            }
            return usuario;
        }

        private static bool ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("El usuario no puede ser nulo");
            }
            if (usuario.DireccionEntrega == null || usuario.DireccionEntrega == "")
            {
                throw new ArgumentException("La direccion de entrega no puede ser nula o vacia");
            }
            if (usuario.CorreoElectronico == null || usuario.CorreoElectronico == "" || !usuario.CorreoElectronico.Contains('@'))
            {
                throw new ArgumentException("El email es incorrecto");
            }
            return true;
        }

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(id);
            usuarioObtenido.DireccionEntrega = direccionEntrega;
            repositorioUsuario.ActualizarUsuario(usuarioObtenido);
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            if (ValidarCompra(compra))
            {
                Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(id);
                usuarioObtenido.Compras.Add(compra);
                repositorioUsuario.ActualizarUsuario(usuarioObtenido);
            }
        }

        private bool ValidarCompra(Compra compra)
        {
            if (compra == null)
            {
                throw new ArgumentNullException("La compra no puede ser nula");
            }
            if (compra.Productos == null || compra.Productos.Count == 0)
            {
                throw new ArgumentException("La compra debe tener al menos un producto");
            }
            return true;
        }

        public List<Compra> ObtenerComprasDelUsuario(int id)
        {
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(id);
            return usuarioObtenido.Compras;
        }

        public Usuario ObtenerUsuario(int id)
        {
            return repositorioUsuario.ObtenerUsuario(id);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Cliente> clientes = repositorioUsuario.ObtenerClientes();
            List<Administrador> administradors = repositorioUsuario.ObtenerAdministradores();
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.AddRange(clientes);
            usuarios.AddRange(administradors);
            return usuarios;
        }

        public void EliminarUsuario(Usuario usuario)
        {
            repositorioUsuario.EliminarUsuario(usuario);
        }
    }
}