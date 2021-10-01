 using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pneustoreAPI.Models;
using System;
using System.Linq;

namespace pneustoreAPI.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(IHost app){

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                const int contador = 50;
                Random rand = new Random();

                string[] marca = new string[] { "Itaro", "Pirelli", "Firestone", "Continental", "Atlas", "Rinaldi", "Michelin" };
                string[] marcaImagem = new string[] { "https://static.pneustore.com.br/medias/sys_master/root/h0c/hc8/9004334055454/itaro-logo2.png",
                    "https://i.imgur.com/RRgw860.png",
                    "https://i.imgur.com/AtPMtQh.png",
                    "https://static.pneustore.com.br/medias/sys_master/root/h26/h78/9004333989918/continental-logo.png",
                    "https://static.pneustore.com.br/medias/sys_master/root/h0e/h03/9039887728670/ATLAS.png",
                    "https://static.pneustore.com.br/medias/sys_master/root/ha6/h2e/9009610326046/Rinaldi-logo.png",
                    "https://i.imgur.com/rqUfEtR.png"};
                string[] imagem = new string[] {
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h64/hb4/9032449261598/pneu-itaro-aro-16-it203-205-60r16-92h-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h58/h8c/8998750257182/pneu-itaro-aro-17-it301-215-45r17-91w-xl-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h73/hf2/8859852046366/pneu-continental-aro-16-conticrosscontact-lx2-225-70r16-103h-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h65/he7/9035424858142/pneu-continental-aro-16-contipremiumcontact-5-215-60r16-95h-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h0e/h21/8916699349022/pneu-continental-aro-16-contipowercontact-2-205-60r16-92h-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h2d/haa/8859639840798/pneu-continental-aro-16-contiprocontact-195-55r16-87v-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/h33/h34/8859635515422/pneu-continental-aro-16-conticrosscontact-at-205-60r16-92h-1.jpg",
                    "https://static.pneustore.com.br/medias/sys_master/images/images/hfd/hd1/8859640037406/pneu-continental-aro-16-conticrosscontact-lx2-265-70r16-112h-1.jpg"};
                string[] aro = new string[] { "13", "14", "15", "16", "17", "18", "22.5" };
                string[] modelo = new string[] { "ExtremeContact DW", "IT01", "Angel GT", "AM520", "BS32", "AGILIS" };
                string[] medida = new string[] { "265/70R16", "235/70R16", "195/75R16", "205/55R16", "205/75R16", "225/70R16", "100/80-16" };

                if (!context.Products.Any())
                {
                    for(int i = 0; i < contador; i++)
                    {
                        var marcaId = rand.Next(0, marca.Length);
                        context.Add(new Product
                        {
                            nome = $"Pneu {marca[marcaId]} {aro[rand.Next(0, aro.Length)]} {modelo[rand.Next(0, modelo.Length)]} {medida[rand.Next(0, medida.Length)]}",
                            imagemUrl = $"{imagem[rand.Next(0, imagem.Length)]}",
                            imagemUrlMarca = $"{marcaImagem[marcaId]}",
                            marca = $"{marca[marcaId]}",
                            preco = rand.NextDouble() * 1000
                        });
                    }
                }

                context.Database.Migrate();
                context.SaveChanges();
            }
        }
    }
}