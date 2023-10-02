﻿using Dominio;
using Dominio.Usuario;

namespace Api.Dtos
{
    public class UsuarioCrearModelo
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public List<Compra> Compras { get; set; }
        public List<CategoriaRol> Rol { get; set; }
        public string contrasena { get; set; }

        public Usuario ToEntity()
        {
            return new Usuario(this.CorreoElectronico, this.DireccionEntrega, this.contrasena)
            {
                Id = this.Id,
                Compras = this.Compras,
                Roles = this.Rol,
            };
        }
    }
}