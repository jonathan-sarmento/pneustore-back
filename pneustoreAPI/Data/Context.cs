using Microsoft.EntityFrameworkCore;
using pneustoreAPI.Models;

namespace pneustoreAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options){ }

        public DbSet<Product> Products { get; set; }
        public DbSet<Product> Estabelecimentos { get; set; }
    }
}