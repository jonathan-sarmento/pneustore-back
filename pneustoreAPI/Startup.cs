using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pneustoreAPI.API;
using pneustoreAPI.Data;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace pneustoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;


        }

       

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "pneustoreAPI", Version = "v1" ,
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Caio Silva",
                        Email = "caio.silva@grupolevel.com.br",
                        Url = new Uri("https://www.pneustore.com.br/")
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = $"{Path.Combine(AppContext.BaseDirectory, xmlFile)}";
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Pedro")));
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<Context>();

            services.AddTransient<IService<Product>, ProductService>();
            services.AddTransient<IService<Estabelecimento>, EstabelecimentoService>();
            services.AddTransient<CarrinhoService>();
            services.AddTransient<IAuthService<IdentityUser>, AuthService>();
            

            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pneustoreAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
                
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
                
            app.UseAuthentication();
            app.UseAuthorization();
           
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoles(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = Enum.GetNames(typeof(RoleType)); ;

            foreach (var role in roleNames)
            {
                var roleExist = RoleManager.RoleExistsAsync(role);
                if (!roleExist.Result)
                {

                    var roleResult = RoleManager.CreateAsync(new IdentityRole(role));
                    roleResult.Wait();
                }
            }
        }
    }
}
