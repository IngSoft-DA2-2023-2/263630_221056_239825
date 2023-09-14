﻿using Dominio;
using Dominio.Usuario;

namespace DataAccess.Interfaces
{
    public interface IRepositorioUsuario
    {
        Usuario AgregarUsuario(Usuario usuario);
        Usuario ObtenerUsuario(int id);
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(int id, string direccionEntrega);
        void AgregarCompraAlUsuario(int id, Compra compra);
        List<Compra> ObtenerComprasDelUsuario(int id);
        void EliminarUsuario(Usuario usuario);
    }
}
