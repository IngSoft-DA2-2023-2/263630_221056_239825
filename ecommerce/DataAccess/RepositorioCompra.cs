using DataAccess.Interfaces;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class RepositorioCompra : IRepositorioCompra
{
    private ECommerceContext _contexto;

    public RepositorioCompra(ECommerceContext contexto)
    {
        _contexto = contexto;
    }

    public List<Compra> RetornarTodas() => _contexto.Compras
        .Include(c => c.Productos)
        .ToList();

    public List<Compra> RetornarPorId(int id)
    {
        List<Compra> listaCompras = _contexto.Compras
            .Include(c => c.Productos)
            .Where(c => c.UsuarioId == id)
            .ToList();
        return listaCompras;
    }
}

