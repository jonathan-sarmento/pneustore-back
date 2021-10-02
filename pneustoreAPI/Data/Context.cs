using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pneustoreAPI.Models;

namespace pneustoreAPI.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Carrinho>()
                .HasKey(tt => new { tt.ProductId, tt.UserId });
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Product> Estabelecimentos { get; set; }

        public DbSet<Carrinho> Carrinho { get; set; }
    }
}