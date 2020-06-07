using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDb: DbContext
    {
            public DbSet<Product> Products { get; set; }

            public DbSet<Section> Sections { get; set; }

            public DbSet<Brand> Brands { get; set; }

            public DbSet<Employee> Employees { get; set; }

            public WebStoreDb(DbContextOptions<WebStoreDb> Options) : base(Options) { } 
    }
}
