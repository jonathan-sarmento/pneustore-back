using Microsoft.EntityFrameworkCore;
using pneustoreAPI.Models;

namespace pneustoreapi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options){ }

        DbSet<Product> Products { get; set; }
    }
}