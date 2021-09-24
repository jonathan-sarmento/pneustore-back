 using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pneustoreAPI.Models;
using System.Linq;

namespace pneustoreAPI.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(IHost app){

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();

                if (!context.Products.Any())
                {
                    for(int i = 0; i < 50; i++)
                    {
                        context.Add(new Product
                        {
                            nome = $"Pneu{i}",
                            imagemUrl = $"https://static.pneustore.com.br/medias/sys_master/images/images/h5d/h7a/8859585544222/pneu-firestone-aro-16-destination-a-t-225-70r16-102-99s-1.jpg",
                            imagemUrlMarca = $"https://static.pneustore.com.br/medias/sys_master/images/images/hb9/hf5/8861805248542/brand-firestone-lg.svg",
                            marca = "Firestone",
                            preco = 928.63
                        });
                    }
                }

                context.Database.Migrate();
                context.SaveChanges();
            }
        }
    }
}