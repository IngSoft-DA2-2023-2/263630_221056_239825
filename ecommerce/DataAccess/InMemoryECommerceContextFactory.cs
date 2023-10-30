using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public interface ECommerceContextFactory
{
    ECommerceContext CreateDbContext();
}

public class InMemoryECommerceContextFactory : ECommerceContextFactory
{
    public ECommerceContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ECommerceContext>();
        optionsBuilder.UseInMemoryDatabase("ECommerceDB");

        return new ECommerceContext(optionsBuilder.Options);
    }
}