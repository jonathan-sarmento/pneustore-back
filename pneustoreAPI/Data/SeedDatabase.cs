using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace pneustoreAPI.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(IHost app){

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();

                context.Database.Migrate();
                context.SaveChanges();
            }
        }
    }
}