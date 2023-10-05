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
}

