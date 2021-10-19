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

            builder.Entity<EstabPneu>()
                .HasKey(tt => new { tt.ProductId, tt.EstabelecimentoId });
            builder.Entity<Cupom>()
                .HasKey(tt => new { tt.id});
           
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Estabelecimento> Estabelecimentos { get; set; }

        public DbSet<Carrinho> Carrinho { get; set; }

        public DbSet<EstabPneu> EstabPneu { get; set; }
        public DbSet<Cupom> Cupoms { get; set; }
    }
}