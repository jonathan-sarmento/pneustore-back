using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pneustoreAPI.Models;

namespace pneustoreAPI.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Product> Estabelecimentos { get; set; }
    }
}